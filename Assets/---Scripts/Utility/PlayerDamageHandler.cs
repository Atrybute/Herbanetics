using UnityEngine;


public class PlayerDamageHandler : MonoBehaviour,IDamagable
{
    [Header("Player Damage Variables")]
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Transform hitSpawn;
    private PlayerStateMachine playerStateMachine;
    
    private int maxHealth;
    private int currentHealth;
  
   
    private bool canTakeDamage;
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    #region Unity Callbacks
    private void Awake()
    {
        maxHealth = playerConfig.maxHealth;
        currentHealth = maxHealth;
        canTakeDamage = true;
        ActionAndEventsManager.Instance.playerHealthActions.FillPlayerHealth(maxHealth);
        playerStateMachine = GetComponent<PlayerStateMachine>();

    }

    private void OnEnable()
    {
        ActionAndEventsManager.Instance.playerHealthActions.OnHealthIncrease += IncreaseHealth;
    }

    private void OnDisable()
    {
        ActionAndEventsManager.Instance.playerHealthActions.OnHealthIncrease -= IncreaseHealth;
    }
    #endregion

    public void TakeDamage(int amount)
    {   
        if(canTakeDamage)
        {
            currentHealth -= amount;
            Instantiate(hitEffect, hitSpawn.position, Quaternion.identity);
            float uiDamageAmount = -(float)amount / playerConfig.maxHealth;
            ActionAndEventsManager.Instance.playerDamagActions.PlayerHitEvent(uiDamageAmount);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
       
    }

    private void IncreaseHealth(float healthIncreasePercent)
    {   
        float increase = (healthIncreasePercent / 100) * (float)maxHealth;
        int calculatedHealth = (int)increase;
        if (currentHealth <= maxHealth)
        {
            currentHealth += calculatedHealth;
            float uiHealthIncreaseAmount = (float)calculatedHealth / playerConfig.maxHealth;
            ActionAndEventsManager.Instance.playerDamagActions.PlayerHitEvent(uiHealthIncreaseAmount);
        }
    }
    private void Die()
    {
       playerStateMachine.SwitchState(new PlayerDeathState(playerStateMachine));
       canTakeDamage = false;   
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public void playerDeathEventCall()
    {
        ActionAndEventsManager.Instance.playerHealthActions.PlayerDeathEvent();
    }
}
