namespace Assets.Script.Base.Items
{
    /// <summary>
    /// Result DTO for <see cref="ICollectable{T}.OnCollect"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct CollectResult<T>
    {
        public bool IsSuccess { get; set; }

        public T Result { get; set; }
    }

    /// <summary>
    /// Base contract for the collectable object in game
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectable<T>
    {
        CollectResult<T> OnCollect();
    }
}
