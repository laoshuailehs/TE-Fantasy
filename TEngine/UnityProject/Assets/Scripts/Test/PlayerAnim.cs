using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogicHs
{
    public class PlayerAnim : MonoBehaviour
    {
        Animator anim;
        public float allowPlayerRotation = 0.1f;
        [Range(0,1f)]
        public float StartAnimTime = 0.3f;
        [Range(0, 1f)]
        public float StopAnimTime = 0.15f;
        CubePlayer player;
        void Start()
        {
            anim = GetComponent<Animator>();
            player = GetComponent<CubePlayer>();
        }

        
        void FixedUpdate()
        {
            anim.SetFloat ("walking", Mathf.Abs(player.rb.velocity.z));
            // if (player.speed > allowPlayerRotation) {
            //     anim.SetFloat ("Blend", player.speed, StartAnimTime, Time.deltaTime);
            //     player.Move();
            // } else if (player.speed < allowPlayerRotation) {
            //     anim.SetFloat ("Blend", player.speed, StopAnimTime, Time.deltaTime);
            // }
            
        }
    }
}
