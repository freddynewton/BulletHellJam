using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        // Reset position
        if (transform.position.y < -20f || transform.position.y > 20f || transform.position.x < -20f || transform.position.x > 20f)
        {
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = -transform.up * moveSpeed;
    }
}
