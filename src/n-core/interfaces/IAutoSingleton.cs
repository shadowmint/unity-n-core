namespace N.Package.Core
{
  /// Implement this interface if you want an auto-creating singleton.
  public interface IAutoSingleton
  {
    /// The default name of the gameobject that will be created.
    string AutoSingletonName { get; }
  }
}