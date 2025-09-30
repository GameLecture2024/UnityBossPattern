using System;
using System.Collections;
using UnityEngine;

public class MushroomAttack1 : ActionBehavior
{
    Transform target;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] float waitTimeForCharging = 1f;   // 차지 시간
    [SerializeField] GameObject projectilePrefab;      // 투사체
    [SerializeField] float projectileRange = 180;      // 투사체 발사될 각도
    [SerializeField] int loopCount = 2;                // 패턴의 반복 횟수

    [SerializeField] float RightAngle = -60f;
    [SerializeField] float LeftAngle = 150f;

    AudioSource audiosource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
    }

    public override void OnEnd()
    {
        // 패턴을 시작할 때 초기화 해야하는 코드가 있다면 여기서 설정한다.
        IsPatternEnd = false;
    }

    public override void OnStart()
    {
        IsPatternEnd = false;
        StartCoroutine(ChargingPattern());
    }

    public override void OnUpdate()
    {
        // 플레이어의 현재 위치 방향으로 flip 하는 코드를
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(transform.position.x < player.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public override void OnStop()
    {
        StopCoroutine(ChargingPattern());
        base.OnStop();
    }

    IEnumerator ChargingPattern()
    {
        // 기를 모은다.
        // 기 모으는 애니메이션 실행 -> 최대한 비슷하게 뭔가 해볼 것
        animator.SetTrigger("A1");
        yield return new WaitForSeconds(waitTimeForCharging);

        for(int i=0; i < loopCount; i++)
        {
            // 180도 각도로 투사체를 발사한다
            Fire();
            audiosource.clip = Resources.Load<AudioClip>("Sound/Ice"); // Resources
            audiosource.Play();
            yield return new WaitForSeconds(1f);
        }

        animator.SetTrigger("Stun");
        yield return new WaitForSeconds(2f);

        IsPatternEnd = true;
    }

    private void Fire()
    {
        // 내 위치와 TargetLocation 위치를 비교해서 flip
        // 내 위치와 Target 비교해서 왼쪽, 오른쪽 각도

        float currentAngle = SelectAngleByPlayerPosition();

        float deltaAngle = projectileRange / 10;

        for(int i =0; i < 10; i++ )  // 10 - numberofprojectils
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            //삼각함수를 이용해서 x,y좌표를 알면 해당 방향을 구할 수 있다.
            float dirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            Vector2 Spawn = new Vector2(transform.position.x + dirX, transform.position.y + dirY);
            Vector2 dir = (Spawn - (Vector2)transform.position).normalized;

            if (projectileInstance.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = dir * 5f;  
            }

            currentAngle += deltaAngle;
        }

    }

    private float SelectAngleByPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(transform.position.x < player.transform.position.x)
        {
            return RightAngle;
        }
        else
        {
            return LeftAngle;
        }
    }
}
