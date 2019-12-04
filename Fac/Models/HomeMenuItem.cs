using System;
using System.Collections.Generic;
using System.Text;

namespace Fac.Models
{
    public enum MenuType
    {
        Cases,
        About,
        //UnreadCases,
        Login
    }
    public class HomeMenuItem
    {
        public MenuType Id { get; set; }

        public string Title { get; set; }
    }
}
