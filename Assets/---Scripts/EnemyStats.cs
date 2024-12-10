using UnityEngine;

public class EnemyStats : MonoBehaviour,IDamagable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] GameObject deathImpact;
    [SerializeField] GameObject damageImpact;
    [SerializeField] Transform fxSpawnPoint;
    private int currentHealth;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Instantiate(damageImpact, fxSpawnPoint.position, Quaternion.identity);
        if (currentHealth <= 0)
        {
            Die();
           
        }
    }
   
    private void Die()
    {
        Instantiate(deathImpact, fxSpawnPoint.position, transform.rotation);
        Destroy(gameObject);
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
