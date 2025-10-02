using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skillexecutor : MonoBehaviour
{
    List<Skill> currentSkill = new();

    [SerializeField] Skill[] startSkill;   // 투사체 쏘는 패턴

    private void Start()
    {
        for(int i=0; i<startSkill.Length; i++)
        {
            currentSkill.Add(startSkill[i]);
        }


        ExcuteSkill(0);
        ExcuteSkill(1);
    }

    // 스킬을 구입, 레벨업.... 추가를 하고 싶다.

    public void AddSkill(Skill skill) // 같은 데이터 타입을 여러개 가질 수 있다. -> 중복을 못하는 방식의 자료구조 가져오면 어떨까?
    {
        currentSkill.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        currentSkill.Remove(skill);
    }    

    public void ExcuteSkill(int index)
    {
        currentSkill[index].Execute();
    }
}
