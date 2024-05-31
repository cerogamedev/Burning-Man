using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BurningMan
{
    public class StartManager : MonoBehaviour
    {
        public Button StartButton;
        public Button OptionButton;
        public Button CloseOptionButton;
        public GameObject OptionsCanva;

        private SoundManager sound;

        void Start()
        {
            sound = SoundManager.Instance;
            StartButton.onClick.AddListener(() => StartButtonVoid());
            OptionButton.onClick.AddListener(() => StartOption());
            CloseOptionButton.onClick.AddListener(() => CloseOption());

        }
        public void StartButtonVoid()
        {
            sound.PlaySfx(sound.ButtonMusic);
            LevelIndex.Instance.ResetTheIndex();
            SceneLoader.Instance.LoadSceneThisIndex(1);
        }
        public void StartOption()
        {
            sound.PlaySfx(sound.ButtonMusic);

            OptionsCanva.SetActive(true);
        }
        public void CloseOption()
        {
            sound.PlaySfx(sound.ButtonMusic);

            OptionsCanva.SetActive(false);
        }
    }
}
