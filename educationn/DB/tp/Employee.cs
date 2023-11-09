using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace educationn.DB
{
    public partial class Employee
    {
        public bool IsChief
        {

            get
            {
                if (Chief == Tab_number)
                {
                    return true;
                }
                else { return false; }
            }
        }
    }
}
