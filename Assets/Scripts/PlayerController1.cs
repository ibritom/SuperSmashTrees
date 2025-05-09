using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    bool canJump = false;

    // Variabe que da accesso a todas las acciones en InputAction
    public InputActionAsset InputActions;

    // Variables relacionadas a las acciones
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction jumpAction;
    private InputAction shieldAction;

    // Variables para saltar y moverse
    public float MoveSpeed = 45f;
    public float JumpForce = 8000f;
    public float AirJumpForce = 25000f;

    // Variable para las hitboxes
    public GameObject HitBox;
    public float HitBoxRadius;
    public LayerMask Fighters;
    public float HitForce;
    private bool hasHit = false;

    // Variables para el shield
    private GameObject shieldVisual;

    public float ForcePushForce;

    public Vector2 movementInput;

    // Donde el jugador va a respawnear
    public Vector3 respawnPosition = new Vector3(0, 10, 0);

    private void Awake()
    {
        // Mapear las acciones del action map a sus variables respectivas
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        shieldAction = InputSystem.actions.FindAction("Shield");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    // Revisar si el jugador se est� cayendo
    bool IsFalling()
    {
        return GetComponent<Rigidbody2D>().linearVelocity.y < -0.1f;
    }

    // Funciones para el movimiento
    public void Jump()
    {
        canJump = false;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
    }
    public void Move()
    {
        gameObject.transform.Translate(movementInput.x * MoveSpeed * Time.deltaTime, 0, 0);
        if (movementInput.x > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1); // Hacia la derecha
        }
        else if (movementInput.x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Hacia la izquierda (espejado)
        }
    }

    // Funcion para revisar si el jugador se esta cayendo sin haber saltado previamente
    public void CheckFallingNoJump()
    {
        int terrainLayer = LayerMask.GetMask("terrain");
        int floorLayer = LayerMask.GetMask("floor");
        bool isTouchingTerrain = GetComponent<Collider2D>().IsTouchingLayers(terrainLayer);
        bool isTouchingFloor = GetComponent<Collider2D>().IsTouchingLayers(floorLayer);

        if (!isTouchingTerrain && !isTouchingFloor && IsFalling())
        {
            gameObject.GetComponent<Animator>().SetBool("IsFalling", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("IsFalling", false);
        }
    }

    // Funciones para atacar, se van a mover a otro script luego
    public void Attack()
    {
        if (hasHit) return; // evita m�ltiples golpes
        hasHit = true;
        Debug.Log("Attacking");
        Collider2D[] fighters = Physics2D.OverlapCircleAll(HitBox.transform.position, HitBoxRadius, Fighters);
        foreach (Collider2D FighterGameObject in fighters)
        {
            if (FighterGameObject.gameObject == this.gameObject) continue; // Evitar pegarse con uno mismo
            if (FighterGameObject.gameObject.layer == LayerMask.NameToLayer("terrain")) continue;
            Debug.Log("Hit Fighter");
            Rigidbody2D FighterRb = FighterGameObject.GetComponent<Rigidbody2D>();
            Animator FighterAnim = FighterGameObject.GetComponent<Animator>();
    
            if (FighterRb != null)
            {
                Vector2 direction = (FighterGameObject.transform.position - transform.position).normalized;
                FighterRb.AddForce(direction * HitForce, ForceMode2D.Impulse);
           }
            if (FighterAnim != null)
            {
                FighterAnim.SetBool("IsHurt", true);
                // Opcional: reiniciar el bool después de cierto tiempo
                StartCoroutine(ResetHurt(FighterAnim, 0.3f));
            }
        }
        Invoke(nameof(ResetHit), 0.3f); // Ajusta duraci�n seg�n tu animaci�n
    }
    private void ResetHit()
    {
        Animator FighterAnim = gameObject.GetComponent<Animator>();
        hasHit = false;
    }
    private System.Collections.IEnumerator ResetHurt(Animator anim, float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.SetBool("IsHurt", false);
    }

    public void EndAttackAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("IsAttacking", false);
    }

    // Renderizar la hitbox para poder verlo en el editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(HitBox.transform.position, HitBoxRadius);
    }

    // Funciones para el shield, todav�a no he implementado hacerlo un movimiento especial
    public void ShieldUp()
    {
        gameObject.GetComponent<Rigidbody2D>().mass = 100f;
        shieldVisual.SetActive(true);
        canJump = false;
    }
    public void ShieldDown()
    {
        gameObject.GetComponent<Rigidbody2D>().mass = 1f;
        shieldVisual.SetActive(false);
        canJump = true;
    }

    // Funci�n para al AirJump, todav�a no he implementado hacerlo un movimiento especial
    public void AirJump()
    {
        canJump = false;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, AirJumpForce));
    }

    // Funcion para el ForcePush, todavía no he implemendado hacerlo un movimiento especial
    public void ForcePush()
    {
        if (hasHit) return; // evita m�ltiples golpes
        hasHit = true;
        Debug.Log("Attacking");
        gameObject.GetComponent<Animator>().SetBool("IsAttacking", true);
        Collider2D[] fighters = Physics2D.OverlapCircleAll(HitBox.transform.position, HitBoxRadius, Fighters);
        foreach (Collider2D FighterGameObject in fighters)
        {
            if (FighterGameObject.gameObject == this.gameObject) continue; // Evitar pegarse con uno mismo
            Debug.Log("Hit Fighter");
            Rigidbody2D FighterRb = FighterGameObject.GetComponent<Rigidbody2D>();

            if (FighterRb != null)
            {
                Vector2 direction = (FighterGameObject.transform.position - transform.position).normalized;
                FighterRb.AddForce(direction * ForcePushForce, ForceMode2D.Impulse);
            }

        }
        Invoke(nameof(ResetHit), 0.3f); // Ajusta duraci�n seg�n tu animaci�n
    }

    // Los Unity Events que son invocados cuando se pulsa el boton
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump && !IsFalling())
        {
            Debug.Log("Jumping");
            gameObject.GetComponent<Animator>().SetBool("IsJumping", true);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        if (context.performed)
        {
            gameObject.GetComponent<Animator>().SetBool("IsWalking", true);
        } else {
            gameObject.GetComponent<Animator>().SetBool("IsWalking", false);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.GetComponent<Animator>().SetBool("IsAttacking", true);
        }
    }
    public void OnShieldUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShieldUp();
            Debug.Log($"Mass: {gameObject.GetComponent<Rigidbody2D>().mass}");
        }
    }
    public void OnShieldDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShieldDown();
            Debug.Log($"Mass: {gameObject.GetComponent<Rigidbody2D>().mass}");
        }
    }

    // Respawnear jugador
    public IEnumerator Respawn(float delay)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(delay);
        transform.position = respawnPosition;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    // Colision en el suelo y destruir el jugador cuando toca el DeathPlane
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hacer que el jugador solo pueda saltar cuando toque el suelo
        if (collision.transform.tag == "floor" || IsFalling() == false)
        {
            canJump = true;
            gameObject.GetComponent<Animator>().SetBool("IsJumping", false);
        }

        // Manejar los death planes
        if (collision.transform.tag == "DeathPlane")
        {
            Debug.Log("Jugador tocó el deathplane, respawneando");
            StartCoroutine(Respawn(1f));
        }
    }

    // Start
    void Start()
    {
        shieldVisual = new GameObject("ShieldVisual");
        shieldVisual.transform.SetParent(transform);
        shieldVisual.transform.localPosition = Vector3.zero;
        shieldVisual.transform.localScale = new Vector3(4.5f, 4.5f, 4.5f); // Aumenta el tamaño 2x

        var sr = shieldVisual.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("circulo");
        sr.color = new Color(1f, 0f, 0f, 0.5f); // Rojo con opacidad
        sr.sortingOrder = 10;

        shieldVisual.SetActive(false);
    }

    // Update
    private void Update()
    {
        IsFalling();
        CheckFallingNoJump();
    }

    // Necesario para moverse
    private void FixedUpdate()
    {
        Move();
    }
}