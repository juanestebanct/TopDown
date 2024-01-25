using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.UIElements;
using UnityEngine.Rendering;

public class InvokeEnemy : Enemy
{
    [Header("Invoke")]
    [SerializeField] protected EnemyMovement movent;
    [SerializeField] private int maxInvoke;
    [SerializeField] private float timeToInvoke, currentime;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Randeon")]
    [SerializeField] private float spawnRadius, spawnDistanceBeyondRadius;

    [Header("Audio")]
    [SerializeField] private AudioSource spawnSource;
    [SerializeField] private AudioClip spawnClip;

    private PlayerController playerController;
    private void Start()
    {
        movent = GetComponent<EnemyMovement>();
        playerController = PlayerController.instance;
        PoolEnemies();
    }
    void Update()
    {
        if (currentime >= timeToInvoke)
        {
            invokeEnemy();
            currentime = 0;
        }
        currentime += Time.deltaTime;
    }
    private Vector3 Configuration()
    {
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        // Radio en el que aparecerá el enemigo

        // Generar una posición aleatoria en la circunferencia del círculo
        Vector2 randomSpawnDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPositionOnCircle = playerPosition + randomSpawnDirection * spawnRadius;

        // Mover la posición más allá del radio
        Vector2 spawnPositionBeyondRadius = spawnPositionOnCircle + randomSpawnDirection * spawnDistanceBeyondRadius;

        Vector3 TempPosition = new Vector3(spawnPositionBeyondRadius.x, spawnPositionBeyondRadius.y, 0);
        return TempPosition;
    }
    private void PoolEnemies()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject Tempblaster = Instantiate(enemyPrefab);
            Tempblaster.GetComponent<EnemyMovement>().GetReference(playerController);
            Tempblaster.SetActive(false);
            enemies.Add(Tempblaster);
        }

    }
    public void invokeEnemy()
    {
        for (int i = 0; i < maxInvoke; i++)
        {
            GameObject enemy = enemies.Find(b => !b.activeSelf);

            if (enemy == null)
            {
                enemy = Instantiate(enemyPrefab);
                enemy.GetComponent<EnemyMovement>().GetReference(PlayerController.instance);
                enemies.Add(enemy);
            }
            enemy.transform.position = Configuration();
            enemy.SetActive(true);
            enemy.GetComponent<CommunEnemy>().ResetMovent(Configuration(), MoventPatron.ChaseToPlayer);
        }
    }
}
