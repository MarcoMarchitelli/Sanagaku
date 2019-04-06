using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che attiva il dissolve sopra una stanza
    /// </summary>
    public class DissolverShaderBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Riferimento all'animator
        /// </summary>
        [SerializeField]
        Animator ShaderController;

        /// <summary>
        /// Funzione che attiva il dissolve
        /// </summary>
        /// <param name="_other"></param>
        public void StartDissolveAnimation(Collider _other)
        {
            if (_other.CompareTag("Player"))
                ShaderController.SetBool("PlayDissolve", true);
        }
    } 
}
