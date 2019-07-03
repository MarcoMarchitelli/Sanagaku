using UnityEngine;
using System.Collections;


public class moving_camera : MonoBehaviour
{
    public float speed;
    public float rSpeed;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveUpD = Input.GetAxis("Debug_UpDown");

        float rotateSxDx = 1.0f;

        Vector3 movement = new Vector3(moveHorizontal, moveUpD, moveVertical);
        Quaternion rotation = new Quaternion(0.0f, rotateSxDx * rSpeed, 0.0f, 0.0f);


        GetComponent<Rigidbody>().velocity = movement * speed;

        if (Input.GetKeyDown("o"))
        {
            GetComponent<Transform>().rotation = rotation;
        }
    }
}
