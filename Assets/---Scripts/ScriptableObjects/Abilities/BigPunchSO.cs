using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Big Punch")]
public class BigPunchSO : ScriptableObject
{
    [Header("Big Punch Data")]
    public GameObject punchPrefab;
    public GameObject punchPrefab2;
    public GameObject punchPrefab3;
    public float fireForce;
    public float fireRate;
    public int bigPunchDamage;
    public bool canAutoAim;
}
