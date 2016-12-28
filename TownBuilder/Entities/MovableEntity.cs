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
using TownBuilder.Extensions;

namespace TownBuilder.Entities
{
    public class MovableEntity : CCNode
    {
        protected const int TileWidth = 16;
        protected const int TileHeight = 16;

        private readonly LocalMap _testTileMap;
        private readonly CCSprite _sprite;

        public MovableEntity(LocalMap map, CCSprite sprite)
        {
            _testTileMap = map;

            // todo: Don't use the player sprite for everything that moves
            _sprite = sprite;
            _sprite.ContentSize = new CCSize(32, 32);

            _sprite.AnchorPoint = CCPoint.AnchorMiddle;
            AddChild(_sprite);
        }

        public CCPoint CurrentTile => Position.ToTileCoordinates();

        public bool IsAdjacentToTile(CCPoint tile)
        {
            return Math.Abs(CurrentTile.X - tile.X) == 1 && Math.Abs(CurrentTile.Y - tile.Y) == 1;
        }

        public void MoveToTile(CCPoint tileCoordinates)
        {
            var diff = tileCoordinates - CurrentTile;

            if (diff.X == 0 && diff.Y == 0)
            {
                return;
            }

            if (CurrentTile.DestinationIsCloserVertically(tileCoordinates))
            {
                if (diff.X > 0)
                {
                    MoveOneTileRight();
                }
                else
                {
                    MoveOneTileLeft();
                }
            }
            else
            {
                if (diff.Y > 0)
                {
                    MoveOneTileUp();
                }
                else
                {
                    MoveOneTileDown();
                }
            }
        }

        protected void MoveOneTileRight()
        {
            var newPosition = new CCPoint((PositionX + TileWidth), PositionY);

            if (_testTileMap.ShouldEntityMoveToLocation(newPosition))
            {
                while (Position != newPosition)
                {
                    PositionX++;
                }
            }
        }

        protected void MoveOneTileLeft()
        {
            var newPosition = new CCPoint((PositionX - TileWidth), PositionY);

            if (_testTileMap.ShouldEntityMoveToLocation(newPosition))
            {
                while (Position != newPosition)
                {
                    PositionX--;
                }
            }
        }

        protected void MoveOneTileUp()
        {
            var newPosition = new CCPoint(PositionX, (PositionY + TileHeight));

            if (_testTileMap.ShouldEntityMoveToLocation(newPosition))
            {
                while (Position != newPosition)
                {
                    PositionY++;
                }
            }
        }

        protected void MoveOneTileDown()
        {
            var newPosition = new CCPoint(PositionX, PositionY - TileHeight);

            if (_testTileMap.ShouldEntityMoveToLocation(newPosition))
            {
                while (Position != newPosition)
                {
                    PositionY--;
                }
            }
        }
    }
}