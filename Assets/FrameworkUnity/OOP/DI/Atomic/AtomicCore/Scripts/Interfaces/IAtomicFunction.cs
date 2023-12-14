namespace Atomic
{
    public interface IAtomicFunction<out R> : IAtomicValue<R>
    {
        R Invoke();

        R IAtomicValue<R>.Value => this.Invoke();
    }

    public interface IAtomicFunction<in T, out R>
    {
        R Invoke(T args);
    }
}