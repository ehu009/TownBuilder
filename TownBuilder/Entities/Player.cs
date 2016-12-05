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
    public interface IPlayer
    {
        void MovePlayer(CCPoint point);
    }

    public class Player : MovableEntity, IPlayer
    {
        public Player(LocalMap map) : base(map, new CCSprite("player.png"))
        {
            Position = new CCPoint(10, 10).ToOnScreenLocation();
        }

        public void MovePlayer(CCPoint point)
        {
            var playerShouldMoveHorizontally = Math.Abs(PositionX - point.X) > Math.Abs(PositionY - point.Y);

            if (playerShouldMoveHorizontally)
            {
                if (point.X > PositionX)
                {
                    MoveOneTileRight();
                }
                else if (point.X < PositionX)
                {
                    MoveOneTileLeft();
                }
            }
            else
            {
                if (point.Y > PositionY)
                {
                    MoveOneTileUp();
                }
                else if (point.Y < PositionY)
                {
                    MoveOneTileDown();
                }
            }
        }
    }
}