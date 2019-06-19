using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    public class SceneLoader : MonoBehaviour
    {

        TemporaryGameManager temporaryGameManager;
        private void Start()
        {
            temporaryGameManager = FindObjectOfType<TemporaryGameManager>();
        }
        // Start is called before the first frame update
        public void LoadScene(string _sceneName)
        {
            temporaryGameManager.LoadScene(_sceneName);
        }
    }
}