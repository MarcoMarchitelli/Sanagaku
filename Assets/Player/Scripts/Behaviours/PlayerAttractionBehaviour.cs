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
        /// Evento lanciato all'inizio dell'attrazione
        /// </summary>
        [SerializeField] UnityVoidEvent OnAttractionStart;
        /// <summary>
        /// Evento lanciato alla fine dell'attrazione
        /// </summary>
        [SerializeField] UnityVoidEvent OnAttractionEnd;

        /// <summary>
        /// Tipo di scelta dell'orb da attrarre
        /// </summary>
        [Tooltip("How attract the orbs. The 'All' case ingores the second option")]
        [SerializeField] AttractionType orbAttractionType;
        /// <summary>
        /// Costo di mana, al secondo, per l'utilizzo dell'abilità
        /// </summary>
        [Tooltip("Per second of usage")]
        [SerializeField] float manaCost;
        /// <summary>
        /// Tempo massimo, in secondi, per la durata dell'abilità.
        /// Se il valore è negativo la durata è infinita.
        /// </summary>
        [Tooltip("In seconds. Negative number equals to infinite time")]
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
        /// Punto in cui sparo l'orb
        /// </summary>
        Transform shootPoint;
        /// <summary>
        /// Lista di orb in gioco
        /// </summary>
        List<OrbController> orbsInPlay;
        /// <summary>
        /// Lista di orb da attrarre
        /// </summary>
        List<OrbAttractionBehaviour> orbsToAttract;

        /// <summary>
        /// True se il behaviour può operare, false altrimenti
        /// </summary>
        bool canAttract;
        /// <summary>
        /// Contatore del tempo di attivita del behaviour
        /// </summary>
        float attractionTime;

        /// <summary>
        /// Custom setup del behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            shootBehaviour = Entity.GetBehaviour<PlayerShootBehaviour>();
            manaBehaviour = Entity.GetBehaviour<PlayerManaBehaviour>();
            orbsInPlay = shootBehaviour.GetOrbsInPlay();
            shootPoint = shootBehaviour.GetShootPoint();
            orbsToAttract = new List<OrbAttractionBehaviour>();
        }

        public override void Enable(bool _value)
        {
            base.Enable(_value);

            if (!_value)
            {
                canAttract = false;
                DisableAttraction();
            }
        }

        public override void OnUpdate()
        {
            if (!IsSetupped)
                return;

            CountAttractionTime();
            DecrementMana();
            AttractOrb();
        }

        /// <summary>
        /// Funzione che attiva o disattiva l'attrazione
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleAttraction(bool _value)
        {
            if (!IsSetupped)
                return;

            canAttract = _value;

            if (canAttract)
                OnAttractionStart.Invoke();
            else
                OnAttractionEnd.Invoke();
        }

        /// <summary>
        /// Funzione che rimuove l'orb dalla lista di quelli da attrarre
        /// </summary>
        /// <param name="_orbAttractionBehaviour"></param>
        public void RemoveOrbFromAttraction(OrbAttractionBehaviour _orbAttractionBehaviour)
        {
            orbsToAttract.Remove(_orbAttractionBehaviour);
        }

        /// <summary>
        /// Funzione che si occupa di contare il tempo di attivazione dell'attrazione
        /// </summary>
        void CountAttractionTime()
        {
            if (maxAttractionTime <= 0 || !canAttract)
                return;

            attractionTime += Time.deltaTime;
            if (attractionTime >= maxAttractionTime)
            {
                attractionTime = 0;
                ToggleAttraction(false);
            }
        }

        /// <summary>
        /// Funzione che si occupa di decrementare il mana
        /// </summary>
        void DecrementMana()
        {
            if (manaCost <= 0)
                return;

            manaBehaviour.AddMana(-(manaCost * Time.deltaTime));
            if (manaBehaviour.GetMana() <= 0)
            {
                ToggleAttraction(false);
            }
        }

        /// <summary>
        /// Funzione che esegue l'attrazione dell'orb in base al comportamento scelto
        /// </summary>
        void AttractOrb()
        {
            if (canAttract)
            {
                if (orbsInPlay.Count == 1)
                {
                    SetOrbAsAttractable(orbsInPlay[0]);
                }
                else
                {
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
            }
            else
            {
                DisableAttraction();
                return;
            }

            if (orbsToAttract.Count > 0)
            {
                for (int i = 0; i < orbsToAttract.Count; i++)
                    orbsToAttract[i].MoveTowardsPosition(shootPoint.position);
            }
        }

        /// <summary>
        /// Funzione che aggiunge un orb alla lista di quelli da attrarre
        /// </summary>
        /// <param name="_orb"></param>
        void SetOrbAsAttractable(OrbController _orb)
        {
            OrbAttractionBehaviour orbAttractionBehaviour = _orb.GetBehaviour<OrbAttractionBehaviour>();
            if (!orbsToAttract.Contains(orbAttractionBehaviour))
            {
                orbAttractionBehaviour.SetPlayerAttractionBehaviour(this);
                orbsToAttract.Add(orbAttractionBehaviour);
            }
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

            orbsToAttract.Clear();
            if (closestIndex < orbsInPlay.Count)
                SetOrbAsAttractable(orbsInPlay[closestIndex]);
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

            orbsToAttract.Clear();
            SetOrbAsAttractable(orbsInPlay[farthestIndex]);
        }

        /// <summary>
        /// Funzione che chiama l'attrazione ad un orb a caso
        /// </summary>
        void AttractRandom()
        {
            orbsToAttract.Clear();
            int randomIndex = Random.Range(0, orbsInPlay.Count);
            SetOrbAsAttractable(orbsInPlay[randomIndex]);
        }

        /// <summary>
        /// Funzione che chiama l'attrazione a tutti gli orb
        /// </summary>
        void AttractAll()
        {
            for (int i = 0; i < orbsInPlay.Count; i++)
                SetOrbAsAttractable(orbsInPlay[i]);
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
        /// Funzione che disabilità l'attrazione per gli orb
        /// </summary>
        void DisableAttraction()
        {
            for (int i = 0; i < orbsToAttract.Count; i++)
                orbsToAttract[i].SetPlayerAttractionBehaviour(null);

            orbsToAttract.Clear();
        }
    }
}
