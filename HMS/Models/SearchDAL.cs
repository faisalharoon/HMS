using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class SearchDAL
    {
        private HMS_DBEntity Context;
        public SearchDAL()
        {
            Context = new HMS_DBEntity();
        }

        public IQueryable<tblPatient> GetSearchResults(ProductSearchModel searchModel)
        {
            var result = Context.tblPatients.AsQueryable();
            if (searchModel != null)
            {
                if (searchModel.Patient_ID.HasValue)
                    result = result.Where(x => x.Patient_id == searchModel.Patient_ID);
                if (!string.IsNullOrEmpty(searchModel.Patient_Name))
                    result = result.Where(x => x.Patient_Name.Contains(searchModel.Patient_Name));
                if (!string.IsNullOrEmpty(searchModel.Patient_Address))
                    result = result.Where(x => x.Patient_address.Contains(searchModel.Patient_Address));
                if (!string.IsNullOrEmpty(searchModel.Contact_Number))
                    result = result.Where(x => x.Contact_no.Contains(searchModel.Contact_Number));
            }
            return result;
        }
       
    }
}