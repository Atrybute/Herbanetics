using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    [Header("Score Elements")]
    [SerializeField] TMP_Text scoreText;
    private const string ScoreVar = "Score";
    public int ScoreCount { get; set; }

    private void OnEnable()
    {
        ActionAndEventsManager.Instance.scoreActions.OnAddScore += AddScore;
       
       
    }

    private void OnDisable()
    {
        ActionAndEventsManager.Instance.scoreActions.OnAddScore -= AddScore;
       
      
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(ScoreVar))
        {
            ScoreCount = 0;
        }
        else
        {
            LoadScore();

        }
    }

    void AddScore(int score)
    {   
        ScoreCount+=score;
        scoreText.text = ScoreCount.ToString();
    }

    

    public void SaveScore()
    {
        PlayerPrefs.SetInt(ScoreVar, ScoreCount);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        int currentScore = PlayerPrefs.GetInt(ScoreVar, 0);
        scoreText.text = currentScore.ToString();
        ScoreCount = currentScore;
    }

    public void RestScore()
    {
        PlayerPrefs.SetInt (ScoreVar, 0);
    }

   

}
