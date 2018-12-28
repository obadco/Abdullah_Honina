using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;

namespace VlcWithWpf
{
    class Bind_Items
    {

        public Bind_Items() { }
        
        //Function to get the values required to fill the list viwe
        public ListView Bind(ListView lstvw,DataTable dt,string ColumnName)
        {  
            //Cleare the items if any in the sent list view
             lstvw.Items.Clear();
           
            //Assign the data of the sent column name to the sent list view.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Get the items of data table one by on
                DataRow dr = dt.Rows[i];

                
              
                
               
                //Add the current data row into the list box
                lstvw.Items.Add(dr[ColumnName].ToString());
                

                 

            }
            //End of for loop

            return lstvw;
        }//End of the function


        public List<string> Bind(List<string> lstLine, DataTable dt, string ColumnName)
        {
            //Cleare the items if any in the sent list view
            lstLine.Clear();

            //Assign the data of the sent column name to the sent list view.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Get the items of data table one by on
                DataRow dr = dt.Rows[i];



                
                //Add the current data row into the list box
                lstLine.Add(dr[ColumnName].ToString());




            }
            //End of for loop

            return lstLine;
        }//End of the function

        //Function to get the values required to fill the list viwe
        public ListBox Bind(ListBox lstvw, DataTable dt, string ColumnName)
        {
            //Cleare the items if any in the sent list box
            lstvw.Items.Clear();
            //Assign the data of the sent column name to the sent list box.
            for (int i = 0; i < dt.Rows.Count; i++)
            {  //Get the items of data table one by on
                DataRow dr = dt.Rows[i];
                //Add the current data row into the list box
                
                   
            }
            //End of for loop

            return lstvw;
        }//End of the function
    }//End of the class
}
