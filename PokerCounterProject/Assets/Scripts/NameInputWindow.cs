using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Button submit;
    [SerializeField] private TMP_InputField input;

    public event Action<string> OnNameSubmitted;

    private void Start()
    {
        submit.onClick.AddListener(OnSubmitClick);
    }

    public void SetPlayerIndex(int index)
    {
        var indexString = "";
        switch (index)
        {
            case 0:
                indexString = "first";
                break;
            case 1:
                indexString = "second";
                break;
            case 2:
                indexString = "third";
                break;
            case 3:
                indexString = "fourth";
                break;
        }
        
        label.SetText($"{indexString} player");
    }

    private void OnSubmitClick()
    {
        OnNameSubmitted?.Invoke(input.text);
        input.text = "";
    }
}
