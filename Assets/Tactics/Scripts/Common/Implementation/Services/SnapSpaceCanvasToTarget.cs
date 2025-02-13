using UnityEngine;

namespace Tactics.Common.Implementation.Services
{
    public class SnapSpaceCanvasToTarget
    {
        private readonly Camera _camera;
        private readonly Transform _source;

        private Vector3? _currentTarget;

        public SnapSpaceCanvasToTarget(Transform source)
        {
            _source = source;
            _camera = Camera.main;
        }

        public void SnapToPoint(Vector3 targetPosition)
        {
            _currentTarget = targetPosition;
            FollowTargetPosition();
        }

        public void FollowTargetPosition()
        {
            if (_currentTarget.HasValue == false)
            {
                return;
            }

            var targetPosition = _camera.WorldToScreenPoint(_currentTarget.Value);
            _source.position = targetPosition;
        }
    }
}
