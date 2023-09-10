using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerationEnemies : MonoBehaviour
{
    [Header("Stats to spawn")]
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private Transform position;
    [SerializeField] private Vector2 spawnTimeRange = new Vector2(5, 10);
    [SerializeField] private Vector2 MaxPosition = new Vector2(-50, 50);

    private float currentSpawnTime, spawnTimer;
    private List<GameObject> enemies = new List<GameObject>();
    private void Awake()
    {
        PoolEnemies();
    }
    private void Update()
    {
        if (spawnTimer >= currentSpawnTime)
        {
            SpawnEnemy(enemies, Configuration(position.position));
            currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            spawnTimer = 0;
        }
        spawnTimer += Time.deltaTime;
    }
    /// <summary>
    /// Instncia los primeros enemigos del pool
    /// </summary>
    private void PoolEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            int range = Random.Range(0, enemys.Length);
            GameObject enemy = Instantiate(enemys[range]);
            enemy.SetActive(false);
            enemy.transform.position = position.position;
        }
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

    }
    private Vector3 Configuration(Vector3 position)
    {
        Vector3 TempPosition = new Vector3(Random.Range(MaxPosition.x, MaxPosition.y),position.y,position.z); 
        return TempPosition;
    }
    /// <summary>
    /// Reinicia a los enemigos y hace el metodo pool con ellos
    /// </summary>
    private void SpawnEnemy(List<GameObject> pool,Vector3 Position)
    {
        GameObject enemy = pool.Find(b => !b.activeSelf);
        if (enemy == null)
        {
            int range = Random.Range(0, enemys.Length);
            enemy = Instantiate(enemys[range]);
            pool.Add(enemy);
        }
        enemy.transform.position = Position;
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().ResetMovent(Position);

    }
}
