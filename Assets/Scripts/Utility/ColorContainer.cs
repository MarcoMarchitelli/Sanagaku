using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorContainer : MonoBehaviour {

    public static ColorContainer Instance;

    public Color[] Colors;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

}
