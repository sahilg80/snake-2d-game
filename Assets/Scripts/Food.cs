using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    FoodType type;
    [SerializeField]
    int duration;
    private float timer;

    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            FoodManager.Instance.SelectRandomFood();
            ObjectPoolManager.Instance.DeSpawnObject(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Snake snake = col.transform.GetComponent<Snake>();
        if (snake != null)
        {
            TakeCollisionAction(snake);
        }
    }

    private void TakeCollisionAction(Snake snake)
    {
        switch (type)
        {
            case FoodType.Gainer:
                snake.BodySize = snake.BodySize + 1;
                break;
            case FoodType.Burner:
                snake.BodySize = snake.BodySize - 1;
                break;
        }
        if (snake.BodySize == 1)
        { 
            FoodManager.Instance.SpawnFood(GameAssets.Instance.MassGainer); 
        }
        else
        {
            FoodManager.Instance.SelectRandomFood();
            ObjectPoolManager.Instance.DeSpawnObject(gameObject);
        }
    }
}
