using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameOverMenu;

    private void OnEnable() => Actions.OnGameOver += GameOver;
    private void OnDisable() => Actions.OnStartGame -= GameOver;
    
    
    public void StartGame()
    {
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Actions.OnStartGame.Invoke();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        gameOverMenu.SetActive(true);
    }
}
