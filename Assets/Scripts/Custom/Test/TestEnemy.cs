using UnityEngine;
using UnityEngine.Events;

public class TestEnemy : MonoBehaviour
{
    public enum DeathBeahviour { diesFromBounces, diesFromDamage};

    //references
    public GameObject deathParticles;

    //behaviours
    public DeathBeahviour deathBehaviour;
    public bool dealsDamageOnContact = true;

    //parameters
    [Range(1,3)]
    public int BouncesNeededToDie;
    public int health;
    public int damage = 1;

    //events
    public UnityEvent OnDeath;
    
    Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        if(deathBehaviour == DeathBeahviour.diesFromBounces)
            material.color = ColorContainer.Instance.Colors[BouncesNeededToDie - 1];
    }

    public void Die()
    {
        if (deathParticles)
        {
            GameObject instantiatedParticles = Instantiate(deathParticles, transform.position, Random.rotation);
            Destroy(instantiatedParticles, 2.5f);
        }
        OnDeath.Invoke();
        Counters.instance.UpdateKills(1);
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }
}
