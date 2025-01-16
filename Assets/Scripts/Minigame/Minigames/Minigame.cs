using Flamingo.GameLoop.Signals;
using Flamingo.ImageProvider;
using Flamingo.Localization;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Flamingo.Minigame
{
    public class Minigame
    {
        #region Data serialization
        public class Answer
        {
            public string ImageID { get; set; }
            public string Text { get; set; }
        }
        public class Root
        {
            public string ID { get; set; }
            public int QuestionType { get; set; }
            public string Question { get; set; }
            public string CustomImageID { get; set; }
            public List<Answer> Answers { get; set; }
            public int CorrectAnswerIndex { get; set; }
        }
        #endregion

        [Inject] private ImageLibraryProvider _imageLibrary;
        [Inject] private ILocalizationService _localizationService;
        private readonly SignalBus _signalBus;
        public int Score { get; set; }
        public int CorrectAnswerIndex => _minigameData.CorrectAnswerIndex;
        public event Action OnDispose;
        public event Action OnGameEnd;
        public event Action OnDataLoaded;
        public Root Data => _minigameData;
        private Root _minigameData;

        private const string MESSAGE_RIGHT = "rightAnswerMessage";
        private const string MESSAGE_WRONG = "wrongAnswerMessage";

        public Minigame(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        internal Sprite GetSpriteFromCode(string spriteId)
        {
            return _imageLibrary.GetSprite(spriteId);
        }

        public void EndGame()
        {
            _signalBus.Fire(new MinigameCompletedSignal { Score = Score });
            OnGameEnd?.Invoke();
        }
        public void AnswerQuestion(int choiceIndex)
        {
            Score = choiceIndex == _minigameData.CorrectAnswerIndex ? 1000 : 0;
            EndGame();
        }
        private void Reset()
        {
            Score = 0;
        }
        public class Pool : MemoryPool<Minigame>
        {
            protected override void Reinitialize(Minigame minigame)
            {
                minigame.Reset();
            }
        }

        internal string GetAnswer()
        {
            return _minigameData.Answers[CorrectAnswerIndex].Text ?? "";
        }

        internal void LoadData(string jsonData)
        {
            _minigameData = JsonConvert.DeserializeObject<Root>(jsonData);
            OnDataLoaded.Invoke();
        }

        internal string GetResultText(int answerIndex)
        {
            if (answerIndex == CorrectAnswerIndex)
            {
                return _localizationService.GetMessage(MESSAGE_RIGHT);
            }
            return _localizationService.GetMessage(MESSAGE_WRONG);
        }
    }
}
