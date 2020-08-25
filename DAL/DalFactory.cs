using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalFactory
    {
        public static IDAL instance;

        protected DalFactory() { instance = null; }
        public static IDAL getDal()
        {
            if (null == instance)
               //instance = new d
             instance = new Dal_XML_imp();
            return instance;
        }
    }
}
