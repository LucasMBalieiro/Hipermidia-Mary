using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameOverMenu;

    private void OnEnable() {
        Actions.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        Actions.OnGameOver -= GameOver;
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
}
