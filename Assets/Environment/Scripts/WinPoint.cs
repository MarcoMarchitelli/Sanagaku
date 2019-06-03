using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    [SerializeField] string sceneToLoad;

    public void LoadNextScene()
    {
        FindObjectOfType<TemporaryGameManager>().LoadScene(sceneToLoad);
    }
}
