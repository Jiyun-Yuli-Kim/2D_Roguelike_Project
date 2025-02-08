using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        if (_moveDirection == Vector2.zero)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        _rb.velocity =  _moveDirection * _playerSpeed;
    }
}
