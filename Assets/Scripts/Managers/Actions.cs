using System;
using UnityEngine;

public static class Actions
{
    public static Action<int> OnScoreChange;
    
    public static Action OnStartGame;
    public static Action OnGameOver;
    public static Action OnAttack;
}
