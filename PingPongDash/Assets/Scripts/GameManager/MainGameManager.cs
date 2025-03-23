using Kabasawa;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
/// Play���̏�Ԃ��Ǘ�����N���X
/// </summary>
public class MainGameManager :  IMouseMove, IMouseScroll,IAlternateMouseClickListener
{
    /*********************�Ǘ�����N���X*********************/

    PlayerAnimation player;
    //TODO ��ƃN���X
    BGMManager bgmManager;

    MainGameUI mainGameUI;

    Enemy enemy;


    /*********************�Ǘ�����N���X*********************/

    /*********************Input�N���X*********************/

    MouseInputManager mouse;

    /*********************Input�N���X*********************/


    public int score = 0;

    float preDashInput = 0;

    public bool isGameOver = false;

    public bool isActive = false;

    public MainGameManager(PlayerAnimation player, BGMManager bgmManager, MainGameUI mainGameUI, Enemy enemy)
    {
        this.player = player;
        this.bgmManager = bgmManager;
        this.mainGameUI = mainGameUI;
        this.enemy = enemy;

        mouse = MouseInputManager.Instance;

        mouse.mouseScroll.Register(this);
        mouse.mouseMoveInput.Register(this);
        mouse.alternateMouseClickObserver.Register(this);

    }

    public void OnAlternateMouseClick()
    {
        if (!isActive) { return; }

        if(
            player.state == PlayerAnimation.State.IDLE ||
            player.state == PlayerAnimation.State.PUSH ||
            player.state == PlayerAnimation.State.DASH
            )
        {
            preDashInput = Time.fixedTime;

            player.dashCount++;

            player.ChangeState(PlayerAnimation.State.DASH);
            bgmManager.SetBGM(player.state);
            mainGameUI.ChangeMouseGUI(Common.STATE_DASH);
        }
    }

    /// <summary>
    /// �펞����
    /// </summary>
    public void Loop()
    {


        if (enemy.isView && player.state != PlayerAnimation.State.HIDE)
        {
            isGameOver = true;
        }

        switch (player.state)
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

        if(mouse.mouseAvg.Get() >= 0.5f)
        {
            player.ChangeState(PlayerAnimation.State.PUSH);
        }
    }

    void PUSH()
    {
        Debug.Log("PUSH���������s");
        //TODO �Ë��Ȃ̂ł��ꂢ�ɂł����炷��
        mainGameUI.ChangeMouseGUI(Common.STATE_PUSH);

        if (mouse.mouseAvg.Get() < 0.5f)
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
        if(preDashInput + 0.6f < Time.fixedTime)
        {
            player.dashCount = 0;

            bgmManager.SetBGM(player.state);
            mainGameUI.ChangeMouseGUI(Common.STATE_NORMAL);
            player.ChangeState(PlayerAnimation.State.NORMAL);
        }
    }

    public void OnScrollDown()
    {
        if (!isActive) { return; }

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
        if (!isActive) { return; }

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
        if (!isActive) { return; }

        if (player.state == PlayerAnimation.State.PUSH)
        {
            score++;
            enemy.PingPong();
            Debug.Log("scoreUP!!");
        }
    }
}
