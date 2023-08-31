using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerationEnemies : MonoBehaviour
{
    [SerializeField] private GameObject CommunEnemy;
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
    private void PoolEnemies()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject enemy = Instantiate(CommunEnemy);
            CommunEnemy.SetActive(false);
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
    /// Reinicia a los enemigos en un punto 
    /// </summary>
    private void SpawnEnemy(List<GameObject> pool,Vector3 Position)
    {
        Debug.Log("Spawn Enemy");
        GameObject enemy = pool.Find(b => !b.activeSelf);
        if (enemy == null)
        {
            enemy = Instantiate(CommunEnemy);
            pool.Add(enemy);
        }
        enemy.transform.position = Position;
        enemy.SetActive(true);
        enemy.GetComponent<CommunEnemy>().ResetMovent(Position);

    }
}
