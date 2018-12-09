using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_tutorialText : MonoBehaviour {

    public GameObject tutorial_text;
    private Collider tutorial_cube_collider;

	void Start () {
        tutorial_text.SetActive(false);
        tutorial_cube_collider = gameObject.GetComponent<Collider>();
	}

    private void OnTriggerEnter(Collider tutorial_cube_collider) //ontriggerstay where are you?
    {
        tutorial_text.SetActive(true);
    }

    private void OnTriggerExit(Collider tutorial_cube_collider)
    {
        tutorial_text.SetActive(false);
    }
}
