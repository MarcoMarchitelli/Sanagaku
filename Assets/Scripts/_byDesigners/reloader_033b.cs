using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sangaku
{
    public class reloader_033b : MonoBehaviour
    {
        [SerializeField] string sceneToUnload;

        public void UnloadScene()
        {
            SceneManager.UnloadSceneAsync(sceneToUnload);
            //FindObjectOfType<TemporaryGameManager>().LoadScene(sceneToLoad);
            SceneManager.LoadScene(sceneToUnload);
        }
    }
}
