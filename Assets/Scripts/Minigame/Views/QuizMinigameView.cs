using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.Minigame
{
    public class QuizMinigameView : MinigameView
    {
        public override void OnMinigameDispose()
        {
            Destroy(gameObject);
        }
        public override void Show()
        {
            gameObject.SetActive(true);
        }
        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void LoadData()
        {
            Debug.Log("Data Loaded!");
        }
    }
}
