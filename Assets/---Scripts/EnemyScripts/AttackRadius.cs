using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
    public new SphereCollider collider;  // Reference to the SphereCollider component
    protected List<IDamagable> damagables = new List<IDamagable>();  // List of IDamagable objects within the attack radius
    public int damage;  // Amount of damage to inflict on the targets
    public delegate void AttackEvent(IDamagable target);  // Delegate for the onAttack event
    public AttackEvent onAttack;  // Event triggered when attacking a target
    protected Coroutine AttackRoutine;  // Coroutine for the attack routine
    public float attackDelay;  // Delay between attacks
    private bool canAttack;
 
    protected virtual void Awake()
    {
        collider = GetComponent<SphereCollider>();  // Get the SphereCollider component
        canAttack = true;
    }

    protected virtual void OnEnable()
    {
        //PlayerDamageHandler.StopAttacking += StopAttack;
    }

    protected virtual void OnDisable()
    {
        //PlayerDamageHandler.StopAttacking -= StopAttack;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagables.Add(damagable);  // Add the IDamagable object to the list

            AttackRoutine ??= StartCoroutine(Attack());  // Start the attack routine coroutine
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagables.Remove(damagable);  // Remove the IDamagable object from the list

            if (damagables.Count == 0)
            {
                StopCoroutine(AttackRoutine);  // Stop the attack routine coroutine if there are no targets
                AttackRoutine = null;
            }
        }
    }

    protected virtual IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);  // Create a wait duration between attacks

        yield return wait;

        IDamagable closestDamagable = null;
        float closestDistance = float.MaxValue;

        while (damagables.Count > 0 && canAttack)
        {
            for (int i = 0; i < damagables.Count; i++)
            {
                Transform damagableTransform = damagables[i].GetTransform();  // Get the transform of the damagable object
                float distance = Vector3.Distance(transform.position, damagableTransform.position);  // Calculate the distance between the attack radius and the damagable object

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDamagable = damagables[i];
                }
            }

            if (closestDamagable != null)
            {
                onAttack?.Invoke(closestDamagable);  // Invoke the onAttack event and pass the closest damagable object
                closestDamagable.TakeDamage(damage);  // Inflict damage on the closest damagable object
            }

            closestDamagable = null;
            closestDistance = float.MaxValue;

            yield return wait;
          
            
            damagables.RemoveAll(DisabledDamagables);  // Remove disabled damagable objects from the list
        }


        AttackRoutine = null;
    }

    protected bool DisabledDamagables(IDamagable damagable)
    {
        return damagable != null && !damagable.GetTransform().gameObject.activeSelf;  // Check if the damagable object is disabled
    }

    void StopAttack()
    {
        canAttack = false;
    }

}
