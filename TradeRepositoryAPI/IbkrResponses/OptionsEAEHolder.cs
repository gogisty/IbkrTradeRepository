using System.Xml.Serialization;

namespace TradeRepositoryAPI.IbkrResponses
{
    public class OptionsEAEHolder
    {
        [XmlElement("OptionEAE")]
        public List<OptionEAE> OptionsEaeList { get; set; }
    }
}
