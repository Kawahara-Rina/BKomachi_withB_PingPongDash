using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    //����p�x�̍ő�l
    float MAX_DISTANCE = 25f;

    float MIN_DISTANCE = 20f;

    //�h�A�܂ł̋���
    float distance;

    //�������������n�߂鋗��
    float audioActiveDistance = 10;

    float MaxWaitTime = 3f;
    float MinWaitTime = 1.5f;


    float waitTime;

    //���Ă�����
    public bool isView;

    //����
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

        // ������ `audioActiveDistance` �ȓ��Ȃ�A�����ɉ����ĉ��ʂ𒲐�
        if (distance < audioActiveDistance)
        {
            float volume = Mathf.Clamp01(1 - (distance / audioActiveDistance)); // �߂Â��ق� 1 �ɋ߂Â�
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0; // ����ȏ㗣�ꂽ�特������
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
