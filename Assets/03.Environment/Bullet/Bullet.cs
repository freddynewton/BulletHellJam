using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    public Vector2 DirectionVector;
    private PlayerManager playerManager;
    public BulletSpawner bulletSpawner;
    public float BulletLifeTime;

    private float currentBulletLifeTime;
    private bool isMoving;

    AudioStation audioStation;

    public void OnGetBullet()
    {
        currentBulletLifeTime = BulletLifeTime;
        isMoving = true;
        gameObject.transform.localScale = Vector3.one;
        gameObject.SetActive(true);
    }

    public void OnReleaseBullet()
    {
        isMoving = false;
        rb.velocity = Vector2.zero;

        gameObject.transform.LeanScale(new Vector3(1.2f, 0.8f), 0.2f).setEaseInBack().setOnComplete(() =>
        {
            gameObject.transform.LeanScale(new Vector3(0.8f, 1.2f), 0.2f).setEaseInBack().setOnComplete(() =>
            {
                gameObject.transform.LeanScale(Vector3.zero, 0.15f).setEaseInBack().setOnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            });
        });
    }

    private void Start()
    {
        audioStation = AudioStation.Instance;
        audioStation.StartNewRandomSFXPlayer(audioStation.firebulletSFX.asset[0].audioClips, pitchMin: 0.9f, pitchMax: 1.1f);
    }

    private void Update()
    {
        if (isMoving)
        {
            currentBulletLifeTime -= Time.deltaTime;
            rb.velocity = DirectionVector * moveSpeed * Time.deltaTime;
        }

        if (currentBulletLifeTime <= 0)
        {
            bulletSpawner.BasicBulletPool.Release(this);
        }
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
            bulletSpawner.BasicBulletPool.Release(this);
            // TODO: DESTROY BULLET - RELEASE TO POOL
        }

        if (other.tag != "Bullet")
        {
            if (bulletSpawner == null)
            {
                bulletSpawner = FindObjectOfType<BulletSpawner>();
            }

            bulletSpawner.BasicBulletPool.Release(this);
        }
    }
}
