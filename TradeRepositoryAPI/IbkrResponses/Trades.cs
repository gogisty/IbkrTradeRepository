using System.Xml.Serialization;

namespace TradeRepositoryAPI.IbkrResponses
{
    public class Trades
    {
        [XmlElement("Trade")]
        public List<Trade> TradeList { get; set; }
    }
}
