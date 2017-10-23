using System;
using System.ComponentModel;

namespace dBosque.Stub.Editor.Controls.Models.Descriptors
{
    /// <summary>
    /// Property descriptor for a MatchPropertyCollection to be able to edit it ocntents in a propertygrid
    /// </summary>
    public class MatchPropertyCollectionPropertyDescriptor : PropertyDescriptor
    {
        private readonly MatchPropertyCollection collection = null;
        private readonly int index = -1;

        public MatchPropertyCollectionPropertyDescriptor(MatchPropertyCollection coll,
                           int idx) : base("#" + idx.ToString(), null)
        {
            collection = coll;
            index = idx;
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return new AttributeCollection(null);
            }
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get
            {
                return this.collection.GetType();
            }
        }

        public override string DisplayName
        {
            get
            {                
                MatchProperty emp = collection[index];
                return emp.DisplayName();
            }
        }

        public override string Description
        {
            get
            {
                MatchProperty emp = collection[index];
                return emp.Value;
            }
        }

        public override object GetValue(object component)
        {
            return collection[index];
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override string Name
        {
            get { return "#" + index.ToString(); }
        }

        public override Type PropertyType
        {
            get { return collection[index].GetType(); }
        }

        public override void ResetValue(object component) { }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
        }
    }
}
