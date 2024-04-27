using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Card", menuName ="Card Menu/Create Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public string pairName;
    public Sprite cardImage;
}
