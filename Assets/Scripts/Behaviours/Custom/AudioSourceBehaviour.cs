using System.Collections;
using UnityEngine;

namespace Sangaku
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceBehaviour : MonoBehaviour
    {
        [SerializeField] UnityVoidEvent OnStopPlaying;

        AudioSource source;

        private void Start()
        {
            source = GetComponent<AudioSource>();
            StartCoroutine(WaitForSound());
        }

        public IEnumerator WaitForSound()
        {
            if (source != null)
            {
                yield return new WaitWhile(() => source.isPlaying == true);
                OnStopPlaying.Invoke();
            }
        }
    }
}