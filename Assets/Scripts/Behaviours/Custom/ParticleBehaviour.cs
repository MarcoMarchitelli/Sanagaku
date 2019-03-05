using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisce le fasi di un particle system
    /// </summary>
    public class ParticleBehaviour : BaseBehaviour
    {
        ParticleSystem Particle;
        bool isActive;

        #region Events
        public UnityVoidEvent OnParticleStart;
        public UnityVoidEvent OnParticleStay;
        public UnityVoidEvent OnParticleEnd;
        #endregion

        protected override void CustomSetup()
        {
            Particle = GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = Particle.main;
            main.stopAction = ParticleSystemStopAction.Callback; // Serve ad ottenere la chiamata "OnParticleSystemStopped"
        }

        /// <summary>
        /// Funzione che esegue prima l'evento di start, poi il play della particle
        /// </summary>
        public void PlayParticle()
        {
            if (IsSetupped)
            {
                OnParticleStart.Invoke();
                Particle.Play();
                isActive = true;
            }
        }

        /// <summary>
        /// Funzione che esegue l'evento di Update della particle se è attivo
        /// </summary>
        public void UpdateParticle()
        {
            if (isActive && IsSetupped)
            {
                OnParticleStay.Invoke();
            }
        }

        /// <summary>
        /// Funzione che esegue prima lo stop, poi l'evento di end della particle
        /// </summary>
        public void StopParticle()
        {
            if (IsSetupped)
            {
                isActive = false;
                Particle.Stop();
                OnParticleEnd.Invoke();
            }
        }

        /// <summary>
        /// Evento chiamato alla fine del ciclo completo di vita di un particle system
        /// </summary>
        public void OnParticleSystemStopped()
        {
            StopParticle();
        }
    }
}
