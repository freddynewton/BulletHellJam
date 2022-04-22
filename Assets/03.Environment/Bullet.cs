using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;

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
        if (other.tag == "Player")
            other.GetComponent<PlayerManager>().GetDamage(damage);
    }
}
