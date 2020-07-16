
SenderMsgSeqNum：1)、设置低了登录拒绝。2）、设置登录成功，Broker 发送35=2, Client重发完之后回35=4。
TargetMsgSeqNum：1)、设置低了登录成功，client会发resend rquest （35=2 	7=BeginSeqNo，16=EndSeqNo）请求，
                     Broker重发完成之后会发重置SeqNum（35=4）的消息 ，36=NewSeqNo。
				 2)、设置高了先登录成功，收到Broker SeqNum低，就发登出消息。

seqnums文件
前一个序列号(NextSenderMsgSeqNum):后一个序列号（NextTargetMsgSeqNum）

+1
一、收到fix message 时候会IncrNextTargetMsgSeqNum()
二、发送fix message 时候会IncrNextSenderMsgSeqNum();


调试 SeqNum:
一、查看49=SenderCompID,56=TargetCompID。
二、查看谁报告对方序列号有问题，就调对方的序列号。
    如：1)、49=TTORDER 56=ZDDEV 34=162 58=MsgSeqNum too low, expecting 192 received 92。
	        Broker说Clinet的序列号低了，要调Client调序列号：NextSenderMsgSeqNum=192。
	    2)、49=ZDDEV 56=TTORDER 58=MsgSeqNum too low, expecting 570 but received 170
		    Clinet说Broker的序列号低了，要调Broker的列号：NextTargetMsgSeqNum=170。

FileStore:
writer.Write(GetNextSenderMsgSeqNum().ToString("D10") + " : " + GetNextTargetMsgSeqNum().ToString("D10") + "  ");
 