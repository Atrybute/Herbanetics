using UnityEngine;

public class WaveAttack : MonoBehaviour
{
    [SerializeField] int waveDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(waveDamage);
            Destroy(gameObject);
        }

    }
}
