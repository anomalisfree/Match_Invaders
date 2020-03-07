using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed;
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed;
        }
    }
}
