using Core;
using Messages;
using TMPro;
using UnityEngine;
using Utils;

namespace UserInterface
{
    public class ScoreLabel : ExtendedBehaviour
    {
        [SerializeField] private PongSide scoreSide;
        private TMP_Text _text;
        
        protected override void Awake()
        {
            base.Awake();

            _text = GetComponent<TMP_Text>();
            
            Subscribe<BallEnteredDeadZoneMessage>(OnBallDead);
        }

        private void OnBallDead(BallEnteredDeadZoneMessage content)
        {
            if (content.Content == scoreSide) return;
            
            var side = content.Content;
            PongApp.Instance.LogScore(side);

            var stringNumber = AddLeadingZeroIfSingleDigit(PongApp.Instance.GetScore(side));
            _text.SetText(stringNumber);
        }
        
        private static string AddLeadingZeroIfSingleDigit(int number)
        {
            return number is >= 0 and <= 9 ? $"0{number}" : number.ToString();
        }

    }
}