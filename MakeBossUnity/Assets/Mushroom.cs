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

    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void Start()
    {
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", startState);
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

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