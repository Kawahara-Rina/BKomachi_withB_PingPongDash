using Kabasawa;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAvg : IMouseMove
{

    private float changeCnt = 0;    // 切り替わり回数
    private Queue<float> secondCnts = new Queue<float>();    // 毎秒回数
    private float timeElapsed = 0f; // 経過時間
    private float duration = 0.5f;    // 間隔


    // Start is called before the first frame update
    public MouseAvg()
    {
        MouseInputManager.Instance.mouseMoveInput.Register(this);

        // 初期化（キューに0を3つ入れておく）
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
            secondCnts.Enqueue(changeCnt); // 最新のカウントを追加

            // 最新のデータを追加,キューを1つ抜く
            if (secondCnts.Count > 3)
            {
                secondCnts.Dequeue(); // 一番古い値を削除
            }

            changeCnt = 0;              // カウントをリセットする
            timeElapsed = 0f;           // 時間をリセットする
        }

    }

    public float Get()
    {
        float total = 0f;
        foreach (float count in secondCnts)     // キュー配列のループ
        {
            total += count;
        }

        float average = total / secondCnts.Count;

        float t = Mathf.InverseLerp(0, 14f, average);
        float easedT = Mathf.Pow(t, 0.5f); // 0.5f以下にすると後半の上がり幅が小さい
        float result = Mathf.Lerp(0, 1.3f, easedT);

        return result;
    }

    public override void OnMouseMoveChangeEvent()
    {
        changeCnt++;
    }

}