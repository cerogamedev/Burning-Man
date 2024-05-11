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

        void Start()
        {
            StartButton.onClick.AddListener(() => StartButtonVoid());
        }
        public void StartButtonVoid()
        {
            LevelIndex.Instance.ResetTheIndex();
            SceneLoader.Instance.LoadSceneThisIndex(1);
        }
    }
}
