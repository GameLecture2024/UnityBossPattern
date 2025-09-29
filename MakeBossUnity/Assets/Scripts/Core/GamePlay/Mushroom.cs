using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mushroom : MonoBehaviour, IDamagable
{
    // Idle, Run, Stun, A1, A2, Hit, Die 이 상태에 따른 코드를 전부다 정의해줘야한다.
    // 각각의 상태를 코드로 정의를 해놓고 조립하는 방식으로 사용해보고 싶다. 유한 상태 머신 finite state machine, Behaviour tree 

    BehaviorGraphAgent behaviorAgent;
    [SerializeField] EnemyState startState;

    [SerializeField] int MaxHealth = 100;
    [field:SerializeField] public int CurrentHealth { get; private set; }

    public Action<bool> OnPatternStart; // Action, Func 함수를 변수처럼 저장해서 사용하겠다.  void 이름(bool enable)
    public Action<string, bool> OnSomeFucStart;
    public Action<int, int> OnHealthbarUpdate;   // 함수를 저장하는 변수

    [SerializeField] ParticleSystem rageVFX; // 보스가 레이지모드가 되었을 때 발동하는 이펙트

    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void Start()
    {
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", startState);
       
        CurrentHealth = MaxHealth;

        OnHealthbarUpdate?.Invoke(CurrentHealth, MaxHealth); // 
    }

    private void OnEnable()
    {
        OnPatternStart += HandlePatternStart;
        OnSomeFucStart += HandleSomeFuncStart;
    }

    private void OnDisable()
    {
        OnPatternStart -= HandlePatternStart;
        OnSomeFucStart -= HandleSomeFuncStart;
    }

    private void HandlePatternStart(bool enable)
    {
        Debug.Log("HandlePatternStart 함수 실행!");
        behaviorAgent.SetVariableValue<Boolean>("IsPatternTrigger", enable);     // 어떤 이벤트가 실행되었을 때

        if(rageVFX.isPlaying) { return; } // 반복적인 play 호출을 방지하기 위함

        rageVFX.Play();
    }

    private void HandleSomeFuncStart(string methodName, bool enable) // 함수를 변수처럼 사용을 하고 싶다. -> Action<string, bool>
    {
        Debug.Log("HandleSomeFuncStart 함수 실행!");
        behaviorAgent.SetVariableValue<Boolean>(methodName, enable);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        OnHealthbarUpdate?.Invoke(CurrentHealth, MaxHealth); // 체력을 업데이트를 하는 함수를 실행하라고 명령.

        if (CurrentHealth < MaxHealth * 0.5f)
        {
            OnPatternStart?.Invoke(true);
            //OnSomeFucStart?.Invoke("IsPatternTrigger", true);
            // 레이지 모드, 보스 체력이 일정 이하가 되면 광폭화, 패턴이 강화가 된다.
        }

        if(IsStun())
        {
            StunRaise();
        }

        if (CurrentHealth <= 0)
        {
            // 죽었다.
            // animator.SetTrigger("Die");
            Debug.Log("죽었다.");
            behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Die);
            
            // 게임 bossDeathEvent, 몬스터 파괴, 폭발 이펙트 발생 -> 보스 죽었을 때 어떤 현상이 일어나는가?
        }
    }

    // Bus<IRaiseStunEvent>.Raise()
    private bool IsStun()
    {
        // 어떤 조건일 때 스턴이 걸리는가?  - 무력화 ( '0') -> 
        // 특정 무기 타입에 맞는 형태로 공격을 했을 때 확률에 따라서 스턴이 걸릴 수 있다.
        // 보스의 특정 기믹을 성공하면 스턴이 걸린다.

        // 주사위 돌려서, 일정 숫자보다 작으면 스턴이다.

        int rand = UnityEngine.Random.Range(0, 101); // 0 ~ 100 반환하는 코드

        if(rand <= 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StunRaise()
    {
        // animator.SetTrigger("Stun");
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Stun);
    }

    void Update()
    {
        if (Keyboard.current.tKey.IsPressed())
        {
            TakeDamage(10);
        }
    }
}