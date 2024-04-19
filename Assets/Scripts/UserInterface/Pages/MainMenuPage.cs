using System;
using DC.Countdown;
using Messages;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Core;
using Utils;

namespace UserInterface.Pages
{
    public class MainMenuPage : BasePage
    {
        [SerializeField]
        private Button singlePlayerButton, multiPlayerButton, controlsButton, settingsButton, quitButton;
        
        protected override void Awake()
        {
            base.Awake();
            
            singlePlayerButton.onClick.AddListener(OnSinglePlayerButton);
            multiPlayerButton.onClick.AddListener(OnMultiPlayerButton);
            controlsButton.onClick.AddListener(OnControlsButton);
            settingsButton.onClick.AddListener(OnSettingsButton);
            quitButton.onClick.AddListener(OnMultiPlayerButton);
            
            Subscribe<OpenMainMenuMessage>(message => Open());
        }

        private void Start() => Open();

        private void OnSinglePlayerButton()
        {
            Locator.EventHub.Publish(new StartSinglePlayerMessage());
            Close();
        }
        
        private void OnMultiPlayerButton()
        {
            Locator.EventHub.Publish(new StartMultiPlayerMessage());
            Close();
        }
        
        private void OnControlsButton()
        {
            Locator.EventHub.Publish(new OpenControlsPageMessage());
            Close();
        }
        
        private void OnSettingsButton()
        {
            Locator.EventHub.Publish(new OpenSettingsPageMessage());
            Close();
        }
        
        private void OnQuitButton() => Application.Quit();
    }
}