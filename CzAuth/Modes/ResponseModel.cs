using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CzAuth.Modes
{
    /// <summary>
    /// 请求响应实体类
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// 响应代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 响应消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回响应数据
        /// </summary>
        public object Data { get; set; }

        public ResponseModel()
        {
            Code = 200;
            Message="Success";
        }

        /// <summary>
        /// 设置响应状态为成功
        /// </summary>
        /// <param name="message"></param>
        public void SetSuccess(string message = "Success")
        {
            Code = 200;
            Message = message;
        }

        /// <summary>
        /// 设置响应状态为失败
        /// </summary>
        /// <param name="message"></param>
        public void SetFailed(string message = "Failed")
        {
            Code = 999;
            Message = message;
        }

    }
}
