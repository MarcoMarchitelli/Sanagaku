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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                SpawnOrbs();
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
                float totalAngle = angleOffset * orbsNumber;
                Spawn(orbsNumber, angleOffset, totalAngle);
            }
        }

        void Spawn(int _number, float _offsetAngle, float _totalAngle)
        {
            Vector3[] directions = CalculateDirections(_number, _offsetAngle, _totalAngle);
            IEntity spawnedOrb;
            for (int i = 0; i < directions.Length; i++)
            {
                spawnedOrb = ObjectSpawner.Instance.SpawnEntity(poolTag, true, transform.position, Quaternion.Euler(directions[i]));
                spawnedOrb.SetUpEntity();
                spawnedOrb.GetBehaviour<OrbMovementBehaviour>().SetEulerAngles(directions[i]);
            }
        }

        Vector3[] CalculateDirections(int _number, float _offsetAngle, float _totalAngle)
        {
            Vector3[] directions = new Vector3[_number];

            float halfAngle = _totalAngle / 2;
            Vector3 rotationVector = new Vector3(0f, halfAngle, 0f);
            directions[0] = Quaternion.Euler(rotationVector) * transform.forward;

            for (int i = 1; i < directions.Length; i++)
            {
                rotationVector = new Vector3(0f, -_offsetAngle, 0f);
                directions[i] = Quaternion.Euler(rotationVector) * directions[i - 1];
            }

            return directions;
        }
    }
}