using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IBenefitRepo<T>
    {
        T AddBenefit(T benefit);
        T UpdateBenefit(T benefit);
        T DeleteBenefit(T benefit);
        List<T> GetAllBenefits();
    }
}