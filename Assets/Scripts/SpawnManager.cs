using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject PrefabToSpawn;

    public float startDelay = 2, repeatRate = 2;

    bool IsActiveTheGame;

    private Vector3 spawnLocation = new Vector3(25,0,0);

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacule", startDelay, repeatRate);
        playerController = GameObject.Find("Player")
            .GetComponent<PlayerController>();;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacule()
    {
        if(/*GameObject.Find("Player")
            .GetComponent<PlayerController>().*/playerController.GetIsGameOver() )
        {
            CancelInvoke("SpawnObstacule");return;
        }

        Instantiate(PrefabToSpawn, spawnLocation, PrefabToSpawn.transform.rotation);
    }
}
