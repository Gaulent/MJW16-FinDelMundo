using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ISpawnManager
{
    //public GameObject PrefabToSpawn;

    public float startDelay = 2, repeatRate = 2;

    [SerializeField] List<GameObject> SpawnTypes;

    [SerializeField] private int SpawnRange = 20;
    bool IsActiveTheGame;

    private IPlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnObstacule", startDelay, repeatRate);
        playerController = GameObject.Find("Player")
            .GetComponent<IPlayerController>();;
    }

    void SpawnObstacule()
    {
        if(!IsActiveTheGame)
        {
            CancelInvoke("SpawnObstacule");return;
        }

        GameObject PrefabToSpawn = SpawnTypes[Random.Range(0, SpawnTypes.Count)];
        Instantiate(SpawnTypes[Random.Range(0, SpawnTypes.Count)], 
            new Vector3(Random.Range(-SpawnRange,SpawnRange),0 ,40), 
            PrefabToSpawn.transform.rotation
        );
    }

    public void SetSpawnStatus(bool status)
    {
        if (status){InvokeRepeating("SpawnObstacule", startDelay, repeatRate); }
        this.IsActiveTheGame = status;
    }
}
