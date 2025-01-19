using System.Xml.Serialization;

namespace TradeRepositoryAPI.IbkrResponses
{
    public class FlexStatements
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("FlexStatement")]
        public List<FlexStatement> FlexStatementList { get; set; }
    }
}
