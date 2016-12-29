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

        private bool _finishedMoving;   //  force entities to move one tile & direction at a time

        public MovableEntity(LocalMap map, CCSprite sprite)
        {
            _testTileMap = map;

            this._finishedMoving = true;

            // todo: Don't use the player sprite for everything that moves
            _sprite = sprite;
            _sprite.ContentSize = new CCSize(32, 32);

            _sprite.AnchorPoint = CCPoint.AnchorMiddle;
            AddChild(_sprite);
        }
        
        public bool isMoving()
        {   // use this in subclass' move methods before performing movement
            return !this._finishedMoving;
        }
        

        public CCPoint CurrentTile => Position.ToTileCoordinates();

        public bool IsAdjacentToTile(CCPoint tile)
        {
            var entityTile = Position.ToTileCoordinates();

            return Math.Abs(entityTile.X - tile.X) == 1 && Math.Abs(entityTile.Y - tile.Y) == 1;
        }


        private void MoveOneTile(CCPoint direction)
        {
            direction.X *= TileWidth;
            direction.Y *= TileHeight;

            if (_testTileMap.ShouldEntityMoveToLocation(this.Position + direction))
            {
                this._finishedMoving = false;
                //  to-do: arrange for more dynamic changing of walk speed
                this.PerformMovement(new CCMoveBy(this._testTileMap.walkSpeed0, direction));
                //  i.e. encode travel speed modification in TileMaps used by the map layer
            }
            else
            {
                this._finishedMoving = true;
            }
        }

        private async void PerformMovement(CCMoveBy moveAction)
        {
            await this.RunActionAsync(moveAction);
            this._finishedMoving = true;
        }

        protected void MoveOneTileRight()
        {
            CCPoint dir;
            dir.X = 1;
            dir.Y = 0;
            this.MoveOneTile(dir);
        }

        protected void MoveOneTileLeft()
        {
            CCPoint dir;
            dir.X = -1;
            dir.Y = 0;
            this.MoveOneTile(dir);
        }

        protected void MoveOneTileUp()
        {
            CCPoint dir;
            dir.X = 0;
            dir.Y = 1;
            this.MoveOneTile(dir);
        }

        protected void MoveOneTileDown()
        {
            CCPoint dir;
            dir.X = 0;
            dir.Y = -1;
            this.MoveOneTile(dir);
        }
    }
}