using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public UnityEvent Completed = default;
    [SerializeField] private List<Panel> panels = default;
    [SerializeField] private PlayerController player = default;
    [SerializeField] private Panel firstPabel = default;
    [SerializeField] private float gameEndDelay = 0.1f;

    private Coroutine gameCompleteCoroutine = default;

    private int progres = 0;

    private void Start()
    {
        StartLevel();
        player.Step.AddListener(StepHandler);
    }

    public void StartLevel()
    {
        player.transform.position = firstPabel.transform.position;

        for(int i = 0; i < panels.Count; ++i)
        {
            panels[i].Show();
        }
        progres = 0;
        player.Revive();
    }

    private void StepHandler()
    {
        progres++;
        if(progres == panels.Count)
        {
            if(gameCompleteCoroutine != null)
            {
                StopCoroutine(gameCompleteCoroutine);
            }
            gameCompleteCoroutine = StartCoroutine(GameCompleteDelay());
        }
    }

    private IEnumerator GameCompleteDelay()
    {
        yield return new WaitForSeconds(gameEndDelay);
        player.Stop();
        gameCompleteCoroutine = default;
        Completed.Invoke();
    }

    private void OnValidate()
    {
        panels.Clear();
        var panelsOnStage = GameObject.FindObjectsOfType<Panel>();
        panels.AddRange(panelsOnStage);
    }
}
