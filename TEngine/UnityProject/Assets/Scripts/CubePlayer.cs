using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogicHs
{
    public class CubePlayer : MonoBehaviour
    {
        public float speed;
        public VariableJoystick variableJoystick;
        public Rigidbody rb;
        
        private void Start()
        {
            variableJoystick = GameObject.FindObjectOfType<VariableJoystick>();
        }

        public void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            if (variableJoystick == null)
            {
                variableJoystick = GameObject.FindObjectOfType<VariableJoystick>();
                return;
            }
            Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

            // 设置物体朝向与移动方向一致
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
            rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}