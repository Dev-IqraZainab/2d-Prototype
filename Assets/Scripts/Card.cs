using UnityEngine;
using UnityEngine.UI;

public interface ICard
{
    void Flip(Sprite sprite);
    void Reset(Sprite background);
    void Disable();
    void Enable();
}

// Implementation of the card behavior
public class Card : MonoBehaviour, ICard
{
    public Image cardImage;
    public Button cardButton;

    private void Awake()
    {
        cardImage = GetComponent<Image>();
        cardButton = GetComponent<Button>();
    }

    public void Flip(Sprite sprite)
    {
        cardImage.sprite = sprite;
    }

    public void Reset(Sprite background)
    {
        cardImage.sprite = background;
    }

    public void Disable()
    {
        cardButton.enabled = false;
    }
    public void Enable()
    {
        cardButton.enabled = true;
    }
}