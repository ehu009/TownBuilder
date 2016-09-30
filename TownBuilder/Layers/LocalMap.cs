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
using TownBuilder.Scenes;

namespace TownBuilder.Layers
{
    public class LocalMap : CCLayer
    {

        private CCTileMap _testTileMap;
        private NonPlayerCharacter _npc;

        public LocalMap(CCTileMap map)
        {
            _testTileMap = map;
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            _testTileMap.Antialiased = false;
            AddChild(_testTileMap);

            _npc = new NonPlayerCharacter(_testTileMap);
            AddChild(_npc);

            HandleCustomTileProperties(_testTileMap);

            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;
        }

        public void HandleTouch()
        {
            _npc.MoveEntityRandomly();
        }

        void HandleCustomTileProperties(CCTileMap tileMap)
        {
            var tileDimension = (int)tileMap.TileTexelSize.Width;

            var numberOfColumns = (int)tileMap.MapDimensions.Size.Width;
            var numberOfRows = (int)tileMap.MapDimensions.Size.Height;

            foreach (CCTileMapLayer layer in tileMap.TileLayersContainer.Children)
            {
                for (int column = 0; column < numberOfColumns; column++)
                {
                    var worldX = tileDimension * column + tileDimension / 2;

                    for (int row = 0; row < numberOfRows; row++)
                    {
                        var worldY = tileDimension * row + tileDimension / 2;

                        HandleCustomTilePropertyAt(worldX, worldY, layer);
                    }
                }
            }
        }

        void HandleCustomTilePropertyAt(int worldX, int worldY, CCTileMapLayer layer)
        {
            var tileAtXy = layer.ClosestTileCoordAtNodePosition(new CCPoint(worldX, worldY));

            var info = layer.TileGIDAndFlags(tileAtXy.Column, tileAtXy.Row);

            if (info != null)
            {
                var properties = _testTileMap.TilePropertiesForGID(info.Gid);

                if (properties != null && properties.ContainsKey("IsTreasure") && properties["IsTreasure"] == "true")
                {
                    layer.RemoveTile(tileAtXy);

                    // todo: Create a treasure chest entitiy
                }
            }
        }
    }
}