using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Play���̏�Ԃ��Ǘ�����N���X
/// </summary>
public class MainGameManager
{

    /*********************�Ǘ�����N���X*********************/

    PlayerAnimation player;
    //TODO ��ƃN���X

    BGMManager bgmManager;

    MainGameUI mainGameUI;



    /*********************�Ǘ�����N���X*********************/

    /*********************Input�N���X*********************/



    /*********************Input�N���X*********************/




    /// <summary>
    /// player��state�������Q�Ƃ��Ď󂯕t������͂Ȃǂ��Ǘ�����
    /// </summary>
    PlayerAnimation.State playerState;


    public MainGameManager(MainGameUI _mainGameUI)
    {
        mainGameUI = _mainGameUI;
    }

    /// <summary>
    /// ����������
    /// </summary>
    public void Load()
    {
        player = Resources.Load<PlayerAnimation>("Prefab/PlayerImage");

    }

    /// <summary>
    /// �펞����
    /// </summary>
    public void Loop()
    {

    }
}
