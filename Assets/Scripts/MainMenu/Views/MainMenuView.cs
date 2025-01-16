using Flamingo.GameState;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Flamingo.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [Inject] private IGameStateService _gameStateService;
        [SerializeField] private MainMenuBoardLoadButton _baseButton;

        void Start()
        {
            foreach (var level in _gameStateService.Levels)
            {
                CreateButton(level);
            }
        }
        private void CreateButton(LevelData level)
        {
            var newButton = Instantiate(_baseButton, _baseButton.transform.parent);
            newButton.LoadData(level.levelName, level.boardIcon);
            newButton._button.onClick.AddListener(() => { LoadLevel(level); });
            newButton.gameObject.SetActive(true);
        }
        private void LoadLevel(LevelData level)
        {
            _gameStateService.LoadLevel(level);
        }
    }
}
