using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerationEnemiesNormal : MonoBehaviour
{
    [Header("Stats to spawn")]
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private Transform positionToSpawn;
    [SerializeField] private Vector2 spawnTimeRange = new Vector2(5, 10);
    [SerializeField] private Vector2 MaxPosition = new Vector2(-50, 50);
    [SerializeField] private int level,PointsToLevel;

    private int indexEnemy = 0, maxTipyEnemy,MaxEnemyBySpawn;
    private float currentSpawnTime, spawnTimer;

    private List<GameObject> communEnemy = new List<GameObject>();
    private List<GameObject> camperEnemy = new List<GameObject>();

    private GenerationGaster generationGaster;
    private GenerationAsteroid generationAsteroid;

    private void Awake()
    {
        MaxEnemyBySpawn = 1;
        level = 1;

        generationGaster = GetComponent<GenerationGaster>();
        generationAsteroid = GetComponent<GenerationAsteroid>();

        PoolEnemies(communEnemy);

        indexEnemy = 1;
        maxTipyEnemy++;

        PoolEnemies(camperEnemy);
    }
    private void Start()
    {
        Score.Instance.NextLevelPoinst = PointsToLevel;
    }
    /// <summary>
    /// genera el pool de cada tipo
    /// </summary>
    private void PoolEnemies(List<GameObject> list)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject enemy = Instantiate(enemys[indexEnemy]);
            enemy.SetActive(false);
            enemy.transform.position = positionToSpawn.position;
            enemy.transform.parent = transform.parent;
            list.Add(enemy);
        }
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

    }
    private void Update()
    {
       
        if (spawnTimer >= currentSpawnTime)
        {
            int spawnCurrent = 1;
            while (MaxEnemyBySpawn >= spawnCurrent)
            {
               
                SpawnEnemy(choose(), Configuration(positionToSpawn.position));
                currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
                spawnTimer = 0;
                spawnCurrent++;
            }
        }
        spawnTimer += Time.deltaTime;
        ChangeDifficulty();
    }
    /// <summary>
    /// se escoje la lista que va a lansar 
    /// </summary>
    /// <returns></returns>
    private List<GameObject> choose()
    {
        int opcion = Random.Range(0,maxTipyEnemy);
        print(opcion);
        if (opcion == 0)
        {
            indexEnemy = 0;
            print("Comun");
            return communEnemy;
        }
        else
        {
            indexEnemy = 1;
            print("Camper");
            return camperEnemy;
        }
    }
    /// <summary>
    /// configura la nueva posicion de spawn
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private Vector3 Configuration(Vector3 position)
    {
        Vector3 TempPosition = new Vector3(Random.Range(MaxPosition.x, MaxPosition.y), position.y, position.z);
        return TempPosition;
    }
    private void SpawnEnemy(List<GameObject> pool, Vector3 Position)
    {
        GameObject enemy = pool.Find(b => !b.activeSelf);
        if (enemy == null)
        {
            int range = Random.Range(0, enemys.Length);
            enemy = Instantiate(enemys[indexEnemy]);
            pool.Add(enemy);
        }
        enemy.transform.position = Position;
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().ResetMovent(Position);
    }
    private void ChangeDifficulty()
    {
        print(Score.Instance.CurrentScore);
        if (Score.Instance.CurrentScore >= PointsToLevel)
        {
            PointsToLevel = (PointsToLevel * 2) + PointsToLevel / 2;
            Score.Instance.NextLevelPoinst = PointsToLevel;
            Score.Instance.PastLevel();
            level++;
            /*
            2.primero se reduce el tiempo, 3.luego se activa los otros enemigos, 4.luego se activa los blaster y 5.luego mas enemigos por spawn y 
            mas enemigos
             * */
            switch (level)
            {
                case 2:
                    ReduseTime();
                    break;
                case 3:
                    maxTipyEnemy++;
                    break;
                case 4:
                    generationGaster.ActiveBlaster();
                    break;

                default:
                    ReduseTime();
                    MoreEnemy();
                    break;
            }
        }
    }
    private void ReduseTime()
    {
        spawnTimeRange = new Vector2 (spawnTimeRange.x - 0.5f, spawnTimeRange.y - 0.5f);
        generationGaster.ReduceTimeBlaster();
        generationAsteroid.ReduceTimeMeteorite();
    }
    private void MoreEnemy()
    {
        MaxEnemyBySpawn++;
    }
}
