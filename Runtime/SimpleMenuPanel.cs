using UnityEngine.Events;
using UnityEngine;

namespace UINavigation
{
    public class SimpleMenuPanel : SelectorPanel
    {
        [SerializeField]
        public new UnityEvent onStack;

        [SerializeField]
        public UnityEvent onDestack;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void StackAbs()
        {
            base.StackAbs();
            onStack?.Invoke();
        }

        protected override void DeStackAbs()
        {
            base.DeStackAbs();
            onDestack?.Invoke();
        }
    }
}