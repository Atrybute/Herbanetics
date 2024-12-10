using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TokolosheController : MonoBehaviour,IDamagable
{
    [Header("Boss Variables")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] int healthThreshold = 50;
    public bool isBossDefeated;
    [SerializeField] Image bossHealthSlider;
    [SerializeField] GameObject bossDamageEffect;
    [SerializeField] Transform bossHitSpawn;
    Animator bossAnim;
    private int currentHealth;
    public bool isInSecondPhase;
    private const string Death = "Death";
    private const string isMoving = "Walk";
    private const string isAttacking = "Attack";
    private bool canTakeDamage;
    [SerializeField] GameObject healthpickup;
    private void Awake()
    {
        bossAnim = GetComponent<Animator>();
        currentHealth = maxHealth;
        bossHealthSlider.fillAmount = currentHealth;
        canTakeDamage = true;
    }
    private void Start()
    {
       
        isInSecondPhase = false;
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



            }
            if (currentHealth <= 0)
            {   
                canTakeDamage = false;
                Instantiate(healthpickup, transform.position + new Vector3(0, 2, 0), transform.rotation);
                isBossDefeated = true;
                bossAnim.SetTrigger(Death);
                bossAnim.SetBool(isMoving, false);
                bossAnim.SetBool(isAttacking, false);
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
    }
}
