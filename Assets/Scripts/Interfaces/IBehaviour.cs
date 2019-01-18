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
    } 
}
