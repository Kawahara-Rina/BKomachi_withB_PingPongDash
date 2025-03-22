using Kabasawa;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAvg : IMouseMove
{

    private float changeCnt = 0;    // 切り替わり回数
    private Queue<float> secondCnts = new Queue<float>(3);    // 毎秒回数
    private float timeElapsed = 0f; // 経過時間
    private float duration = 1f;    // 間隔


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

        return Mathf.Clamp(average / 10f, 0f, 1f);
    }

    public override void OnMouseMoveChangeEvent()
    {
        changeCnt++;
    }

}