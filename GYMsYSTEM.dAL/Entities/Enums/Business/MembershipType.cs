using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities.Enums.Business
{
    public enum MembershipType
    {
        //1,2,3=>G
        Gold_1Month,  //Gold_1Month يعني شتراك ذهبي لمدة شهر واحد.
        Gold_3Months,
        Gold_6Months,
        //4,5,6=>P
        Platinum_1Month, // Platinum_3Months يعني شتراك بلاتيني لمدة 3 أشهر.
        Platinum_3Months,
        Platinum_6Months,
        //7,8,9=>S (Silver)
        Silver_1Month,
        Silver_3Months,
        Silver_6Months,
        //10,11,12=>B (Bronze)
        Bronze_1Month,
        Bronze_3Months,
        Bronze_6Months,
        //13,14,15=>D (Diamond)
        Diamond_1Month,
        Diamond_3Months,
        Diamond_6Months
    }
}
