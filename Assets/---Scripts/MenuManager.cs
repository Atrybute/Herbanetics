using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    #region Viewing Variables
    [BoxGroup("Debugging")]
    public bool showGameMenuInterface;
    #endregion

    #region Menu Variables
    [SerializeField, Scene] private int mainMenuScene;
    private const string GameLevelPrefix = "Level";

    [BoxGroup("Menu CONFIG")]
    [ShowIf("showGameMenuInterface")]
    [SerializeField] GameObject mainMenuUICanvas;

    [BoxGroup("Menu CONFIG")]
    [ShowIf("showGameMenuInterface")]
    [SerializeField] GameObject mainGameUICanvas;

    [BoxGroup("Menu CONFIG")]
    [ShowIf("showGameMenuInterface")]
    [SerializeField] GameObject levelUpUI;

    [BoxGroup("Menu CONFIG")]
    [ShowIf("showGameMenuInterface")]
    [SerializeField] GameObject playerHudUI;


    [BoxGroup("Menu CONFIG")]
    [ShowIf("showGameMenuInterface")]
    [SerializeField] GameObject gameOverUI;

    [BoxGroup("Menu CONFIG")]
    [ShowIf("showGameMenuInterface")]
    [SerializeField] GameObject startGameUI;

    #endregion

    #region Unity Callbacks

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ActionAndEventsManager.Instance.enemyWaveActions.OnWavesCompleted += HandleLevelCompleteUI;
        ActionAndEventsManager.Instance.bossEnemyActions.OnBossDefeat += HandleLevelCompleteUI;
        ActionAndEventsManager.Instance.playerHealthActions.OnHealthDepleted += HandleGameOverUI;
        ActionAndEventsManager.Instance.gameUIActions.OnGameRestartUI += HandleRestartGameUI;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        ActionAndEventsManager.Instance.enemyWaveActions.OnWavesCompleted -=HandleLevelCompleteUI;
        ActionAndEventsManager.Instance.bossEnemyActions.OnBossDefeat -= HandleLevelCompleteUI;
        ActionAndEventsManager.Instance.playerHealthActions.OnHealthDepleted -= HandleGameOverUI;
        ActionAndEventsManager.Instance.gameUIActions.OnGameRestartUI -= HandleRestartGameUI;
    }
    #endregion

    #region UI Handling Functions
    private void HandleLevelCompleteUI()
    {  
        playerHudUI.SetActive(false);
        levelUpUI.SetActive(true);
    }

    private void HandleGameOverUI()
    {
        playerHudUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    private void HandleRestartGameUI()
    {
        gameOverUI.SetActive(false);
        startGameUI.SetActive(true);

    }
    #endregion

    #region Utility Functions
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(mainMenuScene))
        {
            mainMenuUICanvas.SetActive(true);
        }
        else
        {
            mainMenuUICanvas.SetActive(false);
        }

        if (IsScenePrefix(GameLevelPrefix))
        {
            mainGameUICanvas.SetActive(true);
            playerHudUI.SetActive(true);
        }
        else
        {   
             playerHudUI.SetActive(true);
            mainGameUICanvas.SetActive(false);
        }
    }

    private bool IsScenePrefix(string prefix)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return currentSceneName.StartsWith(prefix);
    }
    #endregion
}
