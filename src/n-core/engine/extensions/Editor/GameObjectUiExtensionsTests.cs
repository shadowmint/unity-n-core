using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using N;

public class GameObjectUiExtensionsTests : N.Tests.Test
{
    [Test]
    public void test_set_slider_value()
    {
        var obj = this.SpawnComponent<Slider>();
        Assert(obj.gameObject.SetSlider(1.0f));
        Object.DestroyImmediate(obj);
    }

    [Test]
    public void test_set_text_value()
    {
        var obj = this.SpawnComponent<UnityEngine.UI.Text>();
        Assert(obj.gameObject.SetText("Hello World"));
        Object.DestroyImmediate(obj);
    }
}
