﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile {

    float LifeTime { get; set; }
    int Damage { get; set; }
}