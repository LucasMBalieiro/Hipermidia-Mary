using System.Collections;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Sprite[] sprites;

    public void Initialize(EnemyScriptableObject enemySO)
    {
        sprites = enemySO.animationFrames;
        StartCoroutine(AnimationLoop());
    }

    private IEnumerator AnimationLoop()
    {
        int currentFrame = 0;
        
        while (true)
        {
            spriteRenderer.sprite = sprites[currentFrame];

            yield return new WaitForSeconds(0.5f);

            currentFrame++;
            if (currentFrame >= sprites.Length)
            {
                currentFrame = 0;
            }
        }
    }
    
}
