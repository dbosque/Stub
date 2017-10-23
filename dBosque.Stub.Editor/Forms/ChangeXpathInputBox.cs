using System;
using System.Windows.Forms;
namespace dBosque.Stub.Editor
{
    public class ChangeXpathInputBox : InputMessageBox
    {
        public ChangeXpathInputBox(string path): base ()
        {
            Label = "Change xpath to";
            Caption = "Description";
            Inputstring = path;
        }
    }
}