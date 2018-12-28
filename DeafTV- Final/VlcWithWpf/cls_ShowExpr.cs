using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace VlcWithWpf
{
    class cls_ShowExpr
    {
       
        //Call the data access layer cls to connect with the database
        public DataTable ShowAll()
        {
            cls_DataAccessLayer DAL = new cls_DataAccessLayer();
            
             DataTable dt = new DataTable();
             dt = DAL.SelectData("Show_All_Expr", null);
            return dt;

        }


        public DataTable ShowAll_words()
        {
            cls_DataAccessLayer DAL = new cls_DataAccessLayer();
            DataTable dt = new DataTable();
            dt = DAL.SelectData("Show_All_words", null);
            return dt;

        }

        public DataTable ShowAll_sentences()
        {
            cls_DataAccessLayer DAL = new cls_DataAccessLayer();
            DataTable dt = new DataTable();
            dt = DAL.SelectData("Show_All_sentences", null);
            return dt;

        }

    }
}
