using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace Audio
{
    public class SoundManager : MonoBehaviour
    {
        #region Singleton
            public static SoundManager Instance {get; private set;}
            private void Awake()
            {
                if (Instance != null && Instance != this)
                {
                    Destroy(this.gameObject);
                    return;
                }
                
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        #endregion
        
        private IObjectPool<SoundEmitter> soundEmitterPool;
        readonly List<SoundEmitter> activeSoundEmitters = new();
        public readonly LinkedList<SoundEmitter> FrequentSoundEmitters = new();
        
        [SerializeField] private SoundEmitter soundEmitterPrefab;
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxPoolSize = 32;
        [SerializeField] private int maxSoundInstances = 10;

        private void Start()
        {
            InitializePool();
            InitializeSnapShot();
            GameManager.Instance.PlayMusic();
        }

        public SoundBuilder CreateSound() => new SoundBuilder(this);

        public bool CanPlaySound(SoundData soundData)
        {
            if (!soundData.frequentSound) return true;

            if (FrequentSoundEmitters.Count >= maxSoundInstances) {
                try {
                    FrequentSoundEmitters.First.Value.Stop();
                    return true;
                } catch {
                    Debug.Log("Error: SoundManager.CanPlaySound");
                }
                return false;
            }
            return true;
        }

        public SoundEmitter GetSoundEmitter()
        {
            return soundEmitterPool.Get();
        }

        public void ReturnSoundEmitter(SoundEmitter soundEmitter)
        {
            soundEmitterPool.Release(soundEmitter);
        }
        
        #region Snapshot Filter
        
            [SerializeField] private AudioMixer mainMixer;
            private const string SnapshotUnpaused = "Unpaused";
            private const string SnapshotPaused = "Paused";

            private AudioMixerSnapshot defaultSnapshot;
            private AudioMixerSnapshot dreamySnapshot;

            private void InitializeSnapShot()
            {
                defaultSnapshot = mainMixer.FindSnapshot(SnapshotUnpaused);
                dreamySnapshot = mainMixer.FindSnapshot(SnapshotPaused);
            }
            
            public void SetDefaultFilter() => defaultSnapshot.TransitionTo(0.5f);
            public void SetDreamyFilter() => dreamySnapshot.TransitionTo(0.5f);
        
        #endregion
        
        #region ObjectPool
            
        private void InitializePool()
            {
                soundEmitterPool = new ObjectPool<SoundEmitter>(
                    CreateSoundEmitter,
                    OnGetSoundEmitter,
                    OnReleaseSoundEmitter,
                    OnDestroySoundEmitter,
                    collectionCheck,
                    defaultCapacity,
                    maxPoolSize
                );
        }

        private SoundEmitter CreateSoundEmitter()
        {
            SoundEmitter soundEmitter = Instantiate(soundEmitterPrefab, transform, true);
            soundEmitter.gameObject.SetActive(false);
            return soundEmitter;
        }

        private void OnGetSoundEmitter(SoundEmitter obj)
        {
            obj.gameObject.SetActive(true);
            activeSoundEmitters.Add(obj);
        }

        private void OnReleaseSoundEmitter(SoundEmitter obj)
        {
            obj.gameObject.SetActive(false);
            activeSoundEmitters.Remove(obj);
        }

        private void OnDestroySoundEmitter(SoundEmitter obj)
            {
                if (obj != null) Destroy(obj.gameObject);
            }
            
        #endregion
    }
}