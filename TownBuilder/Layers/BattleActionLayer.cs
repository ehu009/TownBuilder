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
    public class BattleActionLayer : CCLayer
    {
        public BattleActionLayer()
        {
            var battleLabel = new CCLabel("Battle Go!", "Arial", 100, CCLabelFormat.SystemFont)
            {
                Position = new CCPoint(200, 300)
            };

            AddChild(battleLabel);
        }
    }
}