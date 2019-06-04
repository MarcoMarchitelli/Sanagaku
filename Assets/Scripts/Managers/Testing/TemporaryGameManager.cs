using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Collections;

namespace Sangaku
{
    public class TemporaryGameManager : MonoBehaviour
    {
        public static TemporaryGameManager Singleton { get; private set; }

        public GameObject MainMenu;
        public GameObject PauseMenu;
        public GameObject WinMenu;
        public GameObject LossMenu;
        public GameObject LoadingPanel;

        [Header("Controllers")]
        public TemporaryEnemyManager enemyManager;
        public PlayerController playerController;

        bool canPause = false;

        private void Awake()
        {
            if (Singleton == null)
            {
                Singleton = this;
                DontDestroyOnLoad(Singleton.gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        public void Start()
        {
            Time.timeScale = 0;

            SetupEntities();

            playerController.SetUpEntity();

            GoToMainMenu();
        }

        void SetupEntities()
        {
            foreach (ObjectiveController objective in FindObjectsOfType<ObjectiveController>())
                objective.SetUpEntity();

            foreach (RoomController room in FindObjectsOfType<RoomController>())
                room.SetUpEntity();

            foreach (GenericController generic in FindObjectsOfType<GenericController>())
                generic.SetUpEntity();

            enemyManager.SetUpEnemies();
        }

        private void Update()
        {
            if (canPause && Input.GetButton("Pause"))
                GoToPauseMenu();
        }

        public void GoToMainMenu()
        {
            playerController.Enable(false);
            Time.timeScale = 0;
            PauseMenu.SetActive(false);
            WinMenu.SetActive(false);
            LossMenu.SetActive(false);
            MainMenu.SetActive(true);
            LoadingPanel.SetActive(false);
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
            LoadingPanel.SetActive(false);
            canPause = false;
        }

        public void GoToGameplay()
        {
            WinMenu.SetActive(false);
            MainMenu.SetActive(false);
            PauseMenu.SetActive(false);
            LossMenu.SetActive(false);
            LoadingPanel.SetActive(false);
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
            LoadingPanel.SetActive(false);
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
            LoadingPanel.SetActive(false);
            LossMenu.SetActive(true);
            canPause = false;
        }

        public void GoToLoadingPanel()
        {
            playerController.Enable(false);
            MainMenu.SetActive(false);
            PauseMenu.SetActive(false);
            WinMenu.SetActive(false);
            LossMenu.SetActive(false);
            LoadingPanel.SetActive(true);
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

        public void LoadScene(string _sceneName)
        {
            StartCoroutine(LoadAsyncScene(_sceneName));
        }

        IEnumerator LoadAsyncScene(string _sceneName)
        {
            playerController.GetBehaviour<PlayerInputBehaviour>().ToggleAllInputs(false);

            Scene newLevel = SceneManager.GetSceneByName(_sceneName);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);

            WaitForEndOfFrame wfef = new WaitForEndOfFrame();

            GoToLoadingPanel();

            foreach (CinemachineVirtualCamera cam in FindObjectsOfType<CinemachineVirtualCamera>())
                cam.gameObject.SetActive(false);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
                yield return wfef;

            SetupEntities();

            Transform spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
            playerController.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

            GameObject vcamObj = GameObject.FindGameObjectWithTag("StartingVCam");
            vcamObj.SetActive(true);
            CinemachineVirtualCamera vcam = vcamObj.GetComponent<CinemachineVirtualCamera>();
            vcam.Follow = playerController.transform;

            GoToGameplay();

            playerController.GetBehaviour<PlayerInputBehaviour>().ToggleAllInputs(true);
        }
    }
}