using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{

    public static Vector2 SpawnPoint;

    public GameObject spawnPoint;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            SpawnPoint = spawnPoint.transform.position;

        }
    }

}
