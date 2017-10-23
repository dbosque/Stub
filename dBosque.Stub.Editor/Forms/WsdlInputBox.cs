using System;
using System.Windows.Forms;
namespace dBosque.Stub.Editor
{
    public class WsdlInputBox : FileInputMessageBox
    {
        public WsdlInputBox(): base ()
        {
            Label = "Enter a wsdl location to process";
            Caption = "WSDL";
        }
    }
}