using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokolosheProjectileDamage : MonoBehaviour
{
    public int damageAmount;
    private const string bossStr = "Boss";
    private const string playerStr = "Player";




    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(bossStr) && other.gameObject.CompareTag(playerStr))
        {
            IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
            damagable?.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
