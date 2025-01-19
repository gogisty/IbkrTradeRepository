using System.Xml.Serialization;

namespace TradeRepositoryAPI.IbkrResponses
{
    public class OptionEAE
    {
        [XmlAttribute("accountId")]
        public string AccountId { get; set; }

        [XmlAttribute("acctAlias")]
        public string AcctAlias { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("currency")]
        public string Currency { get; set; }

        private string _fxRateToBaseString;
        [XmlAttribute("fxRateToBase")]
        public string FxRateToBaseString
        {
            get => _fxRateToBaseString;
            set
            {
                _fxRateToBaseString = value;
                FxRateToBase = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float FxRateToBase { get; set; }

        [XmlAttribute("assetCategory")]
        public string AssetCategory { get; set; }

        [XmlAttribute("subCategory")]
        public string SubCategory { get; set; }

        [XmlAttribute("symbol")]
        public string Symbol { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlAttribute("conid")]
        public string Conid { get; set; }

        [XmlAttribute("securityID")]
        public string SecurityID { get; set; }

        [XmlAttribute("securityIDType")]
        public string SecurityIDType { get; set; }

        [XmlAttribute("cusip")]
        public string Cusip { get; set; }

        [XmlAttribute("isin")]
        public string Isin { get; set; }

        [XmlAttribute("figi")]
        public string Figi { get; set; }

        [XmlAttribute("listingExchange")]
        public string ListingExchange { get; set; }

        [XmlAttribute("underlyingConid")]
        public string UnderlyingConid { get; set; }

        [XmlAttribute("underlyingSymbol")]
        public string UnderlyingSymbol { get; set; }

        [XmlAttribute("underlyingSecurityID")]
        public string UnderlyingSecurityID { get; set; }

        [XmlAttribute("underlyingListingExchange")]
        public string UnderlyingListingExchange { get; set; }

        [XmlAttribute("issuer")]
        public string Issuer { get; set; }

        [XmlAttribute("issuerCountryCode")]
        public string IssuerCountryCode { get; set; }

        [XmlAttribute("tradeID")]
        public string TradeID { get; set; }

        private string _multiplier;
        [XmlAttribute("multiplier")]
        public string MultiplierString
        {
            get => _multiplier;
            set
            {
                _multiplier = value;
                Multiplier = NumericParser.Parse<int>(value);
            }
        }

        [XmlIgnore()]
        public int Multiplier { get; set; }

        [XmlAttribute("relatedTradeID")]
        public string RelatedTradeID { get; set; }

        private string _strike;
        [XmlAttribute("strike")]
        public string StrikeString
        {
            get => _strike;
            set
            {
                _strike = value;
                Strike = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float Strike { get; set; }

        [XmlAttribute("reportDate")]
        public string ReportDate { get; set; }

        [XmlAttribute("expiry")]
        public string Expiry { get; set; }

        [XmlAttribute("dateTime")]
        public string DateTime { get; set; }

        [XmlAttribute("putCall")]
        public string PutCall { get; set; }

        [XmlAttribute("tradeDate")]
        public string TradeDate { get; set; }

        private string _principalAdjustFactor;
        [XmlAttribute("principalAdjustFactor")]
        public string PrincipalAdjustFactorString
        {
            get => _principalAdjustFactor;
            set
            {
                _principalAdjustFactor = value;
                PrincipalAdjustFactor = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float PrincipalAdjustFactor { get; set; }

        [XmlAttribute("settleDateTarget")]
        public string SettleDateTarget { get; set; }

        [XmlAttribute("transactionType")]
        public string TransactionType { get; set; }

        [XmlAttribute("exchange")]
        public string Exchange { get; set; }

        private string _quantity;
        [XmlAttribute("quantity")]
        public string QuantityString
        {
            get => _quantity;
            set
            {
                _quantity = value;
                Quantity = NumericParser.Parse<int>(value);
            }
        }

        [XmlIgnore()]
        public int Quantity { get; set; }

        private string _tradePriceString;
        [XmlAttribute("tradePrice")]
        public string TradePriceString
        {
            get => _tradePriceString;
            set
            {
                _tradePriceString = value;
                TradePrice = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float TradePrice { get; set; }

        private string _tradeMoneyString;
        [XmlAttribute("tradeMoney")]
        public string TradeMoneyString
        {
            get => _tradeMoneyString;
            set
            {
                _tradeMoneyString = value;
                TradeMoney = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float TradeMoney { get; set; }

        private string _proceedsString;
        [XmlAttribute("proceeds")]
        public string ProceedsString
        {
            get => _proceedsString;
            set
            {
                _proceedsString = value;
                Proceeds = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float Proceeds { get; set; }

        private string _taxes;
        [XmlAttribute("taxes")]
        public string TaxesString
        {
            get => _taxes;
            set
            {
                _taxes = value;
                Taxes = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float Taxes { get; set; }

        private string _ibCommission;
        [XmlAttribute("ibCommission")]
        public string IbCommissionString
        {
            get => _ibCommission;
            set
            {
                _ibCommission = value;
                IbCommission = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float IbCommission { get; set; }

        [XmlAttribute("ibCommissionCurrency")]
        public string IbCommissionCurrency { get; set; }

        private string _netCash;
        [XmlAttribute("netCash")]
        public string NetCashString
        {
            get => _netCash;
            set
            {
                _netCash = value;
                NetCash = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float NetCash { get; set; }

        private string _closePrice;
        [XmlAttribute("closePrice")]
        public string ClosePriceString
        {
            get => _closePrice;
            set
            {
                _closePrice = value;
                ClosePrice = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float ClosePrice { get; set; }

        [XmlAttribute("openCloseIndicator")]
        public string OpenCloseIndicator { get; set; }

        [XmlAttribute("notes")]
        public string Notes { get; set; }

        private string _cost;
        [XmlAttribute("cost")]
        public string CostString
        {
            get => _cost;
            set
            {
                _cost = value;
                Cost = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float Cost { get; set; }

        private string _fifoPnlRealized;
        [XmlAttribute("fifoPnlRealized")]
        public string FifoPnlRealizedString
        {
            get => _fifoPnlRealized;
            set
            {
                _fifoPnlRealized = value;
                FifoPnlRealized = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float FifoPnlRealized { get; set; }

        private string _mtmPnl;
        [XmlAttribute("mtmPnl")]
        public string MtmPnlString
        {
            get => _mtmPnl;
            set
            {
                _mtmPnl = value;
                MtmPnl = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float MtmPnl { get; set; }

        private string _origTradePrice;
        [XmlAttribute("origTradePrice")]
        public string OrigTradePriceString
        {
            get => _origTradePrice;
            set
            {
                _origTradePrice = value;
                OrigTradePrice = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float OrigTradePrice { get; set; }

        [XmlAttribute("origTradeDate")]
        public string OrigTradeDate { get; set; }

        [XmlAttribute("origTradeID")]
        public string OrigTradeID { get; set; }

        [XmlAttribute("origOrderID")]
        public string OrigOrderID { get; set; }

        [XmlAttribute("origTransactionID")]
        public string OrigTransactionID { get; set; }

        [XmlAttribute("buySell")]
        public string BuySell { get; set; }

        [XmlAttribute("clearingFirmID")]
        public string ClearingFirmID { get; set; }

        [XmlAttribute("ibOrderID")]
        public string IbOrderID { get; set; }

        [XmlAttribute("transactionID")]
        public string TransactionID { get; set; }

        [XmlAttribute("ibExecID")]
        public string IbExecID { get; set; }

        [XmlAttribute("relatedTransactionID")]
        public string RelatedTransactionID { get; set; }

        [XmlAttribute("rtn")]
        public string Rtn { get; set; }

        [XmlAttribute("brokerageOrderID")]
        public string BrokerageOrderID { get; set; }

        [XmlAttribute("orderReference")]
        public string OrderReference { get; set; }

        [XmlAttribute("volatilityOrderLink")]
        public string VolatilityOrderLink { get; set; }

        [XmlAttribute("exchOrderId")]
        public string ExchOrderId { get; set; }

        [XmlAttribute("extExecID")]
        public string ExtExecID { get; set; }

        [XmlAttribute("orderTime")]
        public string OrderTime { get; set; }

        [XmlAttribute("openDateTime")]
        public string OpenDateTime { get; set; }

        [XmlAttribute("holdingPeriodDateTime")]
        public string HoldingPeriodDateTime { get; set; }

        [XmlAttribute("whenRealized")]
        public string WhenRealized { get; set; }

        [XmlAttribute("whenReopened")]
        public string WhenReopened { get; set; }

        [XmlAttribute("levelOfDetail")]
        public string LevelOfDetail { get; set; }

        private string _changeInPrice;
        [XmlAttribute("changeInPrice")]
        public string ChangeInPriceString
        {
            get => _changeInPrice;
            set
            {
                _changeInPrice = value;
                ChangeInPrice = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float ChangeInPrice { get; set; }

        private string _changeInQuantity;
        [XmlAttribute("changeInQuantity")]
        public string ChangeInQuantityString
        {
            get => _changeInQuantity;
            set
            {
                _changeInQuantity = value;
                ChangeInQuantity = NumericParser.Parse<int>(value);
            }
        }

        [XmlIgnore()]
        public int ChangeInQuantity { get; set; }

        [XmlAttribute("orderType")]
        public string OrderType { get; set; }

        [XmlAttribute("traderID")]
        public string TraderID { get; set; }

        [XmlAttribute("isAPIOrder")]
        public string IsAPIOrder { get; set; }

        private string _accruedInt;
        [XmlAttribute("accruedInt")]
        public string AccruedIntString
        {
            get => _accruedInt;
            set
            {
                _accruedInt = value;
                AccruedInt = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float AccruedInt { get; set; }

        private string _initialInvestment;
        [XmlAttribute("initialInvestment")]
        public string InitialInvestmentString
        {
            get => _initialInvestment;
            set
            {
                _initialInvestment = value;
                InitialInvestment = NumericParser.Parse<float>(value);
            }
        }

        [XmlIgnore()]
        public float InitialInvestment { get; set; }
    }
}
