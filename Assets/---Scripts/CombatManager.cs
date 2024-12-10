using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    #region Stomp Front Combat Variables
    [Header("Stomp Variables")]
    [SerializeField] Transform stompPos;
    [SerializeField] Transform stompPos2;
    [SerializeField] Transform stompPos3;
    [SerializeField] float StompfrontMaxDistance;
    [SerializeField] StompFrontSO stompFrontSO;
    [SerializeField] GameObject reticle;
    private float stompCooldown;
    public bool CanStomp { get; private set; }
    #endregion

    #region Smol Bolts Combat Variables
    [field: HorizontalLine(color: EColor.Indigo)]
    [field: Space(2f)]
    [field: Header("Bolts Variables")]
    [SerializeField] Transform firePoint;
    [SerializeField] Transform firePoint2;
    [SerializeField] Transform firePoint3;
    [SerializeField] float SmolboltsXaxDistance;
    [SerializeField] SmolBoltsSO SmolBoltsSO;
    private float fireRateTimer;
    public bool CanShoot { get; private set; }
    #endregion

    #region Melee Attack Combat Variables
    [field: HorizontalLine(color: EColor.Indigo)]
    [field: Space(2f)]
    [field: Header("Melee Attack Variables")]
    [SerializeField] BigPunchSO bigPunchSO;
    [SerializeField] Transform punchPos;
    [SerializeField] Transform punchPos2;
    [SerializeField] Transform punchPos3;
    [SerializeField] float PunchmaxDistance;

    #endregion

    #region Dash Variables
    [field: HorizontalLine(color: EColor.Indigo)]
    [field: Space(2f)]
    [field: Header("Dash Variables")]
    [SerializeField] private GameObject dashEffect;
    [SerializeField] private PlayerConfig playerConfig;
    private float dashDistance;  
    private float dashDuration; 
    private float dashCooldown;   
    private CharacterController controller;

    private bool isDashing = false;
    public bool CanDash { get; private set; }
    private Vector3 dashDirection;
    private Coroutine dashCoroutine;
    #endregion

    private PlayerStateMachine playerStateMachine;
    private SkillManager skillManager;

    #region Unity Callbacks
    private void Awake()
    {
        CanShoot = true;
        CanDash = true;
        CanStomp = true;
        controller= GetComponent<CharacterController>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
        ValueInitializer();

    }

    private void ValueInitializer()
    {
        dashCooldown = playerConfig.dashCooldown;
        dashDuration = playerConfig.dashDuration;
        dashDistance = playerConfig.dashDistance;
        stompCooldown = stompFrontSO.abilityCooldown;
    }

    private void Start()
    {
        skillManager = SkillManager.Instance;
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;

        if (fireRateTimer > SmolBoltsSO.fireRate)
        {
            CanShoot = true;
        }
    }
    #endregion

    #region Attack Functions

    #region Stomp Front Attack Functions
    public void StompAttack()
    {
        int numStomps = skillManager.waveSkillLevel + 1;

        if (skillManager.waveSkillLevel >= 2)
            numStomps = 3;

        for (int i = 0; i < numStomps; i++)
        {
            switch (i)
            {
                case 0:
                    SpawnStomp(stompPos, stompFrontSO.abilityPrefab);
                    break;
                case 1:
                    SpawnStomp(stompPos2, stompFrontSO.abilityPrefab2);
                    break;
                case 2:
                    SpawnStomp(stompPos3, stompFrontSO.abilityPrefab3);
                    break;

            }
        }
    }
    private IEnumerator StompAttack(GameObject stompObject, Transform targetEnemy)
    {   
        
        float elapsedTime = 0f;

        while (elapsedTime < stompFrontSO.abilityDuration)
        {
            float t = elapsedTime / stompFrontSO.abilityDuration;
            float currentWaveSize = Mathf.Lerp(0f, stompFrontSO.stompDesiredSize, t);

            if (targetEnemy != null)
            {
                Vector3 directionToTarget = (targetEnemy.position - stompObject.transform.position).normalized;
                reticle.transform.position = targetEnemy.transform.position + directionToTarget;
                stompObject.transform.position += directionToTarget * stompFrontSO.stompSpeed * Time.deltaTime;

            }
            else
            {
                stompObject.transform.position += stompFrontSO.stompSpeed * Time.deltaTime * stompObject.transform.forward;
            }

            stompObject.transform.localScale = Vector3.one * currentWaveSize;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(stompObject);

        StartCoroutine(StartStompCooldown());
       
    }
    private void SpawnStomp(Transform stompPosition, GameObject stomp)
    {
        stomp = Instantiate(stomp, stompPosition.position, stompPosition.rotation);
        stomp.transform.localScale = Vector3.zero;

        if (stompFrontSO.canAutoAim)
        {
            if (FindClosestEnemy(stompPosition.position, out Transform targetEnemy))
            {
                reticle.SetActive(true);
                LookAtTarget(targetEnemy);
                StartCoroutine(StompAttack(stomp, targetEnemy));
                Invoke(nameof(TurnOffReticle), 0.25f);
              
            }
        }
        else
        {
            StartCoroutine(StompAttack(stomp, null));
        }


    }

    private IEnumerator StartStompCooldown()
    {
        CanStomp = false;
        yield return new WaitForSeconds(stompCooldown);
        CanStomp = true;
    }

    #endregion

    #region SmolBolts Attack Functions

    public void ProjectileSpawner()
    {

        int numBolts = skillManager.projectileSkillLevel + 1;

        if (skillManager.projectileSkillLevel >= 2)
            numBolts = 3;


        for (int i = 0; i < numBolts; i++)
        {
            switch (i)
            {
                case 0:
                    SpawnBolt(firePoint, SmolBoltsSO.boltPrefab);
                    break;
                case 1:
                    SpawnBolt(firePoint2, SmolBoltsSO.boltPrefab2);
                    break;
                case 2:
                    SpawnBolt(firePoint3, SmolBoltsSO.boltPrefab3);
                    break;

            }
        }
    }

    private void SpawnBolt(Transform firePoint, GameObject bolt)
    {
        bolt = Instantiate(bolt, firePoint.position, firePoint.rotation);

        if (SmolBoltsSO.canAutoAim)
        {
            if (FindClosestEnemy(firePoint.position, out Transform targetEnemy))
            {
                reticle.SetActive(true);
                Vector3 directionToTarget = (targetEnemy.position - bolt.transform.position).normalized;
                reticle.transform.position = targetEnemy.transform.position + directionToTarget;
                bolt.GetComponent<Rigidbody>().velocity = directionToTarget * SmolBoltsSO.fireForce;
                bolt.transform.rotation = Quaternion.LookRotation(directionToTarget);
                LookAtTarget(targetEnemy);
                Invoke(nameof(TurnOffReticle), 0.5f);
               
            }
            else
            {
                bolt.GetComponent<Rigidbody>().AddForce(firePoint.forward * SmolBoltsSO.fireForce, ForceMode.Impulse);
            }
        }
        else
        {

            bolt.GetComponent<Rigidbody>().AddForce(firePoint.forward * SmolBoltsSO.fireForce, ForceMode.Impulse);
        }


    }
    #endregion

    #region Melee Attack Functions
    public void Punch()
    {
        int numPunches = skillManager.punchSkillLevel + 1;

        if (skillManager.punchSkillLevel >= 2)
            numPunches = 3;

        for (int i = 0; i < numPunches; i++)
        {
            Transform punchPosition = GetPunchPosition(i);
            SpawnPunch(punchPosition, i);
        }
    }
    private Transform GetPunchPosition(int punchIndex)
    {
        switch (punchIndex)
        {
            case 0:
                return punchPos;
            case 1:
                return punchPos2;
            case 2:
                return punchPos3;

            default:
                return punchPos;
        }
    }

    private void SpawnPunch(Transform punchPosition, int punchIndex)
    {
        GameObject punch;

        if (punchIndex == 0)
        {
            punch = Instantiate(bigPunchSO.punchPrefab, punchPosition.position, bigPunchSO.punchPrefab.transform.rotation);

        }
        else if (punchIndex == 1)
        {
            punch = Instantiate(bigPunchSO.punchPrefab2, punchPosition.position, bigPunchSO.punchPrefab2.transform.rotation);
        }
        else if (punchIndex == 2)
        {
            punch = Instantiate(bigPunchSO.punchPrefab3, punchPosition.position, bigPunchSO.punchPrefab3.transform.rotation);
        }
        else
        {
            punch = Instantiate(bigPunchSO.punchPrefab, punchPosition.position, bigPunchSO.punchPrefab.transform.rotation);
        }


        if (bigPunchSO.canAutoAim)
        {
            if (FindClosestEnemy(punchPosition.position, out Transform targetEnemy))
            {
                reticle.SetActive(true);
                Vector3 directionToTarget = (targetEnemy.position - punch.transform.position).normalized;
                reticle.transform.position = targetEnemy.transform.position + directionToTarget;
                punch.GetComponent<Rigidbody>().velocity = directionToTarget * bigPunchSO.fireForce;
                punch.transform.rotation = Quaternion.LookRotation(directionToTarget);
                LookAtTarget(targetEnemy);
                Invoke(nameof(TurnOffReticle), 0.5f);
            }
            else
            {
                punch.GetComponent<Rigidbody>().AddForce(punchPosition.forward * bigPunchSO.fireForce, ForceMode.Impulse);
            }
        }
        else
        {

            punch.GetComponent<Rigidbody>().AddForce(punchPosition.forward * bigPunchSO.fireForce, ForceMode.Impulse);
        }




        Vector3 movDir = punchPosition.forward.normalized;
        //punch.transform.rotation = Quaternion.LookRotation(movDir);
    }
    #endregion

    #region Dash Functions
 
    public void StartDash()
    {
        if (dashCoroutine != null) return; 

        dashDirection = transform.forward;

        if (CanDash)
        {
            dashCoroutine = StartCoroutine(DashCoroutine());
        }
       
    }
    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        float elapsedTime = 0f;
        dashEffect.SetActive(true);

        while (elapsedTime < dashDuration)
        {
            float dashSpeed = dashDistance / dashDuration; // Calculate dash speed
            controller.Move(dashSpeed * Time.deltaTime * dashDirection); // Move the character
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        dashEffect.SetActive(false);
        isDashing = false;
        dashCoroutine = null;

        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        CanDash = false;
        yield return new WaitForSeconds(dashCooldown);
        CanDash = true;
    }
    #endregion

    #endregion

    #region Utility Functions
    void TurnOffReticle()
    {
        reticle.SetActive(false);
    }

    private bool FindClosestEnemy(Vector3 position, out Transform target)
    {
        target = null;
        float closestDistance = StompfrontMaxDistance;


        EnemySetup[] enemies = FindObjectsOfType<EnemySetup>();
        BossTag[] bossTags = FindObjectsOfType<BossTag>();


        foreach (EnemySetup enemy in enemies)
        {
            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }

        foreach (BossTag boss in bossTags)
        {

            float distance = Vector3.Distance(position, boss.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = boss.transform;
            }
        }

        return target != null;
    }

    protected void LookAtTarget(Transform target)
    {    
        Vector3 direction = target.position - transform.position;
        direction.y = 0; 
    
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                playerStateMachine.PlayerRotationValue * Time.deltaTime
            );
        }
    }
    #endregion
}
