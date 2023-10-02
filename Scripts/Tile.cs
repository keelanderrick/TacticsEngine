using Godot;
using System;
using System.Collections.Generic;

public partial class Tile : Node3D
{
	public int x;
	public int y;
	public List<Tile> neighbors;
	public Node3D highlight;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		neighbors = new List<Tile>();
		highlight = (Node3D)FindChild("Highlight");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
