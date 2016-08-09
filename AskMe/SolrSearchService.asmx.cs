using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace AskMe
{
    /// <summary>
    /// Summary description for SolrSearchService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SolrSearchService : System.Web.Services.WebService
    {

        [WebMethod(Description = "Query Solr server and return XML response")]
        public XmlDocument QuerySolr(string keyword, string notKeyword, string exact, string rows, string filter,
            string fuzzy, string highlight)
        {


            /*  Default values should the user not enter anything */
            if (rows.Equals(""))
                rows = "50";

            if (keyword.Equals("") && notKeyword.Equals("") && exact.Equals(""))
                keyword = "*";

            if (filter.Equals(""))
                filter = "id,title";

            if (!exact.Equals(""))
                exact = " \"" + exact + "\"";

            if (!highlight.Equals(""))
                highlight = "&hl=true&hl.fl=content";

            /* Should the user require that a certain word or words are not present */
            if (!notKeyword.Equals(""))
            {

                /* Split each word */
                string[] nwords = notKeyword.Split(null);
                notKeyword = "";

                /* Loops and adds the hyphen to each word */
                for (int i = 0; i < nwords.Length; i++)
                {
                    notKeyword = notKeyword + "-" + nwords[i] + " ";
                }
                notKeyword = notKeyword.Substring(0, notKeyword.Length - 1);

            }

            /* Should the user enable a fuzzy search a tilda must be added on to the end of each word */
            if (fuzzy.Equals("true") && !keyword.Equals(""))
            {

                /* Splits the words by whitespace */
                string[] words = keyword.Split(null);
                keyword = "";

                /* Loops and adds the tilda to each word */
                for (int i = 0; i < words.Length; i++)
                {
                    keyword = keyword + words[i] + "~ ";
                }
                keyword = keyword.Substring(0, keyword.Length - 1);

            }

            /* Will throw an error should it have problems connecting to the Solr server */
            try
            {

                /* Connecting to the Solr server and querying it with the search criteria that the user has provided*/
                WebClient client = new WebClient();
                string text = client.DownloadString(
                    "http://localhost:8983/solr/collection1/select?q=url:(" + keyword + " " + notKeyword + exact + ")&text:(" + keyword
                    + " " + notKeyword + exact + ")&fl=" + filter + "&rows=" + rows + highlight);

                /* The response is returned as a string so it must turn it into an XML document */
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(text.Replace("%20", " ").Replace("\\", "/"));   //Changes to windows style filepath

                return xml;

            }
            catch (Exception)
            {
                /* Should an exception occur it returns an XML document with details of the problem */
                XmlDocument xmle = new XmlDocument();
                xmle.LoadXml("<Problem>Solr server cannot be reached</Problem>");

                return xmle;
            }
        }
    }
}
