using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySetup : PoolableObject, IDamagable
{
    public EnemyMovement movement;  // Reference to the enemy movement component
    public NavMeshAgent agent;  // Reference to the NavMeshAgent component
    public int health;  // Current health of the enemy
    public AttackRadius attackRadius;  // Reference to the attack radius component
    [SerializeField] private EnemyScriptableObject enemyScriptableObject;  // ScriptableObject containing enemy configuration
    [SerializeField] EnemyAnimationHandler enemyAnimantionHandler;  // Reference to the enemy animation handler component
    private Coroutine LookCorutine;  // Coroutine for rotating the enemy towards a target
    [SerializeField] GameObject damageEffects;
    [SerializeField] Transform hitSpawn;
    [SerializeField] Image healthBar;
    [SerializeField] GameObject healthpickup;
    
    private void OnEnable()
    {
        SetupAgentConfiguration();  // Setup agent configuration when the enemy is enabled

    }

    private void Awake()
    {
        attackRadius.onAttack += OnAttack;  // Subscribe to the onAttack event of the attack radius component
        healthBar.fillAmount = (enemyScriptableObject.health/enemyScriptableObject.health);
    }

    private void OnAttack(IDamagable target)
    {
        
        enemyAnimantionHandler.PlayAttackAnim();  // Play the attack animation

        if (LookCorutine != null)
        {
            StopCoroutine(LookCorutine);  // Stop the previous look coroutine if it's running
        }

        if(enemyScriptableObject.isRanged)
        {
            LookCorutine = StartCoroutine(LookAt(target.GetTransform()));  // Start the coroutine to rotate the enemy towards the target
        }

    }

    private IEnumerator LookAt(Transform target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);  // Calculate the target rotation
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);  // Rotate the enemy gradually towards the target
            time += Time.deltaTime * 2;
            yield return null;
        }
        transform.rotation = lookRotation;  // Set the final rotation to the target rotation
    }

    public override void OnDisable()
    {
        //base.OnDisable();
       // agent.enabled = false;  // Disable the NavMeshAgent component when the enemy is disabled
    }

    public virtual void SetupAgentConfiguration()
    {
        // Set the agent configuration based on the values from the EnemyScriptableObject
        agent.acceleration = enemyScriptableObject.acceleration;
        agent.angularSpeed = enemyScriptableObject.angularSpeed;
        agent.areaMask = enemyScriptableObject.areaMask;
        agent.avoidancePriority = enemyScriptableObject.avoidancePriority;
        agent.baseOffset = enemyScriptableObject.baseOffset;
        agent.height = enemyScriptableObject.avoidanceHeight;
        agent.obstacleAvoidanceType = enemyScriptableObject.obstacleAvoidanceType;
        agent.radius = enemyScriptableObject.avoidanceRadius;
        agent.speed = enemyScriptableObject.Movspeed;
        agent.stoppingDistance = enemyScriptableObject.stoppingDistance;

        movement.updateRate = enemyScriptableObject.aiUpdateIntervals;  // Set the movement update rate based on the EnemyScriptableObject
        health = enemyScriptableObject.health;  // Set the initial health based on the EnemyScriptableObject
        attackRadius.attackDelay = enemyScriptableObject.attackDelay;  // Set the attack delay based on the EnemyScriptableObject
        attackRadius.damage = enemyScriptableObject.Damage;  // Set the damage value based on the EnemyScriptableObject
        attackRadius.collider.radius = enemyScriptableObject.attackRadius;  // Set the collider radius of the attack radius based on the EnemyScriptableObject
    }

    public void TakeDamage(int damage)
    {
        health -= damage;  // Decrease the enemy's health by the specified damage amount
        healthBar.fillAmount -= (float)damage/enemyScriptableObject.health;
        //enemyAnimantionHandler.PlayDamageTakenAnim();  // Play the damage taken animation
        Instantiate(damageEffects, hitSpawn.position, hitSpawn.transform.rotation);

        if (health < 0)
        {
            //enemyAnimantionHandler.PlayDeathAnim();  // Play the death animation if the health is below zero
            Instantiate(healthpickup, transform.position + new Vector3(0, 2,0), transform.rotation);
            RemoveEnemy();
        }

    }

    public Transform GetTransform()
    {
        return transform;  // Return the enemy's transform
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);  // Disable the game object of the enemy
        ActionAndEventsManager.Instance.enemyWaveActions.EnemyDefeatedEvent();
       
    }
}
