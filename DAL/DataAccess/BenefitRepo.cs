using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public class BenefitRepo : IBenefitRepo<Benefit>
    {
        public Benefit AddBenefit(Benefit benefit)
        {
            using (var dbContext = new EasypayContext())
            {
                dbContext.Benefits.Add(benefit);
                dbContext.SaveChanges();
                return benefit;
            }
        }

        public Benefit UpdateBenefit(Benefit benefit)
        {
            using (var dbContext = new EasypayContext())
            {
                var existing = dbContext.Benefits.FirstOrDefault(b => b.BenefitId == benefit.BenefitId);
                if (existing != null)
                {
                    existing.Type = benefit.Type;
                    dbContext.SaveChanges();
                }
                return existing;
            }
        }

        public Benefit DeleteBenefit(Benefit benefit)
        {
            using (var dbContext = new EasypayContext())
            {
                var existing = dbContext.Benefits.FirstOrDefault(b => b.BenefitId == benefit.BenefitId);
                if (existing != null)
                {
                    dbContext.Benefits.Remove(existing);
                    dbContext.SaveChanges();
                }
                return existing;
            }
        }

        public List<Benefit> GetAllBenefits()
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.Benefits.ToList();
            }
        }
    }
}