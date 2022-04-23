
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    #region Walk Variables

    private Vector2 InputVector { get; set; }
    private Vector2 DirectionVector { get; set; }
    [SerializeField] private float movementSpeed;
    private bool IsMoving { get; set; }

    #endregion

    #region Dash Variables

    [SerializeField] private float dashTime;
    private float CurrentDashTime { get; set; }
    [SerializeField] private float dashSpeed;
    private bool isDashing = false;
    public UnityEvent onDashStart;
    [SerializeField] private LayerMask wallMask;

    #endregion

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
        Vector2 currentPos = rigidbody2D.position;
        Vector2 adjustedMovement = InputVector * movementSpeed;
        rigidbody2D.MovePosition(currentPos + adjustedMovement * Time.fixedDeltaTime);
    }

    private void GetDirectionVector()
    {
        InputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        IsMoving = (InputVector != Vector2.zero) ? true : false;
    }

    private void Dash()
    {
        // Show the destination of dash
        Debug.DrawRay(transform.position, InputVector * dashTime * dashSpeed, Color.red);

        if (CurrentDashTime <= 0 && isDashing)
        {
            EnableCollider();
            isDashing = false;
        }

        if (CurrentDashTime <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            //TODO: Change CollisionLayer

            CurrentDashTime = dashTime;
            IsMoving = true;

            // Player is dashing
            var tempInputVec = InputVector;
            rigidbody2D.velocity = tempInputVec * dashSpeed;

            isDashing = true;
            onDashStart.Invoke();
        }
        else if (CurrentDashTime >= 0)
        {
            //TODO: Change CollisionLayer

            CurrentDashTime -= Time.deltaTime;
        }
    }

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
}
