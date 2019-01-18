namespace Sangaku
{
    /// <summary>
    /// Interfaccia che definisce un comportamento
    /// </summary>
    public interface IBehaviour
    {
        /// <summary>
        /// Entità che controlla il Behaviour
        /// </summary>
        IEntity Entity { get; }
        /// <summary>
        /// True se il Behaviour è stato setuppato, false altrimenti
        /// </summary>
        bool IsSetupped { get; }

        /// <summary>
        /// Setup del Behaviour
        /// </summary>
        /// <param name="_entity">Riferimento all'entitàche gestisce il Behaviour</param>
        void Setup(IEntity _entity);
    } 
}
