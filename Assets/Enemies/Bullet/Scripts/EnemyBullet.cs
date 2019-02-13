
namespace Sangaku
{
    public class EnemyBullet : BaseEntity, IPoolable
    {
        public void OnGetFromPool()
        {
            SetUpEntity();
        }

        public void OnPoolCreation()
        {
            
        }

        public void OnPutInPool()
        {
            
        }
    }
}