using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entity
{
    public class TClientBaseInfo
    {
        /// <summary>
        /// 客户编号(客户在系统中唯一标识)   交易用
        /// </summary>
        public string FClientNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FUpperNo { get; set; }
        /// <summary>
        /// 客户名称简称   交易用
        /// </summary>
        public string FShortName { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// 客户类型 个人 - ‘I’, 机构 - 'O'    交易用
        /// </summary>
        public string FType { get; set; }
        /// <summary>
        /// 客户结算状态 正常 - 'Y', 销户 - 'N'
        /// </summary>
        public string FState { get; set; }
        /// <summary>
        /// 开户日期   交易用
        /// </summary>
        public DateTime FOpenDate { get; set; }
        /// <summary>
        /// 销户日期
        /// </summary>
        public DateTime FCloseDate { get; set; }
        /// <summary>
        /// 身份信息(个人-身份证号,机构-营业执照号)
        /// </summary>
        public string FIdentity { get; set; }
        /// <summary>
        /// 护照
        /// </summary>
        public string FPassport { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        public string FTelPhone { get; set; }
        /// <summary>
        /// 移动电话  交易用
        /// </summary>
        public string FMobilePhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FFax { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string FPostNo { get; set; }
        /// <summary>
        /// 客户联系地址
        /// </summary>
        public string FAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FPostAddress { get; set; }
        /// <summary>
        /// FMarketFee
        /// </summary>
        public string FCompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FCompanyAddress { get; set; }
        /// <summary>
        /// 下单人
        /// </summary>
        public string FDealMan { get; set; }
        /// <summary>
        /// 下单人证件号
        /// </summary>
        public string FDealManID { get; set; }
        /// <summary>
        /// 资金调拨人
        /// </summary>
        public string FCashMan { get; set; }
        /// <summary>
        /// 资金调拨人身份
        /// </summary>
        public string FCashManID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string FRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IsSimulation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FPassword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FBank { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FAccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FSelAll { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? FSelDepositNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FSex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FExpiryDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FMailAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FImage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FEnabledDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FSwapMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FMailMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FBirthDay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FSendPhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FSalesName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Funding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FSalesPhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FCrossABitrage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FOpenInfaces { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FMultiLogin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FireTicket { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FullStop { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FOption { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FCMEMarket { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FCBT_Market { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FCOMEX_Market { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FNYMEX_Market { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FMarketFeeWay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FExchangeWay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FCMEMarketFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FCBT_MarketFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FCOMEX_MarketFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FNYMEX_MarketFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FSubClientNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FIsSubSettle { get; set; }
        /// <summary>
        /// 股票权限
        /// </summary>
        public string FStock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FModifyPwdDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FDispMarket { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FMarketFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FMarketFeeP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FClientType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FLoanExpireDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FIdentityType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FPassportType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FCanNotBuy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FCanNotSell { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FCanNotTakeStart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FCanNotTakeEnd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HKCanTradeStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string USCanTradeStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FStockMarcket { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FMarcketPriceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FOccupation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KRCanTradeStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FSellStockHK { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FSellStockAM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FMarketIsPayWay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FOpenType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FClientQuestion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FClientAnswer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SGCanTradeStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string API { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? APIExpiryDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FAutoSendMail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AUCanTradeStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FreezingDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UnfreezeDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FExpiryDate2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FPreMarketRiskInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FSpan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FHKOption { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FUSOption { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FFund { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FRiskLevel { get; set; }
    }
}