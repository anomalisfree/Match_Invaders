using System;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform bulletCreator;
    [SerializeField] private GameObject bullet;

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

            if ( position.x < -12)
            {
               position = new Vector3(-12,  position.y,  position.z);
            }
            else if ( position.x > 12)
            {
                position = new Vector3(12, position.y,  position.z);
            }
            
            _transform.position = position;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, bulletCreator.position, bulletCreator.rotation);
        }
    }
}