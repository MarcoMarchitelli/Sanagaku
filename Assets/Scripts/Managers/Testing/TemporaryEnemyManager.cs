using System.Collections.Generic;
using UnityEngine;
using Sangaku;
using System.Linq;

public class TemporaryEnemyManager : MonoBehaviour
{
    List<EnemyController> Enemies;

    public void SetUpEnemies()
    {
        Enemies = FindObjectsOfType<EnemyController>().ToList();

        foreach (EnemyController enemy in Enemies)
        {
            enemy.SetUpEntity();
        }
    }
}
