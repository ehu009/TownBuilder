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
using TownBuilder.Layers;
using CocosSharp;

namespace TownBuilder.Handler
{
    public class PlayAreaTouchHandler
    {
        private readonly LocalMap _map;
        private readonly PlayerLayer _player;

        public PlayAreaTouchHandler(PlayerLayer player, LocalMap map)
        {
            _player = player;
            _map = map;
        }

        public void ReceiveTouch(CCPoint touchLocation)
        {
            var lastPlayerPosition = _player.PlayerPosition;

            _player.ReceiveInput(touchLocation);
            _map.ReceiveInput();

            var diff = (lastPlayerPosition - _player.PlayerPosition);

            // todo: add logic to determine if map should move
            var shouldMapMove = true;
            if (shouldMapMove)
            {
                _map.MoveMapByDiff(diff);

                _player.PlayerPosition += diff;
            }
        }
    }
}