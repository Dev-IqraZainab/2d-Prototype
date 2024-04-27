using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject CardPrefab;
    public GameObject parent;
    int TotalCards = 12;
    public Sprite[] ObjectSprite;
    public List<Sprite> PickedSprite = new List<Sprite>();
    bool FirstClick = false;
    bool SecondClick = false;

    public string FirstMemorySpriteName;
    public string SecondMemorySpriteName;

    public Sprite cardBackground;
    int FirstCardPositionValue,secondCardPositionValue;

    int winCardCount=0;

    public TextMeshProUGUI TimerUi;
    float timerValue=50;
    public List<int> RandomCardValue = new List<int>();
    public GameObject GameOverPanel;
    public bool checkWon=false;
    void Start()
    {
        for(int i=0;i< TotalCards; i++)
        {
            GameObject cardInstance = Instantiate(CardPrefab);
            cardInstance.transform.SetParent(parent.transform);
            cardInstance.transform.gameObject.name = i.ToString();
            ButtonListener(cardInstance);
            RandomCardValue.Add(i);
        }
        CellectingSpriteToCell();
    }

    public void CellectingSpriteToCell()
    {
        int index = 0;
        for (int i=0;i < TotalCards; i++)
        {
            if(i==TotalCards/2)
            {
                index = 0;
            }
            PickedSprite.Add(ObjectSprite[index]);
            index++;
        }
        RandomNumberGeneratator();
    }
    //Random Number Generator
    public void RandomNumberGeneratator()
    {
        RandomCardValue = RandomCardValue.OrderBy(outValue => System.Guid.NewGuid()).ToList();
    }
    void ButtonListener(GameObject cardInstance)
    {
        cardInstance.GetComponent<Button>().onClick.AddListener(DeleteClick);
    }
    void DeleteClick()
    {
        Debug.Log("Click Done "+
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        //FirstCardPositionValue = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        if(!FirstClick)
        {
            FirstCardPositionValue = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            FirstClick = true;
            parent.transform.GetChild(FirstCardPositionValue).GetComponent<Image>().sprite = PickedSprite[RandomCardValue[FirstCardPositionValue]];
            FirstMemorySpriteName = parent.transform.GetChild(FirstCardPositionValue).GetComponent<Image>().sprite.name;
            parent.transform.GetChild(FirstCardPositionValue).GetComponent<Button>().enabled=false;

        }
        else
        if(!SecondClick)
        {
            secondCardPositionValue = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            SecondClick = true;
            parent.transform.GetChild(secondCardPositionValue).GetComponent<Image>().sprite = PickedSprite[RandomCardValue[secondCardPositionValue]];
            SecondMemorySpriteName = parent.transform.GetChild(secondCardPositionValue).GetComponent<Image>().sprite.name;
            parent.transform.GetChild(secondCardPositionValue).GetComponent<Button>().enabled = false;
            Invoke(nameof(Detect), 0.40f);
        }
    }
    // Update is called once per frame
    void Detect()
    {
        if (FirstMemorySpriteName == SecondMemorySpriteName)
        {
            FirstClick = false;
            SecondClick = false;
            Debug.Log("Match");
            winCardCount++;
            if(winCardCount==TotalCards/2)
            {
                checkWon = true;
                GameOverPanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Congratulations \n You Win The Game.";
                GameOverPanel.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Not Match");
            FirstClick = false;
            SecondClick = false;
            parent.transform.GetChild(FirstCardPositionValue).GetComponent<Image>().sprite = cardBackground;
            parent.transform.GetChild(secondCardPositionValue).GetComponent<Image>().sprite = cardBackground;
            parent.transform.GetChild(FirstCardPositionValue).GetComponent<Button>().enabled = true;
            parent.transform.GetChild(secondCardPositionValue).GetComponent<Button>().enabled = true;

        }
    }
    private void Update()
    {
        if (!checkWon) 
        {
            timerValue = timerValue - Time.deltaTime;
            TimerUi.text = "Time: " + timerValue.ToString("0");
            if (timerValue < 0)
            {
                TimerUi.text = "Time: 0";

                GameOverPanel.SetActive(true);
                GameOverPanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "The Time is Over\nBetter Luck Next Time ";
            }
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
