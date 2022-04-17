using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;

    private void OnEnable()
    {
        // TODO: Initialize bullet's fire direction

        rb.velocity = Vector2.down * moveSpeed;
    }
}
