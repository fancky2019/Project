
SenderMsgSeqNum��1)�����õ��˵�¼�ܾ���2�������õ�¼�ɹ���Broker ����35=2, Client�ط���֮���35=4��
TargetMsgSeqNum��1)�����õ��˵�¼�ɹ���client�ᷢresend rquest ��35=2 	7=BeginSeqNo��16=EndSeqNo������
                     Broker�ط����֮��ᷢ����SeqNum��35=4������Ϣ ��36=NewSeqNo��
				 2)�����ø����ȵ�¼�ɹ����յ�Broker SeqNum�ͣ��ͷ��ǳ���Ϣ��

seqnums�ļ�
ǰһ�����к�(NextSenderMsgSeqNum):��һ�����кţ�NextTargetMsgSeqNum��

+1
һ���յ�fix message ʱ���IncrNextTargetMsgSeqNum()
��������fix message ʱ���IncrNextSenderMsgSeqNum();


���� SeqNum:
һ���鿴49=SenderCompID,56=TargetCompID��
�����鿴˭����Է����к������⣬�͵��Է������кš�
    �磺1)��49=TTORDER 56=ZDDEV 34=162 58=MsgSeqNum too low, expecting 192 received 92��
	        Broker˵Clinet�����кŵ��ˣ�Ҫ��Client�����кţ�NextSenderMsgSeqNum=192��
	    2)��49=ZDDEV 56=TTORDER 58=MsgSeqNum too low, expecting 570 but received 170
		    Clinet˵Broker�����кŵ��ˣ�Ҫ��Broker���кţ�NextTargetMsgSeqNum=170��

FileStore:
writer.Write(GetNextSenderMsgSeqNum().ToString("D10") + " : " + GetNextTargetMsgSeqNum().ToString("D10") + "  ");
 