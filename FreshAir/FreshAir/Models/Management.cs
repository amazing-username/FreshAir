using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;

namespace FreshAir.Models
{
    public class Management
    {
        public Management()
        {
            Initialize();
        }


        protected void Initialize()
        {
            UrlBase = "http://50.88.81.55:8024/";
        }

        
        protected RestClient Client { set; get; }
        protected RestRequest Request { set; get; }
        protected IRestResponse Response { set; get; }
        protected string UrlBase { set; get; }
    }
}
