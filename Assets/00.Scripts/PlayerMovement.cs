
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
    [SerializeField] private float dashTimeCooldown = 3f;

    private float currentDashTimeCooldown;
    private float CurrentDashTime { get; set; }

    public Vector2 dashStartPoint { get; private set; } = Vector2.zero;

    [SerializeField] private float dashSpeed;

    private bool isDashing = false;
    #endregion

    AudioStation audioStation;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        dashStartPoint = transform.position;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        audioStation = AudioStation.Instance;
    }


    private void Update()
    {
        if (playerManager.currentHealth > 0)
        {
            ApplyMovement();
        }
    }

    private void ApplyMovement()
    {
        GetDirectionVector();

        Dash();

        if (CurrentDashTime <= 0)
        {
            ApplyMovementVector();
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            spriteRenderer.flipX = true;
        } else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void ApplyMovementVector()
    {
        Vector2 adjustedMovement = InputVector * movementSpeed;
        rigidbody2D.MovePosition((Vector2)transform.position + adjustedMovement * Time.deltaTime);
        animator.SetBool("isWalking", InputVector != Vector2.zero);
    }

    private void GetDirectionVector()
    {
        InputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Dash()
    {
        if (CurrentDashTime <= 0)
        {
            playerManager.isInvincibleBullet = false;
            isDashing = false;
        }

        if (CurrentDashTime <= 0 && Input.GetKeyDown(KeyCode.Space) && playerManager.currentHealth > 0 && currentDashTimeCooldown <= 0)
        {
            StartCoroutine(playerManager.SetFalldownInvincible(dashTime));
            StartCoroutine(playerManager.SetBulletInvincible(dashTime));
            dashStartPoint = transform.position;
            CurrentDashTime = dashTime;
            currentDashTimeCooldown = dashTimeCooldown;

            // Player is dashing
            var tempInputVec = InputVector;
            rigidbody2D.velocity = tempInputVec * dashSpeed * Time.deltaTime;
            AudioStation.Instance.StartNewRandomSFXPlayer(audioStation.chefSFX.asset[1].audioClips, pitchMin: 0.9f, pitchMax: 1.1f);
        }
        else if (CurrentDashTime > 0)
        {
            isDashing = true;
            CurrentDashTime -= Time.deltaTime;
        }

        if (currentDashTimeCooldown > 0)
        {
            currentDashTimeCooldown -= Time.deltaTime;
        }

        // Doesnt work
        //animator.SetBool("isDashing", isDashing);
    }
}
