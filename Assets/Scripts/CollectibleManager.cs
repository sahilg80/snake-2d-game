using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{

    [SerializeField]
    private BaseSnake snakeRed;
    [SerializeField]
    private BaseSnake snakeGreen;
    private float powerUpGapDuration;
    private Array foodEnumValues;
    private Array powerUpEnumValues;
    private float powerUpLookUpTimer;
    private bool shouldPowerUpSpawn;
    private GameObject currentSpawnedFood;
    private GameObject currentSpawnedPowerup;
    private static CollectibleManager instance;
    public static CollectibleManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        foodEnumValues = Enum.GetValues(typeof(FoodType));
        powerUpEnumValues = Enum.GetValues(typeof(PowerUpType));
        powerUpGapDuration = UnityEngine.Random.Range(10,15);
    }

    // Update is called once per frame
    void OnEnable()
    {

    }

    void Update()
    {
        if (snakeGreen.IsPowerActivated || snakeRed.IsPowerActivated) return;

        powerUpLookUpTimer += Time.deltaTime;
        if (powerUpLookUpTimer >= powerUpGapDuration)
        {
            shouldPowerUpSpawn = true;
            powerUpLookUpTimer = 0;
            powerUpGapDuration = UnityEngine.Random.Range(10,15);
        }
        if (shouldPowerUpSpawn)
        {
            SelectRandomPowerUp();
            shouldPowerUpSpawn = false;
        }
    }

    public void SpawnPowerUp(GameObject powerUpObj)
    {
        if (powerUpObj == null) return;
        currentSpawnedPowerup = ObjectPoolManager.Instance.SpawnObject(powerUpObj);
        do
        {
            currentSpawnedPowerup.transform.position = new Vector2(UnityEngine.Random.Range(-GameAssets.Instance.Width, GameAssets.Instance.Width), UnityEngine.Random.Range(-GameAssets.Instance.Height, GameAssets.Instance.Height));
        } while (Vector2.Distance(transform.position, currentSpawnedPowerup.transform.position) < 0.2f || Vector2.Distance(currentSpawnedFood.transform.position, currentSpawnedPowerup.transform.position) < 0.2f);
    }

    private void SelectRandomPowerUp()
    {
        int randomIndex = UnityEngine.Random.Range(0, powerUpEnumValues.Length);

        PowerUpType powerUp = (PowerUpType)powerUpEnumValues.GetValue(randomIndex);

        GameObject objToSpawn = null;
        switch (powerUp)
        {
            case PowerUpType.Shield:
                objToSpawn = GameAssets.Instance.ShieldPowerUp;
                break;
            case PowerUpType.ScoreBoost:
                objToSpawn = GameAssets.Instance.ScoreBoostPowerUp;
                break;
            case PowerUpType.SpeedUp:
                objToSpawn = GameAssets.Instance.SpeedBoostPowerUp;
                break;
        }
        SpawnPowerUp(objToSpawn);
    }

    public void SpawnFood(GameObject obj)
    {
        currentSpawnedFood = ObjectPoolManager.Instance.SpawnObject(obj);
        do
        {
            currentSpawnedFood.transform.position = new Vector2(UnityEngine.Random.Range(-GameAssets.Instance.Width, GameAssets.Instance.Width), UnityEngine.Random.Range(-GameAssets.Instance.Height, GameAssets.Instance.Height));
        } while (Vector2.Distance(transform.position, currentSpawnedFood.transform.position) < 0.2f);
    }

    public void SelectRandomFood()
    {
        if (snakeGreen.BodySize == 1 || snakeRed.BodySize == 1)
        {
            SpawnFood(GameAssets.Instance.MassGainer);
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, foodEnumValues.Length);

        FoodType food = (FoodType)foodEnumValues.GetValue(randomIndex);

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

    private void OnDisable()
    {

    }

}
