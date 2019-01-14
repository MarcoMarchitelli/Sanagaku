using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BouncePreview : MonoBehaviour
{
    public int Bounces = 1;
    public LayerMask BounceLayer;
    public float RayLenght = 100f;
    LineRenderer lineRenderer;
    List<Vector3> linePoints;

    private void Awake()
    {
        linePoints = new List<Vector3>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        linePoints.Clear();
        linePoints.Add(ray.origin);

        for (int i = 0; i <= Bounces; i++)
        {
            if (Physics.Raycast(ray, out hit, RayLenght, BounceLayer))
            {
                Vector3 bounceDirection = Vector3.Reflect(ray.direction, hit.normal);
                linePoints.Add(hit.point);
                ray = new Ray(hit.point, bounceDirection);
                continue;
            }
            else
            {
                linePoints.Add(ray.direction * RayLenght);
                break;
            }
        }

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());

    }

}