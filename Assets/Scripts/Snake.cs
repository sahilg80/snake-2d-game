using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int position;
    private SnakeDirection directionFacing;
    private float currentTimer;
    private int bodySize;
    public int BodySize { 
        get { 
            return bodySize; 
        }
        set {
            bodySize = value;
            UpdateSnakeLength();
        }
    }
    private List<GameObject> restOfBody;
    private List<Vector3> previousPositions;
    [SerializeField]
    private float gridMaxTimer;

    // Start is called before the first frame update
    void Start()
    {
        position = Vector2Int.zero;
        transform.position = new Vector3(position.x, position.y, 0);
        position = new Vector2Int(0, 1);
        restOfBody = new List<GameObject>();
        previousPositions = new List<Vector3>();
        bodySize = 1;
        FoodManager.Instance.SpawnFood(GameAssets.Instance.MassGainer);
    }

    // Update is called once per frame
    void Update()
    {
        Translate();
    }

    private void Translate()
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
        if (gridMaxTimer < currentTimer)
        {
            SetPosition();
        }
    }

    private void SetPosition()
    {
        currentTimer = 0;
        if(bodySize == previousPositions.Count)
        {
            previousPositions.RemoveAt(0);
        }
        previousPositions.Add(transform.position);
        transform.position += new Vector3(position.x, position.y, 0);
        UpdateRestOfBodyPosition();
    }

    private void Rotate()
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

    
    private void UpdateSnakeLength()
    {
        if (bodySize == previousPositions.Count - 1)
        {
            Debug.Log("decrease snake length");
            ObjectPoolManager.Instance.DeSpawnObject(restOfBody[restOfBody.Count - 1]);
            restOfBody.RemoveAt(restOfBody.Count - 1);
            previousPositions.RemoveAt(0);
        }

        else
        {
            Debug.Log("increase snake length");
            GameObject snakeBody = ObjectPoolManager.Instance.SpawnObject(GameAssets.Instance.SnakeBody);
            restOfBody.Add(snakeBody);
            snakeBody.transform.position = previousPositions[0];
        }
    }

    private void UpdateRestOfBodyPosition()
    {
        if (bodySize <= 1) return;
        foreach (var op in restOfBody)
        {
            ObjectPoolManager.Instance.DeSpawnObject(op);
        }
        restOfBody.Clear();

        for (int i=bodySize-1; i > 0; i--)
        {
            GameObject snakeBody = ObjectPoolManager.Instance.SpawnObject(GameAssets.Instance.SnakeBody);
            snakeBody.transform.position = previousPositions[i];
            restOfBody.Add(snakeBody);
        }
    }

}
