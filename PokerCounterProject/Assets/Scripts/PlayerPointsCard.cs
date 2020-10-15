using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointsCard : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI betCountText;
    [SerializeField] private GameObject betIsBlind;
    
    [Header("Background")]
    [SerializeField] private Image topBackground;
    [SerializeField] private Image bottomBackground;

    private Color _regularColor;
    private Color _highlightedColor;

    public string Name
    {
        set => nameText.SetText(value);
    }

    public int Points
    {
        set => pointsText.SetText(value.ToString());
    }

    public int BetCount
    {
        set
        {
            if (value == 0)
            {
                betCountText.SetText("-");
                betCountText.alignment = TextAlignmentOptions.Center;
            }
            else
            {
                betCountText.SetText(value.ToString());
                betCountText.alignment = TextAlignmentOptions.Midline;
            }
        }
    }

    public bool BetIsBlind
    {
        set => betIsBlind.SetActive(value);
    }

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#8F0A00", out _regularColor);
        ColorUtility.TryParseHtmlString("#19E666", out _highlightedColor);
    }

    public void HighlightPanel()
    {
        topBackground.color = _highlightedColor;
        bottomBackground.color = _highlightedColor;
    }

    public void UnhighlightPanel()
    {
        topBackground.color = _regularColor;
        bottomBackground.color = _regularColor;
    }

    public void ClearBetContent()
    {
        betCountText.SetText("");
        BetIsBlind = false;
    }
}