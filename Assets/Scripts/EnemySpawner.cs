using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; 
    [SerializeField]private Transform[] spawnPoints; 
    [SerializeField]private float spawnInterval = 2f; 

    void Start() => StartCoroutine(SpawnEnemies()); // Iniciar la corrutina
    

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy(); 
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    void SpawnEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length); 
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length); 
        Instantiate(enemyPrefabs[randomEnemyIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity); 
    }
}