using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BouncePreview : MonoBehaviour
{
    public int Bounces = 1;
    public LayerMask BounceLayer;
    public float RayLenght = 100f;
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Bounces + 2;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        for (int i = 0, j = 0; i <= Bounces; i++)
        {
            if (Physics.Raycast(ray, out hit, RayLenght, BounceLayer))
            {
                Vector3 bounceDirection = Vector3.Reflect(ray.direction, hit.normal);
                if (i == 0)
                {
                    lineRenderer.SetPosition(j, ray.origin);
                    j++;
                }
                lineRenderer.SetPosition(j, hit.point);
                j++;
                //Debug.DrawLine(ray.origin, hit.point, Color.red);
                ray = new Ray(hit.point, bounceDirection);
            }
        }
    }

}
