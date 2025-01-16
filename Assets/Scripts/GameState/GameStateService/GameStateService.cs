using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flamingo.GameState
{
    public class GameStateService : IGameStateService
    {
        public string BoardConfig => string.IsNullOrEmpty(_boardConfig) ? _defaultBoardConfig : _boardConfig;
        public LevelData[] Levels => _levels;

        private string _boardConfig = "";
        private string _defaultBoardConfig;
        private LevelData[] _levels;
        public GameStateService(TextAsset textAssetBoardConfig, LevelData[] levels)
        {
            _defaultBoardConfig = textAssetBoardConfig.text;
            _levels = levels;
        }
        public void LoadLevel(LevelData level)
        {
            _boardConfig = level.board.text;
            //TODO create a scene management service
            SceneManager.LoadScene(1);
        }
    }
}

