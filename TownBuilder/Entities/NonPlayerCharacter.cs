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
    public class NonPlayerCharacter : MovableEntity
    {
        private readonly Random _rnd;

        public NonPlayerCharacter(LocalMap map) : base(map, new CCSprite("player.png"))
        {
            Position = new CCPoint(5, 10).ToOnScreenLocation();

            _rnd = new Random();
        }

        public void Interact()
        {
            MoveOneTileDown();
            MoveOneTileDown();
            MoveOneTileDown();
        }

        public void MoveEntityRandomly()
        {
            if (this.isMoving())
            {
                return;
            }
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