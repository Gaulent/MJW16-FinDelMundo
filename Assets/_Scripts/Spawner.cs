using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 0.4f;
    [SerializeField] private Color gizmoColor = Color.green;
    [SerializeField] private List<GameObject> spawnTypes;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Game.onGameOver += StopSpawner;
        StartCoroutine(nameof(SpawnPrefab));
    }
    
    IEnumerator SpawnPrefab()
    {
        while (true)
        {
            GameObject prefabToSpawn = spawnTypes[Random.Range(0, spawnTypes.Count)];

            Vector3 spawnPosition = GetRandomPosInsideObject();

            Instantiate(prefabToSpawn, spawnPosition, prefabToSpawn.transform.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomPosInsideObject()
    {
        Transform t = transform;
        Vector3 pos = t.position;
        Vector3 scl = t.localScale;
        
        Vector3 spawnPosition = new Vector3(
            Random.Range(pos.x - Mathf.Abs(scl.x)/2f, pos.x + Mathf.Abs(scl.x)/2f),
            Random.Range(pos.y - Mathf.Abs(scl.y)/2f, pos.y + Mathf.Abs(scl.y)/2f),
            Random.Range(pos.z - Mathf.Abs(scl.z)/2f, pos.z + Mathf.Abs(scl.z)/2f)
        );

        return spawnPosition;
    }
    
    private void OnDrawGizmos()
    {
        Transform myTransform = transform;
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(myTransform.position, myTransform.localScale+Vector3.one*.1f);
    }

    private void StopSpawner()
    {
        gameObject.SetActive(false);
    }
    
}
