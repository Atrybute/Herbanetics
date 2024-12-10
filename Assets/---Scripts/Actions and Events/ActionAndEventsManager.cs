using UnityEngine;

[DefaultExecutionOrder(-102)]
public class ActionAndEventsManager : Singleton<ActionAndEventsManager>
{
    [Header("Events and Actions Config")]
    public PlayerInputActions playerInputActions;
    public PlayerDamageActions playerDamagActions;
    public PlayerHealthActions playerHealthActions;
    public EnemyWaveActions enemyWaveActions;
    public ScoreActions scoreActions;
    public GameUIActions gameUIActions;
    public BossEnemyActions bossEnemyActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerDamagActions = new PlayerDamageActions();
        playerHealthActions = new PlayerHealthActions();
        enemyWaveActions = new EnemyWaveActions();
        scoreActions = new ScoreActions();
        gameUIActions = new GameUIActions();
        bossEnemyActions = new BossEnemyActions();
    }
}
