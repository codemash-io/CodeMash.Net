/* Options:
Date: 2022-10-27 09:15:37
Version: 6.02
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:5010

GlobalNamespace: CodeMash.ServiceContracts.Events.Api
MakePartial: False
//MakeVirtual: True
//MakeInternal: False
//MakeDataContractsExtensible: False
//AddReturnMarker: True
AddDescriptionAsComments: True
AddDataContractAttributes: False
//AddIndexesToDataMembers: False
//AddGeneratedCodeAttributes: False
//AddResponseStatus: False
//AddImplicitVersion: 
//InitializeCollections: True
//ExportValueTypes: False
//IncludeTypes: 
//ExcludeTypes: 
AddNamespaces: System.Runtime.Serialization
//AddDefaultXmlNamespace: http://schemas.servicestack.net/types
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;
using CodeMash.ServiceContracts.Events.Api;

namespace CodeMash.ServiceContracts.Events.Api
{
    public class CodeMashListRequestBase
        : CodeMashRequestBase, IRequestWithPaging, IRequestWithFilter, IRequestWithSorting
    {
        ///<summary>
        ///Amount of records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of records to return", Format="int32", Name="PageSize", ParameterType="form")]
        public virtual int PageSize { get; set; }

        ///<summary>
        ///Page of records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Page of records to return", Format="int32", Name="PageNumber", ParameterType="form")]
        public virtual int PageNumber { get; set; }

        ///<summary>
        ///A query that specifies which records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies which records to return", Name="Filter", ParameterType="body")]
        public virtual string Filter { get; set; }

        ///<summary>
        ///A query that specifies how to sort filtered records
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies how to sort filtered records", Name="Sort", ParameterType="body")]
        public virtual string Sort { get; set; }

        ///<summary>
        ///A query that specifies what fields in records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies what fields in records to return", Name="Projection", ParameterType="body")]
        public virtual string Projection { get; set; }
    }

    public class CodeMashOpenRequestBase
        : RequestBase
    {
    }

    public class CodeMashRequestBase
        : RequestBase
    {
        ///<summary>
        ///ID of your project. Can be passed in a header as X-CM-ProjectId.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of your project. Can be passed in a header as X-CM-ProjectId.", IsRequired=true, Name="X-CM-ProjectId", ParameterType="header")]
        public virtual Guid ProjectId { get; set; }
    }

    public interface ICultureBasedRequest
    {
        string CultureCode { get; set; }
    }

    public interface IRequestWithFilter
    {
        string Filter { get; set; }
    }

    public interface IRequestWithPaging
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }

    public interface IRequestWithSorting
    {
        string Sort { get; set; }
    }

    public interface IVersionBasedRequest
    {
        string CultureCode { get; set; }
    }

    [DataContract(Namespace="http://codemash.io/types/")]
    public class RequestBase
        : ICultureBasedRequest, IVersionBasedRequest
    {
        ///<summary>
        ///Specify culture code when your output should be localised. E.g.: en
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Specify culture code when your output should be localised. E.g.: en", Name="CultureCode", ParameterType="header")]
        public virtual string CultureCode { get; set; }

        ///<summary>
        ///Specify culture code when your output should be localised. E.g.: en
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Specify culture code when your output should be localised. E.g.: en", Name="CultureCode", ParameterType="header")]
        public virtual string Version { get; set; }
    }

    public class ResponseBase<T>
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(Name="result")]
        public virtual T Result { get; set; }
    }

    ///<summary>
    ///Add users to server events channel
    ///</summary>
    [Route("/notifications/server-events/groups/{groupId}/channels/{channelId}/users", "POST")]
    [Route("/{version}/notifications/server-events/groups/{groupId}/channels/{channelId}/users", "POST")]
    [Api(Description="Add users to server events channel")]
    [DataContract]
    public class AddUsersToSseChannelRequest
        : CodeMashRequestBase
    {
        public AddUsersToSseChannelRequest()
        {
            Users = new List<Guid>{};
        }

        ///<summary>
        ///Group ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Group ID", IsRequired=true, Name="GroupId", ParameterType="path")]
        public virtual string GroupId { get; set; }

        ///<summary>
        ///Channel ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Channel ID", IsRequired=true, Name="ChannelId", ParameterType="path")]
        public virtual string ChannelId { get; set; }

        ///<summary>
        ///Users that are already in the group to include in a channel
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Users that are already in the group to include in a channel", Name="Users", ParameterType="body")]
        public virtual List<Guid> Users { get; set; }
    }

    ///<summary>
    ///Add users to server events group
    ///</summary>
    [Route("/notifications/server-events/groups/{id}/users", "POST")]
    [Route("/{version}/notifications/server-events/groups/{id}/users", "POST")]
    [Api(Description="Add users to server events group")]
    [DataContract]
    public class AddUsersToSseGroupRequest
        : CodeMashRequestBase
    {
        public AddUsersToSseGroupRequest()
        {
            Users = new List<Guid>{};
        }

        ///<summary>
        ///Group ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Group ID", IsRequired=true, Name="Id", ParameterType="path")]
        public virtual string Id { get; set; }

        ///<summary>
        ///Users to include in a group
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Users to include in a group", IsRequired=true, Name="Users", ParameterType="body")]
        public virtual List<Guid> Users { get; set; }
    }

    ///<summary>
    ///Get short lived token to open event stream
    ///</summary>
    [Route("/notifications/server-events/connections/initialize", "GET")]
    [Route("/{version}/notifications/server-events/connections/initialize", "GET")]
    [Api(Description="Get short lived token to open event stream")]
    [DataContract]
    public class AuthorizeSseConnectionRequest
        : CodeMashRequestBase, IReturn<AuthorizeSseConnectionResponse>
    {
    }

    public class AuthorizeSseConnectionResponse
        : ResponseBase<ChannelConnectionAuth>
    {
    }

    public class ChannelConnectionAuth
    {
        public virtual string Token { get; set; }
    }

    ///<summary>
    ///Keep server events connection alive
    ///</summary>
    [Route("/notifications/server-events/connections/close", "GET")]
    [Route("/{version}/notifications/server-events/connections/close", "GET")]
    [Api(Description="Keep server events connection alive")]
    [DataContract]
    public class CloseSseConnectionRequest
        : CodeMashOpenRequestBase, IReturnVoid
    {
        ///<summary>
        ///Connection subscription id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Connection subscription id", IsRequired=true, Name="Id", ParameterType="query")]
        public virtual string Id { get; set; }
    }

    ///<summary>
    ///Create server events channel
    ///</summary>
    [Route("/notifications/server-events/groups/{groupId}/channels", "POST")]
    [Route("/{version}/notifications/server-events/groups/{groupId}/channels", "POST")]
    [Api(Description="Create server events channel")]
    [DataContract]
    public class CreateSseChannelRequest
        : CodeMashRequestBase, IReturn<CreateSseChannelResponse>
    {
        public CreateSseChannelRequest()
        {
            Meta = new Dictionary<string, string>{};
            Users = new List<Guid>{};
        }

        ///<summary>
        ///Group ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Group ID", IsRequired=true, Name="GroupId", ParameterType="path")]
        public virtual string GroupId { get; set; }

        ///<summary>
        ///Title of a channel
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Title of a channel", IsRequired=true, Name="Title", ParameterType="body")]
        public virtual string Title { get; set; }

        ///<summary>
        ///Additional custom data
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Additional custom data", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }

        ///<summary>
        ///Users that are already in the group to include in a channel
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Users that are already in the group to include in a channel", Name="Users", ParameterType="body")]
        public virtual List<Guid> Users { get; set; }
    }

    public class CreateSseChannelResponse
        : ResponseBase<string>
    {
    }

    ///<summary>
    ///Create server events group
    ///</summary>
    [Route("/notifications/server-events/groups", "POST")]
    [Route("/{version}/notifications/server-events/groups", "POST")]
    [Api(Description="Create server events group")]
    [DataContract]
    public class CreateSseGroupRequest
        : CodeMashRequestBase, IReturn<CreateSseGroupResponse>
    {
        public CreateSseGroupRequest()
        {
            Meta = new Dictionary<string, string>{};
            Users = new List<Guid>{};
        }

        ///<summary>
        ///Title of a channel
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Title of a channel", IsRequired=true, Name="Title", ParameterType="body")]
        public virtual string Title { get; set; }

        ///<summary>
        ///Additional custom data
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Additional custom data", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }

        ///<summary>
        ///Users to include in a group
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Users to include in a group", Name="Users", ParameterType="body")]
        public virtual List<Guid> Users { get; set; }
    }

    public class CreateSseGroupResponse
        : ResponseBase<string>
    {
    }

    ///<summary>
    ///Delete server events channel
    ///</summary>
    [Route("/notifications/server-events/groups/{groupId}/channels/{channelId}", "DELETE")]
    [Route("/{version}/notifications/server-events/groups/{groupId}/channels/{channelId}", "DELETE")]
    [Api(Description="Delete server events channel")]
    [DataContract]
    public class DeleteSseChannelRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///Group ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Group ID", IsRequired=true, Name="GroupId", ParameterType="path")]
        public virtual string GroupId { get; set; }

        ///<summary>
        ///Channel ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Channel ID", IsRequired=true, Name="ChannelId", ParameterType="path")]
        public virtual string ChannelId { get; set; }
    }

    ///<summary>
    ///Delete server events group
    ///</summary>
    [Route("/notifications/server-events/groups/{id}", "DELETE")]
    [Route("/{version}/notifications/server-events/groups/{id}", "DELETE")]
    [Api(Description="Delete server events group")]
    [DataContract]
    public class DeleteSseGroupRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///Group ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Group ID", IsRequired=true, Name="Id", ParameterType="path")]
        public virtual string Id { get; set; }
    }

    ///<summary>
    ///Get server events connections
    ///</summary>
    [Route("/notifications/server-events/connections", "GET")]
    [Route("/{version}/notifications/server-events/connections", "GET")]
    [Api(Description="Get server events connections")]
    [DataContract]
    public class GetOpenSseConnectionsRequest
        : CodeMashRequestBase, IReturn<GetOpenSseConnectionsResponse>
    {
        public GetOpenSseConnectionsRequest()
        {
            Channels = new List<string>{};
        }

        ///<summary>
        ///Connection subscription id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Connection subscription id", IsRequired=true, Name="Id", ParameterType="query")]
        public virtual List<string> Channels { get; set; }
    }

    public class GetOpenSseConnectionsResponse
        : ResponseBase<List<SseSubscription>>
    {
    }

    ///<summary>
    ///Gets server event group channels
    ///</summary>
    [Route("/notifications/server-events/groups/{groupId}/channels", "GET")]
    [Route("/{version}/notifications/server-events/groups/{groupId}/channels", "GET")]
    [Api(Description="Gets server event group channels")]
    [DataContract]
    public class GetSseChannelsRequest
        : CodeMashListRequestBase, IReturn<GetSseChannelsResponse>
    {
        ///<summary>
        ///Group ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Group ID", IsRequired=true, Name="GroupId", ParameterType="path")]
        public virtual string GroupId { get; set; }
    }

    public class GetSseChannelsResponse
        : ResponseBase<List<SseChannel>>
    {
        public virtual long TotalCount { get; set; }
    }

    ///<summary>
    ///Gets server event group
    ///</summary>
    [Route("/notifications/server-events/groups/{id}", "GET")]
    [Route("/{version}/notifications/server-events/groups/{id}", "GET")]
    [Api(Description="Gets server event group")]
    [DataContract]
    public class GetSseGroupRequest
        : CodeMashRequestBase, IReturn<GetSseGroupResponse>
    {
        ///<summary>
        ///Group ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Group ID", Name="Id", ParameterType="path")]
        public virtual string Id { get; set; }

        ///<summary>
        ///Includes first page of channels
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes first page of channels", Name="IncludeChannels", ParameterType="query")]
        public virtual bool IncludeChannels { get; set; }

        ///<summary>
        ///Includes first page of users
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes first page of users", Name="IncludeUsers", ParameterType="query")]
        public virtual bool IncludeUsers { get; set; }

        ///<summary>
        ///Includes total unread messages in each channel
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes total unread messages in each channel", Name="IncludeNotSeenCount", ParameterType="query")]
        public virtual bool IncludeNotSeenCount { get; set; }
    }

    public class GetSseGroupResponse
        : ResponseBase<SseGroup>
    {
    }

    ///<summary>
    ///Gets server event groups
    ///</summary>
    [Route("/notifications/server-events/groups", "GET")]
    [Route("/{version}/notifications/server-events/groups", "GET")]
    [Api(Description="Gets server event groups")]
    [DataContract]
    public class GetSseGroupsRequest
        : CodeMashListRequestBase, IReturn<GetSseGroupsResponse>
    {
        ///<summary>
        ///Includes first page of channels
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes first page of channels", Name="IncludeChannels", ParameterType="query")]
        public virtual bool IncludeChannels { get; set; }

        ///<summary>
        ///Includes first page of users
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes first page of users", Name="IncludeUsers", ParameterType="query")]
        public virtual bool IncludeUsers { get; set; }

        ///<summary>
        ///Includes total unread messages in each channel
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes total unread messages in each channel", Name="IncludeNotSeenCount", ParameterType="query")]
        public virtual bool IncludeNotSeenCount { get; set; }
    }

    public class GetSseGroupsResponse
        : ResponseBase<List<SseGroup>>
    {
        public virtual long TotalCount { get; set; }
    }

    ///<summary>
    ///Gets messages for server events group channel
    ///</summary>
    [Route("/notifications/server-events/messages", "GET")]
    [Route("/{version}/notifications/server-events/messages", "GET")]
    [Api(Description="Gets messages for server events group channel")]
    [DataContract]
    public class GetSseMessagesRequest
        : CodeMashListRequestBase, IReturn<GetSseMessagesResponse>
    {
        ///<summary>
        ///Channel ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Channel ID", IsRequired=true, Name="ChannelId", ParameterType="query")]
        public virtual string ChannelId { get; set; }
    }

    public class GetSseMessagesResponse
        : ResponseBase<List<SseMessage>>
    {
        public virtual long TotalCount { get; set; }
    }

    ///<summary>
    ///Keep server events connection alive
    ///</summary>
    [Route("/notifications/server-events/connections/health", "GET")]
    [Route("/{version}/notifications/server-events/connections/health", "GET")]
    [Api(Description="Keep server events connection alive")]
    [DataContract]
    public class HeartBeatSseConnectionRequest
        : CodeMashOpenRequestBase, IReturnVoid
    {
        ///<summary>
        ///Connection subscription id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Connection subscription id", IsRequired=true, Name="Id", ParameterType="query")]
        public virtual string Id { get; set; }
    }

    ///<summary>
    ///Open event stream connection
    ///</summary>
    [Route("/notifications/server-events/connections/open", "GET")]
    [Route("/{version}/notifications/server-events/connections/open", "GET")]
    [Api(Description="Open event stream connection")]
    [DataContract]
    public class OpenSseConnectionRequest
        : CodeMashOpenRequestBase, IReturnVoid
    {
        ///<summary>
        ///Auth token to open stream
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Auth token to open stream", IsRequired=true, Name="Token", ParameterType="query")]
        public virtual string Token { get; set; }
    }

    ///<summary>
    ///Mark messages as seen
    ///</summary>
    [Route("/notifications/server-events/messages/read", "POST")]
    [Route("/{version}/notifications/server-events/messages/read", "POST")]
    [Api(Description="Mark messages as seen")]
    [DataContract]
    public class ReadSseMessagesRequest
        : CodeMashRequestBase, IReturnVoid
    {
        public ReadSseMessagesRequest()
        {
            Ids = new List<string>{};
        }

        ///<summary>
        ///Channel ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Channel ID", IsRequired=true, Name="ChannelId", ParameterType="body")]
        public virtual string ChannelId { get; set; }

        ///<summary>
        ///Message ids to read. Pass empty to read all.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Message ids to read. Pass empty to read all.", Name="Ids", ParameterType="body")]
        public virtual List<string> Ids { get; set; }
    }

    ///<summary>
    ///Send server event to event stream
    ///</summary>
    [Route("/notifications/server-events/messages", "POST")]
    [Route("/{version}/notifications/server-events/messages", "POST")]
    [Api(Description="Send server event to event stream")]
    [DataContract]
    public class SendSseMessageRequest
        : CodeMashRequestBase, IReturnVoid
    {
        public SendSseMessageRequest()
        {
            Meta = new Dictionary<string, string>{};
            FileIds = new List<string>{};
        }

        ///<summary>
        ///Channel to send message to
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Channel to send message to", IsRequired=true, Name="ChannelId", ParameterType="body")]
        public virtual string ChannelId { get; set; }

        ///<summary>
        ///Message to send
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Message to send", Name="Message", ParameterType="body")]
        public virtual string Message { get; set; }

        ///<summary>
        ///Additional custom data
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Additional custom data", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }

        ///<summary>
        ///Message files to attach to message
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Message files to attach to message", Name="FileIds", ParameterType="body")]
        public virtual List<string> FileIds { get; set; }

        ///<summary>
        ///Send push notification after seconds if not seen
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Send push notification after seconds if not seen", Format="int32", Name="PushAfter", ParameterType="body")]
        public virtual int PushAfter { get; set; }

        ///<summary>
        ///Push notification template id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Push notification template id", Name="PushTemplateId", ParameterType="body")]
        public virtual Guid? PushTemplateId { get; set; }
    }

    public class SseChannel
    {
        public SseChannel()
        {
            Meta = new Dictionary<string, string>{};
        }

        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual SseMessage LatestMessage { get; set; }
        public virtual int TotalUnseen { get; set; }
    }

    public class SseGroup
    {
        public SseGroup()
        {
            Meta = new Dictionary<string, string>{};
            Channels = new List<SseChannel>{};
            Users = new List<SseGroupUser>{};
        }

        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual List<SseChannel> Channels { get; set; }
        public virtual List<SseGroupUser> Users { get; set; }
    }

    public class SseGroupUser
    {
        public virtual string Id { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual Guid UserId { get; set; }
    }

    public class SseMessage
    {
        public SseMessage()
        {
            Meta = new Dictionary<string, string>{};
            Files = new List<SseMessageFile>{};
            Views = new List<SseMessageView>{};
        }

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string Message { get; set; }
        public virtual string SenderId { get; set; }
        public virtual string UserId { get; set; }
        public virtual string GroupId { get; set; }
        public virtual string ChannelId { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual List<SseMessageFile> Files { get; set; }
        public virtual bool IsSeen { get; set; }
        public virtual List<SseMessageView> Views { get; set; }
    }

    public class SseMessageFile
    {
        public virtual string Id { get; set; }
        public virtual string OriginalFileName { get; set; }
        public virtual string FileName { get; set; }
        public virtual string ContentType { get; set; }
        public virtual long ContentLength { get; set; }
        public virtual string Directory { get; set; }
        public virtual string FilePath { get; set; }
    }

    public class SseMessageView
    {
        public virtual string UserId { get; set; }
        public virtual string SeenAt { get; set; }
    }

    public class SseSubscription
    {
        public virtual string Id { get; set; }
    }

}

