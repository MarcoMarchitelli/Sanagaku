using UnityEngine;

namespace Deirin.AI
{
    public class Mover : MonoBehaviour
    {
        public float speed;
        public Vector3 direction;

        void Update()
        {
            transform.Translate(direction.normalized * Time.deltaTime * speed);
        }
    }
}