using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownController : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerMovement playerMovement;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerManager == null || playerMovement == null)
            {
                playerMovement = collision.GetComponent<PlayerMovement>();
                playerManager = collision.GetComponent<PlayerManager>();
            }

            if (!playerManager.isInvincible)
            {
                playerManager.GetDamage(1);
                playerMovement.transform.position = playerMovement.dashStartPoint;
            }
        }
    }
}
