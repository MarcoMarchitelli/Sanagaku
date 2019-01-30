using UnityEngine;

public class CameraFacer : MonoBehaviour
{

    [SerializeField] Camera camToLookAt;

    private void Awake()
    {
        if (!camToLookAt)
        {
            camToLookAt = Camera.main;
        }
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(camToLookAt.transform.eulerAngles.x, camToLookAt.transform.eulerAngles.y, transform.eulerAngles.z);
    }

}
