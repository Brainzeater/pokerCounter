using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Windows
{
    public class BettingWindow : MonoBehaviour
    {
        #region Inspector Variables

        [Header("Buttons")]
        [SerializeField] private Button lessButton;
        [SerializeField] private Button moreButton;
        [SerializeField] private Button isBlindButton;
        [SerializeField] private TextMeshProUGUI isBlindButtonText;
        [SerializeField] private Button confirmButton;

        [Header("Content")]
        [SerializeField] private TextMeshProUGUI betCountText;

        #endregion

        public event Action<int, bool> OnConfirmed;

        public int ForbiddenBet
        {
            get => _forbiddenBetCount;
            set
            {
                _forbiddenBetCount = value;
                OnLessButtonClick();
                if (value == 0)
                {
                    isBlindButton.interactable = true;
                }
            }
        }

        private int _forbiddenBetCount;

        private Round _currentRound;
        private bool _isCurrentRoundBlind;
        private bool _isBlind;
        private int _betCount;

        public void Initialize()
        {
            _currentRound = RoundController.Instance.CurrentRound;
            
            _isCurrentRoundBlind = _currentRound.TypeOfRound == Round.RoundType.Blind;
            ResetBettingWindow();
            _forbiddenBetCount = -1;
            AttachListeners();
        }

        public void ResetBettingWindow()
        {
            _betCount = 0;
            betCountText.SetText("0");
            OnIsBlindButtonClick();
            _isBlind = false;
            
        }

        private void AttachListeners()
        {
            lessButton.onClick.AddListener(OnLessButtonClick);
            moreButton.onClick.AddListener(OnMoreButtonClick);
            isBlindButton.onClick.AddListener(OnIsBlindButtonClick);
            confirmButton.onClick.AddListener(OnConfirmButtonClick);
        }

        private void DetachListeners()
        {
            lessButton.onClick.RemoveListener(OnLessButtonClick);
            moreButton.onClick.RemoveListener(OnMoreButtonClick);
            isBlindButton.onClick.RemoveListener(OnIsBlindButtonClick);
            confirmButton.onClick.RemoveListener(OnConfirmButtonClick);
        }

        private void OnLessButtonClick()
        {
            var minBet = 0;
            if (ForbiddenBet >= 0)
            {
                if (ForbiddenBet == 0)
                    minBet = 1;
                else if (_betCount - 1 == ForbiddenBet)
                    --_betCount;
            }
            
            _betCount = Mathf.Clamp(--_betCount, minBet, _currentRound.NumOfCardsInHand);
            // _betCount = Mathf.Clamp(--_betCount, minBet, 9);
            betCountText.SetText(_betCount.ToString());

            if (_betCount == 0)
            {
                OnIsBlindButtonClick();
            }
        }

        private void OnMoreButtonClick()
        {
            var turnOnIsBlindButton = _betCount == 0;
            
            var numOfCardsInHand = _currentRound.NumOfCardsInHand;
            // var numOfCardsInHand = 9;
            var maxBet = numOfCardsInHand;
            if (ForbiddenBet >= 0)
            {
                if (ForbiddenBet == numOfCardsInHand)
                    maxBet = numOfCardsInHand - 1;
                else if (_betCount - 1 == ForbiddenBet)
                    --_betCount;
            }
            _betCount = Mathf.Clamp(++_betCount, 0, maxBet);
            betCountText.SetText(_betCount.ToString());
            
            if (turnOnIsBlindButton && _betCount != 0)
            {
                isBlindButton.interactable = true;
            }
        }
        
        private void OnIsBlindButtonClick()
        {
            if (_betCount == 0 || _isCurrentRoundBlind)
            {
                isBlindButton.interactable = false;
                _isBlind = false;
            }
            else
            {
                isBlindButton.interactable = true;
                _isBlind = !_isBlind;
            }

            isBlindButtonText.SetText(_isBlind ? "*" : "");
        }

        private void OnConfirmButtonClick()
        {
            OnConfirmed?.Invoke(_betCount, _isBlind);
        }

        private void OnDestroy()
        {
            DetachListeners();
        }
    }
}