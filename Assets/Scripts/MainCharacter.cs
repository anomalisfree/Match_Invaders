using System;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Action onGetHit;

    [SerializeField] private Transform bulletCreator;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject deadPrefab;

    private Transform _transform;
    private float _speed;
    private float _bulletSpeed;

    private void Start()
    {
        _transform = transform;
    }

    public void Initialize(float speed, float bulletSpeed)
    {
        _speed = speed;
        _bulletSpeed = bulletSpeed;
    }

    private void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            var position = _transform.position;

            position += Vector3.right * (Input.GetAxis("Horizontal") * _speed * Time.deltaTime);

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
            Instantiate(bullet, bulletCreator.position, bulletCreator.rotation).GetComponent<Bullet>().speed =
                _bulletSpeed;
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