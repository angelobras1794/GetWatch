using System;
using GetWatch.Interfaces.Compra;

public class CommandManager: ICommandManager
{
    private readonly List<ICommand> Commands = new List<ICommand>();
    private int Position = -1;

    public bool HasUndo => Position >= 0;
    public bool HasRedo => Position < Commands.Count -1;

    public void Execute(ICommand command)
    {

        if (HasRedo)
        {
            Commands.RemoveRange(Position + 1, Commands.Count - Position - 1);
        }


        Commands.Add(command);
        command.Execute();
        Position++;
        Console.WriteLine($"Position: {Position}"); 
    }


    public void Undo()
    {
        Console.WriteLine($"Position: {Position}");
        if (HasUndo)
        {
            Commands[Position].Undo();
            Position--;
        }
        Console.WriteLine($"Position: {Position}");
    }

    public void Redo()
    {
        if (HasRedo)
        {
            Position++;
            Commands[Position].Redo();
            
        }
    }

}
