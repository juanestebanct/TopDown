using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationEnemies : MonoBehaviour
{
    [SerializeField] private GameObject CommunEnemy;
    [SerializeField] private Transform position;

    [SerializeField] private Vector2 spawnTimeRange = new Vector2(5, 10);
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
            SpawnEnemy(enemies, position.position);
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
