using CosmicCuration.Utilities;
using System.Collections;
using UnityEngine;

namespace CosmicCuration.PowerUps
{
    public class PowerupPool : GenericObjectPool<PowerUpController>
    {
        private PowerUpData powerUpData;

        public PowerUpController GetPowerUp<T>(PowerUpData powerUpData) where T : PowerUpController
        {
            this.powerUpData = powerUpData;
            return GetItem<T>();
        }
        protected override PowerUpController CreateItem<T>()
        {
            if(typeof(T) == typeof(Shield))
            {
                return new Shield(powerUpData);
            }
            else if (typeof(T) == typeof(RapidFire))
            {
                return new RapidFire(powerUpData);
            }
            else if (typeof(T) == typeof(DoubleTurret))
            {
                return new DoubleTurret(powerUpData);
            }
            else
            {
                throw new System.NotSupportedException("Powerup type not supported");
            }
        }
    }
}