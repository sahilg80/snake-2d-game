using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField]
    private Snake snake;
    private Array enumValues;
    private int snakeBodySize;

    private static CollectibleManager instance;
    public static CollectibleManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        enumValues = Enum.GetValues(typeof(FoodType));
        snakeBodySize = 1;
    }

    // Update is called once per frame
    void OnEnable()
    {
        snake.OnCollectFood += OnFoodEaten;
    }

    public void SpawnFood(GameObject obj)
    {
        GameObject spawnedFood = ObjectPoolManager.Instance.SpawnObject(obj);
        do
        {
            spawnedFood.transform.position = new Vector2(UnityEngine.Random.Range(-GameAssets.Instance.Width, GameAssets.Instance.Width), UnityEngine.Random.Range(-GameAssets.Instance.Height, GameAssets.Instance.Height));
        } while (Vector2.Distance(transform.position, spawnedFood.transform.position) < 0.2f);
    }

    public void SelectRandomFood()
    {
        if (snakeBodySize == 1)
        {
            SpawnFood(GameAssets.Instance.MassGainer);
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, enumValues.Length);

        FoodType food = (FoodType)enumValues.GetValue(randomIndex);

        GameObject objToSpawn = null;
        switch (food)
        {
            case FoodType.Gainer:
                objToSpawn = GameAssets.Instance.MassGainer;
                break;
            case FoodType.Burner:
                objToSpawn = GameAssets.Instance.MassBurner;
                break;
            default:
                objToSpawn = GameAssets.Instance.MassGainer;
                break;
        }
        SpawnFood(objToSpawn);
    }

    private void OnFoodEaten(int val)
    {
        snakeBodySize = snakeBodySize + val;
    }

    private void OnDisable()
    {
        snake.OnCollectFood -= OnFoodEaten;
    }

}
