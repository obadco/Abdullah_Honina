using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace VlcWithWpf
{
    class cls_searchSigns
    { //Call the data access layer cls to connect with the database
        public DataTable SearchSign(string cretrion)
        {
            cls_DataAccessLayer DAL = new cls_DataAccessLayer();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@criterion", SqlDbType.VarChar, 20);
            param[0].Value = cretrion;
           
           

            DataTable dt = new DataTable();
            dt = DAL.SelectData("Search_for_Sign", param);
           
            return dt;

        }


        public DataTable RetrieveSign(string cretrion)
        {
            cls_DataAccessLayer DAL = new cls_DataAccessLayer();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@criterion", SqlDbType.VarChar, 20);
            param[0].Value = cretrion;



            DataTable dt = new DataTable();
            dt = DAL.SelectData("retrieve_Sign", param);

            return dt;

        }






    }
}
