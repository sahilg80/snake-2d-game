using UnityEngine;

public class SnakeRed : BaseSnake
{
   
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

}
