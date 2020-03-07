using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            transform.position += Vector3.right * (Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        }
    }
}