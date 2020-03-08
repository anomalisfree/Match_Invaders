using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    private Transform _transform;
    
    private void Start()
    {
        _transform = transform;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        _transform.Translate(Vector3.up * (Time.deltaTime * speed), Space.Self);
    }
}