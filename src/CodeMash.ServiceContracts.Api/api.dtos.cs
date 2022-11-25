/* Options:
Date: 2022-11-25 17:27:31
Version: 6.02
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:5002

GlobalNamespace: CodeMash.ServiceContracts.Api
MakePartial: False
MakeVirtual: False
//MakeInternal: False
//MakeDataContractsExtensible: False
//AddReturnMarker: True
AddDescriptionAsComments: True
AddDataContractAttributes: False
//AddIndexesToDataMembers: False
AddGeneratedCodeAttributes: False
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
using ServiceStack.Auth;
using System.IO;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.ServiceContracts.Api
{
    public interface IIdentityProvider
    {
        bool IsAuthenticated { get; set; }
        string UserId { get; set; }
        string SessionId { get; set; }
        string TenantId { get; set; }
        string ApplicationId { get; set; }
    }

    public class CodeMashDbListRequestBase
        : CodeMashDbRequestBase, IRequestWithPaging, IRequestWithFilter, IRequestWithSorting, IRequestWithProjection
    {
        ///<summary>
        ///Amount of records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of records to return", Format="int32", Name="PageSize", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="integer", Description="Amount of records to return", Format="int32", Name="PageSize", ParameterType="form", Verb="POST")]
        public int PageSize { get; set; }

        ///<summary>
        ///Page of records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Page of records to return", Format="int32", Name="PageNumber", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="integer", Description="Page of records to return", Format="int32", Name="PageNumber", ParameterType="form", Verb="POST")]
        public int PageNumber { get; set; }

        ///<summary>
        ///A query that specifies which records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies which records to return", Name="Filter", ParameterType="body", Verb="POST")]
        public string Filter { get; set; }

        ///<summary>
        ///A query that specifies how to sort filtered records
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies how to sort filtered records", Name="Sort", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="string", Description="A query that specifies how to sort filtered records", Name="Sort", ParameterType="form", Verb="POST")]
        public string Sort { get; set; }

        ///<summary>
        ///A query that specifies what fields in records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies what fields in records to return", Name="Projection", ParameterType="body", Verb="POST")]
        public string Projection { get; set; }
    }

    public class CodeMashDbRequestBase
        : CodeMashRequestBase
    {
        ///<summary>
        ///Collection name - unique, lowercased, collection name without whitespace. E.g., if your collection title you have entered in the CodeMash dashboard is "Business Trips" then collection name would be "business-trips".
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Collection name - unique, lowercased, collection name without whitespace. E.g., if your collection title you have entered in the CodeMash dashboard is \"Business Trips\" then collection name would be \"business-trips\".", IsRequired=true, Name="CollectionName", ParameterType="path")]
        public string CollectionName { get; set; }

        ///<summary>
        ///API key of your cluster. Can be passed in a header as X-CM-Cluster.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="API key of your cluster. Can be passed in a header as X-CM-Cluster.", Name="X-CM-Cluster", ParameterType="header")]
        public string Cluster { get; set; }
    }

    public class CodeMashListRequestBase
        : CodeMashRequestBase, IRequestWithPaging, IRequestWithFilter, IRequestWithSorting
    {
        ///<summary>
        ///Amount of records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of records to return", Format="int32", Name="PageSize", ParameterType="form")]
        public int PageSize { get; set; }

        ///<summary>
        ///Page of records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Page of records to return", Format="int32", Name="PageNumber", ParameterType="form")]
        public int PageNumber { get; set; }

        ///<summary>
        ///A query that specifies which records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies which records to return", Name="Filter", ParameterType="body")]
        public string Filter { get; set; }

        ///<summary>
        ///A query that specifies how to sort filtered records
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies how to sort filtered records", Name="Sort", ParameterType="body")]
        public string Sort { get; set; }

        ///<summary>
        ///A query that specifies what fields in records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies what fields in records to return", Name="Projection", ParameterType="body")]
        public string Projection { get; set; }
    }

    public class CodeMashRequestBase
        : RequestBase
    {
        ///<summary>
        ///ID of your project. Can be passed in a header as X-CM-ProjectId.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of your project. Can be passed in a header as X-CM-ProjectId.", IsRequired=true, Name="X-CM-ProjectId", ParameterType="header")]
        public Guid ProjectId { get; set; }
    }

    public interface ICultureBasedRequest
    {
        string CultureCode { get; set; }
    }

    public interface IOAuthRequest
    {
        string Mode { get; set; }
        string Code { get; set; }
        string State { get; set; }
        string AccessToken { get; set; }
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

    public interface IRequestWithProjection
    {
        string Projection { get; set; }
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
        ///Specify culture code when your response from the API should be localised. E.g.: en
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Specify culture code when your response from the API should be localised. E.g.: en", Name="CultureCode", ParameterType="header")]
        public string CultureCode { get; set; }

        ///<summary>
        ///The CodeMash API version used to fetch data from the API. If not specified, the last version will be used.  E.g.: v2
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The CodeMash API version used to fetch data from the API. If not specified, the last version will be used.  E.g.: v2", Name="version", ParameterType="path")]
        public string Version { get; set; }
    }

    public class ResponseBase<T>
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        [DataMember(Name="result")]
        public T Result { get; set; }
    }

    public class AadFunctionExecutorRequest
    {
        public SystemFunctionExecutorData Data { get; set; }
    }

    [Route("/notifications/email/aws/events", "POST")]
    [Route("/{version}/notifications/email/aws/events", "POST")]
    public class AwsSesEndpoint
        : IReturn<HttpResult>
    {
    }

    public class BarCodeExecutorRequest
    {
        public SystemFunctionExecutorData Data { get; set; }
    }

    public class CollectionFindExecutorRequest
    {
        public SystemFunctionExecutorData Data { get; set; }
    }

    public class CollectionUpdateExecutorRequest
    {
        public SystemFunctionExecutorData Data { get; set; }
    }

    public class DocxTemplateExecutorRequest
    {
        public SystemFunctionExecutorData Data { get; set; }
    }

    public class GoogleFunctionExecutorRequest
    {
        public SystemFunctionExecutorData Data { get; set; }
    }

    public class HtmlToPdfExecutorRequest
    {
        public SystemFunctionExecutorData Data { get; set; }
    }

    public class ImageResizeExecutorRequest
    {
        public SystemFunctionExecutorData Data { get; set; }
    }

    [Route("/notifications/email/mailgun/events", "POST")]
    [Route("/{version}/notifications/email/mailgun/events", "POST")]
    public class MailGunEndpoint
        : IReturn<HttpResult>
    {
    }

    public class QrCodeExecutorRequest
    {
        public SystemFunctionExecutorData Data { get; set; }
    }

    public class RegisterOAuthUser
        : IReturn<RegisterOAuthUserResponse>
    {
        public Guid ProjectId { get; set; }
        public string ProviderUserId { get; set; }
        public ProjectUserProviders Provider { get; set; }
        public CustomUserSession OnRegistersSession { get; set; }
        public string Mode { get; set; }
    }

    public class RegisterOAuthUserResponse
        : ResponseBase<bool>
    {
        public Guid UserId { get; set; }
        public string AccountId { get; set; }
        public IUserAuth UserAuth { get; set; }
    }

    ///<summary>
    ///Email services
    ///</summary>
    [Route("/notifications/email/sendgrid/events", "POST")]
    [Route("/{version}/notifications/email/sendgrid/events", "POST")]
    [Api(Description="Email services")]
    [DataContract]
    public class SendGridEndpoint
        : IReturn<HttpResult>
    {
    }

    public class SystemFunctionExecutorRequest
    {
        public SystemFunctionExecutorRequest()
        {
            Meta = new Dictionary<string, string>{};
            Tokens = new Dictionary<string, string>{};
        }

        public ProjectContextDto ProjectContext { get; set; }
        public ServerlessFunctionDto Function { get; set; }
        public ServerlessProviderDto Connection { get; set; }
        public string Template { get; set; }
        public string Data { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public Dictionary<string, string> Tokens { get; set; }
        public ProjectSmallDto Project { get; set; }
        public Guid? ActivatorId { get; set; }
        public string CultureCode { get; set; }
        public string Version { get; set; }
    }

    public class UsersFindExecutorRequest
    {
        public SystemFunctionExecutorData Data { get; set; }
    }

    [Flags]
    public enum AccountStatus
    {
        Registered = 1,
        PendingValidation = 2,
        Verified = 4,
        Active = 8,
        Unregistered = 16,
        SubscriptionExpired = 64,
        Blocked = 128,
    }

    public enum ProjectUserProviders
    {
        apple,
        aad,
        google,
        facebook,
        twitter,
        credentials,
        apikey,
    }

    public class ModuleData
    {
        public string WidgetType { get; set; }
        public ModuleWidget Settings { get; set; }
    }

    public class ModuleWidget
    {
    }

    public class ProjectModulesData
    {
        public ModuleData Database { get; set; }
        public ModuleData Membership { get; set; }
        public ModuleData Emails { get; set; }
        public ModuleData Notifications { get; set; }
        public ModuleData Files { get; set; }
        public ModuleData Logging { get; set; }
        public ModuleData Scheduler { get; set; }
        public ModuleData Serverless { get; set; }
    }

    public class CustomUserSession
        : AuthUserSession, IIdentityProvider
    {
        public CustomUserSession()
        {
            AllowedProjects = new List<string>{};
        }

        public Guid? ProjectId { get; set; }
        public bool IsProjectAdmin { get; set; }
        public string ApiKey { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string TenantId { get; set; }
        public string ApplicationId { get; set; }
        public List<string> AllowedProjects { get; set; }
        public bool IsAccountUser { get; set; }
        public AccountStatus Status { get; set; }
        public bool IsTokenUser { get; set; }
    }

    public class BaseInvoice
    {
        public DateTime Created { get; set; }
        public string GeneratedInvoiceId { get; set; }
        public string InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomInvoiceNumber { get; set; }
        public long? CmInvoiceNo { get; set; }
        public string CmInvoiceNumber { get; set; }
        public long AmountPaid { get; set; }
        public long AmountRemaining { get; set; }
        public long AttemptCount { get; set; }
        public string Currency { get; set; }
        public string InvoiceUrl { get; set; }
        public string InvoicePdf { get; set; }
        public bool Paid { get; set; }
        public string Status { get; set; }
    }

    public class CustomerInvoice
        : BaseInvoice
    {
        public string CustomerId { get; set; }
        public Guid ProjectId { get; set; }
        public bool IsStaticInvoice { get; set; }
        public string StaticProjectName { get; set; }
        public string StaticToken { get; set; }
        public int StaticCustomerInvoiceNumber { get; set; }
    }

    public class DatabaseCredentials
    {
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Region { get; set; }
        public string SrvClusterName { get; set; }
        public bool UseSrvName { get; set; }
        public string EncVersion { get; set; }
    }

    public class RefundInvoice
        : BaseInvoice
    {
        public string SubscriptionId { get; set; }
        public SubscriptionPlan Plan { get; set; }
        public int? DaysUnused { get; set; }
        public string RefundedInvoiceId { get; set; }
    }

    public enum ServerlessProvider
    {
        None,
        CodemashAmazon,
        Amazon,
        Azure,
        Google,
    }

    public class SubscriptionDomain
    {
        public string SubscriptionId { get; set; }
        public SubscriptionPlan Plan { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateTime SubscribeDate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool PeriodPaid { get; set; }
        public int PayAttemptCount { get; set; }
        public bool PayAttempted { get; set; }
        public DateTime? Canceled { get; set; }
        public DateTime? SuspendOn { get; set; }
        public bool IsTrialPeriod { get; set; }
        public bool IsCurrentAndActive { get; set; }
    }

    public class SubscriptionInvoice
        : BaseInvoice
    {
        public string SubscriptionId { get; set; }
        public SubscriptionPlan Plan { get; set; }
    }

    public enum SubscriptionPlan
    {
        Growth,
        Standard,
        Enterprise,
    }

    public enum SubscriptionStatus
    {
        Inactive,
        Pending,
        Active,
        PendingCancel,
        Expired,
        Changed,
        Unpaid,
    }

    public class TokenResolverField
    {
        public string Name { get; set; }
        public string Config { get; set; }
    }

    public class UsageInvoice
        : BaseInvoice
    {
    }

    ///<summary>
    ///Authentication
    ///</summary>
    [Route("/auth/aad", "GET POST")]
    [Route("/{version}/auth/aad", "GET POST")]
    [Api(Description="Authentication")]
    [DataContract]
    public class AadAuthenticationRequest
        : CodeMashRequestBase, IReturn<AadAuthenticationResponse>, IOAuthRequest
    {
        ///<summary>
        ///Mode to use for authentication
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Mode to use for authentication", Name="Mode", ParameterType="query")]
        public string Mode { get; set; }

        ///<summary>
        ///Code received from Microsoft services
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Code received from Microsoft services", Name="Code", ParameterType="query")]
        public string Code { get; set; }

        ///<summary>
        ///State received with a code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="State received with a code", Name="State", ParameterType="query")]
        public string State { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessToken", ParameterType="query")]
        public string AccessToken { get; set; }
    }

    public class AadAuthenticationResponse
        : ResponseBase<AuthResponse>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/aggregate/{id}", "GET POST")]
    [Route("/{version}/db/{CollectionName}/aggregate/{id}", "GET POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class AggregateRequest
        : CodeMashDbRequestBase, IReturn<AggregateResponse>
    {
        public AggregateRequest()
        {
            Tokens = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Id of an aggregate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of an aggregate", Name="Id", ParameterType="path")]
        public Guid Id { get; set; }

        ///<summary>
        ///Tokens that should be injected into aggregation document
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Tokens that should be injected into aggregation document", Name="Tokens", ParameterType="body")]
        public Dictionary<string, string> Tokens { get; set; }
    }

    public class AggregateResponse
        : ResponseBase<string>
    {
    }

    public class AndroidBackgroundLayout
    {
        public string Image { get; set; }
        public string HeadingColor { get; set; }
        public string ContentColor { get; set; }
    }

    ///<summary>
    ///Authentication
    ///</summary>
    [Route("/auth/apple", "GET POST")]
    [Route("/{version}/auth/apple", "GET POST")]
    [Api(Description="Authentication")]
    [DataContract]
    public class AppleAuthenticationRequest
        : CodeMashRequestBase, IReturn<AppleAuthenticationResponse>, IOAuthRequest
    {
        public AppleAuthenticationRequest()
        {
            Meta = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Mode to use for authentication
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Mode to use for authentication", Name="Mode", ParameterType="query")]
        public string Mode { get; set; }

        ///<summary>
        ///Code received from Google services
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Code received from Google services", Name="Code", ParameterType="query")]
        public string Code { get; set; }

        ///<summary>
        ///State received with a code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="State received with a code", Name="State", ParameterType="query")]
        public string State { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessToken", ParameterType="query")]
        public string AccessToken { get; set; }

        ///<summary>
        ///Attach any data to the request
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Attach any data to the request", Name="Meta", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }
    }

    public class AppleAuthenticationResponse
        : ResponseBase<AuthResponse>
    {
    }

    public class ApplicableDiscount
    {
        public ApplicableDiscount()
        {
            IndividualDiscounts = new List<DiscountIndividualLine>{};
            CategoryDiscounts = new List<DiscountCategory>{};
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string CreatedOn { get; set; }
        public string ValidFrom { get; set; }
        public string ValidUntil { get; set; }
        public string Type { get; set; }
        public string TargetType { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string CollectionName { get; set; }
        public string Cluster { get; set; }
        public List<DiscountIndividualLine> IndividualDiscounts { get; set; }
        public List<DiscountCategory> CategoryDiscounts { get; set; }
        public DiscountAll AllDiscount { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/methods/attach", "POST")]
    [Route("/{version}/payments/methods/attach", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class AttachPaymentMethodRequest
        : CodeMashRequestBase, IReturn<AttachPaymentMethodResponse>
    {
        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", Name="CustomerId", ParameterType="body")]
        public string CustomerId { get; set; }

        ///<summary>
        ///Setup intent ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Setup intent ID.", Name="SetupId", ParameterType="body")]
        public string SetupId { get; set; }

        ///<summary>
        ///Client secret got from creating setup intent.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Client secret got from creating setup intent.", Name="SetupClientSecret", ParameterType="body")]
        public string SetupClientSecret { get; set; }

        ///<summary>
        ///Should this payment method be default.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should this payment method be default.", Name="AsDefault", ParameterType="body")]
        public bool AsDefault { get; set; }

        ///<summary>
        ///Should current payment methods be detached.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should current payment methods be detached.", Name="DetachOthers", ParameterType="body")]
        public bool DetachOthers { get; set; }
    }

    public class AttachPaymentMethodResponse
        : ResponseBase<PaymentMethod>
    {
    }

    ///<summary>
    ///Gets one user
    ///</summary>
    [Route("/auth", "GET POST")]
    [Route("/{version}/auth", "GET POST")]
    [Api(Description="Gets one user")]
    [DataContract]
    public class AuthCheckRequest
        : CodeMashRequestBase, IReturn<AuthCheckResponse>
    {
    }

    public class AuthCheckResponse
        : ResponseBase<AuthResponse>
    {
    }

    public class AuthResponse
    {
        public AuthResponse()
        {
            Roles = new List<Role>{};
            Permissions = new List<string>{};
            AuthTokens = new List<IAuthTokens>{};
            Meta = new Dictionary<string, string>{};
        }

        public string UserId { get; set; }
        public string UserAuthId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SessionId { get; set; }
        public string ReferrerUrl { get; set; }
        public string BearerToken { get; set; }
        public string Email { get; set; }
        public List<Role> Roles { get; set; }
        public List<string> Permissions { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthDateRaw { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Culture { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Language { get; set; }
        public string ProfileUrl { get; set; }
        public long Tag { get; set; }
        public string AuthProvider { get; set; }
        public string MailAddress { get; set; }
        public string Nickname { get; set; }
        public string PostalCode { get; set; }
        public string TimeZone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public string Status { get; set; }
        public List<IAuthTokens> AuthTokens { get; set; }
        public Dictionary<string, string> Meta { get; set; }
    }

    public class Base64FileUpload
    {
        public string Data { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }

    ///<summary>
    ///Blocks selected user preventing the use of authenticated actions
    ///</summary>
    [Route("/membership/users/{Id}/block", "PUT PATCH")]
    [Route("/{version}/membership/users/{Id}/block", "PUT PATCH")]
    [Api(Description="Blocks selected user preventing the use of authenticated actions")]
    [DataContract]
    public class BlockUserRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///ID of user to be blocked
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="ID of user to be blocked", IsRequired=true, Name="Id", ParameterType="body")]
        public Guid Id { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/auth/kevin/token/{id}", "DELETE")]
    [Route("/{version}/payments/auth/kevin/token/{id}", "DELETE")]
    [Api(Description="Payments")]
    [DataContract]
    public class CancelKevinTokenRequest
        : CodeMashRequestBase, IReturn<StartKevinAuthResponse>
    {
        ///<summary>
        ///Payment account token ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment account token ID.", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers/{customerId}/subscriptions/{id}", "DELETE")]
    [Route("/{version}/payments/customers/{customerId}/subscriptions/{id}", "DELETE")]
    [Api(Description="Payments")]
    [DataContract]
    public class CancelSubscriptionRequest
        : CodeMashRequestBase, IReturn<CancelSubscriptionResponse>
    {
        ///<summary>
        ///Subscription ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Subscription ID.", IsRequired=true, Name="SubscriptionId", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", IsRequired=true, Name="CustomerId", ParameterType="path")]
        public string CustomerId { get; set; }

        ///<summary>
        ///Should cancel instantly. Overrides payment settings
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should cancel instantly. Overrides payment settings", Name="CancelInstantly", ParameterType="query")]
        public bool? CancelInstantly { get; set; }

        ///<summary>
        ///Should refund on cancel instantly. Overrides payment settings
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should refund on cancel instantly. Overrides payment settings", Name="RefundOnCancelInstantly", ParameterType="query")]
        public bool? RefundOnCancelInstantly { get; set; }
    }

    public class CancelSubscriptionResponse
        : ResponseBase<Subscription>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/responsibility", "POST")]
    [Route("/{version}/db/{CollectionName}/responsibility", "POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class ChangeResponsibilityRequest
        : CodeMashDbRequestBase, IReturn<ChangeResponsibilityResponse>
    {
        ///<summary>
        ///Id of a record to change responsibility for
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of a record to change responsibility for", IsRequired=true, Name="Id", ParameterType="form")]
        public string Id { get; set; }

        ///<summary>
        ///New responsible user for this record
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New responsible user for this record", IsRequired=true, Name="NewResponsibleUserId", ParameterType="form")]
        public Guid NewResponsibleUserId { get; set; }
    }

    public class ChangeResponsibilityResponse
        : ResponseBase<UpdateResult>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers/{customerId}/subscriptions/{id}", "PUT")]
    [Route("/{version}/payments/customers/{customerId}/subscriptions/{id}", "PUT")]
    [Api(Description="Payments")]
    [DataContract]
    public class ChangeSubscriptionRequest
        : CodeMashRequestBase, IReturn<ChangeSubscriptionResponse>
    {
        ///<summary>
        ///Subscription ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Subscription ID.", IsRequired=true, Name="SubscriptionId", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", IsRequired=true, Name="CustomerId", ParameterType="path")]
        public string CustomerId { get; set; }

        ///<summary>
        ///Subscription plan ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Subscription plan ID.", IsRequired=true, Name="PlanId", ParameterType="body")]
        public Guid NewPlanId { get; set; }

        ///<summary>
        ///Payment method's ID. If not provided will use default.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's ID. If not provided will use default.", Name="PaymentMethodId", ParameterType="body")]
        public string PaymentMethodId { get; set; }

        ///<summary>
        ///Discount coupon ID if supported.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Discount coupon ID if supported.", Name="Coupon", ParameterType="body")]
        public string Coupon { get; set; }
    }

    public class ChangeSubscriptionResponse
        : ResponseBase<Subscription>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/orders/kevin/payment/status", "POST")]
    [Route("/{version}/payments/orders/kevin/payment/status", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class CheckKevinPaymentStatusRequest
        : CodeMashRequestBase, IReturn<CheckKevinPaymentStatusResponse>
    {
        ///<summary>
        ///Kevin payment ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Kevin payment ID.", IsRequired=true, Name="PaymentId", ParameterType="query")]
        public string PaymentId { get; set; }
    }

    public class CheckKevinPaymentStatusResponse
        : ResponseBase<KevinPaymentStatus>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/orders/{Id}/stripe/payment/status", "GET")]
    [Route("/{version}/payments/orders/{Id}/stripe/payment/status", "GET")]
    [Api(Description="Payments")]
    [DataContract]
    public class CheckStripePaymentStatusRequest
        : CodeMashRequestBase, IReturn<CheckStripePaymentStatusResponse>
    {
        ///<summary>
        ///Order ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Order ID.", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///Payment account ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Payment account ID.", IsRequired=true, Name="AccountId", ParameterType="query")]
        public Guid AccountId { get; set; }

        ///<summary>
        ///Transaction ID which was used with this order and client secret.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Transaction ID which was used with this order and client secret.", IsRequired=true, Name="TransactionId", ParameterType="query")]
        public string TransactionId { get; set; }

        ///<summary>
        ///ClientSecret got from creating stripe transaction.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ClientSecret got from creating stripe transaction.", IsRequired=true, Name="ClientSecret", ParameterType="query")]
        public string ClientSecret { get; set; }
    }

    public class CheckStripePaymentStatusResponse
        : ResponseBase<string>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/count", "GET POST")]
    [Route("/{version}/db/{CollectionName}/count", "GET POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class CountRequest
        : CodeMashDbRequestBase, IReturn<CountResponse>
    {
        ///<summary>
        ///Filter cannot be passed as query string, use POST method instead.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Filter cannot be passed as query string, use POST method instead.", Name="Filter", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="string", Description="A query (in JSON format) that specifies which records to count", Name="Filter", ParameterType="form", Verb="POST")]
        public string Filter { get; set; }

        ///<summary>
        ///A limit on the number of objects to be returned, between 1 and 100.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="A limit on the number of objects to be returned, between 1 and 100.", Format="int32", Name="Limit", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="integer", Description="A limit on the number of objects to be returned, between 1 and 100.", Format="int32", Name="Limit", ParameterType="form", Verb="POST")]
        public int? Limit { get; set; }

        ///<summary>
        ///The number of records to skip before counting
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="The number of records to skip before counting", Format="int32", Name="Skip", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="integer", Description="The number of records to skip before counting", Format="int32", Name="Skip", ParameterType="form", Verb="POST")]
        public int? Skip { get; set; }
    }

    public class CountResponse
        : ResponseBase<long>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers", "POST")]
    [Route("/{version}/payments/customers", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class CreateCustomerRequest
        : CodeMashRequestBase, IReturn<CreateCustomerResponse>
    {
        public CreateCustomerRequest()
        {
            Meta = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Payment account ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Payment account ID.", IsRequired=true, Name="AccountId", ParameterType="body")]
        public Guid AccountId { get; set; }

        ///<summary>
        ///User's ID to whom to attach this customer.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User's ID to whom to attach this customer.", Name="UserId", ParameterType="body")]
        public Guid UserId { get; set; }

        ///<summary>
        ///Customer's phone. Overrides user's phone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's phone. Overrides user's phone.", Name="Phone", ParameterType="body")]
        public string Phone { get; set; }

        ///<summary>
        ///Customer's full name. Overrides user's name.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's full name. Overrides user's name.", Name="Name", ParameterType="body")]
        public string Name { get; set; }

        ///<summary>
        ///Customer's description.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's description.", Name="Description", ParameterType="body")]
        public string Description { get; set; }

        ///<summary>
        ///Customer's email. Overrides user's email.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's email. Overrides user's email.", Name="Email", ParameterType="body")]
        public string Email { get; set; }

        ///<summary>
        ///Customer's city. Overrides user's city.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's city. Overrides user's city.", Name="City", ParameterType="body")]
        public string City { get; set; }

        ///<summary>
        ///Customer's country. Overrides user's country.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's country. Overrides user's country.", Name="CountryCode", ParameterType="body")]
        public string CountryCode { get; set; }

        ///<summary>
        ///Customer's address 1. Overrides user's address 1.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's address 1. Overrides user's address 1.", Name="Address1", ParameterType="body")]
        public string Address { get; set; }

        ///<summary>
        ///Customer's address 2. Overrides user's address 2.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's address 2. Overrides user's address 2.", Name="Address2", ParameterType="body")]
        public string Address2 { get; set; }

        ///<summary>
        ///Customer's postal code. Overrides user's postal code.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's postal code. Overrides user's postal code.", Name="PostalCode", ParameterType="body")]
        public string PostalCode { get; set; }

        ///<summary>
        ///Customer's state. Overrides user's area.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's state. Overrides user's area.", Name="Area", ParameterType="body")]
        public string Area { get; set; }

        ///<summary>
        ///Additional key, value data.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Additional key, value data.", Name="Meta", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }
    }

    public class CreateCustomerResponse
        : ResponseBase<Customer>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/orders/{Id}/decta/pay", "POST")]
    [Route("/{version}/payments/orders/{Id}/decta/pay", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class CreateDectaTransactionRequest
        : CodeMashRequestBase, IReturn<CreateDectaTransactionResponse>
    {
        ///<summary>
        ///Order ID to pay for.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Order ID to pay for.", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///Customer ID to whom belongs this payment method.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer ID to whom belongs this payment method.", IsRequired=true, Name="CustomerId", ParameterType="body")]
        public string CustomerId { get; set; }

        ///<summary>
        ///Should try to charge default card
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should try to charge default card", Name="ChargeCard", ParameterType="body")]
        public bool ChargeCard { get; set; }
    }

    public class CreateDectaTransactionResponse
        : ResponseBase<DectaOrder>
    {
    }

    ///<summary>
    ///Registers notification device which can receive push notifications
    ///</summary>
    [Route("/notifications/push/devices", "POST")]
    [Route("/{version}/notifications/push/devices", "POST")]
    [Api(Description="Registers notification device which can receive push notifications")]
    [DataContract]
    public class CreateDeviceRequest
        : CodeMashRequestBase, IReturn<CreateDeviceResponse>
    {
        public CreateDeviceRequest()
        {
            Meta = new Dictionary<string, string>{};
        }

        ///<summary>
        ///UserId. Parameter can be nullable, but if you provide it, device will be combined with the one of membership users.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="UserId. Parameter can be nullable, but if you provide it, device will be combined with the one of membership users.", Name="UserId", ParameterType="form")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///Device operating system
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device operating system", Name="OperatingSystem", ParameterType="body")]
        public string OperatingSystem { get; set; }

        ///<summary>
        ///Device brand
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device brand", Name="Brand", ParameterType="body")]
        public string Brand { get; set; }

        ///<summary>
        ///Device name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device name", Name="DeviceName", ParameterType="body")]
        public string DeviceName { get; set; }

        ///<summary>
        ///Device timezone, expects to get a TZ database name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device timezone, expects to get a TZ database name", Name="TimeZone", ParameterType="body")]
        public string TimeZone { get; set; }

        ///<summary>
        ///Device language, expects to get a 2 letter identifier
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device language, expects to get a 2 letter identifier", Name="Language", ParameterType="body")]
        public string Language { get; set; }

        ///<summary>
        ///Device locale
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device locale", Name="Locale", ParameterType="body")]
        public string Locale { get; set; }

        ///<summary>
        ///Meta information that comes from device.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Meta information that comes from device.", Name="Meta", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }
    }

    public class CreateDeviceResponse
        : ResponseBase<Device>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/discounts", "POST")]
    [Route("/{version}/payments/discounts", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class CreateDiscountRequest
        : CodeMashDbRequestBase, IReturn<CreateDiscountResponse>
    {
        public CreateDiscountRequest()
        {
            Boundaries = new List<PaymentDiscountBoundary>{};
            Records = new List<string>{};
            CategoryValues = new List<string>{};
            PaymentAccounts = new List<string>{};
            Users = new List<string>{};
            Emails = new List<string>{};
        }

        ///<summary>
        ///Type of the discount (Coupon or Discount).
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Type of the discount (Coupon or Discount).", IsRequired=true, Name="Type", ParameterType="body")]
        public string Type { get; set; }

        ///<summary>
        ///Unique discount code.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Unique discount code.", Name="Code", ParameterType="body")]
        public string Code { get; set; }

        ///<summary>
        ///Display name of the discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Display name of the discount.", Name="DisplayName", ParameterType="body")]
        public string DisplayName { get; set; }

        ///<summary>
        ///Start date of being active.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="Start date of being active.", Name="ValidFrom", ParameterType="body")]
        public long? ValidFrom { get; set; }

        ///<summary>
        ///End date of being active.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="End date of being active.", Name="ValidUntil", ParameterType="body")]
        public long? ValidUntil { get; set; }

        ///<summary>
        ///Restriction type of discount. Can be Fixed, Percentage, Price or Quantity.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Restriction type of discount. Can be Fixed, Percentage, Price or Quantity.", Name="RestrictionType", ParameterType="body")]
        public string RestrictionType { get; set; }

        ///<summary>
        ///Discount amount for Fixed or Percentage restriction types.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="double", Description="Discount amount for Fixed or Percentage restriction types.", Name="Amount", ParameterType="body")]
        public double? Amount { get; set; }

        ///<summary>
        ///Discount amounts for Price or Quantity restriction types.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Discount amounts for Price or Quantity restriction types.", Name="Boundaries", ParameterType="body")]
        public List<PaymentDiscountBoundary> Boundaries { get; set; }

        ///<summary>
        ///Target type for specific records. Can be All, Category, Individual.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Target type for specific records. Can be All, Category, Individual.", Name="TargetType", ParameterType="body")]
        public string TargetType { get; set; }

        ///<summary>
        ///Records for Individual target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Records for Individual target type.", Name="Records", ParameterType="body")]
        public List<string> Records { get; set; }

        ///<summary>
        ///Collection field for Category target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Collection field for Category target type.", Name="CategoryField", ParameterType="body")]
        public string CategoryField { get; set; }

        ///<summary>
        ///Values for Category target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Values for Category target type.", Name="CategoryValues", ParameterType="body")]
        public List<string> CategoryValues { get; set; }

        ///<summary>
        ///Limitations for discounts to be used only with certain payment accounts.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Limitations for discounts to be used only with certain payment accounts.", Name="PaymentAccounts", ParameterType="body")]
        public List<string> PaymentAccounts { get; set; }

        ///<summary>
        ///Users who can redeem this discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Users who can redeem this discount.", Name="Users", ParameterType="body")]
        public List<string> Users { get; set; }

        ///<summary>
        ///Emails for potential users who can redeem this discount. When user registers with one of the provided emails, he will be allowed to use this discount. Doesn't work with existing users.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Emails for potential users who can redeem this discount. When user registers with one of the provided emails, he will be allowed to use this discount. Doesn't work with existing users.", Name="Users", ParameterType="body")]
        public List<string> Emails { get; set; }

        ///<summary>
        ///Amount of times that the same user can redeem. Default - 1.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of times that the same user can redeem. Default - 1.", Format="int32", Name="UserCanRedeem", ParameterType="body")]
        public int? UserCanRedeem { get; set; }

        ///<summary>
        ///Amount of times in total this discount can be redeemed.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of times in total this discount can be redeemed.", Format="int32", Name="TotalCanRedeem", ParameterType="body")]
        public int? TotalCanRedeem { get; set; }

        ///<summary>
        ///Should the discount be enabled. Default - true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should the discount be enabled. Default - true.", Name="Enabled", ParameterType="body")]
        public bool Enabled { get; set; }

        ///<summary>
        ///Should the discount be combined with other discounts. Default - true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should the discount be combined with other discounts. Default - true.", Name="CombineDiscounts", ParameterType="body")]
        public bool CombineDiscounts { get; set; }
    }

    public class CreateDiscountResponse
        : ResponseBase<Discount>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/orders/{Id}/kevin/pay", "POST")]
    [Route("/{version}/payments/orders/{Id}/kevin/pay", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class CreateKevinTransactionRequest
        : CodeMashRequestBase, IReturn<CreateStripeTransactionResponse>
    {
        ///<summary>
        ///Order ID to pay for.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Order ID to pay for.", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///Id of the payment method.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of the payment method.", Name="PaymentMethodId", ParameterType="body")]
        public string PaymentMethodId { get; set; }

        ///<summary>
        ///Available values: 'card' or 'bank'. Used if payment method not selected
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Available values: 'card' or 'bank'. Used if payment method not selected", Name="PreferredPaymentMethod", ParameterType="body")]
        public string PreferredPaymentMethod { get; set; }
    }

    ///<summary>
    ///Logs
    ///</summary>
    [Route("/logs", "POST")]
    [Route("/{version}/logs", "POST")]
    [Api(Description="Logs")]
    [DataContract]
    public class CreateLogRequest
        : CodeMashRequestBase, IReturn<CreateLogResponse>
    {
        public CreateLogRequest()
        {
            Items = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Message to log.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Message to log.", IsRequired=true, Name="Message", ParameterType="body")]
        public string Message { get; set; }

        ///<summary>
        ///Custom items providing information.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Custom items providing information.", Name="Items", ParameterType="body")]
        public Dictionary<string, string> Items { get; set; }

        ///<summary>
        ///Severity level of the log.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Severity level of the log.", Name="Level", ParameterType="body")]
        public string Level { get; set; }
    }

    public class CreateLogResponse
        : ResponseBase<string>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/orders", "POST")]
    [Route("/{version}/payments/orders", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class CreateOrderRequest
        : CodeMashRequestBase, IReturn<CreateOrderResponse>
    {
        public CreateOrderRequest()
        {
            Lines = new List<OrderLineInput>{};
            Coupons = new List<string>{};
            Meta = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Payment account ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Payment account ID.", IsRequired=true, Name="AccountId", ParameterType="body")]
        public Guid AccountId { get; set; }

        ///<summary>
        ///Order schema ID. Must match one of your order schemas including user zone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Order schema ID. Must match one of your order schemas including user zone.", IsRequired=true, Name="OrderSchemaId", ParameterType="body")]
        public Guid OrderSchemaId { get; set; }

        ///<summary>
        ///User ID. Requires administrator permission.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User ID. Requires administrator permission.", Name="UserId", ParameterType="body")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///Order lines.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Order lines.", IsRequired=true, Name="Lines", ParameterType="body")]
        public List<OrderLineInput> Lines { get; set; }

        ///<summary>
        ///Coupon discounts.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Coupon discounts.", Name="Coupons", ParameterType="body")]
        public List<string> Coupons { get; set; }

        ///<summary>
        ///If true, will consider a buyer as a guest user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, will consider a buyer as a guest user.", Name="AsGuest", ParameterType="body")]
        public bool AsGuest { get; set; }

        ///<summary>
        ///Customer details. Should be provided if this is a guest. If this is a user, then this will override user details.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Object", Description="Customer details. Should be provided if this is a guest. If this is a user, then this will override user details.", Name="BuyerDetails", ParameterType="body")]
        public OrderCustomerInput CustomerDetails { get; set; }

        ///<summary>
        ///Consider this as a test order
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Consider this as a test order", Name="IsTest", ParameterType="body")]
        public bool IsTest { get; set; }

        ///<summary>
        ///Additional information for order
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Object", Description="Additional information for order", Name="Meta", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }
    }

    public class CreateOrderResponse
        : ResponseBase<Order>
    {
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/password/reset/token", "POST")]
    [Route("/{version}/membership/users/password/reset/token", "POST")]
    [Api(Description="Membership")]
    [DataContract]
    public class CreatePasswordResetRequest
        : CodeMashRequestBase, IReturn<CreatePasswordResetResponse>
    {
        ///<summary>
        ///User login email
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User login email", IsRequired=true, Name="Email", ParameterType="body")]
        public string Email { get; set; }
    }

    public class CreatePasswordResetResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/orders/{Id}/paysera/pay", "POST")]
    [Route("/{version}/payments/orders/{Id}/paysera/pay", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class CreatePayseraTransactionRequest
        : CodeMashRequestBase, IReturn<CreatePayseraTransactionResponse>
    {
        ///<summary>
        ///Order ID to pay for.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Order ID to pay for.", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///To choose which mode to use from payment settings.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="To choose which mode to use from payment settings.", IsRequired=true, Name="Mode", ParameterType="body")]
        public string Mode { get; set; }
    }

    public class CreatePayseraTransactionResponse
        : ResponseBase<string>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/orders/{Id}/stripe/pay", "POST")]
    [Route("/{version}/payments/orders/{Id}/stripe/pay", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class CreateStripeTransactionRequest
        : CodeMashRequestBase, IReturn<CreateStripeTransactionResponse>
    {
        ///<summary>
        ///Order ID to pay for.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Order ID to pay for.", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///Payment method ID to use.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method ID to use.", IsRequired=true, Name="PaymentMethodId", ParameterType="body")]
        public string PaymentMethodId { get; set; }

        ///<summary>
        ///Customer ID to whom belongs this payment method.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer ID to whom belongs this payment method.", IsRequired=true, Name="CustomerId", ParameterType="body")]
        public string CustomerId { get; set; }
    }

    public class CreateStripeTransactionResponse
        : ResponseBase<StripePaymentIntent>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers/{customerId}/subscriptions", "POST")]
    [Route("/{version}/payments/customers/{customerId}/subscriptions", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class CreateSubscriptionRequest
        : CodeMashRequestBase, IReturn<CreateSubscriptionResponse>
    {
        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", IsRequired=true, Name="CustomerId", ParameterType="path")]
        public string CustomerId { get; set; }

        ///<summary>
        ///Subscription plan ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Subscription plan ID.", IsRequired=true, Name="PlanId", ParameterType="body")]
        public Guid PlanId { get; set; }

        ///<summary>
        ///Payment method's ID. If not provided will use default.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's ID. If not provided will use default.", Name="PaymentMethodId", ParameterType="body")]
        public string PaymentMethodId { get; set; }

        ///<summary>
        ///Discount coupon ID if supported.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Discount coupon ID if supported.", Name="Coupon", ParameterType="body")]
        public string Coupon { get; set; }
    }

    public class CreateSubscriptionResponse
        : ResponseBase<Subscription>
    {
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/deactivate/token", "POST")]
    [Route("/{version}/membership/users/deactivate/token", "POST")]
    [Api(Description="Membership")]
    [DataContract]
    public class CreateUserDeactivationRequest
        : CodeMashRequestBase, IReturnVoid
    {
    }

    ///<summary>
    ///Authentication
    ///</summary>
    [Route("/auth/credentials", "GET POST")]
    [Route("/{version}/auth/credentials", "GET POST")]
    [Api(Description="Authentication")]
    [DataContract]
    public class CredentialsAuthenticationRequest
        : CodeMashRequestBase, IReturn<CredentialsAuthenticationResponse>
    {
        ///<summary>
        ///User's login e-mail address.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's login e-mail address.", IsRequired=true, Name="UserName", ParameterType="body")]
        public string UserName { get; set; }

        ///<summary>
        ///User's login password.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's login password.", IsRequired=true, Name="Password", ParameterType="body")]
        public string Password { get; set; }
    }

    public class CredentialsAuthenticationResponse
        : ResponseBase<AuthResponse>
    {
    }

    public class Customer
    {
        public Customer()
        {
            PaymentMethods = new List<PaymentMethod>{};
            Subscriptions = new List<Subscription>{};
            Meta = new Dictionary<string, string>{};
        }

        public string Id { get; set; }
        public string CreatedOn { get; set; }
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string Area { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ProjectId { get; set; }
        public string PaymentAccountId { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public Dictionary<string, string> Meta { get; set; }
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/deactivate", "POST")]
    [Route("/{version}/membership/users/deactivate", "POST")]
    [Api(Description="Membership")]
    [DataContract]
    public class DeactivateUserRequest
        : RequestBase, IReturnVoid
    {
        ///<summary>
        ///Secret token received by email for deactivation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Secret token received by email for deactivation", IsRequired=true, Name="Token", ParameterType="body")]
        public string Token { get; set; }

        ///<summary>
        ///Password for confirmation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Password for confirmation", IsRequired=true, Name="Password", ParameterType="body")]
        public string Password { get; set; }
    }

    public class DectaOrder
    {
        public string Id { get; set; }
        public bool Paid { get; set; }
        public string FullPageCheckout { get; set; }
        public string IframeCheckout { get; set; }
        public string DirectPost { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers/{id}", "DELETE")]
    [Route("/{version}/payments/customers/{id}", "DELETE")]
    [Api(Description="Payments")]
    [DataContract]
    public class DeleteCustomerRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///Customer's ID to delete.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID to delete.", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }
    }

    ///<summary>
    ///Deletes metadata of the device
    ///</summary>
    [Route("/notifications/push/devices/{Id}/metadata", "PATCH")]
    [Route("/{version}/notifications/push/devices/{Id}/metadata", "PATCH")]
    [Api(Description="Deletes metadata of the device")]
    [DataContract]
    public class DeleteDeviceMetaRequest
        : CodeMashRequestBase, IReturn<DeleteDeviceMetaResponse>
    {
        public DeleteDeviceMetaRequest()
        {
            Keys = new List<string>{};
        }

        ///<summary>
        ///Device Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device Id", Name="Id", ParameterType="path")]
        public Guid Id { get; set; }

        ///<summary>
        ///Keys to be deleted
        ///</summary>
        [DataMember]
        [ApiMember(DataType="List", Description="Keys to be deleted", Name="Keys", ParameterType="body")]
        public List<string> Keys { get; set; }

        ///<summary>
        ///Delete all keys
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Delete all keys", Name="DeleteAllKeys", ParameterType="form")]
        public bool DeleteAllKeys { get; set; }
    }

    public class DeleteDeviceMetaResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Deletes the device from push notifications recipients list.
    ///</summary>
    [Route("/notifications/push/devices/{Id}", "DELETE")]
    [Route("/{version}/notifications/push/devices/{Id}", "DELETE")]
    [Api(Description="Deletes the device from push notifications recipients list.")]
    [DataContract]
    public class DeleteDeviceRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///Device Id or device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device Id or device key", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }
    }

    ///<summary>
    ///Deletes push notification token
    ///</summary>
    [Route("/notifications/push/devices/{Id}/token", "DELETE")]
    [Route("/{version}/notifications/push/devices/{Id}/token", "DELETE")]
    [Api(Description="Deletes push notification token")]
    [DataContract]
    public class DeleteDeviceTokenRequest
        : CodeMashRequestBase, IReturn<DeleteDeviceTokenResponse>
    {
        ///<summary>
        ///Device Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device Id", IsRequired=true, Name="Id", ParameterType="path")]
        public Guid Id { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public string DeviceKey { get; set; }
    }

    public class DeleteDeviceTokenResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/discounts/{id}", "DELETE")]
    [Route("/{version}/payments/discounts/{id}", "DELETE")]
    [Api(Description="Payments")]
    [DataContract]
    public class DeleteDiscountRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///Id of discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of discount.", IsRequired=true, Name="Id", ParameterType="body")]
        public string Id { get; set; }
    }

    ///<summary>
    ///Deletes an email from queue
    ///</summary>
    [Route("/notifications/email/{id}", "DELETE")]
    [Route("/{version}/notifications/email/{id}", "DELETE")]
    [Route("/{version}/notifications/emails/{id}", "DELETE")]
    [Api(Description="Deletes an email from queue")]
    [DataContract]
    public class DeleteEmailRequest
        : CodeMashRequestBase, IReturn<DeleteEmailResponse>
    {
        ///<summary>
        ///ID of an email to delete
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of an email to delete", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }
    }

    public class DeleteEmailResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///File services
    ///</summary>
    [Route("/files/{fileId}", "DELETE")]
    [Route("/{version}/files/{fileId}", "DELETE")]
    [Api(Description="File services")]
    [DataContract]
    public class DeleteFileRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///ID of a file to delete
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of a file to delete", IsRequired=true, Name="FileId", ParameterType="path")]
        public string FileId { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/bulk", "DELETE")]
    [Route("/{version}/db/{CollectionName}/bulk", "DELETE")]
    [Api(Description="Database services")]
    [DataContract]
    public class DeleteManyRequest
        : CodeMashDbRequestBase, IReturn<DeleteManyResponse>
    {
        ///<summary>
        ///Deletes all the documents by a specified filter (in JSON format).
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Deletes all the documents by a specified filter (in JSON format).", Name="Filter", ParameterType="form")]
        public string Filter { get; set; }

        ///<summary>
        ///Ignore the delete trigger when executing mass deletion. When this property is set to "true", all delete triggers won't be executed.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Ignore the delete trigger when executing mass deletion. When this property is set to \"true\", all delete triggers won't be executed.", Name="IgnoreTriggers", ParameterType="form")]
        public bool IgnoreTriggers { get; set; }
    }

    public class DeleteManyResponse
        : ResponseBase<DeleteResult>
    {
    }

    ///<summary>
    ///Deletes the push notification from queue
    ///</summary>
    [Route("/notifications/push/{id}", "DELETE")]
    [Route("/{version}/notifications/push/{id}", "DELETE")]
    [Api(Description="Deletes the push notification from queue")]
    [DataContract]
    public class DeleteNotificationRequest
        : CodeMashRequestBase, IReturn<DeleteNotificationResponse>
    {
        ///<summary>
        ///Notification Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Notification Id", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public string DeviceKey { get; set; }
    }

    public class DeleteNotificationResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}", "DELETE")]
    [Route("/{version}/db/{CollectionName}", "DELETE")]
    [Route("/{version}/db/{CollectionName}/{Id}", "DELETE")]
    [Api(Description="Database services")]
    [DataContract]
    public class DeleteOneRequest
        : CodeMashDbRequestBase, IReturn<DeleteOneResponse>
    {
        ///<summary>
        ///Id of a record to delete. Required if filter is empty.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of a record to delete. Required if filter is empty.", IsRequired=true, Name="Id", ParameterType="path", Route="/{version}/db/{CollectionName}/{Id}")]
        public string Id { get; set; }

        ///<summary>
        ///Set the filter (in JSON format) to delete the first document found in the collection. Required when Id is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Set the filter (in JSON format) to delete the first document found in the collection. Required when Id is not set.", Name="Filter", ParameterType="form", Route="/{version}/db/{CollectionName}")]
        public string Filter { get; set; }

        ///<summary>
        ///Ignore the delete trigger when executing one document deletion. When this property is set to "true", all delete triggers won't be executed.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Ignore the delete trigger when executing one document deletion. When this property is set to \"true\", all delete triggers won't be executed.", Name="IgnoreTriggers", ParameterType="form")]
        public bool IgnoreTriggers { get; set; }
    }

    public class DeleteOneResponse
        : ResponseBase<DeleteResult>
    {
    }

    [DataContract]
    public class DeleteResult
    {
        [DataMember(Name="deletedCount")]
        public long DeletedCount { get; set; }

        [DataMember(Name="isAcknowledged")]
        public bool IsAcknowledged { get; set; }
    }

    ///<summary>
    ///Deletes user
    ///</summary>
    [Route("/membership/users/{Id}", "DELETE")]
    [Route("/{version}/membership/users/{Id}", "DELETE")]
    [Api(Description="Deletes user")]
    [DataContract]
    public class DeleteUserRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///ID of user to be deleted
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="ID of user to be deleted", IsRequired=true, Name="Id", ParameterType="path")]
        public Guid Id { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/methods/{id}", "DELETE")]
    [Route("/{version}/payments/methods/{id}", "DELETE")]
    [Api(Description="Payments")]
    [DataContract]
    public class DetachPaymentMethodRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///Payment method's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's ID.", IsRequired=true, Name="Id", ParameterType="query")]
        public string Id { get; set; }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", Name="CustomerId", ParameterType="query")]
        public string CustomerId { get; set; }
    }

    public class Device
    {
        public Device()
        {
            Meta = new Dictionary<string, string>{};
        }

        public string Id { get; set; }
        public string CreatedOn { get; set; }
        public PushNotificationToken Token { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string OperatingSystem { get; set; }
        public string Brand { get; set; }
        public string DeviceName { get; set; }
        public string TimeZone { get; set; }
        public string Language { get; set; }
        public string Locale { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public long TotalNotifications { get; set; }
        public string DeviceKey { get; set; }
        public string AccountId { get; set; }
    }

    public class Discount
    {
        public Discount()
        {
            Boundaries = new List<PaymentDiscountBoundary>{};
            Records = new List<string>{};
            CategoryValues = new List<string>{};
            PaymentAccounts = new List<string>{};
            Users = new List<string>{};
            Emails = new List<string>{};
        }

        public string Id { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string ValidFrom { get; set; }
        public string ValidUntil { get; set; }
        public string SchemaId { get; set; }
        public string Cluster { get; set; }
        public string RestrictionType { get; set; }
        public double? Amount { get; set; }
        public List<PaymentDiscountBoundary> Boundaries { get; set; }
        public string TargetType { get; set; }
        public List<string> Records { get; set; }
        public string CategoryField { get; set; }
        public List<string> CategoryValues { get; set; }
        public List<string> PaymentAccounts { get; set; }
        public List<string> Users { get; set; }
        public List<string> Emails { get; set; }
        public int? UserCanRedeem { get; set; }
        public int? TotalCanRedeem { get; set; }
        public bool Enabled { get; set; }
        public bool CombineDiscounts { get; set; }
    }

    public class DiscountAll
    {
        public DiscountAll()
        {
            Lines = new List<DiscountLine>{};
        }

        public List<DiscountLine> Lines { get; set; }
        public decimal Discount { get; set; }
    }

    public class DiscountCategory
    {
        public DiscountCategory()
        {
            Lines = new List<DiscountLine>{};
        }

        public string Category { get; set; }
        public List<DiscountLine> Lines { get; set; }
        public decimal Discount { get; set; }
    }

    public class DiscountIndividualLine
    {
        public string RecordId { get; set; }
        public string Variation { get; set; }
        public decimal Discount { get; set; }
    }

    public class DiscountLine
    {
        public string RecordId { get; set; }
        public string Variation { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/distinct", "GET POST")]
    [Route("/{version}/db/{CollectionName}/distinct", "GET POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class DistinctRequest
        : CodeMashDbRequestBase, IReturn<DistinctResponse>
    {
        ///<summary>
        ///The field for which to return distinct values
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The field for which to return distinct values", IsRequired=true, Name="Field", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="string", Description="The field for which to return distinct values", IsRequired=true, Name="Field", ParameterType="form", Verb="POST")]
        public string Field { get; set; }

        ///<summary>
        ///A query that specifies the documents from which to retrieve the distinct values
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies the documents from which to retrieve the distinct values", Name="Filter", ParameterType="form", Verb="POST")]
        public string Filter { get; set; }
    }

    public class DistinctResponse
        : ResponseBase<List<Object>>
    {
    }

    ///<summary>
    ///File services
    ///</summary>
    [Route("/files", "GET")]
    [Route("/{version}/files", "GET")]
    [Api(Description="File services")]
    [DataContract]
    public class DownloadFileRequest
        : CodeMashRequestBase, IReturn<HttpResult>
    {
        ///<summary>
        ///ID of a file to download
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of a file to download", IsRequired=true, Name="FileId", ParameterType="query")]
        public string FileId { get; set; }

        ///<summary>
        ///Optimization code of optimized image
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Optimization code of optimized image", Name="Optimization", ParameterType="query")]
        public string Optimization { get; set; }
    }

    ///<summary>
    ///Executes a function
    ///</summary>
    [Route("/serverless/functions/{Id}/execute", "GET POST")]
    [Route("/{version}/serverless/functions/{Id}/execute", "GET POST")]
    [Api(Description="Executes a function")]
    [DataContract]
    public class ExecuteFunctionRequest
        : CodeMashRequestBase, IReturn<ExecuteFunctionResponse>
    {
        public ExecuteFunctionRequest()
        {
            Meta = new Dictionary<string, string>{};
            Tokens = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Function Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Function Id", IsRequired=true, Name="Id", ParameterType="path")]
        public Guid Id { get; set; }

        ///<summary>
        ///Parameters of to pass into function
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Parameters of to pass into function", Name="Template", ParameterType="body")]
        public string Data { get; set; }

        ///<summary>
        ///Additional parameters for specific functions
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Additional parameters for specific functions", Name="Meta", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }

        ///<summary>
        ///Tokens to inject into queries
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Tokens to inject into queries", Name="Tokens", ParameterType="body")]
        public Dictionary<string, string> Tokens { get; set; }

        ///<summary>
        ///Version or Alias of a function
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Version or Alias of a function", Name="Qualifier", ParameterType="body")]
        public string Qualifier { get; set; }
    }

    public class ExecuteFunctionResponse
        : ResponseBase<string>
    {
    }

    ///<summary>
    ///Authentication
    ///</summary>
    [Route("/auth/facebook", "GET POST")]
    [Route("/{version}/auth/facebook", "GET POST")]
    [Api(Description="Authentication")]
    [DataContract]
    public class FacebookAuthenticationRequest
        : CodeMashRequestBase, IReturn<FacebookAuthenticationResponse>, IOAuthRequest
    {
        ///<summary>
        ///Mode to use for authentication
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Mode to use for authentication", Name="Mode", ParameterType="query")]
        public string Mode { get; set; }

        ///<summary>
        ///Code received from Facebook services
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Code received from Facebook services", Name="Code", ParameterType="query")]
        public string Code { get; set; }

        ///<summary>
        ///State received with a code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="State received with a code", Name="State", ParameterType="query")]
        public string State { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessToken", ParameterType="query")]
        public string AccessToken { get; set; }
    }

    public class FacebookAuthenticationResponse
        : ResponseBase<AuthResponse>
    {
    }

    public class File
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string ModifiedOn { get; set; }
        public string CreatedOn { get; set; }
        public string UniqueName { get; set; }
        public int Enumerator { get; set; }
        public string OriginalName { get; set; }
        public string Directory { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public bool IsPublic { get; set; }
        public bool IsParentPublic { get; set; }
        public string PublicUrl { get; set; }
    }

    public class FileDetails
    {
        public string Id { get; set; }
        public string Directory { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/{Id}", "GET POST")]
    [Route("/db/{CollectionName}/findOne", "GET POST")]
    [Route("/{version}/db/{CollectionName}/findOne", "GET")]
    [Route("/{version}/db/{CollectionName}/{Id}", "GET POST")]
    [Route("/{version}/db/{CollectionName}/findOne", "POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class FindOneRequest
        : CodeMashDbRequestBase, IReturn<FindOneResponse>
    {
        public FindOneRequest()
        {
            ReferencedFields = new List<ReferencingField>{};
        }

        ///<summary>
        ///Id of a record. Required if filter is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of a record. Required if filter is not set.", IsRequired=true, Name="Id", ParameterType="path", Route="/{version}/db/{CollectionName}/{Id}")]
        public string Id { get; set; }

        ///<summary>
        ///Set the filter (in JSON format) to find the first document that match search criteria in the collection. Required when Id is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Set the filter (in JSON format) to find the first document that match search criteria in the collection. Required when Id is not set.", IsRequired=true, Name="Filter", ParameterType="form", Route="/{version}/db/{CollectionName}/findOne", Verb="POST")]
        public string Filter { get; set; }

        ///<summary>
        ///Use projection (in JSON format) to return only fields that are required to work with. This is useful to reduce the amount of the returned fields, have less complexity on the frontend and lower network traffic.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Use projection (in JSON format) to return only fields that are required to work with. This is useful to reduce the amount of the returned fields, have less complexity on the frontend and lower network traffic.", Name="Projection", ParameterType="form", Verb="POST")]
        public string Projection { get; set; }

        ///<summary>
        ///Use the IncludeSchema  property when you want to have schema definition in the API response as well. This is useful when you have dynamic data rendering and want to have context over your data structure and how it should be displayed. By default schema is excluded.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Use the IncludeSchema  property when you want to have schema definition in the API response as well. This is useful when you have dynamic data rendering and want to have context over your data structure and how it should be displayed. By default schema is excluded.", Name="ExcludeSchema", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="Use the IncludeSchema  property when you want to have schema definition in the API response as well. This is useful when you have dynamic data rendering and want to have context over your data structure and how it should be displayed. By default schema is excluded.", Name="ExcludeSchema", ParameterType="form", Verb="POST")]
        public bool IncludeSchema { get; set; }

        ///<summary>
        ///Set ExcludeCulture property to "true" when you want to return all the data translations from translatable fields. This is useful when you want to take care about translations in the front-end side. E.g.: when you want to enter product description in your project supported languages.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Set ExcludeCulture property to \"true\" when you want to return all the data translations from translatable fields. This is useful when you want to take care about translations in the front-end side. E.g.: when you want to enter product description in your project supported languages.", Name="ExcludeCulture", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="Set ExcludeCulture property to \"true\" when you want to return all the data translations from translatable fields. This is useful when you want to take care about translations in the front-end side. E.g.: when you want to enter product description in your project supported languages.", Name="ExcludeCulture", ParameterType="form", Verb="POST")]
        public bool ExcludeCulture { get; set; }

        ///<summary>
        ///If your collection record has relationship with the Users collection from the Membership module, you can set IncludeUserNames property to "true" to get full user name and id information altogether without making any extra roundtrip to the server.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the Users collection from the Membership module, you can set IncludeUserNames property to \"true\" to get full user name and id information altogether without making any extra roundtrip to the server.", Name="IncludeUserNames", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the Users collection from the Membership module, you can set IncludeUserNames property to \"true\" to get full user name and id information altogether without making any extra roundtrip to the server.", Name="IncludeUserNames", ParameterType="form", Verb="POST")]
        public bool IncludeUserNames { get; set; }

        ///<summary>
        ///If your collection record has relationship with the system Roles from the Membership module, you can set IncludeRoleNames property to "true" to get full role name and id information altogether without making any extra roundtrip to the server.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the system Roles from the Membership module, you can set IncludeRoleNames property to \"true\" to get full role name and id information altogether without making any extra roundtrip to the server.", Name="IncludeRoleNames", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the system Roles from the Membership module, you can set IncludeRoleNames property to \"true\" to get full role name and id information altogether without making any extra roundtrip to the server.", Name="IncludeRoleNames", ParameterType="form", Verb="POST")]
        public bool IncludeRoleNames { get; set; }

        ///<summary>
        ///If your collection record has relationship with other collections from the Database module, you can set IncludeCollectionNames property to "true" to get display name and id information altogether without making any extra roundtrip to the server.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with other collections from the Database module, you can set IncludeCollectionNames property to \"true\" to get display name and id information altogether without making any extra roundtrip to the server.", Name="IncludeCollectionNames", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with other collections from the Database module, you can set IncludeCollectionNames property to \"true\" to get display name and id information altogether without making any extra roundtrip to the server.", Name="IncludeCollectionNames", ParameterType="form", Verb="POST")]
        public bool IncludeCollectionNames { get; set; }

        ///<summary>
        ///If your collection record has relationship with the taxonomy terms from the Database module, you can set IncludeTermNames property to "true" to get term name and id information altogether without making any extra roundtrip to the server.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the taxonomy terms from the Database module, you can set IncludeTermNames property to \"true\" to get term name and id information altogether without making any extra roundtrip to the server.", Name="IncludeTermNames", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the taxonomy terms from the Database module, you can set IncludeTermNames property to \"true\" to get term name and id information altogether without making any extra roundtrip to the server.", Name="IncludeTermNames", ParameterType="query", Verb="POST")]
        public bool IncludeTermNames { get; set; }

        ///<summary>
        ///If your collection record has relationship with other collections from the Database module, and you want to specify which fields should be returned from that referenced collection, you can pass projection (in JSON format) and return that information in one single roundtrip to the server.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="If your collection record has relationship with other collections from the Database module, and you want to specify which fields should be returned from that referenced collection, you can pass projection (in JSON format) and return that information in one single roundtrip to the server.", Name="ReferencedFields", ParameterType="body", Verb="POST")]
        public List<ReferencingField> ReferencedFields { get; set; }

        ///<summary>
        ///If true, then references are injected before your list queries are applied
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If true, then references are injected before your list queries are applied", Name="AddReferencesFirst", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="If true, then references are injected before your list queries are applied", Name="AddReferencesFirst", ParameterType="query", Verb="POST")]
        public bool AddReferencesFirst { get; set; }
    }

    public class FindOneResponse
        : ResponseBase<string>
    {
        public Schema Schema { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/find", "GET POST")]
    [Route("/{version}/db/{CollectionName}/find", "GET POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class FindRequest
        : CodeMashDbListRequestBase, IReturn<FindResponse>
    {
        public FindRequest()
        {
            ReferencedFields = new List<ReferencingField>{};
        }

        ///<summary>
        ///Use the IncludeSchema  property when you want to have schema definition in the API response as well. This is useful when you have dynamic data rendering and want to have context over your data structure and how it should be displayed. By default schema is excluded.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Use the IncludeSchema  property when you want to have schema definition in the API response as well. This is useful when you have dynamic data rendering and want to have context over your data structure and how it should be displayed. By default schema is excluded.", Name="IncludeSchema", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="Use the IncludeSchema  property when you want to have schema definition in the API response as well. This is useful when you have dynamic data rendering and want to have context over your data structure and how it should be displayed. By default schema is excluded.", Name="IncludeSchema", ParameterType="form", Verb="POST")]
        public bool IncludeSchema { get; set; }

        ///<summary>
        ///If your collection record has relationship with the Users collection from the Membership module, you can set IncludeUserNames property to "true" to get full user name and id information altogether without making any extra roundtrip to the server.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the Users collection from the Membership module, you can set IncludeUserNames property to \"true\" to get full user name and id information altogether without making any extra roundtrip to the server.", Name="IncludeUserNames", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the Users collection from the Membership module, you can set IncludeUserNames property to \"true\" to get full user name and id information altogether without making any extra roundtrip to the server.", Name="IncludeUserNames", ParameterType="form", Verb="POST")]
        public bool IncludeUserNames { get; set; }

        ///<summary>
        ///If your collection record has relationship with the system Roles from the Membership module, you can set IncludeRoleNames property to "true" to get full role name and id information altogether without making any extra roundtrip to the server.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the system Roles from the Membership module, you can set IncludeRoleNames property to \"true\" to get full role name and id information altogether without making any extra roundtrip to the server.", Name="IncludeRoleNames", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the system Roles from the Membership module, you can set IncludeRoleNames property to \"true\" to get full role name and id information altogether without making any extra roundtrip to the server.", Name="IncludeRoleNames", ParameterType="form", Verb="POST")]
        public bool IncludeRoleNames { get; set; }

        ///<summary>
        ///If your collection record has relationship with other collections from the Database module, you can set IncludeCollectionNames property to "true" to get display name and id information altogether without making any extra roundtrip to the server.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with other collections from the Database module, you can set IncludeCollectionNames property to \"true\" to get display name and id information altogether without making any extra roundtrip to the server.", Name="IncludeCollectionNames", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with other collections from the Database module, you can set IncludeCollectionNames property to \"true\" to get display name and id information altogether without making any extra roundtrip to the server.", Name="IncludeCollectionNames", ParameterType="form", Verb="POST")]
        public bool IncludeCollectionNames { get; set; }

        ///<summary>
        ///If your collection record has relationship with the taxonomy terms from the Database module, you can set IncludeTermNames property to "true" to get term name and id information altogether without making any extra roundtrip to the server.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the taxonomy terms from the Database module, you can set IncludeTermNames property to \"true\" to get term name and id information altogether without making any extra roundtrip to the server.", Name="IncludeTermNames", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="If your collection record has relationship with the taxonomy terms from the Database module, you can set IncludeTermNames property to \"true\" to get term name and id information altogether without making any extra roundtrip to the server.", Name="IncludeTermNames", ParameterType="query", Verb="POST")]
        public bool IncludeTermNames { get; set; }

        ///<summary>
        ///Set ExcludeCulture property to "true" when you want to return all the data translations from translatable fields. This is useful when you want to take care about translations in the front-end side. E.g.: when you want to enter product description in your project supported languages.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Set ExcludeCulture property to \"true\" when you want to return all the data translations from translatable fields. This is useful when you want to take care about translations in the front-end side. E.g.: when you want to enter product description in your project supported languages.", Name="ExcludeCulture", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="Set ExcludeCulture property to \"true\" when you want to return all the data translations from translatable fields. This is useful when you want to take care about translations in the front-end side. E.g.: when you want to enter product description in your project supported languages.", Name="ExcludeCulture", ParameterType="form", Verb="POST")]
        public bool ExcludeCulture { get; set; }

        ///<summary>
        ///If your collection record has relationship with other collections from the Database module, and you want to specify which fields should be returned from that referenced collection, you can pass projection (in JSON format) and return that information in one single roundtrip to the server.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="If your collection record has relationship with other collections from the Database module, and you want to specify which fields should be returned from that referenced collection, you can pass projection (in JSON format) and return that information in one single roundtrip to the server.", Name="ReferencedFields", ParameterType="body", Verb="POST")]
        public List<ReferencingField> ReferencedFields { get; set; }

        ///<summary>
        ///If true, then references are injected before your list queries are applied
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If true, then references are injected before your list queries are applied", Name="AddReferencesFirst", ParameterType="query", Verb="GET")]
        [ApiMember(DataType="boolean", Description="If true, then references are injected before your list queries are applied", Name="AddReferencesFirst", ParameterType="query", Verb="POST")]
        public bool AddReferencesFirst { get; set; }
    }

    public class FindResponse
        : ResponseBase<string>
    {
        public long TotalCount { get; set; }
        public Schema Schema { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/taxonomies/{CollectionName}/terms/{id}/children", "GET POST")]
    [Route("/db/taxonomies/{CollectionName}/terms/children", "GET POST")]
    [Route("/{version}/db/taxonomies/{CollectionName}/terms/{id}/children", "GET POST")]
    [Route("/{version}/db/taxonomies/{CollectionName}/terms/children", "GET POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class FindTermsChildrenRequest
        : CodeMashDbListRequestBase, IReturn<FindTermsChildrenResponse>
    {
        ///<summary>
        ///ID of a parent term. Required if filter is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of a parent term. Required if filter is not set.", Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///The selection criteria for the parent terms. Required if Id is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The selection criteria for the parent terms. Required if Id is not set.", Name="ParentFilter", ParameterType="query")]
        public string ParentFilter { get; set; }

        ///<summary>
        ///Prevent setting culture code from headers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Prevent setting culture code from headers", Name="ExcludeCulture", ParameterType="query")]
        public bool ExcludeCulture { get; set; }
    }

    public class FindTermsChildrenResponse
        : ResponseBase<List<Term>>
    {
        public long TotalCount { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/taxonomies/{CollectionName}/terms", "GET POST")]
    [Route("/{version}/db/taxonomies/{CollectionName}/terms", "GET POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class FindTermsRequest
        : CodeMashDbListRequestBase, IReturn<FindTermsResponse>
    {
        ///<summary>
        ///Includes taxonomy to response
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes taxonomy to response", Name="IncludeTaxonomy", ParameterType="query")]
        public bool IncludeTaxonomy { get; set; }

        ///<summary>
        ///Prevent setting culture code from headers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Prevent setting culture code from headers", Name="ExcludeCulture", ParameterType="query")]
        public bool ExcludeCulture { get; set; }
    }

    public class FindTermsResponse
        : ResponseBase<List<Term>>
    {
        public long TotalCount { get; set; }
        public Taxonomy Taxonomy { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/discounts/coupon", "GET POST")]
    [Route("/{version}/payments/discounts/coupon", "GET POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class GetApplicableCouponsRequest
        : CodeMashRequestBase, IReturn<GetApplicableCouponsResponse>
    {
        public GetApplicableCouponsRequest()
        {
            Codes = new List<string>{};
            Lines = new List<OrderLineInput>{};
        }

        ///<summary>
        ///Unique code of a discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Unique code of a discount.", IsRequired=true, Name="Code", ParameterType="query")]
        public List<string> Codes { get; set; }

        ///<summary>
        ///Order schema ID. Must match one of your order schemas including user zone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Order schema ID. Must match one of your order schemas including user zone.", Name="OrderSchemaId", ParameterType="query")]
        public Guid OrderSchemaId { get; set; }

        ///<summary>
        ///User ID. Requires administrator permission.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User ID. Requires administrator permission.", Name="UserId", ParameterType="body")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///If true, will consider a buyer as a guest user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, will consider a buyer as a guest user.", Name="AsGuest", ParameterType="body")]
        public bool AsGuest { get; set; }

        ///<summary>
        ///Order lines. Check which records are applicable for discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Order lines. Check which records are applicable for discount.", IsRequired=true, Name="Lines", ParameterType="query")]
        public List<OrderLineInput> Lines { get; set; }
    }

    public class GetApplicableCouponsResponse
        : ResponseBase<List<ApplicableDiscount>>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/discounts/applicable", "GET POST")]
    [Route("/{version}/payments/discounts/applicable", "GET POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class GetApplicableDiscountsRequest
        : CodeMashRequestBase, IReturn<GetApplicableDiscountsResponse>
    {
        public GetApplicableDiscountsRequest()
        {
            Lines = new List<OrderLineInput>{};
        }

        ///<summary>
        ///Order schema ID. Must match one of your order schemas including user zone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Order schema ID. Must match one of your order schemas including user zone.", IsRequired=true, Name="OrderSchemaId", ParameterType="query")]
        public Guid OrderSchemaId { get; set; }

        ///<summary>
        ///User ID. Requires administrator permission.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User ID. Requires administrator permission.", Name="UserId", ParameterType="body")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///If true, will consider a buyer as a guest user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, will consider a buyer as a guest user.", Name="AsGuest", ParameterType="body")]
        public bool AsGuest { get; set; }

        ///<summary>
        ///Order lines. Check which records are applicable for discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Order lines. Check which records are applicable for discount.", IsRequired=true, Name="Lines", ParameterType="query")]
        public List<OrderLineInput> Lines { get; set; }
    }

    public class GetApplicableDiscountsResponse
        : ResponseBase<List<ApplicableDiscount>>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers/{id}", "GET")]
    [Route("/{version}/payments/customers/{id}", "GET")]
    [Api(Description="Payments")]
    [DataContract]
    public class GetCustomerRequest
        : CodeMashRequestBase, IReturn<GetCustomerResponse>
    {
        ///<summary>
        ///Customer's ID to get.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID to get.", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///If true, id should be customer's provider Id.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, id should be customer's provider Id.", Name="UseProviderId", ParameterType="query")]
        public bool UseProviderId { get; set; }

        ///<summary>
        ///If true, id should be customer's user Id.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, id should be customer's user Id.", Name="UseUserId", ParameterType="query")]
        public bool UseUserId { get; set; }

        ///<summary>
        ///Required if UseUserId is set to true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Required if UseUserId is set to true.", Name="PaymentProvider", ParameterType="query")]
        public string PaymentProvider { get; set; }
    }

    public class GetCustomerResponse
        : ResponseBase<Customer>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers", "GET")]
    [Route("/{version}/payments/customers", "GET")]
    [Api(Description="Payments")]
    [DataContract]
    public class GetCustomersRequest
        : CodeMashListRequestBase, IReturn<GetCustomersResponse>
    {
        ///<summary>
        ///User's ID whose customers to get.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User's ID whose customers to get.", Name="UserId", ParameterType="body")]
        public Guid? UserId { get; set; }
    }

    public class GetCustomersResponse
        : ResponseBase<List<Customer>>
    {
        public long TotalCount { get; set; }
    }

    ///<summary>
    ///Gets the device which can receive push notifications.
    ///</summary>
    [Route("/notifications/push/devices/{Id}", "GET")]
    [Route("/{version}/notifications/push/devices/{Id}", "GET")]
    [Api(Description="Gets the device which can receive push notifications.")]
    [DataContract]
    public class GetDeviceRequest
        : CodeMashRequestBase, IReturn<GetDeviceResponse>
    {
        ///<summary>
        ///Device Id or device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device Id or device key", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }
    }

    public class GetDeviceResponse
        : ResponseBase<Device>
    {
    }

    ///<summary>
    ///Gets mobile devices
    ///</summary>
    [Route("/notifications/push/devices", "GET")]
    [Route("/{version}/notifications/push/devices", "GET")]
    [Api(Description="Gets mobile devices")]
    [DataContract]
    public class GetDevicesRequest
        : CodeMashListRequestBase, IReturn<GetDevicesResponse>
    {
        ///<summary>
        ///User ID of who's devices to get.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User ID of who's devices to get.", Name="UserId", ParameterType="query")]
        public Guid? UserId { get; set; }
    }

    public class GetDevicesResponse
        : ResponseBase<List<Device>>
    {
        public long TotalCount { get; set; }
    }

    ///<summary>
    ///File services
    ///</summary>
    [Route("/files/url", "GET")]
    [Route("/{version}/files/url", "GET")]
    [Api(Description="File services")]
    [DataContract]
    public class GetFileRequest
        : CodeMashRequestBase, IReturn<GetFileResponse>
    {
        ///<summary>
        ///ID of a file to download
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of a file to download", IsRequired=true, Name="FileId", ParameterType="query")]
        public string FileId { get; set; }

        ///<summary>
        ///Optimization code of optimized image
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Optimization code of optimized image", Name="Optimization", ParameterType="query")]
        public string Optimization { get; set; }
    }

    public class GetFileResponse
        : ResponseBase<string>
    {
        public string FileName { get; set; }
        public bool IsImage { get; set; }
        public File File { get; set; }
    }

    ///<summary>
    ///Gets the push notifications
    ///</summary>
    [Route("/notifications/push/{id}", "GET")]
    [Route("/{version}/notifications/push/{id}", "GET")]
    [Api(Description="Gets the push notifications")]
    [DataContract]
    public class GetNotificationRequest
        : CodeMashRequestBase, IReturn<GetNotificationResponse>
    {
        ///<summary>
        ///Notification Id.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Notification Id.", Name="Id", ParameterType="body")]
        public string Id { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public string DeviceKey { get; set; }
    }

    public class GetNotificationResponse
        : ResponseBase<PushNotification>
    {
    }

    ///<summary>
    ///Gets amount of push notifications
    ///</summary>
    [Route("/notifications/push/count", "GET")]
    [Route("/{version}/notifications/push/count", "GET")]
    [Api(Description="Gets amount of push notifications")]
    [DataContract]
    public class GetNotificationsCountRequest
        : CodeMashRequestBase, IReturn<GetNotificationsResponse>
    {
        ///<summary>
        ///Notifications delivered to specified user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Notifications delivered to specified user.", Name="UserId", ParameterType="body")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///Notifications delivered to specified device.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Notifications delivered to specified device.", Name="DeviceId", ParameterType="body")]
        public Guid? DeviceId { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public string DeviceKey { get; set; }

        ///<summary>
        ///Optional filter to count only matched notifications.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Optional filter to count only matched notifications.", IsRequired=true, Name="Filter", ParameterType="body")]
        public string Filter { get; set; }

        ///<summary>
        ///Optional filter to count only matched notifications.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Optional filter to count only matched notifications.", IsRequired=true, Name="Filter", ParameterType="body")]
        public string GroupBy { get; set; }
    }

    ///<summary>
    ///Gets the push notifications
    ///</summary>
    [Route("/notifications/push", "GET POST")]
    [Route("/{version}/notifications/push", "GET POST")]
    [Api(Description="Gets the push notifications")]
    [DataContract]
    public class GetNotificationsRequest
        : CodeMashListRequestBase, IReturn<GetNotificationsResponse>
    {
        ///<summary>
        ///Notifications delivered to specified user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Notifications delivered to specified user.", Name="UserId", ParameterType="body")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///Notifications delivered to specified device.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Notifications delivered to specified device.", Name="DeviceId", ParameterType="body")]
        public Guid? DeviceId { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public string DeviceKey { get; set; }
    }

    public class GetNotificationsResponse
        : ResponseBase<List<PushNotification>>
    {
        public long TotalCount { get; set; }
    }

    ///<summary>
    ///Get push notification template
    ///</summary>
    [Route("/notifications/push/templates/{id}", "GET")]
    [Route("/{version}/notifications/push/templates/{id}", "GET")]
    [Api(Description="Get push notification template")]
    [DataContract]
    public class GetNotificationTemplateRequest
        : CodeMashRequestBase, IReturn<GetNotificationTemplateResponse>
    {
        ///<summary>
        ///ID of push notification template
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="ID of push notification template", IsRequired=true, Name="Id", ParameterType="path")]
        public Guid Id { get; set; }
    }

    public class GetNotificationTemplateResponse
        : ResponseBase<PushNotificationTemplate>
    {
    }

    ///<summary>
    ///Get push notification templates
    ///</summary>
    [Route("/notifications/push/templates", "GET")]
    [Route("/{version}/notifications/push/templates", "GET")]
    [Api(Description="Get push notification templates")]
    [DataContract]
    public class GetNotificationTemplatesRequest
        : CodeMashRequestBase, IReturn<GetNotificationTemplatesResponse>
    {
    }

    public class GetNotificationTemplatesResponse
        : ResponseBase<List<PushNotificationTemplate>>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/orders/{id}", "GET")]
    [Route("/{version}/payments/orders/{id}", "GET")]
    [Api(Description="Payments")]
    [DataContract]
    public class GetOrderRequest
        : CodeMashRequestBase, IReturn<GetOrderResponse>
    {
        ///<summary>
        ///Order ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Order ID.", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///If true, includes paid transactions to response.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, includes paid transactions to response.", Name="IncludePaidTransactions", ParameterType="query")]
        public bool IncludePaidTransactions { get; set; }
    }

    public class GetOrderResponse
        : ResponseBase<Order>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/orders", "GET")]
    [Route("/{version}/payments/orders", "GET")]
    [Api(Description="Payments")]
    [DataContract]
    public class GetOrdersRequest
        : CodeMashListRequestBase, IReturn<GetOrdersResponse>
    {
        ///<summary>
        ///User ID. Requires administrator permission.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User ID. Requires administrator permission.", IsRequired=true, Name="UserId", ParameterType="query")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///If true, includes paid transactions to response.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, includes paid transactions to response.", Name="IncludePaidTransactions", ParameterType="query")]
        public bool IncludePaidTransactions { get; set; }

        ///<summary>
        ///API key of your cluster. Can be passed in a header as X-CM-Cluster.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="API key of your cluster. Can be passed in a header as X-CM-Cluster.", IsRequired=true, Name="Cluster", ParameterType="query")]
        public string Cluster { get; set; }
    }

    public class GetOrdersResponse
        : ResponseBase<List<Order>>
    {
        public long TotalCount { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/methods/setup", "GET")]
    [Route("/{version}/payments/methods/setup", "GET")]
    [Api(Description="Payments")]
    [DataContract]
    public class GetPaymentMethodSetupRequest
        : CodeMashRequestBase, IReturn<GetPaymentMethodSetupResponse>
    {
        ///<summary>
        ///Payment account ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Payment account ID.", IsRequired=true, Name="AccountId", ParameterType="query")]
        public Guid AccountId { get; set; }

        ///<summary>
        ///Can payment method be used without a user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Can payment method be used without a user.", Name="AllowOffline", ParameterType="query")]
        public bool AllowOffline { get; set; }
    }

    public class GetPaymentMethodSetupResponse
        : ResponseBase<PaymentMethodSetup>
    {
    }

    ///<summary>
    ///Gets one user
    ///</summary>
    [Route("/membership/users/profile", "GET")]
    [Route("/{version}/membership/users/profile", "GET")]
    [Api(Description="Gets one user")]
    [DataContract]
    public class GetProfileRequest
        : CodeMashRequestBase, IReturn<GetProfileResponse>
    {
        ///<summary>
        ///Set true if permissions should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if permissions should be returned", Name="IncludePermissions", ParameterType="query")]
        public bool IncludePermissions { get; set; }

        ///<summary>
        ///Set true if user devices should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user devices should be returned", Name="IncludeDevices", ParameterType="query")]
        public bool IncludeDevices { get; set; }

        ///<summary>
        ///Set true if user unread notifications count should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user unread notifications count should be returned", Name="IncludeNotificationsCount", ParameterType="query")]
        public bool IncludeUnreadNotifications { get; set; }

        ///<summary>
        ///Set true if user meta data should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user meta data should be returned", Name="IncludeMeta", ParameterType="query")]
        public bool IncludeMeta { get; set; }
    }

    public class GetProfileResponse
        : ResponseBase<User>
    {
    }

    ///<summary>
    ///Returns project details
    ///</summary>
    [Route("/projects/{projectId}", "GET")]
    [Route("/{version}/projects/{projectId}", "GET")]
    [Api(Description="Returns project details")]
    [DataContract]
    public class GetProjectRequest
        : CodeMashRequestBase, IReturn<GetProjectResponse>
    {
        ///<summary>
        ///ID of your project. Can be passed in a header as X-CM-ProjectId.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of your project. Can be passed in a header as X-CM-ProjectId.", Name="projectId", ParameterType="path")]
        public Guid ProjectId { get; set; }
    }

    public class GetProjectResponse
        : ResponseBase<Project>
    {
    }

    ///<summary>
    ///Gets one user
    ///</summary>
    [Route("/membership/users/{Id}", "GET")]
    [Route("/{version}/membership/users/{id}", "GET")]
    [Route("/{version}/membership/users/by-email", "GET")]
    [Api(Description="Gets one user")]
    [DataContract]
    public class GetUserRequest
        : CodeMashRequestBase, IReturn<GetUserResponse>
    {
        ///<summary>
        ///User identifier ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User identifier ID", IsRequired=true, Name="Id", ParameterType="path")]
        public Guid Id { get; set; }

        ///<summary>
        ///Email of user
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email of user", Name="Email", ParameterType="query")]
        public string Email { get; set; }

        ///<summary>
        ///User phone number, only viable if user registered with phone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User phone number, only viable if user registered with phone", Name="Phone", ParameterType="query")]
        public string Phone { get; set; }

        ///<summary>
        ///Provider of user
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Provider of user", Name="Provider", ParameterType="query")]
        public string Provider { get; set; }

        ///<summary>
        ///Set true if permissions should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if permissions should be returned", Name="IncludePermissions", ParameterType="query")]
        public bool IncludePermissions { get; set; }

        ///<summary>
        ///Set true if user devices should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user devices should be returned", Name="IncludeDevices", ParameterType="query")]
        public bool IncludeDevices { get; set; }

        ///<summary>
        ///Set true if user unread notifications count should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user unread notifications count should be returned", Name="IncludeNotificationsCount", ParameterType="query")]
        public bool IncludeUnreadNotifications { get; set; }

        ///<summary>
        ///Set true if user meta data should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user meta data should be returned", Name="IncludeMeta", ParameterType="query")]
        public bool IncludeMeta { get; set; }
    }

    public class GetUserResponse
        : ResponseBase<User>
    {
    }

    ///<summary>
    ///Gets many users
    ///</summary>
    [Route("/membership/users", "GET POST")]
    [Route("/{version}/membership/users", "GET POST")]
    [Api(Description="Gets many users")]
    [DataContract]
    public class GetUsersRequest
        : CodeMashListRequestBase, IReturn<GetUsersResponse>
    {
        ///<summary>
        ///Set true if permissions should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if permissions should be returned", Name="IncludePermissions", ParameterType="path")]
        public bool IncludePermissions { get; set; }

        ///<summary>
        ///Set true if user devices should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user devices should be returned", Name="IncludeDevices", ParameterType="path")]
        public bool IncludeDevices { get; set; }

        ///<summary>
        ///Set true if user meta data should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user meta data should be returned", Name="IncludeMeta", ParameterType="path")]
        public bool IncludeMeta { get; set; }
    }

    public class GetUsersResponse
        : ResponseBase<List<User>>
    {
        public long TotalCount { get; set; }
    }

    ///<summary>
    ///Authentication
    ///</summary>
    [Route("/auth/google", "GET POST")]
    [Route("/{version}/auth/google", "GET POST")]
    [Api(Description="Authentication")]
    [DataContract]
    public class GoogleAuthenticationRequest
        : CodeMashRequestBase, IReturn<GoogleAuthenticationResponse>, IOAuthRequest
    {
        ///<summary>
        ///Mode to use for authentication
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Mode to use for authentication", Name="Mode", ParameterType="query")]
        public string Mode { get; set; }

        ///<summary>
        ///Code received from Google services
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Code received from Google services", Name="Code", ParameterType="query")]
        public string Code { get; set; }

        ///<summary>
        ///State received with a code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="State received with a code", Name="State", ParameterType="query")]
        public string State { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessToken", ParameterType="query")]
        public string AccessToken { get; set; }
    }

    public class GoogleAuthenticationResponse
        : ResponseBase<AuthResponse>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{collectionName}/bulk", "POST")]
    [Route("/{version}/db/{CollectionName}/bulk", "POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class InsertManyRequest
        : CodeMashDbRequestBase, IReturn<InsertManyResponse>
    {
        public InsertManyRequest()
        {
            Documents = new List<string>{};
        }

        ///<summary>
        ///Array of json records to insert
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Array of json records to insert", IsRequired=true, Name="Document", ParameterType="body")]
        public List<string> Documents { get; set; }

        ///<summary>
        ///Records are validated against CodeMash JSON Schema before each insert. That can slow down API performance. If you have already validated your data with JSON Schema and you can assure of data integrity, you can bypass document validation by setting this property to true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Records are validated against CodeMash JSON Schema before each insert. That can slow down API performance. If you have already validated your data with JSON Schema and you can assure of data integrity, you can bypass document validation by setting this property to true.", Name="BypassDocumentValidation", ParameterType="form")]
        public bool BypassDocumentValidation { get; set; }

        ///<summary>
        ///Triggers are called each time you insert the document. You can disable triggers if it's not needed. This is really important when you insert in database from trigger itself, so then you can avoid infinite loop.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Triggers are called each time you insert the document. You can disable triggers if it's not needed. This is really important when you insert in database from trigger itself, so then you can avoid infinite loop.", Name="IgnoreTriggers", ParameterType="form")]
        public bool IgnoreTriggers { get; set; }

        ///<summary>
        ///Set responsible user for document, but be sure the caller of API has right set of permissions.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Set responsible user for document, but be sure the caller of API has right set of permissions.", Name="ResponsibleUserId", ParameterType="form")]
        public Guid? ResponsibleUserId { get; set; }

        ///<summary>
        ///If your document has file field(s), then you set field value with Id that comes from your file storage provider. E.g.: When your file provider is AWS S3, so then you can set field value to S3 file key, and this will be automatically combined between your file service provider and CodeMash.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your document has file field(s), then you set field value with Id that comes from your file storage provider. E.g.: When your file provider is AWS S3, so then you can set field value to S3 file key, and this will be automatically combined between your file service provider and CodeMash.", Name="ResolveProviderFiles", ParameterType="form")]
        public bool ResolveProviderFiles { get; set; }
    }

    public class InsertManyResponse
        : ResponseBase<List<String>>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}", "POST")]
    [Route("/{version}/db/{CollectionName}", "POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class InsertOneRequest
        : CodeMashDbRequestBase, IReturn<InsertOneResponse>
    {
        ///<summary>
        ///Entity represented as json to insert
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Entity represented as json to insert", IsRequired=true, Name="Document", ParameterType="body")]
        public string Document { get; set; }

        ///<summary>
        ///Records are validated against CodeMash JSON Schema before each insert. That can slow down API performance. If you have already validated your data with JSON Schema and you can assure of data integrity, you can bypass document validation by setting this property to true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Records are validated against CodeMash JSON Schema before each insert. That can slow down API performance. If you have already validated your data with JSON Schema and you can assure of data integrity, you can bypass document validation by setting this property to true.", Name="BypassDocumentValidation", ParameterType="form")]
        public bool BypassDocumentValidation { get; set; }

        ///<summary>
        ///By default file uploads are done after the record is inserted, set to true in case you need to wait for files to be uploaded
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="By default file uploads are done after the record is inserted, set to true in case you need to wait for files to be uploaded", Name="WaitForFileUpload", ParameterType="form")]
        public bool WaitForFileUpload { get; set; }

        ///<summary>
        ///Triggers are called each time you insert the document. You can disable triggers if it's not needed. This is really important when you insert in database from trigger itself, so then you can avoid infinite loop.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Triggers are called each time you insert the document. You can disable triggers if it's not needed. This is really important when you insert in database from trigger itself, so then you can avoid infinite loop.", Name="IgnoreTriggers", ParameterType="form")]
        public bool IgnoreTriggers { get; set; }

        ///<summary>
        ///Set responsible user for document, but be sure the caller of API has right set of permissions.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Set responsible user for document, but be sure the caller of API has right set of permissions.", Name="ResponsibleUserId", ParameterType="form")]
        public Guid? ResponsibleUserId { get; set; }

        ///<summary>
        ///If your document has file field(s), then you set field value with Id that comes from your file storage provider. E.g.: When your file provider is AWS S3, so then you can set field value to S3 file key, and this will be automatically combined between your file service provider and CodeMash.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your document has file field(s), then you set field value with Id that comes from your file storage provider. E.g.: When your file provider is AWS S3, so then you can set field value to S3 file key, and this will be automatically combined between your file service provider and CodeMash.", Name="ResolveProviderFiles", ParameterType="form")]
        public bool ResolveProviderFiles { get; set; }
    }

    [DataContract]
    public class InsertOneResponse
        : ResponseBase<string>
    {
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/invite", "POST")]
    [Route("/{version}/membership/users/invite", "POST")]
    [Api(Description="Membership")]
    [DataContract]
    public class InviteUserRequest
        : CodeMashRequestBase, IReturn<InviteUserV2Response>
    {
        public InviteUserRequest()
        {
            Roles = new List<string>{};
            RolesTree = new List<UserRoleUpdateInput>{};
        }

        ///<summary>
        ///Member display name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member display name", Name="DisplayName", ParameterType="form")]
        public string DisplayName { get; set; }

        ///<summary>
        ///Email address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email address", IsRequired=true, Name="Email", ParameterType="form")]
        public string Email { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="form")]
        public string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="form")]
        public string LastName { get; set; }

        ///<summary>
        ///Role names to be applied
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Role names to be applied", Name="Roles", ParameterType="form")]
        public List<string> Roles { get; set; }

        ///<summary>
        ///Full permission tree
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Full permission tree", Name="RolesTree", ParameterType="body")]
        public List<UserRoleUpdateInput> RolesTree { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="form")]
        public string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="form")]
        public string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="form")]
        public string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="form")]
        public bool? SubscribeToNews { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public string Address2 { get; set; }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", Name="Phone", ParameterType="body")]
        public string Phone { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public string BirthDate { get; set; }

        ///<summary>
        ///User's zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's zone", Name="Zone", ParameterType="body")]
        public string Zone { get; set; }
    }

    public class InviteUserV2Response
        : ResponseBase<User>
    {
    }

    public class KevinAuthorizationLink
    {
        public string AuthorizationLink { get; set; }
        public string State { get; set; }
        public string RequestId { get; set; }
    }

    public class KevinPaymentStatus
    {
        public string GroupStatus { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
    }

    ///<summary>
    ///Marks notification as read
    ///</summary>
    [Route("/notifications/push/read", "PATCH")]
    [Route("/{version}/notifications/push/read", "PATCH")]
    [Api(Description="Marks notification as read")]
    [DataContract]
    public class MarkAllNotificationsAsReadRequest
        : CodeMashRequestBase, IReturn<MarkNotificationAsReadResponse>
    {
        ///<summary>
        ///UserId - (Either userId or deviceId is required). The same massage can be spread across many users, so we need to identify which user read the message
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="UserId - (Either userId or deviceId is required). The same massage can be spread across many users, so we need to identify which user read the message", Name="UserId", ParameterType="body")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///DeviceId - (Either userId or deviceId is required). The same massage can be spread across many devices, so we need to identify in which device the message has been read
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="DeviceId - (Either userId or deviceId is required). The same massage can be spread across many devices, so we need to identify in which device the message has been read", IsRequired=true, Name="DeviceId", ParameterType="body")]
        public Guid? DeviceId { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public string DeviceKey { get; set; }

        ///<summary>
        ///Optional filter to read only matched notifications.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Optional filter to read only matched notifications.", IsRequired=true, Name="Filter", ParameterType="body")]
        public string Filter { get; set; }
    }

    ///<summary>
    ///Marks notification as read
    ///</summary>
    [Route("/notifications/push/{notificationId}/read", "PATCH")]
    [Route("/{version}/notifications/push/{notificationId}/read", "PATCH")]
    [Api(Description="Marks notification as read")]
    [DataContract]
    public class MarkNotificationAsReadRequest
        : CodeMashRequestBase, IReturn<MarkNotificationAsReadResponse>
    {
        ///<summary>
        ///NotificationId - notification to be marked as read
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="NotificationId - notification to be marked as read", IsRequired=true, Name="NotificationId", ParameterType="form")]
        public string NotificationId { get; set; }

        ///<summary>
        ///UserId - (Either userId or deviceId is required). The same massage can be spread across many users, so we need to identify which user read the message
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="UserId - (Either userId or deviceId is required). The same massage can be spread across many users, so we need to identify which user read the message", Name="UserId", ParameterType="form")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///DeviceId - (Either userId or deviceId is required). The same massage can be spread across many devices, so we need to identify in which device the message has been read
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="DeviceId - (Either userId or deviceId is required). The same massage can be spread across many devices, so we need to identify in which device the message has been read", IsRequired=true, Name="DeviceId", ParameterType="form")]
        public Guid? DeviceId { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public string DeviceKey { get; set; }
    }

    public class MarkNotificationAsReadResponse
        : ResponseBase<bool>
    {
    }

    public class Order
    {
        public Order()
        {
            Lines = new List<OrderLine>{};
            Files = new List<OrderFile>{};
            Transactions = new List<OrderTransaction>{};
            Discounts = new List<OrderDiscount>{};
            Meta = new Dictionary<string, string>{};
        }

        public string Id { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
        public string PaidOn { get; set; }
        public long Number { get; set; }
        public string NumberPrefix { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentProvider { get; set; }
        public string Currency { get; set; }
        public bool AsGuest { get; set; }
        public bool IsTest { get; set; }
        public OrderCustomer Customer { get; set; }
        public string Cluster { get; set; }
        public List<OrderLine> Lines { get; set; }
        public List<OrderFile> Files { get; set; }
        public List<OrderTransaction> Transactions { get; set; }
        public List<OrderDiscount> Discounts { get; set; }
        public string UserId { get; set; }
        public string PaymentAccountId { get; set; }
        public decimal Total { get; set; }
        public Dictionary<string, string> Meta { get; set; }
    }

    public class OrderCustomer
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class OrderCustomerInput
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Language { get; set; }
    }

    public class OrderDiscount
    {
        public OrderDiscount()
        {
            IndividualDiscounts = new List<DiscountIndividualLine>{};
            CategoryDiscounts = new List<DiscountCategory>{};
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string TargetType { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<DiscountIndividualLine> IndividualDiscounts { get; set; }
        public List<DiscountCategory> CategoryDiscounts { get; set; }
        public DiscountAll AllDiscount { get; set; }
    }

    public class OrderFile
    {
        public string Category { get; set; }
        public string Id { get; set; }
        public string Directory { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
    }

    public class OrderLine
    {
        public OrderLine()
        {
            PriceFields = new List<string>{};
        }

        public string SchemaId { get; set; }
        public string CollectionName { get; set; }
        public string RecordId { get; set; }
        public List<string> PriceFields { get; set; }
        public string Variation { get; set; }
        public string RecordData { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderLineInput
    {
        public string CollectionName { get; set; }
        public string RecordId { get; set; }
        public int Quantity { get; set; }
        public string Variation { get; set; }
    }

    public class OrderTransaction
    {
        public string Id { get; set; }
        public string CreatedOn { get; set; }
        public string PayUntil { get; set; }
        public string PaidOn { get; set; }
        public string CallbackOn { get; set; }
        public string Provider { get; set; }
        public string EventStatus { get; set; }
        public string EventUniqueId { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public string PayerIpCountry { get; set; }
        public string PayerCountry { get; set; }
        public string PayerEmail { get; set; }
        public string PaymentType { get; set; }
        public string EventAccount { get; set; }
        public string PayText { get; set; }
        public string EventCurrency { get; set; }
        public decimal EventAmount { get; set; }
    }

    public class PaymentDiscountBoundary
    {
        public double Boundary { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
    }

    public class PaymentMethod
    {
        public PaymentMethod()
        {
            Meta = new Dictionary<string, string>{};
        }

        public string Id { get; set; }
        public string CreatedOn { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string Last4 { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public Dictionary<string, string> Meta { get; set; }
    }

    public class PaymentMethodSetup
    {
        public string SetupId { get; set; }
        public string SetupClientSecret { get; set; }
        public string Status { get; set; }
    }

    public class Policy
    {
        public Policy()
        {
            Permissions = new List<string>{};
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Disabled { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class Project
    {
        public Project()
        {
            Tokens = new List<Token>{};
            Languages = new List<string>{};
        }

        public Guid Id { get; set; }
        public List<Token> Tokens { get; set; }
        public List<string> Languages { get; set; }
        public string DefaultLanguage { get; set; }
        public string DefaultTimeZone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SlugifiedName { get; set; }
        public string LogoUrl { get; set; }
    }

    public class PushNotification
    {
        public PushNotification()
        {
            Meta = new Dictionary<string, string>{};
        }

        public string Id { get; set; }
        public string ReceivedOn { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Data { get; set; }
        public string Subtitle { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public bool IsRead { get; set; }
        public string UserId { get; set; }
        public string DeviceId { get; set; }
        public string SenderId { get; set; }
    }

    public class PushNotificationTemplate
    {
        public PushNotificationTemplate()
        {
            Meta = new Dictionary<string, string>{};
            Buttons = new List<PushNotificationTemplateButtons>{};
        }

        public string Id { get; set; }
        public string TemplateName { get; set; }
        public string AccountName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Code { get; set; }
        public string Priority { get; set; }
        public string Data { get; set; }
        public int? Ttl { get; set; }
        public string Url { get; set; }
        public string CollapseId { get; set; }
        public FileDetails Image { get; set; }
        public string AccountId { get; set; }
        public Guid? FileAccountId { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public List<PushNotificationTemplateButtons> Buttons { get; set; }
        public string Subtitle { get; set; }
        public int? IosBadge { get; set; }
        public string IosCategory { get; set; }
        public bool IosContentAvailable { get; set; }
        public string IosSound { get; set; }
        public string IosAppBundleId { get; set; }
        public string IosGroupId { get; set; }
        public string IosPushType { get; set; }
        public string IosLaunchImage { get; set; }
        public string IosAnalyticsLabel { get; set; }
        public string AndroidGroup { get; set; }
        public string AndroidGroupMessage { get; set; }
        public string RestrictedPackageName { get; set; }
        public string AndroidChannelId { get; set; }
        public string AndroidSound { get; set; }
        public string AndroidVisibility { get; set; }
        public bool AndroidDefaultVibration { get; set; }
        public string AndroidVibrateTimings { get; set; }
        public bool AndroidDefaultLight { get; set; }
        public string AndroidAccentColor { get; set; }
        public string AndroidLedColor { get; set; }
        public string AndroidLightOnDuration { get; set; }
        public string AndroidLightOffDuration { get; set; }
        public bool AndroidSticky { get; set; }
        public string AndroidSmallIcon { get; set; }
        public string AndroidLargeIcon { get; set; }
        public AndroidBackgroundLayout AndroidBackground { get; set; }
        public string AndroidAnalyticsLabel { get; set; }
    }

    public class PushNotificationTemplateButtons
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
    }

    public class PushNotificationToken
    {
        public string Provider { get; set; }
        public string Token { get; set; }
        public string AccountId { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/auth/kevin/token", "POST")]
    [Route("/{version}/payments/auth/kevin/token", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class ReceiveKevinTokenRequest
        : CodeMashRequestBase, IReturn<StartKevinAuthResponse>
    {
        ///<summary>
        ///Authentication request ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Authentication request ID.", IsRequired=true, Name="AuthorizationCode", ParameterType="body")]
        public string RequestId { get; set; }

        ///<summary>
        ///Authorization code received from authenticating.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Authorization code received from authenticating.", IsRequired=true, Name="AuthorizationCode", ParameterType="body")]
        public string AuthorizationCode { get; set; }
    }

    public class ReferencingField
    {
        public string Name { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Sort { get; set; }
        public string Projection { get; set; }
    }

    ///<summary>
    ///Registers expo push notification token
    ///</summary>
    [Route("/notifications/push/token/expo", "POST")]
    [Route("/{version}/notifications/push/token/expo", "POST")]
    [Api(Description="Registers expo push notification token")]
    [DataContract]
    public class RegisterDeviceExpoTokenRequest
        : CodeMashRequestBase, IReturn<RegisterDeviceExpoTokenResponse>
    {
        public RegisterDeviceExpoTokenRequest()
        {
            Meta = new Dictionary<string, string>{};
        }

        ///<summary>
        ///User Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User Id", Name="UserId", ParameterType="form")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///Device Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device Id", Name="DeviceId", ParameterType="form")]
        public Guid? DeviceId { get; set; }

        ///<summary>
        ///Token
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Token", IsRequired=true, Name="Token", ParameterType="form")]
        public string Token { get; set; }

        ///<summary>
        ///TimeZone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="TimeZone", Name="TimeZone", ParameterType="form")]
        public string TimeZone { get; set; }

        ///<summary>
        ///Meta information that comes from device.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Meta information that comes from device.", Name="Meta", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }
    }

    public class RegisterDeviceExpoTokenResponse
        : ResponseBase<string>
    {
    }

    ///<summary>
    ///Registers One Signal push notification token
    ///</summary>
    [Route("/notifications/push/devices/token", "POST")]
    [Route("/{version}/notifications/push/devices/token", "POST")]
    [Api(Description="Registers One Signal push notification token")]
    [DataContract]
    public class RegisterDeviceTokenRequest
        : CodeMashRequestBase, IReturn<RegisterDeviceTokenResponse>
    {
        public RegisterDeviceTokenRequest()
        {
            Meta = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Notification provider. Can be "Expo", "OneSignal", "Firebase"
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Notification provider. Can be \"Expo\", \"OneSignal\", \"Firebase\"", Name="Provider", ParameterType="body")]
        public string Provider { get; set; }

        ///<summary>
        ///Push account ID. If you have more than 1 account for provider pass this instead.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Push account ID. If you have more than 1 account for provider pass this instead.", Name="AccountId", ParameterType="body")]
        public Guid? AccountId { get; set; }

        ///<summary>
        ///Identifier for device depending on provider (device ID, player ID)
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Identifier for device depending on provider (device ID, player ID)", IsRequired=true, Name="Token", ParameterType="body")]
        public string Token { get; set; }

        ///<summary>
        ///User ID to attach this token to.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User ID to attach this token to.", Name="UserId", ParameterType="body")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///Device ID to attach this token to. New device will be created if this is null.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device ID to attach this token to. New device will be created if this is null.", Name="DeviceId", ParameterType="body")]
        public Guid? DeviceId { get; set; }

        ///<summary>
        ///Device operating system
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device operating system", Name="OperatingSystem", ParameterType="body")]
        public string OperatingSystem { get; set; }

        ///<summary>
        ///Device brand
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device brand", Name="Brand", ParameterType="body")]
        public string Brand { get; set; }

        ///<summary>
        ///Device name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device name", Name="DeviceName", ParameterType="body")]
        public string DeviceName { get; set; }

        ///<summary>
        ///Device timezone, expects to get a TZ database name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device timezone, expects to get a TZ database name", Name="TimeZone", ParameterType="body")]
        public string TimeZone { get; set; }

        ///<summary>
        ///Device language, expects to get a 2 letter identifier
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device language, expects to get a 2 letter identifier", Name="Language", ParameterType="body")]
        public string Language { get; set; }

        ///<summary>
        ///Device locale
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device locale", Name="Locale", ParameterType="body")]
        public string Locale { get; set; }

        ///<summary>
        ///Other device information
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Other device information", Name="Meta", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }
    }

    public class RegisterDeviceTokenResponse
        : ResponseBase<Device>
    {
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/register/guest", "POST")]
    [Route("/{version}/membership/users/register/guest", "POST")]
    [Api(Description="Membership")]
    [DataContract]
    public class RegisterGuestUserRequest
        : CodeMashRequestBase, IReturn<RegisterGuestUserResponse>
    {
        public RegisterGuestUserRequest()
        {
            RolesTree = new List<UserRoleUpdateInput>{};
        }

        ///<summary>
        ///Member display name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member display name", Name="DisplayName", ParameterType="body")]
        public string DisplayName { get; set; }

        ///<summary>
        ///Email address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email address", IsRequired=true, Name="Email", ParameterType="body")]
        public string Email { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="body")]
        public string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="body")]
        public string LastName { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="body")]
        public string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="body")]
        public string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="body")]
        public string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="body")]
        public bool? SubscribeToNews { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public string Address2 { get; set; }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", Name="Phone", ParameterType="body")]
        public string Phone { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public string BirthDate { get; set; }

        ///<summary>
        ///Full permission tree
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Full permission tree", Name="RolesTree", ParameterType="body")]
        public List<UserRoleUpdateInput> RolesTree { get; set; }

        ///<summary>
        ///User's zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's zone", Name="Zone", ParameterType="body")]
        public string Zone { get; set; }
    }

    public class RegisterGuestUserResponse
        : ResponseBase<User>
    {
        public string BearerToken { get; set; }
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/register/phone", "POST")]
    [Route("/{version}/membership/users/register/phone", "POST")]
    [Api(Description="Membership")]
    [DataContract]
    public class RegisterPhoneUserRequest
        : CodeMashRequestBase, IReturn<RegisterPhoneUserResponse>
    {
        public RegisterPhoneUserRequest()
        {
            RolesTree = new List<UserRoleUpdateInput>{};
        }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", IsRequired=true, Name="Phone", ParameterType="body")]
        public string Phone { get; set; }

        ///<summary>
        ///Member display name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member display name", Name="DisplayName", ParameterType="body")]
        public string DisplayName { get; set; }

        ///<summary>
        ///Email address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email address", Name="Email", ParameterType="body")]
        public string Email { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="body")]
        public string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="body")]
        public string LastName { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="body")]
        public string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="body")]
        public string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="body")]
        public string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="body")]
        public bool? SubscribeToNews { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public string Address2 { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public string BirthDate { get; set; }

        ///<summary>
        ///Full permission tree
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Full permission tree", Name="RolesTree", ParameterType="body")]
        public List<UserRoleUpdateInput> RolesTree { get; set; }

        ///<summary>
        ///User's zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's zone", Name="Zone", ParameterType="body")]
        public string Zone { get; set; }
    }

    public class RegisterPhoneUserResponse
        : ResponseBase<User>
    {
        public string BearerToken { get; set; }
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/register", "POST")]
    [Route("/{version}/membership/users/register", "POST")]
    [Api(Description="Membership")]
    [DataContract]
    public class RegisterUserRequest
        : CodeMashRequestBase, IReturn<RegisterUserV2Response>
    {
        public RegisterUserRequest()
        {
            Roles = new List<string>{};
            RolesTree = new List<UserRoleUpdateInput>{};
        }

        ///<summary>
        ///Member display name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member display name", Name="DisplayName", ParameterType="body")]
        public string DisplayName { get; set; }

        ///<summary>
        ///Email address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email address", Name="Email", ParameterType="body")]
        public string Email { get; set; }

        ///<summary>
        ///Username
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Username", Name="UserName", ParameterType="body")]
        public string UserName { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="body")]
        public string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="body")]
        public string LastName { get; set; }

        ///<summary>
        ///Password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Password", IsRequired=true, Name="Password", ParameterType="body")]
        public string Password { get; set; }

        ///<summary>
        ///Role names to be applied
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Role names to be applied", Name="Roles", ParameterType="body")]
        public List<string> Roles { get; set; }

        ///<summary>
        ///Full permission tree
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Full permission tree", Name="RolesTree", ParameterType="body")]
        public List<UserRoleUpdateInput> RolesTree { get; set; }

        ///<summary>
        ///Login immediately ?
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Login immediately ?", Name="AutoLogin", ParameterType="body")]
        public bool AutoLogin { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="body")]
        public string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="body")]
        public string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="body")]
        public string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="body")]
        public bool? SubscribeToNews { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public string Address2 { get; set; }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", Name="Phone", ParameterType="body")]
        public string Phone { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public string BirthDate { get; set; }

        ///<summary>
        ///User's zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's zone", Name="Zone", ParameterType="body")]
        public string Zone { get; set; }
    }

    public class RegisterUserV2Response
        : ResponseBase<User>
    {
        public string BearerToken { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/replaceOne", "PUT")]
    [Route("/{version}/db/{CollectionName}/replaceOne", "PUT")]
    [Route("/{version}/db/{CollectionName}/replaceOne/{id}", "PUT")]
    [Api(Description="Database services")]
    [DataContract]
    public class ReplaceOneRequest
        : CodeMashDbRequestBase, IReturn<ReplaceOneResponse>
    {
        ///<summary>
        ///Document to replace with.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Document to replace with.", IsRequired=true, Name="Document", ParameterType="body")]
        public string Document { get; set; }

        ///<summary>
        ///Find document by Id and replace it.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Find document by Id and replace it.", Name="Id", ParameterType="path", Route="/{version}/db/{CollectionName}/replaceOne/{id}")]
        public string Id { get; set; }

        ///<summary>
        ///Set the filter (in JSON format) to replace the first document found in the collection. Required when Id is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Set the filter (in JSON format) to replace the first document found in the collection. Required when Id is not set.", Name="Filter", ParameterType="body")]
        public string Filter { get; set; }

        ///<summary>
        ///By default file uploads are done after the record is updated, set to true in case you need to wait for files to be uploaded.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="By default file uploads are done after the record is updated, set to true in case you need to wait for files to be uploaded.", Name="WaitForFileUpload", ParameterType="form")]
        public bool WaitForFileUpload { get; set; }

        ///<summary>
        ///If true, inserts a new record if current record not found
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If true, inserts a new record if current record not found", Name="IsUpsert", ParameterType="form")]
        public bool IsUpsert { get; set; }

        ///<summary>
        ///Records are validated against CodeMash JSON Schema before each update. That can slow down API performance. If you have already validated your data with JSON Schema and you can assure of data integrity, you can bypass document validation by setting this property to true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Records are validated against CodeMash JSON Schema before each update. That can slow down API performance. If you have already validated your data with JSON Schema and you can assure of data integrity, you can bypass document validation by setting this property to true.", IsRequired=true, Name="BypassDocumentValidation", ParameterType="form")]
        public bool BypassDocumentValidation { get; set; }

        ///<summary>
        ///Triggers are called each time you update the document. You can disable triggers if it's not needed. This is really important when you update the database from trigger itself, so then you can avoid infinite loop.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Triggers are called each time you update the document. You can disable triggers if it's not needed. This is really important when you update the database from trigger itself, so then you can avoid infinite loop.", Name="IgnoreTriggers", ParameterType="form")]
        public bool IgnoreTriggers { get; set; }

        ///<summary>
        ///If your document has file field(s), then you set field value with Id that comes from your file storage provider. E.g.: When your file provider is AWS S3, so then you can set field value to S3 file key, and this will be automatically combined between your file service provider and CodeMash.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your document has file field(s), then you set field value with Id that comes from your file storage provider. E.g.: When your file provider is AWS S3, so then you can set field value to S3 file key, and this will be automatically combined between your file service provider and CodeMash.", Name="ResolveProviderFiles", ParameterType="form")]
        public bool ResolveProviderFiles { get; set; }
    }

    public class ReplaceOneResponse
        : ResponseBase<ReplaceOneResult>
    {
    }

    [DataContract]
    public class ReplaceOneResult
    {
        ///<summary>
        ///A boolean acknowledged as true if the operation ran with write concern or false if write concern was disabled
        ///</summary>
        [DataMember(Name="isAcknowledged")]
        [ApiMember(DataType="bool", Description="A boolean acknowledged as true if the operation ran with write concern or false if write concern was disabled", Name="IsAcknowledged")]
        public bool IsAcknowledged { get; set; }

        ///<summary>
        ///Checks if modifiedCount is available
        ///</summary>
        [DataMember(Name="isModifiedCountAvailable")]
        [ApiMember(DataType="bool", Description="Checks if modifiedCount is available", Name="IsModifiedCountAvailable")]
        public bool IsModifiedCountAvailable { get; set; }

        ///<summary>
        ///matchedCount containing the number of matched documents
        ///</summary>
        [DataMember(Name="matchedCount")]
        [ApiMember(DataType="bool", Description="matchedCount containing the number of matched documents", Name="MatchedCount")]
        public long MatchedCount { get; set; }

        ///<summary>
        ///modifiedCount containing the number of modified documents
        ///</summary>
        [DataMember(Name="modifiedCount")]
        [ApiMember(DataType="long", Description="modifiedCount containing the number of modified documents", Name="ModifiedCount")]
        public long ModifiedCount { get; set; }

        ///<summary>
        ///upsertedId containing the _id for the upserted document
        ///</summary>
        [DataMember(Name="upsertedId")]
        [ApiMember(DataType="string", Description="upsertedId containing the _id for the upserted document", Name="UpsertedId")]
        public string UpsertedId { get; set; }
    }

    ///<summary>
    ///Gets one user
    ///</summary>
    [Route("/membership/users/verify/resend", "POST")]
    [Route("/{version}/membership/users/verify/resend", "POST")]
    [Api(Description="Gets one user")]
    [DataContract]
    public class ResendUserVerificationRequest
        : CodeMashRequestBase, IReturn<GetProfileResponse>
    {
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/password/reset", "POST")]
    [Route("/{version}/membership/users/password/reset", "POST")]
    [Api(Description="Membership")]
    [DataContract]
    public class ResetPasswordRequest
        : RequestBase, IReturnVoid
    {
        ///<summary>
        ///Secret token received by email for password reset
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Secret token received by email for password reset", IsRequired=true, Name="Token", ParameterType="body")]
        public string Token { get; set; }

        ///<summary>
        ///New user password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New user password", IsRequired=true, Name="Password", ParameterType="body")]
        public string Password { get; set; }

        ///<summary>
        ///New repeated user password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New repeated user password", IsRequired=true, Name="RepeatedPassword", ParameterType="body")]
        public string RepeatedPassword { get; set; }
    }

    public class Role
    {
        public Role()
        {
            Policies = new List<Policy>{};
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<Policy> Policies { get; set; }
    }

    public class Schema
    {
        public Schema()
        {
            TranslatableFields = new List<string>{};
        }

        public string CollectionNameAsTitle { get; set; }
        public string CollectionName { get; set; }
        public string UiSchema { get; set; }
        public string JsonSchema { get; set; }
        public List<string> TranslatableFields { get; set; }
        public Guid DatabaseId { get; set; }
        public Guid SchemaId { get; set; }
    }

    public class SendEmailNotificationV2Response
        : ResponseBase<string>
    {
    }

    ///<summary>
    ///Sends an email message
    ///</summary>
    [Route("/notifications/email", "POST")]
    [Route("/{version}/notifications/email", "POST")]
    [Api(Description="Sends an email message")]
    [DataContract]
    public class SendEmailRequest
        : CodeMashRequestBase, IReturn<SendEmailNotificationV2Response>
    {
        public SendEmailRequest()
        {
            Emails = new List<string>{};
            Users = new List<Guid>{};
            Roles = new List<string>{};
            CcEmails = new List<string>{};
            CcUsers = new List<Guid>{};
            BccEmails = new List<string>{};
            BccUsers = new List<Guid>{};
            Tokens = new Dictionary<string, string>{};
            Attachments = new List<string>{};
        }

        ///<summary>
        ///ID of a template to use
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="ID of a template to use", IsRequired=true, Name="TemplateId", ParameterType="body")]
        public Guid TemplateId { get; set; }

        ///<summary>
        ///Recipients' email addresses. Emails, Users or Roles are required.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Recipients' email addresses. Emails, Users or Roles are required.", Name="Emails", ParameterType="body")]
        public List<string> Emails { get; set; }

        ///<summary>
        ///Recipients' user IDs. Emails, Users or Roles are required.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Recipients' user IDs. Emails, Users or Roles are required.", Name="Users", ParameterType="body")]
        public List<Guid> Users { get; set; }

        ///<summary>
        ///Recipients' roles. Emails, Users or Roles are required.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Recipients' roles. Emails, Users or Roles are required.", Name="Roles", ParameterType="body")]
        public List<string> Roles { get; set; }

        ///<summary>
        ///CC recipients' email addresses
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="CC recipients' email addresses", Name="CcEmails", ParameterType="body")]
        public List<string> CcEmails { get; set; }

        ///<summary>
        ///CC recipients' user IDs
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="CC recipients' user IDs", Name="CcUsers", ParameterType="body")]
        public List<Guid> CcUsers { get; set; }

        ///<summary>
        ///BCC recipients' email addresses
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="BCC recipients' email addresses", Name="BccEmails", ParameterType="body")]
        public List<string> BccEmails { get; set; }

        ///<summary>
        ///BCC recipients' user IDs
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="BCC recipients' user IDs", Name="BccUsers", ParameterType="body")]
        public List<Guid> BccUsers { get; set; }

        ///<summary>
        ///Custom tokens to inject into template
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Object", Description="Custom tokens to inject into template", Name="Tokens", ParameterType="body")]
        public Dictionary<string, string> Tokens { get; set; }

        ///<summary>
        ///Amount of milliseconds to postpone sending the email
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="Amount of milliseconds to postpone sending the email", Name="Postpone", ParameterType="body")]
        public long? Postpone { get; set; }

        ///<summary>
        ///If true, sends an email by recipient's time zone. Postpone needs to be set for this to have an effect. Requires Users or Roles recipients. Default - true
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, sends an email by recipient's time zone. Postpone needs to be set for this to have an effect. Requires Users or Roles recipients. Default - true", Name="RespectTimeZone", ParameterType="body")]
        public bool RespectTimeZone { get; set; }

        ///<summary>
        ///If true, will try to send an email using a language from CultureCode instead of user's language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, will try to send an email using a language from CultureCode instead of user's language", Name="ForceRequestLanguage", ParameterType="body")]
        public bool ForceRequestLanguage { get; set; }

        ///<summary>
        ///File IDs to attach to email message
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="File IDs to attach to email message", Name="Attachments", ParameterType="body")]
        public List<string> Attachments { get; set; }
    }

    ///<summary>
    ///Pushes a mobile notification
    ///</summary>
    [Route("/notifications/push", "POST")]
    [Route("/{version}/notifications/push", "POST")]
    [Api(Description="Pushes a mobile notification")]
    [DataContract]
    public class SendPushNotificationRequest
        : CodeMashRequestBase, IReturn<SendPushNotificationResponse>
    {
        public SendPushNotificationRequest()
        {
            Users = new List<Guid>{};
            Devices = new List<Guid>{};
            Roles = new List<string>{};
            Tokens = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Template Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Template Id", IsRequired=true, Name="TemplateId", ParameterType="form")]
        public Guid TemplateId { get; set; }

        ///<summary>
        ///Recipients list. You can send messages by specifying CodeMash membership users which are combined with devices.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="List", Description="Recipients list. You can send messages by specifying CodeMash membership users which are combined with devices.", Name="Users", ParameterType="body")]
        public List<Guid> Users { get; set; }

        ///<summary>
        ///Messages to be delivered into specified devices.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="List", Description="Messages to be delivered into specified devices.", Name="Devices", ParameterType="body")]
        public List<Guid> Devices { get; set; }

        ///<summary>
        ///Messages to be delivered to specified roles.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="List", Description="Messages to be delivered to specified roles.", Name="Roles", ParameterType="body")]
        public List<string> Roles { get; set; }

        ///<summary>
        ///Custom tokens which are not provided in project
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Custom tokens which are not provided in project", Name="Tokens", ParameterType="body")]
        public Dictionary<string, string> Tokens { get; set; }

        ///<summary>
        ///Amount of milliseconds to postpone pushing the notification.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="Amount of milliseconds to postpone pushing the notification.", Name="Postpone", ParameterType="body")]
        public long? Postpone { get; set; }

        ///<summary>
        ///Respects user's time zone. If false, send by project time zone. Default - true. Postpone needs to be set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Respects user's time zone. If false, send by project time zone. Default - true. Postpone needs to be set.", Name="RespectTimeZone", ParameterType="body")]
        public bool RespectTimeZone { get; set; }

        ///<summary>
        ///If set to true, notification will not be pushed to user's device. Cannot be true if postpone is set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If set to true, notification will not be pushed to user's device. Cannot be true if postpone is set.", Name="IsNonPushable", ParameterType="body")]
        public bool IsNonPushable { get; set; }

        ///<summary>
        ///Will try to send a template for language from CultureCode over the user's language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Will try to send a template for language from CultureCode over the user's language", Name="ForceRequestLanguage", ParameterType="body")]
        public bool ForceRequestLanguage { get; set; }
    }

    public class SendPushNotificationResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/auth/kevin", "GET POST")]
    [Route("/{version}/payments/auth/kevin", "GET POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class StartKevinAuthRequest
        : CodeMashRequestBase, IReturn<StartKevinAuthResponse>
    {
        ///<summary>
        ///Payment account ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Payment account ID.", IsRequired=true, Name="AccountId", ParameterType="body")]
        public Guid AccountId { get; set; }

        ///<summary>
        ///Id of the bank to connect with.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of the bank to connect with.", Name="BankId", ParameterType="body")]
        public string BankId { get; set; }
    }

    public class StartKevinAuthResponse
        : ResponseBase<KevinAuthorizationLink>
    {
    }

    public class StripePaymentIntent
    {
        public string PaymentId { get; set; }
        public string PaymentClientSecret { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
    }

    public class Subscription
    {
        public string Id { get; set; }
        public string CreatedOn { get; set; }
        public string CurrentPeriodStart { get; set; }
        public string CurrentPeriodEnd { get; set; }
        public string CanceledAt { get; set; }
        public bool CancelAtPeriodEnd { get; set; }
        public string TrialStart { get; set; }
        public string TrialEnd { get; set; }
        public string Status { get; set; }
        public string PlanId { get; set; }
        public string AppliedCoupon { get; set; }
        public string PaymentMethodId { get; set; }
        public string CustomerId { get; set; }
    }

    public class Taxonomy
    {
        public Taxonomy()
        {
            Dependencies = new List<string>{};
            TranslatableFields = new List<string>{};
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ParentId { get; set; }
        public List<string> Dependencies { get; set; }
        public string TermsUiSchema { get; set; }
        public string TermsJsonSchema { get; set; }
        public List<string> TranslatableFields { get; set; }
        public Guid DatabaseId { get; set; }
        public Guid TaxonomyId { get; set; }
    }

    public class Term
    {
        public Term()
        {
            Names = new Dictionary<string, string>{};
            Descriptions = new Dictionary<string, string>{};
            ParentNames = new Dictionary<string, string>{};
            MultiParents = new List<TermMultiParent>{};
        }

        public string TaxonomyId { get; set; }
        public string TaxonomyName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Names { get; set; }
        public Dictionary<string, string> Descriptions { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public Dictionary<string, string> ParentNames { get; set; }
        public int? Order { get; set; }
        public List<TermMultiParent> MultiParents { get; set; }
        public string Meta { get; set; }
    }

    public class TermMultiParent
    {
        public TermMultiParent()
        {
            Names = new Dictionary<string, string>{};
        }

        public string ParentId { get; set; }
        public string TaxonomyId { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Names { get; set; }
    }

    public class Token
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Owner { get; set; }
    }

    ///<summary>
    ///Authentication
    ///</summary>
    [Route("/auth/twitter", "GET POST")]
    [Route("/{version}/auth/twitter", "GET POST")]
    [Api(Description="Authentication")]
    [DataContract]
    public class TwitterAuthenticationRequest
        : CodeMashRequestBase, IReturn<TwitterAuthenticationResponse>, IOAuthRequest
    {
        ///<summary>
        ///Mode to use for authentication
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Mode to use for authentication", Name="Mode", ParameterType="query")]
        public string Mode { get; set; }

        ///<summary>
        ///Code received from Twitter services
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Code received from Twitter services", Name="Code", ParameterType="query")]
        public string Code { get; set; }

        ///<summary>
        ///State received with a code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="State received with a code", Name="State", ParameterType="query")]
        public string State { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessToken", ParameterType="query")]
        public string AccessToken { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessTokenSecret", ParameterType="query")]
        public string AccessTokenSecret { get; set; }

        ///<summary>
        ///Auth token
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Auth token", Name="OAuthToken", ParameterType="query")]
        public string OAuthToken { get; set; }

        ///<summary>
        ///Auth verifier
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Auth verifier", Name="OAuthVerifier", ParameterType="query")]
        public string OAuthVerifier { get; set; }
    }

    public class TwitterAuthenticationResponse
        : ResponseBase<AuthResponse>
    {
    }

    ///<summary>
    ///Unblocks blocked user
    ///</summary>
    [Route("/membership/users/{Id}/unblock", "PUT PATCH")]
    [Route("/{version}/membership/users/{Id}/unblock", "PUT PATCH")]
    [Api(Description="Unblocks blocked user")]
    [DataContract]
    public class UnblockUserRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///User Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User Id", IsRequired=true, Name="Id", ParameterType="path")]
        public Guid Id { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers/{id}", "PATCH PUT")]
    [Route("/{version}/payments/customers/{id}", "PATCH PUT")]
    [Api(Description="Payments")]
    [DataContract]
    public class UpdateCustomerRequest
        : CodeMashRequestBase, IReturnVoid
    {
        public UpdateCustomerRequest()
        {
            Meta = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///Customer's phone. Overrides user's phone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's phone. Overrides user's phone.", Name="Phone", ParameterType="body")]
        public string Phone { get; set; }

        ///<summary>
        ///Customer's full name. Overrides user's name.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's full name. Overrides user's name.", Name="Name", ParameterType="body")]
        public string Name { get; set; }

        ///<summary>
        ///Customer's description.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's description.", Name="Description", ParameterType="body")]
        public string Description { get; set; }

        ///<summary>
        ///Customer's email. Overrides user's email.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's email. Overrides user's email.", Name="Email", ParameterType="body")]
        public string Email { get; set; }

        ///<summary>
        ///Customer's city. Overrides user's city.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's city. Overrides user's city.", Name="City", ParameterType="body")]
        public string City { get; set; }

        ///<summary>
        ///Customer's country. Overrides user's country.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's country. Overrides user's country.", Name="CountryCode", ParameterType="body")]
        public string CountryCode { get; set; }

        ///<summary>
        ///Customer's address 1. Overrides user's address 1.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's address 1. Overrides user's address 1.", Name="Address1", ParameterType="body")]
        public string Address { get; set; }

        ///<summary>
        ///Customer's address 2. Overrides user's address 2.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's address 2. Overrides user's address 2.", Name="Address2", ParameterType="body")]
        public string Address2 { get; set; }

        ///<summary>
        ///Customer's postal code. Overrides user's postal code.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's postal code. Overrides user's postal code.", Name="PostalCode", ParameterType="body")]
        public string PostalCode { get; set; }

        ///<summary>
        ///Customer's state. Overrides user's area.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's state. Overrides user's area.", Name="Area", ParameterType="body")]
        public string Area { get; set; }

        ///<summary>
        ///Additional key, value data.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Additional key, value data.", Name="Meta", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }
    }

    ///<summary>
    ///Applies metadata on the device
    ///</summary>
    [Route("/notifications/push/devices/{Id}/metadata", "PATCH PUT")]
    [Route("/{version}/notifications/push/devices/{Id}/metadata", "PATCH PUT")]
    [Api(Description="Applies metadata on the device")]
    [DataContract]
    public class UpdateDeviceMetaRequest
        : CodeMashRequestBase, IReturn<UpdateDeviceMetaResponse>
    {
        public UpdateDeviceMetaRequest()
        {
            Meta = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Device Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device Id", IsRequired=true, Name="Id", ParameterType="form")]
        public Guid Id { get; set; }

        ///<summary>
        ///Device Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Device Id", IsRequired=true, Name="Id", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }
    }

    public class UpdateDeviceMetaResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Updates device details
    ///</summary>
    [Route("/notifications/push/devices/{Id}", "PATCH")]
    [Route("/{version}/notifications/push/devices/{Id}", "PATCH")]
    [Api(Description="Updates device details")]
    [DataContract]
    public class UpdateDeviceRequest
        : CodeMashRequestBase, IReturnVoid
    {
        public UpdateDeviceRequest()
        {
            Meta = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Device id or device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device id or device key", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///Device operating system
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device operating system", Name="OperatingSystem", ParameterType="body")]
        public string OperatingSystem { get; set; }

        ///<summary>
        ///Device brand
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device brand", Name="Brand", ParameterType="body")]
        public string Brand { get; set; }

        ///<summary>
        ///Device name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device name", Name="DeviceName", ParameterType="body")]
        public string DeviceName { get; set; }

        ///<summary>
        ///Device timezone, expects to get a TZ database name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device timezone, expects to get a TZ database name", Name="TimeZone", ParameterType="body")]
        public string TimeZone { get; set; }

        ///<summary>
        ///Device language, expects to get a 2 letter identifier
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device language, expects to get a 2 letter identifier", Name="Language", ParameterType="body")]
        public string Language { get; set; }

        ///<summary>
        ///Device locale
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device locale", Name="Locale", ParameterType="body")]
        public string Locale { get; set; }

        ///<summary>
        ///Meta information that comes from device. Pass an empty object to delete.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Object", Description="Meta information that comes from device. Pass an empty object to delete.", Name="Meta", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }
    }

    ///<summary>
    ///Updates the time zone of the device
    ///</summary>
    [Route("/notifications/push/devices/{Id}/timezone", "PATCH PUT")]
    [Route("/{version}/notifications/push/devices/{Id}/timezone", "PATCH PUT")]
    [Api(Description="Updates the time zone of the device")]
    [DataContract]
    public class UpdateDeviceTimeZoneRequest
        : CodeMashRequestBase, IReturn<UpdateDeviceTimeZoneResponse>
    {
        ///<summary>
        ///Device Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device Id", IsRequired=true, Name="Id", ParameterType="form")]
        public Guid Id { get; set; }

        ///<summary>
        ///In which time zone device is registered. If we are aware of location, we can provide notifications in right time frame.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="In which time zone device is registered. If we are aware of location, we can provide notifications in right time frame.", Name="TimeZone", ParameterType="form")]
        public string TimeZone { get; set; }
    }

    public class UpdateDeviceTimeZoneResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Attaches user to the device which can receive push notifications
    ///</summary>
    [Route("/notifications/push/devices/{Id}/user", "PATCH")]
    [Route("/{version}/notifications/push/devices/{Id}/user", "PATCH")]
    [Api(Description="Attaches user to the device which can receive push notifications")]
    [DataContract]
    public class UpdateDeviceUserRequest
        : CodeMashRequestBase, IReturn<UpdateDeviceUserResponse>
    {
        ///<summary>
        ///Device Id or device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device Id or device key", IsRequired=true, Name="Id", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///User Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User Id", Name="UserId", ParameterType="body")]
        public Guid? UserId { get; set; }
    }

    public class UpdateDeviceUserResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/discounts/{id}", "PUT PATCH")]
    [Route("/{version}/payments/discounts/{id}", "PUT PATCH")]
    [Api(Description="Payments")]
    [DataContract]
    public class UpdateDiscountRequest
        : CodeMashRequestBase, IReturnVoid
    {
        public UpdateDiscountRequest()
        {
            Boundaries = new List<PaymentDiscountBoundary>{};
            Records = new List<string>{};
            CategoryValues = new List<string>{};
            PaymentAccounts = new List<string>{};
            Users = new List<string>{};
            Emails = new List<string>{};
        }

        ///<summary>
        ///Id of discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of discount.", IsRequired=true, Name="Id", ParameterType="body")]
        public string Id { get; set; }

        ///<summary>
        ///Display name of the discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Display name of the discount.", Name="DisplayName", ParameterType="body")]
        public string DisplayName { get; set; }

        ///<summary>
        ///Start date of being active.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="Start date of being active.", Name="ValidFrom", ParameterType="body")]
        public long? ValidFrom { get; set; }

        ///<summary>
        ///End date of being active.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="End date of being active.", Name="ValidUntil", ParameterType="body")]
        public long? ValidUntil { get; set; }

        ///<summary>
        ///Restriction type of discount. Can be Fixed, Percentage, Price or Quantity.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Restriction type of discount. Can be Fixed, Percentage, Price or Quantity.", Name="RestrictionType", ParameterType="body")]
        public string RestrictionType { get; set; }

        ///<summary>
        ///Discount amount for Fixed or Percentage restriction types.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="double", Description="Discount amount for Fixed or Percentage restriction types.", Name="Amount", ParameterType="body")]
        public double? Amount { get; set; }

        ///<summary>
        ///Discount amounts for Price or Quantity restriction types.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Discount amounts for Price or Quantity restriction types.", Name="Boundaries", ParameterType="body")]
        public List<PaymentDiscountBoundary> Boundaries { get; set; }

        ///<summary>
        ///Target type for specific records. Can be All, Category, Individual.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Target type for specific records. Can be All, Category, Individual.", Name="TargetType", ParameterType="body")]
        public string TargetType { get; set; }

        ///<summary>
        ///Records for Individual target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Records for Individual target type.", Name="Records", ParameterType="body")]
        public List<string> Records { get; set; }

        ///<summary>
        ///Collection field for Category target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Collection field for Category target type.", Name="CategoryField", ParameterType="body")]
        public string CategoryField { get; set; }

        ///<summary>
        ///Values for Category target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Values for Category target type.", Name="CategoryValues", ParameterType="body")]
        public List<string> CategoryValues { get; set; }

        ///<summary>
        ///Limitations for discounts to be used only with certain payment accounts.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Limitations for discounts to be used only with certain payment accounts.", Name="PaymentAccounts", ParameterType="body")]
        public List<string> PaymentAccounts { get; set; }

        ///<summary>
        ///Users who can redeem this discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Users who can redeem this discount.", Name="Users", ParameterType="body")]
        public List<string> Users { get; set; }

        ///<summary>
        ///Emails for potential users who can redeem this discount. When user registers with one of the provided emails, he will be allowed to use this discount. Doesn't work with existing users.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Emails for potential users who can redeem this discount. When user registers with one of the provided emails, he will be allowed to use this discount. Doesn't work with existing users.", Name="Users", ParameterType="body")]
        public List<string> Emails { get; set; }

        ///<summary>
        ///Amount of times that the same user can redeem.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of times that the same user can redeem.", Format="int32", Name="UserCanRedeem", ParameterType="body")]
        public int? UserCanRedeem { get; set; }

        ///<summary>
        ///Amount of times in total this discount can be redeemed.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of times in total this discount can be redeemed.", Format="int32", Name="TotalCanRedeem", ParameterType="body")]
        public int? TotalCanRedeem { get; set; }

        ///<summary>
        ///Should the discount be enabled.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should the discount be enabled.", Name="Enabled", ParameterType="body")]
        public bool? Enabled { get; set; }

        ///<summary>
        ///Should the discount be combined with other discounts
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should the discount be combined with other discounts", Name="CombineDiscounts", ParameterType="body")]
        public bool? CombineDiscounts { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/bulk", "PATCH")]
    [Route("/{version}/db/{CollectionName}/bulk", "PATCH")]
    [Api(Description="Database services")]
    [DataContract]
    public class UpdateManyRequest
        : CodeMashDbRequestBase, IReturn<UpdateManyResponse>
    {
        ///<summary>
        ///The modifications to apply. Use update operators such as $set, $unset, or $rename.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The modifications to apply. Use update operators such as $set, $unset, or $rename.", IsRequired=true, Name="Update", ParameterType="body")]
        public string Update { get; set; }

        ///<summary>
        ///Specify filter (in JSON format) to filter out documents and update them all.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Specify filter (in JSON format) to filter out documents and update them all.", IsRequired=true, Name="Filter", ParameterType="body")]
        public string Filter { get; set; }

        ///<summary>
        ///Records are validated against CodeMash JSON Schema before each update. That can slow down API performance. If you have already validated your data with JSON Schema and you can assure of data integrity, you can bypass document validation by setting this property to true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Records are validated against CodeMash JSON Schema before each update. That can slow down API performance. If you have already validated your data with JSON Schema and you can assure of data integrity, you can bypass document validation by setting this property to true.", Name="BypassDocumentValidation", ParameterType="form")]
        public bool BypassDocumentValidation { get; set; }

        ///<summary>
        ///Triggers are called each time you update the document. You can disable triggers if it's not needed. This is really important when you update the database from trigger itself, so then you can avoid infinite loop.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Triggers are called each time you update the document. You can disable triggers if it's not needed. This is really important when you update the database from trigger itself, so then you can avoid infinite loop.", Name="IgnoreTriggers", ParameterType="form")]
        public bool IgnoreTriggers { get; set; }

        ///<summary>
        ///If your document has file field(s), then you set field value with Id that comes from your file storage provider. E.g.: When your file provider is AWS S3, so then you can set field value to S3 file key, and this will be automatically combined between your file service provider and CodeMash.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your document has file field(s), then you set field value with Id that comes from your file storage provider. E.g.: When your file provider is AWS S3, so then you can set field value to S3 file key, and this will be automatically combined between your file service provider and CodeMash.", Name="ResolveProviderFiles", ParameterType="form")]
        public bool ResolveProviderFiles { get; set; }
    }

    public class UpdateManyResponse
        : ResponseBase<UpdateResult>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}", "PATCH")]
    [Route("/db/{CollectionName}/{id}", "PATCH")]
    [Route("/{version}/db/{CollectionName}", "PATCH")]
    [Route("/{version}/db/{CollectionName}/{Id}", "PATCH")]
    [Api(Description="Database services")]
    [DataContract]
    public class UpdateOneRequest
        : CodeMashDbRequestBase, IReturn<UpdateOneResponse>
    {
        ///<summary>
        ///Id of a record to update. Required if filter is empty.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of a record to update. Required if filter is empty.", Name="Id", ParameterType="path", Route="/{version}/db/{CollectionName}/{Id}")]
        public string Id { get; set; }

        ///<summary>
        ///The modifications to apply. Use update operators such as $set, $unset, or $rename.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The modifications to apply. Use update operators such as $set, $unset, or $rename.", IsRequired=true, Name="Update", ParameterType="body")]
        public string Update { get; set; }

        ///<summary>
        ///Set the filter (in JSON format) to update the first document found in the collection. Required when Id is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Set the filter (in JSON format) to update the first document found in the collection. Required when Id is not set.", Name="Filter", ParameterType="body", Route="/{version}/db/{CollectionName}")]
        public string Filter { get; set; }

        ///<summary>
        ///By default file uploads are done after the record is updated, set to true in case you need to wait for files to be uploaded.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="By default file uploads are done after the record is updated, set to true in case you need to wait for files to be uploaded.", Name="WaitForFileUpload", ParameterType="form")]
        public bool WaitForFileUpload { get; set; }

        ///<summary>
        ///Records are validated against CodeMash JSON Schema before each update. That can slow down API performance. If you have already validated your data with JSON Schema and you can assure of data integrity, you can bypass document validation by setting this property to true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Records are validated against CodeMash JSON Schema before each update. That can slow down API performance. If you have already validated your data with JSON Schema and you can assure of data integrity, you can bypass document validation by setting this property to true.", IsRequired=true, Name="BypassDocumentValidation", ParameterType="form")]
        public bool BypassDocumentValidation { get; set; }

        ///<summary>
        ///Triggers are called each time you update the document. You can disable triggers if it's not needed. This is really important when you update the database from trigger itself, so then you can avoid infinite loop.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="Triggers are called each time you update the document. You can disable triggers if it's not needed. This is really important when you update the database from trigger itself, so then you can avoid infinite loop.", Name="IgnoreTriggers", ParameterType="form")]
        public bool IgnoreTriggers { get; set; }

        ///<summary>
        ///If your document has file field(s), then you set field value with Id that comes from your file storage provider. E.g.: When your file provider is AWS S3, so then you can set field value to S3 file key, and this will be automatically combined between your file service provider and CodeMash.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="boolean", Description="If your document has file field(s), then you set field value with Id that comes from your file storage provider. E.g.: When your file provider is AWS S3, so then you can set field value to S3 file key, and this will be automatically combined between your file service provider and CodeMash.", Name="ResolveProviderFiles", ParameterType="form")]
        public bool ResolveProviderFiles { get; set; }
    }

    public class UpdateOneResponse
        : ResponseBase<UpdateResult>
    {
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/password", "PATCH")]
    [Route("/{version}/membership/users/password", "PATCH")]
    [Api(Description="Membership")]
    [DataContract]
    public class UpdatePasswordRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///User whose password to change.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User whose password to change.", Name="UserId", ParameterType="body")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///Current password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Current password", Name="CurrentPassword", ParameterType="body")]
        public string CurrentPassword { get; set; }

        ///<summary>
        ///New password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New password", IsRequired=true, Name="Password", ParameterType="body")]
        public string Password { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/methods/{id}", "PATCH PUT")]
    [Route("/{version}/payments/methods/{id}", "PATCH PUT")]
    [Api(Description="Payments")]
    [DataContract]
    public class UpdatePaymentMethodRequest
        : CodeMashRequestBase, IReturnVoid
    {
        public UpdatePaymentMethodRequest()
        {
            Meta = new Dictionary<string, string>{};
        }

        ///<summary>
        ///Payment method's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's ID.", IsRequired=true, Name="Id", ParameterType="query")]
        public string Id { get; set; }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", Name="CustomerId", ParameterType="query")]
        public string CustomerId { get; set; }

        ///<summary>
        ///Payment method's phone. Overrides user's phone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's phone. Overrides user's phone.", Name="Phone", ParameterType="body")]
        public string Phone { get; set; }

        ///<summary>
        ///Payment method's full name. Overrides user's name.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's full name. Overrides user's name.", Name="Name", ParameterType="body")]
        public string Name { get; set; }

        ///<summary>
        ///Payment method's description.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's description.", Name="Description", ParameterType="body")]
        public string Description { get; set; }

        ///<summary>
        ///Payment method's email. Overrides user's email.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's email. Overrides user's email.", Name="Email", ParameterType="body")]
        public string Email { get; set; }

        ///<summary>
        ///Payment method's city. Overrides user's city.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's city. Overrides user's city.", Name="City", ParameterType="body")]
        public string City { get; set; }

        ///<summary>
        ///Payment method's country. Overrides user's country.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's country. Overrides user's country.", Name="CountryCode", ParameterType="body")]
        public string CountryCode { get; set; }

        ///<summary>
        ///Payment method's address 1. Overrides user's address 1.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's address 1. Overrides user's address 1.", Name="Address1", ParameterType="body")]
        public string Address { get; set; }

        ///<summary>
        ///Payment method's address 2. Overrides user's address 2.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's address 2. Overrides user's address 2.", Name="Address2", ParameterType="body")]
        public string Address2 { get; set; }

        ///<summary>
        ///Payment method's postal code. Overrides user's postal code.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's postal code. Overrides user's postal code.", Name="PostalCode", ParameterType="body")]
        public string PostalCode { get; set; }

        ///<summary>
        ///Payment method's state. Overrides user's area.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's state. Overrides user's area.", Name="Area", ParameterType="body")]
        public string Area { get; set; }

        ///<summary>
        ///Additional key, value data.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Additional key, value data.", Name="Meta", ParameterType="body")]
        public Dictionary<string, string> Meta { get; set; }
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/profile", "PATCH")]
    [Route("/{version}/membership/users/profile", "PATCH")]
    [Api(Description="Membership")]
    [DataContract]
    public class UpdateProfileRequest
        : CodeMashRequestBase, IReturnVoid
    {
        public UpdateProfileRequest()
        {
            UnsubscribedNewsTags = new List<string>{};
        }

        ///<summary>
        ///Guest email. Will not work for normal user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Guest email. Will not work for normal user.", Name="Email", ParameterType="body")]
        public string Email { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="form")]
        public string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="form")]
        public string LastName { get; set; }

        ///<summary>
        ///Member display name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member display name", Name="DisplayName", ParameterType="form")]
        public string DisplayName { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="form")]
        public string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="form")]
        public string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="form")]
        public string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="form")]
        public bool? SubscribeToNews { get; set; }

        ///<summary>
        ///Marketing email types to unsubscribe from
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Marketing email types to unsubscribe from", Name="UnsubscribedNewsTags", ParameterType="form")]
        public List<string> UnsubscribedNewsTags { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public string Address2 { get; set; }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", Name="Phone", ParameterType="body")]
        public string Phone { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public string BirthDate { get; set; }
    }

    public class UpdateResult
    {
        public bool IsAcknowledged { get; set; }
        public long MatchedCount { get; set; }
        public long ModifiedCount { get; set; }
        public string UpsertedId { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers/{customerId}/subscriptions/{id}", "PATCH")]
    [Route("/{version}/payments/customers/{customerId}/subscriptions/{id}", "PATCH")]
    [Api(Description="Payments")]
    [DataContract]
    public class UpdateSubscriptionRequest
        : CodeMashRequestBase, IReturnVoid
    {
        ///<summary>
        ///Subscription ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Subscription ID.", IsRequired=true, Name="SubscriptionId", ParameterType="path")]
        public string Id { get; set; }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", IsRequired=true, Name="CustomerId", ParameterType="path")]
        public string CustomerId { get; set; }

        ///<summary>
        ///Discount coupon ID if supported.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Discount coupon ID if supported.", Name="Coupon", ParameterType="body")]
        public string Coupon { get; set; }

        ///<summary>
        ///Payment method's ID to use for subscription.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's ID to use for subscription.", Name="PaymentMethodId", ParameterType="body")]
        public string PaymentMethodId { get; set; }

        ///<summary>
        ///If subscription is set to cancel at period end, renews the subscription.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If subscription is set to cancel at period end, renews the subscription.", Name="RenewCanceled", ParameterType="body")]
        public bool RenewCanceled { get; set; }
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users", "PATCH PUT")]
    [Route("/membership/users/{id}", "PATCH PUT")]
    [Route("/{version}/membership/users", "PATCH PUT")]
    [Route("/{version}/membership/users/{id}", "PATCH PUT")]
    [Api(Description="Membership")]
    [DataContract]
    public class UpdateUserRequest
        : CodeMashRequestBase, IReturnVoid
    {
        public UpdateUserRequest()
        {
            Roles = new List<string>{};
            RolesTree = new List<UserRoleUpdateInput>{};
            UnsubscribedNewsTags = new List<string>{};
        }

        ///<summary>
        ///User Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User Id", IsRequired=true, Name="Id", ParameterType="body")]
        public Guid Id { get; set; }

        ///<summary>
        ///Guest email. Will not work for normal user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Guest email. Will not work for normal user.", Name="Email", ParameterType="body")]
        public string Email { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="body")]
        public string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="body")]
        public string LastName { get; set; }

        ///<summary>
        ///Member display name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member display name", Name="DisplayName", ParameterType="body")]
        public string DisplayName { get; set; }

        ///<summary>
        ///Role names to be applied
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Role names to be applied", Name="Roles", ParameterType="body")]
        public List<string> Roles { get; set; }

        ///<summary>
        ///Full permission tree
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Full permission tree", Name="RolesTree", ParameterType="body")]
        public List<UserRoleUpdateInput> RolesTree { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="body")]
        public string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="body")]
        public string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="body")]
        public string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="body")]
        public bool? SubscribeToNews { get; set; }

        ///<summary>
        ///Marketing email types to unsubscribe from
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Marketing email types to unsubscribe from", Name="UnsubscribedNewsTags", ParameterType="body")]
        public List<string> UnsubscribedNewsTags { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public string Address2 { get; set; }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", Name="Phone", ParameterType="body")]
        public string Phone { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public string BirthDate { get; set; }

        ///<summary>
        ///User's zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's zone", Name="Zone", ParameterType="body")]
        public string Zone { get; set; }
    }

    ///<summary>
    ///File services
    ///</summary>
    [Route("/files", "POST")]
    [Route("/{version}/files", "POST")]
    [Api(Description="File services")]
    [DataContract]
    public class UploadFileRequest
        : CodeMashRequestBase, IReturn<UploadFileResponse>
    {
        ///<summary>
        ///Path of directory to store the file into. Leave it as empty to store file into root directory
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Path of directory to store the file into. Leave it as empty to store file into root directory", IsRequired=true, Name="Path", ParameterType="form")]
        public string Path { get; set; }

        ///<summary>
        ///Alternative way to upload a file by providing a base64 encoded string
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Alternative way to upload a file by providing a base64 encoded string", Name="Base64File", ParameterType="body")]
        public Base64FileUpload Base64File { get; set; }

        ///<summary>
        ///File account ID. If not provided, default account will be used.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="File account ID. If not provided, default account will be used.", Name="AccountId", ParameterType="body")]
        public Guid? AccountId { get; set; }
    }

    public class UploadFileResponse
        : ResponseBase<File>
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public long UploadDate { get; set; }
    }

    ///<summary>
    ///File services
    ///</summary>
    [Route("/payments/orders/{id}/files", "POST")]
    [Route("/{version}/payments/orders/{id}/files", "POST")]
    [Api(Description="File services")]
    [DataContract]
    public class UploadOrderFileRequest
        : CodeMashRequestBase, IReturn<UploadOrderFileResponse>
    {
        ///<summary>
        ///ID of an order to upload this file for.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of an order to upload this file for.", IsRequired=true, Name="Id", ParameterType="form")]
        public string Id { get; set; }

        ///<summary>
        ///Category of a file inside order.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Category of a file inside order.", Name="Category", ParameterType="form")]
        public string Category { get; set; }

        ///<summary>
        ///Alternative way to upload a file by providing a base64 encoded string
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Alternative way to upload a file by providing a base64 encoded string", Name="Base64File", ParameterType="body")]
        public Base64FileUpload Base64File { get; set; }
    }

    public class UploadOrderFileResponse
        : ResponseBase<File>
    {
        public string Key { get; set; }
    }

    ///<summary>
    ///File services
    ///</summary>
    [Route("/db/{collectionName}/files", "POST")]
    [Route("/{version}/db/{collectionName}/files", "POST")]
    [Api(Description="File services")]
    [DataContract]
    public class UploadRecordFileRequest
        : CodeMashDbRequestBase, IReturn<UploadRecordFileResponse>
    {
        ///<summary>
        ///ID of a record to upload this file for. If empty, creates a temporary file which then can be saved during record operations.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of a record to upload this file for. If empty, creates a temporary file which then can be saved during record operations.", Name="RecordId", ParameterType="form")]
        public string RecordId { get; set; }

        ///<summary>
        ///Record file field name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Record file field name", IsRequired=true, Name="UniqueFieldName", ParameterType="form")]
        public string UniqueFieldName { get; set; }

        ///<summary>
        ///Alternative way to upload a file by providing a base64 encoded string
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Alternative way to upload a file by providing a base64 encoded string", Name="Base64File", ParameterType="body")]
        public Base64FileUpload Base64File { get; set; }

        ///<summary>
        ///If true, does not activate triggers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, does not activate triggers", Name="IgnoreTriggers", ParameterType="body")]
        public bool IgnoreTriggers { get; set; }
    }

    public class UploadRecordFileResponse
        : ResponseBase<File>
    {
        public string Key { get; set; }
    }

    ///<summary>
    ///File services
    ///</summary>
    [Route("/notifications/server-events/messages/files", "POST")]
    [Route("/{version}/notifications/server-events/messages/files", "POST")]
    [Api(Description="File services")]
    [DataContract]
    public class UploadSseMessageFileRequest
        : CodeMashRequestBase, IReturn<UploadSseMessageFileResponse>
    {
        ///<summary>
        ///Channel to send message to
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Channel to send message to", IsRequired=true, Name="ChannelId", ParameterType="form")]
        public string ChannelId { get; set; }

        ///<summary>
        ///Alternative way to upload a file by providing a base64 encoded string
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Alternative way to upload a file by providing a base64 encoded string", Name="Base64File", ParameterType="body")]
        public Base64FileUpload Base64File { get; set; }
    }

    public class UploadSseMessageFileResponse
        : ResponseBase<File>
    {
        public string Key { get; set; }
    }

    ///<summary>
    ///File services
    ///</summary>
    [Route("/membership/users/files", "POST")]
    [Route("/{version}/membership/users/files", "POST")]
    [Api(Description="File services")]
    [DataContract]
    public class UploadUserFileRequest
        : CodeMashRequestBase, IReturn<UploadUserFileResponse>
    {
        ///<summary>
        ///ID of a user to upload this file for. If empty, creates a temporary file which then can be saved during user save operations.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of a user to upload this file for. If empty, creates a temporary file which then can be saved during user save operations.", Name="UserId", ParameterType="form")]
        public Guid? UserId { get; set; }

        ///<summary>
        ///User meta file field name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User meta file field name", IsRequired=true, Name="MetaFieldName", ParameterType="form")]
        public string MetaFieldName { get; set; }

        ///<summary>
        ///Alternative way to upload a file by providing a base64 encoded string
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Alternative way to upload a file by providing a base64 encoded string", Name="Base64File", ParameterType="body")]
        public Base64FileUpload Base64File { get; set; }
    }

    public class UploadUserFileResponse
        : ResponseBase<File>
    {
        public string Key { get; set; }
    }

    public class User
    {
        public User()
        {
            Roles = new List<Role>{};
            Devices = new List<Device>{};
            AuthProviders = new List<UserAuthProvider>{};
            UnsubscribedNewsTags = new List<string>{};
        }

        public string Id { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<Role> Roles { get; set; }
        public List<Device> Devices { get; set; }
        public bool RolesEditable { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Meta { get; set; }
        public string Language { get; set; }
        public string TimeZone { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string CompanyCode { get; set; }
        public string PostalCode { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string Zone { get; set; }
        public List<UserAuthProvider> AuthProviders { get; set; }
        public bool HasCredentials { get; set; }
        public bool SubscribedToNews { get; set; }
        public List<string> UnsubscribedNewsTags { get; set; }
        public long? UnreadNotifications { get; set; }
    }

    public class UserAuthProvider
    {
        public string Provider { get; set; }
        public string UserId { get; set; }
    }

    public class UserPolicyUpdateInput
    {
        public UserPolicyUpdateInput()
        {
            Permissions = new List<string>{};
        }

        public string Policy { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class UserRoleUpdateInput
    {
        public UserRoleUpdateInput()
        {
            Policies = new List<UserPolicyUpdateInput>{};
        }

        public string Role { get; set; }
        public List<UserPolicyUpdateInput> Policies { get; set; }
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/invitation/token", "GET")]
    [Route("/{version}/membership/users/invitation/token", "GET")]
    [Api(Description="Membership")]
    [DataContract]
    public class ValidateInvitationTokenRequest
        : RequestBase, IReturn<ValidateInvitationTokenResponse>
    {
        ///<summary>
        ///Secret token received by email for invitation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Secret token received by email for invitation", IsRequired=true, Name="Token", ParameterType="query")]
        public string Token { get; set; }

        ///<summary>
        ///If true, includes project details in response
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, includes project details in response", IsRequired=true, Name="IncludeProject", ParameterType="query")]
        public bool IncludeProject { get; set; }
    }

    public class ValidateInvitationTokenResponse
        : ResponseBase<bool>
    {
        public Project Project { get; set; }
        public Guid UserId { get; set; }
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/password/reset/token", "GET")]
    [Route("/{version}/membership/users/password/reset/token", "GET")]
    [Api(Description="Membership")]
    [DataContract]
    public class ValidatePasswordTokenRequest
        : RequestBase, IReturn<ValidatePasswordTokenResponse>
    {
        ///<summary>
        ///Secret token received by email for password reset
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Secret token received by email for password reset", IsRequired=true, Name="Token", ParameterType="query")]
        public string Token { get; set; }

        ///<summary>
        ///If true, includes project details in response
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, includes project details in response", IsRequired=true, Name="IncludeProject", ParameterType="query")]
        public bool IncludeProject { get; set; }
    }

    public class ValidatePasswordTokenResponse
        : ResponseBase<bool>
    {
        public Project Project { get; set; }
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/deactivate/token", "GET")]
    [Route("/{version}/membership/users/deactivate/token", "GET")]
    [Api(Description="Membership")]
    [DataContract]
    public class ValidateUserDeactivationTokenRequest
        : RequestBase, IReturn<ValidateUserDeactivationTokenResponse>
    {
        ///<summary>
        ///Secret token received by email for user deactivation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Secret token received by email for user deactivation", IsRequired=true, Name="Token", ParameterType="query")]
        public string Token { get; set; }

        ///<summary>
        ///If true, includes project details in response
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, includes project details in response", IsRequired=true, Name="IncludeProject", ParameterType="query")]
        public bool IncludeProject { get; set; }
    }

    public class ValidateUserDeactivationTokenResponse
        : ResponseBase<bool>
    {
        public Project Project { get; set; }
        public bool HasCredentials { get; set; }
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers/subscriptions/apple", "POST")]
    [Route("/{version}/payments/customers/subscriptions/apple", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class VerifyAppleAppStoreSubscriptionRequest
        : CodeMashRequestBase, IReturn<VerifyAppleAppStoreSubscriptionResponse>
    {
        ///<summary>
        ///Payment receipt.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment receipt.", IsRequired=true, Name="Receipt", ParameterType="body")]
        public string Receipt { get; set; }

        ///<summary>
        ///ID of customer who subscribed. Required for first time calling this endpoint.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of customer who subscribed. Required for first time calling this endpoint.", Name="CustomerId", ParameterType="body")]
        public string CustomerId { get; set; }

        ///<summary>
        ///Subscription plan ID. Required for first time calling this endpoint.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Subscription plan ID. Required for first time calling this endpoint.", Name="PlanId", ParameterType="body")]
        public Guid PlanId { get; set; }
    }

    public class VerifyAppleAppStoreSubscriptionResponse
        : ResponseBase<List<Subscription>>
    {
    }

    ///<summary>
    ///Payments
    ///</summary>
    [Route("/payments/customers/subscriptions/google", "POST")]
    [Route("/{version}/payments/customers/subscriptions/google", "POST")]
    [Api(Description="Payments")]
    [DataContract]
    public class VerifyGooglePlayStoreSubscriptionRequest
        : CodeMashRequestBase, IReturn<VerifyGooglePlayStoreSubscriptionResponse>
    {
        ///<summary>
        ///Payment receipt.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment receipt.", IsRequired=true, Name="Receipt", ParameterType="body")]
        public string Receipt { get; set; }

        ///<summary>
        ///ID of customer who subscribed. Required for first time calling this endpoint.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of customer who subscribed. Required for first time calling this endpoint.", Name="CustomerId", ParameterType="body")]
        public string CustomerId { get; set; }

        ///<summary>
        ///Subscription plan ID. Required for first time calling this endpoint.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Subscription plan ID. Required for first time calling this endpoint.", Name="PlanId", ParameterType="body")]
        public Guid PlanId { get; set; }
    }

    public class VerifyGooglePlayStoreSubscriptionResponse
        : ResponseBase<List<Subscription>>
    {
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/invitation/verify", "POST")]
    [Route("/{version}/membership/users/invitation/verify", "POST")]
    [Api(Description="Membership")]
    [DataContract]
    public class VerifyUserInvitationRequest
        : RequestBase, IReturnVoid
    {
        ///<summary>
        ///Secret token received by email for invitation confirmation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Secret token received by email for invitation confirmation", IsRequired=true, Name="Token", ParameterType="body")]
        public string Token { get; set; }

        ///<summary>
        ///New user password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New user password", IsRequired=true, Name="Password", ParameterType="body")]
        public string Password { get; set; }

        ///<summary>
        ///New repeated user password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New repeated user password", IsRequired=true, Name="RepeatedPassword", ParameterType="body")]
        public string RepeatedPassword { get; set; }
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users/verify", "POST")]
    [Route("/{version}/membership/users/verify", "POST")]
    [Api(Description="Membership")]
    [DataContract]
    public class VerifyUserRequest
        : RequestBase, IReturnVoid
    {
        ///<summary>
        ///Secret token received by email for registration confirmation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Secret token received by email for registration confirmation", IsRequired=true, Name="Token", ParameterType="body")]
        public string Token { get; set; }
    }

    public class AccountDto
    {
        public AccountDto()
        {
            Members = new List<MemberDto>{};
            Tokens = new List<Token>{};
            Projects = new List<ProjectSmallDto>{};
            ProjectBilling = new Dictionary<string, ProjectBillingSettingsDto>{};
            Customers = new List<CustomerSettingsDto>{};
            SubscriptionInvoices = new List<SubscriptionInvoice>{};
            UsageInvoices = new List<UsageInvoice>{};
            CustomerInvoices = new List<CustomerInvoice>{};
            RefundInvoices = new List<RefundInvoice>{};
            Discounts = new List<DiscountDto>{};
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public AccountStatus Status { get; set; }
        public List<MemberDto> Members { get; set; }
        public List<Token> Tokens { get; set; }
        public DatabaseCredentials DatabaseCredentials { get; set; }
        public List<ProjectSmallDto> Projects { get; set; }
        public Dictionary<string, ProjectBillingSettingsDto> ProjectBilling { get; set; }
        public List<CustomerSettingsDto> Customers { get; set; }
        public Subscription Subscription { get; set; }
        public List<SubscriptionInvoice> SubscriptionInvoices { get; set; }
        public List<UsageInvoice> UsageInvoices { get; set; }
        public List<CustomerInvoice> CustomerInvoices { get; set; }
        public List<RefundInvoice> RefundInvoices { get; set; }
        public SubscriptionInvoice SubscriptionUnpaidInvoice { get; set; }
        public CardDto Card { get; set; }
        public BillingDto Billing { get; set; }
        public List<DiscountDto> Discounts { get; set; }
        public string CustomerId { get; set; }
    }

    public class BillingDto
    {
        public string Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string Vat { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Iban { get; set; }
        public string BillingEmail { get; set; }
    }

    public class CardDto
    {
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public string Last4 { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string ZipCode { get; set; }
        public string Funding { get; set; }
    }

    public class CustomerSettingsDto
    {
        public CardDto Card { get; set; }
        public string UserId { get; set; }
    }

    public class DiscountDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsPercentDiscount { get; set; }
        public double PercentOff { get; set; }
        public decimal FixedDiscount { get; set; }
        public decimal UsedAmount { get; set; }
        public string DurationType { get; set; }
        public long AppliedOn { get; set; }
        public string AppliedOnString { get; set; }
        public long? ValidUntil { get; set; }
        public string ValidUntilString { get; set; }
        public string DiscountType { get; set; }
    }

    public class MemberDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
    }

    public class ProjectBillingChargeDto
    {
        public string BillingType { get; set; }
        public string MarginType { get; set; }
        public double MarginPercent { get; set; }
        public decimal FixedPrice { get; set; }
        public bool ChargeCustomer { get; set; }
    }

    public class ProjectBillingInvoiceDto
    {
        public string NumberPrefix { get; set; }
    }

    public class ProjectBillingSettingsDto
    {
        public bool Enabled { get; set; }
        public ProjectBillingChargeDto Charge { get; set; }
        public ProjectBillingInvoiceDto Invoice { get; set; }
        public BillingDto Billing { get; set; }
    }

    public class ProjectContextDto
    {
        public ProjectSmallDto Project { get; set; }
        public AccountDto Account { get; set; }
    }

    public class ProjectSmallDto
    {
        public ProjectSmallDto()
        {
            Tokens = new List<Token>{};
            Languages = new List<string>{};
            UserZones = new List<string>{};
            AllowedOrigins = new List<string>{};
            Widgets = new List<ProjectWidgetDto>{};
        }

        public Guid Id { get; set; }
        public bool DatabaseEstablished { get; set; }
        public bool DatabaseEnabled { get; set; }
        public int? DatabaseVersion { get; set; }
        public bool EmailEstablished { get; set; }
        public bool EmailEnabled { get; set; }
        public bool MembershipEstablished { get; set; }
        public bool MembershipEnabled { get; set; }
        public bool LoggingEstablished { get; set; }
        public bool LoggingEnabled { get; set; }
        public bool ServerEventsEstablished { get; set; }
        public bool ServerEventsEnabled { get; set; }
        public bool NotificationEstablished { get; set; }
        public bool NotificationEnabled { get; set; }
        public bool SchedulerEstablished { get; set; }
        public bool SchedulerEnabled { get; set; }
        public bool ServerlessEstablished { get; set; }
        public bool ServerlessEnabled { get; set; }
        public bool FilingEstablished { get; set; }
        public bool FilingEnabled { get; set; }
        public int? FilesVersion { get; set; }
        public bool PaymentEstablished { get; set; }
        public bool PaymentEnabled { get; set; }
        public bool AuthorizationEnabled { get; set; }
        public bool AuthenticationEnabled { get; set; }
        public bool BackupsEnabled { get; set; }
        public DatabaseCredentials DatabaseCredentials { get; set; }
        public List<Token> Tokens { get; set; }
        public string LogoUrl { get; set; }
        public string Url { get; set; }
        public string LogoId { get; set; }
        public List<string> Languages { get; set; }
        public string DefaultLanguage { get; set; }
        public ProjectZoneDto ProjectZone { get; set; }
        public List<string> UserZones { get; set; }
        public string DefaultTimeZone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SlugifiedName { get; set; }
        public ProjectModulesData ModuleData { get; set; }
        public List<string> AllowedOrigins { get; set; }
        public List<ProjectWidgetDto> Widgets { get; set; }
        public int Connections { get; set; }
        public int Members { get; set; }
        public bool CanCallDatabaseService { get; set; }
        public bool CanCallEmailService { get; set; }
        public bool CanCallMembershipService { get; set; }
        public bool CanCallFilingService { get; set; }
        public bool CanCallLoggingService { get; set; }
        public bool CanCallNotificationService { get; set; }
        public bool CanCallSchedulerService { get; set; }
        public bool CanCallServerlessService { get; set; }
        public bool CanCallPaymentService { get; set; }
        public bool CanCallServerEventsService { get; set; }
    }

    public class ProjectWidgetDto
    {
        public string WidgetType { get; set; }
        public string Module { get; set; }
    }

    public class ProjectZoneDto
    {
        public string UniqueName { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
    }

    public class ServerlessFunctionAliasDto
    {
        public string Name { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public string CreatedOn { get; set; }
        public string Version { get; set; }
        public string AdditionalVersion { get; set; }
        public double AdditionalVersionWeight { get; set; }
        public string Description { get; set; }
    }

    public class ServerlessFunctionDto
    {
        public ServerlessFunctionDto()
        {
            Environment = new Dictionary<string, string>{};
            Aliases = new List<ServerlessFunctionAliasDto>{};
            Versions = new List<ServerlessFunctionVersionDto>{};
            Meta = new Dictionary<string, string>{};
            AvailableTokens = new List<string>{};
            TokenResolvers = new Dictionary<string, TokenResolverField>{};
            Tags = new List<string>{};
            ResourcesTriggerUsages = new List<string>{};
        }

        public string Id { get; set; }
        public long? ModifiedOn { get; set; }
        public long? RefreshOn { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string MainModule { get; set; }
        public string Runtime { get; set; }
        public long? CodeSize { get; set; }
        public string Region { get; set; }
        public ServerlessProvider Provider { get; set; }
        public string Template { get; set; }
        public bool IsCreated { get; set; }
        public string Handler { get; set; }
        public long Memory { get; set; }
        public int TimeoutMinutes { get; set; }
        public int TimeoutSeconds { get; set; }
        public Dictionary<string, string> Environment { get; set; }
        public List<ServerlessFunctionAliasDto> Aliases { get; set; }
        public List<ServerlessFunctionVersionDto> Versions { get; set; }
        public bool IsSystem { get; set; }
        public bool IsMultiple { get; set; }
        public bool MustConfigure { get; set; }
        public string SystemVersion { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public List<string> AvailableTokens { get; set; }
        public Dictionary<string, TokenResolverField> TokenResolvers { get; set; }
        public string DocsId { get; set; }
        public List<string> Tags { get; set; }
        public List<string> ResourcesTriggerUsages { get; set; }
        public string AuthProvider { get; set; }
        public string InvokeUrl { get; set; }
        public string ServiceAccount { get; set; }
    }

    public class ServerlessFunctionVersionDto
    {
        public string Version { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public string CreatedOn { get; set; }
        public string Description { get; set; }
        public string Runtime { get; set; }
        public string Handler { get; set; }
        public long Memory { get; set; }
        public int TimeoutMinutes { get; set; }
        public int TimeoutSeconds { get; set; }
        public long CodeSize { get; set; }
    }

    public class ServerlessProviderDto
    {
        public ServerlessProviderDto()
        {
            Regions = new List<string>{};
            Tags = new Dictionary<string, string>{};
            SecretKeys = new Dictionary<string, string>{};
        }

        public string Provider { get; set; }
        public int TotalFunctions { get; set; }
        public int RefreshRate { get; set; }
        public long LastUpdated { get; set; }
        public bool IsSystem { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string EncVersion { get; set; }
        public List<string> Regions { get; set; }
        public Dictionary<string, string> Tags { get; set; }
        public string ClientId { get; set; }
        public string PrivateKeyEnding { get; set; }
        public string SubscriptionId { get; set; }
        public string ResourceGroup { get; set; }
        public Dictionary<string, string> SecretKeys { get; set; }
        public string OrgId { get; set; }
    }

    public class SystemFunctionExecutorData
    {
        public SystemFunctionExecutorData()
        {
            Meta = new Dictionary<string, string>{};
            Tokens = new Dictionary<string, string>{};
        }

        public Guid ProjectId { get; set; }
        public Guid AccountId { get; set; }
        public ServerlessFunctionDto Function { get; set; }
        public ServerlessProviderDto Connection { get; set; }
        public string Data { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public Dictionary<string, string> Tokens { get; set; }
        public Guid? ActivatorId { get; set; }
        public ProjectSmallDto Project { get; set; }
        public string CultureCode { get; set; }
        public string Version { get; set; }
    }

}

