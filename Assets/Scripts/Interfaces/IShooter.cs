using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooter {

    BaseGun EquippedGun { get; set; }
    void EquipGun(BaseGun gunToEquip);
}