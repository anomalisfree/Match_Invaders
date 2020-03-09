using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int column;

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float stepDelay;

    [SerializeField] private Text scoreText;

    [SerializeField] private GameObject healthIndicator;
    [SerializeField] private Transform healthUi;

    [SerializeField] private int playerHealth;

    private MainCharacter _mainCharacter;
    
    private EnemyItem[,] _enemyArray;
    private bool _isMovingLeft;

    private int _deadEnemiesInOneTime;
    private float _timerDeadEnemies;

    private int _score;

    private readonly List<GameObject> _playerHealthUi = new List<GameObject>();

    private void Start()
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        _enemyArray = new EnemyItem[rows, column];

        for (var x = 0; x < rows - 1; x++)
        {
            for (var y = 0; y < column - 5; y++)
            {
                var enemy = Instantiate(enemyPrefab, this.transform).GetComponent<EnemyItem>();
                enemy.Initialize(x, y);
                _enemyArray[x, y] = enemy;
                _enemyArray[x, y].onDead += OnEnemyDead;
            }
        }

        _playerHealthUi.Clear();
        
        for (var i = 0; i < playerHealth; i++)
        {
            var healthUiIndicator = Instantiate(healthIndicator, healthUi);
            healthUiIndicator.GetComponent<RectTransform>().anchoredPosition = Vector2.right * (100 + (i * 50));
            _playerHealthUi.Add(healthUiIndicator);
        }

        _mainCharacter = Instantiate(playerPrefab).GetComponent<MainCharacter>();
        _mainCharacter.onGetHit += GetHit;
       
        StartCoroutine(EnemiesSteps());
    }

    private IEnumerator EnemiesSteps()
    {
        while (true)
        {
            yield return new WaitForSeconds(stepDelay/2f);
            EnemyShoot();
            yield return new WaitForSeconds(stepDelay/2f);
            
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
                    MoveRight();
                else
                    MoveDown();
            }
            else
            {
                var canMoveLeft = true;

                for (var y = 0; y < column; y++)
                {
                    if (_enemyArray[0, y] != null)
                        canMoveLeft = false;
                }

                if (canMoveLeft)
                    MoveLeft();
                else
                    MoveDown();
            }
        }
    }

    private void EnemyShoot()
    {
        var allShoots = 0;
        
        for (var x = 0; x < rows; x++)
        {
            if (allShoots < 5)
            {
                if (Random.Range(0, 2) == 0)
                {
                    for (var y = column-1; y >= 0; y--)
                    {
                        if (_enemyArray[x, y] != null)
                        {
                            _enemyArray[x, y].Shoot();
                            allShoots++;
                            break;
                        }
                    }
                }
            }
        }
    }
    private void MoveRight()
    {
        for (var y = 0; y < column; y++)
        {
            for (var x = rows - 1; x > 0; x--)
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
        for (var y = 0; y < column; y++)
        {
            for (var x = 0; x < rows - 1; x++)
            {
                _enemyArray[x, y] = _enemyArray[x + 1, y];

                if (_enemyArray[x, y] != null)
                    _enemyArray[x, y].SetPos(x, y);
            }

            _enemyArray[rows - 1, y] = null;
        }
    }

    private void MoveDown()
    {
        for (var x = 0; x < rows; x++)
        {
            for (var y = column - 1; y > 0; y--)
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

    private void GetHit()
    {
        playerHealth--;
        Destroy(_playerHealthUi[_playerHealthUi.Count - 1]);
        _playerHealthUi.RemoveAt(_playerHealthUi.Count - 1);

        if (playerHealth <= 0)
        {
            Lose();
        }
    }

    private void Lose()
    {
        StopAllCoroutines();
        _mainCharacter.onGetHit -= GetHit;
        _mainCharacter.Dead();
    }

    private void IncrementScore(int incrementation)
    {
        _score += incrementation;
        scoreText.text = _score.ToString();
    }

    public void Reset()
    {
        _score = 0;
        // reset logic
    }

    private static int Fibonacci(int index)
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