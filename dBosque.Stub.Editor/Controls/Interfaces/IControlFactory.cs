using WeifenLuo.WinFormsUI.Docking;

namespace dBosque.Stub.Editor.Controls.Interfaces
{
    public interface IControlFactory
    {
        /// <summary>
        /// Create a specific control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Create<T>();

        /// <summary>
        /// Create a specific control from a persisString
        /// </summary>
        /// <param name="persistString"></param>
        /// <returns></returns>
        IDockContent CreateFrom(string persistString);
    }
}
