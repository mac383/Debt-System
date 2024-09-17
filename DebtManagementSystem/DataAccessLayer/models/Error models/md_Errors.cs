using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models
{
    public class md_Errors
    {
        public string ErrorMessage { get; set; }
        public string? Source { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
        public string? StackTrace { get; set; }
        public int CompanyId { get; set; }
        public string Action { get; set; }
        public string Parameters { get; set; }

        public md_Errors(string errorMessage, string? source, string _class, string method,
            string? stackTrace, int companyId, string action, string parameters)
        {
            this.ErrorMessage = errorMessage;
            this.Source = source;
            this.Class = _class;
            this.Method = method;
            this.StackTrace = stackTrace;
            this.CompanyId = companyId;
            this.Action = action;
            this.Parameters = parameters;
        }

    }
}
