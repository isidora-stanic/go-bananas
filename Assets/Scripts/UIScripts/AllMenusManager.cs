using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public enum CanvasType
{
    MainMenu,
    SettingsMenu,
    PauseMenu,
    GameUI,
    WinScreen,
    LoseScreen,
}

public class AllMenusManager : Singleton<AllMenusManager>
{
    [SerializeField] private GameObject pauseGamePanel;
    [SerializeField] private bool isLevel = false;
    private List<CanvasController> canvasControllerList;
    private CanvasController lastActiveController;

    protected override void Awake()
    {
        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        if (isLevel) SwitchCanvas(CanvasType.GameUI);
        else SwitchCanvas(CanvasType.MainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
            PauseGame();
    }

    public void SwitchCanvas(CanvasType _type)
    {
        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));
        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveController = desiredCanvas;
        } else
        {
            Debug.Log("Desired canvas not found!");
        }

        if (_type != CanvasType.GameUI)
        {
            ShowCursor();
        } 
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ShowCursor()
    {
        if (!Cursor.visible || Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void LoadSettingsMenu()
    {
        SwitchCanvas(CanvasType.SettingsMenu);
    }

    public void LoadMainMenu()
    {
        TransitionScene(0);
    }

    public void LoadNextLevel()
    {
        TransitionScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void LoadMainMenuSwitch()
    {
        SwitchCanvas(CanvasType.MainMenu);
    }

    public void LoadGameUI()
    {
        SwitchCanvas(CanvasType.GameUI);
    }

    public void LoadWinScreen()
    {
        SwitchCanvas(CanvasType.WinScreen);
        // Time.timeScale = 0f;
    }

    public void LoadLoseScreen()
    {
        SwitchCanvas(CanvasType.LoseScreen);
        // Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        SwitchCanvas(CanvasType.PauseMenu);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        SwitchCanvas(CanvasType.GameUI);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        // #if UNITY_EDITOR
        // UnityEditor.EditorApplication.isPlaying = false;
        // #endif
        Application.Quit();
    }

    public void TransitionScene(int levelIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelIndex);
    }
}
