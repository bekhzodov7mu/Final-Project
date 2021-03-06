﻿using ScriptableObjects;
using UnityEngine;

namespace BasePlayer
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] protected Joystick _joystick;
        [SerializeField] protected Rigidbody2D _rigidbody2D;
        [SerializeField] protected PlayerSettings _playerSettings;

        private float _horizontalMove;

        private void Update()
        {
            SetDirections();
        }

        private void FixedUpdate()
        {
            HorizontalMovement();
        }

        protected virtual void SetDirections()
        {
            _horizontalMove = _joystick.Horizontal;
        }

        protected virtual void HorizontalMovement()
        {                
            _rigidbody2D.velocity = new Vector2(_horizontalMove * _playerSettings._moveSpeed, _rigidbody2D.velocity.y);
        }
    }
}
