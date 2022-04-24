using System;
using System.Collections;
using UnityEngine;

public class FireGroundManager : MonoBehaviour
{
    // Store the postions of every fire grounds
    private GameObject[] fireAreas;
    [SerializeField] private float ignitionInterval;

    private void Start()
    {
        fireAreas = new GameObject[6];
        for (int i = 0; i < fireAreas.Length; i++)
        {
            fireAreas[i] = transform.GetChild(i).gameObject;
        }

        StartCoroutine(IgniteGround());
    }

    private IEnumerator IgniteGround()
    {
        while (true)
        {
            // Ignite two areas
            int areaToIgnite = UnityEngine.Random.Range(0, 6);
            Debug.Log(areaToIgnite);

            yield return new WaitForSeconds(ignitionInterval);
        }
    }
}
