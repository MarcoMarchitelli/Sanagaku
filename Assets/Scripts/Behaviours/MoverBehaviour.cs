using UnityEngine;

namespace Sangaku
{
    public class MoverBehaviour : MonoBehaviour, IBehaviour
    {

        /// <summary>
        /// Riferimento all'entitià che controlla il Behaviour
        /// </summary>
        public IEntity Entity { get; private set; }
        /// <summary>
        /// True se il Behaviour è stato setuppato, false altrimenti
        /// </summary>
        public bool IsSetupped { get; private set; }

        /// <summary>
        /// Eseguo il setup del behaviour
        /// </summary>
        /// <param name="_entity"></param>
        public void Setup(IEntity _entity)
        {
            Entity = _entity;
            IsSetupped = true;
        }

        [SerializeField] protected float speed;
        [SerializeField] protected Vector3 direction;

        void Update()
        {
            if (IsSetupped)
                Move();
        }

        void Move()
        {
            transform.Translate(direction.normalized * Time.deltaTime * speed);
        }
    }
}