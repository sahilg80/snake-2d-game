using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake2 : BaseSnake
{
    //[SerializeField]
    //private float maxTimerToTakeNextPositionOnGrid;
    //[SerializeField]
    //private float speedMultiplier;
    //[SerializeField]
    //private SpriteRenderer powerUpCollectedSprite;
    //[SerializeField]
    //private Sprite speedUpSprite;
    //[SerializeField]
    //private Sprite scoreBoostSprite;
    //[SerializeField]
    //private Sprite shieldSprite;
    //private float previousGridMaxTimer;
    //private Vector2Int position;
    //private SnakeDirection directionFacing;
    //private float currentTimer;
    //private int bodySize;
    //public bool IsAlive { get; private set; }
    //private bool isShielded;
    //public bool IsShielded { get => isShielded; set { isShielded = value; powerUpCollectedSprite.sprite = shieldSprite; OnCollectPowerUp?.Invoke(PowerUpType.Shield.ToString()); } }
    //private bool isScoreBoosted;
    //public bool IsScoreBoosted { get => isScoreBoosted; set { isScoreBoosted = value; powerUpCollectedSprite.sprite = scoreBoostSprite; OnCollectPowerUp?.Invoke(PowerUpType.ScoreBoost.ToString()); } }
    //private bool isSpeedBoosted;
    //public bool IsSpeedBoosted
    //{
    //    get
    //    {
    //        return isSpeedBoosted;
    //    }
    //    set
    //    {
    //        isSpeedBoosted = value;
    //        powerUpCollectedSprite.sprite = speedUpSprite;
    //        OnCollectPowerUp?.Invoke(PowerUpType.SpeedUp.ToString());
    //        if (isSpeedBoosted) ChangeMovementSpeed(speedMultiplier);
    //        else ChangeMovementSpeed(0);
    //    }
    //}
    //private bool isPowerActivated;
    //public bool IsPowerUpActivated
    //{
    //    get => isPowerActivated;
    //    set
    //    {
    //        isPowerActivated = value;
    //        powerUpCollectedSprite.enabled = value;
    //        if (!isPowerActivated)
    //        {
    //            OnCollectPowerUp?.Invoke(null);
    //        }
    //    }
    //}

    //public int BodySize
    //{
    //    get
    //    {
    //        return bodySize;
    //    }
    //    set
    //    {
    //        bodySize = value;
    //        UpdateSnakeLength();
    //    }
    //}
    //private List<GameObject> restOfBody;
    //private List<Vector3> previousPositions;
    //public event Action<int> OnCollectFood;
    //public event Action OnSnakeDeath;
    //public event Action OnWinSnake;
    //public event Action<string> OnCollectPowerUp;

    // Start is called before the first frame update
    protected override void Start()
    {
        position = new Vector2Int(GameAssets.Instance.Width, -GameAssets.Instance.Height);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (IsAlive)
        {
            Translate();
        }
    }

    protected override void Translate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && directionFacing != SnakeDirection.Up && directionFacing != SnakeDirection.Down)
        {
            position = new Vector2Int(0, 1);
            SetPosition();
            directionFacing = SnakeDirection.Up;
            Rotate();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && directionFacing != SnakeDirection.Down && directionFacing != SnakeDirection.Up)
        {
            position = new Vector2Int(0, -1);
            SetPosition();
            directionFacing = SnakeDirection.Down;
            Rotate();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && directionFacing != SnakeDirection.Left && directionFacing != SnakeDirection.Right)
        {
            position = new Vector2Int(-1, 0);
            SetPosition();
            directionFacing = SnakeDirection.Left;
            Rotate();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && directionFacing != SnakeDirection.Right && directionFacing != SnakeDirection.Left)
        {
            position = new Vector2Int(1, 0);
            SetPosition();
            directionFacing = SnakeDirection.Right;
            Rotate();
            return;
        }
        currentTimer += Time.deltaTime;
        if (maxTimerToTakeNextPositionOnGrid < currentTimer)
        {
            SetPosition();
        }
    }

    //private void SetPosition()
    //{
    //    currentTimer = 0;
    //    if (bodySize == previousPositions.Count)
    //    {
    //        previousPositions.RemoveAt(0);
    //    }
    //    previousPositions.Add(transform.position);
    //    transform.position += new Vector3(position.x, position.y, 0);
    //    ValidateGridPosition();
    //    UpdateRestOfBodyPosition();
    //}

    //private void Rotate()
    //{
    //    float angle = 0f;
    //    Vector2 currentRot = new Vector2(0, transform.forward.z);
    //    switch (directionFacing)
    //    {
    //        case SnakeDirection.Up:
    //            angle = Vector2.SignedAngle(currentRot, Vector2.up);
    //            break;
    //        case SnakeDirection.Down:
    //            angle = Vector2.SignedAngle(currentRot, Vector2.down);
    //            break;
    //        case SnakeDirection.Left:
    //            angle = Vector2.SignedAngle(currentRot, Vector2.left);
    //            break;
    //        case SnakeDirection.Right:
    //            angle = Vector2.SignedAngle(currentRot, Vector2.right);
    //            break;
    //    }
    //    transform.rotation = Quaternion.Euler(0, 0, angle);
    //}


    //protected override void UpdateSnakeLength()
    //{
    //    int factor = 1;
    //    if (IsScoreBoosted)
    //        factor = 2;

    //    if (bodySize == previousPositions.Count - 1)
    //    {
    //        Debug.Log("decrease snake length");
    //        ObjectPoolManager.Instance.DeSpawnObject(restOfBody[restOfBody.Count - 1]);
    //        restOfBody.RemoveAt(restOfBody.Count - 1);
    //        previousPositions.RemoveAt(0);
    //        OnCollectFood?.Invoke(-1 * factor);
    //    }

    //    else
    //    {
    //        Debug.Log("increase snake length");
    //        GameObject snakeBody = ObjectPoolManager.Instance.SpawnObject(GameAssets.Instance.SnakeBody2);
    //        restOfBody.Add(snakeBody);
    //        snakeBody.transform.position = previousPositions[0];
    //        OnCollectFood?.Invoke(1 * factor);
    //    }
    //    if (bodySize == 10)
    //    {
    //        IsAlive = false;
    //        ObjectPoolManager.Instance.objectPools.Clear();
    //        OnWinSnake?.Invoke();
    //        Time.timeScale = 0f;
    //    }
    //}

    //protected override void UpdateRestOfBodyPosition()
    //{
    //    if (bodySize <= 1) return;
    //    foreach (var op in restOfBody)
    //    {
    //        ObjectPoolManager.Instance.DeSpawnObject(op);
    //    }
    //    restOfBody.Clear();

    //    for (int i = bodySize - 1; i > 0; i--)
    //    {
    //        GameObject snakeBody = ObjectPoolManager.Instance.SpawnObject(GameAssets.Instance.SnakeBody2);
    //        snakeBody.transform.position = previousPositions[i];
    //        restOfBody.Add(snakeBody);
    //    }
    //}

    //private void ValidateGridPosition()
    //{
    //    if (transform.position.x < -GameAssets.Instance.Width)
    //    {
    //        transform.position = new Vector3(GameAssets.Instance.Width, transform.position.y, 0);
    //    }
    //    else if (transform.position.x > GameAssets.Instance.Width)
    //    {
    //        transform.position = new Vector3(-GameAssets.Instance.Width, transform.position.y, 0);
    //    }
    //    else if (transform.position.y < -GameAssets.Instance.Height)
    //    {
    //        transform.position = new Vector3(transform.position.x, GameAssets.Instance.Height, 0);
    //    }
    //    else if (transform.position.y > GameAssets.Instance.Height)
    //    {
    //        transform.position = new Vector3(transform.position.x, -GameAssets.Instance.Height, 0);
    //    }
    //}

    //public override void Death()
    //{
    //    if (IsShielded) return;
    //    IsAlive = false;
    //    OnSnakeDeath?.Invoke();
    //    Time.timeScale = 0f;
    //    ObjectPoolManager.Instance.objectPools.Clear();
    //}

    //protec override void ChangeMovementSpeed(float multiplier)
    //{
    //    if (multiplier <= 0)
    //    {
    //        maxTimerToTakeNextPositionOnGrid = previousGridMaxTimer;
    //        return;
    //    }
    //    previousGridMaxTimer = maxTimerToTakeNextPositionOnGrid;
    //    maxTimerToTakeNextPositionOnGrid = maxTimerToTakeNextPositionOnGrid / multiplier;
    //}

}