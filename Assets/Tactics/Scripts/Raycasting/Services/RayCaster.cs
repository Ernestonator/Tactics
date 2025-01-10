using UnityEngine;

namespace Tactics.Raycasting.Services
{
    public class RayCaster
    {
        private readonly Camera _camera = Camera.main;

        public bool TryCastRayFromCamera<T>(
            float distance,
            out T castResult,
            out Vector3 hitPoint, 
            out Vector3 hitNormal) where T : IInteractable
        {
            hitPoint = new Vector3();
            hitNormal = new Vector3();

            var mousePosition = Input.mousePosition;
            var ray = _camera.ScreenPointToRay(mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                hitPoint = hit.point;
                hitNormal = hit.normal;
                castResult = hit.collider.GetComponentInChildren<T>();
                return true;
            }

            castResult = default;
            return false;
        }
    }
}