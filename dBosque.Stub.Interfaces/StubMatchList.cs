using System.Collections.Generic;

namespace dBosque.Stub.Interfaces
{
    /// <summary>
    /// A list of found matches
    /// </summary>
    public class StubMatchList : List<MatchCombination>
    {
        /// <summary>
        /// Are there any matches?
        /// </summary>
        public bool HasMatch { get { return Count == 1; } }

        /// <summary>
        /// A possible error
        /// </summary>
        public string Error { get; set; }
    }
}
