using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    
    private float _moveInput;
    private bool _isGrounded;
    private Rigidbody2D _body;

    private float _jumpTimeCounter;
    private bool _isJumping;
    public float jumpTime;
    
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        _body.velocity = new Vector2(_moveInput * speed, _body.velocity.y);
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _isJumping = true;
            _jumpTimeCounter = jumpTime;
            _body.velocity = Vector2.up * jumpForce;
        }

        if (_isJumping && Input.GetKey(KeyCode.Space))
        {
            if (_jumpTimeCounter >= 0)
            {
                _body.velocity = Vector2.up * jumpForce;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
        }
    }
}
