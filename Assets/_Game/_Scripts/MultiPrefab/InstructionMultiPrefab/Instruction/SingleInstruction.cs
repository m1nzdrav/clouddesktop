using System;

[Serializable]
public class SingleInstruction
{
    public AInstruction Instruction;
    public float time;
    public bool isPlayed;


    public Action GetAction()
    {
        isPlayed = true;
        switch (Instruction.animation)
        {
            case AnimationInstruction.On:
                return Instruction.OnAction();
            case AnimationInstruction.Off:
                return Instruction.OffAction();
            default: return null;
        }
    }

    public Action Prepare()
    {
        return Instruction.Prepare();
    }

    public Action GetPause()
    {
        isPlayed = false;
        return Instruction.GetPause();
    }

    public Action Restart()
    {
        isPlayed = false;
        return Instruction.GetPause();
    }

    public float GetMaxTime()
    {
        return Instruction.GetMaxTime();
    }

    public void SetTime(float time)
    {
        Instruction.SetTime(time);
        if (this.time > time) GetPause().Invoke();
    }
}