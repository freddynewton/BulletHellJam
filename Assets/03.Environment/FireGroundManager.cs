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
        int[] areasToIgnite = new int[2];

        while (true)
        {
            // Get two areas to ignite
            int holder1 = UnityEngine.Random.Range(0, 6);
            int holder2 = UnityEngine.Random.Range(0, 6);
            while (Array.IndexOf(areasToIgnite, holder1) > -1)
            {
                holder1 = UnityEngine.Random.Range(0, 6);
            }
            while (Array.IndexOf(areasToIgnite, holder2) > -1 || holder1 == holder2)
            {
                holder2 = UnityEngine.Random.Range(0, 6);
            }
            areasToIgnite[0] = holder1;
            areasToIgnite[1] = holder2;

            // Ignite two areas
            foreach (int index in areasToIgnite)
            {
                fireAreas[index].SetActive(true);
            }

            yield return new WaitForSeconds(ignitionInterval);
        }
    }
}
