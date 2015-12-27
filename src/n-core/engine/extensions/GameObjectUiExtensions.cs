using UnityEngine;
using UnityEngine.UI;
using N.Tests;

namespace N
{
    /// Camera extension methods
    public static class GameObjectUiExtensions
    {
        /// Set the value of a slider
        public static bool SetSlider(this GameObject target, float value)
        {
            var slider = target.GetComponent<Slider>();
            if (slider != null)
            {
                slider.normalizedValue = value;
                return true;
            }
            return false;
        }

        /// Set the value of a text area
        public static bool SetText(this GameObject target, string value)
        {

            // Normal text type?
            var text = target.GetComponent<Text>();
            if (text != null)
            {
                text.text = value;
                return true;
            }

            // For a button, etc where there's a child Text object?
            text = target.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = value;
                return true;
            }

            return false;
        }

        /// Set the value of a text area
        public static bool SetText(this GameObject target, string format, params object[] args)
        {
            var msg = string.Format(format, args);
            return target.SetText(msg);
        }

        /// Set the color of some text
        public static bool SetTextColor(this GameObject target, Color value)
        {

            // Normal text type?
            var text = target.GetComponent<Text>();
            if (text != null)
            {
                text.color = value;
                return true;
            }

            // For a button, etc where there's a child Text object?
            text = target.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.color = value;
                return true;
            }

            return false;
        }

        /// Set the value of a text area
        public static bool SetEnabled(this GameObject target, bool value)
        {
            var active = target.GetComponent<Selectable>();
            if (active != null)
            {
                active.interactable = value;
                return true;
            }
            return false;
        }

        /// Add a child elements to a scrollable block.
        /// Use this on the 'Content' elements of a ScrollView UI item.
        /// @param child The child object to add, notice it must have a fixed height
        /// @param offset The offset into the scrollable region
        public static void AddChildToRegion(this GameObject self, GameObject child, int offset)
        {
            var pt = self.GetComponent<RectTransform>();
            var ct = child.GetComponent<RectTransform>();
            var magic = ct.sizeDelta.y;
            pt.sizeDelta = pt.sizeDelta + new Vector2(0f, magic);
            child.SetParent(self);
            ct.offsetMin = new Vector2(0f, -magic * offset);
            ct.offsetMax = new Vector2(0f, -magic * offset + magic);
        }

        /// Find Marker
        public static Option<GameObject> Marker(this GameObject target, string name)
        {
            return N.Marker.Find(name, target);
        }

        /// Add a click event to a button
        public static void AddClickEvent(this GameObject target, UnityEngine.Events.UnityAction callback, bool replaceExistingEvents = true)
        {
            var button = target.GetComponent<Button>();
            if (button != null)
            {
                if (replaceExistingEvents)
                {
                    button.onClick.RemoveAllListeners();
                }
                button.onClick.AddListener(callback);
            }
        }
    }
}
