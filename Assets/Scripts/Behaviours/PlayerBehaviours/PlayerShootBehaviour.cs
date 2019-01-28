using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    [RequireComponent(typeof(ManaBehaviour))]
    public class PlayerShootBehaviour : ShootBehaviour
    {

        ManaBehaviour mana;

        /// <summary>
        /// Costo di un proiettile in mana
        /// </summary>
        [Tooltip("Costo di un proiettile in mana")]
        [SerializeField] float cost;

        /// <summary>
        /// Eseguo il setup del behaviour
        /// </summary>
        /// <param name="_entity"></param>
        public override void Setup(IEntity _entity)
        {
            Entity = _entity;
            mana = GetComponent<ManaBehaviour>();
            IsSetupped = true;
        }

        /// <summary>
        /// Instanzia proiettile solo se c'è abbastanza mana
        /// </summary>
        public override void Shoot()
        {
            if (mana.SetMana(-cost))
            {
                //----- NOT COOL YET
                Orb instantiatedOrb = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation).GetComponent<Orb>();
                instantiatedOrb.SetUpSM();
                //----- AWFUL
            }
        }
    }

}