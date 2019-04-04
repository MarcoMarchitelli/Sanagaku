using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Spawner che gestisce le fasi del particle system che ha in gestione
    /// </summary>
    public class ObjectSpawner : MonoBehaviour
    {
        #region Singleton
        /// <summary>
        /// SIngleton
        /// </summary>
        public static ObjectSpawner Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(this);
        }
        #endregion

        /// <summary>
        /// Funzione che si occupa di spawnare
        /// </summary>
        public void Spawn(string _pool, bool _isPoolable, Vector3 _position, Quaternion _rotation)
        {
            if (ObjectPooler.Instance == null)
                return;

            if (_isPoolable)
                ObjectPooler.Instance.GetPoolableFromPool(_pool, _position, _rotation);
            else
                ObjectPooler.Instance.GetObjectFromPool(_pool, _position, _rotation);
        }

        /// <summary>
        /// Funzione che si occupa di spawnare
        /// </summary>
        public void Spawn(string _pool, bool _isPoolable, Transform _transform)
        {
            Spawn(_pool, _isPoolable, _transform.position, _transform.rotation);
        }

        /// <summary>
        /// Funzione che si occupa di spawnare un GameObject e di ritornarne la referenza
        /// </summary>
        /// <param name="_pool"></param>
        /// <param name="_position"></param>
        /// <param name="_rotation"></param>
        /// <returns></returns>
        public GameObject SpawnObject(string _pool, Vector3 _position, Quaternion _rotation)
        {
            if (ObjectPooler.Instance == null)
                return null;

            return ObjectPooler.Instance.GetObjectFromPool(_pool, _position, _rotation);
        }

        /// <summary>
        /// Funzione che si occupa di spawnare un GameObject e di ritornarne la referenza
        /// </summary>
        /// <param name="_pool"></param>
        /// <param name="_transform"></param>
        /// <returns></returns>
        public GameObject SpawnObject(string _pool, Transform _transform)
        {
            return SpawnObject(_pool, _transform.position, _transform.rotation);
        }

        /// <summary>
        /// Funzione che si occupa di spawnare un IEntity  e di ritornarne la referenza
        /// </summary>
        /// <param name="_pool"></param>
        /// <param name="_isPoolable"></param>
        /// <param name="_position"></param>
        /// <param name="_rotation"></param>
        /// <returns></returns>
        public IEntity SpawnEntity(string _pool, bool _isPoolable, Vector3 _position, Quaternion _rotation)
        {
            if (ObjectPooler.Instance == null)
                return null;

            IEntity entityToReturn;

            if (_isPoolable)
                entityToReturn = ObjectPooler.Instance.GetPoolableFromPool(_pool, _position, _rotation).gameObject.GetComponent<IEntity>();
            else
                entityToReturn = ObjectPooler.Instance.GetObjectFromPool(_pool, _position, _rotation).GetComponent<IEntity>(); ;

            return entityToReturn;
        }

        /// <summary>
        /// Funzione che si occupa di spawnare un IEntity  e di ritornarne la referenza
        /// </summary>
        /// <param name="_pool"></param>
        /// <param name="_isPoolable"></param>
        /// <param name="_transform"></param>
        /// <returns></returns>
        public IEntity SpawnEntity(string _pool, bool _isPoolable, Transform _transform)
        {
            return SpawnEntity(_pool, _isPoolable, _transform.position, _transform.rotation);
        }
    }
}
