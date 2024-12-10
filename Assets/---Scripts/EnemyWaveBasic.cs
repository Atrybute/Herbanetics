using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveBasic : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<GameObject> enemyPrefabs;
        public float spawnInterval;
    }

    [Header("Enemy Wave Settings")]
    [SerializeField] protected List<Wave> waves;
    [SerializeField] protected int totalWaves;
    [SerializeField] protected float timeBtnWaves;
    [SerializeField] protected float startDelay;
    [SerializeField] protected GameObject enemySpawnEffect;
    [SerializeField] protected Transform[] spawnPoints;
   
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
}
