
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
            CurrentDashTime = dashTime;
            IsMoving = false;

            // Player is dashing
            // statHandler.canInteract = true;
            var tempInputVec = InputVector;
            //Rigidbody2D.velocity = tempInputVec * PlayerManager.Instance.BaseDashSpeed;

            // rb.AddForce(inputVector * statHandler.dashSpeed, ForceMode2D.Impulse);
        }
        else if (CurrentDashTime >= 0)
        {
            CurrentDashTime -= Time.deltaTime;
        }
    }

}
