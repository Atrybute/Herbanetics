using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] GameObject attackTrigger;

    public void ToggleTriggerOn()
    {
        attackTrigger.SetActive(true);
    }

    public void ToggleTriggerOff()
    {
        attackTrigger.SetActive(false);
    }
}
