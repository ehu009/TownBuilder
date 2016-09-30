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

namespace TownBuilder.Layers
{
    class PlayerLayer : CCLayer
    {
        private Player _player;
        private CCTileMap _map;

        protected const int TileWidth = 16;
        protected const int TileHeight = 16;

        public PlayerLayer(CCTileMap map)
        {
            _map = map;
        }

        public CCPoint PlayerPosition => _player.Position;

        protected override void AddedToScene()
        {
            base.AddedToScene();

            _player = new Player(_map);
            AddChild(_player);
        }

        public void ReceiveInput(CCPoint location)
        {
            _player.MovePlayer(RoundPointToTile(location));
        }

        private static CCPoint RoundPointToTile(CCPoint point)
        {
            return new CCPoint(((int)(point.X / TileWidth)) * TileWidth, (((int)(point.Y / TileHeight)) * TileHeight));
        }
    }
}