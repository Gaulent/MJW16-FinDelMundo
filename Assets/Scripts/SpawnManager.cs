using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour, ISpawnManager
{
    //public GameObject PrefabToSpawn;

    public float startDelay = 2, repeatRate = 2;

    private int enemiesSpawned, enemiesToSpawn;

    [SerializeField] List<GameObject> SpawnTypes;

    [SerializeField] private int SpawnRange = 20;

    protected UnityEvent WaveEndedSignal = new UnityEvent();

    public UnityEvent OnWaveEndedSignal { get { return WaveEndedSignal; } }


    bool IsActiveTheGame;

    private IPlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnObstacule", startDelay, repeatRate);
        playerController = GameObject.Find("Player")
            .GetComponent<IPlayerController>();
    }

    void SpawnObstacule()
    {
        if(!IsActiveTheGame)
        {
            CancelInvoke("SpawnObstacule");return;
        }

        GameObject PrefabToSpawn = SpawnTypes[Random.Range(0, SpawnTypes.Count)];
        Instantiate(PrefabToSpawn, 
            new Vector3(Random.Range(-SpawnRange,SpawnRange),0 ,40), 
            PrefabToSpawn.transform.rotation
        );

        enemiesSpawned++;
        if (enemiesSpawned == enemiesToSpawn){ WaveEndedSignal.Invoke();}
    }

    public void SetSpawnStatus(bool status)
    {
        if (status){InvokeRepeating("SpawnObstacule", startDelay, repeatRate); }
        this.IsActiveTheGame = status;
    }

    public void SetEnemiesWaves(List<GameObject> test, int values)
    {
        SpawnTypes = test;
        enemiesToSpawn = values;
        enemiesSpawned = 0;
    }
}
