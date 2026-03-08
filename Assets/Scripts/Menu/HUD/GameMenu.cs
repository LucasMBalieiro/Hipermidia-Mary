using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameOverMenu;
    
    [SerializeField] private TextMeshProUGUI highscoreText;

    private void OnEnable() {
        Actions.OnGameOver += GameOver;
        Actions.OnHighscoreUpdate += SetHighScore;
    }

    private void OnDisable()
    {
        Actions.OnGameOver -= GameOver;
        Actions.OnHighscoreUpdate -= SetHighScore;
    }
    
    
    public void StartGame()
    {
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        SnapshotActions.SetDefaultFilter.Invoke();
        Actions.OnStartGame.Invoke();
    }

    public void BackToMenu()
    {
        SnapshotActions.SetDefaultFilter.Invoke();
        SceneManager.LoadScene(0);
    }
    
    private void GameOver()
    {
        SnapshotActions.SetMuffledFilter.Invoke();
        gameOverMenu.SetActive(true);
    }

    private void SetHighScore()
    {
        highscoreText.text = "Highscore: " + GameManager.Instance.HighScore.ToString();
    }
}
