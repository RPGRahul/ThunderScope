using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager uiManager;
    private AudioManager audioManager;

    private float currentScore;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        audioManager = AudioManager.instance;

        SetupMainMenu();
    }

    private void SetupMainMenu()
    {
        uiManager.ShowInGameCanvas(false);
        uiManager.ShowSelectDifficultyCanvas(false);
        uiManager.ShowGameEndCanvas(false);
        uiManager.ShowMainMenuCanvas(true);

        if (PlayerPrefs.HasKey("HighScore"))
        {
            uiManager.UpdateMainMenuHighScoreValueText(PlayerPrefs.GetInt("HighScore").ToString());
        }
        else
        {
            uiManager.UpdateMainMenuHighScoreValueText("0");
        }
    }

    public void RequestStartGame()
    {
        uiManager.ShowMainMenuCanvas(false);
        uiManager.ShowSelectDifficultyCanvas(true);
    }

    public void RequestEasyDifficulty()
    {

    }

    public void RequestMediumDifficulty()
    {

    }

    public void RequestHardDifficulty()
    {

    }
}
