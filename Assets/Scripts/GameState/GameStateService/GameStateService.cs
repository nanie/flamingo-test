using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Flamingo.GameState
{
    public class GameStateService : IGameStateService
    {
        public string BoardConfig => string.IsNullOrEmpty(_boardConfig) ? _defaultBoardConfig : _boardConfig;
        private string _boardConfig = "";
        private string _defaultBoardConfig;
        public GameStateService(TextAsset textAssetBoardConfig)
        {
            _defaultBoardConfig = textAssetBoardConfig.text;
        }
        public void LoadBoardConfig(TextAsset textAssetBoardConfig)
        {
            _boardConfig = textAssetBoardConfig.text;
        }
    }
}

