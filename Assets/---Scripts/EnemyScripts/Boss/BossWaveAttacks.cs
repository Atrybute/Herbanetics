using System.Collections;
using UnityEngine;

public class BossWaveAttacks : MonoBehaviour
{
    [SerializeField] GameObject[] warningIndicators;
    [SerializeField] GameObject [] bossWaveAttacks;
    [SerializeField] Transform[] waveAttackSpots;
    public float timeBetweenWaveAttacks;
    public float attackWaveForce;
    [SerializeField] float numberOfAttacks;
    public float warningDelay;
    [SerializeField] Animator bossAnim;
    private const string isBossStunned = "isStunned";
    private const string bossAttackStr = "isAttacking";

    [Header("Second Phase Stuff")]
    [SerializeField] BossHealth bossHealth;

    public IEnumerator BossWaveAttackRoutine()
    {
        float tempNumOfAttacks = numberOfAttacks;

        while (tempNumOfAttacks > 0)
        {   
            if(!bossHealth.isInSecondPhase)
            {
                // Choose a random set of warning indicators and wave attack from the enemy wave spawner
                int randomWaveSetIndex = Random.Range(0, warningIndicators.Length);
                warningIndicators[randomWaveSetIndex].SetActive(true);
                yield return new WaitForSeconds(warningDelay); // Add a short delay for warning visibility

                WaveAttack(randomWaveSetIndex);
                warningIndicators[randomWaveSetIndex].SetActive(false);

                // Wait for the wave attack to finish before spawning the next wave
                yield return new WaitForSeconds(timeBetweenWaveAttacks);
            }
            else
            {
                int randomWaveSetIndex = Random.Range(0, warningIndicators.Length);
                warningIndicators[randomWaveSetIndex].SetActive(true);
                    
               
                int randomWaveSetIndex2 = Random.Range(0, warningIndicators.Length);
                warningIndicators[randomWaveSetIndex2].SetActive(true);
                yield return new WaitForSeconds(warningDelay);


                WaveAttack(randomWaveSetIndex); // Spawn the first wave
                WaveAttack(randomWaveSetIndex2); // Spawn the second wave
                warningIndicators[randomWaveSetIndex].SetActive(false);
                warningIndicators[randomWaveSetIndex2].SetActive(false);


                // Wait for the wave attack to finish before spawning the next wave
                yield return new WaitForSeconds(timeBetweenWaveAttacks);
            }
            

            tempNumOfAttacks--;
            
        }

        bossAnim.SetBool(bossAttackStr, false);
        bossAnim.SetBool(isBossStunned, true);
    }

    public void WaveAttack(int attackIndex)
    {
       GameObject wave = Instantiate(bossWaveAttacks[attackIndex], waveAttackSpots[attackIndex].position, Quaternion.identity);
       wave.GetComponent<Rigidbody>().AddForce(waveAttackSpots[attackIndex].forward * attackWaveForce, ForceMode.Impulse);
    }
}
