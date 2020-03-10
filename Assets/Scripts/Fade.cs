using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public float targetAlpha;

    private Image _spriteRenderer;
    private float _currentAlpha;

    private void Start()
    {
        _spriteRenderer = GetComponent<Image>();
        _currentAlpha = _spriteRenderer.color.a;
    }

    private void Update()
    {
        if (targetAlpha != _currentAlpha)
        {
            _currentAlpha = Mathf.MoveTowards(_currentAlpha, targetAlpha, Time.deltaTime);
            var color = _spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, _currentAlpha);
            _spriteRenderer.color = color;
        }
        else if (_currentAlpha == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
