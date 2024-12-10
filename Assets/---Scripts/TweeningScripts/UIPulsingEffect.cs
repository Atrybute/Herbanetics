using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIPulsingEffect : MonoBehaviour
{
    public GameObject targetUIObject;     // Reference to the UI GameObject (Button, Panel, etc.)
    public float scaleUpAmount = 1.2f;    // Scale size when scaling up
    public float scaleDuration = 0.5f;    // Duration of the scaling animation

    private void Start()
    {
        // Start the pulsing effect on the UI object
        StartPulsingEffect();
    }

    private void StartPulsingEffect()
    {
        // Scale the UI GameObject up and down indefinitely
        targetUIObject.transform.DOScale(scaleUpAmount, scaleDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);  // Loop indefinitely, scaling up and down
    }
}
