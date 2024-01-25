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

    [Header("spawn zone")]
    [SerializeField] float spawnRadius = 5.0f;
    [SerializeField] float spawnDistanceBeyondRadius = 2.0f;

    [Header("To next level")]
    [SerializeField] private int level,PointsToLevel;
    [SerializeField] private int maxTipyEnemy,maxEnemyBySpawn,moreEnemyForLevel;

    private int indexEnemy = 0;
    private float currentSpawnTime, spawnTimer;

    private List<GameObject> communEnemy = new List<GameObject>();
    private List<GameObject> camperEnemy = new List<GameObject>();
    private List<GameObject> invokeEnemy = new List<GameObject>();

    private PlayerController playerController;
    private GenerationGaster generationGaster;
    private GenerationAsteroid generationAsteroid;

    private void Awake()
    {
        level = 1;
        maxTipyEnemy = 1;

        generationGaster = GetComponent<GenerationGaster>();
        generationAsteroid = GetComponent<GenerationAsteroid>();

        PoolEnemies(communEnemy);
        PoolEnemies(camperEnemy);
        PoolEnemies(invokeEnemy);

        playerController = PlayerController.instance;
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
            enemy.GetComponent<EnemyMovement>().GetReference(playerController);
            list.Add(enemy);
        }
        indexEnemy++;
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

    }
    private void Update()
    {
       
        if (spawnTimer >= currentSpawnTime)
        {
            int spawnCurrent = 1;
            while (maxEnemyBySpawn >= spawnCurrent)
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

        switch (opcion)
        {
            case 0:
                indexEnemy = 0;
                print("Comun");
                return communEnemy;
            case 1:
                indexEnemy = 1;
                print("Camper");
                return camperEnemy;
            case 2:
                indexEnemy = 2;
                print("invoke");
                return invokeEnemy;
            default: 
                return communEnemy;
        }
    }
    /// <summary>
    /// configura la nueva posicion de spawn
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private Vector3 Configuration(Vector3 position)
    {
        Vector2 playerPosition = new Vector2(playerController.transform.position.x, playerController.transform.position.y);
        // Radio en el que aparecerá el enemigo

        // Generar una posición aleatoria en la circunferencia del círculo
        Vector2 randomSpawnDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPositionOnCircle = playerPosition + randomSpawnDirection * spawnRadius;

        // Mover la posición más allá del radio
        Vector2 spawnPositionBeyondRadius = spawnPositionOnCircle + randomSpawnDirection * spawnDistanceBeyondRadius;

        Vector3 TempPosition = new Vector3(spawnPositionBeyondRadius.x, spawnPositionBeyondRadius.y,0);
        return TempPosition;
    }
    private Vector3 ConfigurationPocition(Vector3 position)
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
            enemy.GetComponent<EnemyMovement>().GetReference(playerController);
            pool.Add(enemy);
        }
        if (enemy.GetComponent<Enemy>() is CamperEnemy) Position = ConfigurationPocition(positionToSpawn.position);

        enemy.transform.position = Position;
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().UpdateLevel(level);
        enemy.GetComponent<Enemy>().ResetMovent(Position);
    }
    private void ChangeDifficulty()
    {
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
                case 5:
                    maxTipyEnemy++;
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
        maxEnemyBySpawn += moreEnemyForLevel;
        print("algo de enemgiso ");
    }
}
