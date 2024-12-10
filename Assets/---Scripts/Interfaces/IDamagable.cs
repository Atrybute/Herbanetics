using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damage);
    Transform GetTransform();
}
