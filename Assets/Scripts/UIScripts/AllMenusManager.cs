using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public enum CanvasType
{
    MainMenu,
    SettingsMenu,
    GameUI,
    WinScreen,
    LoseScreen,
}

public class AllMenusManager : Singleton<AllMenusManager>
{
    
    private List<CanvasController> canvasControllerList;
    private CanvasController lastActiveController;
    protected override void Awake()
    {
        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));
        SwitchCanvas(CanvasType.MainMenu);
    }

    public void SwitchCanvas(CanvasType _type)
    {
        if (lastActiveController != null)
        {
            lastActiveController.gameObject.SetActive(false);
            Debug.Log("Turned off " + lastActiveController.canvasType);
        }
        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveController = desiredCanvas;
        } else
        {
            Debug.Log("Desired canvas not found!");
        }
    }

    public void LoadSettingsMenu()
    {
        SwitchCanvas(CanvasType.SettingsMenu);
    }

    public void LoadMainMenu()
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
    }

    public void LoadLoseScreen()
    {
        SwitchCanvas(CanvasType.LoseScreen);
    }

    public void PauseGame()
    {
        Debug.Log("PAUSED!");
        Time.timeScale = 0.0f;
        // pauseGamePanel.SetActive(true);
        // pauseButton.SetActive(false);
    }

    public void Resume()
    {
        Debug.Log("RESUMED!");
        Time.timeScale = 1.0f;
        // pauseGamePanel.SetActive(false);
        // pauseButton.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void TransitionScene(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
