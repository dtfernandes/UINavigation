using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace dtfernandes.UINavigation
{

    /// <summary>
    /// Represents a button 
    /// </summary>
    public class SimpleButton : Selectable, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
    {

        /// <summary>
        /// Event that represent the click of the button
        /// </summary>
        [field: SerializeField]
        public UnityEvent OnClick { get; set; }

        /// <summary>
        /// Event that represent the click of a non interactable button
        /// </summary>
        [field: SerializeField]
        public UnityEvent OnNonInteractClick { get; set; }

        protected Image _image;
        protected override Image Image
        {
            get
            {
                if (_image == null)
                    _image = GetComponent<Image>();
                    
                return _image;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            UpdateDisplay();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Deselect();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Select(true);
        }


        /// <summary>
        /// Click the button and activate the event
        /// </summary>
        public virtual void Click()
        {
            if (Interactable)
            {
                OnClick?.Invoke();
            }
            else
            {
                OnNonInteractClick?.Invoke();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Click();
        }

    }


}

