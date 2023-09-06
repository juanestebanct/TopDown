using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private Vector2[] areaGeneration = new Vector2[2];
    [SerializeField] private Vector2 spawnTimeRange;
    private List<GameObject> coins = new List<GameObject>();
    private float currentSpawnTime, spawnTimer;

    private void Start()
    {
        PoolCoins();
    }
    private void Update()
    {
        if (spawnTimer >= currentSpawnTime)
        {
            SpawnCoins();
            currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            spawnTimer = 0;
        }
        spawnTimer += Time.deltaTime;
    }
    private void PoolCoins()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject _coin = Instantiate(coin);
            _coin.SetActive(false);
        }
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

    }
    private Vector2 GetRandomSpawnPoint()
    {

        float x = Random.Range(areaGeneration[0].x, areaGeneration[0].y);
        float y = Random.Range(areaGeneration[1].x, areaGeneration[1].y);

        return new Vector2(x,y);
    }
    private void SpawnCoins()
    {
        GameObject _coin = coins.Find(b => !b.activeSelf);
        if (_coin == null)
        {
            _coin = Instantiate(this.coin);
            coins.Add(_coin);
        }
        _coin.transform.position = GetRandomSpawnPoint();
        _coin.SetActive(true);
    }
}
