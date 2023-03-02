using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace _Root.Code
{
    public struct Parallel : IJobParallelFor
    {
        private NativeArray<Vector3> _positions;
        private NativeArray<Vector3> _velocities;
        private NativeArray<Vector3> _finalPosition;

        public Parallel(NativeArray<Vector3> positions, NativeArray<Vector3> velocities, NativeArray<Vector3> finalPosition)
        {
            _positions = positions;
            _velocities = velocities;
            _finalPosition = finalPosition;
        }
        
        public void Execute(int index)
        {
            _finalPosition[index] = _positions[index] + _velocities[index];
        }
    }
}