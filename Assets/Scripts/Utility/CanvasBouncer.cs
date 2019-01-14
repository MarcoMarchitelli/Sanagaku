using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ANCORARE LA PALLYNA AL CENTRO :)
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class CanvasBouncer : MonoBehaviour {

    //parameters
    public float speed = 100;

    //data
    RectTransform rt;
    Vector3 direction;
    Vector2 contactPoint;
    Vector2 topRight, botLeft;

    #region MonoBehaviour Methods

    private void Awake()
    {
        Setup();
    }

    private void Update()
    {
        topRight = new Vector2(Screen.width, Screen.height);
        Move();
        CheckCollisions();
    }

    #endregion

    #region API

    void Setup()
    {
        rt = transform as RectTransform;      
        botLeft = Vector2.zero;
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
    }

    void Move()
    {
        rt.position = rt.position + direction * speed * Time.deltaTime;
    }

    void CheckCollisions()
    {
        //hit right wall
        if (rt.position.x + rt.rect.xMax * .5f>= topRight.x)
        {
            contactPoint = rt.anchoredPosition;
            Bounce(Vector2.left);
        }
        //hit left wall
        if (rt.position.x - rt.rect.xMax * .5f <= botLeft.x)
        {
            contactPoint = rt.anchoredPosition;
            Bounce(Vector2.right);
        }
        //hit bottom wall
        if (rt.position.y - rt.rect.yMax * .5f <= botLeft.y)
        {
            contactPoint = rt.anchoredPosition;
            Bounce(Vector2.up);
        }
        //hit top wall
        if (rt.position.y + rt.rect.yMax * .5f >= topRight.y)
        {
            contactPoint = rt.anchoredPosition;
            Bounce(Vector2.down);
        }
    }

    void Bounce(Vector2 _normal)
    {
        direction = Vector2.Reflect(direction, _normal).normalized;
    }

    #endregion

}
