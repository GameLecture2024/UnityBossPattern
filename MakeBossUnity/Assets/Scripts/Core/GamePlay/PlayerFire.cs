using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerFire : MonoBehaviour
{
    Player player;

    [Header("Fire 속성")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRate = 2;
    [SerializeField] Transform firePos;
    bool shouldFire;          // true일 때 발사가능
    float previousFireTime;   // 직전에 총알을 발사한 시간

    int playerForwardDir = 1;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.OnFire += HandleFire;
    }

    private void OnDisable()
    {
        player.OnFire -= HandleFire;
    }

    private void HandleFire(bool enable)
    {
        shouldFire = enable;
    }

    private void Update()
    {
        if (shouldFire == false) { return; } // if (!shouldFire)
        if (Time.time < previousFireTime + (1 / fireRate)) { return; }

        GameObject PJinstance = 
            Instantiate(projectilePrefab, firePos.position, Quaternion.identity);

        // 총알을 언제 왼쪽에 쏘고 언제 오른쪽에 쏴야 하는가?
        // - player 어떻게 움직였을 때 결과가 어떻게 이루어져야 하는가

        float tempValue = player.controls.Player.Move.ReadValue<float>();

        if(tempValue < 0)
        {
            playerForwardDir = -1;
        }
        else if(tempValue > 0)
        {
            playerForwardDir = 1;
        }

        PJinstance.GetComponent<Rigidbody2D>().linearVelocity = Vector3.right * playerForwardDir * 10f; // 10 투사체 속도 변수
        previousFireTime = Time.time;
    }
}
