using DC.Countdown;
using Messages;
using UnityEngine;
using Utils;

namespace Core
{
    public class PongApp : Singleton<PongApp>
    {
        [SerializeField] private Countdown countdown;
        
        private int _leftPlayerScore, _rightPlayerScore;

        protected override void Awake()
        {
            base.Awake();
            
            Subscribe<StartSinglePlayerMessage>(OnStartSinglePlayer);

            countdown.CountdownComplete += OnCountdownComplete;
        }

        private void OnStartSinglePlayer(StartSinglePlayerMessage message) => countdown.BeginCountDown();

        private void OnCountdownComplete() => Locator.EventHub.Publish(new StartGameMessage());

        public void LogScore(PongSide side)
        {
            if (side == PongSide.Left)
            {
                _leftPlayerScore++;
            }
            else
            {
                _rightPlayerScore++;
            }
            
            Debug.Log($"Score has been changed! " +
                      $"Current Score: Left: {_leftPlayerScore} Right: {_rightPlayerScore}");
        }

        public int GetScore(PongSide side) => side == PongSide.Left ? _leftPlayerScore : _rightPlayerScore;
    }
}