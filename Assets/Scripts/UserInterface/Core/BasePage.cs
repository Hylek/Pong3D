using UnityEngine;
using Utils;

namespace UserInterface.Core
{
    public class BasePage : ExtendedBehaviour
    {
        protected GameObject Content;

        protected override void Awake()
        {
            base.Awake();

            // Content will always be the 1st and only child of a Page
            Content = transform.GetChild(0).gameObject;
        }

        protected virtual void Open() => Content.SetActive(true);

        protected virtual void Close() => Content.SetActive(false);
    }
}
