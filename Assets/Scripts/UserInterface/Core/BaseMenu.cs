using UnityEngine;
using Utils;

namespace UserInterface.Core
{
    public class BaseMenu : ExtendedBehaviour
    {
        protected GameObject Content;

        protected override void Awake()
        {
            base.Awake();

            // Content will always be the 1st and only child of a Page
            Content = transform.GetChild(0).gameObject;
        }
    }
}