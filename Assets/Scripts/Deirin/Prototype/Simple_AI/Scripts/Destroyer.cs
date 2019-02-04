using UnityEngine;

namespace Deirin.Utility
{
    public class Destroyer : MonoBehaviour
    {
        public bool destroyOnCollision;
        public string DestructionLayer;

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void DestroyAfter(float _time)
        {
            Destroy(gameObject, _time);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(destroyOnCollision && other.gameObject.layer == LayerMask.NameToLayer(DestructionLayer))
            {
                Destroy();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (destroyOnCollision && collision.collider.gameObject.layer == LayerMask.NameToLayer(DestructionLayer))
            {
                Destroy();
            }
        }
    }
}