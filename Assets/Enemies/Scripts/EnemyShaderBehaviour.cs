using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    [RequireComponent(typeof(Renderer))]
    public class EnemyShaderBehaviour : BaseBehaviour
    {
        Renderer rend;
        [SerializeField]
        string shaderDamageParameter;
        [SerializeField]
        float DamageValue;
        [SerializeField]
        float damagetime;

        [SerializeField]
        string shaderDeathParameter;
        [SerializeField]
        float DeathValue;
        [SerializeField]
        float deathtime;

        IEnumerator deathCoroutine;

        protected override void CustomSetup()
        {
            rend = GetComponent<Renderer>();
            deathCoroutine = ChangeDeathShaderValue();
        }

        public void SetDamageValue()
        {
            rend.material.SetFloat(shaderDamageParameter, DamageValue);
        }

        public void SetDeathValue()
        {
            StartCoroutine(deathCoroutine);
        }

        IEnumerator ChangeDamageColor()
        {
            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            float paramValue = rend.material.GetFloat(shaderDamageParameter);
            while (paramValue < DamageValue)
            {
                paramValue += Time.deltaTime;
                rend.material.SetFloat(shaderDeathParameter, paramValue);
                yield return wait;
            }
        }


        IEnumerator ChangeDeathShaderValue()
        {
            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            float paramValue = rend.material.GetFloat(shaderDeathParameter);
            while ( paramValue != DeathValue)
            {
                paramValue += Time.deltaTime;
                rend.material.SetFloat(shaderDeathParameter, paramValue);
                yield return wait;
            }
        }

    } 
}
