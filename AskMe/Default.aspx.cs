using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AskMe
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //GlobalClass.ImportantData = txtSearchCopy.Text.Trim();
            try
            {
                
                //Mapping URL and Rank
                if (!IsPostBack)
                {

                    //loading DocID and URL
                   GlobalClass.dtURLDocID = new DataTable();
                    DataColumn dcURL = new DataColumn("URL");
                    GlobalClass.dtURLDocID.Columns.Add(dcURL);
                    DataColumn dcDocID = new DataColumn("Doc ID");
                    GlobalClass.dtURLDocID.Columns.Add(dcDocID);
                    GlobalClass.dtURLDocID.Columns[0].DataType = typeof(String);
                    GlobalClass.dtURLDocID.Columns[1].DataType = typeof(Int32);
                    if (File.Exists(Server.MapPath(@"~/App_Data/docIDURL.txt")))
                    {
                        //reading from text file method 1
                        string line = "";
                        using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/docIDURL.txt")))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] urlDocID = line.Split(' ');
                                DataRow dr = GlobalClass.dtURLDocID.NewRow();
                                dr[0] = urlDocID[0].Trim();
                                dr[1] = urlDocID[1];
                                GlobalClass.dtURLDocID.Rows.Add(dr);
                            }
                        }

                        //reading from textfile method 2
                        //string[] urlDocIDAll = File.ReadAllLines(Server.MapPath(@"~/App_Data/docIDURL.txt"));
                        //foreach (string item in urlDocIDAll)
                        //{
                        //    string[] urlDocID = item.Split(' ');
                        //    DataRow dr = GlobalClass.dtURLDocID.NewRow();
                        //    dr[0] = urlDocID[0].Trim();
                        //    dr[1] = urlDocID[1];
                        //    GlobalClass.dtURLDocID.Rows.Add(dr);
                        //    //GlobalClass.dicURLDocID.Add(urlDocID[0].Trim(), Convert.ToInt32(urlDocID[1]));
                        //}
                    }

                    //loading DocID and Rank for normal Page rank
                    //DataTable dtDocIDRank = new DataTable();
                    //DataColumn dcDocId = new DataColumn("Doc ID");
                    //dtDocIDRank.Columns.Add(dcDocId);
                    //DataColumn dcRank = new DataColumn("Rank");
                    //dtDocIDRank.Columns.Add(dcRank);
                    //dtDocIDRank.Columns[0].DataType = typeof(Int32);
                    //dtDocIDRank.Columns[1].DataType = typeof(Double);
                    //if (File.Exists(Server.MapPath(@"~/App_Data/examPageRank.txt")))
                    //{
                    //    string[] DocIDRankAll = File.ReadAllLines(Server.MapPath(@"~/App_Data/examPageRank.txt"));
                    //    foreach (string item in DocIDRankAll)
                    //    {
                    //        string[] DocIDRank = item.Split(' ');
                    //        DataRow dr = dtDocIDRank.NewRow();
                    //        dr[0] = DocIDRank[0];
                    //        dr[1] = DocIDRank[1];
                    //        dtDocIDRank.Rows.Add(dr);
                    //        if (!GlobalClass.dicDocIDRank.ContainsKey(Convert.ToInt32(DocIDRank[0])))
                    //            GlobalClass.dicDocIDRank.Add(Convert.ToInt32(DocIDRank[0]), Convert.ToDouble(DocIDRank[1]));
                    //    }
                    //}

                    //loading DocID and Rank for Asia rank
                    //DataTable dtDocIDRankAsia = new DataTable();
                    //DataColumn dcDocIdAsia = new DataColumn("Doc ID");
                    //dtDocIDRankAsia.Columns.Add(dcDocIdAsia);
                    //DataColumn dcRankAsia = new DataColumn("Rank");
                    //dtDocIDRankAsia.Columns.Add(dcRankAsia);
                    //dtDocIDRankAsia.Columns[0].DataType = typeof(Int32);
                    //dtDocIDRankAsia.Columns[1].DataType = typeof(Double);
                    //if (File.Exists(Server.MapPath(@"~/App_Data/AsiaexamPageRank.txt")))
                    //{
                    //    string[] DocIDRankAllAsia = File.ReadAllLines(Server.MapPath(@"~/App_Data/AsiaexamPageRank.txt"));
                    //    foreach (string item in DocIDRankAllAsia)
                    //    {
                    //        string[] DocIDRank = item.Split(' ');
                    //        DataRow dr = dtDocIDRank.NewRow();
                    //        dr[0] = DocIDRank[0];
                    //        dr[1] = DocIDRank[1];
                    //        dtDocIDRank.Rows.Add(dr);
                    //        if (!GlobalClass.dicDocIDRankAsia.ContainsKey(Convert.ToInt32(DocIDRank[0])))
                    //            GlobalClass.dicDocIDRankAsia.Add(Convert.ToInt32(DocIDRank[0]), Convert.ToDouble(DocIDRank[1]));
                    //    }
                    //}

                    ////mapping URL and Rank and storing them into a dictionary
                    //for (int i = 0; i < dtURLDocID.Rows.Count; i++)
                    //{
                    //    if (!GlobalClass.dicURLRank.ContainsKey(dtURLDocID.Rows[i][0].ToString()))
                    //    {
                    //        GlobalClass.dicURLRank.Add(dtURLDocID.Rows[i][0].ToString(), GlobalClass.dicDocIDRank[Convert.ToInt32(dtURLDocID.Rows[i][1])]);
                    //    }
                    //}
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.StackTrace);
            }
            
        }
    }
}