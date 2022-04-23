using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private PlayerManager playerManager;

    AudioStation audioStation;

    private void Start()
    {
        audioStation = AudioStation.Instance;
        audioStation.StartNewRandomSFXPlayer(audioStation.firebulletSFX.asset[0].audioClips, pitchMin: 0.9f, pitchMax: 1.1f);
    }

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
