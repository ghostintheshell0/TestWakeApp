using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent Dead;
    public UnityEvent Step;
    [SerializeField] private Rigidbody2D body = default;
    [SerializeField] private Animator animator = default;
    [SerializeField] private float speed = default;
    [SerializeField] private List<Panel> panels = default;

    private void Start()
    {
        Revive();
    }
    private void Update()
    {

        if(body.simulated != true) return;
        
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var velocity = new Vector2();
        int animation = 0;

        if(x != 0f || y != 0f)
        {
            if(x != 0f)
            {
                velocity.x = (x > 0f ? 1f : -1f);
                animation = x > 0f ? AnimationNames.Right : AnimationNames.Left;
            }
            else
            {
                velocity.y = (y > 0f ? 1f : -1f);
                animation = y > 0f ? AnimationNames.Up : AnimationNames.Down;
            }

            body.velocity = velocity * speed;
            animator.Play(animation);
        }
    }

    public void AddPanel(Panel panel)
    {
        panels.Add(panel);
        Step.Invoke();
    }
    public void RemovePanel(Panel panel)
    {
        panels.Remove(panel);
        if(panels.Count == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Stop();
        Dead.Invoke();
    }
    
    public void Revive()
    {
        body.simulated = true;
        panels.Clear();
    }

    public void Stop()
    {
        body.velocity = Vector2.zero;
        body.simulated = false;
        animator.Play(AnimationNames.Idle);
    }
}

public static class AnimationNames
{
    public static int Up = Animator.StringToHash("Base Layer.Up");
    public static int Down = Animator.StringToHash("Base Layer.Down");
    public static int Left = Animator.StringToHash("Base Layer.Left");
    public static int Right = Animator.StringToHash("Base Layer.Right");
    public static int Idle = Animator.StringToHash("Base Layer.Idle");
}