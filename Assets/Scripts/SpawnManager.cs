using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnee;
    public float spawnDelay;
    public float spawnInterval;
    private PlayerController playerControllerScript;


    void Start()
    {
        Invoke("Spawn", spawnDelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    void Spawn()
    {

        if (playerControllerScript.gameOver == false)
        {
            Instantiate(spawnee, transform.position, spawnee.transform.rotation);
            Invoke("Spawn", spawnInterval);
        }
    }
}
