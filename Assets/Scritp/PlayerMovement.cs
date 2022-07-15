using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const float GRAVITY = -9.81f;

    [Header("Movement")]
    [SerializeField] Animator bodyAnim;             // �÷��̾� ��ü �ִϸ��̼�.
    [SerializeField] float walkSpeed;               // �ȴ� �ӵ�.
    [SerializeField] float runSpeed;                // �ٴ� �ӵ�.
    [SerializeField] float jumpHeight;              // ���� ����.

    [Range(1.0f, 4.0f)]
    [SerializeField] float gravityScale;

    [Header("Ground")]
    [SerializeField] Transform groundChecker;       // ���� üũ ������.
    [SerializeField] float groundRadius;            // ���� üũ ������.
    [SerializeField] LayerMask groundMask;          // ���� ���̾� ����ũ.

    CharacterController controller;         // ĳ���� ���� ������Ʈ.

    float gravity => GRAVITY * gravityScale; // �߷� ���ӵ� * �߷� ����.

    float velocityY
    {
        get
        {
            return bodyAnim.GetFloat("velocityY");
        }
        set
        {
            bodyAnim.SetFloat("velocityY", value);
        }
    }
    bool isGround
    {
        get
        {
            return bodyAnim.GetBool("isGround");
        }
        set
        {
            bodyAnim.SetBool("isGround", value);
        }
    }
    float inputX
    {
        get
        {
            return bodyAnim.GetFloat("inputX");
        }
        set
        {
            bodyAnim.SetFloat("inputX", value);
        }
    }
    float inputY
    {
        get
        {
            return bodyAnim.GetFloat("inputY");
        }
        set
        {
            bodyAnim.SetFloat("inputY", value);
        }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckGround();          // ground üũ.

        Movement();         // �̵�.
        Jump();             // ����.

        Gravity();              // �߷� ��.

        //Resetpoint();
    }
    private void CheckGround()
    {
        isGround = Physics.CheckSphere(groundChecker.position, groundRadius, groundMask);
    }
    private void Movement()
    {
        // Input.GetAxisRaw = -1, 0  1.
        // Input.GetAxis = -1.0f ~ 1.0f.
        inputX = Input.GetAxis("Horizontal");       // Ű���� ��,�� (����,����)
        inputY = Input.GetAxis("Vertical");         // Ű���� ��,�� (����,�ĸ�)

        bool isMove = (inputX != 0) || (inputY != 0);                 // �̵�Ű�� ������ ���� ���.
        bool isWalk = isMove && !Input.GetKey(KeyCode.LeftShift);     // ���� ����Ʈ ��ư�� ������ �ʾ��� ��� true.
        bool isRun = isMove && Input.GetKey(KeyCode.LeftShift);      // ���� ����Ʈ ��ư�� ������ ��� true.

        float movementSpeed = isWalk ? walkSpeed : runSpeed;

        // transform.���� => �� ���� ���� (���� ��ǥ)
        Vector3 direction = (transform.right * inputX) + (transform.forward * inputY);
        controller.Move(direction * movementSpeed * Time.deltaTime);
    }
    /*
    public void Resetpoint()
    {
        if (transform.localPosition.y <= -20f)
        {
            transform.position = new Vector3(0, 2, -2f);
        }
    }
    */
    private void Jump()
    {
        if (isGround && Input.GetButtonDown("Jump"))
        {
            bodyAnim.SetBool("isJump", true);
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (!isGround)
        {
            bodyAnim.SetBool("isJump", false);
        }
    }
    private void Gravity()
    {
        if (isGround && velocityY < 0f)          // ���� ��Ұ� �ϰ� �ӷ��� �ִٸ�.
        {
            velocityY = -2f;                       // �ּ����� ������ �ӷ� ����.
        }

        velocityY += gravity * Time.deltaTime;
        controller.Move(new Vector3(0f, velocityY, 0f) * Time.deltaTime);

        bodyAnim.SetFloat("velocityY", velocityY);     // �ִϸ������� �ĸ����͸� ����.
    }

    private void OnDrawGizmos()
    {
        if (groundChecker != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundChecker.position, groundRadius);
        }
    }
}