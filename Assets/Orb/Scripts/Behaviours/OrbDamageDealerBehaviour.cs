using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour di danno specifico per l'orb
    /// </summary>
    public class OrbDamageDealerBehaviour : DamageDealerBehaviour
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (IsSetupped && dealsOnTrigger)
            {
                DamageReceiverBehaviour receiver = other.GetComponent<DamageReceiverBehaviour>();
                if (receiver != null && !receiver.Entity.GetType().IsAssignableFrom(typeof(PlayerController)))
                    DealDamage(receiver);
            }
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            if (IsSetupped && dealsOnCollision)
            {
                DamageReceiverBehaviour receiver = collision.collider.GetComponent<DamageReceiverBehaviour>();
                if (receiver != null && !receiver.Entity.GetType().IsAssignableFrom(typeof(PlayerController)))
                    DealDamage(receiver);
            }
        }
    }
}