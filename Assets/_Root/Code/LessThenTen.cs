using Unity.Collections;
using Unity.Jobs;


namespace _Root.Code
{
    public struct LessThenTen : IJob
    {

        private NativeArray<int> _ints;

        public LessThenTen(NativeArray<int> ints)
        {
            _ints = ints;
        }
        
        public void Execute()
        {
            for (int i = 0; i < _ints.Length; i++)
            {
                if (_ints[i] > 10)
                {
                    _ints[i] = 0;
                }
            }
        }
    }
}