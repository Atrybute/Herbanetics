using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    //private EnemySetup enemySetup;
    public List <int> tickTimers = new List<int>();
    [SerializeField] StompFrontSO StompFrontSO;
    private IDamagable damagable;

    private void Awake()
    {
       // enemySetup = GetComponent<EnemySetup>();
        damagable = GetComponent<IDamagable>();
    }

    public void ApplyStompTick(int ticks)
    {
        if(tickTimers.Count <= 0)
        {
            tickTimers.Add(ticks);
            StartCoroutine(StompTick());
        }
        else
        {
            tickTimers.Add (ticks);
        }
    
    }

    IEnumerator StompTick()
    {
        while (tickTimers.Count > 0)
        {
            for(int i = 0; i < tickTimers.Count; i++)
            {
                tickTimers[i]--;
            }

            //enemySetup.TakeDamage(StompFrontSO.stompTickDamage);

            damagable.TakeDamage(StompFrontSO.stompTickDamage);
            tickTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
