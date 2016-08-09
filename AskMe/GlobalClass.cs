using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AskMe
{
    public static class GlobalClass
    {
        static string _importantData;

        /// <summary>
        /// Get or set the static important data.
        /// </summary>
        public static string ImportantData
        {
            get
            {
                return _importantData;
            }
            set
            {
                _importantData = value;
            }
        }

        //flags to indicate which dropdown item has been selected
        public static string ddlRankSelected = "Default";
        public static string ddlClusterSelected = "No";



        //dictionary to store the doc ID and URL
      // public static Dictionary<String, Int32> dicURLDocID = new Dictionary<String, Int32>();

        //dictionary to store the doc ID and Rank
       public static Dictionary<Int32, Double> dicDocIDRank = new Dictionary<Int32, Double>(1200000);

       //dictionary to store the doc ID and Rank
      // public static Dictionary<Int32, Double> dicDocIDRankAsia = new Dictionary<Int32, Double>(1200000);

       //dictionary to store the doc ID and Rank
       //public static Dictionary<Int32, Double> dicDocIDRankAmerica = new Dictionary<Int32, Double>(1200000);

       //dictionary to store the URL and Rank
       public static Dictionary<String, Double> dicURLRank = new Dictionary<String, Double>(1200000);

       //dictionary to store the URL and Rank
       public static Dictionary<String, String> dicURLClusterID = new Dictionary<String, String>(80000);

       //dictionary to store the URL and Rank
       //public static Dictionary<String, Double> dicURLRankAsia = new Dictionary<String, Double>(1200000);

       //dictionary to store the URL and Rank
       //public static Dictionary<String, Double> dicURLRankAmerica = new Dictionary<String, Double>(1200000);


       public static DataTable dtURLDocID;

       //you can defined other stop word list here
       public static ArrayList stopWordsList = new ArrayList { "a","all","an","and",
			"any",
			"are",
			"as",
			"be",
			"been",
			"but",
			"by",
			"few",
			"for",
			"have",
			"he",
			"her",
			"here",
			"him",
			"his",
			"how",
			"i",
			"in",
			"is",
			"it",
			"its",
			"many",
			"me",
			"my",
			"none",
			"of",
			"on", 
			"or",
			"our",
			"she",
			"some",
			"the",
			"their",
			"them",
			"there",
			"they",
			"that", 
			"this",
			"us",
			"was",
			"what",
			"when",
			"where",
			"which",
			"who",
			"why",
			"will",
			"with",
			"you",
			"your"};
    }
}