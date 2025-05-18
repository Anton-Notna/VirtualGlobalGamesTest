using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class WinWindow : Window
    {
        [SerializeField]
        private TextMeshProUGUI _level;
        [SerializeField]
        private Button _close;

        private void Awake()
        {
            _close.onClick.RemoveAllListeners();
            _close.onClick.AddListener(Hide);
        }

        public void Show(int level) => _level.text = level.ToString();
    }
}