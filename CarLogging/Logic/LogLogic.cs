using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CarLogging.Concrete;
using CarLogging.Models;
//using Log = CarLogging.Entities.Log;
using CarLogging = CarLogging.Models.LogViewModel;

namespace CarLogging.Logic
{
    public class LogLogic
    {
        private EFDbContext Context;

        public LogLogic()
        {
           Context = new EFDbContext();
        }
        public IQueryable<LogViewModel> GetLog(LogViewModel searchModel)
        {
            
            var result = Context.Logs.AsQueryable();
            if (searchModel != null)
            {
                if (searchModel.LogID.HasValue)
                    result = result.Where(x => x.LogID == searchModel.LogID);
                if (!string.IsNullOrEmpty(searchModel.Last_Name))
                    result = result.Where(x => x.Last_Name.Contains(searchModel.Last_Name));
                if (searchModel.StartDate != null)
                    result = result.Where(x => x.StartDate>= searchModel.StartDate);
                if (searchModel.EndDate != null)
                    result = result.Where(x => x.EndDate<= searchModel.EndDate);
            }
            return result;
        }
    }
    
}

