using System;
using GetWatch.Interfaces.Compra;

public class CommandManager: ICommandManager
{
    private readonly List<ICommand> Commands = new List<ICommand>();
    private int Position = 0;

    public bool HasUndo => Position > 0;
    public bool HasRedo => Position < Commands.Count;

    // Executa um novo comando
    public void Execute(ICommand command)
    {
        // Limpa qualquer comando que esteja à frente (não mais válido para redo)
        if (HasRedo)
        {
            Commands.RemoveRange(Position, Commands.Count - Position);
        }

        // Adiciona e executa
        Commands.Add(command);
        Position++;
    }

    // Desfaz o comando atual
    public void Undo()
    {
        if (HasUndo)
        {
            Position--;
            Commands[Position].Undo();
        }
    }

    // Refaz o comando seguinte
    public void Redo()
    {
        if (HasRedo)
        {
            Commands[Position].Redo();
            Position++;
        }
    }

}
