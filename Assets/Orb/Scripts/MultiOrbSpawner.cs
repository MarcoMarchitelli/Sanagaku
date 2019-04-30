using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Classe che si occupa di spawnare più palline in un range di direzioni
    /// </summary>
    public class MultiOrbSpawner : MonoBehaviour
    {
        /// <summary>
        /// Tag del pool da cui prendere gli oggetti
        /// </summary>
        [SerializeField] string poolTag = "SpawnedOrb";
        /// <summary>
        /// Se true la classe usa dei valori random nei range specificati.
        /// Altrimenti usa i valori specifici settati
        /// </summary>
        [SerializeField] bool randomize;

        /// <summary>
        /// Numero specifico di orb
        /// </summary>
        [SerializeField] int orbsNumber = 2;
        /// <summary>
        /// Offset di angolo
        /// </summary>
        [SerializeField] float angleOffset = 45f;

        /// <summary>
        /// Numero minimo di orb
        /// </summary>
        [SerializeField] int minOrbsNumber = 2;
        /// <summary>
        /// Numero massimo di orb
        /// </summary>
        [SerializeField] int maxOrbsNumber = 4;
        /// <summary>
        /// Angolo minimo da coprire
        /// </summary>
        [SerializeField] float minAngle = 25f;
        /// <summary>
        /// Angolo massimo da coprire
        /// </summary>
        [SerializeField] float maxAngle = 160f;
        /// <summary>
        /// Offset di spawn degli orb
        /// </summary>
        [SerializeField] float forwardOffset = 0.5f;

        /// <summary>
        /// Funzione che spawna gli orb in base ad una collisione
        /// </summary>
        /// <param name="_collision"></param>
        public void SpawnOrbs(Collision _collision)
        {
            Vector3 pointToLook = _collision.contacts[0].normal;
            pointToLook += transform.position;
            pointToLook.y = transform.position.y;
            transform.LookAt(pointToLook, Vector3.up);

            SpawnOrbs();
        }

        /// <summary>
        /// Funzione che spawna gli orb nella direzione in cui sta guardando l'oggetto
        /// </summary>
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

        /// <summary>
        /// Funzione di spawn degli orb
        /// </summary>
        /// <param name="_number"></param>
        /// <param name="_offsetAngle"></param>
        /// <param name="_totalAngle"></param>
        void Spawn(int _number, float _offsetAngle, float _totalAngle)
        {
            Vector3[] directions = CalculateDirections(_number, _offsetAngle, _totalAngle);

            IEntity spawnedOrb;
            for (int i = 0; i < directions.Length; i++)
            {
                spawnedOrb = ObjectSpawner.Instance.SpawnEntity(poolTag, true, transform.position + new Vector3(0f, 0f, forwardOffset), transform.rotation);
                spawnedOrb.SetUpEntity();
                spawnedOrb.gameObject.transform.Rotate(directions[i]);
            }
        }

        /// <summary>
        /// Funzione che calcola le direzioni degli orb
        /// </summary>
        /// <param name="_number"></param>
        /// <param name="_offsetAngle"></param>
        /// <param name="_totalAngle"></param>
        /// <returns></returns>
        Vector3[] CalculateDirections(int _number, float _offsetAngle, float _totalAngle)
        {
            Vector3[] directions = new Vector3[_number];

            float negativeLimit = _totalAngle / 2;
            for (int i = 0; i < directions.Length; i++)
                directions[i] = new Vector3(0f, -negativeLimit + (i * _offsetAngle), 0f);

            return directions;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, forwardOffset);
        }
    }
}