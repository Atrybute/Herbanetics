using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyConfiguration/Melee Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Enemy Stats")]
    public int health;
    public float attackDelay = 1f;
    public int Damage = 5;
    public float attackRadius = 1.5f;
    public bool isRanged = false;

    [Header("NavMesh Configs")]
    public float aiUpdateIntervals = 0.1f;
    public float baseOffset = 0;
    public float acceleration = 8;
    public float Movspeed = 3f;
    public float angularSpeed = 120;
    public int areaMask = -1;
    public int avoidancePriority = 50;
    public float avoidanceHeight = 2f;
    public ObstacleAvoidanceType obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float avoidanceRadius = 0.5f;
    public float stoppingDistance = 0.5f;


}
