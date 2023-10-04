public class Command
{
    public Command() { }
    public void Execute(Unit unit) { }
}

public class MoveCommand : Command
{
    public void Execute(Unit unit, Tile tile)
    {
        unit.Move(tile);
    }
}