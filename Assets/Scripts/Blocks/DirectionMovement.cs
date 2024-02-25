using UnityEngine;

namespace Play.Block
{
    public enum WindDirection
    {
        Up = 0,
        Down = 1,
        Right = 2,
        Left = 3,
        None
    }

    [System.Serializable]
    public class DirectionMovement
    {
        [SerializeField] private WindDirection directionType;

        public Vector2 CheckDirectionWind()
        {
            switch (directionType)
            {
                case WindDirection.Up:
                    return Vector2.up;
                case WindDirection.Down:
                    return Vector2.down;
                case WindDirection.Right:
                    return Vector2.right;
                case WindDirection.Left:
                    return Vector2.left;
                case WindDirection.None:
                    return Vector2.zero;
            }
            return Vector2.zero;
        }
    }
}