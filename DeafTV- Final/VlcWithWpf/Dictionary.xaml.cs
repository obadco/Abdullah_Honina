using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Ribbon;
using System.Data;
using System.Windows.Forms;


using System.IO;

namespace VlcWithWpf
{
    /// <summary>
    /// Interaction logic for Dictionary.xaml
    /// </summary>
    public partial class Dictionary 
    {
        public Dictionary()
        {
            InitializeComponent();
            btnPlay.Visibility = System.Windows.Visibility.Hidden;
            dt_sign=clsShowExpr.ShowAll();
            dt_sug = clsShowExpr.ShowAll_sentences();
           
            lstbxEngExpr = bindItems.Bind(lstbxEngExpr, dt_sign, "Expr");
            lstbxArbExpr = bindItems.Bind(lstbxArbExpr, dt_sign, "Arb_trnsl");


            lstSugEng = bindItems.Bind(lstSugEng, dt_sug, "Expr");
            lstSugArb = bindItems.Bind(lstSugArb, dt_sug, "Arb_trnsl");

           
           
             //lstbxEngExpr.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Content", System.ComponentModel.ListSortDirection.Ascending));
           
        }

        Bind_Items bindItems = new Bind_Items();
        cls_searchSigns clsSearch = new cls_searchSigns();
       cls_ShowExpr clsShowExpr = new cls_ShowExpr();
        DataTable dt_sign = new DataTable();
        DataTable dt_sug = new DataTable();
        string videoPath = "";
     
        private void txtSrch_TextChanged(object sender, TextChangedEventArgs e)
        {
          
            //Search for a text that a user wants
              dt_sign = clsSearch.SearchSign(txtSrch.Text);
            //Fill the list by the corresponding searched words 
               lstbxArbExpr = bindItems.Bind(lstbxArbExpr, dt_sign, "Arb_trnsl");
               lstbxEngExpr = bindItems.Bind(lstbxEngExpr, dt_sign, "Expr");

               lstSugEng = bindItems.Bind(lstSugEng, dt_sug, "Expr");
               lstSugArb = bindItems.Bind(lstSugArb, dt_sug, "Arb_trnsl");
           
           
       }
       

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //If the user clicks to show the video then checks if he search for a word or not
           /* if (txtSrch.Text != "")
            {
                //If the word exists in the database then show the appropriate sign video
                videoPath = dt_sign.Rows[0]["Sign_video_path"].ToString();
                mElmDic.Source = new Uri(dt_sign.Rows[0]["Sign_video_path"].ToString());
                //Play the video 
                mElmDic.LoadedBehavior = MediaState.Play;
            } */
          

           
           
            
        }

        private void lstbxEngExpr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {




            if (lstbxEngExpr.SelectedItem != null)
            {
                DataTable srch_dt = new DataTable();
                srch_dt = clsSearch.SearchSign(lstbxEngExpr.SelectedItem.ToString());

                //If the word exists in the database then show the appropriate sign video
                videoPath = srch_dt.Rows[0]["Sign_video_path"].ToString();
                 mElmDic.Source = new Uri(videoPath);
                //Play the video 
                mElmDic.LoadedBehavior = MediaState.Play;
               // lblPath.Text = srch_dt.Rows[0]["Sign_video_path"].ToString();
            }
           
            
           
           
           // mElmDic.Source = new Uri(@"C:\Users\Murtada Al-Junaid\Documents\Visual Studio 2013\Projects\Deaf T.V (V1) - Copy\Deaf T.V (V1)\bin\Debug\" + dt_sign.Rows[0]["Sign_video_path"].ToString());

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if ( videoPath != " ")
            {
               
                //btnSearch_Click(sender, e);
                mElmDic.LoadedBehavior = MediaState.Play;
               btnPlay.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                
                //DataTable srch_dt = clsSearch.SearchSign(lstbxEngExpr.SelectedValue.ToString());
                ////If the word exists in the database then show the appropriate sign video
                //mElmDic.Source = new Uri(srch_dt.Rows[0]["Sign_video_path"].ToString());
                ////Play the video 
                //mElmDic.LoadedBehavior = MediaState.Play;
                
                //lblPath.Text = srch_dt.Rows[0]["Sign_video_path"].ToString();
            }
            
           
        }

