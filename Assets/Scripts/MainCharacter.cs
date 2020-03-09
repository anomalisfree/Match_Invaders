using System;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Action onGetHit;

    [SerializeField] private float speed;
    [SerializeField] private Transform bulletCreator;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject deadPrefab;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            var position = _transform.position;

            position += Vector3.right * (Input.GetAxis("Horizontal") * speed * Time.deltaTime);

            if (position.x < -12)
            {
                position = new Vector3(-12, position.y, position.z);
            }
            else if (position.x > 12)
            {
                position = new Vector3(12, position.y, position.z);
            }

            _transform.position = position;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, bulletCreator.position, bulletCreator.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null)
        {
            Destroy(other.gameObject);
            onGetHit.Invoke();
        }
    }

    public void Dead()
    {
        Destroy(Instantiate(deadPrefab, _transform.position, _transform.rotation), 0.2f);
        Destroy(gameObject);
    }
}