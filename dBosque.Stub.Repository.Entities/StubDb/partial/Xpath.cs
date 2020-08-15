using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;

namespace dBosque.Stub.Repository.StubDb.Entities
{
    public partial class Xpath
    {
        public string Name { get { return $"{TypeToName()} : {CleanExpression}"; } }

        public string CleanExpression => Regex.Replace(Expression, @"\/\*\[local-name\(\)='(.*?)'\]", "/$1");

        public string TypeToName()
        {
            if (Type == 0)
                return "Content";
            if (Type == 1)
                return "Uri";
            if (Type == 2)
                return "Content Regex";
            return "??";
        }

        public int? TypeFromName(string name)
        {
            if (string.Compare(name, "Content", true) == 0)
                return 0;

            if (string.Compare(name, "Uri", true) == 0)
                return 1;
            if (string.Compare(name, "Content Regex", true) == 0)
                return 2;
            return null; 
        }

      

        public static IEnumerable<Xpath> GetAllValidFor(IEnumerable<Xpath> collection, XmlDocument document, bool stripSoapEnvelope)
        {
            // Als er een document geladen is, filteren, anders gewoon zo laten
            if (document != null)
            {
                System.Xml.XPath.XPathNavigator navigator = document.CreateNavigator();
                foreach (var p in collection.Where(a => a.Type == 0))
                {
                    System.Xml.XPath.XPathNavigator n = null;
                    try
                    {
                        var exp = p.Expression;
                        if (!stripSoapEnvelope && !p.Expression.StartsWith("/*[local-name()='Envelope']"))
                        {
                            exp = @"/*[local-name()='Envelope']/*[local-name()='Body']" + exp;
                        }

                        n = navigator.SelectSingleNode(exp);
                    }
                    catch
                    {
                        // Geen catch,
                        // Exception betekend dat de Expression niet gevonden kon worden.
                        // Dus niet opnemen in de lijst.
                    }
                    // Indien de Expression gevonden kan worden, toevoegen aan de lijst.
                    if (n != null)
                        yield return p;
                }
            }
        }
    }
}
