
namespace Sangaku
{
    /// <summary>
    /// Entità che identifica un particle come ogetto nel mondo
    /// </summary>
    public class ParticleController : BaseEntity, IPoolable
    {
        #region IPoolable
        public void OnGetFromPool()
        {
            SetUpEntity();
        }

        public void OnPoolCreation() { }

        public void OnPutInPool() { }
        #endregion
    }
}