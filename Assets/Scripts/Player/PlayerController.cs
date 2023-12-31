using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get {  return facingLeft; } set { facingLeft = value; } }

    [SerializeField] private float moveSpeed = 1f;

    Rigidbody2D rb;
    PlayerControls playerControls;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    Vector2 movement;

    private bool facingLeft=false;


    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement=playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX",movement.x);
        myAnimator.SetFloat("moveY",movement.y);

    }

    private void Move()
    {
        rb.MovePosition(rb.position+(movement*moveSpeed*Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos=Input.mousePosition;
        Vector3 playerScreePoint=Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x<playerScreePoint.x)
        {
            mySpriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            FacingLeft=false;
        }
           

    }
}
