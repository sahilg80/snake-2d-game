
using UnityEngine;

public class SnakeBody : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        BaseSnake snake = col.transform.GetComponent<BaseSnake>();
        if (snake != null)
        {
            Debug.Log("snake dead");
            snake.Death();
        }
    }
}
