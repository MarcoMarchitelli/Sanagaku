using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che si occupa di tracciare la liena di mira
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class PlayerAimPreviewBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Layer di rimbalzo
        /// </summary>
        [SerializeField] LayerMask bounceLayer;
        /// <summary>
        /// Lunghezza del raggio di preview
        /// </summary>
        [SerializeField] float rayLenght = 5f;
        /// <summary>
        /// Raggio della sfera castata
        /// </summary>
        [SerializeField] float spehreRadius = 1f;

        /// <summary>
        /// Riferimento al LineRenderer
        /// </summary>
        LineRenderer lineRenderer;


        protected override void CustomSetup()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public override void OnUpdate()
        {
            if (!IsSetupped)
                return;

            Vector3 startPoint = transform.position;
            Vector3 endPoint;
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, spehreRadius, out hit, rayLenght, bounceLayer))
                endPoint = hit.point;
            else
                endPoint = (ray.direction.normalized * rayLenght) + ray.origin;

            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}