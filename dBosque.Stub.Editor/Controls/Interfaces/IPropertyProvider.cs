using dBosque.Stub.Editor.Controls.Models;

namespace dBosque.Stub.Editor.Controls.Interfaces
{
    /// <summary>
    /// Provider interface to support propertygrid loading and saving
    /// </summary>
    public interface IPropertyProvider
    {
        /// <summary>
        /// Save a property to the database
        /// </summary>
        /// <param name="property">The item to save</param>
        void Save(IPropertyBase property);

        /// <summary>
        /// Save only the message from the property
        /// </summary>
        /// <param name="property">The item to get the message from</param>
        void SaveMessage(IPropertyBase property);

        /// <summary>
        /// Load the property from the database.
        /// </summary>
        /// <returns></returns>
        IPropertyBase Load();
    }
}
