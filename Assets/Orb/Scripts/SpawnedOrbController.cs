namespace Sangaku
{
    public class SpawnedOrbController : BaseEntity, IPoolable
    {
        #region IPoolable
        public void OnGetFromPool() { }
        public void OnPoolCreation() { }
        public void OnPutInPool()
        {
            Enable(false);
        }
        #endregion
    }
}
