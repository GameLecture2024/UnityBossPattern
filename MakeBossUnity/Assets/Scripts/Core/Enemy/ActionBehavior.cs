using UnityEngine;


// ����� �� ���ΰ�, �������̽��� ���� ���ΰ�    "A is B"  ���(A)��(is) ����(B)�̴�. => Ŭ���� ���, Human Animal
//                                           " ������ �ݵ�� �����Ѵ�" => A is B? �ƴ� ��찡 �����մϴ�. �������̽��� ����ϼ���

// ����� �ֳ���? -> interface IStoppable
// Stop�� �� �־���Ѵ�. ��� �׼���. -> 

public interface IStoppableActionBehavior
{
    void OnStop();
}

public abstract class ActionBehavior : MonoBehaviour
{
    public bool IsPatternEnd;

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();
    public virtual void OnStop()
    {
        IsPatternEnd = false;
        // ������ ������ �ִ� �ڷ�ƾ�� ���߱� ���ؼ�
    }
}