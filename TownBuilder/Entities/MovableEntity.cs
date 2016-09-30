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

namespace TownBuilder.Entities
{
    public class MovableEntity : CCNode
    {
        protected const int TileWidth = 16;
        protected const int TileHeight = 16;

        private CCTileMap _testTileMap;
        private readonly CCSprite _sprite;

        public MovableEntity(CCTileMap map, CCSprite sprite)
        {
            _testTileMap = map;

            // todo: Don't use the player sprite for everything that moves
            _sprite = sprite;
            _sprite.ContentSize = new CCSize(32, 32);

            _sprite.AnchorPoint = CCPoint.AnchorMiddle;
            AddChild(_sprite);
        }


        protected void MoveOneTileRight()
        {
            var newPosition = new CCPoint((((int)(PositionX / TileWidth)) + 1) * TileWidth, PositionY);

            if (CanEntityMoveToPoint(newPosition))
            {
                while (PositionX < newPosition.X)
                {
                    PositionX++;
                }
            }
        }

        protected void MoveOneTileLeft()
        {
            var newPosition = new CCPoint((((int)(PositionX / TileWidth)) - 1) * TileWidth, PositionY);

            if (CanEntityMoveToPoint(newPosition))
            {
                while (PositionX > newPosition.X)
                {
                    PositionX--;
                }
            }
        }

        protected void MoveOneTileUp()
        {
            var newPosition = new CCPoint(PositionX, (((int)(PositionY / TileHeight)) + 1) * TileHeight);

            if (CanEntityMoveToPoint(newPosition))
            {
                while (PositionY < newPosition.Y)
                {
                    PositionY++;
                }
            }
        }

        protected void MoveOneTileDown()
        {
            var newPosition = new CCPoint(PositionX, (((int)(PositionY / TileHeight)) - 1) * TileHeight);

            if (CanEntityMoveToPoint(newPosition))
            {
                while (PositionY > newPosition.Y)
                {
                    PositionY--;
                }
            }
        }

        private bool CanEntityMoveToPoint(CCPoint tileLocation)
        {
            foreach (CCTileMapLayer layer in _testTileMap.TileLayersContainer.Children)
            {
                var tileAtXy = layer.ClosestTileCoordAtNodePosition(tileLocation);

                var info = layer.TileGIDAndFlags(tileAtXy.Column, tileAtXy.Row);

                if (info != null)
                {
                    var properties = _testTileMap.TilePropertiesForGID(info.Gid);

                    if (properties != null && properties.ContainsKey("CanPlayerEnter") && properties["CanPlayerEnter"] == "false")
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}