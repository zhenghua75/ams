using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using ams.Common;
namespace ams.Common
{
    class GlobalParams
    {
        /// <summary>
        /// 参数
        /// </summary>
        public static List<tbCommCode> CommCode;
        /// <summary>
        /// 消费项
        /// </summary>
        public static List<tbGoods> Goods;
        /// <summary>
        /// 会员级别与消费项
        /// </summary>
        public static List<tbGoodsRate> GoodsRate;
        /// <summary>
        /// 充值优惠参数
        /// </summary>
        public static List<tbFillProm> FillProm;
        /// <summary>
        /// 操作员列表
        /// </summary>
        public static List<tbOper> Opers;
        /// <summary>
        /// 角色
        /// </summary>
        public static List<tbLimit> Limit;
        /// <summary>
        /// 权限
        /// </summary>
        public static List<tbOperLimit> OperLimit;
        /// <summary>
        /// 打折优惠级别
        /// </summary>
        public static List<tbOperLevel> OperLevel;
        /// <summary>
        /// 当前操作员
        /// </summary>
        public static tbOper oper;
        /// <summary>
        /// 当前班次
        /// </summary>
        public static string strClass;
        public static string strInputBoxMes;
        /// <summary>
        /// 班次
        /// </summary>
        public static string[] strClasses = { "早班", "中班", "晚班" };
        /// <summary>
        /// 性别
        /// </summary>
        public static string[] strSexes = { "男", "女"};
        public GlobalParams(AMSEntities amsContext)
        {
            CommCode = (from cc in amsContext.tbCommCode orderby cc.vcCommSign,cc.vcCommCode select cc).ToList<tbCommCode>(); 
            Goods = (from gs in amsContext.tbGoods orderby gs.vcGoodsCode select gs).ToList<tbGoods>();
            Opers = (from o in amsContext.tbOper orderby o.vcLimit, o.vcOperName select o).ToList<tbOper>();//where o.iFlag==0
            Limit = (from l in amsContext.tbLimit orderby l.vcLimitCode select l).ToList<tbLimit>();

            GoodsRate = (from gr in amsContext.tbGoodsRate orderby gr.vcGoodsCode, gr.vcAssLevel select gr).ToList<tbGoodsRate>();
            FillProm = (from fp in amsContext.tbFillProm orderby fp.iFloor select fp).ToList<tbFillProm>();
            OperLimit = (from ol in amsContext.tbOperLimit orderby ol.vcLimitCode, ol.vcMenu1, ol.vcMenu2 select ol).ToList<tbOperLimit>();
            OperLevel = (from ol in amsContext.tbOperLevel orderby ol.vcOperLevel select ol).ToList<tbOperLevel>();
        }
    }
}
