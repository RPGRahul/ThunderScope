using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private GameObject mainMenuCanvas;
    [SerializeField]
    private GameObject inGameCanvas;

    private void Awake()
    {
        instance = this;
    }

    public void ShowMainMenuCanvas(bool show)
    {
        mainMenuCanvas.SetActive(show);
    }

    public void ShowInGameCanvas(bool show)
    {
        inGameCanvas.SetActive(show);
    }
}
