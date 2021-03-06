using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownController : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerManager == null || playerMovement == null)
            {
                playerMovement = collision.GetComponent<PlayerMovement>();
                playerManager = collision.GetComponent<PlayerManager>();
            }

            if (!playerManager.isInvicibleFallDamage)
            {
                if (!playerManager.isInvincibleBullet && !playerManager.isInvincibleBullet)
                {
                    playerManager.GetDamage(1);
                }

                if (playerManager.currentHealth > 0)
                {
                    playerMovement.transform.position = playerMovement.dashStartPoint;
                }
            }
        }
    }
}
