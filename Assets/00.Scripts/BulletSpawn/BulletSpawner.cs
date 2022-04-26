using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public GameObject BulletPrefab;
    public List<GameObject> bulletPatternSpawners = new List<GameObject>();
    public PlayerManager playerManager { get; private set; }

    public ObjectPool<Bullet> BasicBulletPool
    {
        get
        {
            if (m_basicBulletPool == null)
            {
                m_basicBulletPool = new ObjectPool<Bullet>(() =>
                {
                    var go = Instantiate(BulletPrefab, transform);
                    var bullet = go.GetComponent<Bullet>();

                    return bullet;
                }, bullet =>
                {
                    bullet.transform.parent = transform;
                    bullet.OnGetBullet();
                }, bullet =>
                {
                    bullet.OnReleaseBullet();
                }, bullet =>
                {
                    Destroy(bullet.gameObject);
                }, false, 200, 2000);
            }
            return m_basicBulletPool;
        }
    }

    private ObjectPool<Bullet> m_basicBulletPool;

    public float BulletSpawnRadiusMax = 40;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();

        for (int i = 0; i < 2; i++)
        {
            SpawnRandomBulletPattern();
        }
    }

    public void SpawnRandomBulletPattern()
    {
        var pattern = bulletPatternSpawners[Random.Range(0, bulletPatternSpawners.Count)];
        var go = Instantiate(pattern, Vector2.zero, Quaternion.identity, transform);
        go.transform.position = (Vector2)transform.position + (Vector2)Random.onUnitSphere * BulletSpawnRadiusMax;
        go.GetComponent<BulletPatternSpawner>().Init(this);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BulletSpawnRadiusMax);
        //Gizmos.DrawWireSphere(transform.position, BulletSpawnRadiusMin);
    }

}