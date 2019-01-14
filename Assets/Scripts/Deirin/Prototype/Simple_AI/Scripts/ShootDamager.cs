using UnityEngine;

namespace Deirin.AI
{
    public class ShootDamager : MonoBehaviour
    {

        public int damage = 1;

        private void OnCollisionEnter(Collision collision)
        {
            ShootTarget t = collision.collider.GetComponent<ShootTarget>();
            if (t)
                t.TakeDamage(damage);
        }
    }
}
