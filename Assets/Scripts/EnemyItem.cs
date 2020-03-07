using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyItem : MonoBehaviour
{
    public Color color;

    [SerializeField] private float gridScale;
    [SerializeField] private float movingSpeed;

    private Transform _enemyTransform;
    private SpriteRenderer _spriteRenderer;

    private int _x;
    private int _y;

    public void Initialize(int x, int y)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyTransform = transform;

        switch (Random.Range(0, 5))
        {
            case 0:
                color = _spriteRenderer.color = Color.yellow;
                break;

            case 1:
                color = _spriteRenderer.color = Color.red;
                break;

            case 2:
                color = _spriteRenderer.color = Color.green;
                break;

            case 3:
                color = _spriteRenderer.color = Color.blue;
                break;
        }

        SetPos(x, y);

        _enemyTransform.localPosition = (Vector3.right * x + Vector3.down * y) * gridScale;
    }

    public void SetPos(int x, int y)
    {
        _x = x;
        _y = y;
    }

    private void Update()
    {
        if (_enemyTransform.localPosition != (Vector3.right * _x + Vector3.down * _y) * gridScale)
        {
            _enemyTransform.localPosition = Vector3.MoveTowards(_enemyTransform.localPosition,
                (Vector3.right * _x + Vector3.down * _y) * gridScale, Time.deltaTime * movingSpeed);
        }
    }
}