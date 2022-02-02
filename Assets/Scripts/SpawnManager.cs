using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3[] spawnLocations;
    public GameObject enemyPrefab;

    private GameObject[] enemies;
  

    // Start is called before the first frame update
    void Start()
    {
        foreach (Vector3 spawnPosition in spawnLocations)
        {
            Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        }
        
    }

 
    public void SpawnEnemy(Vector3 position)
    {
        StartCoroutine(SpawnEnemyAtDeathPosition(position));
    }

    IEnumerator SpawnEnemyAtDeathPosition(Vector3 position)
    {
        yield return new WaitForSeconds(5);
        Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation);
    }



}
