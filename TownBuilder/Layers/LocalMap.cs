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
using TownBuilder.Extensions;

namespace TownBuilder.Layers
{
    public class LocalMap : CCLayer
    {
        private CCTileMap _testTileMap;
        private List<NonPlayerCharacter> _npcs;

        private Player _player;

        private const string TileEntryPropertyKey = "CanPlayerEnter";

        public LocalMap(CCTileMap map)
        {
            _testTileMap = map;
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            _testTileMap.Antialiased = false;
            AddChild(_testTileMap);

            _player = new Player(this);
            AddChild(_player);

            _npcs = new List<NonPlayerCharacter> { new NonPlayerCharacter(this) };
            _npcs.ForEach(x => AddChild(x));

            HandleCustomTileProperties(_testTileMap);

            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;
        }

        private void MoveMapByDiff(CCPoint diff)
        {
            _testTileMap.TileLayersContainer.Position += diff;

            _npcs.ForEach(x => x.Position += diff);
        }

        public void ReceiveInput(CCPoint location)
        {
            var tile = location.ToTileCoordinates();

            var playerNextTile = FindNextTileForPlayer(tile);

            if (EntityOccupiesTile(playerNextTile))
            {
                NpcAtTile(playerNextTile).Interact();
            }

            MoveEntities(tile);
        }

        private CCPoint FindNextTileForPlayer(CCPoint destination)
        {
            var tileIsVertical = _player.CurrentTile.DestinationIsCloserVertically(destination);

            var diff = _player.CurrentTile - destination;

            if (tileIsVertical)
            {
                return new CCPoint(diff.Y > 0 ? _player.CurrentTile.Y + 1 : _player.CurrentTile.Y - 1, _player.CurrentTile.X);
            }
            else
            {
                return new CCPoint(_player.CurrentTile.Y, diff.X > 0 ? _player.CurrentTile.X + 1 : _player.CurrentTile.X - 1);
            }
        }

        private NonPlayerCharacter NpcAtTile(CCPoint tile)
        {
            return _npcs.First(x => x.Position.ToTileCoordinates() == tile);
        }

        private void MoveEntities(CCPoint tile)
        {
            var lastPlayerPosition = _player.Position;

            _player.SetDestination(tile);

            _npcs.ForEach(x => x.MoveEntityRandomly());

            var diff = lastPlayerPosition - _player.Position;

            // todo: add logic to determine if map should move
            var shouldMapMove = true;
            if (shouldMapMove)
            {
                MoveMapByDiff(diff);

                _player.Position += diff;
            }
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

        public bool ShouldEntityMoveToLocation(CCPoint tileLocation)
        {
            if (EntityOccupiesTile(tileLocation.ToTileCoordinates()))
            {
                return false;
            }

            var mapOffset = _testTileMap.TileLayersContainer.Position;
            var locationOnMap = tileLocation -= mapOffset;

            return CanDestinationTerrainBeEntered(locationOnMap);
        }

        private bool EntityOccupiesTile(CCPoint tile)
        {
            var playerIsInLocation = _player?.CurrentTile == tile;
            var npcIsInLocation = _npcs.Any(x => x.CurrentTile == tile);

            return playerIsInLocation || npcIsInLocation;
        }

        private bool CanDestinationTerrainBeEntered(CCPoint pixelLocationOnMap)
        {
            foreach (CCTileMapLayer layer in _testTileMap.TileLayersContainer.Children)
            {
                var tileAtXy = layer.ClosestTileCoordAtNodePosition(pixelLocationOnMap);

                var info = layer.TileGIDAndFlags(tileAtXy.Column, tileAtXy.Row);

                if (info != null)
                {
                    var properties = _testTileMap.TilePropertiesForGID(info.Gid);

                    if (properties != null && properties.ContainsKey(TileEntryPropertyKey) && properties[TileEntryPropertyKey].ToLower() == false.ToString().ToLower())
                    {
                        return false;
                    }
                }
            }

            return true;
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