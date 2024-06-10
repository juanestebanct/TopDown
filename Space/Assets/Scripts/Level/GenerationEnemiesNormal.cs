using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerationEnemiesNormal : MonoBehaviour
{
    [Header("Stats to spawn")]
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private GameObject[] boss;
    [SerializeField] private Transform positionToSpawn;
    [SerializeField] private GameObject ParticuleSpawn;
    [SerializeField] private Vector2 spawnTimeRange = new Vector2(5, 10);
    [SerializeField] private Vector2 MaxPosition = new Vector2(-50, 50);

    [Header("spawn zone")]
    [SerializeField] float spawnRadius = 5.0f;
    [SerializeField] float spawnDistanceBeyondRadius = 2.0f;

    [Header("To next level")]
    [SerializeField] private int level,PointsToLevel,eventIndex, indexBoss;
    [SerializeField] private int maxTipyEnemy,maxEnemyBySpawn,moreEnemyForLevel;
    [SerializeField] float elapsedTime, timeToNowdifficulty, timeBeforeSpawn;
    [SerializeField] float[] listTimer;
    [SerializeField] private TextMeshProUGUI timer;

    [SerializeField] private int indexEnemy = 0;
    private float currentSpawnTime, spawnTimer;
    private bool bossEvent;
    private List<List<GameObject>> AllEnemy = new List<List<GameObject>>();

    private PlayerController playerController;
    private GenerationGaster generationGaster;
    private GenerationAsteroid generationAsteroid;

    private void Awake()
    {
        level = 1;
        maxTipyEnemy = 1;
        eventIndex = 0;

        generationGaster = GetComponent<GenerationGaster>();
        generationAsteroid = GetComponent<GenerationAsteroid>();

        PoolEnemies();
        PoolEnemies();
        PoolEnemies();
        PoolEnemies();


        playerController = PlayerController.instance;
    }
    private void Start()
    {
        Score.Instance.NextLevelPoinst = PointsToLevel;
       
    }
    /// <summary>
    /// genera el pool de cada tipo
    /// </summary>
    private void PoolEnemies()
    {
        List<GameObject> list = new List<GameObject>();

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
        AllEnemy.Add(list);

    }
    private void Update()
    {
        if (bossEvent) return;
        if (spawnTimer >= currentSpawnTime)
        {
            int spawnCurrent = 1;
            while (maxEnemyBySpawn >= spawnCurrent)
            {
                StartCoroutine(SpawnCorrutineBucle());
                currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
                spawnTimer = 0;
                spawnCurrent++;
            }
        }
        spawnTimer += Time.deltaTime;
        ChangeDifficulty();
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        nextLevel();
    }
    /// <summary>
    /// se escoje la lista que va a lansar 
    /// </summary>
    /// <returns></returns>
    private List<GameObject> choose()
    {
        int opcion = Random.Range(0,maxTipyEnemy);
        print(AllEnemy[opcion]);

        switch (opcion)
        {
            case 0:
                indexEnemy = 0;
                print("Comun");
                return AllEnemy[opcion];
            case 1:
                indexEnemy = 1;
                print("Camper");
                return AllEnemy[opcion];
            case 2:
                indexEnemy = 2;
                print("invoke");
                return AllEnemy[opcion];
            case 3:
                indexEnemy = 3;
                print("Tacle");
                return AllEnemy[opcion];
            case 4:
                indexEnemy = 4;
                print("Shoot");
                return AllEnemy[opcion];

            default: 
                return AllEnemy[0];
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
        if (elapsedTime >= timeToNowdifficulty)
        {
            timeToNowdifficulty += timeToNowdifficulty;
            eventIndex++;
            print("Cambio nivel"+ eventIndex );
            /*
            2.primero se reduce el tiempo, 3.luego se activa los otros enemigos, 4.luego se activa los blaster y 5.luego mas enemigos por spawn y 
            mas enemigos
             * */
            switch (eventIndex)
            {
                case 1:
                    ReduseTime();
                    break;
                case 2:
                    maxTipyEnemy++;
                    break;
                case 3:
                    generationGaster.ActiveBlaster();
                    break;
                case 4:
                    maxTipyEnemy++;
                    break;
                case 5:
                    maxTipyEnemy++;
                    break;
                case 6:
                    maxTipyEnemy++;
                    break;


                default:
                    ReduseTime();
                    MoreEnemy();
                    break;
            }
        }
        if (elapsedTime >= listTimer[indexBoss])
        {
            Transform temp = transform;
            temp.position = Configuration(positionToSpawn.position);
            GameObject bossTemp = Instantiate(boss[indexBoss], Configuration(positionToSpawn.position), Quaternion.identity);
            bossTemp.GetComponent<BossChaster>().Ref(this);
            bossEvent = true;
            generationGaster.DesactiveBlaster();
            indexBoss++;
        }
    }
    private void nextLevel()
    {
        if (Score.Instance.CurrentScore >= PointsToLevel)
        {
            PointsToLevel = (PointsToLevel * 2) + PointsToLevel / 2;
            Score.Instance.NextLevelPoinst = PointsToLevel;
            Score.Instance.PastLevel();
            level++;
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
    }

    public void ActiveEvent()
    {
        bossEvent = false;
        generationGaster.ActiveBlaster();
    }
    IEnumerator SpawnCorrutineBucle()
    {
        Vector3 tempVector = Configuration(positionToSpawn.position);
        var spawnParticle = Instantiate(ParticuleSpawn, tempVector, transform.rotation);
        spawnParticle.GetComponent<ParticleSystem>().Play();
        Destroy(spawnParticle, 2f);
        yield return new WaitForSeconds(timeBeforeSpawn);
        
        SpawnEnemy(choose(), tempVector);
        //SpawnEnemys(choose(), tempVector);
    }
}
