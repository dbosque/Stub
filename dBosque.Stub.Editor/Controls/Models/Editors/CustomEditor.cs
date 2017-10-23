using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace dBosque.Stub.Editor.Controls.Models.Editors
{   
    /// <summary>
    /// Custom editorclass to get rid of the ... of the default collectioneditor
    /// </summary>
    internal class CustomEditor : CollectionEditor
    {
        public CustomEditor(Type type) : base(type)
        { }
        /// <summary>
        /// Override the editstyle
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
        }
    }
}
