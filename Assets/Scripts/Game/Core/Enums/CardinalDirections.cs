using UnityEngine;

namespace Game.Core.Enums
{
    public enum CardinalDirection
    {
        FORWARD,
        BACK,
        LEFT,
        RIGHT,
    }

    public static class CardinalDirectionExtensions
    {
        public static bool IsOppositeOf(this CardinalDirection dirOne, CardinalDirection dirTwo)
        {
            return (dirOne == CardinalDirection.BACK && dirTwo == CardinalDirection.FORWARD) ||
                   (dirOne == CardinalDirection.RIGHT && dirTwo == CardinalDirection.LEFT) ||
                   (dirOne == CardinalDirection.FORWARD && dirTwo == CardinalDirection.BACK) ||
                   (dirOne == CardinalDirection.LEFT && dirTwo == CardinalDirection.RIGHT);
        }

        public static CardinalDirection Reverse(this CardinalDirection dir)
        {
            return dir switch
            {
                CardinalDirection.FORWARD => CardinalDirection.BACK,
                CardinalDirection.BACK => CardinalDirection.FORWARD,
                CardinalDirection.RIGHT => CardinalDirection.LEFT,
                CardinalDirection.LEFT => CardinalDirection.RIGHT,
                _ => CardinalDirection.FORWARD
            };
        }

        public static Quaternion GetRotation(this CardinalDirection dir)
        {
            return dir switch
            {
                CardinalDirection.FORWARD => Quaternion.identity,
                CardinalDirection.BACK => Quaternion.Euler(0,180,0)  ,
                CardinalDirection.RIGHT => Quaternion.Euler(0,90,0) ,
                CardinalDirection.LEFT => Quaternion.Euler(0,-90,0) ,
                _ => Quaternion.identity 
            };
            
        }

        public static Vector2Int ToVector2Int(this CardinalDirection direction)
        {
            return direction switch
            {
                CardinalDirection.FORWARD => new Vector2Int(1, 0),
                CardinalDirection.BACK => new Vector2Int(-1, 0),
                CardinalDirection.LEFT => new Vector2Int(0, 1),
                CardinalDirection.RIGHT => new Vector2Int(0, -1),
                _ => Vector2Int.zero
            };
        }
    }
}