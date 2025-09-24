using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayActionBehavior", story: "play [ActionBehavior] .", category: "Action/Pattern", id: "036e1fa7940081ade2eb1339fb4aaea5")]
public partial class PlayActionBehaviorAction : Action
{
    [SerializeReference] public BlackboardVariable<ActionBehavior> ActionBehavior;

    protected override Status OnStart()
    {
        ActionBehavior.Value.OnStart();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // 패턴이 성공할 때 까지
        if(ActionBehavior.Value.IsPatternEnd)
        {
            return Status.Success;
        }
        else
        {
            ActionBehavior.Value.OnUpdate();
            return Status.Running;
        }
     
    }

    protected override void OnEnd()
    {
        ActionBehavior.Value.OnEnd();
    }
}

