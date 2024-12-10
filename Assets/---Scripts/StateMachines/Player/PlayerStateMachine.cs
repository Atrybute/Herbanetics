using NaughtyAttributes;
using UnityEngine;


public class PlayerStateMachine : StateMachine
{
    #region External Script References
    [field: Header("External Script References")]
    [field: SerializeField] public PlayerActionsInputManager PlayerInputManager { get; private set; }
    [field: SerializeField] public ForceReciever ForceReciever { get; private set; }

    [field: SerializeField] public CombatManager CombatManager { get; private set; }

    #endregion

    #region Unity Component References

    [field: HorizontalLine(color: EColor.Indigo)]
    [field: Space(2f)]
    [field: Header("Unity Components")]
    [field: SerializeField] public Animator PlayerAnimator { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    public Transform MainCamTransform { get; private set; }
    #endregion

    #region Scriptable Object References
    [field: HorizontalLine(color: EColor.Indigo)]
    [field: Space(2f)]
    [field: Header("Scriptable Object References")]

    [field: SerializeField] PlayerConfig playerConfig;
    [field: SerializeField] public MeleeAttackConfig [] MeleeAttackConfig { get; private set; }
    #endregion

    #region Player Config Variables
    [field: HorizontalLine(color: EColor.Indigo)]
    [field: Space(2f)]
    [field: Header("Player Config Variables")]

    [field: SerializeField] public bool ShowPlayerConfigStats { get; private set; }
    [field: ShowIf("ShowPlayerConfigStats")]
    [Tooltip("This Variable dicatates how quickly the player rotates to their desired position")]
    [field: SerializeField] public float PlayerRotationValue { get; set; }
    [field: ShowIf("ShowPlayerConfigStats")]
    [field: SerializeField] public float DashSpeed { get; private set; }
    [field: ShowIf("ShowPlayerConfigStats")]
    [field: SerializeField] public float AttackMovementSpeed { get; private set; }
    [field: ShowIf("ShowPlayerConfigStats")]
    [field: SerializeField] public float RotationSmoothValue { get; private set; }

    [field: ShowIf("ShowPlayerConfigStats")]
    [field: SerializeField] public float MovementSpeed { get; private set; }


    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        Initializer();
    }

    private void Start()
    {
        MainCamTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        ActionAndEventsManager.Instance.enemyWaveActions.OnWavesCompleted += SwitchToIdle;
        ActionAndEventsManager.Instance.playerHealthActions.OnHealthDepleted += SwitchToDeath;
    }

    private void OnDisable()
    {
        ActionAndEventsManager.Instance.enemyWaveActions.OnWavesCompleted -= SwitchToIdle;
        ActionAndEventsManager.Instance.playerHealthActions.OnHealthDepleted -= SwitchToDeath;
    }

    #endregion

    private void Initializer()
    {
        MovementSpeed = playerConfig.movementSpeed;
        DashSpeed = playerConfig.dashSpeed; 
        PlayerRotationValue = playerConfig.playerRotationValue;
        RotationSmoothValue = playerConfig.rotationSmoothValue;
        SwitchState(new PlayerFreelookState(this));
        CombatManager = GetComponent<CombatManager>();
    }

    private void SwitchToIdle()
    {
        SwitchState(new PlayerIdleState(this));
    }

    private void SwitchToDeath()
    {
        SwitchState(new PlayerDeathState(this));
    }
}
