using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Game.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        protected bool Shown => _canvas.enabled;

        public async UniTask WaitForHide(CancellationToken cancellationToken)
        {
            while (Shown)
                await UniTask.Yield(cancellationToken);
        }

        protected void Show() => _canvas.enabled = true;

        protected void Hide() => _canvas.enabled = false;
    }
}