
using CosmicCuration.Bullets;
using System.Collections.Generic;

namespace CosmicCuration.Utilities
{
    public abstract class GenericObjectPool<T> where T : class
    {
        private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        public T GetItem()
        {
            PooledItem<T> pooledItem = pooledItems.Find((item) => !item.IsUsed);
            if (pooledItem != null)
            {
                pooledItem.IsUsed = true;
                return pooledItem.Item;
            }
            else
            {
                pooledItem = CreateNewPooledItem();
                return pooledItem.Item;
            }
        }

        private PooledItem<T> CreateNewPooledItem()
        {
            PooledItem<T> pooledItem = new PooledItem<T>();
            pooledItem.Item = CreateItem();
            pooledItem.IsUsed = true;
            pooledItems.Add(pooledItem);
            return pooledItem;
        }

        protected abstract T CreateItem();

        public void ReturnItem(T item)
        {
            PooledItem<T> pooledItem = pooledItems.Find((i) => i.Item.Equals(item));
            pooledItem.IsUsed = false;
        }

        private class PooledItem<T>
        {
            public T Item;
            public bool IsUsed;
        }
    }
}