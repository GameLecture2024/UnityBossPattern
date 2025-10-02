using UnityEngine;

public class Skill : ScriptableObject
{
    public string Name;

    public virtual void Execute()
    {
        Debug.Log($"{Name} 스킬을 사용했습니다!");
    }
}

// 리펙토링을 하고 있기 시작한다. - 하나씩 표현하는 것이 아니라 범주, 추상적

[CreateAssetMenu(fileName = "Fireball", menuName = "ScriptableObject/Skill/Fireball")]
public class ProjectileSkill : Skill
{
    int count = 5; 
}