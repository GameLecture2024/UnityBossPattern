using UnityEngine;


// 상속을 할 것인가, 인터페이스로 만들 것인가    "A is B"  사람(A)은(is) 동물(B)이다. => 클래스 상속, Human Animal
//                                           " 유닛은 반드시 공격한다" => A is B? 아닌 경우가 존재합니다. 인터페이스를 사용하세요

// 멈출수 있나요? -> interface IStoppable
// Stop할 수 있어야한다. 모든 액션이. -> 

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
        // 각각이 가지고 있는 코루틴을 멈추기 위해서
    }
}