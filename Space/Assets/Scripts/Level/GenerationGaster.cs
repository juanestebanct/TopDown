using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class GenerationGaster : MonoBehaviour
{
    [Header("Stats to Blaster")]
    [SerializeField] private GameObject blasterGaster;
    [SerializeField] private Vector2[] areaGeneration = new Vector2[2];
    [SerializeField] private Vector2 spawnTimeRange;

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
        for (int i = 0; i < 1; i++)
        {
            GameObject Tempblaster = Instantiate(blasterGaster);
            Tempblaster.GetComponent<EnemyMovement>().GetReference(playerController);
            Tempblaster.SetActive(false);
            blaster.Add(Tempblaster);
        }
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

    }
    private Vector2 GetRandomSpawnPoint()
    {

        float x = Random.Range(areaGeneration[0].x, areaGeneration[0].y);
        float y = Random.Range(areaGeneration[1].x, areaGeneration[1].y);

        return new Vector2(x, y);
    }
    private void SpawnEnemy(Vector2 Position)
    {
        GameObject Tempblaster = blaster.Find(b => !b.activeSelf);
        if (Tempblaster == null)
        {
            Tempblaster = Instantiate(blasterGaster);
            Tempblaster.GetComponent<EnemyMovement>().GetReference(playerController);
            blaster.Add(Tempblaster);
        }
        Tempblaster.transform.position = GetRandomSpawnPoint();
        Tempblaster.SetActive(true);
        Tempblaster.GetComponent<Enemy>().ResetMovent(Position);

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
}
