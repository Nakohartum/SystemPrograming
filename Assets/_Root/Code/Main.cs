using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace _Root.Code
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private Vector3[] _vector3s;
        [SerializeField] private Vector3[] _velocities;
        public bool IsHealing;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken;
        private bool _result;
        
        
        private void Start()
        {
            
        }

        private void TestIJobParallel()
        {
            NativeArray<Vector3> positions = new NativeArray<Vector3>(10, Allocator.Persistent);
            NativeArray<Vector3> velocities = new NativeArray<Vector3>(10, Allocator.Persistent);
            NativeArray<Vector3> finalPositions = new NativeArray<Vector3>(10, Allocator.Persistent);

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = _vector3s[i];
                velocities[i] = _velocities[i];
            }

            Parallel parallel = new Parallel(positions, velocities, finalPositions);

            JobHandle hanlde = parallel.Schedule(10, 0);
            hanlde.Complete();

            for (int i = 0; i < finalPositions.Length; i++)
            {
                Debug.Log($"Position: {finalPositions[i]}");
            }

            positions.Dispose();
            velocities.Dispose();
            finalPositions.Dispose();
        }
        
        private void TestIJob()
        {
            NativeArray<int> ints = new NativeArray<int>(10, Allocator.Persistent);
            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = i + 3;
            }

            for (int i = 0; i < ints.Length; i++)
            {
                Debug.Log($"Old value of {i}: {ints[i]}");
            }
            
            LessThenTen lessThenTen = new LessThenTen(ints);
            JobHandle handle = lessThenTen.Schedule();
            handle.Complete();
            
            for (int i = 0; i < ints.Length; i++)
            {
                Debug.Log($"New value of {i}: {ints[i]}");
            }
            
            ints.Dispose();
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