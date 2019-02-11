using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshAir.Views
{

    public class LandingPageMenuItem
    {
        public LandingPageMenuItem()
        {
            TargetType = typeof(LandingPageDetail);
        }
        public LandingPageMenuItem(int type)
        {
            switch (type)
            {
                case 1:
                    TargetType = typeof(Settings);
                    break;
            }
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}