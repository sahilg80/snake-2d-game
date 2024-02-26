using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField]
    GameObject GameStateButton;
    [SerializeField]
    Sprite pauseSprite;
    [SerializeField]
    Sprite resumeSprite;
    [SerializeField]
    GameObject pauseText;
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    GameObject gameWinPanel;

    [SerializeField]
    private BaseSnake snakeRed;
    [SerializeField]
    private BaseSnake snakeGreen;

    private Button stateButton;
    private Image stateButtonImage;
    private int score;
    public int Score
    {
        get { return score; }
        private set
        {
            score = value;
        }
    }
    private GameState currentState;
    private void Awake()
    {
        stateButton = GameStateButton.GetComponent<Button>();
        stateButtonImage = GameStateButton.GetComponent<Image>();
    }

    private void Start()
    {
        currentState = GameState.Playing;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        stateButton.onClick.AddListener(ChangeGameState);
        InitializeSnakeUI(snakeGreen);
        InitializeSnakeUI(snakeRed);
    }

    private void InitializeSnakeUI(BaseSnake snake)
    {
        snake.OnSnakeDeath += OnSnakeDead;
        snake.OnCollectFood += UpdateScore;
        snake.OnCollectPowerUp += OnPowerUpCollected;
        snake.OnWinSnake += OnSnakeWin;
    }

    private void ChangeGameState()
    {
        switch (currentState)
        {
            case GameState.Paused:
                currentState = GameState.Playing;
                Time.timeScale = 1;
                stateButtonImage.sprite = (Sprite)pauseSprite;
                pauseText.SetActive(false);
                break;
            case GameState.Playing:
                currentState = GameState.Paused;
                Time.timeScale = 0;
                stateButtonImage.sprite = (Sprite)resumeSprite;
                pauseText.SetActive(true);
                break;
        }
    }

    private void UpdateScore(SnakeUI snakeUI, float scoreVal)
    {
        snakeUI.ScoreCountText.SetText(scoreVal.ToString());
    }

    private void OnDisable()
    {
        stateButton.onClick.RemoveAllListeners();
        DeInitializeSnakeUI(snakeGreen);
        DeInitializeSnakeUI(snakeRed);
    }

    private void DeInitializeSnakeUI(BaseSnake snake)
    {
        snake.OnSnakeDeath -= OnSnakeDead;
        snake.OnCollectFood -= UpdateScore;
        snake.OnCollectPowerUp -= OnPowerUpCollected;
        snake.OnWinSnake -= OnSnakeWin;
    }


    private void OnSnakeDead()
    {
        gameOverPanel.SetActive(true);
    }

    private void OnSnakeWin(SnakeUI snakeUI)
    {
        gameWinPanel.SetActive(true);
        snakeUI.GameWinTitle.SetActive(true);
    }

    public void OnPowerUpCollected(SnakeUI snakeUI, string power)
    {
        snakeUI.CollectedPowerUpText.SetText(power);
    }

    public void OnClickRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OnClickMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void OnClickNext()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}


[System.Serializable]
public class SnakeUI
{
    public TextMeshProUGUI CollectedPowerUpText;
    public TextMeshProUGUI ScoreCountText;
    public GameObject GameWinTitle;
}