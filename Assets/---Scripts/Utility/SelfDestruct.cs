using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    private float destructTime;

    void Update()
    {
        Destroy(obj: gameObject, destructTime);
    }
}
