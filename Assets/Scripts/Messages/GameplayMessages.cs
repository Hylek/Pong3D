using DC.MessageService;
using Gameplay;
using UnityEngine;

namespace Messages
{
    public class BallEnteredDeadZoneMessage : GenericTinyMessage<DeadZoneSide>
    {
        public BallEnteredDeadZoneMessage(object sender, DeadZoneSide content) : base(sender, content)
        {
            Debug.Log($"BallEnteredDeadZoneMessage Fired. Content: {content}");
        }
    }
}