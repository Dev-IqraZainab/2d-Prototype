using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{
    // Serialized fields for inspector customization
    [Header("Prefab and UI Elements")]
    [SerializeField] private GameObject cardPrefab; // Prefab for the card object
    [SerializeField] private GameObject parent; // Parent object for organizing cards
    [SerializeField] private Sprite[] objectSprites; // Sprites used for the cards
    [SerializeField] private Sprite cardBackground; // Background sprite for the cards
    [SerializeField] private GameObject gameOverPanel; // Panel for displaying game over message

    // Lists to store card objects, random card values, and picked sprites
    [Header("Game Logic")]
    private List<Card> cards = new List<Card>(); // List of card objects
    private List<int> randomCardValue = new List<int>(); // List of random card values
    private List<Sprite> pickedSprites = new List<Sprite>(); // List of picked sprites

    // Variables for game logic
    private float timerValue; // Current value of the timer
    private int winCardCount = 0; // Number of matched card pairs
    private int moveCount = 0; // Number of moves made by the player
    private bool firstClick = false; // Flag for first card click
    private bool secondClick = false; // Flag for second card click
    private int firstCardIndex; // Index of the first clicked card
    private int secondCardIndex; // Index of the second clicked card
    private string firstMemorySpriteName; // Name of the sprite on the first clicked card
    private string secondMemorySpriteName; // Name of the sprite on the second clicked card
    private bool checkWon = false; // Flag for checking if the game is won
    public static int totalCards; // Total number of cards in the game



    [SerializeField] private TimeManager timerManager; // Reference to the TimerManager
    [SerializeField] private float maxTimerValue = 50f; // Maximum value for the timer
    [SerializeField] private TextMeshProUGUI timerUi; // UI element for displaying timer


    private void Start()
    {
        // Initialize the game
        InitializeCards();
        SoundManager.instance.PlayGameplayMusic();

        timerManager.InitializeTimer(maxTimerValue, timerUi, this);
        timerManager.StartTimer();
    }

    private void InitializeCards()
    {
        // Instantiate and organize card objects
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

    private void CollectSprites()
    {
        // Assign sprites to cards
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

    private void GenerateRandomNumbers()
    {
        // Generate random numbers for shuffling card values
        randomCardValue = randomCardValue.OrderBy(outValue => System.Guid.NewGuid()).ToList();
    }

    private void OnCardClick(Card card)
    {
        // Handle card click events
        int index = cards.IndexOf(card);
        if (!firstClick)
        {
            firstCardIndex = index;
            firstMemorySpriteName = pickedSprites[randomCardValue[index]].name;
            card.Flip(pickedSprites[randomCardValue[index]]);
            card.Disable();
            firstClick = true;
            SoundManager.instance.PlayCardFlipSound(); // Play card flip sound

        }
        else if (!secondClick)
        {
            secondCardIndex = index;
            secondMemorySpriteName = pickedSprites[randomCardValue[index]].name;
            card.Flip(pickedSprites[randomCardValue[index]]);
            card.Disable();
            secondClick = true;
            Invoke(nameof(Detect), 0.40f);
        }
        // Increment move count
        moveCount++;
    }

    private void Detect()
    {
        // Compare clicked card sprites and handle match/mismatch
        if (firstMemorySpriteName == secondMemorySpriteName)
        {
            winCardCount++;
            if (winCardCount == totalCards / 2)
            {
                checkWon = true;
                DisplayGameOver("Congratulations \n You Win The Game.");
            }
            else
            {
                SoundManager.instance.PlayMatchSound(); // Play match sound

            }
        }
        else
        {
            cards[firstCardIndex].Reset(cardBackground);
            cards[secondCardIndex].Reset(cardBackground);
            cards[firstCardIndex].Enable();
            cards[secondCardIndex].Enable();
            SoundManager.instance.PlayMismatchSound(); // Play mismatch sound

        }
        firstClick = false;
        secondClick = false;
    }

    private void Update()
    {
        
        if (!checkWon)
        {
            timerManager.UpdateTimer();
            ////timerValue -= Time.deltaTime;
            ////timerUi.text = "Time: " + Mathf.Max(timerValue, 0).ToString("0");
            //if (timerValue <= 0)
            //{
            //    DisplayGameOver("The Time is Over\nBetter Luck Next Time");
            //}
        }
    }

    public void DisplayGameOver(string message)
    {
        // Display game over message
        SoundManager.instance.StopMusic();
        SoundManager.instance.PlayLevelCompleteSound();
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = message;
    }

    public void RestartGame()
    {
        // Restart the game
        SceneManager.LoadScene(0);
    }
}
