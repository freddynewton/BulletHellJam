using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public event Action OnPlayerDeath;
    public event Action OnPlayerHealthChange;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damageInvincibleTime = 2;
    public PlayerMovement playerMovement;

    public int DamageInvincibleTime => damageInvincibleTime;

    public bool isInvincibleBullet { get; set; }
    public bool isInvicibleFallDamage { get; set; }

    public int MaxHealth => maxHealth;
    public int currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    AudioStation audioStation;

    private void Start()
    {
        audioStation = AudioStation.Instance;
    }


    public void GetDamage(int damage)
    {
        if (!isInvincibleBullet && !isInvicibleFallDamage)
        {
            currentHealth -= damage;
            OnPlayerHealthChange?.Invoke();
            Debug.Log(currentHealth);

            if (currentHealth <= 0)
            {
                Dead();
                return;
            }
            else
                audioStation.StartNewRandomSFXPlayer(audioStation.chefSFX.asset[2].audioClips, pitchMin: -0.9f, pitchMax: 1.1f, parent: transform);
        }
    }

    public void GetHealth(int amount)
    {
        currentHealth += amount;
        OnPlayerHealthChange?.Invoke();
        Debug.Log(currentHealth);
    }

    public IEnumerator SetFalldownInvincible(float time)
    {
        isInvicibleFallDamage = true;

        yield return new WaitForSecondsRealtime(time);

        isInvicibleFallDamage = false;
    }

    public IEnumerator SetBulletInvincible(float time)
    {
        isInvincibleBullet = true;

        yield return new WaitForSecondsRealtime(time);

        isInvincibleBullet = false;
    }

    private void Dead()
    {
        OnPlayerDeath?.Invoke();
        transform.GetChild(0).gameObject.SetActive(false);
        audioStation.StartNewRandomSFXPlayer(audioStation.chefSFX.asset[3].audioClips, parent: transform);
    }
}
