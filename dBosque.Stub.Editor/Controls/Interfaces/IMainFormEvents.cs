using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using dBosque.Stub.Editor.Controls.Errorhandling;
using dBosque.Stub.Editor.Flow;
using System;

namespace dBosque.Stub.Editor.Controls.Interfaces
{
    /// <summary>
    /// Event to be consumed or send by the dockpanelform
    /// </summary>
    public interface IMainFormEvents
    {

        /// <summary>
        /// Handle the Error event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription("topic://Error", typeof(OnPublisher))]
        void OnError(ErrorEventArgs e);

        /// <summary>
        /// Handle the event when the repository is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription("topic://RepositoryChanged", typeof(OnPublisher))]
        void OnRepositoryChanged(RepositoryFlowEventArgs e);

        /// <summary>
        /// Handle the event when the application is shown for the first time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription("topic://ApplicationShow", typeof(OnPublisher))]
        void OnShow(EventArgs e);

        /// <summary>
        /// Send an event when the application is shown for the first time
        /// </summary>
        [EventPublication("topic://ApplicationShow")]
        event EventHandler Shown;


        /// <summary>
        /// Send an event when the application closes
        /// </summary>
        [EventPublication("topic://ApplicationClosed")]
        event EventHandler ApplicationClosed;

        /// <summary>
        /// Send an event when the application is loaded
        /// </summary>
        [EventPublication("topic://ApplicationLoaded")]
        event EventHandler ApplicationLoaded;

        /// <summary>
        /// Send an event to request all documents to be closed
        /// </summary>
        [EventPublication("topic://Documents.CloseAll")]
        event EventHandler CloseAllDocuments;

        /// <summary>
        /// Send an event when the repository changes
        /// </summary>
        [EventPublication("topic://RepositoryChanged")]
        event EventHandler<RepositoryFlowEventArgs> RepositoryChanged;
    }
}
