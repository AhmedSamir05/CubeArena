using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [Header("Spawn Settings")]
    public GameObject[] enemyPrefabs;
    int enemyCount;
    [SerializeField] float spawnRadius = 20f;
    [SerializeField] float enemyRadius = 1f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;

    [HideInInspector] public bool isSpawningComplete;

    [SerializeField] WaveData[] waveData;
    WaveData currentWaveData;


    bool isSpawningPaused = false;
    bool shouldSpawnButPaused = false;
    int currentWaveNum;
    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.OnNewWave += SatrtNewWave;
        GameManager.Instance.OnPauseChanged += SpwaningState;
    }

    private void SatrtNewWave(int waveNum)
    {        
        //stop spawing from previous wave
        StopAllCoroutines();

        currentWaveNum = waveNum;
        if (isSpawningPaused)
        {
            shouldSpawnButPaused = true;
            return;
        }
        UpdateWaveData(waveNum);
        // Stop current initiation and Create new wave
        StartCoroutine(SpawnEnemies());
    }

    private void UpdateWaveData(int waveNum)
    {
        //get wave data or last wave data
        if (waveData.Length >= waveNum)
            currentWaveData = waveData[waveNum - 1];
        else
            currentWaveData = waveData[waveData.Length - 1];


        enemyPrefabs = currentWaveData.enemyPrefabs;
        //increse 10 every wave after we reach the last wave 
        enemyCount = currentWaveData.enemyCount + Mathf.Max(0, waveNum - waveData.Length) * 10;
    }

    //Note: Created with LLM but with detailed instruction for the time :)
    IEnumerator SpawnEnemies()
    {
        int spawned = 0;
        int attempts = 0;
        isSpawningComplete = false;
        while (spawned < enemyCount && attempts < enemyCount * 10)
        {
            attempts++;

            // Random XZ position
            Vector3 randomPos = transform.position +
                                new Vector3(
                                    Random.Range(-spawnRadius, spawnRadius),
                                    10f,
                                    Random.Range(-spawnRadius, spawnRadius)
                                );

            // Raycast down to detect any collider
            if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, 50f))
            {
                // Check if the hit object is on the Ground layer
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Vector3 spawnPoint = hit.point;

                    // Check for overlap with other enemies
                    bool blocked = Physics.CheckSphere(
                        spawnPoint,
                        enemyRadius,
                        enemyLayer
                    );

                    if (!blocked)
                    {
                        GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

                        // Spawn random enemy
                        PoolManager.Instance.Instantiate(
                            randomEnemy,
                            spawnPoint,
                            Quaternion.Euler(
                                randomEnemy.transform.eulerAngles.x,
                                Random.Range(0f, 360f),
                                randomEnemy.transform.eulerAngles.z
                            )
                        );

                        spawned++;
                    }
                }
            }


            yield return new WaitForEndOfFrame();
        }
        isSpawningComplete = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void SpwaningState(bool isPaused)
    {
        isSpawningPaused = isPaused;

        if (!isPaused)
        {
            if (shouldSpawnButPaused)
            {
                SatrtNewWave(currentWaveNum);
                shouldSpawnButPaused = false;

            }
        }
    }
}
