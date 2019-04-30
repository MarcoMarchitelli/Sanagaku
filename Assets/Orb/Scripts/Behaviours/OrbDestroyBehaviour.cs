using UnityEngine;
using System.Collections;

namespace Sangaku
{
    public class OrbDestroyBehaviour : BaseBehaviour
    {
        #region Serialized Fields
        [SerializeField] bool usesObjectPooler = false;
        [SerializeField] string poolTag;
        [SerializeField] bool isIPoolable = false;
        #endregion

        #region Events
        [SerializeField] UnityOrbControllerEvent OnDestruction;
        #endregion

        ObjectPooler objPooler;
        OrbControllerData data;

        protected override void CustomSetup()
        {

            if (usesObjectPooler)
                objPooler = ObjectPooler.Instance;

            if (Entity.GetType() == typeof(OrbController))
            {
                data = Entity.Data as OrbControllerData;
                OnDestruction.AddListener(data.PlayerShootBehaviour.RemoveOrbFromPlay);
            }
        }

        /// <summary>
        /// Destroys the gameObject
        /// </summary>
        public void Destroy()
        {
            if (usesObjectPooler)
            {
                if (isIPoolable)
                {
                    objPooler.PutPoolableInPool(poolTag, Entity as IPoolable);
                    OnDestruction.Invoke(Entity as OrbController);
                }
                else
                {
                    objPooler.PutObjectInPool(poolTag, Entity.gameObject);
                    OnDestruction.Invoke(Entity as OrbController);
                }
            }
            else
            {
                Destroy(gameObject);
                OnDestruction.Invoke(Entity as OrbController);
            }
        }

        /// <summary>
        /// Destroys the gameObject after a given time
        /// </summary>
        /// <param name="_time"></param>
        public void DestroyAfter(float _time)
        {
            if (usesObjectPooler)
            {
                StartCoroutine(PutInPoolAfter(_time));
            }
            else
            {
                Destroy(gameObject, _time);
                OnDestruction.Invoke(Entity as OrbController);
            }
        }

        /// <summary>
        /// Waits for the given time before putting the Entity in pool.
        /// </summary>
        /// <param name="_time">Time to wait.</param>
        /// <returns></returns>
        IEnumerator PutInPoolAfter(float _time)
        {
            yield return new WaitForSeconds(_time);

            if (isIPoolable)
            {
                objPooler.PutPoolableInPool(poolTag, Entity as IPoolable);
                OnDestruction.Invoke(Entity as OrbController);
            }
            else
            {
                objPooler.PutObjectInPool(poolTag, Entity.gameObject);
                OnDestruction.Invoke(Entity as OrbController);
            }
        }

        private void OnDestroy()
        {
            OnDestruction.Invoke(Entity as OrbController);
        }
    }
}