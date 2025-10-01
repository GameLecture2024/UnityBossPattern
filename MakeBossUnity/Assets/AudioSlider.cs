using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] string parameterName;   // Master, BGM, SFX
    [SerializeField] float multiplier = 25f; // 사운드 슬라이더 1바뀔 때 증가하는 수치 (-80db ~ 20db) <- ("0 ~ 1")
    [SerializeField] AudioMixer audioMixer;  // Master, BGM, SFX
    public void SetSlider(float value) // 0 ~ 1 소수점
    {
        float newValue = Mathf.Log10(Mathf.Max(value,0)) * multiplier;  // Log10 0 = 음의 무한대 0 -> 최대치            
        audioMixer.SetFloat(parameterName, newValue); // -80 , 20  0.5db
    }
}
