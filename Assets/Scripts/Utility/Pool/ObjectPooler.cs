using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public bool isIPoolable;
    }

    #region Singleton
    public static ObjectPooler Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        Init();
    }
    #endregion

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> ObjectPoolDictionary;
    public Dictionary<string, Queue<IPoolable>> PoolablePoolDictionary;

    public void Init()
    {
        ObjectPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        PoolablePoolDictionary = new Dictionary<string, Queue<IPoolable>>();

        foreach (Pool pool in Pools)
        {

            GameObject poolContainer = new GameObject(pool.tag);
            poolContainer.transform.parent = transform;

            if (pool.isIPoolable)
            {
                Queue<IPoolable> objectPool = new Queue<IPoolable>();
                for (int i = 0; i < pool.size; i++)
                {
                    IPoolable instPoolable = Instantiate(pool.prefab, poolContainer.transform).GetComponent<IPoolable>();
                    instPoolable.OnPoolCreation();
                    instPoolable.gameObject.SetActive(false);
                    objectPool.Enqueue(instPoolable);
                }

                PoolablePoolDictionary.Add(pool.tag, objectPool);
            }
            else
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject instObj = Instantiate(pool.prefab, poolContainer.transform);
                    instObj.SetActive(false);
                    objectPool.Enqueue(instObj);
                }

                ObjectPoolDictionary.Add(pool.tag, objectPool);
            }
        }
    }

    public GameObject GetObjectFromPool(string _poolTag, Vector3 _position, Quaternion _rotation, bool autoResize = false, bool autoEnqueue = false)
    {
        if (!ObjectPoolDictionary.ContainsKey(_poolTag))
        {
            Debug.LogWarning("The pool with tag: " + _poolTag + " does not exist!");
            return null;
        }

        GameObject objToGet = ObjectPoolDictionary[tag].Dequeue();

        objToGet.SetActive(true);
        objToGet.transform.position = _position;
        objToGet.transform.rotation = _rotation;

        if (autoEnqueue)
            ObjectPoolDictionary[tag].Enqueue(objToGet);

        return objToGet;
    }

    public IPoolable GetPoolableFromPool(string _poolTag, Vector3 _position, Quaternion _rotation, bool autoResize = false, bool autoEnqueue = false)
    {
        if (!PoolablePoolDictionary.ContainsKey(_poolTag))
        {
            Debug.LogWarning("The pool with tag: " + _poolTag + " does not exist!");
            return null;
        }

        IPoolable objToGet = PoolablePoolDictionary[_poolTag].Dequeue();

        objToGet.gameObject.SetActive(true);
        objToGet.gameObject.transform.position = _position;
        objToGet.gameObject.transform.rotation = _rotation;
        objToGet.OnGetFromPool();

        if (autoEnqueue)
            PoolablePoolDictionary[_poolTag].Enqueue(objToGet);

        return objToGet;
    }

    public void PutPoolableInPool(string _poolTag, IPoolable _poolable)
    {
        if (!PoolablePoolDictionary.ContainsKey(_poolTag))
        {
            Debug.LogWarning("The pool with tag: " + _poolTag + " does not exist!");
            return;
        }

        _poolable.OnPutInPool();
        _poolable.gameObject.SetActive(false);

        PoolablePoolDictionary[_poolTag].Enqueue(_poolable);
    }

    public void PutObjectInPool(string _poolTag, GameObject _obj)
    {
        if (!ObjectPoolDictionary.ContainsKey(_poolTag))
        {
            Debug.LogWarning("The pool with tag: " + _poolTag + " does not exist!");
            return;
        }

        _obj.gameObject.SetActive(false);

        ObjectPoolDictionary[tag].Enqueue(_obj);
    }
}