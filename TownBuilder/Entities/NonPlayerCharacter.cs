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
    class NonPlayerCharacter : MovableEntity
    {
        private readonly Random _rnd;

        public NonPlayerCharacter(CCTileMap map) : base(map, new CCSprite("player.png"))
        {
            Position = new CCPoint(92, 198);

            _rnd = new Random();
        }

        public void MoveEntityRandomly()
        {
            var num = _rnd.Next(10);

            switch (num)
            {
                case 1:
                    MoveOneTileDown();
                    break;
                case 2:
                    MoveOneTileRight();
                    break;
                case 3:
                    MoveOneTileLeft();
                    break;
                case 4:
                    MoveOneTileUp();
                    break;
                default:
                    break;
            }

        }
    }
}