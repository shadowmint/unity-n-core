using UnityEngine;
using UnityEngine.UI;
using N.Tests;
using N;

namespace N {

  /// Camera extension methods
  public static class GameObjectUiExtensions {

    /// Set the value of a slider
    public static bool SetSlider(this GameObject target, float value) {
      var slider = target.GetComponent<Slider>();
      if (slider != null) {
        slider.normalizedValue = value;
        return true;
      }
      return false;
    }

    /// Set the value of a text area
    public static bool SetText(this GameObject target, string value) {

      // Normal text type?
      var text = target.GetComponent<Text>();
      if (text != null) {
        text.text = value;
        return true;
      }

      // For a button, etc where there's a child Text object?
      text = target.GetComponentInChildren<Text>();
      if (text != null) {
        text.text = value;
        return true;
      }

      return false;
    }

    /// Set the value of a text area
    public static bool SetText(this GameObject target, string format, params object[] args) {
      var msg = string.Format(format, args);
      return target.SetText(msg);
    }

    /// Set the color of some text
    public static bool SetTextColor(this GameObject target, Color value) {

      // Normal text type?
      var text = target.GetComponent<Text>();
      if (text != null) {
        text.color = value;
        return true;
      }

      // For a button, etc where there's a child Text object?
      text = target.GetComponentInChildren<Text>();
      if (text != null) {
        text.color = value;
        return true;
      }

      return false;
    }


    /// Set the value of a text area
    public static bool SetEnabled(this GameObject target, bool value) {
      var active = target.GetComponent<Selectable>();
      if (active != null) {
        active.interactable = value;
        return true;
      }
      return false;
    }
  }

  /// Tests
  public class GameObjectUiExtensionsTests : TestSuite {

    public void test_set_slider_value() {
      var obj = this.SpawnComponent<Slider>();
      Assert(obj.gameObject.SetSlider(1.0f));
      Object.DestroyImmediate(obj);
    }

    public void test_set_text_value() {
      var obj = this.SpawnComponent<Text>();
      Assert(obj.gameObject.SetText("Hello World"));
      Object.DestroyImmediate(obj);
    }
  }
}
