using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utils
{
    [CreateAssetMenu(menuName = "DC Tools/EventToken", fileName = "EventToken")]
    public class EventToken : ScriptableObject
    {
        public string eventName;

        private event Action Event;

        public void Subscribe(Action method) => Event += method;

        public void Unsubscribe(Action method) => Event -= method;

        public void Execute() => Event?.Invoke();
    }
}