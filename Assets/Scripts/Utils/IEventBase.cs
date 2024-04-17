using System;
using DC.MessageService;

namespace Utils
{
    public interface IEventBase
    {
        public void Subscribe<T>(Action<T> action) where T : class, ITinyMessage;
        public void Unsubscribe<T>();
    }
}