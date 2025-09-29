using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DisplayGameUI : MonoBehaviour
{
    private Mushroom mushroom; // 개별 객체를 가져온다.  데미지를 입었다! 함수로 연결

    [SerializeField] Image Bosshealthbar; // 어떤 체력바를 선택할지 고르세요.
    [SerializeField] Image HUDhealthbar; // 어떤 체력바를 선택할지 고르세요.
    [SerializeField] TextMeshProUGUI ragedText;
    private void Awake()
    {
        mushroom = GetComponent<Mushroom>();
    }

    private void Start()
    {
        ragedText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        mushroom.OnHealthbarUpdate += OnUpdateHealthBar; // int , int 함수로 저장할 수 있는 타입을 선언해보세요. -> Action <  >  두 타입 선언   OnHealthbarUpdate
        mushroom.OnPatternStart += OnRaged;
    }

    private void OnDisable()
    {
        mushroom.OnHealthbarUpdate -= OnUpdateHealthBar;
        mushroom.OnPatternStart -= OnRaged;
    }

    private void OnRaged(bool enable)
    {
        if(ragedText.gameObject.activeSelf) { return; }

        ragedText.gameObject.SetActive(enable);
    }

    private void OnUpdateHealthBar(int current, int max) // 현재 체력 / 최대 체력 -> 0 ~ 1 소수점 => fillamount
    {
        if(Bosshealthbar != null)
            Bosshealthbar.fillAmount = (float)current / max;    

        if(HUDhealthbar != null)
            HUDhealthbar.fillAmount = (float)current / max;
    }

}

