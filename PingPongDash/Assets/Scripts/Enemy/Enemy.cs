using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    //来る頻度の最大値
    float MAX_DISTANCE = 25f;

    float MIN_DISTANCE = 20f;

    //ドアまでの距離
    float distance;

    //足音が聞こえ始める距離
    float audioActiveDistance = 10;

    float MaxWaitTime = 3f;
    float MinWaitTime = 1.5f;


    float waitTime;

    //見ている状態
    public bool isView;

    //足音
    AudioSource audioSource;

    Animator animator;


    void RandomDistance()
    {
        distance = Random.Range(MIN_DISTANCE, MAX_DISTANCE);
    }

    void RandomWait()
    {
        waitTime = Random.Range(MinWaitTime, MaxWaitTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomDistance();

        audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
    }

    public void PingPong()
    {
        distance += -Random.Range(0.1f, 2f);
    }

    void SetAudioVol()
    {
        if(waitTime > 0)
        {
            audioSource.volume = 0;
            return;
        }

        // 距離が `audioActiveDistance` 以内なら、距離に応じて音量を調整
        if (distance < audioActiveDistance)
        {
            float volume = Mathf.Clamp01(1 - (distance / audioActiveDistance)); // 近づくほど 1 に近づく
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0; // それ以上離れたら音を消す
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetAudioVol();

        if (waitTime <= 0)
        {
            isView = false;

            distance += -Time.deltaTime;

            if (distance <= 0)
            {
                RandomWait();
                animator.SetTrigger("Show");
            }

            return;
        }
        else
        {
            isView = true;
            waitTime += -Time.deltaTime;

            if (waitTime <= 0)
            {
                RandomDistance();
                animator.SetTrigger("Out");
            }

            return;
        }

        
    }
}
