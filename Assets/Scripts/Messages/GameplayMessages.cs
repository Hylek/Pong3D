using Core;
using DC.MessageService;
using UnityEngine;

namespace Messages
{
    public class BallEnteredDeadZoneMessage : GenericTinyMessage<PongSide>
    {
        public BallEnteredDeadZoneMessage(object sender, PongSide content) : base(sender, content)
        {
            Debug.Log($"BallEnteredDeadZoneMessage Fired. Content: {content}");
        }
    }
    
    public class BallResetMessage : ITinyMessage
    {
        public object Sender { get; }
    }
}