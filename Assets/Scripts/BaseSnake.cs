using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSnake : MonoBehaviour
{
    [SerializeField]
    protected float maxTimerToTakeNextPositionOnGrid;
    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    protected SpriteRenderer powerUpCollectedSprite;
    [SerializeField]
    protected Sprite speedUpSprite;
    [SerializeField]
    private Sprite scoreBoostSprite;
    [SerializeField]
    protected Sprite shieldSprite;
    [SerializeField]
    protected GameObject bodyPart;
    [SerializeField]
    private SnakeUI snakeUI;
    protected float score;
    protected Vector2Int position;
    protected SnakeDirection directionFacing;
    protected float currentTimer;
    protected int bodySize;
    private float previousGridMaxTimer;
    protected bool IsAlive { get; set; }
    protected bool isShielded;
    public bool IsShielded 
    { 
        get => isShielded; 
        set { 
            isShielded = value; 
            powerUpCollectedSprite.sprite = shieldSprite; 
            OnCollectPowerUp?.Invoke(snakeUI, PowerUpType.Shield.ToString()); 
        } 
    }
    protected bool isScoreBoosted;
    public bool IsScoreBoosted 
    { 
        get => isScoreBoosted; 
        set { 
            isScoreBoosted = value; 
            powerUpCollectedSprite.sprite = scoreBoostSprite;
            OnCollectPowerUp?.Invoke(snakeUI, PowerUpType.ScoreBoost.ToString()); 
        } 
    }
    protected bool isSpeedBoosted;
    public bool IsSpeedBoosted
    {
        get
        {
            return IsSpeedBoosted;
        }
        set
        {
            isSpeedBoosted = value;
            powerUpCollectedSprite.sprite = speedUpSprite;
            OnCollectPowerUp?.Invoke(snakeUI, PowerUpType.SpeedUp.ToString());
            if (isSpeedBoosted) ChangeMovementSpeed(speedMultiplier);
            else ChangeMovementSpeed(0);
        }
    }
    private bool isPowerActivated;
    public bool IsPowerActivated
    {
        get => isPowerActivated;
        set
        {
            isPowerActivated = value;
            powerUpCollectedSprite.enabled = value;
            if (!isPowerActivated)
            {
                OnCollectPowerUp?.Invoke(snakeUI, null);
            }
        }
    }

    public int BodySize
    {
        get
        {
            return bodySize;
        }
        set
        {
            bodySize = value;
            UpdateSnakeLength();
        }
    }
    protected List<GameObject> restOfBody;
    protected List<Vector3> previousPositions;
    public event Action<SnakeUI,float> OnCollectFood;
    public event Action OnSnakeDeath;
    public event Action<SnakeUI> OnWinSnake;
    public event Action<SnakeUI,string> OnCollectPowerUp;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Time.timeScale = 1;
        transform.position = new Vector3(position.x, position.y, 0);
        position = new Vector2Int(0, 1);
        restOfBody = new List<GameObject>();
        previousPositions = new List<Vector3>();
        bodySize = 1;
        IsAlive = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected abstract void Translate();

    protected void SetPosition()
    {
        currentTimer = 0;
        if (bodySize == previousPositions.Count)
        {
            previousPositions.RemoveAt(0);
        }
        previousPositions.Add(transform.position);
        transform.position += new Vector3(position.x, position.y, 0);
        ValidateGridPosition();
        UpdateRestOfBodyPosition();
    }

    protected void Rotate()
    {
        float angle = 0f;
        Vector2 currentRot = new Vector2(0, transform.forward.z);
        switch (directionFacing)
        {
            case SnakeDirection.Up:
                angle = Vector2.SignedAngle(currentRot, Vector2.up);
                break;
            case SnakeDirection.Down:
                angle = Vector2.SignedAngle(currentRot, Vector2.down);
                break;
            case SnakeDirection.Left:
                angle = Vector2.SignedAngle(currentRot, Vector2.left);
                break;
            case SnakeDirection.Right:
                angle = Vector2.SignedAngle(currentRot, Vector2.right);
                break;
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    protected  void UpdateSnakeLength()
    {
        int factor = 1;
        if (IsScoreBoosted)
            factor = 2;

        if (bodySize == previousPositions.Count - 1)
        {
            ObjectPoolManager.Instance.DeSpawnObject(restOfBody[restOfBody.Count - 1]);
            restOfBody.RemoveAt(restOfBody.Count - 1);
            previousPositions.RemoveAt(0);
            score = score + (-1 * factor);
        }

        else
        {
            GameObject snakeBody = ObjectPoolManager.Instance.SpawnObject(bodyPart);
            restOfBody.Add(snakeBody);
            snakeBody.transform.position = previousPositions[0];
            score = score + (1 * factor);
        }
        OnCollectFood?.Invoke(snakeUI, score);
        if (bodySize == 10)
        {
            IsAlive = false;
            ObjectPoolManager.Instance.objectPools.Clear();
            OnWinSnake?.Invoke(snakeUI);
            Time.timeScale = 0f;
        }
    }

    protected void UpdateRestOfBodyPosition()
    {
        if (bodySize <= 1) return;
        foreach (var op in restOfBody)
        {
            ObjectPoolManager.Instance.DeSpawnObject(op);
        }
        restOfBody.Clear();

        for (int i = bodySize - 1; i > 0; i--)
        {
            GameObject snakeBody = ObjectPoolManager.Instance.SpawnObject(bodyPart);
            snakeBody.transform.position = previousPositions[i];
            restOfBody.Add(snakeBody);
        }
    }

    protected void ValidateGridPosition()
    {
        if (transform.position.x < -GameAssets.Instance.Width)
        {
            transform.position = new Vector3(GameAssets.Instance.Width, transform.position.y, 0);
        }
        else if (transform.position.x > GameAssets.Instance.Width)
        {
            transform.position = new Vector3(-GameAssets.Instance.Width, transform.position.y, 0);
        }
        else if (transform.position.y < -GameAssets.Instance.Height)
        {
            transform.position = new Vector3(transform.position.x, GameAssets.Instance.Height, 0);
        }
        else if (transform.position.y > GameAssets.Instance.Height)
        {
            transform.position = new Vector3(transform.position.x, -GameAssets.Instance.Height, 0);
        }
    }

    public void Death()
    {
        if (IsShielded) return;
        IsAlive = false;
        OnSnakeDeath?.Invoke();
        Time.timeScale = 0f;
        ObjectPoolManager.Instance.objectPools.Clear();
    }

    protected void ChangeMovementSpeed(float multiplier)
    {
        if (multiplier <= 0)
        {
            maxTimerToTakeNextPositionOnGrid = previousGridMaxTimer;
            return;
        }
        previousGridMaxTimer = maxTimerToTakeNextPositionOnGrid;
        maxTimerToTakeNextPositionOnGrid = maxTimerToTakeNextPositionOnGrid / multiplier;
    }

}
