namespace N 
{
    /// <summary>
    /// Implement this interface if you want an auto-creating singleton.
    /// </summary>

	public interface IAutoSingleton
	{
		/// <summary>
		/// The default name of the gameobject that will be created.
		/// </summary>
		string AutoSingletonName { get; }
	}

}
