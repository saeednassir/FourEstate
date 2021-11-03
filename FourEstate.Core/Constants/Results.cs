using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Core.Constants
{
    public class Results
    {
        public static string GetSuccessResult()
        {
            return "s: تم جلب البيانات بنجاح";
        }
        public static object AddSuccessResult()
        {
            return new { status = 1, msg = "s: تمت اضافة العنصر بنجاح", close = 1 };
        }
        public static object UpdateStatusResult()
        {
            return new { status = 1, msg = "s: تمت تحديث الحالة بنجاح", close = 1 };
        }
        public static object EditSuccessResult()
        {
            return new { status = 1, msg = "s: تم تحديث بيانات العنصر بنجاح ", close = 1 };
        }

        public static object DeleteSuccessResult()
        {
            return new { status = 1, msg = "s: تم حذف العنصر بنجاح", close = 1 };
        }

    
    }
}
