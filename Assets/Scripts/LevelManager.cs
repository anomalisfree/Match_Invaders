using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int column;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float stepDelay;

    private EnemyItem[,] _enemyArray;
    private bool _isMovingLeft;

    private int _deadEnemiesInOneTime;
    private float _timerDeadEnemies;

    public int Score { get; private set; }


    private void Start()
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        _enemyArray = new EnemyItem[rows, column];

        for (var x = 0; x < column - 1; x++)
        {
            for (var y = 0; y < column - 5; y++)
            {
                var enemy = Instantiate(enemyPrefab, this.transform).GetComponent<EnemyItem>();
                enemy.Initialize(x, y);
                _enemyArray[x, y] = enemy;
                _enemyArray[x, y].onDead += OnEnemyDead;
            }
        }

        StartCoroutine(EnemiesSteps());
    }

    private IEnumerator EnemiesSteps()
    {
        while (true)
        {
            yield return new WaitForSeconds(stepDelay);
            if (!_isMovingLeft)
            {
                var canMoveRight = true;

                for (var y = 0; y < column; y++)
                {
                    if (_enemyArray[rows - 1, y] != null)
                    {
                        canMoveRight = false;
                    }
                }

                if (canMoveRight)
                {
                    MoveRight();
                }
                else
                {
                    MoveDown();
                }
            }
            else
            {
                var canMoveLeft = true;

                for (var y = 0; y < column; y++)
                {
                    if (_enemyArray[0, y] != null)
                    {
                        canMoveLeft = false;
                    }
                }

                if (canMoveLeft)
                {
                    MoveLeft();
                }
                else
                {
                    MoveDown();
                }
            }
        }
    }

    private void MoveRight()
    {
        for (var y = 0; y < rows; y++)
        {
            for (var x = column - 1; x > 0; x--)
            {
                _enemyArray[x, y] = _enemyArray[x - 1, y];

                if (_enemyArray[x, y] != null)
                    _enemyArray[x, y].SetPos(x, y);
            }

            _enemyArray[0, y] = null;
        }
    }

    private void MoveLeft()
    {
        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < column - 1; x++)
            {
                _enemyArray[x, y] = _enemyArray[x + 1, y];

                if (_enemyArray[x, y] != null)
                    _enemyArray[x, y].SetPos(x, y);
            }

            _enemyArray[column - 1, y] = null;
        }
    }

    private void MoveDown()
    {
        for (var x = 0; x < column; x++)
        {
            for (var y = rows - 1; y > 0; y--)
            {
                _enemyArray[x, y] = _enemyArray[x, y - 1];

                if (_enemyArray[x, y] != null)
                    _enemyArray[x, y].SetPos(x, y);
            }

            _enemyArray[x, 0] = null;
        }

        _isMovingLeft = !_isMovingLeft;

        for (var x = 0; x < rows; x++)
        {
            if (_enemyArray[x, column - 1] != null)
            {
                Lose();
            }
        }
    }

    private void Update()
    {
        if (_timerDeadEnemies > 0)
        {
            _timerDeadEnemies -= Time.deltaTime;
        }
        else if (_deadEnemiesInOneTime > 0)
        {
            Debug.Log(_deadEnemiesInOneTime);
            IncrementScore(_deadEnemiesInOneTime * Fibonacci(_deadEnemiesInOneTime + 1) * 10);
            _deadEnemiesInOneTime = 0;
        }
    }

    private void OnEnemyDead(int x, int y, Color color)
    {
        _enemyArray[x, y].onDead -= OnEnemyDead;

        _timerDeadEnemies = Time.deltaTime * 2;
        _deadEnemiesInOneTime++;

        if (x > 0 && _enemyArray[x - 1, y] != null && _enemyArray[x - 1, y].color == color)
            _enemyArray[x - 1, y].DelayDead();

        if (x < rows - 1 && _enemyArray[x + 1, y] != null && _enemyArray[x + 1, y].color == color)
            _enemyArray[x + 1, y].DelayDead();

        if (y > 0 && _enemyArray[x, y - 1] != null && _enemyArray[x, y - 1].color == color)
            _enemyArray[x, y - 1].DelayDead();

        if (y < column - 1 && _enemyArray[x, y + 1] != null && _enemyArray[x, y + 1].color == color)
            _enemyArray[x, y + 1].DelayDead();
    }

    private void Lose()
    {
        StopAllCoroutines();
    }

    public void IncrementScore(int incrementation)
    {
        Score += incrementation;
    }

    public void Reset()
    {
        Score = 0;
        // reset logic
    }

    public static int Fibonacci(int index)
    {
        var n1 = 0;
        var n2 = 1;

        for (var i = 1; i < index; i++)
        {
            var tmp = n1 + n2;
            n1 = n2;
            n2 = tmp;
        }

        return n1;
    }
}