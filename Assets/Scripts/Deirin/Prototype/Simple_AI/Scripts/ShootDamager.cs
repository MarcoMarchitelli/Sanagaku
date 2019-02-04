using UnityEngine;
using UnityEngine.Events;

namespace Deirin.AI
{
    public class ShootDamager : MonoBehaviour
    {
        public int damage = 1;
        public UnityEvent OnDamageDealt;

        private void OnTriggerEnter(Collider other)
        {
            ShootTarget t = other.GetComponent<ShootTarget>();
            if (t)
            {
                t.TakeDamage(damage);
                OnDamageDealt.Invoke();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            ShootTarget t = collision.collider.GetComponent<ShootTarget>();
            if (t)
            {
                t.TakeDamage(damage);
                OnDamageDealt.Invoke();
            }
        }
    }
}
