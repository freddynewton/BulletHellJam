using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletPatternSpawner : MonoBehaviour
{
    public int BulletAmount;
    public int Waves;
    public float WaveTimer;
    public float BulletSpawnTimeOffset;
    protected int currentWave;

    protected BulletSpawner bulletSpawner;

    public void Init(BulletSpawner bulletSpawner)
    {
        this.bulletSpawner = bulletSpawner;
        StartCoroutine(bulletPatternShot());
    }

    public abstract IEnumerator bulletPatternShot();
}
