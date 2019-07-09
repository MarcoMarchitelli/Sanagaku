using UnityEngine;
using System.Collections;


public class moving_camera : MonoBehaviour
{
    //public float speed;
    //public float rSpeed;

    //float moveHorizontal = Input.GetAxis("Horizontal");
    //float moveVertical = Input.GetAxis("Vertical");

    public float rotateSxDx = 1.0f;
    public float mVel = 0.08f;
    public float maxVel = 0;


    void FixedUpdate()
    {
        //Vector3 movement = new Vector3(moveHorizontal, moveUpD, moveVertical);
        //GetComponent<Rigidbody>().velocity = movement * speed;
        float moveUpD = Input.GetAxis("Debug_UpDown");

        if (Input.GetKey("w"))
        {
            GetComponent<Transform>().Translate(0, 0, mVel+maxVel);
        }

        if (Input.GetKey("s"))
        {
            GetComponent<Transform>().Translate(0, 0, -mVel-maxVel);
        }

        if (Input.GetKey("a"))
        {
            GetComponent<Transform>().Translate(mVel+maxVel, 0, 0);
        }

        if (Input.GetKey("d"))
        {
            GetComponent<Transform>().Translate(-mVel-maxVel, 0, 0);
        }

        if (Input.GetKey("e"))
        {
            GetComponent<Transform>().Rotate(0, rotateSxDx, 0);
        }
        if (Input.GetKey("q"))
        {
            GetComponent<Transform>().Rotate(0, -rotateSxDx, 0);
        }
    }
}
