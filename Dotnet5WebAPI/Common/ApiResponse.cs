using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Common
{
    /// <summary>
    /// 默认的API返回包装格式体
    /// </summary>
    [Serializable]
    public class ApiResponse : IResultDataWrapper
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 返回code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回不包含结果的未成功api结果
        /// </summary>
        /// <returns></returns>
        public static ApiResponse Failed()
        {
            return new ApiResponse { Success = false };
        }

        /// <summary>
        /// 返回未成功的api结果
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ApiResponse Failed(object data, string message = "", int code = 0)
        {
            return new ApiResponse
            {
                Success = false,
                Code = code == 200 ?
                    500
                    : code,
                Data = data,
                Message = message
            };
        }

        /// <summary>
        /// 返回不包含结果的成功api结果
        /// </summary>
        /// <returns></returns>
        public static ApiResponse Succeed()
        {
            return new ApiResponse { Success = true };
        }

        /// <summary>
        /// 返回成功的api结果
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ApiResponse Succeed(object data, string message = "", int code = 0)
        {
            return new ApiResponse
            {
                Success = true,
                Code = code,
                Data = data,
                Message = message
            };
        }
    }

    /// <summary>
    /// 默认的API返回包装格式体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public new T Data { get; set; }
    }
}
