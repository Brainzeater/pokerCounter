using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Windows
{
    public class ScoringWindow : MonoBehaviour
    {
        #region Inspector Variables

        [Header("Buttons")]
        [SerializeField] private Button lessButton;
        [SerializeField] private Button moreButton;
        [SerializeField] private Button confirmButton;

        [Header("Content")]
        [SerializeField] private TextMeshProUGUI trickCountText;

        #endregion

        public event Action<int> OnConfirmed;
        public int SumOfTricks { get; set; }
        public int NumOfCalculatedPlayers { get; set; }

        private Round _currentRound;
        private int _tricksCount;

        public void Initialize()
        {
            _currentRound = RoundController.Instance.CurrentRound;
            
            ResetScoringWindow();
            AttachListeners();
        }

        public void ResetScoringWindow()
        {
            if (NumOfCalculatedPlayers != GameController.NumberOfPlayers - 1 ||
                SumOfTricks == RoundController.Instance.CurrentRound.NumOfCardsInHand)
            {
                _tricksCount = 0;
                trickCountText.SetText("0");
            }
            
            OnLessButtonClick();
        }

        public void Close()
        {
            DetachListeners();
        }

        private void AttachListeners()
        {
            lessButton.onClick.AddListener(OnLessButtonClick);
            moreButton.onClick.AddListener(OnMoreButtonClick);
            confirmButton.onClick.AddListener(OnConfirmButtonClick);
        }

        private void DetachListeners()
        {
            lessButton.onClick.RemoveListener(OnLessButtonClick);
            moreButton.onClick.RemoveListener(OnMoreButtonClick);
            confirmButton.onClick.RemoveListener(OnConfirmButtonClick);
        }

        private void OnLessButtonClick()
        {
            TryUpdateTricksCount(false);
        }

        private void OnMoreButtonClick()
        {
            TryUpdateTricksCount(true);
        }

        private void TryUpdateTricksCount(bool plus)
        {
            var isLastPlayer = NumOfCalculatedPlayers == GameController.NumberOfPlayers - 1;
            var min = isLastPlayer ? _currentRound.NumOfCardsInHand - SumOfTricks : 0;
            _tricksCount = Mathf.Clamp(plus ? ++_tricksCount : --_tricksCount,
                min, _currentRound.NumOfCardsInHand - SumOfTricks);
            trickCountText.SetText(_tricksCount.ToString());
        }
        
        private void OnConfirmButtonClick()
        {
            OnConfirmed?.Invoke(_tricksCount);
        }

        private void OnDestroy()
        {
            DetachListeners();
        }
    }
}
