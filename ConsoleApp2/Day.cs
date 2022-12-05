public abstract class Day
{
    protected abstract string testInput { get; }
    protected abstract string input { get; }

    public void Solve()
    {
        Run(input);
    }

    public void Test()
    {
        Run(testInput);
    }

    protected abstract void Run(string input);
}