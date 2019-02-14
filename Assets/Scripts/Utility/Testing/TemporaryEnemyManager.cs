using System.Collections.Generic;
using UnityEngine;
using Sangaku;

public class TemporaryEnemyManager : MonoBehaviour
{
    public List<EnemyController> Enemies;


    public void SetUpEnemies()
    {
        foreach (EnemyController enemy in Enemies)
        {
            enemy.SetUpEntity();
        }
    }
}
