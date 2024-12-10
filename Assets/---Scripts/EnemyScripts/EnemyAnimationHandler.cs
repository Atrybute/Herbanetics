using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator anim;
    private const string attackStr = "Attack";

   
    public void PlayAttackAnim()
    {
        anim.SetTrigger(attackStr);
    }
}
