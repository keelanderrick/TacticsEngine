using Godot;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;

public partial class GameManager : Node3D
{
	List<Node3D> previousHighlight;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		previousHighlight = new List<Node3D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _PhysicsProcess(double delta)
	{
		PhysicsDirectSpaceState3D space = GetWorld3D().DirectSpaceState;
		Camera3D camera = GetViewport().GetCamera3D();
		Vector2 mousePosition = GetViewport().GetMousePosition();

		Vector3 origin = camera.ProjectRayOrigin(mousePosition);
		Vector3 end = origin + camera.ProjectRayNormal(mousePosition) * 100;
		PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(origin,end);
		query.CollideWithAreas = true;

		var result = space.IntersectRay(query);
		foreach(Node3D highlight in previousHighlight) {
			highlight.Hide();
		}
		previousHighlight.Clear();
		if(result.ContainsKey("collider")) {
			Tile target = (Node3D)result["collider"] as Tile;
			if(target != null) {
				previousHighlight.Add(target.highlight);
				target.highlight.Show();
				foreach(Tile tile in target.neighbors) {
					previousHighlight.Add(tile.highlight);
					tile.highlight.Show();
				}
			}
		}
	}
}
