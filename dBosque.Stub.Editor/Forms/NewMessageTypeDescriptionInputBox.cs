using System;
using System.Windows.Forms;
namespace dBosque.Stub.Editor
{
    public class NewMessageTypeDescriptionInputBox : InputMessageBox
    {
        public NewMessageTypeDescriptionInputBox(string name, string root): base ()
        {
            Label = "Enter a description for the new messagetype " + Environment.NewLine + name + " and rootnode " + root;
            Caption = "Description";
        }
    }
}