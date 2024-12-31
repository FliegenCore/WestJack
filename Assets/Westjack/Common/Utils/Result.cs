namespace Common.Utils
{
    public struct Result<T>
    {
        public readonly T Object;
        public readonly bool IsExit;

        public Result(T result, bool isExit)
        {
            Object = result;
            IsExit = isExit;
        }
    }
}
