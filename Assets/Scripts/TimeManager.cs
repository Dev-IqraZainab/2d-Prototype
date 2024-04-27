using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float timerValue; // Current value of the timer
    private bool timerRunning = false; // Flag to indicate if the timer is running
    private TextMeshProUGUI timerUi; // Reference to the UI element for displaying timer
    private GameController gameController; // Reference to the GameController

    public void InitializeTimer(float maxValue, TextMeshProUGUI uiElement, GameController controller)
    {
        timerValue = maxValue;
        timerUi = uiElement;
        gameController = controller;
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void UpdateTimer()
    {
        if (timerRunning)
        {
            timerValue -= Time.deltaTime;
            timerUi.text = "Time: " + Mathf.Max(timerValue, 0).ToString("0");
            if (timerValue <= 0)
            {
                gameController.DisplayGameOver("The Time is Over\nBetter Luck Next Time");
                StopTimer(); // Stop the timer when it reaches zero
            }
        }
    }

    public void ResetTimer()
    {
        timerValue = 0f;
        timerRunning = false;
    }
}

