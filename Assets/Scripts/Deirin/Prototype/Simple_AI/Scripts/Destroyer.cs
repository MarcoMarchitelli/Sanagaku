using UnityEngine;

namespace Deirin.Utility
{
    public class Destroyer : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void DestroyAfter(float _time)
        {
            Destroy(gameObject, _time);
        }
    }
}