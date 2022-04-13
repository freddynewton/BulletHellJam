using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 DirectionVector { get; set; }
    [SerializeField] private Rigidbody2D Rigidbody2D;

    private bool isMoving { get; set; }
    private Vector2 InputVector { get; set; }
    private float currentDashTime { get; set; }

    private void Update()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        GetDirectionVector();

        Dash();

        if (currentDashTime <= 0)
        {
            ApplyMovementVector();
        }
    }

    private void ApplyMovementVector()
    {
        Vector2 currentPos = Rigidbody2D.position;
        Vector2 adjustedMovement = InputVector * PlayerManager.Instance.BaseMovementSpeed;
        Rigidbody2D.MovePosition(currentPos + adjustedMovement * Time.fixedDeltaTime);
    }

    private void GetDirectionVector()
    {
        InputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isMoving = (InputVector != Vector2.zero) ? true : false;
    }

    private void Dash()
    {
        if (currentDashTime <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            currentDashTime = PlayerManager.Instance.DashTime;
            isMoving = false;

            // Player is dashing
            // statHandler.canInteract = true;
            var tempInputVec = InputVector;
            Rigidbody2D.velocity = tempInputVec * PlayerManager.Instance.BaseDashSpeed;

            // rb.AddForce(inputVector * statHandler.dashSpeed, ForceMode2D.Impulse);
        }
        else if (currentDashTime >= 0)
        {
            currentDashTime -= Time.deltaTime;
        }
    }

}
