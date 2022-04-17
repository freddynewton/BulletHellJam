using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            // TODO: Player death event

            Debug.Log("Die");
        }
    }
}
