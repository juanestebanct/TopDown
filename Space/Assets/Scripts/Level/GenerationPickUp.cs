using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GenerationPickUp : MonoBehaviour
{
    [Header("Stats to PickUp spawns ")]

    [SerializeField] private GameObject[] pickUps;
    [SerializeField] private Vector2[] areaGeneration = new Vector2[2];
    [SerializeField] private Vector2 spawnTimeRange;

    private List<GameObject> listpickUps = new List<GameObject>();
    private float currentSpawnTime, spawnTimer;
    void Start()
    {
        PoolPickUp();
    }
    private void Update()
    {
        if (spawnTimer >= currentSpawnTime)
        {
            SpawnEnemy(GetRandomSpawnPoint());
            currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            spawnTimer = 0;
        }
        spawnTimer += Time.deltaTime;
    }
    private void PoolPickUp()
    {
        for (int i = 0; i < pickUps.Length; i++)
        {
            GameObject TempPickUp = Instantiate(pickUps[i]);
            TempPickUp.SetActive(false);
            listpickUps.Add(TempPickUp);
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
        GameObject tempPickUp = listpickUps.Find(b => !b.activeSelf);

        if (tempPickUp == null)
        {
            tempPickUp = Instantiate(pickUps[Random.Range(0, pickUps.Length)]);
            listpickUps.Add(tempPickUp);
        }

        tempPickUp.transform.position = GetRandomSpawnPoint();
        tempPickUp.SetActive(true);
        

    }
}
