using System;
using System.Collections.Generic;
using Core;
using Core.Core;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

namespace Gameplay
{
    public class AudioManager : MonoBehaviour
    {
        [Tooltip("Add Clips to Start key to play at the beginning")]
        public SerializedDictionary<string,AudioControl> AudioControls = new SerializedDictionary<string, AudioControl>();

        public SerializedDictionary<string, AudioGroup> AudioGroups = new SerializedDictionary<string, AudioGroup>();

        public static AudioManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            AudioControls.ForEach(s => s.Value.Check());
        }
        
        public void ChangeVolume(string channel, float volume)
        {
            AudioControls[channel].ChangeVolume(volume/0.6f);
        }

        private void Start()
        {
            ChangeVolume("Music", UIManager.Instance.MusicSlider.value);
            ChangeVolume("SFX", UIManager.Instance.VFXSlider.value);
        }

        public void PlayInController(string group, string control)
        {
            AudioControls[control].AddGroupAndPlay(AudioGroups[group]);
        }
        
        public void GradualPlayInController(string group, string control)
        {
            AudioControls[control].AddGroupAndGradualPlay(AudioGroups[group]);
        }
        public void Play(string s) => AudioControls[s].Play();
        public void Play() => AudioControls.ForEach(s => s.Value.Play());

        public void GradualPlay(string s) => AudioControls[s].GradualPlay();
        public void GradualPlay() => AudioControls.ForEach(s=>s.Value.GradualPlay());
        
        public void GradualStop(string s) => AudioControls[s].GradualStop();
        public void GradualStop() => AudioControls.ForEach(s=>s.Value.GradualStop());

        public void Pause(string s) => AudioControls[s].Pause();
        public void Pause() => AudioControls.ForEach(s => s.Value.Pause());

        public void Resume(string s) => AudioControls[s].Resume();
        public void Resume() => AudioControls.ForEach(s => s.Value.Resume());

        public void Stop(string s) => AudioControls[s].Stop();
        public void Stop() => AudioControls.ForEach(s => s.Value.Stop());
 
        
        private Queue<AudioSource> _pool = new Queue<AudioSource>();

        public AudioSource GetFromPool()
        {
            return _pool.Count == 0 ? gameObject.AddComponent<AudioSource>() : _pool.Dequeue();
        }
        
        public void ReturnToSource(AudioSource source)
        {
            source.clip = null;
            _pool.Enqueue(source);
        }
    }
}