using Godot;
using System;
using System.Collections.Generic;

// Class represents a single tile on the game board
public partial class Tile : Node3D
{
	// Tile's x and y position on the grid
	public int x;
	public int y;

	// A list of all neighboring tiles
	public List<Tile> neighbors;

	// This tile's 'highlight', which can be enabled and altered when selected
	public Node3D highlight;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Setup the list and highlight
		neighbors = new List<Tile>();
		highlight = (Node3D)FindChild("Highlight");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
