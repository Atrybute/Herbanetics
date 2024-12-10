using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class ForceReciever : MonoBehaviour
{
    #region External References
    [Header("External References")]
    [SerializeField] private CharacterController controller;
    #endregion

    #region Force Reciever Variables
    [field: Header("Force Receiever Variables")]
    [field: SerializeField] public float VerticalVelocity { get; set; }
    private Vector3 impact;
    private Vector3 dampingVelocity;

    [SerializeField, Range(0f, 1f)] float drag;
    #endregion
    public Vector3 Movement => impact + Vector3.up * VerticalVelocity;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
      
    }
    private void Update()
    {
        if (controller != null)
        {
            if (VerticalVelocity < 0f && controller.isGrounded)
            {
                VerticalVelocity = Physics.gravity.y * Time.deltaTime;
            }
            else
            {
                VerticalVelocity += Physics.gravity.y * Time.deltaTime;
            }
        }
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

    }
    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
