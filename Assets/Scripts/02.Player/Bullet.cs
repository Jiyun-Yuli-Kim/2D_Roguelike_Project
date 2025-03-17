using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float _bulletDamage;
    public float bulletCoolTime;
    protected CustomPool<Bullet> bulletPool;
    public Animator bulletAnimator;

    protected Rigidbody2D _rb;
    protected Collider2D _col;
    
    public Monster target;

    private void Awake()
    {
        _bulletDamage = 5;
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        bulletAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) // 어딘가에 충돌했을 때 반드시 불렛 반납
    {
        target = null;

        StartCoroutine(ReturnBullet(collision));
    }

    // 문제가 뭘까?
    // 가설 1. 총알이 반납되면서 collider가 깨지고 이 사이클 자체가 문제가 된다.
    // 가설 2. 피격된 몬스터가 제거되며 콜라이더가 사라지고 참조를 잃는다.
    private IEnumerator ReturnBullet(Collision2D curCol)
    {
            var colObj = curCol.gameObject;
            bulletAnimator.SetTrigger("OnDestroy");
            yield return new WaitForSeconds(0.2f);

            if (colObj != null && colObj.CompareTag("Enemy")) // 몬스터 피격시
            {
                colObj.GetComponent<Monster>().GetDamage(_bulletDamage); // 데미지 부여 및 애니메이션 재생 
                SoundManager.Instance.PlaySFX(ESFXs.HitSFX);
                yield return new WaitForSeconds(0.5f);
                Destroy(colObj);
            }
            
            bulletPool.Return(this); // 일단 충돌했다면 반드시 반납하도록

    }

    public virtual void ToTarget(Vector3 origin, Vector3 target)
    {
        Vector3 direction = (target - this.transform.position).normalized;
        _rb.velocity = direction*bulletSpeed;
    }

    public void Init(CustomPool<Bullet> BulletPool)
    {
        bulletPool = BulletPool;
    }
}
