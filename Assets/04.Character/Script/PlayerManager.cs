using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void GetHurt(int damage)
    {
        if (currentHealth - damage <= 0)
        {
            Dead();
            return;
        }
        currentHealth -= damage;
        Debug.Log(currentHealth);
    }

    private void Dead()
    {
        OnPlayerDeath?.Invoke();
        GetComponent<CapsuleCollider2D>().enabled = false;
        currentHealth = 0;
        Debug.Log(currentHealth);
    }
}
