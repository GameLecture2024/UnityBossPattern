using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSkill", menuName = "ScriptableObject/Skill/ShieldSkill")]
public class ShieldSkill : Skill
{
    public override void Execute()
    {
        base.Execute();

        Debug.Log("Guard!");
    }
}