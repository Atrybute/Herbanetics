using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackRadius : AttackRadius
{
    public NavMeshAgent agent;
    public RingProjectile projectilePrefab;
    public Vector3 projectileSpawnOffset = new(0, 1, 0);
    public LayerMask layerMask;
    private ObjectPool projectilePool;
    [SerializeField] private float sphereCastRadius = 0.1f;
    private RaycastHit hit;
    private IDamagable targetDamageable;
    private RingProjectile projectile;

    protected override void Awake()
    {
        base.Awake();

        projectilePool = ObjectPool.CreateInstance(projectilePrefab, Mathf.CeilToInt((1 / attackDelay) * projectilePrefab.autoDestroyTime));
    }
    protected override IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);

        yield return wait;

        while (damagables.Count > 0)
        {
            for (int i = 0; i < damagables.Count; i++)
            {

                if (HasLineOfSightTo(damagables[i].GetTransform()))
                {
                    targetDamageable = damagables[i];
                    onAttack?.Invoke(damagables[i]);
                    break;
                }
            }
            if (targetDamageable != null)
            {
                PoolableObject poolableObject = projectilePool.GetObject();

                if (poolableObject != null)
                {
                    projectile = poolableObject.GetComponent<RingProjectile>();
                    projectile.damage = damage;
                    projectile.transform.position = transform.position + projectileSpawnOffset;
                    projectile.transform.rotation = agent.transform.rotation;
                    projectile.projectileRigidbody.AddForce(agent.transform.forward * projectilePrefab.movSpeed, ForceMode.VelocityChange);
                }
            }
            else
            {
                agent.enabled = true;
            }

            yield return wait;

            if (targetDamageable == null || !HasLineOfSightTo(targetDamageable.GetTransform()))
            {
                agent.enabled = true;
            }

            damagables.RemoveAll(DisabledDamagables);

        }

        agent.enabled = true;
        AttackRoutine = null;
    }

    private bool HasLineOfSightTo(Transform target)
    {
        if (Physics.SphereCast(transform.position + projectileSpawnOffset, sphereCastRadius, ((target.position + projectileSpawnOffset) - (transform.position + projectileSpawnOffset)).normalized, out hit, collider.radius, layerMask))
        {
            IDamagable damagable;
            if (hit.collider.TryGetComponent<IDamagable>(out damagable))
            {
                return damagable.GetTransform() == target;
            }

        }

        return false;
    }


    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (AttackRoutine == null)
        {
            agent.enabled = true;
        }
    }
}
