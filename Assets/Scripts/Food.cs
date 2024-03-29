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
            timer = 0f;
            ObjectPoolManager.Instance.DeSpawnObject(gameObject);
            CollectibleManager.Instance.SelectRandomFood();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        BaseSnake snake = col.transform.GetComponent<BaseSnake>();
        if (snake != null)
        {
            CollectFood(snake);
        }
    }

    private void CollectFood(BaseSnake snake)
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
        timer = 0f;
        ObjectPoolManager.Instance.DeSpawnObject(gameObject);
        if (snake.BodySize == 1)
        {
            CollectibleManager.Instance.SpawnFood(GameAssets.Instance.MassGainer); 
        }
        else
        {
            CollectibleManager.Instance.SelectRandomFood();
        }
    }
}
