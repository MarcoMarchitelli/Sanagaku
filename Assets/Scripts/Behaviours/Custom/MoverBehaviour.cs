using UnityEngine;

namespace Sangaku
{
    public class MoverBehaviour : BaseBehaviour
    {
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