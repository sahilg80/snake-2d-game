using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float visibilityTimer;
    private bool isActivated;
    private bool isVisible;
    private SpriteRenderer spriteRenderer;
    private Snake snakeCollided;
    [SerializeField]
    private int visibilityDuration;
    [SerializeField]
    private PowerUpType type;
    private float activatedTimer;
    [SerializeField]
    private float activatedDuration;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        isVisible = true;
        isActivated = false;
        snakeCollided = null;
        spriteRenderer.enabled = true;
    }

    // Start is called before the first frame update
    void Update()
    {
        if (isVisible)
        {
            visibilityTimer += Time.deltaTime;
            if (visibilityTimer >= visibilityDuration)
            {
                visibilityTimer = 0f;
                isVisible = false;
                ObjectPoolManager.Instance.DeSpawnObject(gameObject);
            }
        }
        else if (isActivated)
        {
            activatedTimer += Time.deltaTime;
            if (activatedTimer >= activatedDuration)
            {
                activatedTimer = 0f;
                isActivated = false;
                DecisionBasedOnType();
            }
        }
    }

    private void DecisionBasedOnType()
    {
        switch (type)
        {
            case PowerUpType.Shield:
                snakeCollided.IsShielded = false;
                break;
            case PowerUpType.ScoreBoost:
                snakeCollided.IsScoreBoosted = false;
                break;
            case PowerUpType.SpeedUp:
                snakeCollided.IsSpeedBoosted = false;
                break;
        }
        snakeCollided.IsPowerUpActivated = false;
        ObjectPoolManager.Instance.DeSpawnObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        snakeCollided = collision.transform.GetComponent<Snake>();
        if (snakeCollided != null)
        {
            TakeCollisionAction();
        }
    }

    private void TakeCollisionAction()
    {
        snakeCollided.IsPowerUpActivated = true;
        switch (type)
        {
            case PowerUpType.Shield:
                snakeCollided.IsShielded = true;
                Debug.Log("collided with shield");
                //snake.BodySize = snake.BodySize + 1;
                break;
            case PowerUpType.ScoreBoost:
                Debug.Log("collided with score bost");
                snakeCollided.IsScoreBoosted = true;
                //snake.BodySize = snake.BodySize - 1;
                break;
            case PowerUpType.SpeedUp:
                Debug.Log("collided with speed up");
                snakeCollided.IsSpeedBoosted = true;
                break;
        }
        isActivated = true;
        isVisible = false;
        visibilityTimer = 0f;
        activatedTimer = 0f;
        spriteRenderer.enabled = false;
    }

    private void OnDisable()
    {
        isVisible = false;
    }
}
