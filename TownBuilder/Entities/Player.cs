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
    public class Player : MovableEntity
    {
        private CCPoint _destination;

        public Player(LocalMap map) : base(map, new CCSprite("player.png"))
        {
            Position = new CCPoint(10, 10).ToOnScreenLocation();

            _destination = CurrentTile;

            Schedule(MovePlayer);
        }

        public void SetDestination(CCPoint destinationTile)
        {
            _destination = destinationTile;
        }

        private void MovePlayer(float timeInSeconds)
        {
            MoveToTile(_destination);
        }
    }
}