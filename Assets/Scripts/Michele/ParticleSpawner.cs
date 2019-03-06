using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Spawner che gestisce le fasi del particle system che ha in gestione
    /// </summary>
    public class ParticleSpawner : MonoBehaviour
    {
        HitSmokeParticle currentActiveParticle;

        [SerializeField] protected string particlePool = "HitSmoke";

        #region Events
        public UnityVoidEvent OnParticleStart;
        public UnityVoidEvent OnParticleEnd;
        #endregion

        /// <summary>
        /// Funzione che esegue prima l'evento di start, poi il play della particle
        /// </summary>
        public void PlayParticle()
        {
            OnParticleStart.Invoke();
            currentActiveParticle = ObjectPooler.Instance.GetPoolableFromPool(particlePool, transform.position, transform.rotation) as HitSmokeParticle;
        }

        /// <summary>
        /// Funzione che esegue prima lo stop, poi l'evento di end della particle
        /// </summary>
        public void StopParticle()
        {
            ObjectPooler.Instance.PutPoolableInPool(particlePool, currentActiveParticle);
            OnParticleEnd.Invoke();
        }

        #region Debug
        // REGION PER LE PROVE DI DEBUG NELLA SCENA MICHELETEST!!!
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayParticle();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                StopParticle();
            }
        }
        #endregion
    }
}
