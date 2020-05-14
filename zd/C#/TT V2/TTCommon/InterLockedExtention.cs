﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TT.Common
{
    /// <summary>
    /// 采用CAS思想实现轻量锁。
    /// 测试发现：Debug下Monitor的性能好
    ///           Release下InterLockedExtention拓展好。
    /// </summary>
    public class InterLockedExtention
    {
        /// <summary>
        /// 1:锁被占用，0：未占用
        /// </summary>
        private static volatile int _lock = 0;

        /// <summary>
        /// 获取锁
        /// </summary>
        /// <returns></returns>
        public static bool Acquire()
        {
            //尝试获取锁
            return Interlocked.CompareExchange(ref _lock, 1, 0) == 0;

        }

        /// <summary>
        /// 释放锁
        /// </summary>
        public static void Release()
        {
            //释放锁
            //Interlocked.CompareExchange(ref _lock, 0, 1);
            //其实不用比较，此方法只有在获取锁的块内调用
            Interlocked.Exchange(ref _lock, 0);

        }
    }
}
