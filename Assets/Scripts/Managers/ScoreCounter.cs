using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int totalScore = 0;


    private void OnEnable()
    {
        Actions.OnScoreChange += AddScore;
        Actions.OnStartGame += ResetScore;
        Actions.OnGameOver += UpdateHighScore;
    }

    private void OnDisable()
    {
        Actions.OnScoreChange -= AddScore;
        Actions.OnStartGame -= ResetScore;
        Actions.OnGameOver -= UpdateHighScore;
    } 
        
    
    private void AddScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        scoreText.text = totalScore.ToString();
    }

    private void UpdateHighScore()
    {
        GameManager.Instance.CompareHighScore(totalScore);
    }

    private void ResetScore()
    {
        totalScore = 0;
        UpdateVisual();
    }
}
