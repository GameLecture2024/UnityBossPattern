using UnityEngine;

public class MushroomAttack2 : ActionBehavior
{
    public override void OnEnd()
    {
        
    }

    public override void OnStart()
    {
        Debug.Log("버섯패턴2실행");
        IsPatternEnd = true;
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnStop()
    {
        base.OnStop();
    }
}
