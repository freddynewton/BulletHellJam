using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        // Reset position
        if (transform.position.y < -20f)
        {
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * moveSpeed;
    }

    public void Initialize()
    {
        Vector3 direction = new Vector3(0f, 0f, Random.Range(-40f, 40f));
        transform.Rotate(direction);
    }
}
