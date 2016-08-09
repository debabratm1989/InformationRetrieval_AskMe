using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bing;
using AskMe.ServiceReference1;
using System.Xml;
using System.Data;
using System.IO;
using System.Collections;

namespace AskMe
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
              

                String searchTxt = "";
                //TextBox txtSearch = this.Master.FindControl("q") as TextBox;
                searchTxt = GlobalClass.ImportantData;
               
                //***********Code fo Bing Search Starts****************************
                var objBing = new Bing.BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/SearchWeb/v1/"));
                //  Replace this Account Key with you Account Key
                string accountKey = "F2eb8VoYAnZpeOjx27RkVOAL8SfzSzPiR4uBAx9eRS4";
                // define page size and offset (for paging)
                const int resultsPerPage = 10;
                int offset = 1; //first page
                // Passing the Credintial
                objBing.Credentials = new System.Net.NetworkCredential(accountKey, accountKey);
                // Following Line is used to get the Search Result as DataSource.B
               DataServiceQuery<WebResult> webResult = objBing.Web(searchTxt, null, null, null, null, null, null, null, resultsPerPage, offset);

                IEnumerable<WebResult> results = webResult.Execute();
                // Binding the Resultant DataSource to the GridView
                GridViewSearchResult.DataSource = results;
                GridViewSearchResult.DataBind();

                //***********Code fo Bing Search ends****************************

                //***********Code fo Solr Search Starts****************************
                SolrSearchService srvSolr = new SolrSearchService();
                XmlDocument xmlResult = new XmlDocument();
                xmlResult = srvSolr.QuerySolr(searchTxt, "", "", "", "", "", "");
                XmlNodeList nodes = xmlResult.DocumentElement.SelectNodes("/response/result/doc");
                DataTable dtSolrSearch = new DataTable();
                dtSolrSearch.Columns.Add("title");
                dtSolrSearch.Columns.Add("id");
                foreach (XmlNode node in nodes)
                {

                    string title = node.SelectSingleNode("str").InnerText;
                    string id = node.InnerText.ToString();
                    DataRow dr = dtSolrSearch.NewRow();
                    if (title.StartsWith("http:") || title.Contains("http:"))
                    {
                        if (GlobalClass.ddlRankSelected.ToLower() == "Default".ToLower())
                        {
                            dr["title"] = id.Replace(title, "");
                            title = title.Substring(title.LastIndexOf("/") + 1).Replace(".html", "");
                            title = "https://en.wikipedia.org/wiki/" + title;
                            dr["id"] = title;
                        }
                        else
                        {
                            dr["id"] = title;
                            dr["title"] = id.Replace(title, "");
                        }
                       
                    }
                    else
                    {
                        if (GlobalClass.ddlRankSelected.ToLower() == "Default".ToLower())
                        {
                            dr["title"] = title;
                            id = id.Replace(title + "http", "http");
                            id = id.Substring(id.LastIndexOf("/") + 1).Replace(".html", "");
                            id = "https://en.wikipedia.org/wiki/" + id;
                            dr["id"] = id;
                        }
                        else
                        {
                            dr["title"] = title;
                            id = id.Replace(title + "http", "http");
                            dr["id"] = id;
                        }
                    }
                    dtSolrSearch.Rows.Add(dr);
 

                }

                //now check for clusters
                  ArrayList arrClusteredURL = new ArrayList();
                if(GlobalClass.ddlRankSelected.ToLower()!="No".ToLower())
                {
                  
                    for(int i=0;i<dtSolrSearch.Rows.Count;i++)
                    {
                        if(GlobalClass.dicURLClusterID.ContainsKey(dtSolrSearch.Rows[i][1].ToString()))
                        {
                            arrClusteredURL.Add(GlobalClass.dicURLClusterID[dtSolrSearch.Rows[i][1].ToString()]);
                        }
                    }
                    if (GlobalClass.ddlClusterSelected.ToLower() == "KMeans".ToLower())
                    {
                       
                        if (arrClusteredURL.Contains("cluster0") && arrClusteredURL.Contains("cluster1")
                            && arrClusteredURL.Contains("cluster2"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/kmeansNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    DataRow dr = dtSolrSearch.NewRow();
                                    dr[0] = clusterIDURL[0].Trim();
                                    dr[1] = clusterIDURL[1];
                                    dtSolrSearch.Rows.Add(dr);
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster0") && arrClusteredURL.Contains("cluster1"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/kmeansNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster0".ToLower() || clusterIDURL[0].ToLower() == "cluster1".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster0") && arrClusteredURL.Contains("cluster2"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/kmeansNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster0".ToLower() || clusterIDURL[0].ToLower() == "cluster2".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster1") && arrClusteredURL.Contains("cluster2"))
                        { //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/kmeansNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster2".ToLower() || clusterIDURL[0].ToLower() == "cluster1".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }

                        }
                        else if (arrClusteredURL.Contains("cluster2"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/kmeansNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster2".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster1"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/kmeansNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster1".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster0"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/kmeansNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster0".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                    else if (GlobalClass.ddlClusterSelected.ToLower() == "SingleLink".ToLower())
                    {
                        if (arrClusteredURL.Contains("cluster0") && arrClusteredURL.Contains("cluster1")
                            && arrClusteredURL.Contains("cluster2"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/singleNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    DataRow dr = dtSolrSearch.NewRow();
                                    dr[0] = clusterIDURL[0].Trim();
                                    dr[1] = clusterIDURL[1];
                                    dtSolrSearch.Rows.Add(dr);
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster0") && arrClusteredURL.Contains("cluster1"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/singleNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster0".ToLower() || clusterIDURL[0].ToLower() == "cluster1".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster0") && arrClusteredURL.Contains("cluster2"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/singleNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster0".ToLower() || clusterIDURL[0].ToLower() == "cluster2".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster1") && arrClusteredURL.Contains("cluster2"))
                        { //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/singleNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster2".ToLower() || clusterIDURL[0].ToLower() == "cluster1".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }

                        }
                        else if (arrClusteredURL.Contains("cluster2"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/singleNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster2".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster1"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/singleNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster1".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster0"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/singleNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster0".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                    else if (GlobalClass.ddlClusterSelected.ToLower() == "CompleteLink".ToLower())
                    {
                        if (arrClusteredURL.Contains("cluster0") && arrClusteredURL.Contains("cluster1")
                           && arrClusteredURL.Contains("cluster2"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/completeNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    DataRow dr = dtSolrSearch.NewRow();
                                    dr[0] = clusterIDURL[0].Trim();
                                    dr[1] = clusterIDURL[1];
                                    dtSolrSearch.Rows.Add(dr);
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster0") && arrClusteredURL.Contains("cluster1"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/completeNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster0".ToLower() || clusterIDURL[0].ToLower() == "cluster1".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster0") && arrClusteredURL.Contains("cluster2"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/completeNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster0".ToLower() || clusterIDURL[0].ToLower() == "cluster2".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster1") && arrClusteredURL.Contains("cluster2"))
                        { //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/completeNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster2".ToLower() || clusterIDURL[0].ToLower() == "cluster1".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }

                        }
                        else if (arrClusteredURL.Contains("cluster2"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/completeNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster2".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster1"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/completeNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster1".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        else if (arrClusteredURL.Contains("cluster0"))
                        {
                            //reading from text file method 1
                            string line = "";
                            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/completeNormalRankOutput.txt")))
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] clusterIDURL = line.Split(' ');
                                    if (clusterIDURL[0].ToLower() == "cluster0".ToLower())
                                    {
                                        DataRow dr = dtSolrSearch.NewRow();
                                        dr[0] = clusterIDURL[0].Trim();
                                        dr[1] = clusterIDURL[1];
                                        dtSolrSearch.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                }

                if (GlobalClass.ddlRankSelected.ToLower() == "Default".ToLower())
                {
                    //now check for clusters
                    gvSolrSearch.DataSource = dtSolrSearch;
                    gvSolrSearch.DataBind();
                    dtSolrSearch.Clear();
                }
                else if (GlobalClass.ddlRankSelected.ToLower() != "Default".ToLower())
                {
                    //*****************Normal PageRank Starts*****************
                    //rearranging the unordered rows 

                    //Map the retrieved URL's to the page rank and store it in another data table
                    DataTable dtPageRankedSolrSearch = new DataTable();
                    DataColumn dcURLFinal = new DataColumn("id");
                    dtPageRankedSolrSearch.Columns.Add(dcURLFinal);
                    DataColumn dcTitle = new DataColumn("title");
                    dtPageRankedSolrSearch.Columns.Add(dcTitle);
                    DataColumn dcRankFinal = new DataColumn("Rank");
                    dtPageRankedSolrSearch.Columns.Add(dcRankFinal);

                    dtPageRankedSolrSearch.Columns[0].DataType = typeof(String);
                    dtPageRankedSolrSearch.Columns[1].DataType = typeof(String);
                    dtPageRankedSolrSearch.Columns[2].DataType = typeof(Double);

                    for (int i = 0; i < dtSolrSearch.Rows.Count; i++)
                    {
                        string title = dtSolrSearch.Rows[i][0].ToString();
                        string id = dtSolrSearch.Rows[i][1].ToString();
                        double pageRank = GlobalClass.dicURLRank[(id)];
                        DataRow dr = dtPageRankedSolrSearch.NewRow();
                        dr["title"] = title;
                        id = id.Substring(id.LastIndexOf("/") + 1).Replace(".html", "");
                        id = "https://en.wikipedia.org/wiki/" + id;
                        dr["id"] = id;
                        dr["Rank"] = pageRank;
                        dtPageRankedSolrSearch.Rows.Add(dr);
                    }

                    //Now sort the datatable as per page rank
                    //dtPageRankedSolrSearch.DefaultView.Sort = "Rank Desc";
                    dtPageRankedSolrSearch.DefaultView.Sort = "Rank Desc";
                    dtPageRankedSolrSearch = dtPageRankedSolrSearch.DefaultView.ToTable();

                    // Binding the Resultant DataSource to the GridView
                    gvSolrSearch.DataSource = dtPageRankedSolrSearch;
                    gvSolrSearch.DataBind();
                    dtPageRankedSolrSearch.Clear();
                }
                else if (GlobalClass.ddlRankSelected.ToLower() == "Asia".ToLower())
                {
                    
                }
                else if (GlobalClass.ddlRankSelected.ToLower() == "North America".ToLower())
                {
                   
                }
                //*****************Normal PageRank ends*****************


                //***********Code fo Solr Search ends****************************
                dtSolrSearch.Clear();
                GlobalClass.dicDocIDRank.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();

            }
            catch (Exception ex)
            {
                Response.Write(ex.StackTrace);
            }
        }
    }
}