        private void Element_MediaEnded(object sender, RoutedEventArgs e)
        {
            btnPlay.Visibility = System.Windows.Visibility.Visible;
            mElmDic.LoadedBehavior = MediaState.Stop;
        }
           

        private void Element_MediaOpened(object sender, RoutedEventArgs e)
        {
            btnPlay.Visibility = System.Windows.Visibility.Hidden;

        }

       


        private void lstbxArbExpr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lblPath.Text = lstbxArbExpr.SelectedItem.ToString();

            if (lstbxArbExpr.SelectedItem != null)
            {
                DataTable srch_dt = new DataTable();
                srch_dt = clsSearch.SearchSign(lstbxArbExpr.SelectedItem.ToString());

                //If the word exists in the database then show the appropriate sign video
                videoPath = srch_dt.Rows[0]["Sign_video_path"].ToString();
                mElmDic.Source = new Uri(videoPath);
                //Play the video 
                mElmDic.LoadedBehavior = MediaState.Play;
                //lblPath.Text = srch_dt.Rows[0]["Sign_video_path"].ToString();
            }
           
        }


      

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            


           //mElmDic.Source = new Uri(@"C:\Users\Murtada Al-Junaid\Desktop\Yahia.avi");
            ////Play the video 
            //mElmDic.LoadedBehavior = MediaState.Play;
          
        }

      
        private void txtSrch_LayoutUpdated(object sender, EventArgs e)
        {
            

        }

        private void UserControl_LayoutUpdated(object sender, EventArgs e)
        {

            
            /*   if ( (InputLanguage.CurrentInputLanguage.LayoutName == "Arabic (101)" ) && (Properties.Settings.Default.Language=="English"))
               {
                   txtSrch.TextAlignment = TextAlignment.Right;
                   TCLangs.SelectedIndex = 1;

               }
               else if ((InputLanguage.CurrentInputLanguage.LayoutName == "US") && (Properties.Settings.Default.Language == "English"))
               {
                   txtSrch.TextAlignment = TextAlignment.Left;
                   TCLangs.SelectedIndex = 0;


               }
               else if ((InputLanguage.CurrentInputLanguage.LayoutName == "US") && (Properties.Settings.Default.Language == "Arabic"))
                {
                   txtSrch.TextAlignment = TextAlignment.Right;
                   TCLangs.SelectedIndex = 0;
                    tbEng.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                    

               }
                else if ((InputLanguage.CurrentInputLanguage.LayoutName == "Arabic (101)") && (Properties.Settings.Default.Language == "Arabic"))
               {
               txtSrch.TextAlignment = TextAlignment.Left;
                  TCLangs.SelectedIndex = 1;
                   tbArb.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                   
               }
           */

            // Display progress.
        }

        private void TCLangs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( tbEng.IsSelected)
            {
                TCLangs.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                TcSuggessions.SelectedIndex = 0;
            }




            else 
            {
                tbArb.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                TcSuggessions.SelectedIndex = 1;
            }
              
          
            
          

        }

      
        private void txtSrch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (InputLanguage.CurrentInputLanguage.LayoutName == "US")
            {
                txtSrch.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                
            }



            else 
            {
                txtSrch.FlowDirection = System.Windows.FlowDirection.RightToLeft;

               
            }

        }

        private void lstSug_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSugEng.SelectedItem != null)
            {
                DataTable srch_sentences_dt = new DataTable();
                srch_sentences_dt = clsSearch.SearchSign(srch_sentences_dt.ToString());

                //If the word exists in the database then show the appropriate sign video
                videoPath = srch_sentences_dt.Rows[0]["Sign_video_path"].ToString();
                mElmDic.Source = new Uri(videoPath);
                //Play the video 
                mElmDic.LoadedBehavior = MediaState.Play;
                //lblPath.Text = srch_dt.Rows[0]["Sign_video_path"].ToString();
            }
        }

       
        
       
    }
}
