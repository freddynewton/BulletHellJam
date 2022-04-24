using System;
using UnityEngine;

public class FireGroundManager : MonoBehaviour
{
    public static event Action onPlayerStepOnFire;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            onPlayerStepOnFire?.Invoke();
        }
    }
}
