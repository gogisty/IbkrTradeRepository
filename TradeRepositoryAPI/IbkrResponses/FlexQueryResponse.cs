using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace TradeRepositoryAPI.IbkrResponses
{
    [XmlRoot("FlexQueryResponse")]
    public class FlexQueryResponse
    {
        [XmlAttribute("queryName")]
        public string QueryName { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("FlexStatements")]
        public List<FlexStatement> FlexStatements { get; set; }
    }
}
