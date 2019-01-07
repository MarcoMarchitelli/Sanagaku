using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExit : MonoBehaviour {


    void Update()
    {

        if (Input.GetKey("escape"))
        {
            Application.Quit();
            Debug.Log("Quit");
        }

        if (Input.GetKey("m"))
        {
            SceneManager.LoadScene("Prototype_02");
            Debug.Log("Restart");
        }
    }

}

