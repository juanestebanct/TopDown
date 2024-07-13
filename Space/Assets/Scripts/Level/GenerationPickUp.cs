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
    [SerializeField] private float spawnRadius;
    private PlayerController playerController;

    [SerializeField] private List<List<GameObject>> listpickUps = new List<List<GameObject>>();
    private float currentSpawnTime, spawnTimer;
    private int currentIndex;
    void Start()
    {
        PoolPickUp();
        playerController = PlayerController.instance;
        for (int i = 0; i < pickUps.Length-1; i++) PoolPickUp();
    }
    private void Update()
    {
        if (spawnTimer >= currentSpawnTime)
        {
            int newItemIndex =Random.Range(0, pickUps.Length-1);
            SpawnPickUp(listpickUps[newItemIndex]);
            currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            spawnTimer = 0;
        }
        spawnTimer += Time.deltaTime;
    }
    private void PoolPickUp()
    {
        List<GameObject> TempListSpawn = new List<GameObject>();
        for (int i = 0; i < pickUps.Length; i++)
        {
            GameObject TempPickUp = Instantiate(pickUps[currentIndex]);
            TempPickUp.SetActive(false);
            TempListSpawn.Add(TempPickUp);
            TempPickUp.transform.SetParent(transform);
        }
        listpickUps.Add(TempListSpawn);
        currentIndex++;
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    }
    private Vector2 GetRandomSpawnPoint()
    {
        if(playerController ==null) playerController = PlayerController.instance;
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
    private void SpawnPickUp(List<GameObject> pool)
    {
        GameObject tempPickUp = pool.Find(b => !b.activeSelf);

        if (tempPickUp == null)
        {
            tempPickUp = Instantiate(pickUps[Random.Range(0, pickUps.Length)]);
            pool.Add(tempPickUp);
            tempPickUp.transform.SetParent(transform);
        }

        tempPickUp.transform.position = GetRandomSpawnPoint();
        tempPickUp.SetActive(true);
        

    }
}
