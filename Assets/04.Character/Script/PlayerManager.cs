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

    Animator animator;

    public int DamageInvincibleTime => damageInvincibleTime;

    public bool isInvincibleBullet { get; set; }
    public bool isInvicibleFallDamage { get; set; }

    public int MaxHealth => maxHealth;
    public int currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    AudioStation audioStation;

    private void Start()
    {
        audioStation = AudioStation.Instance;
    }

    private void OnEnable()
    {
        FireGroundManager.onPlayerStepOnFire += Dead;
    }

    private void OnDisable()
    {
        FireGroundManager.onPlayerStepOnFire -= Dead;
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
            {
                animator.SetBool("isHit", true);
                audioStation.StartNewRandomSFXPlayer(audioStation.chefSFX.asset[2].audioClips, pitchMin: 0.9f, pitchMax: 1.1f);
            }
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
        animator.SetBool("isDead", true);
        audioStation.StartNewRandomSFXPlayer(audioStation.chefSFX.asset[3].audioClips);
    }

    public void PlayFootstepSFX()
    {
        audioStation.StartNewRandomSFXPlayer(audioStation.chefSFX.asset[0].audioClips, pitchMin: 0.9f, pitchMax: 1.1f);
    }

    public void StopHitAnim()
    {
        animator.SetBool("isHit", false);
    }
}
