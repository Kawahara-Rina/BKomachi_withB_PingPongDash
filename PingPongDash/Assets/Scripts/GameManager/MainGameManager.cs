using Kabasawa;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
/// Play���̏�Ԃ��Ǘ�����N���X
/// </summary>
public class MainGameManager :  IMouseMove, IMouseScroll
{

    /*********************�Ǘ�����N���X*********************/

    PlayerAnimation player;
    //TODO ��ƃN���X
    BGMManager bgmManager;

    MainGameUI mainGameUI;


    /*********************�Ǘ�����N���X*********************/

    /*********************Input�N���X*********************/

    MouseInputManager mouse;

    /*********************Input�N���X*********************/


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
    /// �펞����
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
        Debug.Log("PUSH���������s");
        //TODO �Ë��Ȃ̂ł��ꂢ�ɂł����炷��
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
        Debug.Log("DASH���������s");
    }

    public void OnScrollDown()
    {
        //Debug.Log("HIDE���������s");

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
        //Debug.Log("SHOW���������s");

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
