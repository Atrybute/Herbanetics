using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class SkillManager : Singleton<SkillManager> 
{
    private const string GameLevelPrefix = "Level";
    [SerializeField, Scene] private int mainMenuScene;

    #region Skill Variables
    [Header("Projectile Skill")]
    public int projectileSkillLevel = 0;

    [Header("Punch Skill")]
    public int punchSkillLevel = 0;

    [Header("Wave Skill")]
    public int waveSkillLevel = 0;

    [Header("Dash Skill")]
    public int dashLevel = 0;
    #endregion

    #region Unity Callbacks
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
   
    #endregion

    #region Level Up Functions
    public void LevelUpProjectile()
    {
        PlayerPrefs.SetInt("ProjectileLvl", projectileSkillLevel + 1); 

    }

    public  void LevelUpWave()
    {
        PlayerPrefs.SetInt("WaveSkill", waveSkillLevel + 1);
    }

    public void  LevelUpPunch()
    {
        PlayerPrefs.SetInt("PunchSkill", punchSkillLevel + 1);
    }

    public void LevelUpDash()
    {
        PlayerPrefs.SetInt("DashSkill", dashLevel + 1);
    }
    #endregion

    public void BackHome()
    {
        PlayerPrefs.SetInt("ProjectileLvl", 0);
        PlayerPrefs.SetInt("WaveSkill", 0);
        PlayerPrefs.SetInt("PunchSkill", 0);
        PlayerPrefs.SetInt("DashSkill", 0);
        SceneManager.LoadScene(mainMenuScene);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsScenePrefix(GameLevelPrefix))
        {
            int projectileLvl = PlayerPrefs.GetInt("ProjectileLvl");
            projectileSkillLevel = Mathf.RoundToInt(projectileLvl);

            int punchSkill = PlayerPrefs.GetInt("PunchSkill");
            punchSkillLevel = Mathf.RoundToInt(punchSkill);

            int waveSkill = PlayerPrefs.GetInt("WaveSkill");
            waveSkillLevel = Mathf.RoundToInt(waveSkill);

            int dashSkill = PlayerPrefs.GetInt("DashSkill");
            dashLevel = Mathf.RoundToInt(dashSkill);
        }
    }
    private bool IsScenePrefix(string prefix)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return currentSceneName.StartsWith(prefix);
    }
}
