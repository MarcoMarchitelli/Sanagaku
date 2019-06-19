using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

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

        public AudioListener menuAudioListener;

        bool canPause = false;

        ObjectPooler pooler;

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

            pooler = FindObjectOfType<ObjectPooler>();

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

            foreach (RobotController generic in FindObjectsOfType<RobotController>())
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

            playerController.GetAudioListener().enabled = false;
            menuAudioListener.enabled = true;

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

            playerController.GetAudioListener().enabled = false;
            menuAudioListener.enabled = true;

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

            menuAudioListener.enabled = false;
            playerController.GetAudioListener().enabled = true;

            playerController.Enable(true);

            canPause = true;
        }

        public void GoToWinMenu()
        {
            playerController.Enable(false);

            playerController.GetAudioListener().enabled = false;
            menuAudioListener.enabled = true;

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

            playerController.GetAudioListener().enabled = false;
            menuAudioListener.enabled = true;

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
            SceneManager.sceneLoaded += HandleReloadSceneDone;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void HandleReloadSceneDone(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= HandleReloadSceneDone;

            Time.timeScale = 0;

            SetupEntities();

            playerController = FindObjectOfType<PlayerController>();
            playerController.SetUpEntity();

            GoToMainMenu();
        }

        public void LoadScene(string _sceneName)
        {
            StartCoroutine(LoadAsyncScene(_sceneName));
        }

        IEnumerator LoadAsyncScene(string _sceneName)
        {
            List<GameObject> objectsToMove = new List<GameObject>();
            objectsToMove.Add(playerController.gameObject);
            objectsToMove.Add(Camera.main.gameObject);
            objectsToMove.Add(pooler.gameObject);
            objectsToMove.Add(FindObjectOfType<RobotController>().gameObject);

            playerController.GetBehaviour<PlayerInputBehaviour>().ToggleAllInputs(false);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
            WaitForEndOfFrame wfef = new WaitForEndOfFrame();

            GoToLoadingPanel();

            foreach (CinemachineVirtualCamera cam in FindObjectsOfType<CinemachineVirtualCamera>())
                cam.gameObject.SetActive(false);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
                yield return wfef;

            Scene sceneToLoad = SceneManager.GetSceneByName(_sceneName);
            for (int i = 0; i < objectsToMove.Count; i++)
                SceneManager.MoveGameObjectToScene(objectsToMove[i], sceneToLoad);

            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            // Wait until the asynchronous scene fully unloads
            while (!asyncUnload.isDone)
                yield return wfef;

            SetupEntities();

            Transform spawnPoint = null;
            if (GameObject.FindGameObjectWithTag("PlayerSpawn") != null)
                spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
            else
                Debug.LogError("**** Player Spawn non trovato ****");

            if (!playerController)
                playerController = FindObjectOfType<PlayerController>();

            playerController.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            Destroy(spawnPoint.gameObject);

            GameObject vcamObj = GameObject.FindGameObjectWithTag("StartingVCam");
            vcamObj.SetActive(true);
            CinemachineVirtualCamera vcam = vcamObj.GetComponent<CinemachineVirtualCamera>();
            vcam.Follow = playerController.transform;

            GoToGameplay();

            playerController.GetBehaviour<PlayerInputBehaviour>().ToggleAllInputs(true);
        }
    }
}