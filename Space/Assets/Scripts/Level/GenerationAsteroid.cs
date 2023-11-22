using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GenerationAsteroid : MonoBehaviour
{
    [Header("Stats to SpawnAsteroid")]
    [SerializeField] private GameObject meteoritePrefb,miniMetoritePrefb;
    [SerializeField] private Vector2 spawnTimeRange;
    [SerializeField] private Transform[] SpawnPoint ;
    [SerializeField] private int minAsteroidSplinters, maxAsteroidSplinters;

    private List<GameObject> bigMeteorite = new List<GameObject>();
    private List<GameObject> minMeteorite = new List<GameObject>();

    private GameObject player;

    private float currentSpawnTime, spawnTimer;
    void Start()
    {
        player = PlayerController.instance.gameObject;
        PoolMeteorite();
    }
    private void Update()
    {
        if (spawnTimer >= currentSpawnTime)
        {
            SpawnMeteorite();
            currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            spawnTimer = 0;
        }
        spawnTimer += Time.deltaTime;
    }

    private void PoolMeteorite()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject meteorite = Instantiate(meteoritePrefb);
            meteorite.GetComponent<Meteorite>().GenerationAsteroid = this;
            meteorite.SetActive(false);
            bigMeteorite.Add(meteorite);

            GameObject miniMeteorite = Instantiate(miniMetoritePrefb);
            miniMeteorite.GetComponent<Meteorite>().GenerationAsteroid = this;
            miniMeteorite.SetActive(false);
            minMeteorite.Add(miniMeteorite);
        }
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    }

    private Vector2 GetRandomSpawnPoint()
    {
        int temp = Random.Range(1,3);
        float x,y;

        switch (temp)
        {
            // izquierda 
            case 1:
                x = SpawnPoint[1].transform.position.x;
                y = Random.Range(SpawnPoint[0].transform.position.y, SpawnPoint[1].transform.position.y);

                break;
            // Derecha 
            case 2:
                x = SpawnPoint[2].transform.position.x;
                y = Random.Range(SpawnPoint[2].transform.position.y, SpawnPoint[3].transform.position.y);

                break;

            default:
                x = 0;
                y = 0;

                break;
        }


        return new Vector2(x, y);
    }
    private void SpawnMeteorite()
    {
        GameObject tempMeteorite = bigMeteorite.Find(b => !b.activeSelf);
        if (tempMeteorite == null)
        {
            tempMeteorite = Instantiate(meteoritePrefb);
            bigMeteorite.Add(tempMeteorite);
        }
        tempMeteorite.transform.position = GetRandomSpawnPoint();
        tempMeteorite.SetActive(true);

        Vector2 direction = player.transform.position - tempMeteorite.transform.position;
        float torque = Random.Range(500.0f, 1500.0f);
        tempMeteorite.GetComponent<Meteorite>().AddForce(direction, torque);

    }

    private void SpawnMiniMeteorite(Vector3 direction, float torque, Vector3 position)
    {
        GameObject tempMeteorite = minMeteorite.Find(b => !b.activeSelf);
        if (tempMeteorite == null)
        {
            tempMeteorite = Instantiate(miniMetoritePrefb);
            minMeteorite.Add(tempMeteorite);
        }
        tempMeteorite.transform.position = position;
        tempMeteorite.SetActive(true);
        tempMeteorite.GetComponent<Meteorite>().AddForce(direction, torque);

    }
    /// <summary>
    /// Se llama para dividir el meteorito en varias partes 
    /// </summary>
    public void SpinBigAsteroid(Vector3 position)
    {
        int splintersToSpawn = Random.Range(minAsteroidSplinters, maxAsteroidSplinters);

        //se calcula cunatos van a explotar 
        for (int counter = 0; counter < splintersToSpawn; ++counter)
        {
            Vector2 direction = Quaternion.Euler(0, 0, counter * 360.0f / splintersToSpawn) * Vector2.up;
            float torque = Random.Range(500.0f, 1500.0f);

            SpawnMiniMeteorite(direction, torque, position);
        }
    }
    public void ReduceTimeMeteorite()
    {
        if (spawnTimeRange.x > 1) spawnTimeRange.x -= 0.5f;
        if (spawnTimeRange.y > 3) spawnTimeRange.y -= 0.5f;
    }
}
