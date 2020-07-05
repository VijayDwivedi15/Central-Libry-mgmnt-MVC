using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.DAL
{
    public class Global
    {
        public static string result = String.Empty;

 
        public class Page
        {
            public int PageSize { get; set; }
        }
        public List<Page> GetPaging()
        {
            List<Page> PageList = new List<Page>();
            PageList.Add(new Page { PageSize = 1 });
            PageList.Add(new Page { PageSize = 5 });
            PageList.Add(new Page { PageSize = 10 });
            PageList.Add(new Page { PageSize = 20 });
            PageList.Add(new Page { PageSize = 50 });
            PageList.Add(new Page { PageSize = 100 });

            return PageList;
        }
        //for task priority
        public class Priority
        {

            public string PName { get; set; }
        }

        public static List<Priority> GetPriority()
        {
            List<Priority> Prioritylist = new List<Priority>();
            Prioritylist.Add(new Priority { PName = "Highest Priority" });
            Prioritylist.Add(new Priority { PName = "High Priority" });
            Prioritylist.Add(new Priority { PName = "Normal Priority" });
            Prioritylist.Add(new Priority { PName = "Low Priority" });

            return Prioritylist;
        }
        //end
    }
}