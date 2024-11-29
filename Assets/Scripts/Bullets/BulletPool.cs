using CosmicCuration.Bullets;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

public class BulletPool
{
    private BulletView bulletView;
    private BulletScriptableObject bulletScriptableObject;
    private List<PooledBullet> pooledBullets = new List<PooledBullet>();
    public BulletPool(BulletView bulletView, BulletScriptableObject bulletScriptableObject)
    {
        this.bulletView = bulletView;
        this.bulletScriptableObject = bulletScriptableObject;

    }

    public BulletController GetBullet()
    {
        PooledBullet pooledBullet = pooledBullets.Find((item) => !item.isUsed);
        if(pooledBullet != null)
        {
            pooledBullet.isUsed = true;
            return pooledBullet.bulletController;
        }
        else
        {
            pooledBullet = CreatePooledBullet();

            return pooledBullet.bulletController;
        }
    }

    private PooledBullet CreatePooledBullet()
    {
        PooledBullet pooledBullet = new PooledBullet();
        pooledBullet.bulletController = new BulletController(bulletView, bulletScriptableObject);
        pooledBullet.isUsed = true;
        pooledBullets.Add(pooledBullet);
        return pooledBullet;
    }

    public void ReturnBulletToPool(BulletController bullet)
    {
        PooledBullet pooledBullet = pooledBullets.Find((item) => item.bulletController.Equals(bullet));
        pooledBullet.isUsed = false;
    }

    private class PooledBullet
    {
        public BulletController bulletController;
        public bool isUsed;
    }
}