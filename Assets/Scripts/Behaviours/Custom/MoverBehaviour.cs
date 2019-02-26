using UnityEngine;

namespace Sangaku
{
    public class MoverBehaviour : BaseBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected Vector3 direction;

        public override void OnUpdate()
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