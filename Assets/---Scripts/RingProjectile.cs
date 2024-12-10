using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RingProjectile : PoolableObject
{
    public float autoDestroyTime;
    public float movSpeed;
    public int damage;
    public Rigidbody projectileRigidbody;

    private const string DisableMethodName = "Disable";
  

    private void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        CancelInvoke(DisableMethodName);
        //Invoke(DisableMethodName, autoDestroyTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable;
        if (other.TryGetComponent<IDamagable>(out damagable))
        {
            damagable.TakeDamage(damage);
        }

        Disable();
    }

    private void Disable()
    {
        CancelInvoke(DisableMethodName);
        projectileRigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
