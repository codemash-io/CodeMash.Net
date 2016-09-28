using System;
using System.Configuration;
using System.Runtime.Serialization;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public abstract class ListRequestBase : RequestBase, IPaginationRequest
    {
        private readonly Func<string, int> tryParseConfigurationItemAsInt = (name) =>
        {
            var pageSize = 2000;
            var pageSizeAsString = ConfigurationManager.AppSettings[name];
            if (!string.IsNullOrEmpty(pageSizeAsString))
            {
                int.TryParse(pageSizeAsString, out pageSize);
            }
            return pageSize;
        };

        protected ListRequestBase()
        {
            PageNumber = 0;
            PageSize = tryParseConfigurationItemAsInt("DefaultPageSize");
        }

        protected ListRequestBase(IPaginationRequest list) : this()
        {        
            PageSize = list.PageSize;
            PageNumber = list.PageNumber;
        }
        
        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int PageNumber { get; set; }
        
        public int Skip => PageNumber * PageSize;
        

        [DataMember]
        public string Sort { get; set; }

        

        
    }

}
