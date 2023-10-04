using Godot;
using System;

public partial class Unit : Node3D
{
	public Tile currentPosition;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Move(Tile tile)
	{
		Position = new Vector3(tile.Position.X, 0.0f, tile.Position.Z);
	}
}
