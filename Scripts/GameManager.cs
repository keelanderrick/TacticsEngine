using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class GameManager : Node3D
{
	Node3D previousHighlight;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

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
		if(result.ContainsKey("collider")) {
			Node3D target = (Node3D)result["collider"];
			target = target.FindChild("Highlight") as Node3D;
			if(previousHighlight != null) previousHighlight.Hide();
			if(target != null) {
				previousHighlight = target;
				target.Show();
			}
		} else if (previousHighlight != null) {
			previousHighlight.Hide();
			previousHighlight = null;
		}
	}
}
