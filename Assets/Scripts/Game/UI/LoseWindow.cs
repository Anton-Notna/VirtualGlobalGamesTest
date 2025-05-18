using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class LoseWindow : Window
    {
        [SerializeField]
        private Button _close;

        private void Awake()
        {
            _close.onClick.RemoveAllListeners();
            _close.onClick.AddListener(Hide);
        }

        new public void Show() => base.Show();
    }
}