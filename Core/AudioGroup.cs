using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    [Serializable]
    public class AudioGroup
    {
        [SerializeField]
        private AudioEffect[] audioPool;

        AudioGroup(AudioEffect s)
        {
            audioPool = new AudioEffect[1];
            audioPool[0] = s;
        }
        
        public float Set2Source(AudioSource source, float volume)
        {
            return audioPool[Random.Range(0, audioPool.Length)].Set2AudioSource(source, volume);
            
        }
    }
}