using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace _Root.Code
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        public bool IsHealing;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken;
        private bool _result;
        private void Start()
        {
            _cancellationToken = _cancellationTokenSource.Token;
            GetResult();
        }

        private async void GetResult()
        {
            _result = await TaskExample.WhatTaskFasterAsync(_cancellationToken, Task1(_cancellationToken),
                Task2(_cancellationToken));
            Debug.Log(_result);
        }
        
        private void Update()
        {
            if (IsHealing)
            {
                IsHealing = false;
                _unit.ReceiveHealing();
            }
        }
        
        public async Task Task1(CancellationToken cancellationToken)
        {
            float timer = 0f;
            while (timer < 1000f)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Debug.Log($"Task1 was cancelled");
                    return;
                }
                await Task.Delay(150);
                timer += 150f;
            }
            Debug.Log($"Task1 finished");
        }

        public async Task Task2(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 60; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Debug.Log($"Task2 was cancelled");
                    return;
                }
                await Task.Yield();
            }
            Debug.Log("Task2 Finished");
            _cancellationTokenSource.Cancel();
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}