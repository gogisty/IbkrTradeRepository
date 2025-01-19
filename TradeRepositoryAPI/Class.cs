using System.Xml.Serialization;
using TradeRepositoryAPI.IbkrResponses;

namespace TradeRepositoryAPI
{
    public class OptionsEAE
    {
        [XmlElement("OptionEAE")]
        public List<OptionEAE> OptionsEaeList { get; set; }
    }
}
