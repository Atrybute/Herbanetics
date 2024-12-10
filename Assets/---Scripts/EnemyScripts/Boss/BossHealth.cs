using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour,IDamagable
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int healthThreshold = 50;
    [SerializeField] BossWaveAttacks waveAttacks;
    public bool isBossDefeated;
    [SerializeField] GameObject bossDamageEffect;
    [SerializeField] Transform bossHitSpawn;
    [SerializeField] Image bossHealthSlider;
    [SerializeField] Animator bossAnim;
    private int currentHealth;
    public bool isInSecondPhase;
    [SerializeField] GameObject bossBarrier;
    private bool canTakeDamage;
    [SerializeField] GameObject bossPoints;
    [SerializeField] Transform bossPointsSpawn;


    [Header("Second Phase Variables")]
    [SerializeField] float newWaveForce;
    [SerializeField] float newTimeForWarnings;
    private const string deathStr = "Death";
    private const string isBossStunned = "isStunned";
    private const string bossAttackStr = "isAttacking";
    private void Start()
    {
        currentHealth = maxHealth;
        bossHealthSlider.fillAmount = currentHealth;
        isInSecondPhase = false;
        canTakeDamage = true;
    }

    public void TakeDamage(int damage)
    {   
        if (canTakeDamage)
        {
            currentHealth -= damage;
            Instantiate(bossDamageEffect, bossHitSpawn.position, Quaternion.identity);
            bossHealthSlider.fillAmount -= (float)damage / maxHealth;


            if (currentHealth <= healthThreshold)
            {
                isInSecondPhase = true;

                waveAttacks.attackWaveForce = newWaveForce;
                waveAttacks.warningDelay = newTimeForWarnings;

            }
            if (currentHealth <= 0)
            {
                Instantiate(bossPoints, bossPointsSpawn.position, Quaternion.identity);
                canTakeDamage = false;
                bossBarrier.SetActive(false);
                isBossDefeated = true;
                bossAnim.SetTrigger(deathStr);
                bossAnim.SetBool(isBossStunned, false);
                bossAnim.SetBool(bossAttackStr, false);
                Invoke(nameof(BossCompletedUI), 8f);
            }
        }
        
    }

    public Transform GetTransform()
    {
       return transform;
    }

   
    public void BossCompletedUI()
    {
        ActionAndEventsManager.Instance.bossEnemyActions.BossDefeatedEvent();
        Destroy(gameObject);
    }

    
}
