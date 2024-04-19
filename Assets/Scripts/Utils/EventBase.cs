using System;
using System.Collections.Generic;
using System.Linq;
using DC.MessageService;

namespace Utils
{
    public class EventBase : IEventBase, IDisposable
    {
        private readonly Dictionary<Type, TinyMessageSubscriptionToken> _tokens = new();

        public void Subscribe<T>(Action<T> action) where T : class, ITinyMessage
        {
            _tokens.Add(typeof(T), Locator.EventHub.Subscribe(action));
        }

        public void Unsubscribe<T>()
        {
            var type = typeof(T);
            
            foreach (var token in
                     _tokens.Where(token => token.Key == type))
            {
                Locator.EventHub.Unsubscribe(token.Value);
                _tokens.Remove(token.Key);

                break;
            }
        }

        public void Dispose()
        {
            if (_tokens.Count <= 0) return;
            
            foreach (var token in _tokens)
            {
                Locator.EventHub.Unsubscribe(token.Value);
            }
            _tokens.Clear();
        }
    }
}