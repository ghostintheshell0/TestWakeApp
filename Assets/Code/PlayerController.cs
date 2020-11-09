using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent Dead;
    public UnityEvent Step;
    [SerializeField] private Rigidbody2D body = default;
    [SerializeField] private float speed = default;
    private float actualSpeed = 0f;

    [SerializeField] private List<Panel> panels = default;

    private void Start()
    {
        Revive();
    }
    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var velocity = new Vector2();

        if(x != 0f || y != 0f)
        {
            if(x != 0f)
            {
                velocity.x = (x > 0f ? 1f : -1f);
            }
            else
            {
                velocity.y = (y > 0f ? 1f : -1f);
            }

            body.velocity = velocity * actualSpeed;
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
        actualSpeed = speed;
    }

    public void Stop()
    {
        actualSpeed = 0;
        body.velocity = Vector2.zero;
    }
}
