using UnityEngine;
using UnityEngine.SceneManagement;
using Sangaku;
using System.Collections.Generic;

public class TemporaryGameManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject PauseMenu;
    public GameObject WinMenu;
    public GameObject LossMenu;

    [Header("Controllers")]
    public List<ObjectiveController> objectiveControllers;
    public TemporaryEnemyManager enemyManager;
    public PlayerController playerController;
    public TemporaryUIManager uiManager;

    bool canPause = false;

    public void Start()
    {
        Time.timeScale = 0;
        foreach (ObjectiveController objective in objectiveControllers)
        {
            objective.SetUpEntity();
        }
        enemyManager.SetUpEnemies();
        playerController.SetUpEntity();
        uiManager.SetupPlayerHUD();
        
        GoToMainMenu();
    }

    private void Update()
    {
        if (canPause && Input.GetKeyDown(KeyCode.Escape))
        {
            GoToPauseMenu();
        }
    }

    public void GoToMainMenu()
    {
        playerController.Enable(false);
        Time.timeScale = 0;
        PauseMenu.SetActive(false);
        WinMenu.SetActive(false);
        LossMenu.SetActive(false);
        MainMenu.SetActive(true);
        canPause = false;
    }

    public void GoToPauseMenu()
    {
        playerController.Enable(false);
        Time.timeScale = 0;
        WinMenu.SetActive(false);
        MainMenu.SetActive(false);
        LossMenu.SetActive(false);
        PauseMenu.SetActive(true);
        canPause = false;
    }

    public void GoToGameplay()
    {
        WinMenu.SetActive(false);
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        LossMenu.SetActive(false);
        Time.timeScale = 1;
        playerController.Enable(true);
        canPause = true;
    }

    public void GoToWinMenu()
    {
        playerController.Enable(false);
        Time.timeScale = 0;
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        LossMenu.SetActive(false);
        WinMenu.SetActive(true);
        canPause = false;
    }

    public void GoToLossMenu()
    {
        playerController.Enable(false);
        Time.timeScale = 0;
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        WinMenu.SetActive(false);
        LossMenu.SetActive(true);
        canPause = false;
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}