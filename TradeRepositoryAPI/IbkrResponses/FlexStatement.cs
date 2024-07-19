using System.Xml.Serialization;

namespace TradeRepositoryAPI.IbkrResponses
{
    public class FlexStatement
    {
        [XmlAttribute("accountId")]
        public string AccountId { get; set; }

        [XmlAttribute("fromDate")]
        public string FromDate { get; set; }

        [XmlAttribute("toDate")]
        public string ToDate { get; set; }

        [XmlAttribute("period")]
        public string Period { get; set; }

        [XmlAttribute("whenGenerated")]
        public string WhenGenerated { get; set; }

        [XmlElement("Trades")]
        public List<Trade> Trades { get; set; }

        [XmlElement("OptionEAE")]
        public List<OptionEAE> OptionEAE { get; set; }
    }
}
