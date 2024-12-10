using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TokolosheAttacks : MonoBehaviour
{
    [Header("Attack Variables")]
    [SerializeField] Transform[] pathPoints;
    [SerializeField] Transform playerPos;
    [SerializeField] float movSpeed;
    [SerializeField] float minimumDistance;
    [SerializeField] Transform throwPos;
    Animator animator;
    private const string isMoving = "Walk";
    private const string isAttacking = "Attack";
    [SerializeField] float projectileSpeed;
    [SerializeField] float lobHeight;
    [SerializeField] GameObject projectilePrefab;
    private bool canAttack;
    private bool canMove;
    [SerializeField] TokolosheController tokolosheController;
    [SerializeField] int numOfAttack;
    private int attackCounter;
    private bool canGenerateIndex;
    int index;
    [SerializeField] int secondPhaseNumberOfAttacks;
    [SerializeField] GameObject bossShield;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canAttack = true;
        canMove = false;
        canGenerateIndex = true;
    }

   
    public int GetPosition()
    {
        int index = UnityEngine.Random.Range(0, pathPoints.Length);

        return index;
      
    }

    public void WalkToPosition()
    {
        if (canGenerateIndex)
        {
             index = GetPosition();
             canGenerateIndex= false;
        }

        if(Vector3.Distance(transform.position, pathPoints[index].position) > minimumDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, pathPoints[index].position, movSpeed * Time.deltaTime);
            LookAtTarget(pathPoints[index].position);
        }
        else
        {
            AttackTransition();
            canAttack = true;
            canMove = false;
        }
    }

    private void LookAtTarget(Vector3 position)
    {
        Vector3 lookPos = (position - transform.position).normalized;
        lookPos.y = 0f;
        quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    public void ThrowLobbingProjectile()
    {
        if (canAttack)
        {
            if (!tokolosheController.isInSecondPhase && attackCounter < numOfAttack)
            {

                LobbingProjectile(playerPos.position);
                attackCounter++;

            }else if(!tokolosheController.isInSecondPhase &&  attackCounter >= numOfAttack)
            {
                
                WalkTransition();
                attackCounter = 0;
                canAttack = false;

            }else if(tokolosheController.isInSecondPhase && attackCounter < numOfAttack)
            {
                LobbingProjectile(playerPos.position);
                
                attackCounter++;

            }else if(tokolosheController.isInSecondPhase && attackCounter >= numOfAttack)
            {
                WalkTransition();
                attackCounter = 0;
                canAttack = false;
            }
           
        }
      
        
    }

    void AttackTransition()
    {
        bossShield.SetActive(false);
        animator.SetBool(isMoving, false);
        animator.SetBool(isAttacking, true);
    }

    void WalkTransition()
    {
        bossShield.SetActive(true);
        canMove = true;
        canGenerateIndex = true;
        animator.SetBool(isMoving, true);
        animator.SetBool(isAttacking, false);
    }

     void LobbingProjectile(Vector3 targetPosition)
    {
         Vector3 direction = (targetPosition - transform.position).normalized;


        // Calculate the distance to the target on the XZ plane
        float distance = Vector3.Distance(transform.position, targetPosition);

        // Calculate the initial velocity required for a lobbing projectile
        float initialVelocity = Mathf.Sqrt((distance * Physics.gravity.magnitude) / Mathf.Sin(2 * Mathf.Atan2(lobHeight, distance)));

        // Calculate the velocity vector
        Vector3 velocity = direction * initialVelocity;

        // Spawn the projectile
        GameObject projectile = Instantiate(projectilePrefab, throwPos.position, Quaternion.identity);

        // Get the Rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Apply the velocity to the projectile
        projectileRb.velocity = velocity;

        // Apply rotation (optional)
        projectile.transform.LookAt(targetPosition);
    }

    private void Update()
    {
        if(canMove)
        {
            WalkToPosition();
        }

        if (canAttack)
        {
            LookAtTarget(playerPos.position);
        }

        if (tokolosheController.isInSecondPhase)
        {
            numOfAttack = secondPhaseNumberOfAttacks;
        }

        if (tokolosheController.isBossDefeated)
        {
            canAttack = false;
        }
    }

}
