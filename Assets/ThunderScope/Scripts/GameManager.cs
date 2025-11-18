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

        uiManager.ShowInGameCanvas(false);
        uiManager.ShowMainMenuCanvas(true);
    }
}
