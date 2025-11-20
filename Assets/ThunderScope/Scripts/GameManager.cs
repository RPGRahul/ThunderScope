using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager uiManager;
    private AudioManager audioManager;
    private CardGrid cardGrid;

    private enum Difficulty
    {
        easy,
        medium,
        hard
    }

    [SerializeField]
    private bool deleteHighScore;

    [SerializeField]
    private Camera mainCamera;

    private bool isInGame;
    private int currentScore, totalPairs, pairsFound, currentCombo;
    private Card firstCard, secondCard;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (deleteHighScore)
            PlayerPrefs.DeleteAll();

        uiManager = UIManager.instance;
        audioManager = AudioManager.instance;
        cardGrid = CardGrid.instance;

        SetupMainMenu();
    }

    private void Update()
    {
        if (isInGame)
        {
            // For Desktop
            if (Input.GetMouseButtonDown(0))
            {
                CheckClick(Input.mousePosition);
            }

            // For Mobile
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                CheckClick(Input.GetTouch(0).position);
            }
        }
    }

    private void CheckClick(Vector2 screenPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Only cards have colliders, so there will be always be a Card object
            Card tappedCard = hit.collider.GetComponentInParent<Card>();
            Card selectedCard = tappedCard.Tapped();

            // If card was selected, it will return itself
            if (selectedCard != null)
            {
                audioManager.PlaySFXOneShot(0);
                if (firstCard == null)
                {
                    firstCard = selectedCard;
                }
                else
                {
                    secondCard = selectedCard;

                    // Matching check
                    if (firstCard.id == secondCard.id)
                    {
                        // Match success
                        currentScore += 5;
                        ++pairsFound;
                        ++currentCombo;
                        if (currentCombo > 1)
                        {
                            currentScore += currentCombo - 1;
                            uiManager.UpdateInGameComboText(string.Format("X{0} Combo!", currentCombo));
                        }
                        uiManager.UpdateInGameScoreValueText(currentScore.ToString());
                        audioManager.PlaySFXOneShot(1);

                        firstCard.StartDeactivationSequence();
                        secondCard.StartDeactivationSequence();

                        // Check game end condition
                        if (pairsFound == totalPairs)
                        {
                            Invoke(nameof(SetupGameEnd), 1f);
                        }
                    }
                    else
                    {
                        // Match fail
                        currentCombo = 0;
                        uiManager.UpdateInGameComboText(string.Empty);
                        audioManager.PlaySFXOneShot(2);

                        firstCard.StartUnselectSequence();
                        secondCard.StartUnselectSequence();
                    }

                    firstCard = null;
                    secondCard = null;
                }
            }
        }
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

    private void SetupGameSession(Difficulty difficulty)
    {
        currentScore = 0;
        currentCombo = 0;
        pairsFound = 0;

        uiManager.ShowSelectDifficultyCanvas(false);
        uiManager.ShowInGameCanvas(true);

        uiManager.UpdateInGameScoreValueText("0");
        uiManager.UpdateInGameComboText(string.Empty);

        switch (difficulty)
        {
            case Difficulty.easy:
                cardGrid.BuildCards(3, 4);
                totalPairs = 6;
                break;

            case Difficulty.medium:
                cardGrid.BuildCards(3, 6);
                totalPairs = 9;
                break;

            case Difficulty.hard:
                cardGrid.BuildCards(4, 6);
                totalPairs = 12;
                break;
        }

        isInGame = true;
    }

    private void SetupGameEnd()
    {
        isInGame = false;

        audioManager.PlaySFXOneShot(3);
        uiManager.UpdateGameEndScoreValueText(currentScore.ToString());
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int highScore = PlayerPrefs.GetInt("HighScore");
            if (currentScore > highScore)
            {
                uiManager.UpdateGameEndHighScoreValueText(currentScore.ToString());
                PlayerPrefs.SetInt("HighScore", currentScore);
            }
            else
            {
                uiManager.UpdateGameEndHighScoreValueText(highScore.ToString());
            }
        }
        else
        {
            uiManager.UpdateGameEndHighScoreValueText(currentScore.ToString());
            PlayerPrefs.SetInt("HighScore", currentScore);
        }

        uiManager.ShowInGameCanvas(false);
        uiManager.ShowGameEndCanvas(true);
    }

    // Public Methods, to be called by other classes

    public void RequestStartGame()
    {
        uiManager.ShowMainMenuCanvas(false);
        uiManager.ShowSelectDifficultyCanvas(true);
    }

    public void RequestEasyDifficulty()
    {
        SetupGameSession(Difficulty.easy);
    }

    public void RequestMediumDifficulty()
    {
        SetupGameSession(Difficulty.medium);
    }

    public void RequestHardDifficulty()
    {
        SetupGameSession(Difficulty.hard);
    }

    public void RequestReturnFromEnd()
    {
        SetupMainMenu();
    }
}
