using GameTemplate.UI.Core;
using GameTemplate.UI.Core.Buttons;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace GameTemplate.UI.Gameplay.PauseMenu.Views
{
    public class PauseMenuView : ViewWithBackButton
    {
        [SerializeField, Required] private UIButton _exitButton;

        public event Action ExitButtonClicked;

        protected override void OnEnable()
        {
            base.OnEnable();

            _exitButton.Clicked += OnExitButtonClick;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _exitButton.Clicked -= OnExitButtonClick;
        }

        private void OnExitButtonClick() =>
            ExitButtonClicked?.Invoke();
    }
}
