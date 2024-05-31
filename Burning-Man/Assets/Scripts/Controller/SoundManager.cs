using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BurningMan
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField] AudioSource music;
        [SerializeField] AudioSource sfxEffect;
        public AudioClip BackroundMusic;
        public Slider MusicSlider, SfxSlider;
        public GameObject OptionCanva;

        public AudioClip Jump, Win, Death,LoseHearth , DestroyTile, DestroyEnemy, ButtonMusic,Fire;
        void Awake()
        {   
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(OptionCanva);

        }
        private void Start()
        {
            music.clip = BackroundMusic;
            music.Play();
            
        }

        void Update()
        {
            music.volume = MusicSlider.value;
            sfxEffect.volume = SfxSlider.value;
        }
        public void PlaySfx(AudioClip clip)
        {
            sfxEffect.PlayOneShot(clip);
        }
    }
}
