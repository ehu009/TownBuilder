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

namespace TownBuilder.Scenes
{
    public class MenuLayer : CCLayer
    {
        private readonly CCLayer _layer;

        public MenuLayer()
        {
            _layer = new CCLayer();

            AddChild(_layer);

            CreateText();
            CreateTouchListener();
        }

        private void CreateTouchListener()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce
            {
                OnTouchesBegan = HandleTouch
            };
            _layer.AddEventListener(touchListener);
        }

        private void CreateText()
        {
            var label = new CCLabel("Main Menu Goes Here", "Arial", 30, CCLabelFormat.SystemFont)
            {
                Position = new CCPoint(192, 298)
            };

            _layer.AddChild(label);
        }

        private static void HandleTouch(List<CCTouch> touches, CCEvent touchEvent)
        {
            // Don't do anything for now.
        }
    }
}