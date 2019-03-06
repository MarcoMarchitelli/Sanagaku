using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Script dell'oggetto HitSmoke che è poolabile
    /// </summary>
    public class HitSmokeParticle : BaseEntity, IPoolable
    {
        ParticleSystem particle;
        DestroyBehaviour destroyBehaviour;
        
        public override void CustomSetup()
        {
            particle = GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = particle.main;
            main.stopAction = ParticleSystemStopAction.Callback; // Serve per chiamare l'evento OnParticleSystemStopped
            destroyBehaviour = GetComponent<DestroyBehaviour>();
        }

        public void OnGetFromPool()
        {
            SetUpEntity();
            particle.Play();
        }

        public void OnPoolCreation()
        {

        }

        public void OnPutInPool()
        {

        }

        /// <summary>
        /// Evento chiamato alla fine del ciclo completo di vita di un particle system
        /// </summary>
        public void OnParticleSystemStopped()
        {
            destroyBehaviour.Destroy();
        }
    }
}