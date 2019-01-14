using UnityEngine;

/// <summary>
/// Tells the bullet that hits this object how to behave. Requires a collider.
/// </summary>
public class BounceBehaviour : MonoBehaviour {

    public enum Type { realistic, catchAndFire, goThrough, destroy}

    public Type BehaviourType;
}
