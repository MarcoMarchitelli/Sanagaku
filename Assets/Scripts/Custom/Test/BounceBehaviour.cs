using UnityEngine;

/// <summary>
/// Tells the bullet that hits this object how to behave. Requires a collider.
/// </summary>
public class BounceBehaviour : MonoBehaviour {

    public enum Type { realistic =1, catchAndFire =2, goThrough=3, destroy=4}

    [Tooltip("1 = realistic, 2 = CNF, 3 = goThrough, 4 = destroy")] public Type BehaviourType;

    public void SetBehaviour(int typeIndex)
    {
        BehaviourType = (Type)typeIndex;
    }
}
