using UnityEngine;
using UnityEngine.Serialization;

public class ParticleCleanup : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    
    private void Start()
    {
        float totalDuration = particle.main.duration + particle.main.startLifetime.constantMax;
        
        Destroy(gameObject, totalDuration);
    }
}