一、SocketInitiator类 负责和上手通信
二、SessionState 网络状态TimedOut
    心跳机制：double elapsed = now.Subtract(lastReceivedTime).TotalMilliseconds;
              return elapsed >= (2.4 * heartBtIntMillis);
三、FileStore保存Fix 文件。