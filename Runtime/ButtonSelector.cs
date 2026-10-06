using System;
using System.Collections.Generic;
using UnityEngine;

namespace dtfernandes.UINavigation
{
    public class ButtonSelector : MonoBehaviour
    { 
        public Action<Selectable> OnSelection;

        [SerializeField]
        private List<Selectable> _selectables;

        public List<Selectable> Buttons { get => _selectables; set => _selectables = value; }

        private Selectable _current;
        private int _currentIndex;

        [SerializeField]
        private bool _isEnabled;

        [SerializeField]
        private bool _reverse;

        void Start()
        {
            if (_selectables.Count > 0 && _isEnabled)
                SelectButton(_currentIndex);


            for (int i = 0; i < _selectables.Count; i++)
            {
                int o = i;

                Selectable sB = _selectables[i];

                sB.OnSelection += () =>
                {
                    if (sB.Selected) return;
                    SelectButton(o);
                };

            }
        }

        void Update()
        {
            if (!_isEnabled) return;
            if (_selectables == null) return;
            if (_selectables.Count <= 0) return;

            int rev = _reverse ? -1 : 1;

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SelectButton(_currentIndex + rev, true);
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectButton(_currentIndex - rev, false);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (!_current.CaptureHorizontal)
                    SelectButton(_currentIndex - rev, false);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (!_current.CaptureHorizontal)
                    SelectButton(_currentIndex + rev, false);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                ActivateButton();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                ActivateButton();
            }
        }

        public void ActivateButton()
        {
            //current.Click();
        }

        public void Enable(bool v)
        {
            _isEnabled = v;

            if (v)
            {
                // Bad
                foreach (Selectable selectable in _selectables)
                {
                    selectable.Active = true;
                }
                SelectButton(_currentIndex);
            }
            else
            {
                DeselectAll();
            }
        }

        public void DeselectAll()
        {
            if (_current == null) return;

            foreach (Selectable selectable in _selectables)
            {
                selectable.Active = false;
            }
        }

        /// <summary>
        /// Method responsible for handling the selection of a new button
        /// </summary>
        /// <param name="newButton">New button beeing selected</param>
        private void SelectButton(int newButton, bool direction)
        {
            //Ignore if there's no buttons associated with this component
            if (_selectables.Count == 0)
                return;

            //Classic loop around
            if (newButton > _selectables.Count - 1)
            {
                newButton = 0;
            }
            else if (newButton < 0)
            {
                newButton = _selectables.Count - 1;
            }

            // If the new button we want to activate is not active...
            if (!_selectables[newButton].Active)
            {
                //.. increment/decrement
                if (direction)
                    SelectButton(newButton + 1);
                else
                    SelectButton(newButton - 1);

                return;
            }

            //Deselect current Button
            if (_current != null)
            {
                //Handle deselction on the button
                _current.Deselect();
            }

            //Update current button
            _currentIndex = newButton;
            _current = _selectables[newButton];

            _current.Select(false);
        }



        private void SelectButton(int newButton)
        {
            // Ignore if there's no buttons associated with this component
            if (_selectables.Count == 0)
                return;


            // Deselect the currently selected button
            if (_current != null)
            {
                //Handle deselection on the button
                _current.Deselect();
            }

            //Update current button
            _currentIndex = newButton;
            _current = _selectables[newButton];

            _current.Select(false);
        }


        public void Clear()
        {
            _selectables.Clear();
        }

        public void Add(SimpleButton btn, int index = -1)
        {
            if (index != -1)
                _selectables.Insert(index, btn);
            else
            {
                _selectables.Add(btn);
            }
        }

        public void Remove(SimpleButton btn)
        {
            _selectables.Remove(btn);
        }
    }
}