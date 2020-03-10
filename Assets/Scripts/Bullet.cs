using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    
    private float _lifeTime;
    private Transform _transform;
    
    private void Start()
    {
        _transform = transform;
        _lifeTime = 20 / speed;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        _transform.Translate(_transform.up * (Time.deltaTime * speed), Space.World);
    }
}