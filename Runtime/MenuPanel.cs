using System;
using UnityEngine;

namespace dtfernandes.UINavigation
{
    //Class that represents a focusable UI panel
    public abstract class MenuPanel : MonoBehaviour
    {
        public Action<MenuPanel> onStackonTop;
        public Action onDeStack;
        public Action onStack;

        public void DeStack()
        {
            DeStackAbs();
            onDeStack?.Invoke();
        }

        public void StackMenu()
        {
            MenuManager.Instance.StackMenu(this);
        }

        public void Stack()
        {
            StackAbs();
            onStack?.Invoke();
        }

        public void OverStack()
        {
            OverStackAbs();
        }

        protected virtual void OverStackAbs() { }

        protected virtual void DeStackAbs() { }
        
        protected virtual void StackAbs() { }
    }
}