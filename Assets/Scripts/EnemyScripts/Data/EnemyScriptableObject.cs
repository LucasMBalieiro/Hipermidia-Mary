using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptable Objects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public int minRangeGesture;
    public int maxRangeGesture;
    public Sprite[] animationFrames;
    public float yPosition;
    public int scorePoints;

    public float speedMultiplier = 1;
}
