using UnityEngine;

[CreateAssetMenu(menuName ="PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("PlayerStats")]
    public int maxHealth;
    public float movementSpeed;
    public float playerRotationValue;
    public float rotationSmoothValue;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    public float dashDistance;
}
