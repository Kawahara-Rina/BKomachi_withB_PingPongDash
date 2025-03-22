using Kabasawa;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
/// Play中の状態を管理するクラス
/// </summary>
public class MainGameManager :  IMouseMove, IMouseScroll
{

    /*********************管理するクラス*********************/

    PlayerAnimation player;
    //TODO 大家クラス
    BGMManager bgmManager;

    MainGameUI mainGameUI;


    /*********************管理するクラス*********************/

    /*********************Inputクラス*********************/

    MouseInputManager mouse;

    /*********************Inputクラス*********************/


    int score = 0;

    public MainGameManager(PlayerAnimation player, BGMManager bgmManager, MainGameUI mainGameUI)
    {
        this.player = player;
        this.bgmManager = bgmManager;
        this.mainGameUI = mainGameUI;

        mouse = MouseInputManager.Instance;

        mouse.mouseScroll.Register(this);
        mouse.mouseMoveInput.Register(this);


        player.ChangeState(PlayerAnimation.State.PUSH);
    }

    /// <summary>
    /// 常時処理
    /// </summary>
    public void Loop()
    {
        switch(player.state)
        {
            case PlayerAnimation.State.IDLE:
                IDLE();
                break;

            case PlayerAnimation.State.PUSH:
                PUSH();
                break;

            case PlayerAnimation.State.DASH:
                DASH();
                break;
        }
    }

    void IDLE()
    {
        mainGameUI.ChangeMouseGUI(Common.STATE_PUSH);

        if(mouse.mouseAvg.Get() >= 0.2f)
        {
            player.ChangeState(PlayerAnimation.State.PUSH);
        }
    }

    void PUSH()
    {
        Debug.Log("PUSH処理を実行");
        //TODO 妥協なのできれいにできたらする
        mainGameUI.ChangeMouseGUI(Common.STATE_PUSH);

        if (mouse.mouseAvg.Get() < 0.2f)
        {
            player.ChangeState(PlayerAnimation.State.IDLE);
            player.pushSpeed = 0;
        }
        else
        {
            player.pushSpeed = mouse.mouseAvg.Get();
        }
    }

    void DASH()
    {
        Debug.Log("DASH処理を実行");
    }

    public void OnScrollDown()
    {
        //Debug.Log("HIDE処理を実行");

        if (
            player.state == PlayerAnimation.State.IDLE ||
            player.state == PlayerAnimation.State.PUSH ||
            player.state == PlayerAnimation.State.DASH 
            )
        {
            player.ChangeState(PlayerAnimation.State.HIDE);
            mainGameUI.ChangeMouseGUI(Common.STATE_HIDE);
            bgmManager.SetBGM(player.state);
        }
    }

    public void OnScrollUp()
    {
        //Debug.Log("SHOW処理を実行");

        if (
            player.state == PlayerAnimation.State.HIDE
            )
        {
            player.ChangeState(PlayerAnimation.State.SHOW);
            mainGameUI.ChangeMouseGUI(Common.STATE_SHOW);
            bgmManager.SetBGM(player.state);
        }
    }

    public override void OnMouseMoveChangeUP()
    {
        if(player.state == PlayerAnimation.State.PUSH)
        {
            score++;
            Debug.Log(score);
        }
    }

}
