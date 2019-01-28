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
    /// Evento di unity void
    /// </summary>
    [System.Serializable]
    public class UnityVoidEvent : UnityEvent { }
}