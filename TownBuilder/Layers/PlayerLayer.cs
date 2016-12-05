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
using TownBuilder.Entities;
using TownBuilder.Extensions;

namespace TownBuilder.Layers
{
    public class PlayerLayer : CCLayer
    {
        private Player _player;
        private LocalMap _map;

        protected const int TileWidth = 16;
        protected const int TileHeight = 16;

        public PlayerLayer(LocalMap map)
        {
            _map = map;
        }

        public CCPoint PlayerPosition
        {
            get { return _player.Position; }
            set { _player.Position = value; }
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            _player = new Player(_map);
            AddChild(_player);
        }

        public void ReceiveInput(CCPoint location)
        {
            if (location == null)
            {
                return;
            }

            _player.MovePlayer(location);
        }
    }
}