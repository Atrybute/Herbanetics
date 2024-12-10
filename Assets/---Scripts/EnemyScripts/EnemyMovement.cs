using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy Movement Variables")]
    [SerializeField] Animator enemyAnim;
    private NavMeshAgent agent;
    Transform target;
    public float updateRate;
    private Coroutine followCoroutine;
    [SerializeField] EnemyScriptableObject enemyScriptableObject;

    private const string CanRun = "CanRun";
    private const string PlayerStr = "Player";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        FindPlayer();
    }
    public void StartChasing()
    {
      
        if (followCoroutine == null)
        {
            followCoroutine = StartCoroutine(FollowTarget());
        }
        else
        {
            Debug.LogWarning("Called StartChasing on enemy that is already chasing! This is likely a bug in some calling class");
        }

    }

    private void Update()
    {
        enemyAnim.SetBool(CanRun, agent.velocity.magnitude > 0.01f);
    }

    void FindPlayer()
    {
        target = GameObject.FindWithTag(PlayerStr).transform;
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new(updateRate);

        while (enabled)
        {
            agent.SetDestination(target.transform.position);
            yield return wait;
        }
    }

    public void ApplyEnemyMovSpeedPen()
    {
        agent.speed *= 0;
    }

    public void RemoveEnemyMoveSpeedPen()
    {
        agent.speed = enemyScriptableObject.Movspeed;
        Debug.Log(agent.speed);
    }

}
