using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D playerRigid;

    private void Init()
    {
        playerRigid = GetComponent<Rigidbody2D>();


    }

    void Start() => Init();

    void Update()
    {
        PlayerMove();
    }

    private void Keyboard()
    {
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
        
    }
}
