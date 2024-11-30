using System.Collections;
using UnityEngine;
using CosmicCuration.Utilities;

namespace CosmicCuration.VFX
{
    public class VFXPool : GenericObjectPool<VFXController>
    {
        VFXView vfxPrefab;

        public VFXController GetVFXItem(VFXView vfxPrefab)
        {
            this.vfxPrefab = vfxPrefab;
            return GetItem<VFXController>();
        }

        protected override VFXController CreateItem<T>()
        {
            return new VFXController(this.vfxPrefab);
        }
    }
}