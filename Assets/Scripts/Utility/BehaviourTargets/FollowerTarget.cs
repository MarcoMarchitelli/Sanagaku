using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Compoenete che identifica un oggetto come target del behaviour di follower
    /// </summary>
    public class FollowerTarget : MonoBehaviour
    {
        /// <summary>
        /// Posizione da dell'oggetto
        /// </summary>
        public Vector3 TargetPosition { get { return transform.position; } }
    } 
}
