using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public static class LayerManager
    {
        #region Layers
        public static int PlayerLayer = 6;
        public static int FoodLayer = 7;
        public static int InteractableLayer = 8;
        #endregion

        #region Layer Masks

        public static int PlayerMask = 1 << PlayerLayer;
        public static int InteractableMask => 1 << InteractableLayer;
        public static int EnemyProjectileMask = 1 << FoodLayer;
        //public static int PlayerMask = DoorMask | WallMask;
        #endregion
        
        
        public static bool IsInLayerMask(int layerWantToCheck, LayerMask layerMask)
        {
            if (((1 << layerWantToCheck) & layerMask) != 0)
            {
                return true;
            }
            return false;
        }
    }

}
