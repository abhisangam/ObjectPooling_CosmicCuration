using CosmicCuration.Enemy;
using System.Collections.Generic;

public class EnemyPool
{
    private EnemyView enemyView;
    private EnemyScriptableObject enemyScriptableObject;
    private List<PooledEnemy> pooledEnemies = new List<PooledEnemy>();
    public EnemyPool(EnemyView enemyView, EnemyScriptableObject enemyScriptableObject)
    {
        this.enemyView = enemyView;
        this.enemyScriptableObject = enemyScriptableObject;

    }

    public EnemyController GetEnemy()
    {
        PooledEnemy pooledEnemy = pooledEnemies.Find((item) => !item.isUsed);
        if (pooledEnemy != null)
        {
            pooledEnemy.isUsed = true;
            return pooledEnemy.enemyController;
        }
        else
        {
            pooledEnemy = CreatePooledEnemy();
            return pooledEnemy.enemyController;
        }
    }

    private PooledEnemy CreatePooledEnemy()
    {
        PooledEnemy pooledEnemy = new PooledEnemy();
        pooledEnemy.enemyController = new EnemyController(enemyView, enemyScriptableObject.enemyData);
        pooledEnemy.isUsed = true;
        pooledEnemies.Add(pooledEnemy);
        return pooledEnemy;
    }

    public void ReturnEnemyToPool(EnemyController enemy)
    {
        PooledEnemy pooledEnemy = pooledEnemies.Find((item) => item.enemyController.Equals(enemy));
        pooledEnemy.isUsed = false;
    }

    private class PooledEnemy
    {
        public EnemyController enemyController;
        public bool isUsed;
    }
}