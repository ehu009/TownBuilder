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
    public class MainScene : CCScene
    {
        private readonly CCGameView _gameView;
        private readonly LocalMap _mapLayer;
        private readonly MenuLayer _menuLayer;

        private readonly CCEventListenerTouchAllAtOnce _touchListener;

        private bool _menuIsOpen;

        private readonly CCTileMap _map;

        public MainScene(CCGameView gameView) : base(gameView)
        {
            _map = new CCTileMap("TileMaps/dungeon.tmx");

            _mapLayer = new LocalMap(_map);

            _menuLayer = new MenuLayer();

            AddLayer(_mapLayer);
            AddLayer(new MenuToggleLayer());

            // Register for touch events
            _touchListener = new CCEventListenerTouchAllAtOnce
            {
                OnTouchesBegan = HandleInput
            };
            _mapLayer.AddEventListener(_touchListener);

            _gameView = gameView;
        }

        private void HandleInput(List<CCTouch> touches, CCEvent touchEvent)
        {
            var firstTouch = touches[0];

            if (ShouldToggleMenu(firstTouch.Location))
            {
                ToggleMainMenu();
            }
            else if (ShouldToggleBattle(firstTouch.Location))
            {
                EnterBattle();
            }
            else
            {
                _mapLayer.ReceiveInput(firstTouch.Location);
            }
        }

        private bool ShouldToggleMenu(CCPoint  touchLocation)
        {
            return (touchLocation != null && touchLocation.Y < 50 && touchLocation.X < 175) || MenuIsOpen;
        }

        private bool ShouldToggleBattle(CCPoint touchLocation)
        {
            return touchLocation != null && touchLocation.Y < 50 && touchLocation.X > 225;
        }

        private bool MenuIsOpen {
            get
            {
                return _menuIsOpen;
            }
            set
            {
                _menuIsOpen = value;
                if (value)
                {
                    AddChild(_menuLayer);
                }
                else
                {
                    RemoveChild(_menuLayer, false);
                }
            }
        }

        public void ToggleMainMenu()
        {
            MenuIsOpen = !MenuIsOpen;
        }

        public void EnterBattle()
        {
            _gameView.Director.PushScene(new BattleScene(_gameView, this));
        }
    }
}