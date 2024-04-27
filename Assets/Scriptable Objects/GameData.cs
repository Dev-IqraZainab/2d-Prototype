
using UnityEngine;

[CreateAssetMenu(fileName ="GameDatas", menuName ="Game/Datas")]
public class GameData : ScriptableObject
{
    [Header("Game Difficulty Settings")]
    public Difficulty difficulty;
    public int rows;
    public int columns;
   
    [Header("Card Background Image")]
    public Sprite background;

    [Header("Grid Layout")]
    public int prefferedTopBottomPadding;
    public Vector2 spacing;


}
