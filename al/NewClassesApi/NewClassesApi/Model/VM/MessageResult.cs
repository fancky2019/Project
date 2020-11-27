using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewClassesApi.Model.VM
{
    public class MessageResult<T>
    {

        /**
         * 执行结果（true:成功，false:失败）
         */
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
