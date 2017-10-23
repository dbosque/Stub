using System;

namespace dBosque.Stub.Editor.Interfaces
{
    public interface IStubEditorlugin
    {
        event EventHandler OnExit;

        string Name
        {
            get;
        }

        bool Start();
    }
}
