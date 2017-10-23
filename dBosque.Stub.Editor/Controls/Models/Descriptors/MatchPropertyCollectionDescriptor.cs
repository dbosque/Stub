using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace dBosque.Stub.Editor.Controls.Models.Descriptors
{   
    /// <summary>
    /// Collection of matches (readonly, so nothing can be added, only changed)
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]  
    public class MatchPropertyCollection : ReadOnlyCollectionBase, ICustomTypeDescriptor
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="data"></param>
        public MatchPropertyCollection(IEnumerable<MatchProperty> data = null) 
            : base()
        {
            if (data != null)
                AddRange(data);
        }
        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public MatchProperty this[string key]
        {
            get { return InnerList.Cast<MatchProperty>().FirstOrDefault(p => p.Match == key); }
        }


        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MatchProperty this[int index]
        {
            get { return (MatchProperty)InnerList[index]; }
        }

        /// <summary>
        /// Add new items
        /// </summary>
        /// <param name="props"></param>
        public void AddRange(IEnumerable<MatchProperty> props)
        {
            InnerList.AddRange(props?.ToArray());
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        /// <summary>
        /// Custom GetProperties
        /// </summary>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties()
        {

            // Create a new collection object PropertyDescriptorCollection
            var pds = new PropertyDescriptorCollection(null);
            // Iterate the list of employees
            for (int i = 0; i < this.InnerList.Count; i++)
                pds.Add(new MatchPropertyCollectionPropertyDescriptor(this, i));

            return pds;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
    }
}
