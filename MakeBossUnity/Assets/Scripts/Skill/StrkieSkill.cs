using UnityEngine;

[CreateAssetMenu(fileName = "StrikeSkill", menuName = "ScriptableObject/Skill/StrikeSkill")]
public class StrkieSkill : Skill
{
    // strike스킬을 사용하기 위해 필요한 것.......
    // 사운드, 이펙트, 효과, 작용 방식..?
    public override void Execute()
    {
        base.Execute();

        Debug.Log("Deal 2d6 damage to the target");
    }
}
