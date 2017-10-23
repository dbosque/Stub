namespace dBosque.Stub.Editor
{
    public class Version
    {
        public string Id { get; set; }

        /// <summary>
        /// Is the current version the full version or not.
        /// </summary>
        /// <returns></returns>
        public bool IsFull => string.Compare(Id, "Full", true) == 0; 
    }
}
