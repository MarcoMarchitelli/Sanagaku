using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// A child of DamageDealerBehaviour that first checks if it hits the OrbInteractionBehaviour(Player).
    /// </summary>
    public class OrbDamageDealerBehaviour : DamageDealerBehaviour
    {

        protected override void OnCollisionEnter(Collision collision)
        {
            if (dealsOnCollision)
            {
                OrbInteractionBehaviour o = collision.collider.GetComponent<OrbInteractionBehaviour>();
                if (o)
                {
                    return;
                }

                DamageReceiverBehaviour receiver = collision.collider.GetComponent<DamageReceiverBehaviour>();
                if (receiver)
                {
                    DealDamage(receiver);
                }
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (dealsOnTrigger)
            {
                OrbInteractionBehaviour o = other.GetComponent<OrbInteractionBehaviour>();
                if (o)
                {
                    return;
                }

                DamageReceiverBehaviour receiver = other.GetComponent<DamageReceiverBehaviour>();
                if (receiver)
                {
                    DealDamage(receiver);
                    print(name + " dealt " + damage + " damage to " + receiver.name);
                }
            }
        }

    } 
}