using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StartGameManager : Singleton<StartGameManager>
{
    private const string GameLevelPrefix = "Level";
    #region Unity Callbacks

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ActionAndEventsManager.Instance.playerHealthActions.OnHealthDepleted += StopGamePlay;
        ActionAndEventsManager.Instance.enemyWaveActions.OnWavesCompleted += StopGamePlay;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ActionAndEventsManager.Instance.playerHealthActions.OnHealthDepleted -= StopGamePlay;
        ActionAndEventsManager.Instance.enemyWaveActions.OnWavesCompleted -= StopGamePlay;
    }
    #endregion

    public void StartGame()
    {
        Time.timeScale = 1f;
        PauseNavMeshAgents(false);
    }

    public void StopGamePlay()
    {
        Time.timeScale = 0f;
        PauseNavMeshAgents(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ActionAndEventsManager.Instance.gameUIActions.GameRestartUIEvent();
    }

    public void EndGame()
    {
        Application.Quit();
    }
    private void PauseNavMeshAgents(bool pause)
    {
        NavMeshAgent[] navMeshAgents = FindObjectsOfType<NavMeshAgent>();

        foreach (NavMeshAgent agent in navMeshAgents)
        {
            if (agent.isActiveAndEnabled) // Check if the NavMeshAgent is active and enabled
            {
                agent.isStopped = pause;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsScenePrefix(GameLevelPrefix))
        {
            Time.timeScale = 0f;
        }
    }
    private bool IsScenePrefix(string prefix)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return currentSceneName.StartsWith(prefix);
    }

}
