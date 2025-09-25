using UnityEngine;

// A is B 가 아닌데, 데미지를 받는 것을 공통으로 표현하고 싶다. => 플레이어, 몬스터 데미지를 입다. PlayerDamage, EnemyDamage, NPc -Damage , Npc - nonDamage

public interface IDamagable  
{
    int CurrentHealth { get; }

    void TakeDamage(int damage);
}
