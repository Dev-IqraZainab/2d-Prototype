
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.PlayMainMenuMusic();
    }
    public void PlayGame(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
