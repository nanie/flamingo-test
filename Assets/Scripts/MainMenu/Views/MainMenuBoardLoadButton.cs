using Flamingo.GameState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Flamingo.MainMenu
{
    public class MainMenuBoardLoadButton : MonoBehaviour
    {
        [SerializeField] internal Button _button;
        [SerializeField] private TextMeshProUGUI _levelName;
        [SerializeField] private Image _levelIcon;
        public void LoadData(string levelName, Sprite levelIcon)
        {
            _levelName.text = levelName;
            _levelIcon.sprite = levelIcon;
        }
    }
}
