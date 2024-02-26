using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float visibilityTimer;
    private bool isActivated;
    private bool isVisible;
    private SpriteRenderer spriteRenderer;
    private BaseSnake snakeCollided;
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
                DeActivatePowerUp();
            }
        }
    }

    private void DeActivatePowerUp()
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
        snakeCollided.IsPowerActivated = false;
        ObjectPoolManager.Instance.DeSpawnObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        snakeCollided = collision.transform.GetComponent<BaseSnake>();
        if (snakeCollided != null)
        {
            ActivatePowerUp();
        }
    }

    private void ActivatePowerUp()
    {
        snakeCollided.IsPowerActivated = true;
        switch (type)
        {
            case PowerUpType.Shield:
                snakeCollided.IsShielded = true;
                break;
            case PowerUpType.ScoreBoost:
                snakeCollided.IsScoreBoosted = true;
                break;
            case PowerUpType.SpeedUp:
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
