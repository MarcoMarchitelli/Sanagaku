using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Classe che definisce il comportamento di attrazione degli orb
    /// </summary>
    [RequireComponent(typeof(PlayerShootBehaviour))]
    public class PlayerAttractionBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Tipo di attrazione dell'orb
        /// </summary>
        public enum AttractionType { Closest, Farthest, Random, All }
        /// <summary>
        /// Tipo di azione da eseguire per gli altri orb
        /// </summary>
        public enum OhterAction { DoNothing, ConvertToMana }

        /// <summary>
        /// Tipo di scelta dell'orb da attrarre
        /// </summary>
        [Tooltip("How attract the orbs. The 'All' case ingores the second option")]
        [SerializeField] AttractionType orbAttractionType;
        /// <summary>
        /// Azione da eseguire sugli altri orb
        /// </summary>
        [Tooltip("What does with other orbs.")]
        [SerializeField] OhterAction otherOrbAction;
        /// <summary>
        /// Costo di mana, al secondo, per l'utilizzo dell'abilità
        /// </summary>
        [Tooltip("Per second of usage")]
        [SerializeField] float manaCost;
        /// <summary>
        /// Tempo massimo, in secondi, per la durata dell'abilità.
        /// Se il valore è negativo la durata è infinita.
        /// </summary>
        [Tooltip("In seconds. Negative number equals infinite time")]
        [SerializeField] float maxAttractionTime;

        /// <summary>
        /// Riferimento al behaviour della mana
        /// </summary>
        PlayerManaBehaviour manaBehaviour;
        /// <summary>
        /// Riferimento al behaviour di shoot
        /// </summary>
        PlayerShootBehaviour shootBehaviour;
        /// <summary>
        /// Lista di orb in gioco
        /// </summary>
        List<OrbController> orbsInPlay;
        /// <summary>
        /// Lista di orb da attrarre
        /// </summary>
        List<OrbAttractionBehaviour> orbsToAttract;

        bool isAttracting;

        /// <summary>
        /// Custom setup del behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            shootBehaviour = GetComponent<PlayerShootBehaviour>();
            manaBehaviour = GetComponent<PlayerManaBehaviour>();
            orbsInPlay = shootBehaviour.GetOrbsInPlay();
            orbsToAttract = new List<OrbAttractionBehaviour>();
        }

        /// <summary>
        /// Funzione che attiva o disattiva l'attrazione dell'orb in base al comportamento scelto
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleAttraction(bool _value)
        {
            if (!IsSetupped)
                return;

            isAttracting = _value;

            if (isAttracting)
            {
                if (orbsInPlay.Count == 0)
                {
                    return;
                }
                else if (orbsInPlay.Count == 1)
                {
                    SetOrbAsAttractable(orbsInPlay[0]);
                    return;
                }

                switch (orbAttractionType)
                {
                    case AttractionType.Closest:
                        AttractClosest();
                        break;
                    case AttractionType.Farthest:
                        AttractFarthest();
                        break;
                    case AttractionType.Random:
                        AttractRandom();
                        break;
                    case AttractionType.All:
                        AttractAll();
                        break;
                }
            }
            else
            {
                orbsToAttract.Clear();
            }
        }

        /// <summary>
        /// Funzione che rimuove l'orb dalla lista di quelli da attrarre
        /// </summary>
        /// <param name="_orbAttractionBehaviour"></param>
        public void RemoveOrbFromAttraction(OrbAttractionBehaviour _orbAttractionBehaviour)
        {
            orbsToAttract.Remove(_orbAttractionBehaviour);
        }

        void Update()
        {
            if (IsSetupped && isAttracting)
                AttractOrb();
        }

        /// <summary>
        /// Funzione che effettua l'attrazione dell'orb
        /// </summary>
        /// <param name="_orb"></param>
        void AttractOrb()
        {
            for (int i = 0; i < orbsToAttract.Count; i++)
            {
                orbsToAttract[i].MoveTowardsPosition(transform.position);
            }
        }

        /// <summary>
        /// Funzione che aggiunge un orb alla lista di quelli da attrarre
        /// </summary>
        /// <param name="_orb"></param>
        void SetOrbAsAttractable(OrbController _orb)
        {
            OrbAttractionBehaviour orbAttractionBehaviour = _orb.GetComponent<OrbAttractionBehaviour>();
            orbAttractionBehaviour.SetPlayerAttractionBehaviour(this);
            orbsToAttract.Add(orbAttractionBehaviour);
        }

        /// <summary>
        /// Funzione che chiama l'attrazione all'orb più vicino
        /// </summary>
        void AttractClosest()
        {
            float tempDistance;
            float distance = tempDistance = 10000f;
            int closestIndex = 0;

            for (int i = 0; i < orbsInPlay.Count; i++)
            {
                tempDistance = CalculateOrbDistance(orbsInPlay[i]);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    closestIndex = i;
                }
            }

            SetOrbAsAttractable(orbsInPlay[closestIndex]);
            PerformActionOnOtherOrbs(orbsInPlay[closestIndex]);
        }

        /// <summary>
        /// Funzione che chiama l'attrazione all'orb più lontano
        /// </summary>
        void AttractFarthest()
        {
            float tempDistance;
            float distance = tempDistance = -1f;
            int farthestIndex = 0;

            for (int i = 0; i < orbsInPlay.Count; i++)
            {
                tempDistance = CalculateOrbDistance(orbsInPlay[i]);
                if (tempDistance > distance)
                {
                    distance = tempDistance;
                    farthestIndex = i;
                }
            }

            SetOrbAsAttractable(orbsInPlay[farthestIndex]);
            PerformActionOnOtherOrbs(orbsInPlay[farthestIndex]);
        }

        /// <summary>
        /// Funzione che chiama l'attrazione ad un orb a caso
        /// </summary>
        void AttractRandom()
        {
            int randomIndex = Random.Range(0, orbsInPlay.Count);
            SetOrbAsAttractable(orbsInPlay[randomIndex]);
            PerformActionOnOtherOrbs(orbsInPlay[randomIndex]);
        }

        /// <summary>
        /// Funzione che chiama l'attrazione a tutti gli orb
        /// </summary>
        void AttractAll()
        {
            for (int i = 0; i < orbsInPlay.Count; i++)
                SetOrbAsAttractable(orbsInPlay[i]);

            // questo caso ignora la scelta sull'azione sugli orb non attratti
        }

        /// <summary>
        /// Funzione che calcola la distanza tra orb e player
        /// </summary>
        /// <param name="_orb"></param>
        /// <returns></returns>
        float CalculateOrbDistance(OrbController _orb)
        {
            return Vector3.Distance(transform.position, _orb.transform.position);
        }

        /// <summary>
        /// Funzione che esegue l'azione scelta per gli orb non in attrazione
        /// </summary>
        /// <param name="_attractedOrb"></param>
        void PerformActionOnOtherOrbs(OrbController _attractedOrb)
        {
            switch (otherOrbAction)
            {
                case OhterAction.DoNothing:
                    break;
                case OhterAction.ConvertToMana:
                    for (int i = 0; i < orbsInPlay.Count; i++)
                    {
                        if (orbsInPlay[i] == _attractedOrb)
                            continue;
                        else
                        {
                            // DO mana conversion of the orb
                        }
                    }
                    break;
            }
        }
    }
}
