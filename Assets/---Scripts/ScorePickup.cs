using System;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    [SerializeField] GameObject pickupEffect;
    [SerializeField] int scoreIncremenet;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
            ActionAndEventsManager.Instance.scoreActions.AddScore(scoreIncremenet);
            Destroy(gameObject);
        }
    }
}
