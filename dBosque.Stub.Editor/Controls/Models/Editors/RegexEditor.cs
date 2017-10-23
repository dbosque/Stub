using dBosque.Stub.Editor.Models;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace dBosque.Stub.Editor.Controls.Models.Editors
{
    internal class RegexEditor : UITypeEditor
    {


        /// <summary>Edits the specified object value using the editor style provided by GetEditorStyle. A service provider is provided so that any required editing services can be obtained.</summary>
		/// <returns>The new value of the object. If the value of the object hasn't changed, this should return the same object it was passed.</returns>
		/// <param name="context">A type descriptor context that can be used to provide additional context information. </param>
		/// <param name="provider">A service provider object through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (windowsFormsEditorService != null)
                {
                    using (RegexEditorUI ui = new RegexEditorUI())
                    {
                        var item = context.Instance as MessageTypeItem;

                        ui.Start(windowsFormsEditorService, value, item.NameSpace);                       
                        windowsFormsEditorService.DropDownControl(ui);
                        value = ui.Value;
                        ui.End();
                    }
                }
            }
            return value;
        }

        /// <summary>Retrieves the editing style of the <see cref="Overload:System.ComponentModel.Design.DateTimeEditor.EditValue" /> method. If the method is not supported, this will return None.</summary>
        /// <returns>An enum value indicating the provided editing style.</returns>
        /// <param name="context">A type descriptor context that can be used to provide additional context information. </param>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
