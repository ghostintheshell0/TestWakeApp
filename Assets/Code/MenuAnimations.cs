using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimations : MonoBehaviour
{
    [SerializeField] private Transform particleTemplate = default;
    [SerializeField] private int count = default;
    [SerializeField] private float radius = default;
    [SerializeField] private float speed = default;
    private List<Transform> particles = new List<Transform>();
    [SerializeField] private GameObject menuUI = default;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3();
        var delta = 360f / count;
        for(int i = 0; i < count; ++i)
        {
            var particle = Instantiate(particleTemplate);
            var angle = delta * i * Mathf.Deg2Rad;
            pos.x = Mathf.Cos(angle) * radius;
            pos.y = Mathf.Sin(angle) * radius;
            particle.position = pos + transform.position;
            particles.Add(particle);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Stop();
            menuUI.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        for(int i = 0; i < particles.Count; ++i)
        {
            var dir = (transform.position - particles[i].position).normalized;
            dir = Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.forward) * dir;
            particles[i].position = dir * radius + transform.position;
        }
    }

    private void Rotate()
    {
        for(int i = 0; i < particles.Count; ++i)
        {
            var dir = (transform.position - particles[i].position).normalized;
            var rotation = Mathf.Atan2(dir.y, dir.x);
            particles[i].rotation = Quaternion.Euler(0f, 0f, rotation * Mathf.Rad2Deg);
        }
    }
    private void Stop()
    {
        enabled = false;
        for(int i = 0; i < particles.Count; ++i)
        {
            Destroy(particles[i].gameObject);
        }
    }
}
