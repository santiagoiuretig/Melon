using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerBehaviour : Character
{

    public float lapse = 5f;
    public float timeBetweenActions = 2f;

    Queue<IEnumerator> actions = new Queue<IEnumerator>();
    public static bool isPlayingActions = false;
    
    void Awake()
    {
        ActionManager.OnActionSelected += OnActionSelected;
        Bullet.OnBulletHit += OnBulletHit;
    }
    
    void OnDestroy()
    {
        ActionManager.OnActionSelected -= OnActionSelected;
        Bullet.OnBulletHit -= OnBulletHit;
    }
    
    private void OnActionSelected(ActionManager.Action action, Vector2 position)
    {
        actions.Enqueue(Walk(position));
        actions.Enqueue(Wait(timeBetweenActions));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlayingActions)
        {
            isPlayingActions = true;
            StartCoroutine(PlayActions());
        }
    }

    private IEnumerator PlayActions()
    {
        while (isPlayingActions)
        {
            if (actions.Count > 0)
            {
                yield return StartCoroutine(actions.Dequeue());
            }
            else
            {
                isPlayingActions = false;
            }
        }
    }
    
    private IEnumerator Walk(Vector2 toPosition)
    {
        StartMoving(transform.position, toPosition, lapse);

        while (KeepMoving())
        {
            yield return null;
        }
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private void OnBulletHit(GameObject target)
    {
        if (target == this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
