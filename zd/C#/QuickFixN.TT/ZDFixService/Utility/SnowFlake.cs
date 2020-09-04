﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZDFixService.Utility
{
    /// <summary>
    /// 去除dataCenter概念，改成10位机器ID;序列号为可配置。
    /// </summary>
    public class SnowFlake
    {

        /// <summary>
        ///  开始时间
        /// </summary>
        private long _twepoch;


        /// <summary>
        ///机器id所占的位数
        /// </summary>
        private const int _workerIdBits = 10;


        /// <summary>
        ///  支持的最大机器id，结果是1024 (这个移位算法可以很快的计算出几位二进制数所能表示的最大十进制数) 
        /// </summary>
        private const long _maxWorkerId = -1L ^ (-1L << _workerIdBits);

        /// <summary>
        ///序列在id中占的位数 
        /// </summary>
        private readonly int _sequenceBits;

        /// <summary>
        ///机器ID向左移位数
        /// </summary>
        private readonly int _workerIdShift;


        /// <summary>
        ///时间截向左移位数
        /// </summary>
        private readonly int _timestampLeftShift;

        /// <summary>
        ///生成序列的掩码
        /// </summary>
        private readonly long _sequenceMask;

        /// <summary>
        /// 工作机器ID(0~1023) 
        /// </summary>
        private readonly long _workerId;

        /// <summary>
        ///毫秒内序列
        /// </summary>
        private long _sequence = 0L;

        /// <summary>
        /// 上次生成ID的时间截 
        /// </summary>
        private long _lastTimestamp = -1L;

        private object _lockObj = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="sequenceBits"></param>
        /// <param name="startTime"></param>
        public SnowFlake(long workerId, DateTime startTime, int sequenceBits=8)
        {

            if (workerId > _maxWorkerId || workerId < 0)
            {
                throw new Exception($"worker Id can't be greater than {_maxWorkerId} or less than 0");
            }
            if (sequenceBits < 1 || sequenceBits > 12)
            {
                throw new Exception($"illegal sequenceBits -  {sequenceBits}");
            }
            _twepoch = startTime.Ticks / 10000;
            if ((DateTime.Now.Year - startTime.Year) > 69)
            {
                throw new Exception($"There is not enough id to generate.Reset start time.");
            }
            this._workerId = workerId;
            this._sequenceBits = sequenceBits;
            _workerIdShift = _sequenceBits;
            _timestampLeftShift = _sequenceBits + _workerIdBits;
            _sequenceMask = -1L ^ (-1L << _sequenceBits);
        }

        /// <summary>
        /// 获得下一个ID,单线程可不用加锁
        /// </summary>
        /// <returns></returns>
        public long NextId()
        {

            lock (_lockObj)
            {
                long timestamp = TimeGen();
                //如果当前时间小于上一次ID生成的时间戳，说明系统时钟回退过这个时候应当抛出异常
                if (timestamp < _lastTimestamp)
                {
                    throw new Exception($"Clock moved backwards.  Refusing to generate id for {_lastTimestamp - timestamp} milliseconds");
                }

                //如果是同一时间生成的，则进行毫秒内序列
                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & _sequenceMask;
                    //毫秒内序列溢出
                    if (_sequence == 0)
                    {
                        //阻塞到下一个毫秒,获得新的时间戳
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                //时间戳改变，毫秒内序列重置
                else
                {
                    _sequence = 0L;
                }

                //上次生成ID的时间截
                _lastTimestamp = timestamp;
                var r1 = (timestamp - _twepoch);
                var r2 = r1 << _timestampLeftShift;
                //移位并通过或运算拼到一起组成64位的ID
                return ((timestamp - _twepoch) << _timestampLeftShift)
                        | (_workerId << _workerIdShift)
                        | _sequence;
            }

        }

        /// <summary>
        /// 阻塞到下一个毫秒，直到获得新的时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns>当前时间戳</returns>
        protected long TilNextMillis(long lastTimestamp)
        {
            long timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                new SpinWait().SpinOnce();
                //Thread.Sleep(1);自旋1ms
                timestamp = TimeGen();
            }
            return timestamp;
        }

        /// <summary>
        ///  返回以毫秒为单位的当前时间
        /// </summary>
        /// <returns>当前时间(毫秒)</returns>
        protected long TimeGen()
        {
            return DateTime.Now.Ticks / 10000;
        }

        public string ResolveID(long id)
        {
            var bitStr = System.Convert.ToString(id, 2);
            int len = bitStr.Length;

            int timestampLength = len - _workerIdBits - _sequenceBits;
            int timestampStart = 0;
            int workerIdStart = timestampLength;
            int sequenceStart = workerIdStart + _workerIdBits;

            string timestampBit = bitStr.Substring(timestampStart, timestampLength);
            string workerIdBit = bitStr.Substring(workerIdStart, _workerIdBits);
            string sequenceBit = bitStr.Substring(sequenceStart, _sequenceBits);

            int sequenceInt = Convert.ToInt32(sequenceBit, 2);
            int workerIdInt = Convert.ToInt32(workerIdBit, 2);
            long timestampLong = Convert.ToInt64(timestampBit, 2);

            var generateMillisecond = timestampLong + _twepoch;
            string dateTime = new DateTime(generateMillisecond * 10000).ToString("yyyy-MM-dd HH:mm:ss fff");
            var anonymous = new { CreateTime = dateTime, WorkID = workerIdInt, Sequence = sequenceInt };
            var jsonAnonymousStr = NewtonsoftHelper.JsonSerializeObjectFormat(anonymous);
            return jsonAnonymousStr;
        }



        public long Test()
        {
            //DateTime startDate = DateTime.Parse("1970-01-01");
            //Random random = new Random();
            //for (int i = 0; i < 49; i++)
            //{
            //    var year = startDate.Year + i;
            //    var month = 1 + random.Next(0, 12);
            //    var day = 1 + random.Next(0, 27);
            //    var dateStr = $"{year}-{month}-{day}";
            //    DateTime dateTime = startDate;
            //    try
            //    {
            //        dateTime = DateTime.Parse(dateStr);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Exception " + dateStr);
            //    }

            //    _twepoch = dateTime.Ticks / 10000;

            //    long id1 = this.NextId();
            //    var bitStr = System.Convert.ToString(id1, 2);
            //    string idStr = $"{dateTime.ToString("yyyy-MM-dd")} - {id1} - {id1.ToString().Length} - {bitStr.Length}";
            //    Console.WriteLine(idStr);
            //}




            //long id = this.NextId();

            //ResolveID(id);
            //return id;
            return 0;
        }


    }
}
