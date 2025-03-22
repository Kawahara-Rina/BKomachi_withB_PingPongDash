using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Play中の状態を管理するクラス
/// </summary>
public class MainGameManager
{

    /*********************管理するクラス*********************/

    PlayerAnimation player;
    //TODO 大家クラス

    BGMManager bgmManager;

    MainGameUI mainGameUI;



    /*********************管理するクラス*********************/

    /*********************Inputクラス*********************/



    /*********************Inputクラス*********************/




    /// <summary>
    /// playerのstateここを参照して受け付ける入力などを管理する
    /// </summary>
    PlayerAnimation.State playerState;


    public MainGameManager(MainGameUI _mainGameUI)
    {
        mainGameUI = _mainGameUI;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Load()
    {
        player = Resources.Load<PlayerAnimation>("Prefab/PlayerImage");

    }

    /// <summary>
    /// 常時処理
    /// </summary>
    public void Loop()
    {

    }
}
