using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PinkyPinkyAttacks : MonoBehaviour
{
    [Header("Attack Variables")]
    [SerializeField] Transform playerPos;
    [SerializeField] float movSpeed;
    [SerializeField] float minimumDistance;
    Animator animator;
    private const string isMoving = "Walk";
    private const string isAttacking = "Attack";
    private const string isStunned = "isStunned";
    [SerializeField] PinkyPinkyController pinkyPinkyController;
    [SerializeField] int numOfAttack;
    private int attackCounter;
    [SerializeField] int secondPhaseNumberOfAttacks;
    [SerializeField] float stunDuration;
    private bool canChase;
    private bool canAttack;
    [SerializeField] GameObject bossShield;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canChase = true;
        canAttack = true;
    }

    public void RollToPlayer()
    {   
        bossShield.SetActive(true);
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, movSpeed * Time.deltaTime);
        LookAtTarget(playerPos.position);
        animator.SetBool(isMoving, true);
        animator.SetBool(isAttacking, false);
    }

    private void LookAtTarget(Vector3 position)
    {
        Vector3 lookPos = (position - transform.position).normalized;
        lookPos.y = 0f;
        quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    void AttackTransition()
    {
        LookAtTarget(playerPos.position);
        bossShield.SetActive(false);
        animator.SetBool(isMoving, false);
        animator.SetBool(isAttacking, true);
    }

    public void AttackCounter()
    {
        if(!pinkyPinkyController.isInSecondPhase && attackCounter < numOfAttack)
        {
            attackCounter++;
           
                
        }else if(!pinkyPinkyController.isInSecondPhase && attackCounter >= numOfAttack)
        {   
            
            StunTransition();
            attackCounter = 0;

        }else if(pinkyPinkyController.isInSecondPhase && attackCounter < numOfAttack)
        {
            attackCounter++;

        }else if(pinkyPinkyController.isInSecondPhase && attackCounter >= numOfAttack)
        {
            StunTransition();
            attackCounter = 0;
        }
        
    }
   
    void StunTransition()
    {
        animator.SetBool(isMoving, false);
        animator.SetBool(isAttacking,false);
        animator.SetBool(isStunned, true);
        bossShield.SetActive(false);
        canChase = false;
        canAttack = false;
        StartCoroutine(StunCooldown());
    }

    IEnumerator StunCooldown()
    {
        yield return new WaitForSeconds(stunDuration);

        animator.SetBool(isStunned, false);
        animator.SetBool(isMoving, true);
        animator.SetBool(isAttacking, false);
        canChase = true;
        canAttack = true;
    }

    private void Update()
    {       
        if(Vector3.Distance(playerPos.position, transform.position) > minimumDistance && canChase)
        {
            RollToPlayer();
        }
        else if(Vector3.Distance(playerPos.position,transform.position) < minimumDistance && canAttack)
        {   
           
            AttackTransition();
           
        }

       
        if (pinkyPinkyController.isBossDefeated)
        {
            canAttack = false;
            canChase= false;
        }

        if (pinkyPinkyController.isInSecondPhase)
        {
            numOfAttack = secondPhaseNumberOfAttacks;
        }
    }
}
