using UnityEngine;


namespace UINavigation
{
    /// <summary>
    /// Class responsible for handling menu panels that use Button Selectors
    /// </summary>
    [RequireComponent(typeof(ButtonSelector))]
    public abstract class SelectorPanel : MenuPanel
    {
        /// <summary>
        /// Component that gives the user the ability to navigate through a list of 
        /// buttons
        /// </summary>
        public ButtonSelector Selector { get; private set; }

        //Setting up with local (to the GameObject) variables
        protected virtual void Awake()
        {
            //Get the selector component
            Selector = GetComponent<ButtonSelector>();

            //When a button is selected, disable this selector
            Selector.OnSelection += (b) => { Selector.Enable(false); };
        }

        /// <summary>
        /// Method called when the panel is added to the stack or reveal due to an
        /// DeStack.
        /// 
        /// The override for the class SelectorPanel enables the ButtonSelector.
        /// </summary>
        protected override void StackAbs()
        {
            base.StackAbs();

            if (Selector == null)
            {
                //Get the selector component
                Selector = GetComponent<ButtonSelector>();

                //When a button is selected, disable this selector
                Selector.OnSelection += (b) => { Selector.Enable(false); };
            }

            Selector.Enable(true);
        }

        /// <summary>
        /// Method called when the panel is Destacked.
        /// 
        /// The override for the class SelectorPanel disables the ButtonSelector.
        /// </summary>
        protected override void DeStackAbs()
        {
            base.DeStackAbs();
            Selector?.Enable(false);
        }

        /// <summary>
        /// Method called when another panel is stacked on top of this one.
        /// 
        /// The override for the class SelectorPanel disables the ButtonSelector.
        /// </summary>
        protected override void OverStackAbs()
        {
            base.OverStackAbs();
            Selector.Enable(false);
        }
    }
}