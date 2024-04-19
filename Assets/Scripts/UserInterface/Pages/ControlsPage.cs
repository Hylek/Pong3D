using Messages;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Core;
using Utils;

namespace UserInterface.Pages
{
    public class ControlsPage : BasePage
    {
        [SerializeField] private Button backButton;
        
        protected override void Awake()
        {
            base.Awake();
            
            backButton.onClick.AddListener(OnBackButton);
            
            Subscribe<OpenControlsPageMessage>(message => Open());
        }

        private void OnBackButton()
        {
            Locator.EventHub.Publish(new OpenMainMenuMessage());
            Close();
        }
    }
}