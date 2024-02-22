using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int position;
    //[SerializeField]
    private Direction gridPosition;
    private float currentTimer;
    [SerializeField]
    private float gridMaxTimer;

    // Start is called before the first frame update
    void Start()
    {
        position = Vector2Int.zero;
        transform.position = new Vector3(position.x, position.y, 0);
        position = new Vector2Int(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && gridPosition != Direction.Up)
        {
            position = new Vector2Int( 0, 1);
            transform.position += new Vector3(position.x, position.y, 0);
            currentTimer = 0;
            gridPosition = Direction.Up;
            Rotate();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && gridPosition != Direction.Down)
        {
            position = new Vector2Int(0, -1);
            transform.position += new Vector3(position.x, position.y, 0);
            currentTimer = 0;
            gridPosition = Direction.Down;
            Rotate();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && gridPosition != Direction.Left)
        {
            position = new Vector2Int(-1,0);
            transform.position += new Vector3(position.x, position.y, 0);
            currentTimer = 0;
            gridPosition = Direction.Left;
            Rotate();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && gridPosition != Direction.Right)
        {
            position = new Vector2Int(1, 0);
            transform.position += new Vector3(position.x, position.y, 0);
            currentTimer = 0;
            gridPosition = Direction.Right;
            Rotate();
            return;
        }
        currentTimer += Time.deltaTime;
        if(gridMaxTimer < currentTimer)
        {
            currentTimer = 0;
            transform.position += new Vector3(position.x, position.y, 0);
        }
    }

    private void Rotate()
    {
        float angle = 0f;
        Vector2 currentRot = new Vector2(0, transform.forward.z);
        Debug.Log("pos" + currentRot);
        switch (gridPosition)
        {
            case Direction.Up:
                angle = Vector2.SignedAngle(currentRot, Vector2.up);
                //RotateTo(Vector2.up);
                //transform.rotation = Quaternion.Euler(90,0,0);
                break;
            case Direction.Down:
                angle = Vector2.SignedAngle(currentRot, Vector2.down);
                //RotateTo(Vector2.down);
                break;
            case Direction.Left:
                angle = Vector2.SignedAngle(currentRot, Vector2.left);
                //RotateTo(Vector2.left);
                break;
            case Direction.Right:
                angle = Vector2.SignedAngle(currentRot, Vector2.right);
                //RotateTo(Vector2.right);
                break;
        }
        Debug.Log("angle " + angle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void RotateTo(Vector2 direction)
    {
        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1);
    }

}

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}