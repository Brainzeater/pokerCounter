using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    public Image Image => image;
    
    private Card _card;
    public Player Holder;

    public void Initialize(Card card)
    {
        _card = card;
        button.onClick.AddListener(OnCardButtonClicked);
    }

    private void OnCardButtonClicked()
    {
        Debug.LogError(Holder != null ? $"{Holder}'s {_card}" : $"{_card}");
    }
    
    // TODO: Remove listener
    // TODO: Lock/Unlock Interaction
}
