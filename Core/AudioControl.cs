using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Gameplay;
using Sirenix.Utilities;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class AudioControl
    {
        public float globalVolume = 1;
        class AudioSourcePack
        {
            public AudioSource Source;
            public float originalVolume;
            public bool isPlaying => Source.isPlaying;

            public float Volume
            {
                set => Source.volume = value;
                get => Source.volume;
            }

            public AudioSourcePack(AudioSource source, float volume)
            {
                Source = source;
                originalVolume = volume;
            }

            public void DOFade(float a, float b)
            {
                Source.DOFade(a,b);
            }

            public void Play()
            {
                Source.Play();
            }
            
            public void Stop()
            {
                Source.Stop();
            }
            
            public void Pause()
            {
                Source.Pause();
            }

            public void UnPause()
            {
                Source.UnPause();
            }
        }
        
        private List<AudioSourcePack> _sources = new List<AudioSourcePack>();
        [HideInInspector]
        public bool isPaused;
        [SerializeField] bool isRecycle = true;

        public void Check()
        {
            if (isPaused || !isRecycle) return;
            AudioManager manager = AudioManager.Instance;
            _sources.RemoveAll(s =>
            {
                if (s == null) return true;
                if (s.isPlaying) return false;
                manager.ReturnToSource(s.Source);
                return true;
            });
        }

        public void ChangeVolume(float ratio)
        {
            globalVolume = ratio;
            _sources.ForEach(p =>
            {
                if(p == null) return;
                p.Volume = ratio * p.originalVolume;
            });
        }
        public void AddGroup(AudioGroup audioGroup)
        {
            var s = AudioManager.Instance.GetFromPool();
            _sources.Add(new AudioSourcePack(s,0));
            _sources.Last().originalVolume = audioGroup.Set2Source(_sources.Last().Source, globalVolume);
        }
        
        public void AddGroupAndPlay(AudioGroup audioGroup)
        {
            AddGroup(audioGroup);
            _sources.Last().Play();
        }
        
        public void AddGroupAndGradualPlay(AudioGroup audioGroup)
        {
            AddGroup(audioGroup);
            var s = _sources.Last();
            var t = s.Volume;
            s.Volume = 0;
            s.Play();
            s.DOFade(t, 1f);
        }

        public void GradualPlay()
        {
            _sources.ForEach(s=>
            {
                var t = s.Volume;
                s.Volume = 0;
                s.DOFade(t, 1f);
            });
        }
        
        public void GradualStop()
        {
            _sources.ForEach(s=>
            {
                s.DOFade(0, 1f);
            });
        }

        public void Stop()
        {
            isPaused = false;
            _sources.ForEach(s=>s.Stop());
        } 

        public void Play()
        {
            isPaused = false;
            _sources.ForEach(s => s.Play());
        }

        public void Pause()
        {
            isPaused = true;
            _sources.ForEach(s => s.Pause());
        }

        public void Resume()
        {
            isPaused = false;
            _sources.ForEach(s=>s.UnPause());
        }
    }
}