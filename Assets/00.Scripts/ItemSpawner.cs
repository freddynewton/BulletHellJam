using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;

    public List<SpawnLocation> ItemSpawnLocations = new List<SpawnLocation>();

    private GameObject currentSpawnedItem;

    private void Update()
    {
        if (currentSpawnedItem == null)
        {
            currentSpawnedItem = Instantiate(ItemPrefab, transform);
            currentSpawnedItem.transform.position = GetSpawnPosition();
        }
    }

    private Vector2 GetSpawnPosition()
    {
        var randomSpawnLocation = ItemSpawnLocations[Random.Range(0, ItemSpawnLocations.Count)];

        return randomSpawnLocation.SpawnPositionArea + (Vector2)Random.insideUnitSphere * randomSpawnLocation.Size;
    }

    private void OnDrawGizmos()
    {
        foreach (var spawnLocation in ItemSpawnLocations)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(spawnLocation.SpawnPositionArea, spawnLocation.Size);
        }
    }
}

[System.Serializable]
public struct SpawnLocation
{
    public float Size;
    public Vector2 SpawnPositionArea;
}
