using System.Collections;
using UnityEngine;

public class EnemyWaveSpawner : EnemyWaveBasic
{
    #region Enemy Wave Settings
    [Header("Enemy Wave Settings")]

    private int currentWaveIndex;
    private int enemiesRemaining;
    private float waveTimer;
    EnemyMovement enemyMovement;
    [SerializeField] float healthIncreasePercentage;
    [SerializeField] float delayTimeAfterWaveCompleteion = 2f;
    #endregion


    private void OnEnable()
    {
        ActionAndEventsManager.Instance.enemyWaveActions.OnEnemyDefeated += EnemyDefeated;
       
    }

    private void OnDisable()
    {
        ActionAndEventsManager.Instance.enemyWaveActions.OnEnemyDefeated -= EnemyDefeated;
      
    }

    protected override void Start()
    {
        StartCoroutine(SpawnWaves(startDelay));
        ActionAndEventsManager.Instance.enemyWaveActions.InitializeWaveTextEvent(currentWaveIndex + 1, totalWaves);
       
    }

    IEnumerator SpawnWaves(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);

        while (currentWaveIndex < totalWaves)
        {
            Wave currentWave = waves[currentWaveIndex];
            enemiesRemaining = currentWave.enemyPrefabs.Count;  // Ensure this resets for each wave

            for (int i = 0; i < currentWave.enemyPrefabs.Count; i++)
            {

                GameObject enemyPrefab = currentWave.enemyPrefabs[i];
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemySpawnEffect, spawnPoint.position, enemySpawnEffect.transform.rotation);
                yield return new WaitForSeconds(0.05f);

                SpawnEnemy(enemyPrefab, spawnPoint);

                yield return new WaitForSeconds(currentWave.spawnInterval);

            }

            // Wait until all enemies are defeated before proceeding
            yield return new WaitUntil(() => enemiesRemaining <= 0);


            currentWaveIndex++;
            waveTimer = timeBtnWaves;
            if(currentWaveIndex < totalWaves)
            {
                ActionAndEventsManager.Instance.enemyWaveActions.UpdateWaveTextEvent(currentWaveIndex + 1, totalWaves);
                ActionAndEventsManager.Instance.playerHealthActions.IncreaseHealth(healthIncreasePercentage);
                Debug.Log($"{currentWave.name} Completed. Proceeding to the next wave");
            }
            yield return new WaitForSeconds(timeBtnWaves);
        }

        yield return new WaitForSeconds(delayTimeAfterWaveCompleteion);
        
       
        ActionAndEventsManager.Instance.enemyWaveActions.WaveCompletedEvent();
        
        
    }

    private void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        newEnemy.SetActive(true);
        enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        enemyMovement.StartChasing();
       
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
    } 
}

