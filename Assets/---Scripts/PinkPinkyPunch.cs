using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkPinkyPunch : MonoBehaviour
{
    [SerializeField] int pinkypinkyDamage;
    public const string bossStr = "Boss";
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable) && !other.CompareTag(bossStr))
        {
            damagable.TakeDamage(pinkypinkyDamage);

        }

    }
}
