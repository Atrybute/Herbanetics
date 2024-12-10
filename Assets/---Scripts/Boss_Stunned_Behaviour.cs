using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Stunned_Behaviour : StateMachineBehaviour
{
    GameObject bossBarrier;
    BossHealth bossHealth;
    private const string bossBarrierStr = "BossBarrier";
    private const string isBossStunned = "isStunned";
    [SerializeField] float stunDuration;
    [SerializeField] float secondPhaseStunDuration;
    private float countDown;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        bossBarrier = GameObject.FindWithTag(bossBarrierStr);
        bossHealth =  animator.GetComponent<BossHealth>();
        bossBarrier.SetActive(false);

        if(!bossHealth.isInSecondPhase)
        {
            countDown = stunDuration;
        }
        else
        {
            countDown = secondPhaseStunDuration;
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        countDown -= Time.deltaTime;
       
        if(countDown <= 0)
        {
            bossBarrier.SetActive(true);
            animator.SetBool(isBossStunned, false);
           
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

   
}
