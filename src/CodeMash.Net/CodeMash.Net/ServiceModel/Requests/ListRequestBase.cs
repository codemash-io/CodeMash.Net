using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace CodeMash
{
    [DataContract]
    public abstract class ListRequestBase : RequestBase /*, IPaginationRequest */
    {

#if NET452
        private readonly Func<string, int, int> tryParseConfigurationItemAsInt = (name, defaultPageSize) =>
        {
            var pageSize = defaultPageSize;
            var pageSizeAsString = ConfigurationManager.AppSettings[name];
            if (!string.IsNullOrEmpty(pageSizeAsString))
            {
                int.TryParse(pageSizeAsString, out pageSize);
            }
            return pageSize;
        };
#endif

        protected ListRequestBase()
        {
            PageNumber = 0;
#if NET452
            PageSize = tryParseConfigurationItemAsInt("DefaultPageSize", 1000);
#else
            PageSize = 1000;
#endif

        }

        /*protected ListRequestBase(IPaginationRequest list) : this()
        {
            PageSize = list.PageSize;
            PageNumber = list.PageNumber;
        }*/

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int PageNumber { get; set; }
#if NET452
        public int Skip => PageNumber * PageSize;
#else
        [DataMember]
        public int Skip 
        {
            get
            {
                return PageNumber * PageSize;
            }
        }
#endif


        [DataMember]
        public string Sort { get; set; }




    }
}