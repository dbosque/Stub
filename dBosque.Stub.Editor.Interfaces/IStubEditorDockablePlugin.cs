using System;

namespace dBosque.Stub.Editor.Interfaces
{
	public interface IStubEditorDockablePlugin : IStubEditorlugin
	{
		bool Dockable
		{
			get;
		}

		bool Start(MarshalByRefObject window, int dockstate);
	}
}
