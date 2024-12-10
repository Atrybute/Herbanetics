using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/Smol Bolts")]
public class SmolBoltsSO : ScriptableObject
{
    [Header("Smol Bolt Data")]
    public GameObject boltPrefab;
    public GameObject boltPrefab2;
    public GameObject boltPrefab3;
    public float fireForce;
    public float fireRate;
    public int boltDamage;
    public bool canAutoAim;
}
