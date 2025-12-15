using UnityEngine;

public enum EnemyType
{
    Ground,
    Flying,
    Boss,
}

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptable Objects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public GestureName[] gestureNames;
    public Sprite[] animationFrames;
    public EnemyType enemyType;
    public int scorePoints;

    public float speedMultiplier = 1;
}
