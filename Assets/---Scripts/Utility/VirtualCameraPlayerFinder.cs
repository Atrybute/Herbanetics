using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirtualCameraPlayerFinder : Singleton<VirtualCameraPlayerFinder>
{
    private const string PlayerStr = "Player";
    private GameObject player;
    private CinemachineVirtualCamera virtualCamera;
    private const string GameLevelPrefix = "Level";

    #region Unity Callbacks
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();    
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsScenePrefix(GameLevelPrefix))
        {
            player = GameObject.FindGameObjectWithTag(PlayerStr);
            virtualCamera.Follow = player.transform;
        }
        else
        {
            Debug.Log($"No player on this scene, virtual cam has no reference");
        }
    }
    private bool IsScenePrefix(string prefix)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return currentSceneName.StartsWith(prefix);
    }
}
