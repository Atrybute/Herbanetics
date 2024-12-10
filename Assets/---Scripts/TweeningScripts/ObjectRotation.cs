using DG.Tweening;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationDuration = 2f;  // Duration for one full 360-degree rotation
    public Vector3 rotationAxis = Vector3.up;  // Axis of rotation (e.g., Vector3.up for Y-axis)

    private Tween rotationTween;  // Reference to the tween for controlling it later

   

    private void OnEnable()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        // Rotate the object 360 degrees around the given axis
        rotationTween = transform.DORotate(rotationAxis * 360, rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);  // Infinite loop to keep rotating
    }

    private void OnDisable()
    {
        // Stop the rotation when the object is disabled
        if (rotationTween != null && rotationTween.IsActive())
        {
            rotationTween.Kill();  // Kill the tween to stop the rotation
        }
    }
}
