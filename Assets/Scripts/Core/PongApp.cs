using UnityEngine;
using Utils;

namespace Core
{
    public interface IPongApp
    {
        public void LogScore(PongSide side);
        public int GetScore(PongSide side);
    }
    
    public class PongApp : EventBase, IPongApp
    {
        private int _leftPlayerScore, _rightPlayerScore;
        
        public void LogScore(PongSide side)
        {
            Debug.Log($"Score has been changed! " +
                      $"Current Score: Left: {_leftPlayerScore} Right: {_rightPlayerScore}");
            
            if (side == PongSide.Left)
            {
                _leftPlayerScore++;
            }
            else
            {
                _rightPlayerScore++;
            }
        }

        public int GetScore(PongSide side) => side == PongSide.Left ? _leftPlayerScore : _rightPlayerScore;
    }
}