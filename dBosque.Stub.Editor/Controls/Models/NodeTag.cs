namespace dBosque.Stub.Editor.Controls.Models
{
    /// <summary>
    /// Een NodeTag bevat informatie over wat er moet gebeuren op het moment van clicken op een node.
    /// </summary>
    public class NodeTag
    {
        /// <summary>
        /// The type of node (XPath, value, Response etc)
        /// </summary>
        public NodeType nodeType { get; set; }

        /// <summary>
        /// A link to the actual database component
        /// </summary>
        public long Key { get; set; }

        /// <summary>
        /// The hashcode provider
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return $"NodeTag_{nodeType.ToString()}_{Key}".GetHashCode();  
        }
        /// <summary>
        /// Equals event
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is NodeTag))
                return false;
            var tag = obj as NodeTag;
            return tag.Key == Key && tag.nodeType == nodeType;
        }      
    }
}
