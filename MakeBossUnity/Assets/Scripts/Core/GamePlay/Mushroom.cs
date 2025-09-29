using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mushroom : MonoBehaviour, IDamagable
{
    // Idle, Run, Stun, A1, A2, Hit, Die �� ���¿� ���� �ڵ带 ���δ� ����������Ѵ�.
    // ������ ���¸� �ڵ�� ���Ǹ� �س��� �����ϴ� ������� ����غ��� �ʹ�. ���� ���� �ӽ� finite state machine, Behaviour tree 

    BehaviorGraphAgent behaviorAgent;
    [SerializeField] EnemyState startState;

    [SerializeField] int MaxHealth = 100;
    [field:SerializeField] public int CurrentHealth { get; private set; }

    public Action<bool> OnPatternStart; // Action, Func �Լ��� ����ó�� �����ؼ� ����ϰڴ�.  void �̸�(bool enable)
    public Action<string, bool> OnSomeFucStart;
    public Action<int, int> OnHealthbarUpdate;   // �Լ��� �����ϴ� ����

    [SerializeField] ParticleSystem rageVFX; // ������ ��������尡 �Ǿ��� �� �ߵ��ϴ� ����Ʈ

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
        Debug.Log("HandlePatternStart �Լ� ����!");
        behaviorAgent.SetVariableValue<Boolean>("IsPatternTrigger", enable);     // � �̺�Ʈ�� ����Ǿ��� ��

        if(rageVFX.isPlaying) { return; } // �ݺ����� play ȣ���� �����ϱ� ����

        rageVFX.Play();
    }

    private void HandleSomeFuncStart(string methodName, bool enable) // �Լ��� ����ó�� ����� �ϰ� �ʹ�. -> Action<string, bool>
    {
        Debug.Log("HandleSomeFuncStart �Լ� ����!");
        behaviorAgent.SetVariableValue<Boolean>(methodName, enable);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        OnHealthbarUpdate?.Invoke(CurrentHealth, MaxHealth); // ü���� ������Ʈ�� �ϴ� �Լ��� �����϶�� ���.

        if (CurrentHealth < MaxHealth * 0.5f)
        {
            OnPatternStart?.Invoke(true);
            //OnSomeFucStart?.Invoke("IsPatternTrigger", true);
            // ������ ���, ���� ü���� ���� ���ϰ� �Ǹ� ����ȭ, ������ ��ȭ�� �ȴ�.
        }

        if(IsStun())
        {
            StunRaise();
        }

        if (CurrentHealth <= 0)
        {
            // �׾���.
            // animator.SetTrigger("Die");
            Debug.Log("�׾���.");
            behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Die);
            
            // ���� bossDeathEvent, ���� �ı�, ���� ����Ʈ �߻� -> ���� �׾��� �� � ������ �Ͼ�°�?
        }
    }

    // Bus<IRaiseStunEvent>.Raise()
    private bool IsStun()
    {
        // � ������ �� ������ �ɸ��°�?  - ����ȭ ( '0') -> 
        // Ư�� ���� Ÿ�Կ� �´� ���·� ������ ���� �� Ȯ���� ���� ������ �ɸ� �� �ִ�.
        // ������ Ư�� ����� �����ϸ� ������ �ɸ���.

        // �ֻ��� ������, ���� ���ں��� ������ �����̴�.

        int rand = UnityEngine.Random.Range(0, 101); // 0 ~ 100 ��ȯ�ϴ� �ڵ�

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