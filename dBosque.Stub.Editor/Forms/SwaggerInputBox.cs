namespace dBosque.Stub.Editor
{
    public class SwaggerInputBox : FileInputMessageBox
    {
        public SwaggerInputBox(): base ()
        {
            Label = "Enter a swagger url location to process";
            Caption = "Swagger";
        }
    }
}