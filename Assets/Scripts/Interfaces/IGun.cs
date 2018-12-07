using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun {
    
    float TimeBetweenShots { get; set; }
    int MagazineSize { get; set; }
    BaseProjectile ProjectileToShoot { get; set; }
    Transform ProjectileSpawnPoint { get; set; }
    void Shoot();
}
