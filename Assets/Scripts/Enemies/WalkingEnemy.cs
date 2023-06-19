using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
    
public class WalkingEnemy : Entity
{
    public bool isAttackingWalkingEnemy = false;
    public bool isRechargedWalkingEnemy = true;
    public Transform attackPos;
    public float attackRange = 3;
    public LayerMask playerMask;
    public GameObject player;
    private Animator animatorWalkingEnemy;
    public bool faceRightWalkingEnemy;
    public bool runProcess;

    public float radiusToSee;

    public override void GetDamage()
    {
        base.GetDamage();
    }

    public float speedMove = 10.0f;

    void Start()
    {
        lives = 1;
        player = GameObject.FindWithTag("Player");
        animatorWalkingEnemy = GetComponent<Animator>();          
    }

    void Update()
    {
        float direction = player.transform.position.x + 1.5f - transform.position.x;
        if (Mathf.Abs(direction) < radiusToSee)
        {
            runProcess = true;
            if (!isAttackingWalkingEnemy)
            {
                StateEnemyWalking = StatesWalkingEnemy.run;
            }
            Vector3 pos = transform.position;
            pos.x += Mathf.Sign(direction) * speedMove * Time.deltaTime;
            transform.position = pos;
            
        }
        if (Mathf.Abs(direction) > radiusToSee)
        {
            runProcess = false;
        }
        if (!isAttackingWalkingEnemy && !runProcess)
        {
            StateEnemyWalking = StatesWalkingEnemy.idle;
        }
        if ((Mathf.Abs(player.transform.position.x - transform.position.x) <= attackRange + 1) && Mathf.Abs(player.transform.position.y - transform.position.y) <= 4)
        {
            Attack();
        }
        Reflect();
    }
    void Reflect() // поворачиваем персонажа
    {
        if ((this.gameObject.transform.position.x - player.transform.position.x > 0 && !faceRightWalkingEnemy) || (this.gameObject.transform.position.x - player.transform.position.x < 0 && faceRightWalkingEnemy))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRightWalkingEnemy = !faceRightWalkingEnemy;
        }
    }
    private void Attack() //тут должны быть анимации
    {
        if (isRechargedWalkingEnemy)
        {
            StateEnemyWalking = StatesWalkingEnemy.attack;

            isAttackingWalkingEnemy = true;
            isRechargedWalkingEnemy = false;

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
        isAttackingWalkingEnemy = false;
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRechargedWalkingEnemy = true;
    }
    private StatesWalkingEnemy StateEnemyWalking
    {
        get { return (StatesWalkingEnemy)animatorWalkingEnemy.GetInteger("StateEnemyWalking"); }
        set { animatorWalkingEnemy.SetInteger("StateEnemyWalking", (int)value); }
    }
    public enum StatesWalkingEnemy
    {
        idle,
        attack,
        run
    }
}