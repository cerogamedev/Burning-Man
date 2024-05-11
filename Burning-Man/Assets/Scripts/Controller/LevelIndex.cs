using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class LevelIndex : MonoSingleton<LevelIndex>
    {
        public int Index = 0;
        void Awake()
        {
            if (PlayerPrefs.GetInt("LevelIndex") == 0 || PlayerPrefs.GetInt("LevelIndex") > 10)
            {
                ResetTheIndex();
            }
            else {Index = PlayerPrefs.GetInt("LevelIndex");}
        }
        public void ResetTheIndex()
        {
            PlayerPrefs.SetInt("LevelIndex", 1);
            Index = 1;
        }
        public int GetLevelIndex()
        {
            return Index;
        }
        public void IncreaseLevelIndex(int Increase)
        {
            Index += Increase;
            PlayerPrefs.SetInt("LevelIndex", Index);
        }
    }
}
