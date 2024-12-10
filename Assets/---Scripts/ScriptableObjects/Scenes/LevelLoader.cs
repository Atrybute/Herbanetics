using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelLoader", menuName = "Custom/LevelLoader")]
public class LevelLoader : ScriptableObject
{
    public LevelData[] baseLevels; // Levels that are not boss levels
    public string[] bossScenes;    // Boss scenes

    private int currentLevelIndex = -1;
    private int[] levelIndices;
    public int bossAppearcanceIndex = 3; // Ensure this is > 0

    public void LoadNextLevel()
    {
        currentLevelIndex++;

        if (baseLevels.Length == 0)
        {
            Debug.LogError("No base levels available to load.");
            return;
        }

        if (currentLevelIndex % bossAppearcanceIndex == (bossAppearcanceIndex - 1))
        {
            if (bossScenes.Length == 0)
            {
                Debug.LogError("No boss scenes available.");
                return;
            }

            // Load a random boss scene
            int randomBossIndex = Random.Range(0, bossScenes.Length);
            TryLoadScene(bossScenes[randomBossIndex]);
        }
        else
        {
            // Ensure the level index is within bounds
            int levelIndex = levelIndices[currentLevelIndex % levelIndices.Length];
            TryLoadScene(baseLevels[levelIndex].sceneName);
        }
    }

    public void LevelCompleted()
    {
        LoadNextLevel();
    }

    public void StartGame()
    {
        if (baseLevels.Length == 0)
        {
            Debug.LogError("Cannot start game. No base levels defined.");
            return;
        }

        GenerateRandomLevelIndices();
        LoadNextLevel();
    }

    private void GenerateRandomLevelIndices()
    {
        levelIndices = new int[baseLevels.Length];
        for (int i = 0; i < baseLevels.Length; i++)
        {
            levelIndices[i] = i;
        }

        // Fisher-Yates shuffle
        for (int i = 0; i < baseLevels.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, baseLevels.Length);
            (levelIndices[randomIndex], levelIndices[i]) = (levelIndices[i], levelIndices[randomIndex]);
        }

        currentLevelIndex = -1; // Reset the current level index
    }

    private void TryLoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings.");
        }
    }
}

