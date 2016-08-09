using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Services.Client;
//using Bing;
using System.Net;
using System.Xml;
using System.Collections;
using Bing;

namespace AskMe
{
    public partial class BingSearch1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                var objBing = new Bing.BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/SearchWeb/v1/"));
              //  Replace this Account Key with you Account Key
                string accountKey = "F2eb8VoYAnZpeOjx27RkVOAL8SfzSzPiR4uBAx9eRS4";
                // define page size and offset (for paging)
                const int resultsPerPage = 10;
                int offset =1 ; //first page
               // Passing the Credintial
                objBing.Credentials = new System.Net.NetworkCredential(accountKey, accountKey);
               // Following Line is used to get the Search Result as DataSource.B
                DataServiceQuery<WebResult> webResult = objBing.Web(txtSearch.Text.ToString(), null, null, null, null, null, null, null, resultsPerPage, offset);
               
                IEnumerable<WebResult> results = webResult.Execute();
               // Binding the Resultant DataSource to the GridView
                GridViewSearchResult.DataSource = results;
                GridViewSearchResult.DataBind();

              
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}