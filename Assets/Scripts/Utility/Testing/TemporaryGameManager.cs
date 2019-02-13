using UnityEngine;
using UnityEngine.SceneManagement;
using Sangaku;

public class TemporaryGameManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject PauseMenu;
    public GameObject WinMenu;

    [Header("Controllers")]
    public TemporaryEnemyManager enemyManager;
    public ObjectiveController objectiveController;
    public PlayerController playerController;
    public TemporaryUIManager uiManager;

    bool canPause = false;

    public void Start()
    {
        Time.timeScale = 0;
        objectiveController.SetUpEntity();
        enemyManager.SetUpEnemies();
        playerController.SetUpEntity();
        uiManager.SetupPlayerHUD();
        playerController.Enable(false);
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
        Time.timeScale = 0;
        PauseMenu.SetActive(false);
        WinMenu.SetActive(false);
        MainMenu.SetActive(true);
        canPause = false;
    }

    public void GoToPauseMenu()
    {
        Time.timeScale = 0;
        WinMenu.SetActive(false);
        MainMenu.SetActive(false);
        PauseMenu.SetActive(true);
        canPause = false;
    }

    public void GoToGameplay()
    {
        WinMenu.SetActive(false);
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        playerController.Enable(true);
        canPause = true;
    }

    public void GoToWinMenu()
    {
        Time.timeScale = 0;
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        WinMenu.SetActive(true);
        canPause = false;
    }

    public void GoToLossMenu()
    {
        Time.timeScale = 0;
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        WinMenu.SetActive(true);
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