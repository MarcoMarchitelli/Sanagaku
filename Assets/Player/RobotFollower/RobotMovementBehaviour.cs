using UnityEngine;

namespace Sangaku
{
    public class RobotMovementBehaviour : BaseBehaviour
    {
        Vector3 direction;
        Rigidbody rBody;
        float moveSpeed;

        public bool IsRbodyMovement;

        [Header("Transform movement")]
        public float LerpTime;

        [Header("General")]
        public Transform Target;
        public float DistanceFromTarget;
        public Vector3 Offset;

        protected override void CustomSetup()
        {
            moveSpeed = 12;
            rBody = GetComponent<Rigidbody>();
        }

        public override void OnLateUpdate()
        {
            if (transform != null && transform.position != Target.position - Offset)
                Move(GetTargetDirection());
        }

        Vector3 GetTargetDirection()
        {
            return new Vector3(Target.position.x - transform.position.x, Target.position.y - transform.position.y, Target.position.z - transform.position.z);
        }
        
        public void Move(Vector3 _direction)
        {
            if(IsRbodyMovement)
                rBody.MovePosition(rBody.position + _direction * moveSpeed * Time.deltaTime);
            else
                transform.position = Vector3.Lerp(transform.position, Target.position - Offset, LerpTime);
        }
    }
}
