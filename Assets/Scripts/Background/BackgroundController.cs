using System;
using System.Collections;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] private float parallaxFactor = 1f;
    
    private float textureWidth;
    private Vector3 startPosition;

    private Coroutine coroutine;

    private void Start()
    {
        textureWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        Actions.OnStartGame += StartBackground;
        Actions.OnGameOver += StopBackground;
    }

    private void OnDisable()
    {
        Actions.OnStartGame -= StartBackground;
        Actions.OnGameOver -= StopBackground;
    }

    private void StartBackground()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        
        coroutine = StartCoroutine(Scroll());
    }

    private void StopBackground()
    {
        if (coroutine == null) return;
        
        StopCoroutine(coroutine);
        coroutine = null;
    }

    private IEnumerator Scroll()
    {
        while (true)
        {
            float currentGlobalSpeed = GameManager.Instance.CurrentGameSpeed;
            
            float finalSpeed = currentGlobalSpeed * parallaxFactor;

            transform.Translate(Vector3.left * (finalSpeed * Time.deltaTime));

            if (transform.position.x <= startPosition.x - textureWidth)
            {
                transform.position = startPosition;
            }

            yield return null; 
        }
    }
}