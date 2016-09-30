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
        private readonly LocalMap _mapLayer;
        private readonly PlayerLayer _playerLayer;
        private readonly MenuLayer _menuLayer;

        private readonly CCEventListenerTouchAllAtOnce _touchListener;

        private bool _menuIsOpen;

        private readonly CCTileMap _map;

        public MainScene(CCGameView gameView) : base(gameView)
        {
            _map = new CCTileMap("TileMaps/dungeon.tmx");

            _mapLayer = new LocalMap(_map);

            _playerLayer = new PlayerLayer(_map);

            _menuLayer = new MenuLayer();

            AddLayer(_mapLayer);
            AddLayer(_playerLayer);

            // Register for touch events
            _touchListener = new CCEventListenerTouchAllAtOnce
            {
                OnTouchesBegan = HandleInput
            };
            _mapLayer.AddEventListener(_touchListener);

        }

        private void HandleInput(List<CCTouch> touches, CCEvent touchEvent)
        {
            var firstTouch = touches[0];

            var lastPlayerPosition = _playerLayer.PlayerPosition;

            if ((firstTouch.Location != null && firstTouch.Location.Y < 50) || MenuIsOpen)
            {
                ToggleMainMenu();
            }
            else
            {
                _mapLayer.HandleTouch();
                _playerLayer.ReceiveInput(firstTouch.Location);
            }

            var diff = lastPlayerPosition - _playerLayer.PlayerPosition;

            _map.TileLayersContainer.Position += diff;
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
    }
}