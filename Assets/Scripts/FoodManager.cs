using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : SingletonBehaviour<FoodManager>
{
    private Array enumValues;
    // Start is called before the first frame update
    void Start()
    {
        enumValues = Enum.GetValues(typeof(FoodType));
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
