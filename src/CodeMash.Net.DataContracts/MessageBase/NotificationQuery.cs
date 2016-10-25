using System.Runtime.Serialization;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class NotificationQuery
    {
        public NotificationQuery()
        {
            QueryAsJson = "{}";
        }

        [DataMember]
        public string CollectionName { get; set; }

        /// <summary>
        /// Specify MongoDB filter as json to query-filter your recipients. By default QueryAsJson is '{}' - which means get all recipients from collection defined in <see cref="CollectionName">CollectionName</see>  . : http://CodeMash.com/documentation/api/notifications/recipients#target-audience
        /// </summary>
        /// <value>The query as json.</value>
        [DataMember]
        public string QueryAsJson { get; set; }

        /// <summary>
        /// Specify MongoDB projection as json to select your recipients from Collection. : http://CodeMash.com/documentation/api/notifications/recipients#target-audience
        /// </summary>
        /// <value>The projection as json.</value>
        [DataMember]
        public string ProjectionAsJson { get; set; }


    }
}