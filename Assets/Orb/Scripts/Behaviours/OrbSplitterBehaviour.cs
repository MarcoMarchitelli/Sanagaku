using UnityEngine;

namespace Sangaku
{
    public class OrbSplitterBehaviour : MonoBehaviour
    {
        [SerializeField] string poolTag = "SpawnedOrb";
        [SerializeField] bool randomize = true;

        [SerializeField] int orbsNumber = 2;
        [SerializeField] float angleOffset = 45f;

        [SerializeField] int minOrbsNumber = 2;
        [SerializeField] int maxOrbsNumber = 4;
        [SerializeField] float minAngle = 25f;
        [SerializeField] float maxAngle = 160f;

        [SerializeField] Vector3 forwardOffset = new Vector3(0f, 0f, 0.5f);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                SpawnOrbs();
        }

        public void SpawnOrbs(Collision _collision)
        {
            Vector3 pointToLook = _collision.contacts[0].normal;
            pointToLook.y = transform.position.y;
            transform.LookAt(pointToLook, Vector3.up);

            if (randomize)
            {
                int randomNumber = Random.Range(minOrbsNumber, maxOrbsNumber);
                float randomAngle = Random.Range(minAngle, maxAngle);
                float angleOffset = randomAngle / randomNumber;
                Spawn(randomNumber, angleOffset, randomAngle);
            }
            else
            {
                float totalAngle = angleOffset * orbsNumber - 1;
                Spawn(orbsNumber, angleOffset, totalAngle);
            }
        }

        public void SpawnOrbs()
        {
            if (randomize)
            {
                int randomNumber = Random.Range(minOrbsNumber, maxOrbsNumber);
                float randomAngle = Random.Range(minAngle, maxAngle);
                float angleOffset = randomAngle / randomNumber;
                Spawn(randomNumber, angleOffset, randomAngle);
            }
            else
            {
                float totalAngle = angleOffset * orbsNumber - 1;
                Spawn(orbsNumber, angleOffset, totalAngle);
            }
        }

        void Spawn(int _number, float _offsetAngle, float _totalAngle)
        {
            Vector3[] directions = CalculateDirections(_number, _offsetAngle, _totalAngle);

            IEntity spawnedOrb;
            for (int i = 0; i < directions.Length; i++)
            {
                spawnedOrb = ObjectSpawner.Instance.SpawnEntity(poolTag, true, transform.position + forwardOffset, transform.rotation);
                spawnedOrb.SetUpEntity();
                spawnedOrb.gameObject.transform.Rotate(directions[i]);
            }
        }

        Vector3[] CalculateDirections(int _number, float _offsetAngle, float _totalAngle)
        {
            Vector3[] directions = new Vector3[_number];

            float negativeLimit = _totalAngle / 2;
            for (int i = 0; i < directions.Length; i++)
                directions[i] = new Vector3(0f, -negativeLimit + (i * _offsetAngle), 0f);

            return directions;
        }
    }
}