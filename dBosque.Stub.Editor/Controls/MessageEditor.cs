using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using dBosque.Stub.Editor.Controls.Behaviours;
using dBosque.Stub.Editor.Controls.Interfaces;
using dBosque.Stub.Editor.Controls.Models;
using dBosque.Stub.Editor.Flow;
using dBosque.Stub.Services.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls
{
    public partial class MessageEditor : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        [DesignOnly(true)]
        public MessageEditor()
            : this(null)
        { }

        public MessageEditor(IEventBroker broker)
        {
            broker.Register(this);
            InitializeComponent();
            _dec = new DragDropPasteBehaviour(textBox1, (s, e) => { textBox1.Text = e.Data?.Pretty(); UpdateButtonStates(); });
            saveButton.Enabled = resetButton.Enabled = false;
        }

        private IPropertyProvider _controller;
        private IPropertyBase _data;
        private DragDropPasteBehaviour _dec;

        [EventSubscription("topic://PropertyFlowRequested", typeof(OnPublisher))]
        public void OnControllerFlow(PropertyFlowEventArgs args)
        {
            _controller = args.Provider;
            _data = _controller?.Load();
            textBox1.Text = _data?.Message.Pretty() ?? "";
            UpdateButtonStates();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            _data.Message = textBox1.Text;
            _controller.SaveMessage(_data);
            UpdateButtonStates();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = _data.Message;
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            saveButton.Enabled = resetButton.Enabled = (_data != null && string.Compare(_data.Message, textBox1.Text) != 0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox1.SelectAll();
            }
        }

        private void prettyPrintButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Pretty();
        }
    }
}
