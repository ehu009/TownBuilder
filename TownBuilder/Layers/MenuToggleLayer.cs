using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CocosSharp;

namespace TownBuilder.Layers
{
    /// <summary>
    /// Displays the UI for the menu toggle. This should be visible at most times
    /// </summary>
    class MenuToggleLayer : CCLayer
    {
        public MenuToggleLayer()
        {
            CreateToggleInterface();
        }

        private void CreateToggleInterface()
        {
            var label = new CCLabel("Open Menu", "Arial", 50, CCLabelFormat.SystemFont)
            {
                Position = new CCPoint(100, 25)
            };

            AddChild(label);
        }
    }
}