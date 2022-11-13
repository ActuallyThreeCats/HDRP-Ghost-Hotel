using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="FurnitureObject", menuName ="Crafty/Furniture")]
public class FurnitureScriptableObject : ScriptableObject
{
    public static Dir GetNextDirClockwise(Dir dir)
    {
 
            switch (dir)
            {
                case Dir.Up:
                    return Dir.Right;
                case Dir.Down:
                    return Dir.Left;
                case Dir.Left:
                    return Dir.Up;
                case Dir.Right:
                    return Dir.Down;
                default:
                    return Dir.Down;
            }

    }
    public static Dir GetNextDirCounterClockwise(Dir dir)
    {

            switch (dir)
            {
                case Dir.Up:
                    return Dir.Left;
                case Dir.Down:
                    return Dir.Right;
                case Dir.Left:
                    return Dir.Down;
                case Dir.Right:
                    return Dir.Up;
                default:
                    return Dir.Down;
            }

    }

    public enum Dir
    {
        Up,
        Down,
        Left,
        Right
    }

    public string nameString;
    public Transform prefab;
    public Transform visual;
    public int width;
    public int height;
    public Dir dir;

    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            case Dir.Up:
                return 180;
               
            case Dir.Down:
                return 0;

            case Dir.Left:
                return 90;

            case Dir.Right:
                return 270;

            default:
                return 0;
        }
    }

    public Vector2Int GetRotationOffset(Dir dir)
    {
        switch (dir)
        {
            case Dir.Up:
                return new Vector2Int(width,height);
            case Dir.Down:
                return new Vector2Int(0, 0);
            case Dir.Left:
                return new Vector2Int(0, width);
            case Dir.Right:
                return new Vector2Int(height, 0);
            default:
                return new Vector2Int(0, 0);
        }
    }

    public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
    {

        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        switch (dir)
        {
            case Dir.Up:
            case Dir.Down:
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
            case Dir.Left:
            case Dir.Right:
                for (int x = 0; x < height; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
            default:
                break;
        }




        return gridPositionList;
    }
}
