using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class GenerationGaster : MonoBehaviour
{
    [Header("Stats to Blaster")]
    [SerializeField] private GameObject blasterGaster;
    [SerializeField] private Vector2 spawnTimeRange;
    [SerializeField] private float spawnRadius;

    private List<GameObject> blaster = new List<GameObject>();
    private float currentSpawnTime, spawnTimer;
    private bool Spawn;
    private PlayerController playerController;
    void Start()
    {
        Spawn = false;
        PoolEnemies();
    }
    private void Update()
    {
        if (!Spawn) return; 
        if (spawnTimer >= currentSpawnTime)
        {
            SpawnEnemy(GetRandomSpawnPoint());
            currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            spawnTimer = 0;
        }
        spawnTimer += Time.deltaTime;
    }
    private void PoolEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newBlaster = Instantiate(blasterGaster);
            newBlaster.GetComponent<EnemyMovement>().GetReference(playerController);
            newBlaster.SetActive(false);
            blaster.Add(newBlaster);
            newBlaster.transform.SetParent(transform); 
        }
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

    }
    private Vector2 GetRandomSpawnPoint()
    {
        if (playerController == null) playerController = PlayerController.instance;
        Vector2 playerPosition = new Vector2(playerController.transform.position.x, playerController.transform.position.y);
        // Radio en el que aparecerá el enemigo
        // Generar una posición aleatoria en la circunferencia del círculo
        Vector2 randomSpawnDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPositionOnCircle = playerPosition + randomSpawnDirection * spawnRadius;

        // Mover la posición más allá del radio
        Vector2 spawnPositionBeyondRadius = spawnPositionOnCircle + randomSpawnDirection * spawnRadius;

        Vector3 newPosition = new Vector3(spawnPositionBeyondRadius.x, spawnPositionBeyondRadius.y, 0);
        return newPosition;
    }
    private void SpawnEnemy(Vector2 Position)
    {
        GameObject newBlaster = blaster.Find(b => !b.activeSelf);
        if (newBlaster == null)
        {
            newBlaster = Instantiate(blasterGaster);
            newBlaster.GetComponent<EnemyMovement>().GetReference(playerController);
            blaster.Add(newBlaster);
        }
        newBlaster.transform.position = GetRandomSpawnPoint();
        newBlaster.SetActive(true);
        print("Algo de camperos");
        newBlaster.GetComponent<Enemy>().ResetMovent(Position);

    }
    public void ReduceTimeBlaster()
    {
        if(spawnTimeRange.x > 1) spawnTimeRange.x -= 0.5f;
        if (spawnTimeRange.y > 3) spawnTimeRange.y -= 0.5f;
    }
    public void ActiveBlaster()
    {
        Spawn = true;
    }
    public void DesactiveBlaster()
    {
        Spawn = false;
    }
}
