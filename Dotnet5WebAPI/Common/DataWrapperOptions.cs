using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Common
{
    /// <summary>
    /// 自动数据格式包装的配置项
    /// </summary>
    public class DataWrapperOptions
    {
        /// <summary>
        /// 当HTTP状态码返回以下结果时，不进行数据包装
        /// 
        /// <para>
        ///     默认集合为:  201, 202, 404
        /// </para>
        /// </summary>
        public List<int> NoWrapStatusCode { get; set; } = new List<int>() { 201, 202, 404 };

        /// <summary>
        /// 是否包装ProblemDetails
        /// <para>
        ///     当存在模型验证错误时，将会返回格式为ProblemDetails的结果
        /// </para>
        /// <para>
        ///     Default :true.
        /// </para>
        /// </summary>
        public bool WrapProblemDetails { get; set; } = true;

        /// <summary>
        /// 当ProblemDetails被包装时，重写返回的http status code。 
        /// 默认值为:200。
        /// <para>
        ///   当该参数为空时，将使用原有的status code。
        /// </para>
        /// </summary>
        public int? RewriteProblemDetailsResponseStatusCode { get; set; } = 200;
    }
}
