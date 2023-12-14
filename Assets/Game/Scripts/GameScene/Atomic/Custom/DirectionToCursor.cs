using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public enum SpaceType
    {
        Space_2D,
        Space_3D
    }

    public class DirectionToCursor
    {
        private Camera _camera;


        public void Init() => _camera = Camera.main;

        public Vector3 GetDirectionToCursor(Vector3 worldPos, SpaceType space)
        {
            Vector3 cursorPos = _camera.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                _camera.transform.position.y));

            Vector3 direction = cursorPos - worldPos;
            
            if (space == SpaceType.Space_2D)
            {
                direction.y = 0;
            }

            return direction.normalized;
        }
    }
}