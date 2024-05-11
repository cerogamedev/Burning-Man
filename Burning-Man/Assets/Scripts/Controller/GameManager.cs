using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BurningMan
{
    public class GameManager : MonoSingleton<GameManager>
    {
        void OnEnable()
        {
            EndLevel._levelEnd += LevelEnds;
        }

        private void LevelEnds()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (LevelIndex.Instance.GetLevelIndex() == 11) 
            {
                SceneLoader.Instance.LoadSceneThisIndex(sceneIndex+1);
            }
            else 
            {
                SceneLoader.Instance.LoadSceneThisIndex(sceneIndex);
            }
        }
        void OnDisable()
        {
            EndLevel._levelEnd -= LevelEnds;
        }
    }
}
