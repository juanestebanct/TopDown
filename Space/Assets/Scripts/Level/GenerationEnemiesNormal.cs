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
    [SerializeField] private int pointsToLevel;
    [SerializeField] private int maxEnemyBySpawn, moreEnemyForLevel;
    [SerializeField] float timeToNowdifficulty, timeBeforeSpawn;
    [SerializeField] float[] listTimer;
    [SerializeField] private TextMeshProUGUI timer;

    private int indexEnemy, eventIndex, indexBoss, maxTipyEnemy, level;
    private float currentSpawnTime, spawnTimer, addTimeToNowdifficulty, elapsedTime;
    private bool bossEvent;

    private List<List<GameObject>> AllEnemy = new List<List<GameObject>>();

    private PlayerController playerController;
    private GenerationGaster generationGaster;
    private GenerationAsteroid generationAsteroid;

    private void Start()
    {
        level = 1;
        maxTipyEnemy = 1;
        eventIndex = 0;
        addTimeToNowdifficulty = timeToNowdifficulty;

        Score.Instance.NextLevelPoinst = pointsToLevel;
        playerController = PlayerController.instance;

        generationGaster = GetComponent<GenerationGaster>();
        generationAsteroid = GetComponent<GenerationAsteroid>();

        for (int i = 0; i < enemys.Length; i++) PoolEnemies();

    }
    /// <summary>
    /// genera el pool de cada tipo
    /// </summary>
    private void PoolEnemies()
    {
        List<GameObject> TempListSpawn = new List<GameObject>();

        for (int i = 0; i < 8; i++)
        {
            TempListSpawn.Add(SpawnEnemy(indexEnemy));
        }
        indexEnemy++;
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        AllEnemy.Add(TempListSpawn);

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
        NextLevel();
    }
    /// <summary>
    /// se escoje la lista que va a lansar 
    /// </summary>
    /// <returns></returns>
    private List<GameObject> choose()
    {
        int opcion = Random.Range(0, maxTipyEnemy);
        switch (opcion)
        {
            case 0:
                indexEnemy = 0;
                return AllEnemy[opcion];
            case 1:
                indexEnemy = 1;
                return AllEnemy[opcion];
            case 2:
                indexEnemy = 2;
                return AllEnemy[opcion];
            case 3:
                indexEnemy = 3;
                return AllEnemy[opcion];
            case 4:
                indexEnemy = 4;
                return AllEnemy[opcion];

            default:
                return AllEnemy[0];
        }
    }
    private Vector3 NewPosition()
    {
        Vector2 playerPosition = new Vector2(playerController.transform.position.x, playerController.transform.position.y);
        // Radio en el que aparecerá el enemigo
        // Generar una posición aleatoria en la circunferencia del círculo
        Vector2 randomSpawnDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPositionOnCircle = playerPosition + randomSpawnDirection * spawnRadius;

        // Mover la posición más allá del radio
        Vector2 spawnPositionBeyondRadius = spawnPositionOnCircle + randomSpawnDirection * spawnDistanceBeyondRadius;

        Vector3 newPosition = new Vector3(spawnPositionBeyondRadius.x, spawnPositionBeyondRadius.y, 0);
        return newPosition;
    }
    private Vector3 ConfigurationPocition(Vector3 position)
    {
        Vector3 newPosition = new Vector3(position.x + Random.Range(MaxPosition.x, MaxPosition.y), position.y, position.z);
        return newPosition;
    }
    private void SpawnEnemy(List<GameObject> pool, Vector3 Position)
    {
        GameObject enemy = pool.Find(b => !b.activeSelf);

        if (enemy == null)
        {
            int range = Random.Range(0, enemys.Length);
            enemy = SpawnEnemy(range);
            pool.Add(enemy);
            enemy.transform.SetParent(transform);
        }
        if (enemy.GetComponent<Enemy>() is CamperEnemy) Position = ConfigurationPocition(Position);

        enemy.transform.position = Position;
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().UpdateLevel(level);
        enemy.GetComponent<Enemy>().ResetMovent(Position);
    }
    private void ChangeDifficulty()
    {
        if (elapsedTime >= timeToNowdifficulty)
        {
            timeToNowdifficulty += addTimeToNowdifficulty;
            eventIndex++;
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
            print("Pasa la dificultad");
        }
        if (elapsedTime >= listTimer[indexBoss])
        {
            StartCoroutine(SpawnBossSpawn());
        }
    }
    private void NextLevel()
    {
        if (Score.Instance.CurrentScore >= pointsToLevel)
        {
            pointsToLevel = (pointsToLevel * 2) + pointsToLevel / 3;
            Score.Instance.NextLevelPoinst = pointsToLevel;
            Score.Instance.PastLevel();
            level++;
        }
    }
    private void ReduseTime()
    {
        spawnTimeRange = new Vector2(spawnTimeRange.x - 0.5f, spawnTimeRange.y - 0.5f);
        generationGaster.ReduceTimeBlaster();
        generationAsteroid.ReduceTimeMeteorite();
    }
    private GameObject SpawnEnemy(int indexList)
    {
        GameObject enemy = Instantiate(enemys[indexList]);
        enemy.SetActive(false);
        enemy.transform.position = positionToSpawn.position;
        enemy.transform.parent = transform.parent;
        enemy.GetComponent<EnemyMovement>().GetReference(playerController);
        enemy.transform.SetParent(this.transform);
        return enemy;
    }
    private void MoreEnemy() => maxEnemyBySpawn += moreEnemyForLevel; 
    public void ActiveEvent()
    {
        bossEvent = false;
        generationGaster.ActiveBlaster();
    }

    IEnumerator SpawnCorrutineBucle()
    {
        Vector3 tempVector = NewPosition();
        var spawnParticle = Instantiate(ParticuleSpawn, tempVector, transform.rotation);
        spawnParticle.GetComponent<ParticleSystem>().Play();
        Destroy(spawnParticle, 2f);
        yield return new WaitForSeconds(timeBeforeSpawn);
        
        SpawnEnemy(choose(), tempVector);
        //SpawnEnemys(choose(), tempVector);
    }
    IEnumerator SpawnBossSpawn()
    {
        
        generationGaster.DesactiveBlaster();
        indexBoss++;
        var spawnParticle = Instantiate(ParticuleSpawn, Vector3.zero, transform.rotation);
        spawnParticle.GetComponent<ParticleSystem>().Play();
        Destroy(spawnParticle, 2f);
        yield return new WaitForSeconds(timeBeforeSpawn);

        GameObject bossTemp = Instantiate(boss[indexBoss], Vector3.zero, Quaternion.identity);
        bossTemp.GetComponent<BossChaster>().Ref(this);
        bossEvent = true;
    }
}
