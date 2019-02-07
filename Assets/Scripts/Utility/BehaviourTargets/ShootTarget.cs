using UnityEngine;
using UnityEngine.Events;

namespace Deirin.AI
{
    public class ShootTarget : MonoBehaviour
    {
        public IntEvent OnHit;

        public void TakeDamage(int _damage)
        {
            OnHit.Invoke(_damage);
        }
    }

    [System.Serializable]
    public class IntEvent : UnityEvent<int>
    {

    }
}
