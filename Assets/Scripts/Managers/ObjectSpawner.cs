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
        /// Funzione che si occupa di spawnare un'entità
        /// </summary>
        public void SpawnEntity(string _pool, bool _isPoolable, Vector3 _position, Quaternion _rotation)
        {
            if (ObjectPooler.Instance == null)
                return;

            if (_isPoolable)
                ObjectPooler.Instance.GetPoolableFromPool(_pool, _position, _rotation);
            else
                ObjectPooler.Instance.GetObjectFromPool(_pool, _position, _rotation);
        }

        /// <summary>
        /// Funzione che si occupa di spawnare un'entità
        /// </summary>
        public void SpawnEntity(string _pool, bool _isPoolable, Transform _transform)
        {
            SpawnEntity(_pool, _isPoolable, _transform.position, _transform.rotation);
        }
    }
}
