using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float _health;
    private Coroutine _healingRoutine;

    public void ReceiveHealing()
    {
        if (_healingRoutine != null)
        {
            Debug.Log("Healing already started");
            return;
        }
        _healingRoutine = StartCoroutine(Healing());
    }

    private IEnumerator Healing()
    {
        float timer = 0.0f;
        do
        {
            _health += 5;
            if (_health > 100)
            {
                _health = 100;
            }
            yield return new WaitForSeconds(0.5f);
            timer += 0.5f;
            Debug.Log($"Health: {_health}. Timer: {timer}");
        } while (_health < 100 && timer < 3.0f);

        _healingRoutine = null;
    }

    
}
