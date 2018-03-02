using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace ams.Common
{
    class ConstApp
    {
        /// <summary>
        /// AF	Awoke
        /// </summary>
        public const string AF = "AF";
        /// <summary>
        /// AGF	AgianFee
        /// </summary>
        public const string AGF = "AGF";
        /// <summary>
        /// Ak	Awoke   
        /// </summary>
        public const string Ak = "Ak";
        /// <summary>
        /// AL	会员级别
        /// </summary>
        public const string AL = "AL";
        /// <summary>
        /// AST	会员状态
        /// </summary>
        public const string AST = "AST";
        /// <summary>
        /// 会员状态 未发卡
        /// </summary>
        public const string AST_0 = "0";
        /// <summary>
        /// 会员状态 已发卡
        /// </summary>
        public const string AST_1 = "1";
        /// <summary>
        /// CST	会员卡状态
        /// </summary>
        public const string CST = "CST";
        /// <summary>
        /// CST	会员卡状态 退卡
        /// </summary>
        public const string CST_0 = "0";
        /// <summary>
        /// CST	会员卡状态 正常在用
        /// </summary>
        public const string CST_1 = "1";
        /// <summary>
        /// CST	会员卡状态 挂失
        /// </summary>
        public const string CST_2 = "2";
        /// <summary>
        /// CST	会员卡状态 挂失换卡
        /// </summary>
        public const string CST_3 = "3";
        /// <summary>
        /// CST	会员卡状态 已回收
        /// </summary>
        public const string CST_4 = "4";
        /// <summary>
        /// EX	积分兑换项目
        /// </summary>
        public const string EX = "EX";
        /// <summary>
        /// FC	费用代码
        /// </summary>
        public const string FC = "FC";
        /// <summary>
        /// IG	积分类型
        /// </summary>
        public const string IG = "IG";
        /// <summary>
        /// OT	操作类型
        /// </summary>
        public const string OT = "OT";
        //交错数组
        public static readonly string[][] strmenu = new string[][] { 
            //菜单级别 上级菜单 菜单CODE 菜单名称
        new string[]{"0","","0","会员管理"}, 
            new string[]{"1","0","0","添加会员"}, 
            new string[]{"1","0","1","修改会员"}, 
            new string[]{"1","0","2","会员卡发卡"}, 
            new string[]{"1","0","3","卡密码修改"},                
            new string[]{"1","0","4","会员卡充值"}, 
            new string[]{"1","0","5","会员卡挂失"},
            new string[]{"1","0","6","会员卡补卡"},
            new string[]{"1","0","7","会员卡解挂"},
            new string[]{"1","0","8","会员卡退卡"},
            new string[]{"1","0","9","会员卡回收"},
        new string[]{"0","","2","会员消费积分"},
            new string[]{"1","2","0","会员消费"},
            new string[]{"1","2","1","帐单返销"},
            new string[]{"1","2","2","会员积分兑换"},
            new string[]{"1","2","3","会员卡帐单重打"},
        new string[]{"0","","3","统计查询"},
            new string[]{"1","3","0","会员资料查询"},
            new string[]{"1","3","1","会员充值查询"},
            new string[]{"1","3","2","会员消费查询"},
            new string[]{"1","3","3","会员积分查询"},
            new string[]{"1","3","4","班次汇总统计"},
            new string[]{"1","3","5","营业日志查询"},
            //new string[]{"1","3","6","结算统计报表"},
        new string[]{"0","","4","系统管理"},
            new string[]{"1","4","0","操作员管理"},
            new string[]{"1","4","1","操作员权限设置"},
            new string[]{"1","4","2","参数设置"}//,
            //new string[]{"1","4","3","卡密码重置"}
        };
    }
    
}