using UnityEngine;

public class Skill : ScriptableObject
{
    public string Name;

    public virtual void Execute()
    {
        Debug.Log($"{Name} ��ų�� ����߽��ϴ�!");
    }
}

// �����丵�� �ϰ� �ֱ� �����Ѵ�. - �ϳ��� ǥ���ϴ� ���� �ƴ϶� ����, �߻���

[CreateAssetMenu(fileName = "Fireball", menuName = "ScriptableObject/Skill/Fireball")]
public class ProjectileSkill : Skill
{
    int count = 5; 
}