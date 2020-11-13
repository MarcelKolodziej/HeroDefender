using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private BasicEnemy EnemyPrefab;
    [SerializeField] private Transform[] EnemyPath;

    [Header("ObjectPoolSettings")]
    [SerializeField] private int DefaultPoolSize = 10;

    private Queue<BasicEnemy> pooledEnemies = new Queue<BasicEnemy>();
    private List<BasicEnemy> activeEnemies = new List<BasicEnemy>();
    private float spawnRate = 0.0f;
    private float currentSpawnTick = 0.0f;
    private bool spawnEnemies = false;

    public void Start()
    {
        BasicEnemy basicEnemy;

        for (int i = 0; i < DefaultPoolSize; i++)
        {
            basicEnemy = Instantiate(EnemyPrefab);
            basicEnemy.gameObject.SetActive(false);
            pooledEnemies.Enqueue(basicEnemy);
        }
    }

    public void SpawnEnemy()
    {
        if (pooledEnemies.Count > 0)
        {
            activeEnemies.Add(pooledEnemies.Dequeue());
            Debug.Log("Take From Pool: " + pooledEnemies.Count);
        }
        else
        {
            activeEnemies.Add(Instantiate(EnemyPrefab));
            Debug.Log("Spawn New: " + activeEnemies.Count);
        }

        activeEnemies[activeEnemies.Count-1].RespawnEnemy();
        activeEnemies[activeEnemies.Count - 1].transform.position = EnemyPath[0].position;
    }

    public void Update()
    {
        if (spawnEnemies == true)
        {
            currentSpawnTick += Time.deltaTime;

            if (currentSpawnTick >= spawnRate)
            {
                currentSpawnTick = 0f;
                SpawnEnemy();
            }
        }

        // Moves Enemies along the path
        foreach (BasicEnemy enemy in activeEnemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, EnemyPath[enemy.CurrentLocationIndex + 1].position, enemy.MovementSpeed * Time.deltaTime);

                if (enemy.transform.position == EnemyPath[enemy.CurrentLocationIndex + 1].position)
                {
                    enemy.CurrentLocationIndex++;

                    if (enemy.CurrentLocationIndex >= EnemyPath.Length-1)
                    {
                        Debug.Log("Damage Base");
                        enemy.gameObject.SetActive(false);
                    }
                }
            }
        }

        // Addeds dead enemies back into the object pool so they can re-used. 
        // This is actually the cleanest way to do this if you were wondering...
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (!activeEnemies[i].gameObject.activeInHierarchy)
            {
                pooledEnemies.Enqueue(activeEnemies[i]);
                activeEnemies.Remove(activeEnemies[i]);
            }
        }
    }

    public void StartSpawningEnemies(float SpawnRate)
    {
        spawnRate = SpawnRate;
        spawnEnemies = true;
    }

    public void StopSpawningEnemies()
    {
        spawnEnemies = false;
    }
}
