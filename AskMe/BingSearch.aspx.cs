using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AskMe
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           // var objBing = new Bing.BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/Search"));
            //Replace this Account Key with you Account Key
            var accountKey = "EMdPzpSl/qTbvz6/ScfE5sFLzuxNfU/crNSmIYjQDes=";
            //Passing the Credintial
           // objBing.Credentials = new System.Net.NetworkCredential(accountKey, accountKey);
            //Following Line is used to get the Search Result as DataSource.
           // var webResult = objBing.Web(txtSearch.Text.ToString(), null, null, null, null, null, null, null,10,1);
            //Binding the Resultant DataSource to the GridView
            //GridViewSearchResult.DataSource = webResult;
            GridViewSearchResult.DataBind();
        }
    }
}