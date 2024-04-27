
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameData easyLevel;
    public GameData mediumLevel;
    public GameData HardLevel;

    GameData gameData;
    public void Awake()
    {
        GetGameDataDifficulty();
        SetCardGridLayoutParams();
        GenerateCards();

    }

    private void GenerateCards()
    {

        GameController.totalCards = gameData.rows * gameData.columns;
      
    }

    private void SetCardGridLayoutParams()
    {
        CardGridLayout cardGridLayout =
            this.GetComponent<CardGridLayout>();
        cardGridLayout.preferredPadding = gameData.prefferedTopBottomPadding;
        cardGridLayout.rows = gameData.rows;
        cardGridLayout.columns = gameData.columns;
        cardGridLayout.spacing = gameData.spacing;

    }

    void GetGameDataDifficulty()
    {
        Difficulty difficulty =
            (Difficulty)PlayerPrefs.GetInt
            (
            "Difficulty",
            (int)Difficulty.Medium
            );

        switch (difficulty)
        {
            case Difficulty.Easy:
                gameData = easyLevel;
                break;

            case Difficulty.Medium:
                gameData = mediumLevel;
                break;

            case Difficulty.Hard:
                gameData = HardLevel;
                break;
        }
    }
}
