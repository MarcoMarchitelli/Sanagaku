using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

namespace Sangaku
{
    public class CameraPostProcessingValueSetter : BaseBehaviour
    {
        PostProcessVolume postProcess;
        ChromaticAberration aberration;
        public float AberrationTime;

        protected override void CustomSetup()
        {
            postProcess = FindObjectOfType<PostProcessVolume>();
            aberration = postProcess.profile.GetSetting<ChromaticAberration>();
        }

        public void SetAberrationValue()
        {
            if (aberration != null)
                StartCoroutine(PunchValue(aberration));
            else
                Debug.LogError("Chromatic aberration non trovata");
        }

        IEnumerator PunchValue(ChromaticAberration aberration)
        {
            WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

            while (aberration.intensity.value < 1)
            {
                aberration.intensity.value += Time.deltaTime *2;
                yield return endOfFrame;
            }

            yield return new WaitForSeconds(AberrationTime);

            while (aberration.intensity.value > 0)
            {
                aberration.intensity.value -= Time.deltaTime *2;
                yield return endOfFrame;
            }

        }
    }
}
