using Cysharp.Threading.Tasks;
using Game.Core;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class Windows : ILevelResultShowcase
    {
        private readonly WinWindow _win;
        private readonly LoseWindow _lose;
        private readonly GameHUD _hud;
        private readonly CursorBehaviour _cursor;

        public Windows(WinWindow win, LoseWindow lose, GameHUD hud, CursorBehaviour cursor)
        {
            _win = win;
            _lose = lose;
            _hud = hud;
            _cursor = cursor;

            _win.Hide();
            _lose.Hide();
            _hud.Show();
            _cursor.Hide();
        }

        public async UniTask ShowResult(int levelIndex, bool completed, CancellationToken cancellationToken)
        {
            _hud.Hide();
            _cursor.Show();
            if (completed) 
            {
                _win.Show(levelIndex + 1);
                await _win.WaitForHide(cancellationToken);
            }
            else
            {
                _lose.Show();
                await _lose.WaitForHide(cancellationToken);
            }
            _cursor.Hide();
            _hud.Show();
        }
    }

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