using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GenerationAsteroid : MonoBehaviour
{
    [Header("Stats to SpawnAsteroid")]
    [SerializeField] private GameObject meteoritePrefb;
    [SerializeField] private Vector2 spawnTimeRange;
    [SerializeField] private Transform[] SpawnPoint ;

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
        tempMeteorite.GetComponent<Meteorite>().AddForce(direction);

    }
    /// <summary>
    /// Se llama para dividir el meteorito en varias partes 
    /// </summary>
    public void SpinBigAsteroid()
    {

    }
}
