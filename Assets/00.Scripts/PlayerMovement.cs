
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private float movementSpeed;

    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;

    private Vector2 DirectionVector { get; set; }

    private bool IsMoving { get; set; }

    private Vector2 InputVector { get; set; }

    private float CurrentDashTime { get; set; }

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
        if (CurrentDashTime <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            //TODO: Change CollisionLayer

            CurrentDashTime = dashTime;
            IsMoving = true;

            // Player is dashing
            var tempInputVec = InputVector;
            rigidbody2D.velocity = tempInputVec * dashSpeed;
        }
        else if (CurrentDashTime >= 0)
        {
            //TODO: Change CollisionLayer

            CurrentDashTime -= Time.deltaTime;
        }
    }

}
