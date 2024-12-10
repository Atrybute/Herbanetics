using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    [SerializeField] BigPunchSO bigPunchSO;
    private const string playerStr = "Player";
    private const string bossBarrier = "BossBarrier";
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out var damagable) &&  !other.gameObject.CompareTag(playerStr))
        {
            damagable.TakeDamage(bigPunchSO.bigPunchDamage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag(bossBarrier))
        {
            Destroy(gameObject);
        }
    }



}
