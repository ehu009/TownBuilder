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

namespace TownBuilder.Extensions
{
    public static class CCPointExtensions
    {
        // Use square tiles
        private const int TileSideSize = 16;

        public static CCPoint ToOnScreenLocation(this CCPoint tileCoordinates)
        {
            if (tileCoordinates == null)
            {
                return new CCPoint(0, 0);
            }

            // In case we're passed fractional coordinates - cast to int to round to the nearest integer. We don't want to deal with fractional tiles.
            var newX = (int)(tileCoordinates.X * TileSideSize);
            var newY = (int)(tileCoordinates.Y * TileSideSize);

            return new CCPoint(newX, newY);
        }

        public static CCPoint ToTileCoordinates(this CCPoint onScreenLocation)
        {
            if (onScreenLocation == null)
            {
                return new CCPoint(0, 0);
            }

            var newX = (Math.Floor(onScreenLocation.X / TileSideSize));
            var newY = Math.Floor(onScreenLocation.Y / TileSideSize);

            return new CCPoint((float)newX, (float)newY);
        }

        public static bool DestinationIsCloserVertically(this CCPoint original, CCPoint locationToCompare)
        {
            var diff = locationToCompare - original;

            return Math.Abs(diff.X) > Math.Abs(diff.Y);
        }
    }
}