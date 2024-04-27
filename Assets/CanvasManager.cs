using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardCollection collection;

    public GameData easyLevel;
    public GameData mediumLevel;
    public GameData HardLevel;

    GameData gameData;
    //List<CardController> cardController;
    public void Awake()
    {
        //cardController = new List<CardController>();
        GetGameDataDifficulty();
        SetCardGridLayoutParams();
        GenerateCards();

    }

    private void GenerateCards()
    {

        GameController.TotalCards = gameData.rows * gameData.columns;
        //for (int i = 0; i < GameController.TotalCards; i++)
        //{
        //    GameObject card = Instantiate(cardPrefab,
        //        this.transform);
        //    card.transform.name = "card(" + i.ToString() + ")";
        //    cardController.Add(card.GetComponent<CardController>());
        //}
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