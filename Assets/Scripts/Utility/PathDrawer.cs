using UnityEngine;

namespace Deirin.Utility
{
    public class PathDrawer : MonoBehaviour
    {
        [SerializeField] Color firstWaypointColor = Color.magenta;
        [SerializeField] Color waypointsColor = Color.cyan;
        [SerializeField] Color lineColor = Color.cyan;

        private void OnDrawGizmos()
        {
            if (transform.childCount > 0)
            {
                Vector3 startPos = transform.GetChild(0).position;
                Vector3 lastPos = startPos;

                for (int i = 0; i < transform.childCount; i++)
                {
                    if (i == 0)
                    {
                        Gizmos.color = firstWaypointColor;
                        Gizmos.DrawSphere(transform.GetChild(i).position, .3f);
                    }
                    else
                    {
                        Gizmos.color = waypointsColor;
                        Gizmos.DrawSphere(transform.GetChild(i).position, .3f);
                    }

                    Gizmos.color = lineColor;
                    Gizmos.DrawLine(lastPos, transform.GetChild(i).position);
                    lastPos = transform.GetChild(i).position;
                }

                Gizmos.color = lineColor;
                Gizmos.DrawLine(lastPos, startPos);
            }
        }
    }
}

