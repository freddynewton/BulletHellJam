using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private PlayerManager playerManager;

    private void Update()
    {
        // Reset position
        if (transform.position.y < -10f || transform.position.y > 10f || transform.position.x < -10f || transform.position.x > 10f)
        {
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = -transform.up * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: JUST USE DIFFERENT PHYSICS LAYER
        if (other.tag == "BulletDetector")
        {
            if (playerManager == null)
            {
                playerManager = other.GetComponentInParent<PlayerManager>();
            }

            playerManager.GetDamage(1);

            // TODO: DESTROY BULLET - RELEASE TO POOL
        }

    }
}
