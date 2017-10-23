using System.Collections.Generic;

namespace dBosque.Stub.Interfaces
{
    /// <summary>
    /// Een combinatie item
    /// </summary>
    public class MatchCombination
    {
        public MatchCombination(string description)
        {
            Description = description;
            Items = new List<MatchItem>();
        }

        /// <summary>
        /// A list of items that matched
        /// </summary>
        public List<MatchItem> Items { get; set; }

        /// <summary>
        /// The description of the match
        /// </summary>
        public string Description { get; private set; }
    }
}
