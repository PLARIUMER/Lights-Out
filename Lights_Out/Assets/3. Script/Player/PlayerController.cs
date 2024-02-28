using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed, jumpPower, dashPower;

    private Rigidbody2D playerRigid;
    private bool isDash = true;

    private void Init()
    {
        playerRigid = GetComponent<Rigidbody2D>();

        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard; 
    }

    private void Start() => Init();

    private void OnKeyboard()
    {
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetMouseButtonDown(1)) Dash();
    }

    private void PlayerMove()
    {
        float x = Input.GetAxisRaw("Horizontal");

        transform.Translate(x * speed * Time.deltaTime, 0f, 0f);
    }

    private void Jump()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (groundHit.collider != null)
        {
            //animator.Play("Jump");

            playerRigid.velocity = new Vector2(0f, jumpPower);
        }
        Debug.DrawRay(transform.position, Vector3.down, new Color(0, 1, 0));
    }

    private void Dash()
    {
        if (isDash)
        {
            isDash = false;
            playerRigid.gravityScale = 0f;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mousePos - (Vector2)transform.position;

            playerRigid.AddForce(transform.up = dashPower * dir.normalized, ForceMode2D.Impulse);

            StartCoroutine(DashGravity());
        }
    }

    private IEnumerator DashGravity()
    {
        yield return new WaitForSeconds(0.075f);
        playerRigid.gravityScale = 9.8f;
        yield return new WaitForSeconds(3f);
        isDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            playerRigid.gravityScale = 9.8f;
        }
    }
}
