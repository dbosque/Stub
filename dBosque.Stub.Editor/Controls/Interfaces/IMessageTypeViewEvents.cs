using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using dBosque.Stub.Editor.Controls.Errorhandling;
using dBosque.Stub.Editor.Flow;
using System;

namespace dBosque.Stub.Editor.Controls.Interfaces
{
    public interface IMessageTypeViewEvents
    {

        [EventPublication("topic://Error")]
        event EventHandler<ErrorEventArgs> OnError;
        [EventPublication("topic://PropertyFlowRequested")]
        event EventHandler<PropertyFlowEventArgs> OnPropertyFlowRequested;
        [EventPublication("topic://MessageTypeActivated")]
        event EventHandler<MessageTypeFlowEventArgs> OnMessageTypeActivated;

        [EventSubscription("topic://RepositoryChanged", typeof(OnPublisher))]
        void OnRepositoryChanged(RepositoryFlowEventArgs e);

        [EventSubscription("topic://CreateMessageType", typeof(OnPublisher))]
        void CreateMessageTypeFor(CreateMessageTypeFlowEventArgs args);
    }
}
