using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : Singleton<HUDManager>
{
    #region Viewing Variables
    [BoxGroup("Debugging")]
    public bool showGameHUDInterface, showPlayerHUDInterface;
    #endregion

    #region Player HUD
    [BoxGroup("Player HUD CONFIG")]
    [ShowIf("showPlayerHUDInterface")]
    [SerializeField] Image playerHealthBar;

    [BoxGroup("Player HUD CONFIG")]
    [ShowIf("showPlayerHUDInterface")]
    [SerializeField] GameObject playergGameplayHUDParent;
    #endregion

    #region Game HUD
    [BoxGroup("Game HUD CONFIG")]
    [ShowIf("showGameHUDInterface")]
    [SerializeField] TextMeshProUGUI waveText;
    #endregion

    #region Unity Callbacks

    

    private void OnEnable()
    {
        ActionAndEventsManager.Instance.playerHealthActions.OnHealthFillStart += FillPlayerHealth;
        ActionAndEventsManager.Instance.playerDamagActions.OnPlayerHit += UpdatePlayerHealthBar;
        ActionAndEventsManager.Instance.enemyWaveActions.OnWaveTextStart += InitializeWaveText;
        ActionAndEventsManager.Instance.enemyWaveActions.OnWaveTextUpdate += UpdateWaveText;
        ActionAndEventsManager.Instance.enemyWaveActions.OnWavesCompleted += TurnOffWaveText;
        
    }

    private void OnDisable()
    {
        ActionAndEventsManager.Instance.playerHealthActions.OnHealthFillStart -= FillPlayerHealth;
        ActionAndEventsManager.Instance.playerDamagActions.OnPlayerHit -= UpdatePlayerHealthBar;
        ActionAndEventsManager.Instance.enemyWaveActions.OnWaveTextStart -= InitializeWaveText;
        ActionAndEventsManager.Instance.enemyWaveActions.OnWaveTextUpdate -= UpdateWaveText;
        ActionAndEventsManager.Instance.enemyWaveActions.OnWavesCompleted -= TurnOffWaveText;
    }
    #endregion

    #region Health Event Functions
    private void FillPlayerHealth(int playerHealth)
    {
        playerHealthBar.fillAmount = playerHealth;
    }

    private void UpdatePlayerHealthBar(float playerHealth)
    {
        playerHealthBar.fillAmount += playerHealth;
    }
    #endregion

    #region Wave Event Functions
    private void InitializeWaveText(int currentWave, int totalWaves)
    {   
        waveText.gameObject.SetActive(true);
        waveText.text = $"WAVE {currentWave} / {totalWaves}";
    }

    private void UpdateWaveText(int currentWave, int totalWaves)
    {
        waveText.text = $"WAVE {currentWave} / {totalWaves}";
    }

    private void TurnOffWaveText()
    {
        waveText.gameObject.SetActive(false);
    }
    #endregion

  
}
