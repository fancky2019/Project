// This is a generated file.  Don't edit it directly!

namespace QuickFix
{
    namespace FIX42
    {
        public class MessageFactory : IMessageFactory
        {
            public QuickFix.Message Create(string beginString, string msgType)
            {
                switch (msgType)
                {
                    case QuickFix.FIX42.Heartbeat.MsgType: return new QuickFix.FIX42.Heartbeat();
                    case QuickFix.FIX42.TestRequest.MsgType: return new QuickFix.FIX42.TestRequest();
                    case QuickFix.FIX42.ResendRequest.MsgType: return new QuickFix.FIX42.ResendRequest();
                    case QuickFix.FIX42.Reject.MsgType: return new QuickFix.FIX42.Reject();
                    case QuickFix.FIX42.SequenceReset.MsgType: return new QuickFix.FIX42.SequenceReset();
                    case QuickFix.FIX42.Logout.MsgType: return new QuickFix.FIX42.Logout();
                    case QuickFix.FIX42.UnknownFixMessage.MsgType: return new QuickFix.FIX42.UnknownFixMessage();
                    case QuickFix.FIX42.ExecutionReport.MsgType: return new QuickFix.FIX42.ExecutionReport();
                    case QuickFix.FIX42.OrderCancelReject.MsgType: return new QuickFix.FIX42.OrderCancelReject();
                    case QuickFix.FIX42.NewOrderSingle.MsgType: return new QuickFix.FIX42.NewOrderSingle();
                    case QuickFix.FIX42.OrderCancelReplaceRequest.MsgType: return new QuickFix.FIX42.OrderCancelReplaceRequest();
                    case QuickFix.FIX42.OrderCancelRequest.MsgType: return new QuickFix.FIX42.OrderCancelRequest();
                    case QuickFix.FIX42.SecurityDefinitionRequest.MsgType: return new QuickFix.FIX42.SecurityDefinitionRequest();
                    case QuickFix.FIX42.SecurityDefinition.MsgType: return new QuickFix.FIX42.SecurityDefinition();
                    case QuickFix.FIX42.SecurityStatusRequest.MsgType: return new QuickFix.FIX42.SecurityStatusRequest();
                    case QuickFix.FIX42.SecurityStatus.MsgType: return new QuickFix.FIX42.SecurityStatus();
                    case QuickFix.FIX42.MarketDataRequest.MsgType: return new QuickFix.FIX42.MarketDataRequest();
                    case QuickFix.FIX42.MarketDataRequestReject.MsgType: return new QuickFix.FIX42.MarketDataRequestReject();
                    case QuickFix.FIX42.MarketDataSnapshot.MsgType: return new QuickFix.FIX42.MarketDataSnapshot();
                    case QuickFix.FIX42.MarketDataIncrementalRefresh.MsgType: return new QuickFix.FIX42.MarketDataIncrementalRefresh();
                    case QuickFix.FIX42.QuoteRequest.MsgType: return new QuickFix.FIX42.QuoteRequest();
                    case QuickFix.FIX42.Logon.MsgType: return new QuickFix.FIX42.Logon();
                    case QuickFix.FIX42.BusinessMessageReject.MsgType: return new QuickFix.FIX42.BusinessMessageReject();
                    case QuickFix.FIX42.OrderStatusRequest.MsgType: return new QuickFix.FIX42.OrderStatusRequest();
                    case QuickFix.FIX42.TradeCaptureReport.MsgType: return new QuickFix.FIX42.TradeCaptureReport();
                    case QuickFix.FIX42.TradeCaptureReportAck.MsgType: return new QuickFix.FIX42.TradeCaptureReportAck();
                    case QuickFix.FIX42.News.MsgType: return new QuickFix.FIX42.News();
                }

                return new QuickFix.Message();
            }


            public Group Create(string beginString, string msgType, int correspondingFieldID)
            {
                if (QuickFix.FIX42.ExecutionReport.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX42.ExecutionReport.NoPartyIDsGroup();
                        case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX42.ExecutionReport.NoSecurityAltIDGroup();
                        case QuickFix.Fields.Tags.NoStrategyParameters: return new QuickFix.FIX42.ExecutionReport.NoStrategyParametersGroup();
                        case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX42.ExecutionReport.NoLegsGroup();
                        case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX42.ExecutionReport.NoLegsGroup.NoLegSecurityAltIDGroup();
                        case QuickFix.Fields.Tags.NoLinks: return new QuickFix.FIX42.ExecutionReport.NoLinksGroup();
                        case QuickFix.Fields.Tags.NoFills: return new QuickFix.FIX42.ExecutionReport.NoFillsGroup();
                        case QuickFix.Fields.Tags.NoOrderAttributes: return new QuickFix.FIX42.ExecutionReport.NoOrderAttributesGroup();
                    }
                }

                if (QuickFix.FIX42.OrderCancelReject.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoStrategyParameters: return new QuickFix.FIX42.OrderCancelReject.NoStrategyParametersGroup();
                    }
                }

