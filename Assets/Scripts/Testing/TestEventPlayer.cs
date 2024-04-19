using System;
using UnityEngine;
using Utils;

namespace Testing
{
    public class TestEventPlayer : MonoBehaviour
    {
        [SerializeField] private EventToken token;

        private void Start()
        {
            token.Subscribe(MyEventCallback);
            token.Execute();
            token.Unsubscribe(MyEventCallback);
            token.Execute();
        }

        private void MyEventCallback()
        {
            Debug.Log("My EventCallback Called!");
        }
    }
}