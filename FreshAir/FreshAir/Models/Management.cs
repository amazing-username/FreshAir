using System;
using System.Collections.Generic;
using System.Text;

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

        public string UrlBase { set; get; }
    }
}
