using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Sangaku
{
    [RequireComponent(typeof(Renderer))]
    public class EnemyShaderBehaviour : BaseBehaviour
    {

        [Header("Damage")]
        [SerializeField]
        string shaderDamageParameter;
        [SerializeField]
        float DamageValue;
        [SerializeField]
        float damageTime;

        [Header("Death")]
        [SerializeField]
        string shaderDeathParameter;
        [SerializeField]
        float DeathValue;
        [SerializeField]
        float deathtime;

        Renderer rend;

        protected override void CustomSetup()
        {
            rend = GetComponent<Renderer>();
        }

        /// <summary>
        /// Cambia il colore del nemico in un lasso di tempo e ritorna al colore originale
        /// </summary>
        public void SetDamageValue()
        {
            if (rend != null)
            {
                float startV = rend.material.GetFloat(shaderDamageParameter);
                rend.material.DOFloat(DamageValue, shaderDamageParameter, damageTime).OnComplete(() =>
                {
                    if (rend != null)
                        rend.material.DOFloat(startV, shaderDamageParameter, damageTime);
                });
            }
        }

        /// <summary>
        /// Cambia la proprietà shaderdeathparameter fino al valore indicato in DeathValue
        /// </summary>
        public void SetDeathValue()
        {
            rend.material.DOFloat(DeathValue, shaderDeathParameter, deathtime);
        }
    } 
}
