using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BurningMan
{
    public class DeadSceneManager : MonoSingleton<DeadSceneManager>
    {
        public GameObject DeadCanva;
        public int DeadCount=0;
        public TextMeshProUGUI _deadCount;
        public TextMeshProUGUI _sceneInfo;
        public float TimeCount=0f;
        public TextMeshProUGUI _timeInfo;
        void Start()
        {
            DeadCanva.SetActive(false);
        }

        void Update()
        {
            TimeCount +=Time.deltaTime;
        }
        public void Death()
        {
            DeadCanva.SetActive(true);
            _deadCount.text = "You killed " + DeadCount.ToString() + " thing!";
            _sceneInfo.text = "You died in level " + SceneManager.sceneCount.ToString() + " - episode "+ LevelIndex.Instance.Index.ToString();
            _timeInfo.text = "You survived "+ TimeCount.ToString() + " seconds!";
            SoundManager.Instance.PlaySfx(SoundManager.Instance.Death);
        }
        public void StartAgain()
        {
            LevelIndex.Instance.ResetTheIndex();
            DeadCount = 0;
            TimeCount = 0;
            DeadCanva.SetActive(false);

            SceneLoader.Instance.LoadSceneThisIndex(1);

        }
    }
}
