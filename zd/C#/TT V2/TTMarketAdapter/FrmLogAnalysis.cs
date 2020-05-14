using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TT.Common;

namespace TTMarketAdapter
{
    public partial class FrmLogAnalysis : Form
    {
        List<EntityModel> _entityModels = null;
        public FrmLogAnalysis()
        {
            InitializeComponent();

        }
        private void FrmLogAnalysis_Load(object sender, EventArgs e)
        {
            _entityModels = new List<EntityModel>();
            List<string> msgType = new List<string>() { "All" };
            //msgType.AddRange(Enum.GetNames(typeof(MessageType)));
            msgType.Add(LogMessageType.None);
            msgType.Add(LogMessageType.Trade);
            msgType.Add(LogMessageType.Incremental);
            msgType.Add(LogMessageType.SnapShot);
            msgType.Add(LogMessageType.FracPrx);
            msgType.Add(LogMessageType.EventHandler);
            this.cmbDataType.Items.AddRange(msgType.ToArray());
            this.cmbDataType.SelectedIndex = 0;
        }

        private void BtnAddLog_Click(object sender, EventArgs e)
        {

            //  ofd.InitialDirectory = @"C:\Users\Administrator\Desktop";
            ofd.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SendMsg");
            ofd.Filter = "txt files (*.txt)|*.txt";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.FileName = "";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.txtLogPath.Text = this.ofd.FileName;

                }
                catch (Exception ex)
                {
                    TT.Common.NLogUtility.Info(ex.ToString());
                }
            }

        }

        private void BtnReadLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtLogPath.Text))
                {
                    MessageBox.Show("请选择日志路径！");
                    return;
                }
                BtnClearQueryCondition_Click(null, null);
                var content = TxtFile.ReadTxtFile(this.txtLogPath.Text);

                //var content = new List<string> { @"2019-12-27 13:47:47:860  2019-12-27 13:47:46:989 Incremental MsgSeqNum:574136 - ICE@BRN2002@65.84@37@65.85@8@@0@@@@@@2019-12-27 13:47:46@13238@0@@65.82@65.81@65.80@0@51@55@51@65.86@65.87@65.88@@46@50@43@0@@@65.85@3@Z@@15@@@@@Z" };
                _entityModels = CreateEntityMode(content);
                this.dgvMarketData.DataSource = _entityModels;

            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
        }

        private List<EntityModel> CreateEntityMode(List<string> strList)
        {
            List<EntityModel> entityModels = new List<EntityModel>();

            strList.ForEach(p =>
            {
                try
                {
                    EntityModel entityModel = new EntityModel();
                    entityModel.SendTime = p.Substring(25, 23);


                    //2019-07-01 13:21:04:632  SnapShot LME
                    //var forthEmptyIndex = p.IndexOf(" ", 25);
                    var msgSeqNumIndex = p.IndexOf("MsgSeqNum");
                    var msgType = p.Substring(49, msgSeqNumIndex - 1 - 49);
                    entityModel.Type = msgType;
                    string data = p.Substring(p.IndexOf(" - ") + 3);

                    var dataArr = data.Split('@');
                    entityModel.ZDExchange = dataArr[0];
                    entityModel.ZDCode = dataArr[1];
                    if (msgType == "Trade")//pumpTrade
                    {
                        entityModel.Price = dataArr[2];
                        entityModel.Quantity = dataArr[3];

                    }
                    else//processIncrementalRefresh
                    {
                        entityModel.OpenPrice = dataArr[10];
                        entityModel.SettlementPrice = dataArr[11];
                        entityModel.PreviousSettlementPrice = dataArr[40];
                        entityModel.Price = dataArr[6];
                        entityModel.Quantity = dataArr[7];

                        entityModel.BidPrice1 = dataArr[2];
                        entityModel.BidPrice2 = dataArr[16];
                        entityModel.BidPrice3 = dataArr[17];
                        entityModel.BidPrice4 = dataArr[18];
                        entityModel.BidPrice5 = dataArr[19];
                        entityModel.BidPriceQuantity1 = dataArr[3];
                        entityModel.BidPriceQuantity2 = dataArr[20];
                        entityModel.BidPriceQuantity3 = dataArr[21];
                        entityModel.BidPriceQuantity4 = dataArr[22];
                        entityModel.BidPriceQuantity5 = dataArr[23];

                        entityModel.AskPrice1 = dataArr[4];
                        entityModel.AskPrice2 = dataArr[24];
                        entityModel.AskPrice3 = dataArr[25];
                        entityModel.AskPrice4 = dataArr[26];
                        entityModel.AskPrice5 = dataArr[27];
                        entityModel.AskPriceQuantity1 = dataArr[5];
                        entityModel.AskPriceQuantity2 = dataArr[28];
                        entityModel.AskPriceQuantity3 = dataArr[29];
                        entityModel.AskPriceQuantity4 = dataArr[30];
                        entityModel.AskPriceQuantity5 = dataArr[31];
                    }

                    entityModels.Add(entityModel);
                }
                catch (Exception ex)
                {
                    TT.Common.NLogUtility.Error(ex.ToString());
                }
            });
            entityModels = entityModels.OrderByDescending(p => p.SendTime).ToList();
            return entityModels;
        }


        //表格只读  ReadOnly=True
        private void DgvMarketData_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void DgvMarketData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //DataGridViewRow dgr = dgvMarketData.Rows[e.RowIndex];
            //try
            //{
            //    if (dgr.Cells["Type"].Value.ToString() == "50")
            //    {
            //        if (dgr.Cells["BidPrice1"].Value == null || string.IsNullOrEmpty(dgr.Cells["BidPrice1"].Value.ToString()))
            //        {
            //            // dgr.Cells["BidPrice1"].Style.BackColor = Color.Red;

            //            dgr.DefaultCellStyle.BackColor = Color.Red;
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
        }


        private void CmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnQuery_Click(null, null);
            this.dgvMarketData.Focus();

        }
        private void BtnClearQueryCondition_Click(object sender, EventArgs e)
        {
            this.cmbDataType.SelectedIndex = 0;


            this.cbOpenPrice.Checked = false;
            this.cbSettleMentPrice.Checked = false;
            this.cbPreviousSettlementPrice.Checked = false;
            this.cbLatestPrice.Checked = false;
            this.cbQuantity.Checked = false;

            this.cbBidPrice1.Checked = false;
            this.cbBidPrice2.Checked = false;
            this.cbBidPrice3.Checked = false;
            this.cbBidPrice4.Checked = false;
            this.cbBidPrice5.Checked = false;

            this.cbBidPriceQuantity1.Checked = false;
            this.cbBidPriceQuantity2.Checked = false;
            this.cbBidPriceQuantity3.Checked = false;
            this.cbBidPriceQuantity4.Checked = false;
            this.cbBidPriceQuantity5.Checked = false;

            this.cbAskPrice1.Checked = false;
            this.cbAskPrice2.Checked = false;
            this.cbAskPrice3.Checked = false;
            this.cbAskPrice4.Checked = false;
            this.cbAskPrice5.Checked = false;

            this.cbAskPriceQuantity1.Checked = false;
            this.cbAskPriceQuantity2.Checked = false;
            this.cbAskPriceQuantity3.Checked = false;
            this.cbAskPriceQuantity4.Checked = false;
            this.cbAskPriceQuantity5.Checked = false;

            this.cbBidPriceCompare.Checked = false;
            this.cbAskPriceCompare.Checked = false;
        }
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            IEnumerable<EntityModel> filterList = _entityModels;

            if (this.cmbDataType.SelectedIndex != 0)
            {
                filterList = filterList.Where(p => p.Type == this.cmbDataType.Text);
            }

            if (this.cbOpenPrice.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.OpenPrice));
            }
            if (this.cbSettleMentPrice.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.SettlementPrice));
            }
            if (this.cbPreviousSettlementPrice.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.PreviousSettlementPrice));
            }
            if (this.cbLatestPrice.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.Price));
            }
            if (this.cbQuantity.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.Quantity));
            }





            if (this.cbBidPrice1.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.BidPrice1));
            }
            if (this.cbBidPrice2.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.BidPrice2));
            }
            if (this.cbBidPrice3.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.BidPrice3));
            }
            if (this.cbBidPrice4.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.BidPrice4));
            }
            if (this.cbBidPrice5.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.BidPrice5));
            }


            if (this.cbBidPriceQuantity1.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.BidPriceQuantity1));
            }
            if (this.cbBidPriceQuantity2.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.BidPriceQuantity2));
            }
            if (this.cbBidPriceQuantity3.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.BidPriceQuantity3));
            }
            if (this.cbBidPriceQuantity4.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.BidPriceQuantity4));
            }
            if (this.cbBidPriceQuantity5.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.BidPriceQuantity5));
            }



            if (this.cbAskPrice1.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.AskPrice1));

            }
            if (this.cbAskPrice2.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.AskPrice2));
            }
            if (this.cbAskPrice3.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.AskPrice3));
            }
            if (this.cbAskPrice4.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.AskPrice4));
            }
            if (this.cbAskPrice5.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.AskPrice5));
            }


            if (this.cbAskPriceQuantity1.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.AskPriceQuantity1));
            }
            if (this.cbAskPriceQuantity2.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.AskPriceQuantity2));
            }
            if (this.cbAskPriceQuantity3.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.AskPriceQuantity3));
            }
            if (this.cbAskPriceQuantity4.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.AskPriceQuantity4));
            }
            if (this.cbAskPriceQuantity5.Checked)
            {
                filterList = filterList.Where(p => string.IsNullOrEmpty(p.AskPriceQuantity5));
            }


            if (this.cbBidPriceCompare.Checked)
            {
                //筛选是方法不能延迟查询，必须立即执行。
                //filterList = filterList.Where(p =>!( ParesePrice(p.BidPrice1) <= ParesePrice(p.BidPrice2) &&
                //                                   ParesePrice(p.BidPrice2) <= ParesePrice(p.BidPrice3) &&
                //                                   ParesePrice(p.BidPrice3) <= ParesePrice(p.BidPrice4) &&
                //                                   ParesePrice(p.BidPrice4) <= ParesePrice(p.BidPrice5))).ToList();


                filterList = filterList.Where(p => !p.BidPriceCompare()).ToList();
            }
            if (this.cbAskPriceCompare.Checked)
            {
                //filterList = filterList.Where(p => !(ParesePrice(p.AskPrice1) >= ParesePrice(p.AskPrice2) &&
                //                                   ParesePrice(p.AskPrice2) >= ParesePrice(p.AskPrice3) &&
                //                                   ParesePrice(p.AskPrice3) >= ParesePrice(p.AskPrice4) &&
                //                                   ParesePrice(p.AskPrice4) >= ParesePrice(p.AskPrice5))).ToList(); ;
                filterList = filterList.Where(p => !p.AskPriceCompare()).ToList();
            }


            //var  re = filterList.ToList();
            this.dgvMarketData.DataSource = filterList.ToList();
        }

        private decimal ParesePrice(string price)
        {
            return string.IsNullOrEmpty(price) ? 0M : decimal.Parse(price);
        }


    }





    class EntityModel
    {
        //sb.Append(cb.zdExchg).Append('@').Append(cb.zdCode).Append('@')           //交易所代码和合约代码
        //            .Append((bidList[0].Price == 0D) ? String.Empty : String.Format(prxFormat, bidList[0].Price)).Append('@')   //买价
        //            .Append(bidList[0].Quantity).Append('@')                              //买量
        //            .Append((askList[0].Price == 0D) ? String.Empty : String.Format(prxFormat, askList[0].Price)).Append('@') //卖价
        //            .Append(askList[0].Quantity).Append('@');                              //卖量
        //sb.Append((orderBookAll.lastTrade.Price == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.lastTrade.Price)).Append('@') //最新价
        //            .Append(orderBookAll.lastTrade.Quantity).Append('@');                              //现手  （最新价的成交量）
        //sb.Append((orderBookAll.tsHighPrice == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.tsHighPrice)).Append('@')  //最高价
        //            .Append((orderBookAll.tsLowPrice == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.tsLowPrice)).Append('@')   //最低价
        //            .Append((orderBookAll.openingPrice == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.openingPrice)).Append('@')//开盘价
        //            .Append((orderBookAll.settPrice == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.settPrice)).Append('@')//昨结算价
        //.Append((orderBookAll.lastTrade.Price == 0F) ? String.Empty : String.Format(prxFormat, orderBookAll.lastTrade.Price)).Append('@') //最新价
        //    .Append(orderBookAll.lastTrade.Quantity).Append('@')                              //现手量
        public string ZDExchange { get; set; }
        public string ZDCode { get; set; }
        public string SendTime { get; set; }
        public string OpenPrice { get; set; }
        public string SettlementPrice { get; set; }
        public string PreviousSettlementPrice { get; set; }

        public string BidPrice1 { get; set; }
        public string BidPrice2 { get; set; }
        public string BidPrice3 { get; set; }
        public string BidPrice4 { get; set; }
        public string BidPrice5 { get; set; }

        public string BidPriceQuantity1 { get; set; }
        public string BidPriceQuantity2 { get; set; }
        public string BidPriceQuantity3 { get; set; }
        public string BidPriceQuantity4 { get; set; }
        public string BidPriceQuantity5 { get; set; }



        public string AskPrice1 { get; set; }
        public string AskPrice2 { get; set; }
        public string AskPrice3 { get; set; }
        public string AskPrice4 { get; set; }
        public string AskPrice5 { get; set; }
        public string AskPriceQuantity1 { get; set; }
        public string AskPriceQuantity2 { get; set; }
        public string AskPriceQuantity3 { get; set; }
        public string AskPriceQuantity4 { get; set; }
        public string AskPriceQuantity5 { get; set; }

        /// <summary>
        /// 最新价
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 现手量
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// 50:processIncrementalRefresh
        /// 34:pumpTrade
        /// </summary>
        public string Type { get; set; }


        public bool BidPriceCompare()
        {
            //bool re = PriceCompare(ParesePrice(this.BidPrice1), ParesePrice(this.BidPrice2)) &&
            //          PriceCompare(ParesePrice(this.BidPrice2), ParesePrice(this.BidPrice3)) &&
            //          PriceCompare(ParesePrice(this.BidPrice3), ParesePrice(this.BidPrice4)) &&
            //          PriceCompare(ParesePrice(this.BidPrice4), ParesePrice(this.BidPrice5));
            bool re = ParesePrice(this.BidPrice1) >= ParesePrice(this.BidPrice2) &&
                      ParesePrice(this.BidPrice2) >= ParesePrice(this.BidPrice3) &&
                      ParesePrice(this.BidPrice3) >= ParesePrice(this.BidPrice4) &&
                      ParesePrice(this.BidPrice4) >= ParesePrice(this.BidPrice5);
            return re;
        }


        public bool AskPriceCompare()
        {
            var re = PriceCompare(ParesePrice(this.AskPrice1), ParesePrice(this.AskPrice2)) &&
                     PriceCompare(ParesePrice(this.AskPrice2), ParesePrice(this.AskPrice3)) &&
                     PriceCompare(ParesePrice(this.AskPrice3), ParesePrice(this.AskPrice4)) &&
                     PriceCompare(ParesePrice(this.AskPrice4), ParesePrice(this.AskPrice5));

            return re;
        }

        private bool PriceCompare(decimal x, decimal y)
        {
            if (y == 0)
            {
                return true;
            }
            else
            {
                return x <= y;
            }
        }
        private decimal ParesePrice(string price)
        {
            var re = string.IsNullOrEmpty(price) ? 0M : decimal.Parse(price);
            return re;
        }
    }
}
