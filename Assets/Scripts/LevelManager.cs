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
                else
                {
                    _isMovingLeft = true;
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
                else
                {
                    _isMovingLeft = false;
                }
            }
        }
    }

    public void IncrementScore()
    {
        Score++;
    }

    public void Reset()
    {
        Score = 0;
        // reset logic
    }
}