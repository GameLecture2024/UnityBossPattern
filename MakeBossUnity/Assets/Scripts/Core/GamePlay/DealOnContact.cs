using Unity.VisualScripting;
using UnityEngine;

public class DealOnContact : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private int applyDamage = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // 충돌한 대상이 데미지를 줄 수 있는 컴포넌트여야 한다. Idamagable 인터페이스를 만들지 않았다면 MushRoom.TakeDamage collision.
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.TryGetComponent<IDamagable>(out var damagable)) // iDamagable인터페이스를 상속한 모든 클래스는 작동한다.
       {
            SetApplyDamage();
            damagable.TakeDamage(applyDamage);

            Destroy(gameObject);
       }


    }

    private void SetApplyDamage()
    {
        
    }
    
}
