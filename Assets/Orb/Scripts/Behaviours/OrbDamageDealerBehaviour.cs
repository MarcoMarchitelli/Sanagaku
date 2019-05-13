using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour di danno specifico per l'orb
    /// </summary>
    public class OrbDamageDealerBehaviour : DamageDealerBehaviour
    {
        protected override void TriggerEnter(Collider other)
        {
            if (IsSetupped && dealsOnTrigger)
            {
                DamageReceiverBehaviour receiver = other.GetComponent<DamageReceiverBehaviour>();
                if (receiver != null && receiver.Entity.GetType() != typeof(PlayerController))
                    DealDamage(receiver);
            }
        }

        protected override void CollisionEnter(Collision collision)
        {
            if (IsSetupped && dealsOnCollision)
            {
                DamageReceiverBehaviour receiver = collision.collider.GetComponent<DamageReceiverBehaviour>();

                ///Controlla se l'owner è l'orb e il ricevente è il player, in tal caso non applica danno e ritorna
                if (Entity.GetType() == typeof(OrbController) && receiver.Entity.GetType() == typeof(PlayerController))
                    return;

                if (receiver != null && receiver.Entity.GetType() != typeof(PlayerController))
                    DealDamage(receiver);
            }
        }
    }
}