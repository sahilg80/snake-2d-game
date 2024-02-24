
using UnityEngine;

public class SnakeBody : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        Snake snake = col.transform.GetComponent<Snake>();
        if (snake != null)
        {
            Debug.Log("snake dead");
            snake.Death();
        }
    }
}
