using UnityEngine;

public abstract class ActionBehavior : MonoBehaviour
{
    public bool IsPatternEnd;

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();
}