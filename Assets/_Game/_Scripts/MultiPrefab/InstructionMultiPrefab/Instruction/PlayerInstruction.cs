using System;
using UnityEngine;

[Serializable]
public class PlayerInstruction : AInstruction
{
    public PrefabPlay player;
    private bool isPrepared = false;

    public override Action OnAction()
    {
        return player.Play;
    }

    public override Action OffAction()
    {
        return player.Off;
    }

    public override Action GetPause()
    {
        return player.Pause;
    }

    public override Action Restart()
    {
        return player.Off;
    }

    public override Action Prepare()
    {
        if (isPrepared) return null;

        isPrepared = true;
        return player.Prepare;
    }

    public override void SetTime(float time)
    {
        player.SetTime(time);
    }

    public override float GetMaxTime()
    {
        return player.GetMaxTime();
    }
}