using UnityEngine;

// A is B �� �ƴѵ�, �������� �޴� ���� �������� ǥ���ϰ� �ʹ�. => �÷��̾�, ���� �������� �Դ�. PlayerDamage, EnemyDamage, NPc -Damage , Npc - nonDamage

public interface IDamagable  
{
    int CurrentHealth { get; }

    void TakeDamage(int damage);
}
