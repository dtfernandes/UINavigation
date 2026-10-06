using System.Collections.Generic;
using UnityEngine;

namespace UINavigation
{

    /// <summary>
    /// Manager that handles menus 
    /// </summary>
    public class MenuManager : MenuPanel
    {
        // Stack representing the sequence of menus
        private Stack<MenuPanel> menuStack;

        /// <summary>
        /// Singleton instance 
        /// </summary>
        public static MenuManager Instance;

        //Panel to be stack initially
        [SerializeField]
        private MenuPanel _intialPanel;
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(this.gameObject);
            }
        }

        void Start()
        {
            if (menuStack == null)
                menuStack = new Stack<MenuPanel> { };

            if (_intialPanel != null)
                _intialPanel.StackMenu();
        }

        /// <summary>
        /// Add a new panel to the stack
        /// </summary>
        /// <param name="panel">New panel to add</param>
        public void StackMenu(MenuPanel panel)
        {
            //Create the stack if it doens't exist
            if (menuStack == null)
                menuStack = new Stack<MenuPanel> { };

            //If there was alread a panel in the state...
            MenuPanel previous = null;
            if (menuStack.TryPeek(out previous))
            {
                //... handle that interaction in the panel
                previous.OverStack();
            }

            //Add the panel to the stack
            menuStack.Push(panel);
            panel.Stack();
        }

        /// <summary>
        /// Remove the top most menu from the stack and reveal the one 
        /// below it
        /// </summary>
        public void DeStackMenu()
        {
            //If the stack is empty or is in its last panel ignore
            if (menuStack.Count <= 1)
                return;

            //Remove top most panel
            MenuPanel panel = menuStack.Pop();

            panel.DeStack();

            if (menuStack.Count > 0)
            {
                menuStack.Peek().Stack();
            }

        }

        public void FullDeStackMenu()
        {
            //If the stack is empty or is in its last panel ignore
            if (menuStack.Count <= 1)
                return;

            while (menuStack.Count > 1)
            {

                //Remove top most panel
                MenuPanel panel = menuStack.Pop();

                panel.DeStack();

                if (menuStack.Count > 0)
                {
                    menuStack.Peek().Stack();
                }
            }

        }

        public void ClearStack()
        {
            menuStack.Clear();
        }

        public void ReStack(MenuPanel menu)
        {
            MenuPanel peekedMenu = null;
            if (menuStack?.TryPeek(out peekedMenu) ?? false)
            {
                if (peekedMenu == menu)
                    menuStack.Pop();
            }

            StackMenu(menu);
        }

        void Update()
        {
            //If the player presses the 'Go Back' key, go to the previous menu panel
            if (Input.GetKeyDown(KeyCode.C))
            {
                DeStackMenu();
            }
        }
        protected override void DeStackAbs()
        {
            gameObject.SetActive(false);
        }
    }

}