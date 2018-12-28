using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace VlcWithWpf
{
    //Class that is used for set a connection to a database and contains the main methods used to manipulate the data from this connected db
    class cls_DataAccessLayer

    {
        //Initialize connection 
        public SqlConnection sqlDbconn;

        //Constructor to start the connection
        public cls_DataAccessLayer()
        {
                        //The . here means local computer ,with server apps it's changed to the server name
            /* We made the  Integrated Security= true 'cuz the authentication is windows authentication,but in case the authentication is sql server
            * ,then we should change it to false and type the user name and pwd.
            */
            sqlDbconn = new SqlConnection(@"Data Source=Abdullah;Integrated Security=True; Database=Deaf T.V; Integrated Security=true");
        }
        //End of the constructor

        //Method to open the connection
        public void openCon()
        {
            if (sqlDbconn.State != ConnectionState.Open)
            {
                sqlDbconn.Open();

            }//End if

        }//End open method


        //Method to close the connection
        public void closeCon()
        {
            if (sqlDbconn.State == ConnectionState.Open)
            {
                sqlDbconn.Close();
            }//End if
        } //End close Method 

        //Method to read the data from the database
        public DataTable SelectData(string Stored_Precedure, SqlParameter[] param)
        {
            openCon();
            //Instance of the SqlDb command in where the command will be stored and executed
            SqlCommand sqlcmd = new SqlCommand();

            //Set the type of the command to be a stored procedure  since we have three types of commands(txt,strdPrc,DirectTable)
            sqlcmd.CommandType = CommandType.StoredProcedure;

            //Set the command to the txt of the strdPrc E.g(select .... form ....)
            sqlcmd.CommandText = Stored_Precedure;

            //Set the connection of the database to the designate db

            sqlcmd.Connection = sqlDbconn;

            //Then if stm chks whether there is a params with this prc 
            if (param != null)
            {

                //Loop to add the sent params corresponding to the strdPrc params

                //instead of loop we can use the built in func  ( sqlcmd.Parameters.AddRange(param));
                for (int i = 0; i < param.Length; i++)

                    sqlcmd.Parameters.Add(param[i]);
            }//End of If stm


            //SqlDataAdapter to execute the command
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);

            //Then fill the result into a datatable
            DataTable dt = new DataTable();
            da.Fill(dt);
            closeCon();
            return dt;



        }//End of selecting data method

        //Method to execute sent strdPrc from other methods
        public void Execute_command(string Stored_Precedure, SqlParameter[] param)
        {
            openCon();
            //Instance of the SqlDb command in where the command will be stored and executed
            SqlCommand sqlcmd = new SqlCommand();

            //Set the type of the command to be a stored procedure  since we have three types of commands(txt,strdPrc,DirectTable)
            sqlcmd.CommandType = CommandType.StoredProcedure;

            //Set the command to the txt of the strdPrc (insert or delete or whatever)
            sqlcmd.CommandText = Stored_Precedure;


            //Set the connection of the database to the designate db

            sqlcmd.Connection = sqlDbconn;

            //Then if stm chks whether there is a params with this prc like the update,delete and so on  prc almost or it does not 
            if (param != null)
            {

                //use the bulit in func or for loop to add the sent params corresponding to the strdPrc params 

                sqlcmd.Parameters.AddRange(param);

            }//End of If stm

            //Now execute the cmd without returin' any val

            sqlcmd.ExecuteNonQuery();
            closeCon();

        }//End of executing method
    }
}
