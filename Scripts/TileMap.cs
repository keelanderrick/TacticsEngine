using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class TileMap : Node3D
{
	List<List<Tile>> tiles;
	int width;
	int len;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		tiles = new List<List<Tile>>();
		width = 10;
		len = 10;
		PackedScene packedScene = ResourceLoader.Load("res://Scenes/Objects/SampleTile.tscn") as PackedScene;
		for(int i = 0; i < width; i++) {
			tiles.Add(new List<Tile>());
			for(int j = 0; j < len; j++) {
				Tile tile = packedScene.Instantiate<Tile>();
				tile.x = i;
				tile.y = j;
				tile.Position = new Vector3(i, 0.0f, j);
				this.AddChild(tile);
				tiles[i].Add(tile);
			}
		}
		for(int i = 0; i < width; i++) {
			for(int j = 0; j < len; j++) {
				if(i+1 < width) tiles[i][j].neighbors.Add(tiles[i+1][j]);
				if(i-1 >= 0) tiles[i][j].neighbors.Add(tiles[i-1][j]);
				if(j+1 < len) tiles[i][j].neighbors.Add(tiles[i][j+1]);
				if(j-1 >= 0) tiles[i][j].neighbors.Add(tiles[i][j-1]);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
