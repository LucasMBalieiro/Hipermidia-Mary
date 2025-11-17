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
    }

    private void OnDisable()
    {
        Actions.OnScoreChange -= AddScore;
        Actions.OnStartGame -= ResetScore;
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

    private void ResetScore()
    {
        totalScore = 0;
        UpdateVisual();
    }
}
