using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager uiManager;
    private AudioManager audioManager;

    [SerializeField]
    private Camera mainCamera;

    private bool isInGame;
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
            tappedCard.Tapped();
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
