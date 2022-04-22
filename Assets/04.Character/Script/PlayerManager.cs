using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletDetector;
    public event Action OnPlayerDeath;
    public event Action OnPlayerHealthChange;
    [SerializeField] private int maxHealth;

    public int MaxHealth => maxHealth;
    public int currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        OnPlayerHealthChange?.Invoke();
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Dead();
            return;
        }
    }

    public void GetHealth(int amount)
    {
        currentHealth += amount;
        OnPlayerHealthChange?.Invoke();
        Debug.Log(currentHealth);
    }

    private void Dead()
    {
        OnPlayerDeath?.Invoke();
        bulletDetector.SetActive(false);
    }
}
