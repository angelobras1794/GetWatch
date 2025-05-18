
using System;

namespace GetWatch.Interfaces.Compra
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();

    }
}