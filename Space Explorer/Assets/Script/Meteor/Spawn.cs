using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    // [SerializeField] private float rate;
    // [SerializeField] private GameObject[] meteor;
    //
    // private void Start()
    // {
    //     InvokeRepeating("SpawnMeteor", rate, rate);
    // }
    //
    // private void SpawnMeteor()
    // {
    //     int meteorCount = Random.Range(3, 5);
    //
    //     for (int i = 0; i < meteorCount; i++)
    //     {
    //         Vector3 spawnPos = new Vector3(Random.Range(-7.6f, 7.6f), Random.Range(4.5f, 4.55f), 0f);
    //         float delay = Random.Range(0f, 0.1f);
    //         Invoke("SpawnAMeteor", delay);
    //     }
    // }
    //
    // private void SpawnAMeteor()
    // {
    //     Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), Random.Range(2f, 6f), 0f);
    //     GameObject meteorInstance = Instantiate(meteor[Random.Range(0, meteor.Length)], spawnPos,  Quaternion.identity);
    // }
    
    
    [Header("Spawn Settings")] 
    [SerializeField] private float rate;
    [SerializeField] private GameObject[] meteors;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private float starSpawnChance;
    [SerializeField] private float minSpawnDistance;

    private List<Vector3> activeSpawnPositions = new List<Vector3>();

    private void Start()
    {
        InvokeRepeating("SpawnObjects", rate, rate);
    }

    private void SpawnObjects()
    {
        int objectCount = Random.Range(1, 5);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 spawnPos = GetValidSpawnPosition();
            if (spawnPos != Vector3.zero) 
            {
                float delay = Random.Range(0f, 0.2f);
                Invoke(nameof(SpawnSingleObject), delay);
            }
        }
    }

    private void SpawnSingleObject()
    {
        Vector3 spawnPos = GetValidSpawnPosition();
        if (spawnPos == Vector3.zero)
        {
            return;
        }

        bool spawnStar = Random.value < starSpawnChance;
        GameObject prefab = spawnStar ? stars[Random.Range(0, stars.Length)] : meteors[Random.Range(0, meteors.Length)];
        
        GameObject instance = Instantiate(prefab, spawnPos, Quaternion.identity);
        activeSpawnPositions.Add(spawnPos);

        StartCoroutine(ClearSpawnPosition(spawnPos, instance));
    }

    private Vector3 GetValidSpawnPosition()
    {
        int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), Random.Range(4f, 4.55f), 0f);
            if (IsValidSpawnPosition(spawnPos))
            {
                return spawnPos;
            }
        }
        return Vector3.zero;
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        foreach (Vector3 activePos in activeSpawnPositions)
        {
            if (Vector3.Distance(position, activePos) < minSpawnDistance)
            {
                return false;
            }
        }
        if (Physics2D.OverlapCircle(position, minSpawnDistance / 2f))
        {
            return false;
        }
        return true;
    }

    private IEnumerator ClearSpawnPosition(Vector3 position, GameObject instance)
    {
        while (instance != null)
        {
            yield return null;
        }
        activeSpawnPositions.Remove(position);
    }
}