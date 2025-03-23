using Kabasawa;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAvg : IMouseMove
{

    private float changeCnt = 0;    // �؂�ւ���
    private Queue<float> secondCnts = new Queue<float>();    // ���b��
    private float timeElapsed = 0f; // �o�ߎ���
    private float duration = 0.5f;    // �Ԋu


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

        float t = Mathf.InverseLerp(0, 14f, average);
        float easedT = Mathf.Pow(t, 0.5f); // 0.5f�ȉ��ɂ���ƌ㔼�̏オ�蕝��������
        float result = Mathf.Lerp(0, 1.3f, easedT);

        return result;
    }

    public override void OnMouseMoveChangeEvent()
    {
        changeCnt++;
    }

}