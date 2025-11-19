using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private GameManager gameManager;

    [SerializeField]
    private GameObject mainMenuCanvas;
    [SerializeField]
    private GameObject selectDifficultyCanvas;
    [SerializeField]
    private GameObject inGameCanvas;
    [SerializeField]
    private GameObject gameEndCanvas;

    [SerializeField]
    private TMP_Text mainMenuHighScoreValueText;

    [SerializeField]
    private TMP_Text inGameScoreValueText;

    [SerializeField]
    private TMP_Text gameEndScoreValueText;
    [SerializeField]
    private TMP_Text gameEndHighScoreValueText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void ShowMainMenuCanvas(bool show)
    {
        mainMenuCanvas.SetActive(show);
    }

    public void ShowSelectDifficultyCanvas(bool show)
    {
        selectDifficultyCanvas.SetActive(show);
    }

    public void ShowInGameCanvas(bool show)
    {
        inGameCanvas.SetActive(show);
    }

    public void ShowGameEndCanvas(bool show)
    {
        gameEndCanvas.SetActive(show);
    }

    // Main Menu Canvas

    public void MainMenuStartButtonPressed()
    {
        gameManager.RequestStartGame();
    }

    public void UpdateMainMenuHighScoreValueText(string text)
    {
        mainMenuHighScoreValueText.text = text;
    }

    // Select Difficulty Canvas

    public void SelectDifficultyEasyButtonPressed()
    {
        gameManager.RequestEasyDifficulty();
    }

    public void SelectDifficultyMediumButtonPressed()
    {
        gameManager.RequestMediumDifficulty();
    }

    public void SelectDifficultyHardButtonPressed()
    {
        gameManager.RequestHardDifficulty();
    }

    // In Game Canvas

    public void UpdateInGameScoreValueText(string text)
    {
        inGameScoreValueText.text = text;
    }

    // Game End Canvas

    public void GameEndReturnButtonPressed()
    {
        gameManager.RequestReturnFromEnd();
    }

    public void UpdateGameEndScoreValueText(string text)
    {
        gameEndScoreValueText.text = text;
    }

    public void UpdateGameEndHighScoreValueText(string text)
    {
        gameEndHighScoreValueText.text = text;
    }
}
