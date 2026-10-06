using UnityEngine;

namespace UINavigation
{
    public class SlideHighlighter : MonoBehaviour, IHighlighter
    {

        private Vector2 _defaultPos;
        [SerializeField] private Vector2 _slideFactor;

        private Vector2 _targetPos;
        private Vector2 _startPos;
        private float _t;
        [SerializeField] private float _speed = 1;
        [SerializeField] GameObject _pointerHelper;

        void Start()
        {
            _defaultPos = transform.position;
            _targetPos = _defaultPos;
        }

        void Update()
        {
            if (Vector2.Distance(transform.position, _targetPos) >= 0.1f)
            {
                transform.position = Vector2.Lerp(_startPos, _targetPos, _t);
                _t += Time.deltaTime * _speed;
            }
        }

        public void EnterHighlight()
        {
            _t = 0;
            _startPos = transform.position;
            _targetPos = _defaultPos + _slideFactor;
            _pointerHelper.SetActive(true);
        }

        public void ExitHighlight()
        {
            _t = 0;
            _startPos = transform.position;
            _targetPos = _defaultPos;
            _pointerHelper.SetActive(false);
        }

    }
}

