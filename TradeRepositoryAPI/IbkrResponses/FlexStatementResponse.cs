using System.Xml.Serialization;

namespace TradeRepositoryAPI.IbkrResponses
{
    [XmlRoot("FlexStatementResponse")]
    public class FlexStatementResponse
    {
        [XmlAttribute("timestamp")]
        public string Timestamp { get; set; }

        public string Status { get; set; }

        public string ReferenceCode { get; set; }

        public string Url { get; set; }
    }
}
