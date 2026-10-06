using UnityEngine;
using UnityEngine.UI;

namespace dtfernandes.UINavigation
{
    public class SimpleSlider : Selectable
    {
        [SerializeField] private Slider _slider;

        protected override Image Image => _slider.image;

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _slider.value -= 10;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _slider.value += 10;
            }
        }

    }
}
