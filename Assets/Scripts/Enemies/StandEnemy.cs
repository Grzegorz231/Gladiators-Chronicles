using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandEnemy : Entity
{
    public AudioSource audioSource;
    public bool isAttackingStandEnemy = false;
    public bool isRechargedStandEnemy = true;
    public Transform attackPos;
    public float attackRange = 3;
    public LayerMask playerMask;
    public GameObject player;
    private Animator animator;
    public bool faceRightStandEnemy;
    public void Start()
    {
        lives = 1;
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isAttackingStandEnemy)
        {
            StateEnemy = StatesEnemy.idle;
        }
        if ((Mathf.Abs(player.transform.position.x - transform.position.x) <= attackRange + 1) && Mathf.Abs(player.transform.position.y - transform.position.y) <= 4)
        {
            Attack();
        }
        Reflect();
    }

    public override void GetDamage()
    {
        lives--;
        if (lives < 1)
        {
            audioSource.Play();
            Die();
        }
    }
    void Reflect() // поворачиваем персонажа
    {
        if ((this.gameObject.transform.position.x - player.transform.position.x > 0  && !faceRightStandEnemy) || (this.gameObject.transform.position.x - player.transform.position.x < 0 && faceRightStandEnemy))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRightStandEnemy = !faceRightStandEnemy;
        }
    }
    private void Attack() //тут должны быть анимации
    {
        if (isRechargedStandEnemy)
        {
            StateEnemy = StatesEnemy.attack;

            isAttackingStandEnemy = true;
            isRechargedStandEnemy = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }
    private void OnAttack() // метод, который вызывает атаку (его добавить в события)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, playerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }
    private void OnDrawGizmosSelected() // показывает дальность атаки и радиус (для настройки и калибровки)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        isAttackingStandEnemy = false;
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRechargedStandEnemy = true;
    }
    private StatesEnemy StateEnemy
    {
        get { return (StatesEnemy)animator.GetInteger("StateEnemy"); }
        set { animator.SetInteger("StateEnemy", (int)value); }
    }
    public enum StatesEnemy
    {
        idle,
        attack
    }
}
