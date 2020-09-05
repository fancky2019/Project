using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix.Fields;
using System.IO;
using StockAdapterHKEX.Common;
using CommonClassLib;

namespace StockAdapterHKEX
{
    /// <summary>
    /// Added by Rainer on 20120323
    /// </summary>
    public class HKEXFixStrategy : StockAdapterHKEX.ICustomFixStrategy
    {
        public static int nextExpectedMsgSeqNum = 0;
        #region ICustomFixStrategy Members

        public QuickFix.SessionSettings SessionSettings { get; set; }

        private CfgManager cfgManager = CfgManager.getInstance(null);

        //private CfgManager cfgManager = CfgManager.getInstance("FutureAdapterHKEX.exe");

        private TargetSubID targetSubID = null;
        private SenderLocationID senderLocationID = null;
        //private ApplicationSystemName appSysName = null;
        //private TradingSystemVersion tradingSysVersion = null;
        //private ApplicationSystemVendor appSysVendor = null;
        // SenderSubID(Tag50 ID) is dependent on order instructions, removed to GlobexCommunication
        // For admin message
        private SenderSubID senderSubID = null;
        private int expectedMsgSeqNum = 1;
        private DateTime expectMsgSeqDT = DateTime.Now;

        public HKEXFixStrategy()
        {
            expectedMsgSeqNum = getExpectedMsgSeqNum();

            targetSubID = new TargetSubID("G");
            senderLocationID = new SenderLocationID(cfgManager.SenderLocationID);
            //appSysName = new ApplicationSystemName(cfgManager.ApplicationSystemName);
            //tradingSysVersion = new TradingSystemVersion(cfgManager.TradingSystemVersion);
            //appSysVendor = new ApplicationSystemVendor(cfgManager.ApplicationSystemVendor);
            senderSubID = new SenderSubID(cfgManager.SenderSubID);
        }


        public Dictionary<int, string> DefaultNewOrderSingleCustomFields
        { get { return new Dictionary<int, string>(); } }

        public void ProcessToAdmin(QuickFix.Message msg, QuickFix.Session session)
        {
            //msg.Header.SetField(senderSubID);
            msg.Header.SetField(new SendingTime(DateTime.UtcNow));
            //msg.Header.SetField(targetSubID);
            //msg.Header.SetField(senderLocationID);

            if (isMessageOfType(msg, MsgType.LOGON))
                addLogonField(msg);
        }
        public void ProcessToApp(QuickFix.Message msg, QuickFix.Session session)
        {
            //msg.Header.SetField(senderSubID);
            msg.Header.SetField(new SendingTime(DateTime.UtcNow));
            //msg.Header.SetField(targetSubID);
            msg.Header.SetField(senderLocationID);

        }

        public void ProcessOrderCancelRequest(QuickFix.FIX50.NewOrderSingle nos, QuickFix.FIX50.OrderCancelRequest msg)
        {
            // Do nothing, logic is placed at GlobexCommnunication.cs
        }
        public void ProcessOrderCancelReplaceRequest(QuickFix.FIX50.NewOrderSingle nos, QuickFix.FIX50.OrderCancelReplaceRequest msg)
        {
            // Do nothing, logic is placed at GlobexCommnunication.cs
        }

        #endregion

        private void addLogonField(QuickFix.Message message)
        {
            string[] user_passwd = cfgManager.SessionAndPsw.Split(',');

            //message.SetField(new Username(user_passwd[0]));
            //message.SetField(new Password(user_passwd[1]));

            message.SetField(new EncryptMethod(0));
            message.SetField(new EncryptedPasswordMethod(101));
            message.SetField(new EncryptedPassword(RSAEncrypt.getRSAEncrypt(cfgManager.RSAPublicKeyPem, user_passwd[1])));
            //首次登陆修改密码
            //message.SetField(new EncryptedNewPassword(RSAEncrypt.getRSAEncrypt(cfgManager.RSAPublicKeyPem, "SHZd7890")));


            #region   fancky  修改 - 自动密码


            #region old
            //if (!string.IsNullOrWhiteSpace(cfgManager.NewPasswd))
            //    message.SetField(new EncryptedNewPassword(RSAEncrypt.getRSAEncrypt(cfgManager.RSAPublicKeyPem, cfgManager.NewPasswd)));
            #endregion

            try
            {
                if (AlterPassword.Instance.CheckShouldAlterPassword(out string newPassword))
                {
                    //set tag 1404
                    message.SetField(new EncryptedNewPassword(RSAEncrypt.getRSAEncrypt(cfgManager.RSAPublicKeyPem, newPassword)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TradeServerFacade.writeLog(LogLevel.SYSTEMERROR, ex.ToString());
            }
            #endregion

            //message.SetField(new EncryptedPassword(user_passwd[1]));
            //message.SetField(new EncryptedPassword("abc123"));

            /*if (message.Header.IsSetField(Tags.LastMsgSeqNumProcessed))
                message.SetField(new NextExpectedMsgSeqNum(message.Header.GetInt(Tags.LastMsgSeqNumProcessed) + 1));
            else
                message.SetField(new NextExpectedMsgSeqNum(1));
            */

            //到了第二天。重置expectedMsgSeqNum
            if (DateTime.Now.DayOfYear > expectMsgSeqDT.DayOfYear && DateTime.Now.Year == expectMsgSeqDT.Year)
            {
                expectedMsgSeqNum = 1;
                nextExpectedMsgSeqNum = 0;
                expectMsgSeqDT = DateTime.Now;
            }
            else if (DateTime.Now.Year > expectMsgSeqDT.Year)
            {
                expectedMsgSeqNum = 1;
                nextExpectedMsgSeqNum = 0;
                expectMsgSeqDT = DateTime.Now;
            }

            message.SetField(new NextExpectedMsgSeqNum(expectedMsgSeqNum > nextExpectedMsgSeqNum ? expectedMsgSeqNum : nextExpectedMsgSeqNum));

            //test code
            /* 
            int i = expectedMsgSeqNum > nextExpectedMsgSeqNum ? expectedMsgSeqNum : nextExpectedMsgSeqNum;
            i += 2;
            message.SetField(new NextExpectedMsgSeqNum(i));
            */
            //test code

            //message.SetField(new HeartBtInt(55));
            message.SetField(new DefaultApplVerID("9"));

        }

        private bool isMessageOfType(QuickFix.Message message, String type)
        {
            try
            {
                return type == message.Header.GetField(Tags.MsgType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        private int getExpectedMsgSeqNum()
        {
            int seqnums = 1;
            StreamReader fileStream = null;
            try
            {
                string directory = cfgManager.ExpectedMsgSeqNumDir;
                if (!Directory.Exists(directory)) return seqnums;

                DateTime maxDT = new DateTime(1000, 1, 1);
                string filePath = "";

                DirectoryInfo dir = new DirectoryInfo(directory);
                FileSystemInfo[] files = dir.GetFileSystemInfos("*.seqnums");
                foreach (FileSystemInfo file in files)
                {
                    DateTime curDT = file.LastWriteTime;
                    if (curDT.Ticks > maxDT.Ticks)
                    {
                        maxDT = curDT;
                        filePath = file.FullName;
                    }
                }
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return seqnums;

                fileStream = System.IO.File.OpenText(filePath);
                string resultString = string.Empty;
                StringBuilder sb = new StringBuilder();
                while ((resultString = fileStream.ReadLine()) != null)
                {
                    sb.AppendLine(resultString);
                }

                seqnums = Int16.Parse(sb.ToString().Split(':')[1]);

            }
            catch (Exception)
            {
                return seqnums;
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }

            return seqnums;
        }
    }
}
