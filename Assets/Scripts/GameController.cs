using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{
    [Header("Prefab and UI Elements")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private Sprite[] objectSprites;
    [SerializeField] private Sprite cardBackground;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Game Logic")]
    private List<Card> cards = new List<Card>();
    private List<int> randomCardValue = new List<int>();
    private List<Sprite> pickedSprites = new List<Sprite>();

    private int winCardCount = 0;
    private int moveCount = 0;
    private bool firstClick = false;
    private bool secondClick = false;
    private int firstCardIndex;
    private int secondCardIndex;
    private string firstMemorySpriteName;
    private string secondMemorySpriteName;
    private bool checkWon = false;
    public static int totalCards;
    private int matchedPairsCount = 0;

    [SerializeField] private TimeManager timerManager;
    [SerializeField] private float maxTimerValue = 50f;
    [SerializeField] private TextMeshProUGUI timerUi;
    [SerializeField] private TextMeshProUGUI moveUi;
    [SerializeField] private TextMeshProUGUI pairUi;

    private void Start()
    {
        InitializeGame();
    }

    // Initialize the game
    private void InitializeGame()
    {
        InitializeManagers();
        InitializeUI();
        InitializeCards();
        InitializeCounts();
        SoundManager.instance.PlayGameplayMusic();
        timerManager.StartTimer();
    }

    // Initialize managers, such as the timer manager
    private void InitializeManagers()
    {
        timerManager.InitializeTimer(maxTimerValue, timerUi, this);
    }

    // Initialize UI elements
    private void InitializeUI()
    {
        moveUi.text = moveCount.ToString();
        pairUi.text = matchedPairsCount.ToString();
    }

    // Initialize card objects
    private void InitializeCards()
    {
        for (int i = 0; i < totalCards; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, parent.transform);
            Card card = cardObject.GetComponent<Card>();
            cards.Add(card);
            cardObject.name = i.ToString();
            cardObject.GetComponent<Button>().onClick.AddListener(() => OnCardClick(card));
            randomCardValue.Add(i);
        }
        CollectSprites();
    }

    // Collect sprites to be used on cards
    private void CollectSprites()
    {
        int index = 0;
        for (int i = 0; i < totalCards; i++)
        {
            if (i == totalCards / 2)
            {
                index = 0;
            }
            pickedSprites.Add(objectSprites[index]);
            index++;
        }
        GenerateRandomNumbers();
    }

    // Generate random numbers for shuffling card values
    private void GenerateRandomNumbers()
    {
        randomCardValue = randomCardValue.OrderBy(outValue => System.Guid.NewGuid()).ToList();
    }

    // Handle card click event
    private void OnCardClick(Card card)
    {
        int index = cards.IndexOf(card);
        if (!firstClick)
        {
            HandleFirstClick(index, card);
        }
        else if (!secondClick)
        {
            HandleSecondClick(index, card);
        }
        moveCount++;
        moveUi.text = moveCount.ToString();
    }

    // Handle first card click
    private void HandleFirstClick(int index, Card card)
    {
        firstCardIndex = index;
        firstMemorySpriteName = pickedSprites[randomCardValue[index]].name;
        card.Flip(pickedSprites[randomCardValue[index]]);
        card.Disable();
        firstClick = true;
        SoundManager.instance.PlayCardFlipSound();
    }

    // Handle second card click
    private void HandleSecondClick(int index, Card card)
    {
        secondCardIndex = index;
        secondMemorySpriteName = pickedSprites[randomCardValue[index]].name;
        card.Flip(pickedSprites[randomCardValue[index]]);
        card.Disable();
        secondClick = true;
        Invoke(nameof(Detect), 0.40f);
    }

    // Detect if the clicked cards match
    private void Detect()
    {
        if (firstMemorySpriteName == secondMemorySpriteName)
        {
            HandleMatch();
        }
        else
        {
            HandleMismatch();
        }
        firstClick = false;
        secondClick = false;
    }

    // Handle matching cards
    private void HandleMatch()
    {
        winCardCount++;
        matchedPairsCount++;
        pairUi.text = matchedPairsCount.ToString();
        if (winCardCount == totalCards / 2)
        {
            checkWon = true;
            DisplayGameOver("Congratulations \n You Win The Game.");
        }
        else
        {
            SoundManager.instance.PlayMatchSound();
        }
    }

    // Handle mismatched cards
    private void HandleMismatch()
    {
        cards[firstCardIndex].Reset(cardBackground);
        cards[secondCardIndex].Reset(cardBackground);
        cards[firstCardIndex].Enable();
        cards[secondCardIndex].Enable();
        SoundManager.instance.PlayMismatchSound();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!checkWon)
        {
            timerManager.UpdateTimer();
        }
    }

    // Display game over message
    public void DisplayGameOver(string message)
    {
        SoundManager.instance.StopMusic();
        SoundManager.instance.PlayLevelCompleteSound();
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = message;
    }

    // Restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    // Initialize move count and matched pairs count
    private void InitializeCounts()
    {
        moveCount = 0;
        matchedPairsCount = 0;
    }
}
