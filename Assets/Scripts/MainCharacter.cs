using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform bulletCreator;
    [SerializeField] private GameObject bullet;

    private void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            transform.position += Vector3.right * (Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, bulletCreator.position, bulletCreator.rotation);
        }
    }
}