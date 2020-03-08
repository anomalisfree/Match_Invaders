using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyItem : MonoBehaviour
{
    public Color color;
    public Action<int, int, Color> onDead;

    [SerializeField] private float gridScale;
    [SerializeField] private float movingSpeed;
    [SerializeField] private GameObject deadPrefab;
    
    [SerializeField] private Transform bulletCreator;
    [SerializeField] private GameObject bullet;

    private Transform _enemyTransform;
    private SpriteRenderer _spriteRenderer;

    private int _x;
    private int _y;

    public void Initialize(int x, int y)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyTransform = transform;

        switch (Random.Range(0, 4))
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null)
        {
            Destroy(other.gameObject);
            EnemyDead();
        }
    }

    public void DelayDead()
    {
        StartCoroutine(DeadAfterDaley());
    }

    private IEnumerator DeadAfterDaley()
    {
        yield return null;
        EnemyDead();
    }

    private void EnemyDead()
    {
        onDead.Invoke(_x, _y, color);
        
        var transformThis = transform;
        var deadEnemy = Instantiate(deadPrefab, transformThis.position, transformThis.rotation);
        deadEnemy.GetComponent<SpriteRenderer>().color = color;
        Destroy(deadEnemy, 0.2f);
            
        Destroy(gameObject);
    }

    public void Shoot()
    {
        Instantiate(bullet, bulletCreator.position, bulletCreator.rotation);
    }
}