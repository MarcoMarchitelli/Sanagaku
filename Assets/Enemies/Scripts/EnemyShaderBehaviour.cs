using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField]
        float DamageShowTime;

        [Header("Death")]
        [SerializeField]
        string shaderDeathParameter;
        [SerializeField]
        float DeathValue;
        [SerializeField]
        float deathtime;

        Renderer rend;
        IEnumerator deathCoroutine;

        protected override void CustomSetup()
        {
            rend = GetComponent<Renderer>();
            deathCoroutine = ChangeDeathShaderValue();
        }

        public void SetDamageValue()
        {
            //rend.material.SetFloat(shaderDamageParameter, DamageValue);
        }

        public void SetDeathValue()
        {
            StartCoroutine(deathCoroutine);
        }

        IEnumerator ChangeDamageColor()
        {
            float counter = damageTime;
            float multiplier = DamageValue / damageTime;
            WaitForEndOfFrame yieldInstruction = new WaitForEndOfFrame();

            while (counter > 0)
            {
                rend.material.SetFloat(shaderDamageParameter, DamageValue * multiplier);
                counter -= Time.deltaTime;
                yield return yieldInstruction;
            }

            yield return new WaitForSeconds(2);

            while (counter < damageTime)
            {
                rend.material.SetFloat(shaderDamageParameter, DamageValue * multiplier);
                counter += Time.deltaTime;
                yield return yieldInstruction;
            }
        }


        IEnumerator ChangeDeathShaderValue()
        {
            float counter = deathtime;
            float multiplier = DeathValue / deathtime;
            WaitForEndOfFrame yieldInstruction = new WaitForEndOfFrame();

            while (counter > 0)
            {
                rend.material.SetFloat(shaderDamageParameter, DeathValue * multiplier);
                counter -= Time.deltaTime;
                yield return yieldInstruction;
            }
        }

    } 
}
