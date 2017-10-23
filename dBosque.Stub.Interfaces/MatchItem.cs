namespace dBosque.Stub.Interfaces
{
    /// <summary>
    /// Een xpath item
    /// </summary>
    public class MatchItem
    {
        public MatchItem(string exp, string val)
        {
            Expression = exp;
            Value = val;
        }
        public string Expression { get; private set; }
        public string Value { get; private set; }
    }
}
