using Godot;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.XPath;

public partial class GameManager : Node3D
{
	// The list of previously highlighted tiles
	List<Node3D> previousHighlight;
	// The list of units on the board
	List<Unit> units;
	// Unit who's turn it currently is
	int currentUnit;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		previousHighlight = new List<Node3D>();
		units = new List<Unit>();
		PackedScene packedScene = ResourceLoader.Load("res://Scenes/Objects/SampleUnit.tscn") as PackedScene;
		for (int i = 0; i <= 3; i++)
		{
			units.Add(packedScene.Instantiate<Unit>());
			units[i].Position = new Vector3(i, 0.0f, i);
			this.AddChild(units[i]);
		}
		currentUnit = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Input events
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("LeftClick"))
		{

			Tile selectedTile = GetHoveredTile();
			if (selectedTile != null)
			{
				units[currentUnit].Move(selectedTile);
				currentUnit++;
				if (currentUnit >= units.Count) currentUnit = 0;
			}

		}
	}
	public override void _PhysicsProcess(double delta)
	{
		PhysicsDirectSpaceState3D space = GetWorld3D().DirectSpaceState;
		Camera3D camera = GetViewport().GetCamera3D();
		Vector2 mousePosition = GetViewport().GetMousePosition();

		Vector3 origin = camera.ProjectRayOrigin(mousePosition);
		Vector3 end = origin + camera.ProjectRayNormal(mousePosition) * 100;
		PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(origin, end);
		query.CollideWithAreas = true;

		var result = space.IntersectRay(query);
		foreach (Node3D highlight in previousHighlight)
		{
			highlight.Hide();
		}
		previousHighlight.Clear();
		if (result.ContainsKey("collider"))
		{
			Tile target = (Node3D)result["collider"] as Tile;
			if (target != null)
			{
				previousHighlight.Add(target.highlight);
				target.highlight.Show();
				foreach (Tile tile in target.neighbors)
				{
					previousHighlight.Add(tile.highlight);
					tile.highlight.Show();
				}
			}
		}
	}

	public Tile GetHoveredTile()
	{
		PhysicsDirectSpaceState3D space = GetWorld3D().DirectSpaceState;
		Camera3D camera = GetViewport().GetCamera3D();
		Vector2 mousePosition = GetViewport().GetMousePosition();

		Vector3 origin = camera.ProjectRayOrigin(mousePosition);
		Vector3 end = origin + camera.ProjectRayNormal(mousePosition) * 100;
		PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(origin, end);
		query.CollideWithAreas = true;

		var result = space.IntersectRay(query);
		if (result.ContainsKey("collider"))
		{
			return (Node3D)result["collider"] as Tile;
		}
		return null;
	}
}
