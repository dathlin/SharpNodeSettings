using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SharpNodeSettings.Node.Regular
{
    /// <summary>
    /// 单个规则配置项相关的类型资源，比如配置了什么类型的数据，多少长之类的
    /// </summary>
    public class RegularNodeTypeItem
    {
        #region Constructor
        
        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public RegularNodeTypeItem( )
        {

        }
        
        /// <summary>
        /// 实例化信息
        /// </summary>
        /// <param name="code">代号</param>
        /// <param name="text">文本</param>
        /// <param name="backColor">背景色</param>
        /// <param name="length">单位长度</param>
        public RegularNodeTypeItem( int code, string text, Brush backColor, int length )
        {
            Code = code;
            Text = text;
            BackColor = backColor;
            Length = length;
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// 类型的代号
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 类型的文本描述
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 类型使用的背景色
        /// </summary>
        public Brush BackColor { get; set; }

        /// <summary>
        /// 类型的长度，仅仅是单个数据对象的长度
        /// </summary>
        public int Length { get; set; }

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串信息</returns>
        public override string ToString( )
        {
            return Text;
        }

        #endregion

        #region Const Resource
        
        /// <summary>
        /// Bool数据类型
        /// </summary>
        public static readonly RegularNodeTypeItem Bool = new RegularNodeTypeItem( 1, "bool", Brushes.PaleGreen, 1 );
        /// <summary>
        /// Byte数据类型
        /// </summary>
        public static readonly RegularNodeTypeItem Byte = new RegularNodeTypeItem( 2, "byt", Brushes.Aquamarine, 1 );
        /// <summary>
        /// short数据类型
        /// </summary>
        public static readonly RegularNodeTypeItem Int16 = new RegularNodeTypeItem( 3, "short", Brushes.Pink, 2 );
        /// <summary>
        /// ushort数据类型
        /// </summary>
        public static readonly RegularNodeTypeItem UInt16 = new RegularNodeTypeItem( 4, "ushort", Brushes.Gold, 2 );
        /// <summary>
        /// int数据类型
        /// </summary>
        public static readonly RegularNodeTypeItem Int32 = new RegularNodeTypeItem( 5, "int", Brushes.BlanchedAlmond, 4 );
        /// <summary>
        /// uint数据类型
        /// </summary>
        public static readonly RegularNodeTypeItem UInt32 = new RegularNodeTypeItem( 6, "uint", Brushes.DarkKhaki, 4 );
        /// <summary>
        /// long数据类型
        /// </summary>
        public static readonly RegularNodeTypeItem Int64 = new RegularNodeTypeItem( 7, "long", Brushes.Khaki, 8 );
        /// <summary>
        /// ulong数据类型
        /// </summary>
        public static readonly RegularNodeTypeItem UInt64 = new RegularNodeTypeItem( 8, "ulong", Brushes.Thistle, 8 );
        /// <summary>
        /// float数据类型
        /// </summary>
        public static readonly RegularNodeTypeItem Float = new RegularNodeTypeItem( 9, "float", Brushes.Wheat, 4 );
        /// <summary>
        /// double数据类型
        /// </summary>
        public static readonly RegularNodeTypeItem Double = new RegularNodeTypeItem( 10, "double", Brushes.LightGoldenrodYellow, 8 );
        /// <summary>
        /// string数据类型，ASCII编码
        /// </summary>
        public static readonly RegularNodeTypeItem StringAscii = new RegularNodeTypeItem( 11, "ascii", Brushes.Yellow, 0 );
        /// <summary>
        /// string数据类型，Unicode编码
        /// </summary>
        public static readonly RegularNodeTypeItem StringUnicode = new RegularNodeTypeItem( 12, "unicode", Brushes.YellowGreen, 0 );
        /// <summary>
        /// string数据类型，UTF8编码
        /// </summary>
        public static readonly RegularNodeTypeItem StringUtf8 = new RegularNodeTypeItem( 13, "utf8", Brushes.SandyBrown, 0 );


        public static RegularNodeTypeItem GetDataPraseItemByCode( int code )
        {
            switch (code)
            {
                case 1: return Bool;
                case 2: return Byte;
                case 3: return Int16;
                case 4: return UInt16;
                case 5: return Int32;
                case 6: return UInt32;
                case 7: return Int64;
                case 8: return UInt64;
                case 9: return Float;
                case 10: return Double;
                case 11: return StringAscii;
                case 12: return StringUnicode;
                case 13: return StringUtf8;
                default: return new RegularNodeTypeItem( code, "none", Brushes.Black, 1 );
            }
        }

        #endregion
    }
    
}
