using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieAnimationEvents : MonoBehaviour
{
    public UnityEvent OnDeathFinished = new UnityEvent();
    public UnityEvent OnStartAttack = new UnityEvent();
    public UnityEvent OnEndAttack = new UnityEvent();
    void OnDeath()
    {
        OnDeathFinished.Invoke();
    }

    void StartAttack()
    {
        OnStartAttack.Invoke();
    }

    void EndAttack()
    {
        OnEndAttack.Invoke();
    }
}
