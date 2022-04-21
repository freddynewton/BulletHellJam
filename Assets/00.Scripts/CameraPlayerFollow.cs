using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    public GameObject Player;

    public float CameraDistance = -10f;
    public float FollowSpeed;

    private void Awake()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, CameraDistance);
    }

    private void Update()
    {
        //if (Vector2.Distance((Vector2)transform.position + FollowZone, (Vector2)Player.transform.position) < )

        var newPosition = Vector3.Lerp((Vector2)transform.position, (Vector2)Player.transform.position, FollowSpeed * Time.deltaTime);
        newPosition.z = CameraDistance;
        transform.position = newPosition;
    }
}
