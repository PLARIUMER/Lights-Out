using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed, jumpPower;

    private Rigidbody2D playerRigid;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
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
}
