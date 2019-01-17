using System.Collections.Generic;

namespace Sangaku
{
    /// <summary>
    /// Interfaccia che definisce il comportamento di un'entità
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Lista di Behaviour dell'entità
        /// </summary>
        List<IBehaviour> Behaviours { get; }
    }
}