                if (QuickFix.FIX42.NewOrderSingle.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX42.NewOrderSingle.NoSecurityAltIDGroup();
                        case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX42.NewOrderSingle.NoPartyIDsGroup();
                        case QuickFix.Fields.Tags.NoStrategyParameters: return new QuickFix.FIX42.NewOrderSingle.NoStrategyParametersGroup();
                        case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX42.NewOrderSingle.NoLegsGroup();
                        case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX42.NewOrderSingle.NoLegsGroup.NoLegSecurityAltIDGroup();
                        case QuickFix.Fields.Tags.NoOrderAttributes: return new QuickFix.FIX42.NewOrderSingle.NoOrderAttributesGroup();
                    }
                }

                if (QuickFix.FIX42.OrderCancelReplaceRequest.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoStrategyParameters: return new QuickFix.FIX42.OrderCancelReplaceRequest.NoStrategyParametersGroup();
                        case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX42.OrderCancelReplaceRequest.NoPartyIDsGroup();
                        case QuickFix.Fields.Tags.NoOrderAttributes: return new QuickFix.FIX42.OrderCancelReplaceRequest.NoOrderAttributesGroup();
                    }
                }

                if (QuickFix.FIX42.SecurityDefinition.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX42.SecurityDefinition.NoSecurityAltIDGroup();
                        case QuickFix.Fields.Tags.NoEvents: return new QuickFix.FIX42.SecurityDefinition.NoEventsGroup();
                        case QuickFix.Fields.Tags.NumTickTblEntries: return new QuickFix.FIX42.SecurityDefinition.NumTickTblEntriesGroup();
                        case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX42.SecurityDefinition.NoLegsGroup();
                        case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX42.SecurityDefinition.NoLegsGroup.NoLegSecurityAltIDGroup();
                    }
                }

                if (QuickFix.FIX42.SecurityStatusRequest.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX42.SecurityStatusRequest.NoSecurityAltIDGroup();
                        case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX42.SecurityStatusRequest.NoLegsGroup();
                        case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX42.SecurityStatusRequest.NoLegsGroup.NoLegSecurityAltIDGroup();
                    }
                }

                if (QuickFix.FIX42.MarketDataRequest.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoMDEntryTypes: return new QuickFix.FIX42.MarketDataRequest.NoMDEntryTypesGroup();
                        case QuickFix.Fields.Tags.NoRelatedSym: return new QuickFix.FIX42.MarketDataRequest.NoRelatedSymGroup();
                        case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX42.MarketDataRequest.NoRelatedSymGroup.NoSecurityAltIDGroup();
                        case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX42.MarketDataRequest.NoRelatedSymGroup.NoLegsGroup();
                        case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX42.MarketDataRequest.NoRelatedSymGroup.NoLegsGroup.NoLegSecurityAltIDGroup();
                    }
                }

                if (QuickFix.FIX42.MarketDataSnapshot.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoMDEntries: return new QuickFix.FIX42.MarketDataSnapshot.NoMDEntriesGroup();
                    }
                }

                if (QuickFix.FIX42.MarketDataIncrementalRefresh.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoMDEntries: return new QuickFix.FIX42.MarketDataIncrementalRefresh.NoMDEntriesGroup();
                    }
                }

                if (QuickFix.FIX42.QuoteRequest.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoRelatedSym: return new QuickFix.FIX42.QuoteRequest.NoRelatedSymGroup();
                    }
                }

                if (QuickFix.FIX42.TradeCaptureReport.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX42.TradeCaptureReport.NoSecurityAltIDGroup();
                        case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX42.TradeCaptureReport.NoLegsGroup();
                        case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX42.TradeCaptureReport.NoLegsGroup.NoLegSecurityAltIDGroup();
                        case QuickFix.Fields.Tags.NoSides: return new QuickFix.FIX42.TradeCaptureReport.NoSidesGroup();
                        case QuickFix.Fields.Tags.NoPartyIDs: return new QuickFix.FIX42.TradeCaptureReport.NoSidesGroup.NoPartyIDsGroup();
                        case QuickFix.Fields.Tags.NoTCRLegs: return new QuickFix.FIX42.TradeCaptureReport.NoTCRLegsGroup();
                     //   case QuickFix.Fields.Tags.NoSides: return new QuickFix.FIX42.TradeCaptureReport.NoTCRLegsGroup.NoSidesGroup();
                    }
                }

                if (QuickFix.FIX42.TradeCaptureReportAck.MsgType.Equals(msgType))
                {
                    switch (correspondingFieldID)
                    {
                        case QuickFix.Fields.Tags.NoSecurityAltID: return new QuickFix.FIX42.TradeCaptureReportAck.NoSecurityAltIDGroup();
                        case QuickFix.Fields.Tags.NoLegs: return new QuickFix.FIX42.TradeCaptureReportAck.NoLegsGroup();
                        case QuickFix.Fields.Tags.NoLegSecurityAltID: return new QuickFix.FIX42.TradeCaptureReportAck.NoLegsGroup.NoLegSecurityAltIDGroup();
                    }
                }

                return null;
            }

        }
    }
}
