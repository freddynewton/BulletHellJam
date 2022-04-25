using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;

    public float Size;

    private GameObject currentSpawnedItem;

    private void LateUpdate()
    {
        if (currentSpawnedItem == null)
        {
            currentSpawnedItem = Instantiate(ItemPrefab, transform);
            currentSpawnedItem.transform.position = GetSpawnPosition();
        }
    }

    private Vector2 GetSpawnPosition()
    {
        return (Vector2)transform.position + (Vector2)Random.insideUnitSphere * Size;
    }

    private void OnDrawGizmos()
    {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, Size);
    }
}
