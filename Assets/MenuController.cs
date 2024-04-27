
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
