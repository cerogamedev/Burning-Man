using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BurningMan
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        public GameObject loaderUI;
        public Slider ProgressSlider;
        void Awake()
        {
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(loaderUI);
        }

        public void LoadSceneThisIndex(int _index)
        {
            StartCoroutine(LoadScene_Coroutine(_index));
        }
        public IEnumerator LoadScene_Coroutine(int index) 
        {
            ProgressSlider.value = 0;
            loaderUI.SetActive(true);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;
            float progress = 0;
            while(!asyncOperation.isDone)
            {
                progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
                ProgressSlider.value = progress;
                if (progress >= 0.9f)
                {
                    ProgressSlider.value = 1;
                    asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }

        }
    }
}
