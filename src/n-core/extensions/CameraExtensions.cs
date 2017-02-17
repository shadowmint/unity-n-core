using UnityEngine;

namespace N.Package.Core
{
  /// Camera extension methods
  /// Note that the extension methods here take Option<Camera> not Camera,
  /// because that's what the Camera() GameObjectExtensions method returns.
  public static class CameraExtensions
  {
    /// Project a world coordinate set to screen space.
    /// Screenspace is defined in pixels.
    /// The bottom-left of the screen is (0,0);
    /// The right-top is (pixelWidth,pixelHeight).
    /// The z position is in world units from the camera.
    public static Vector3 ScreenCoordinates(this Option<Camera> target, Vector3 position)
    {
      return target ? target.Unwrap().WorldToScreenPoint(position) : new Vector3(0.0f, 0.0f, 0.0f);
    }

    /// Project a gameobject into screen space by it's position
    public static Vector3 ScreenCoordinates(this Option<Camera> target, GameObject obj)
    {
      return target.ScreenCoordinates(obj.transform.position);
    }
  }
}