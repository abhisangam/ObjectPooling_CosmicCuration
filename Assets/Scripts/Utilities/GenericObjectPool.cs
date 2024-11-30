
using CosmicCuration.Bullets;
using System.Collections.Generic;

namespace CosmicCuration.Utilities
{
    public abstract class GenericObjectPool<T> where T : class
    {
        private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        public T GetItem<U>() where U : T
        {
            PooledItem<T> pooledItem = pooledItems.Find((item) => !item.IsUsed && item is U);
            if (pooledItem != null)
            {
                pooledItem.IsUsed = true;
                return pooledItem.Item;
            }
            else
            {
                pooledItem = CreateNewPooledItem<U>();
                return pooledItem.Item;
            }
        }

        private PooledItem<T> CreateNewPooledItem<U>() where U : T
        {
            PooledItem<T> pooledItem = new PooledItem<T>();
            pooledItem.Item = CreateItem<U>();
            pooledItem.IsUsed = true;
            pooledItems.Add(pooledItem);
            return pooledItem;
        }

        protected abstract T CreateItem<U>() where U : T;

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