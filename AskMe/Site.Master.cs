using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


namespace AskMe
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        //DataTable dtDocIDRank;


        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }
            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GlobalClass.ImportantData = q.Text.Trim();
            //dtDocIDRank = new DataTable();
            //DataColumn dcDocId = new DataColumn("Doc ID");
            //dtDocIDRank.Columns.Add(dcDocId);
            //DataColumn dcRank = new DataColumn("Rank");
            //dtDocIDRank.Columns.Add(dcRank);
            //dtDocIDRank.Columns[0].DataType = typeof(Int32);
            //dtDocIDRank.Columns[1].DataType = typeof(Double);
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //store the dropdown selected value in the global class
            GlobalClass.ddlRankSelected = ddlRanking.SelectedItem.Text;
            GlobalClass.ddlClusterSelected = ddlClustering.SelectedItem.Text;

            string query = q.Text.Trim();
           //checkin for stop words
            String[] querySplit = query.Split(' ');
            for (int i = 0; i < querySplit.Length;i++ )
            {
                if(GlobalClass.stopWordsList.Contains(querySplit[i]))
                {
                    query=query.Replace(querySplit[i],"");
                }
            }
            GlobalClass.ImportantData = query;
      

            //load rank according to the option chosen
            if (ddlRanking.SelectedItem.Text.ToLower() == "Normal".ToLower())
            {
                GlobalClass.dicDocIDRank.Clear();
               
                if (File.Exists(Server.MapPath(@"~/App_Data/examPageRank.txt")))
                {
                    //reading from text file method 1
                    string line = "";
                    using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/examPageRank.txt")))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] DocIDRank = line.Split(' ');
                         
                                if (!GlobalClass.dicDocIDRank.ContainsKey(Convert.ToInt32(DocIDRank[0])))
                                   GlobalClass.dicDocIDRank.Add(Convert.ToInt32(DocIDRank[0]), Convert.ToDouble(DocIDRank[1]));
                        }
                    }
                   
                }
                
            }
            else if (ddlRanking.SelectedItem.Text.ToLower() == "Asia".ToLower())
            {
                GlobalClass.dicDocIDRank.Clear();
               
                if (File.Exists(Server.MapPath(@"~/App_Data/AsiaexamPageRank.txt")))
                {
                    //reading from text file method 1
                    string line = "";
                    using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/AsiaexamPageRank.txt")))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] DocIDRank = line.Split(' ');
                       
                                if (!GlobalClass.dicDocIDRank.ContainsKey(Convert.ToInt32(DocIDRank[0])))
                                    GlobalClass.dicDocIDRank.Add(Convert.ToInt32(DocIDRank[0]), Convert.ToDouble(DocIDRank[1]));
                        }
                    }

                  
                }
            }
            else if (ddlRanking.SelectedItem.Text.ToLower() == "North America".ToLower())
            {
                GlobalClass.dicDocIDRank.Clear();
               
                if (File.Exists(Server.MapPath(@"~/App_Data/North_AmericaexamPageRank.txt")))
                {
                    //reading from text file method 1
                    string line = "";
                    using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/North_AmericaexamPageRank.txt")))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] DocIDRank = line.Split(' ');
                        
                                if (!GlobalClass.dicDocIDRank.ContainsKey(Convert.ToInt32(DocIDRank[0])))
                                    GlobalClass.dicDocIDRank.Add(Convert.ToInt32(DocIDRank[0]), Convert.ToDouble(DocIDRank[1]));
                        }
                    }

                 
                }
            }

            //mapping URL and Rank and storing them into a dictionary
            if (ddlRanking.SelectedItem.Text.ToLower() != "Default".ToLower())
            {
                if (GlobalClass.dicURLRank.Count == 0)
                {
                    //GlobalClass.dicDocIDRank.Clear();
                    for (int i = 0; i < GlobalClass.dtURLDocID.Rows.Count; i++)
                    {
                        if (!GlobalClass.dicURLRank.ContainsKey(GlobalClass.dtURLDocID.Rows[i][0].ToString()))
                        {
                            GlobalClass.dicURLRank.Add(GlobalClass.dtURLDocID.Rows[i][0].ToString(), GlobalClass.dicDocIDRank[Convert.ToInt32(GlobalClass.dtURLDocID.Rows[i][1])]);
                        }
                    }
                }
            }
           
            //load data into cluster dictionary according to the dropdown value selected
            if (ddlClustering.SelectedItem.Text.Trim().ToLower() != "No".ToLower())
            {
                GlobalClass.dicURLClusterID.Clear();

                if (ddlClustering.SelectedItem.Text.ToLower() == "KMeans".ToLower())
                {
                    if (File.Exists(Server.MapPath(@"~/App_Data/kmeansOutput.csv")))
                    {
                        //reading from text file method 1
                        string line = "";
                        using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/kmeansOutput.csv")))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] URLClusterID = line.Split(',');

                                if (!GlobalClass.dicURLClusterID.ContainsKey((URLClusterID[0])))
                                    GlobalClass.dicURLClusterID.Add((URLClusterID[0]), (URLClusterID[1]));
                            }
                        }
                    }
                }
                else if (ddlClustering.SelectedItem.Text.ToLower() == "SingleLink".ToLower())
                {
                    GlobalClass.dicURLClusterID.Clear();
                    if (File.Exists(Server.MapPath(@"~/App_Data/single.csv")))
                    {
                        //reading from text file method 1
                        string line = "";
                        using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/single.csv")))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] URLClusterID = line.Split(',');

                                if (!GlobalClass.dicURLClusterID.ContainsKey((URLClusterID[0])))
                                    GlobalClass.dicURLClusterID.Add((URLClusterID[0]), (URLClusterID[1]));
                            }
                        }
                    }
                }
                else if (ddlClustering.SelectedItem.Text.ToLower() == "CompleteLink".ToLower())
                {
                    GlobalClass.dicURLClusterID.Clear();
                    if (File.Exists(Server.MapPath(@"~/App_Data/complete.csv")))
                    {
                        //reading from text file method 1
                        string line = "";
                        using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/complete.csv")))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] URLClusterID = line.Split(',');

                                if (!GlobalClass.dicURLClusterID.ContainsKey((URLClusterID[0])))
                                    GlobalClass.dicURLClusterID.Add((URLClusterID[0]), (URLClusterID[1]));
                            }
                        }
                    }
                }
            }
            //calling google script to display results
            Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "SiteSearch();", true);
        }

        protected void q_TextChanged(object sender, EventArgs e)
        {
           
           // GlobalClass.ImportantData = q.Text.Trim();
           
        }

        protected void ddlRanking_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

      
    }

}