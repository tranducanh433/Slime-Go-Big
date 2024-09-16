using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension.Movement
{
    
    public static class MyControl
    {
        public static void TopDownMove2D(Rigidbody2D rb, Vector2 moveInput, float speed)
        {
            rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        }
        public static void PlatformerMove2D(Rigidbody2D rb, float moveInput, float speed)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        public static void PlatformerJump2D(Rigidbody2D rb, float jumpForce)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


    }
}