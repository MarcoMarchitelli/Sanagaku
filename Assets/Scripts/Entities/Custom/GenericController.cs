using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Entità generica
    /// </summary>
    public class GenericController : BaseEntity
    {
        [SerializeField] UnityVoidEvent OnSetup;

        [SerializeField] bool autoStart;
        void Start() 
        {
            if (autoStart)
            {
                SetUpEntity();
                OnSetup.Invoke();
            }
        }
    } 
}
