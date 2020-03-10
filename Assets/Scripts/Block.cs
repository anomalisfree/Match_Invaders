using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int health;
    
    [SerializeField] private GameObject deadPrefab;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null)
        {
            health--;
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.GetComponent<EnemyItem>() != null)
        {
            health = 0;
        }
        
        if(health <= 0)
        {
            var deadObject = Instantiate(deadPrefab, _transform.position, _transform.rotation);
            deadObject.GetComponent<SpriteRenderer>().color = Color.green;
            Destroy(deadObject, 0.2f);
            
            Destroy(gameObject);
        }
    }
}
