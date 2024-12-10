using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    [SerializeField][Range(0.01f, 1f)] private float smoothness;
    [SerializeField] Vector3 offset;
    private const string playerStr = "Player";
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag(playerStr).transform;
    }
    private void LateUpdate()
    {
        if (target != null)
        {

            Vector3 desiredPosition = target.position + offset;
            Vector3 newPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness);
            transform.position = newPosition;
        }
    }
}
