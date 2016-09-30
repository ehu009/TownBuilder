using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using CocosSharp;
using TownBuilder.Scenes;

namespace TownBuilder
{
    [Activity(Label = nameof(TownBuilder), MainLauncher = true, Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our game view from the layout resource,
            // and attach the view created event to it
            var gameView = (CCGameView)FindViewById(Resource.Id.GameView);
            gameView.ViewCreated += LoadGame;
        }

        static void LoadGame(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;

            if (gameView != null)
            {
                var contentSearchPaths = new List<string> { "Fonts", "Sounds" };
                var viewSize = gameView.ViewSize;

                const int width = 768;
                const int height = 1027;

                // Set world dimensions
                gameView.DesignResolution = new CCSizeI(width, height);

                // Determine whether to use the high or low def versions of our images
                // Make sure the default texel to content size ratio is set correctly
                // Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
                if (width < viewSize.Width)
                {
                    contentSearchPaths.Add("Images/Hd");
                    CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
                }
                else
                {
                    contentSearchPaths.Add("Images/Ld");
                    CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
                }

                gameView.DesignResolution = new CCSizeI(384, 496);

                gameView.ContentManager.SearchPaths = contentSearchPaths;

                CCScene gameScene = new MainScene(gameView);

                gameView.RunWithScene(gameScene);
            }
        }
    }
}

