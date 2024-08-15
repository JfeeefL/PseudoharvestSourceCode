using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class AudioEffect
    {
        public AudioClip clip;
        public float pitch;
        public float volume;
        public bool isLoop;

        public float Set2AudioSource(AudioSource source, float gbVolume)
        {
            source.clip = clip;
            source.pitch = pitch;
            source.volume = volume * gbVolume;
            source.loop = isLoop;
            return volume;
        }
    }
}