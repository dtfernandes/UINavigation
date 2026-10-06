using UnityEngine;
using UnityEngine.UI;

namespace dtfernandes.UINavigation
{
    public abstract class Selectable : MonoBehaviour
    {
        public event System.Action OnSelection;

        [SerializeField]
        [HideInInspector]
        private Sprite _highlightSprite, _defaultSprite;

        [SerializeField]
        [HideInInspector]
        private Color _highlightColor, _defaultColor;

        [SerializeField] private HighlightMode _highlightMode;

        //Image component of the button
        protected abstract Image Image { get; }

        private IHighlighter[] _highlighter;

        [SerializeField] private bool _active;

        /// <summary>
        /// Is the button active and ready to use
        /// </summary>
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;

                UpdateDisplay();
            }
        }

        [SerializeField]
        private bool _interactable = true;
        /// <summary>
        /// Can the button be interacted with with
        /// </summary>
        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                UpdateDisplay();
            }
        }

        //Button is selected
        private bool _selected;
        public bool Selected => _selected;

        [field: SerializeField] public bool CaptureHorizontal { get; private set; }


        protected virtual void Awake()
        {
            _highlighter = GetComponents<IHighlighter>();
        }

        public virtual void Select(bool fromCursor)
        {
            if (fromCursor)
                OnSelection?.Invoke();

            _selected = true;
            UpdateDisplay();
        }

        public void Deselect()
        {
            _selected = false;
            UpdateDisplay();
        }

        /// <summary>
        /// Updates this button's style
        /// </summary>
        public void UpdateDisplay()
        {
            //Setup the inactive disaply
            if (!_active)
            {
                Image.color = new Color(0.3f, 0.3f, 0.3f, Image.color.a);
            }
            else
            {
                Image.color = _defaultColor;
            }

            //Setup other styles
            if (_selected)
            {
                EnterHighlight();
            }
            else if (!_interactable)
            {
                Image.color = new Color(0.5f, 0.29f, 0.29f, Image.color.a);
            }
            else
            {
                ExitHighlight();
            }
        }


        private void EnterHighlight()
        {
            if (!Active || !Interactable) return;

            switch (_highlightMode)
            {
                case HighlightMode.Color:
                    Image.color = _highlightColor;
                    break;
                case HighlightMode.Sprite:
                    Image.sprite = _highlightSprite;
                    break;
                case HighlightMode.Script:
                    if (_highlighter != null)
                    {
                        if (_highlighter.Length == 0) break;
                        foreach (IHighlighter highlight in _highlighter)
                        {
                            highlight.EnterHighlight();
                        }
                    }
                    break;
            }
        }

        private void ExitHighlight()
        {
            if (!Active || !Interactable) return;

            switch (_highlightMode)
            {
                case HighlightMode.Color:
                    Image.color = _defaultColor;
                    break;
                case HighlightMode.Sprite:
                    Image.sprite = _defaultSprite;
                    break;
                case HighlightMode.Script:
                    if (_highlighter != null)
                    {
                        if (_highlighter.Length == 0) break;
                        foreach (IHighlighter highlight in _highlighter)
                        {
                            highlight.ExitHighlight();
                        }
                    }
                    break;
            }
        }

    }


}

