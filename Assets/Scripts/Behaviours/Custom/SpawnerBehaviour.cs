using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che richiama l'objectSpawner
    /// </summary>
    public class SpawnerBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Lista di elementi da spawnare
        /// </summary>
        [SerializeField] List<SpawnerData> objectsToSpawn = new List<SpawnerData>();

        /// <summary>
        /// Funzione che si occupa di spawnare gli oggetti nella lista
        /// </summary>
        public void Spawn()
        {
            for (int i = 0; i < objectsToSpawn.Count; i++)
            {
                ObjectSpawner.Instance.SpawnEntity(objectsToSpawn[i].poolTag, objectsToSpawn[i].isPoolable, objectsToSpawn[i].transform);
            }
        }

        /// <summary>
        /// Dato dell'oggetto da spawnare
        /// </summary>
        [System.Serializable]
        public class SpawnerData
        {
            /// <summary>
            /// Tag del pool
            /// </summary>
            public string poolTag;
            /// <summary>
            /// Se è IPoolable o meno
            /// </summary>
            public bool isPoolable;
            /// <summary>
            /// Dove spawnare l'oggetto
            /// </summary>
            public Transform transform;
        }
    }
}