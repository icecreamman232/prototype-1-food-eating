using UnityEngine;

namespace JustGame.Script.Helper
{
    public static class DebugHelper
    {
        public static RaycastHit2D RayCast(Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance, LayerMask mask, Color color,bool drawGizmo=false)
        {	
            if (drawGizmo) 
            {
                Debug.DrawRay (rayOriginPoint, rayDirection * rayDistance, color);
            }
            return Physics2D.Raycast(rayOriginPoint,rayDirection,rayDistance,mask);		
        } 
    }
}

