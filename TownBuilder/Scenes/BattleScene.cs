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
using TownBuilder.Layers;

namespace TownBuilder.Scenes
{
    public class BattleScene : CCScene
    {
        private readonly CCGameView _gameView;
        private readonly MainScene _mainScene;
        private readonly BattleActionLayer _actionLayer;

        public BattleScene(CCGameView gameView, MainScene mainScene) : base(gameView)
        {
            _actionLayer = new BattleActionLayer();

            AddLayer(_actionLayer);

            var touchListener = new CCEventListenerTouchAllAtOnce
            {
                OnTouchesBegan = HandleInput
            };

            _mainScene = mainScene;
            _gameView = gameView;

            _actionLayer.AddEventListener(touchListener);
        }

        private void HandleInput(List<CCTouch> touches, CCEvent touchEvent)
        {
            _gameView.Director.PopScene();
        }
    }
}