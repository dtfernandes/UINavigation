using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UINavigation
{
    public class SimpleSlider : Selectable
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _label, _valueLabel;

        protected override Image Image => _slider.image;

        protected override void Awake()
        {
            _valueLabel.text = _slider.value + "";
            _slider.onValueChanged.AddListener((v) =>
            {
                _valueLabel.text = v + "";
            });

            base.Awake();
        }

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
