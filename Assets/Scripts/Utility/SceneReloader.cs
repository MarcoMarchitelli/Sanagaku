using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneReloader : MonoBehaviour {

    public KeyCode ReloadKey;
    public string SceneToLoad;
	
	void Update () {
        if (Input.GetKeyDown(ReloadKey))
        {
            RealoadScene();
        }
	}

    public void RealoadScene()
    {
        Scene s = SceneManager.GetSceneByName(SceneToLoad);
        if (s == null)
        {
            Debug.LogError(name + " cannot load scene named: " + SceneToLoad + ", because it doesn't exist!");
            return;
        }
        SceneManager.LoadScene(SceneToLoad);
    }
}