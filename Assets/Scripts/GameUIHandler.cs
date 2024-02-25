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
    TextMeshProUGUI scoreUIText;
    [SerializeField]
    Snake snake;
    [SerializeField]
    GameObject pauseText;
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    GameObject gameWinPanel;
    private Button stateButton;
    private Image stateButtonImage;
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreUIText.SetText(score.ToString());
        }
    }
    private GameState currentState;
    public GameState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }

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
        snake.OnSnakeDeath += OnSnakeDead;
        snake.OnCollectFood += UpdateScore;
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
        Debug.Log("curent state " + currentState);
    }

    private void UpdateScore(int val)
    {
        score = score + val;
        scoreUIText.SetText(score.ToString());
    }

    private void OnDisable()
    {
        stateButton.onClick.RemoveAllListeners();
        snake.OnSnakeDeath -= OnSnakeDead;
        snake.OnCollectFood -= UpdateScore;
    }

    private void OnSnakeDead()
    {
        gameOverPanel.SetActive(true);
    }

    private void OnSnakeWin()
    {
        gameWinPanel.SetActive(true);
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
