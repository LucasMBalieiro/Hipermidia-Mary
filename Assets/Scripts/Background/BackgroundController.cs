using System;
using System.Collections;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
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
            transform.Translate(Vector3.left * (moveSpeed * Time.deltaTime));

            if (transform.position.x <= startPosition.x - textureWidth)
            {
                transform.position = startPosition;
            }

            yield return null; 
        }
    }
}