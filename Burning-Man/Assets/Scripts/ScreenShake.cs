using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class ScreenShake : MonoSingleton<ScreenShake>
    {
        
        public IEnumerator StartShake(float duration, float magnitude)
        {
            Vector3 originalPos = transform.localPosition;
            float elapsed = 0.0f;
            while (elapsed<duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;
                transform.localPosition = new Vector3 (transform.position.x+x, transform.position.y+y, transform.position.z);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = new Vector3(0f, originalPos.y, originalPos.z);
        }
    }
}
