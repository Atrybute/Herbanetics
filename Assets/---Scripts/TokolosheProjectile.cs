using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokolosheProjectile : MonoBehaviour
{
    [SerializeField] float spreadAngle;
    [SerializeField] float spreadSpeed;
    [SerializeField] int projectileDamage;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] int numberOfProjectiles;

    private const string bossStr = "Boss";

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(bossStr)){

            SpreadProjectiles();


            Destroy(gameObject);
        }  
    }


    void SpreadProjectiles()
    {
        // Calculate the angle between each projectile
        float angleStep = spreadAngle / (numberOfProjectiles - 1);
        float startAngle = spreadAngle / 2f;

        // Instantiate projectiles in a spread pattern
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Calculate the rotation based on the angle
            Quaternion rotation = Quaternion.Euler(0f, startAngle + i * angleStep, 0f);

            // Instantiate the new projectile
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, rotation);
            Rigidbody newProjectileRb = newProjectile.GetComponent<Rigidbody>();

            // Apply force to the new projectile
            newProjectileRb.velocity = rotation * Vector3.forward * spreadSpeed;

            // Attach a script to the new projectile to handle damage
            TokolosheProjectileDamage projectileDamageScript = newProjectile.AddComponent<TokolosheProjectileDamage>();
            projectileDamageScript.damageAmount = projectileDamage;

            // Destroy the new projectile after a certain time (optional)
            Destroy(newProjectile, 5f);
        }
    }


}
