using System;

namespace Mir.WindowsServiceTemplate.Extension
{
    /// <summary>
    /// Double类型扩展
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// 将Double转换成Int16
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static short ToInt16(this double obj) => short.Parse(obj.ToString());

        /// <summary>
        /// 将Double转换成整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt32(this double obj) => int.Parse(obj.ToString());

        /// <summary>
        /// 将Double转换成长整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToInt64(this double obj) => long.Parse(obj.ToString());

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="digits">四舍五入位数</param>
        /// <returns></returns>
        public static double ToRound(this double obj, int digits = 0) => Math.Round(obj, digits);
    }
}