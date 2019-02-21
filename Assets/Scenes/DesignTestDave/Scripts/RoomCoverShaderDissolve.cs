using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCoverShaderDissolve : MonoBehaviour
{
    [SerializeField]
    private Animator ShaderController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ShaderController.SetBool("PlayDissolve", true);

        }
    }

}
