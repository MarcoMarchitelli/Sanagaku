using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneReloader : MonoBehaviour {

    public KeyCode ReloadKey;
	
	void Update () {
        if (Input.GetKeyDown(ReloadKey))
        {
            RealoadScene();
        }
	}

    void RealoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
