using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    
    private static readonly int IsRunningKey = Animator.StringToHash("isRunning");
    private static readonly int AttackKey = Animator.StringToHash("Attack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Actions.OnStartGame += HandleStartGame;
        Actions.OnGameOver += HandleGameOver;
        Actions.OnAttack += TriggerAttack;
    }

    private void OnDisable()
    {
        Actions.OnStartGame -= HandleStartGame;
        Actions.OnGameOver -= HandleGameOver;
        Actions.OnAttack -= TriggerAttack;
    }

    private void HandleStartGame()
    {
        animator.SetBool(IsRunningKey, true);
    }

    private void HandleGameOver()
    {
        animator.SetBool(IsRunningKey, false);
    }
    
    private void TriggerAttack()
    {
        if (animator.GetBool(IsRunningKey))
        {
            animator.SetTrigger(AttackKey);
        }
    }
}