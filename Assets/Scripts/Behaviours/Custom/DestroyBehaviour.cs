using UnityEngine;
using System.Collections;

namespace Sangaku
{
    public class DestroyBehaviour : BaseBehaviour
    {

        [SerializeField] bool usesObjectPooler = false;
        [SerializeField] string poolTag;
        [SerializeField] bool isIPoolable = false;

        #region Events
        [SerializeField] UnityVoidEvent OnDestruction;
        #endregion

        ObjectPooler objPooler;

        protected override void CustomSetup()
        {
            if (usesObjectPooler)
                objPooler = ObjectPooler.Instance;
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
                }
                else
                {
                    objPooler.PutObjectInPool(poolTag, Entity.gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
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
            }
            else
            {
                objPooler.PutObjectInPool(poolTag, Entity.gameObject);
            }
        }

        private void OnDestroy()
        {
            if(!usesObjectPooler)
                OnDestruction.Invoke();
        }

    }

}