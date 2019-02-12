using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sangaku
{
    /// <summary>
    /// Evento di unity che porta un float
    /// </summary>
    [System.Serializable]
    public class UnityFloatEvent : UnityEvent<float> { }

    /// <summary>
    /// Unity Event passing an int
    /// </summary>
    [System.Serializable]
    public class UnityIntEvent : UnityEvent<int> { }

    /// <summary>
    /// Evento di unity che porta un Vector2
    /// </summary>
    [System.Serializable]
    public class UnityVector2Event : UnityEvent<Vector2> { }

    /// <summary>
    /// Evento di unity che porta un Vector3
    /// </summary>
    [System.Serializable]
    public class UnityVector3Event : UnityEvent<Vector3> { }

    /// <summary>
    /// Unity Event passing a Transform
    /// </summary>
    [System.Serializable]
    public class UnityTransformEvent : UnityEvent<Transform> { }

    /// <summary>
    /// Unity Event passing a DamageReceiverBehaviour
    /// </summary>
    [System.Serializable]
    public class UnityDamageReceiverEvent : UnityEvent<DamageReceiverBehaviour> { }

    /// <summary>
    /// Unity Event passing a SubObjectiveBehaviour
    /// </summary>
    [System.Serializable]
    public class UnitySubObjectiveEvent : UnityEvent<SubObjectiveBehaviour> { }

    /// <summary>
    /// Unity Event passing an OrbController
    /// </summary>
    [System.Serializable]
    public class UnityOrbControllerEvent : UnityEvent<OrbController> { }

    /// <summary>
    /// Evento di unity void
    /// </summary>
    [System.Serializable]
    public class UnityVoidEvent : UnityEvent { }
}