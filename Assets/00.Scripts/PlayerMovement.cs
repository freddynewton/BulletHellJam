
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    public PlayerManager playerManager;

    #region Walk Variables
    [Header("Movement")]
    [SerializeField] private float movementSpeed;

    private Vector2 InputVector { get; set; }
    #endregion

    #region Dash Variables
    [Header("Dash")]
    [SerializeField] private float dashTime;

    private float CurrentDashTime { get; set; }

    public Vector2 dashStartPoint { get; private set; } = Vector2.zero;

    [SerializeField] private float dashSpeed;

    private bool isDashing = false;
    #endregion

    AudioStation audioStation;

    private void Start()
    {
        audioStation = AudioStation.Instance;
    }

    private void Update()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        GetDirectionVector();

        Dash();

        if (CurrentDashTime <= 0)
        {
            ApplyMovementVector();
        }
    }

    private void ApplyMovementVector()
    {
        Vector2 adjustedMovement = InputVector * movementSpeed;
        rigidbody2D.MovePosition((Vector2)transform.position + adjustedMovement * Time.deltaTime);
    }

    private void GetDirectionVector()
    {
        InputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Dash()
    {
        // Show the destination of dash
        // Debug.DrawRay(transform.position, InputVector * dashTime * dashSpeed, Color.red);

        if (CurrentDashTime <= 0 && !isDashing)
        {
            playerManager.isInvincibleBullet = false;
            isDashing = false;
        }

        if (CurrentDashTime <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(playerManager.SetFalldownInvincible(dashTime));
            StartCoroutine(playerManager.SetBulletInvincible(dashTime));
            CurrentDashTime = dashTime;

            // Player is dashing
            var tempInputVec = InputVector;
            rigidbody2D.velocity = tempInputVec * dashSpeed;
            AudioStation.Instance.StartNewRandomSFXPlayer(audioStation.chefSFX.asset[1].audioClips, pitchMin: 0.9f, pitchMax: 1.1f, parent: transform);
        }
        else if (CurrentDashTime > 0)
        {
            isDashing = true;
            CurrentDashTime -= Time.deltaTime;
        }
    }

    /*
    private void EnableCollider()
    {
        // Check if the destination of dash is in wall (With overlapCapsule)
        Vector2 destination = (Vector2)transform.position + InputVector * dashSpeed * dashTime;
        Vector2 size = GetComponent<CapsuleCollider2D>().size;
        CapsuleDirection2D direction = GetComponent<CapsuleCollider2D>().direction;

        Collider2D[] walls = Physics2D.OverlapCapsuleAll(destination, size, direction, 0f, wallMask);

        Debug.Log(walls);

        // If true then push player onto the platform
        if (walls.Length > 0)
        {
            // Push player onto the platform
        }

        // Enable Player and BulletDetector collider
    }
    */
}
