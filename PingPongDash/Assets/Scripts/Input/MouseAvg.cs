using Kabasawa;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAvg : IMouseMove
{

    private float changeCnt = 0;    // �؂�ւ���
    private Queue<float> secondCnts = new Queue<float>(3);    // ���b��
    private float timeElapsed = 0f; // �o�ߎ���
    private float duration = 1f;    // �Ԋu


    // Start is called before the first frame update
    public MouseAvg()
    {
        MouseInputManager.Instance.mouseMoveInput.Register(this);

        // �������i�L���[��0��3����Ă����j
        for (int i = 0; i < 3; i++)
        {
            secondCnts.Enqueue(0f);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= duration)
        {
            secondCnts.Enqueue(changeCnt); // �ŐV�̃J�E���g��ǉ�

            // �ŐV�̃f�[�^��ǉ�,�L���[��1����
            if (secondCnts.Count > 3)
            {
                secondCnts.Dequeue(); // ��ԌÂ��l���폜
            }

            changeCnt = 0;              // �J�E���g�����Z�b�g����
            timeElapsed = 0f;           // ���Ԃ����Z�b�g����
        }

    }

    public float Get()
    {
        float total = 0f;
        foreach (float count in secondCnts)     // �L���[�z��̃��[�v
        {
            total += count;
        }

        float average = total / secondCnts.Count;

        return Mathf.Clamp(average / 10f, 0f, 1f);
    }

    public override void OnMouseMoveChangeEvent()
    {
        changeCnt++;
    }

}