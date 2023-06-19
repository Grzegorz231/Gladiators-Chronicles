using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : Entity
{
    //взбираемся по лестнице
    public bool checkLadder;
    public LayerMask ladderMask;
    public float ladderSpeed = 1.5f;

    //собираем монетки
    public int money;
    public Text count;

    // атакуем
    public bool isAttacking = false;
    public bool isRecharged = true;
    public bool playerHaveSpear = false;
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy; //не забывай добавлять отдельные слои для всех врагов

    // двигаемся
    [SerializeField] private float speed = 3;
    private Vector2 moveVector;
    public bool faceRight = true;

    // рывок
    Vector2 currentPos;
    Vector2 targetPos;
    [SerializeField] GameObject targetDashPoint;
    bool dashProcess = false;
    [SerializeField] float _processPercen = 0f;
    float _dashDistance = 0f;
    [SerializeField] float _dashSpeed = 30f;
    private bool lockDash = false;
    public float dashLock = 10f;

    // прыгаем
    public float jumpForce = 60;
    private bool jumpControl; // можешь приписать к этой переменной [SerializeField], чтобы видеть их значения в unity
    private float jumpIteration = 0; // и к этой тоже
    public float jumpValueIteration = 60;

    // проверяем на нахождение на земле
    public float checkRadius;
    public Transform groundCheck;
    public LayerMask Ground;
    public bool onGround;

    // спрайты, анимации и физика
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite aliveheart;
    [SerializeField] private Sprite deadHeart;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Sprite playerWithSword;
    public Sprite playerWithSpear;
    private Animator anim;
    public GameObject pressR;

    public static Hero Instance { get; set; }

    void Awake()
    {
        ToLoad();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Instance = this;
        isRecharged = true;
        lives = 1;
    }

    private void FixedUpdate()
    {
        count.text = money.ToString();
        if (dashProcess == true) { DashFixedUpdate(); }
    }
    private void Update()
    {
        PlayerHaveSpear();
        CheckingGround();
        if (onGround && !isAttacking && !dashProcess)
        {
            if (playerHaveSpear)
            {
                State = States.idleSpear;
            }
            else
            {
                State = States.idle;
            }
        }
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        Jump();
        if (Input.GetKeyDown(KeyCode.LeftShift) && !lockDash) 
        {
            Timer.TimerSwitch();
            lockDash = true;
            Invoke("DashLock", dashLock);
            Dash();
        }
        if (Input.GetButton("Fire1"))
        {
            Attack();
        }
        Reflect();
        CheckLadder();
        LadderMechanic();
        LadderUpDown();

        for (int i = 0; i<hearts.Length; i++)
        {
            if(i < lives)
            {
                hearts[i].sprite = aliveheart;
            }
            else
            {
                hearts[i].sprite = deadHeart;
            }
        }
    }
    public void OnDestroy()
    {
        ToSave();
    }
    void LadderUpDown()
    {
        moveVector.y = Input.GetAxisRaw("Vertical");
    }
    void LadderMechanic()
    {
        if (checkLadder)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = new Vector2(rb.velocity.x, moveVector.y * ladderSpeed);
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    void CheckLadder()
    {
        checkLadder = Physics2D.OverlapPoint(groundCheck.position, ladderMask);
    }
    public void ToLoad()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            DataHolder.livesToSave = 1;
            DataHolder.moneyToSave = 0;
            DataHolder.jumpForceToSave = 220;
            DataHolder.playerHaveSpearToSave = false;
            DataHolder.attackRangeToSave = 1;
            DataHolder.dashLockToSave = 10;
        }
        lives = DataHolder.livesToSave;
        money = DataHolder.moneyToSave;
        playerHaveSpear = DataHolder.playerHaveSpearToSave;
        attackRange = DataHolder.attackRangeToSave;
        dashLock = DataHolder.dashLockToSave;
        jumpForce = DataHolder.jumpForceToSave;
    }
    public void ToSave()
    {
        DataHolder.moneyToSave = money;
        DataHolder.playerHaveSpearToSave = playerHaveSpear;
        DataHolder.attackRangeToSave = attackRange;
        DataHolder.dashLockToSave = dashLock;
        DataHolder.jumpForceToSave = jumpForce;

    }
    public void PlayerHaveSpear()
    {
        if (playerHaveSpear)
        {
            attackRange = 2;
            spriteRenderer.sprite = playerWithSpear;
        }
    }
    private void Run()
    {
        if (onGround && !isAttacking && !dashProcess)
        {
            if (playerHaveSpear)
            {
                State = States.runSpear;
            }
            else
            {
                State = States.run;
            }
        }
        moveVector.x = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);


        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector2.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        //sprite.flipX = direction.x < 0.0f;
    }
    void Reflect() // поворачиваем персонажа
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }
    private void Jump()
    {
        if (!onGround && !isAttacking && !dashProcess)
        {
            if (playerHaveSpear)
            {
                State = States.jumpSpear;
            }
            else
            {                
                State = States.jump;
            }
        }
        if (Input.GetKey(KeyCode.Space)) // если клавиша прыжка была зажата
        {
            if (onGround) { jumpControl = true; } // когда персонаж был на земле

        }
        else { jumpControl = false; } // до тех пор пока клавиша не будет отпущена

        if (jumpControl)
        {
            if (jumpIteration++ < jumpValueIteration) // или не пройдёт с момента нажатия клавиши 60(опционально) или более кадров
            {
                rb.AddForce(Vector2.up * jumpForce / jumpIteration); // то персонаж будет набирать высоту, которая с каждым кадром будет становиться всё меньше
            }
        }
        else { jumpIteration = 0; }
    }
    public override void GetDamage()
    {
        lives--;
        if (lives < 1)
        {
            pressR.SetActive(true);
            Die();
        }
    }
    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    public void Dash()
    {
        if (dashProcess == false)
        {
            currentPos = transform.position;
            targetPos = targetDashPoint.transform.position;
            _dashDistance = Vector2.Distance(currentPos, targetPos);
            _processPercen = 0f;
            dashProcess = true;
            if (playerHaveSpear)
            {
                State = States.dashSpear;
            }
            else
            {              
                State = States.dash;
            }
        }
        
    }
    void DashFixedUpdate()
    {
        _processPercen += Time.fixedDeltaTime * _dashSpeed / _dashDistance;

        if (_processPercen <= 1f)
        {
            currentPos = Vector2.MoveTowards(currentPos, targetPos, _dashSpeed * Time.fixedDeltaTime);
            rb.MovePosition(currentPos);
            transform.Translate((targetPos - currentPos).normalized * _dashSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, _dashSpeed * Time.fixedDeltaTime);
            dashProcess = false;
            _processPercen = 0f;
        }
    }
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);
    }
    void DashLock()
    {
        lockDash = false;
    }
    private void Attack() //тут должны быть анимации
    {
        if (isRecharged)
        {
            if (playerHaveSpear)
            {
                State = States.attackSpear;
            }
            else
            {
                State = States.attack;
            }

            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private void OnAttack() // метод, который вызывает атаку (его добавить в события)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

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
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }

}
public enum States
{
    idle,
    run,
    attack,
    jump,
    dash,
    idleSpear,
    runSpear,
    attackSpear,
    jumpSpear,
    dashSpear
}