namespace Sangaku
{
    /// <summary>
    /// Interfaccia che definisce un comportamento
    /// </summary>
    public interface IBehaviour
    {
        IEntity Entity { get; }

        /// <summary>
        /// Funzione che avvia una Behaviour
        /// </summary>
        void StartBehaviour();
        /// <summary>
        /// Funzione che ferma una Behaviour
        /// </summary>
        void StopBehaviour();
    } 
}
