using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public struct ScoreRankingConfig
{
    public int saveDataCnt;
    public string key;

    public ScoreRankingConfig(string _key,int _saveCnt)
    {
        this.key = _key;
        this.saveDataCnt = _saveCnt;
    }
}


public class ScoreRanking
{
    /// <summary>
    /// 渡された値がハイスコアか検証し保存処理を行う
    /// </summary>
    /// <param name="_config">ランキングの設定データ</param>
    /// <param name="_score">登録データ</param>
    public static bool Save(ScoreRankingConfig _config, float _score)
    {

        bool saved = false;

        List<float> scores = LoadRanking(_config);

        for(int i = 0; i < _config.saveDataCnt; i++)
        {
            if (scores[i] < _score )
            {
                saved = true;
                scores.Insert(i, _score);
                
                if(scores.Count > _config.saveDataCnt)
                {
                    scores.RemoveAt(scores.Count - 1);
                }

                break;
            }
        }

        for (int i = 0; i < _config.saveDataCnt; i++)
        {
            PlayerPrefs.SetFloat(_config.key + i, scores[i]);
        }

        return saved;

    }

    public static List<float> LoadRanking(ScoreRankingConfig _config)
    {
        List<float> scores = new List<float>();

        for(int i=0;i<_config.saveDataCnt;i++)
        {
            scores.Add(PlayerPrefs.GetFloat(_config.key + i,0));
        }

        return scores;
    }



}
