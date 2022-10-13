/* Options:
Date: 2022-10-13 21:12:11
Version: 6.02
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:5002

GlobalNamespace: CodeMash.Tests.Types.Api
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
using ServiceStack.Auth;
using System.IO;
using CodeMash.Tests.Types.Api;

namespace CodeMash.Tests.Types.Api
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
        [ApiMember(DataType="integer", Description="Amount of records to return", Format="int32", Name="PageSize", ParameterType="query")]
        public virtual int PageSize { get; set; }

        ///<summary>
        ///Page of records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Page of records to return", Format="int32", Name="PageNumber", ParameterType="query")]
        public virtual int PageNumber { get; set; }

        ///<summary>
        ///A query that specifies which records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies which records to return", Name="Filter", ParameterType="query")]
        public virtual string Filter { get; set; }

        ///<summary>
        ///A query that specifies how to sort filtered records
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies how to sort filtered records", Name="Sort", ParameterType="query")]
        public virtual string Sort { get; set; }

        ///<summary>
        ///A query that specifies what fields in records to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies what fields in records to return", Name="Projection", ParameterType="query")]
        public virtual string Projection { get; set; }
    }

    public class CodeMashDbRequestBase
        : CodeMashRequestBase
    {
        ///<summary>
        ///Collection name - unique table name without whitespaces
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Collection name - unique table name without whitespaces", IsRequired=true, Name="CollectionName", ParameterType="path")]
        public virtual string CollectionName { get; set; }

        ///<summary>
        ///API key of your cluster. Can be passed in a header as X-CM-Cluster.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="API key of your cluster. Can be passed in a header as X-CM-Cluster.", Name="Cluster", ParameterType="query")]
        public virtual string Cluster { get; set; }
    }

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

    public class AadFunctionExecutorRequest
    {
        public virtual SystemFunctionExecutorData Data { get; set; }
    }

    [Route("/notifications/email/aws/events", "POST")]
    public class AwsSesEndpoint
        : IReturn<HttpResult>
    {
    }

    public class BarCodeExecutorRequest
    {
        public virtual SystemFunctionExecutorData Data { get; set; }
    }

    public class CollectionFindExecutorRequest
    {
        public virtual SystemFunctionExecutorData Data { get; set; }
    }

    public class CollectionUpdateExecutorRequest
    {
        public virtual SystemFunctionExecutorData Data { get; set; }
    }

    public class DocxTemplateExecutorRequest
    {
        public virtual SystemFunctionExecutorData Data { get; set; }
    }

    public class GoogleFunctionExecutorRequest
    {
        public virtual SystemFunctionExecutorData Data { get; set; }
    }

    public class HtmlToPdfExecutorRequest
    {
        public virtual SystemFunctionExecutorData Data { get; set; }
    }

    public class ImageResizeExecutorRequest
    {
        public virtual SystemFunctionExecutorData Data { get; set; }
    }

    [Route("/notifications/email/mailgun/events", "POST")]
    public class MailGunEndpoint
        : IReturn<HttpResult>
    {
    }

    public class QrCodeExecutorRequest
    {
        public virtual SystemFunctionExecutorData Data { get; set; }
    }

    public class RegisterOAuthUser
        : IReturn<RegisterOAuthUserResponse>
    {
        public virtual Guid ProjectId { get; set; }
        public virtual string ProviderUserId { get; set; }
        public virtual ProjectUserProviders Provider { get; set; }
        public virtual CustomUserSession OnRegistersSession { get; set; }
        public virtual string Mode { get; set; }
    }

    public class RegisterOAuthUserResponse
        : ResponseBase<bool>
    {
        public virtual Guid UserId { get; set; }
        public virtual string AccountId { get; set; }
        public virtual IUserAuth UserAuth { get; set; }
    }

    [Route("/notifications/email/sendgrid/events", "POST")]
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

        public virtual ProjectContextDto ProjectContext { get; set; }
        public virtual ServerlessFunctionDto Function { get; set; }
        public virtual ServerlessProviderDto Connection { get; set; }
        public virtual string Template { get; set; }
        public virtual string Data { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual Dictionary<string, string> Tokens { get; set; }
        public virtual ProjectSmallDto Project { get; set; }
        public virtual Guid? ActivatorId { get; set; }
        public virtual string CultureCode { get; set; }
        public virtual string Version { get; set; }
    }

    public class UsersFindExecutorRequest
    {
        public virtual SystemFunctionExecutorData Data { get; set; }
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
        public virtual string WidgetType { get; set; }
        public virtual ModuleWidget Settings { get; set; }
    }

    public class ModuleWidget
    {
    }

    public class ProjectModulesData
    {
        public virtual ModuleData Database { get; set; }
        public virtual ModuleData Membership { get; set; }
        public virtual ModuleData Emails { get; set; }
        public virtual ModuleData Notifications { get; set; }
        public virtual ModuleData Files { get; set; }
        public virtual ModuleData Logging { get; set; }
        public virtual ModuleData Scheduler { get; set; }
        public virtual ModuleData Serverless { get; set; }
    }

    public class CustomUserSession
        : AuthUserSession, IIdentityProvider
    {
        public CustomUserSession()
        {
            AllowedProjects = new List<string>{};
        }

        public virtual Guid? ProjectId { get; set; }
        public virtual bool IsProjectAdmin { get; set; }
        public virtual string ApiKey { get; set; }
        public virtual string UserId { get; set; }
        public virtual string SessionId { get; set; }
        public virtual string TenantId { get; set; }
        public virtual string ApplicationId { get; set; }
        public virtual List<string> AllowedProjects { get; set; }
        public virtual bool IsAccountUser { get; set; }
        public virtual AccountStatus Status { get; set; }
        public virtual bool IsTokenUser { get; set; }
    }

    public class BaseInvoice
    {
        public virtual DateTime Created { get; set; }
        public virtual string GeneratedInvoiceId { get; set; }
        public virtual string InvoiceId { get; set; }
        public virtual string InvoiceNumber { get; set; }
        public virtual string CustomInvoiceNumber { get; set; }
        public virtual long? CmInvoiceNo { get; set; }
        public virtual string CmInvoiceNumber { get; set; }
        public virtual long AmountPaid { get; set; }
        public virtual long AmountRemaining { get; set; }
        public virtual long AttemptCount { get; set; }
        public virtual string Currency { get; set; }
        public virtual string InvoiceUrl { get; set; }
        public virtual string InvoicePdf { get; set; }
        public virtual bool Paid { get; set; }
        public virtual string Status { get; set; }
    }

    public class CustomerInvoice
        : BaseInvoice
    {
        public virtual string CustomerId { get; set; }
        public virtual Guid ProjectId { get; set; }
        public virtual bool IsStaticInvoice { get; set; }
        public virtual string StaticProjectName { get; set; }
        public virtual string StaticToken { get; set; }
        public virtual int StaticCustomerInvoiceNumber { get; set; }
    }

    public class RefundInvoice
        : BaseInvoice
    {
        public virtual string SubscriptionId { get; set; }
        public virtual SubscriptionPlan Plan { get; set; }
        public virtual int? DaysUnused { get; set; }
        public virtual string RefundedInvoiceId { get; set; }
    }

    public class SubscriptionInvoice
        : BaseInvoice
    {
        public virtual string SubscriptionId { get; set; }
        public virtual SubscriptionPlan Plan { get; set; }
    }

    public class UsageInvoice
        : BaseInvoice
    {
    }

    public class Subscription
    {
        public virtual string SubscriptionId { get; set; }
        public virtual SubscriptionPlan Plan { get; set; }
        public virtual SubscriptionStatus Status { get; set; }
        public virtual DateTime SubscribeDate { get; set; }
        public virtual DateTime From { get; set; }
        public virtual DateTime To { get; set; }
        public virtual bool PeriodPaid { get; set; }
        public virtual int PayAttemptCount { get; set; }
        public virtual bool PayAttempted { get; set; }
        public virtual DateTime? Canceled { get; set; }
        public virtual DateTime? SuspendOn { get; set; }
        public virtual bool IsTrialPeriod { get; set; }
        public virtual bool IsCurrentAndActive { get; set; }
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

    public class DatabaseCredentials
    {
        public virtual string DbName { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Region { get; set; }
        public virtual string SrvClusterName { get; set; }
        public virtual bool UseSrvName { get; set; }
        public virtual string EncVersion { get; set; }
    }

    public enum ServerlessProvider
    {
        None,
        CodemashAmazon,
        Amazon,
        Azure,
        Google,
    }

    public class TokenResolverField
    {
        public virtual string Name { get; set; }
        public virtual string Config { get; set; }
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
        public virtual string Mode { get; set; }

        ///<summary>
        ///Code received from Microsoft services
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Code received from Microsoft services", Name="Code", ParameterType="query")]
        public virtual string Code { get; set; }

        ///<summary>
        ///State received with a code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="State received with a code", Name="State", ParameterType="query")]
        public virtual string State { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessToken", ParameterType="query")]
        public virtual string AccessToken { get; set; }
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
        ///ID of an aggregate. Required of Pipeline is empty.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="ID of an aggregate. Required of Pipeline is empty.", Name="Id", ParameterType="path")]
        public virtual Guid Id { get; set; }

        ///<summary>
        ///Tokens that should be injected into aggregation document
        ///</summary>
        [DataMember]
        [ApiMember(DataType="json", Description="Tokens that should be injected into aggregation document", Name="Tokens", ParameterType="query")]
        public virtual Dictionary<string, string> Tokens { get; set; }
    }

    public class AggregateResponse
        : ResponseBase<string>
    {
    }

    public class AndroidBackgroundLayout
    {
        public virtual string Image { get; set; }
        public virtual string HeadingColor { get; set; }
        public virtual string ContentColor { get; set; }
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
        public virtual string Mode { get; set; }

        ///<summary>
        ///Code received from Google services
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Code received from Google services", Name="Code", ParameterType="query")]
        public virtual string Code { get; set; }

        ///<summary>
        ///State received with a code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="State received with a code", Name="State", ParameterType="query")]
        public virtual string State { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessToken", ParameterType="query")]
        public virtual string AccessToken { get; set; }

        ///<summary>
        ///Attach any data to the request
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Attach any data to the request", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }
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

        public virtual string Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string ValidFrom { get; set; }
        public virtual string ValidUntil { get; set; }
        public virtual string Type { get; set; }
        public virtual string TargetType { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Description { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string Cluster { get; set; }
        public virtual List<DiscountIndividualLine> IndividualDiscounts { get; set; }
        public virtual List<DiscountCategory> CategoryDiscounts { get; set; }
        public virtual DiscountAll AllDiscount { get; set; }
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
        public virtual string CustomerId { get; set; }

        ///<summary>
        ///Setup intent ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Setup intent ID.", Name="SetupId", ParameterType="body")]
        public virtual string SetupId { get; set; }

        ///<summary>
        ///Client secret got from creating setup intent.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Client secret got from creating setup intent.", Name="SetupClientSecret", ParameterType="body")]
        public virtual string SetupClientSecret { get; set; }

        ///<summary>
        ///Should this payment method be default.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should this payment method be default.", Name="AsDefault", ParameterType="body")]
        public virtual bool AsDefault { get; set; }

        ///<summary>
        ///Should current payment methods be detached.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should current payment methods be detached.", Name="DetachOthers", ParameterType="body")]
        public virtual bool DetachOthers { get; set; }
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

        public virtual string UserId { get; set; }
        public virtual string UserAuthId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string SessionId { get; set; }
        public virtual string ReferrerUrl { get; set; }
        public virtual string BearerToken { get; set; }
        public virtual string Email { get; set; }
        public virtual List<Role> Roles { get; set; }
        public virtual List<string> Permissions { get; set; }
        public virtual string Company { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual string BirthDateRaw { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
        public virtual string Culture { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Language { get; set; }
        public virtual string ProfileUrl { get; set; }
        public virtual long Tag { get; set; }
        public virtual string AuthProvider { get; set; }
        public virtual string MailAddress { get; set; }
        public virtual string Nickname { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime LastModified { get; set; }
        public virtual string Status { get; set; }
        public virtual List<IAuthTokens> AuthTokens { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
    }

    public class Base64FileUpload
    {
        public virtual string Data { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string FileName { get; set; }
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
        public virtual Guid Id { get; set; }
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
        public virtual string Id { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", IsRequired=true, Name="CustomerId", ParameterType="path")]
        public virtual string CustomerId { get; set; }

        ///<summary>
        ///Should cancel instantly. Overrides payment settings
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should cancel instantly. Overrides payment settings", Name="CancelInstantly", ParameterType="query")]
        public virtual bool? CancelInstantly { get; set; }

        ///<summary>
        ///Should refund on cancel instantly. Overrides payment settings
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should refund on cancel instantly. Overrides payment settings", Name="RefundOnCancelInstantly", ParameterType="query")]
        public virtual bool? RefundOnCancelInstantly { get; set; }
    }

    public class CancelSubscriptionResponse
        : ResponseBase<PaymentSubscription>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{collectionName}/responsibility", "POST")]
    [Route("/{version}/db/{collectionName}/responsibility", "POST")]
    [Api(Description="Database services")]
    [DataContract]
    public class ChangeResponsibilityRequest
        : CodeMashDbRequestBase, IReturn<ChangeResponsibilityResponse>
    {
        ///<summary>
        ///Id of a record to change responsibility for
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of a record to change responsibility for", IsRequired=true, Name="Id", ParameterType="body")]
        public virtual string Id { get; set; }

        ///<summary>
        ///New responsible user for this record
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="New responsible user for this record", IsRequired=true, Name="NewResponsibleUserId", ParameterType="body")]
        public virtual Guid NewResponsibleUserId { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", IsRequired=true, Name="CustomerId", ParameterType="path")]
        public virtual string CustomerId { get; set; }

        ///<summary>
        ///Subscription plan ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Subscription plan ID.", IsRequired=true, Name="PlanId", ParameterType="body")]
        public virtual Guid NewPlanId { get; set; }

        ///<summary>
        ///Payment method's ID. If not provided will use default.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's ID. If not provided will use default.", Name="PaymentMethodId", ParameterType="body")]
        public virtual string PaymentMethodId { get; set; }

        ///<summary>
        ///Discount coupon ID if supported.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Discount coupon ID if supported.", Name="Coupon", ParameterType="body")]
        public virtual string Coupon { get; set; }
    }

    public class ChangeSubscriptionResponse
        : ResponseBase<PaymentSubscription>
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
        public virtual string PaymentId { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Payment account ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Payment account ID.", IsRequired=true, Name="AccountId", ParameterType="query")]
        public virtual Guid AccountId { get; set; }

        ///<summary>
        ///Transaction ID which was used with this order and client secret.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Transaction ID which was used with this order and client secret.", IsRequired=true, Name="TransactionId", ParameterType="query")]
        public virtual string TransactionId { get; set; }

        ///<summary>
        ///ClientSecret got from creating stripe transaction.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ClientSecret got from creating stripe transaction.", IsRequired=true, Name="ClientSecret", ParameterType="query")]
        public virtual string ClientSecret { get; set; }
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
        ///A query that specifies which records to count
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies which records to count", Name="Filter", ParameterType="query")]
        public virtual string Filter { get; set; }

        ///<summary>
        ///The maximum number of records to count
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="The maximum number of records to count", Format="int32", Name="Limit", ParameterType="query")]
        public virtual int? Limit { get; set; }

        ///<summary>
        ///The number of records to skip before counting
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="The number of records to skip before counting", Format="int32", Name="Skip", ParameterType="query")]
        public virtual int? Skip { get; set; }
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
        public virtual Guid AccountId { get; set; }

        ///<summary>
        ///User's ID to whom to attach this customer.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User's ID to whom to attach this customer.", Name="UserId", ParameterType="body")]
        public virtual Guid UserId { get; set; }

        ///<summary>
        ///Customer's phone. Overrides user's phone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's phone. Overrides user's phone.", Name="Phone", ParameterType="body")]
        public virtual string Phone { get; set; }

        ///<summary>
        ///Customer's full name. Overrides user's name.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's full name. Overrides user's name.", Name="Name", ParameterType="body")]
        public virtual string Name { get; set; }

        ///<summary>
        ///Customer's description.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's description.", Name="Description", ParameterType="body")]
        public virtual string Description { get; set; }

        ///<summary>
        ///Customer's email. Overrides user's email.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's email. Overrides user's email.", Name="Email", ParameterType="body")]
        public virtual string Email { get; set; }

        ///<summary>
        ///Customer's city. Overrides user's city.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's city. Overrides user's city.", Name="City", ParameterType="body")]
        public virtual string City { get; set; }

        ///<summary>
        ///Customer's country. Overrides user's country.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's country. Overrides user's country.", Name="CountryCode", ParameterType="body")]
        public virtual string CountryCode { get; set; }

        ///<summary>
        ///Customer's address 1. Overrides user's address 1.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's address 1. Overrides user's address 1.", Name="Address1", ParameterType="body")]
        public virtual string Address { get; set; }

        ///<summary>
        ///Customer's address 2. Overrides user's address 2.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's address 2. Overrides user's address 2.", Name="Address2", ParameterType="body")]
        public virtual string Address2 { get; set; }

        ///<summary>
        ///Customer's postal code. Overrides user's postal code.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's postal code. Overrides user's postal code.", Name="PostalCode", ParameterType="body")]
        public virtual string PostalCode { get; set; }

        ///<summary>
        ///Customer's state. Overrides user's area.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's state. Overrides user's area.", Name="Area", ParameterType="body")]
        public virtual string Area { get; set; }

        ///<summary>
        ///Additional key, value data.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Additional key, value data.", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Customer ID to whom belongs this payment method.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer ID to whom belongs this payment method.", IsRequired=true, Name="CustomerId", ParameterType="body")]
        public virtual string CustomerId { get; set; }

        ///<summary>
        ///Should try to charge default card
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should try to charge default card", Name="ChargeCard", ParameterType="body")]
        public virtual bool ChargeCard { get; set; }
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
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///Device operating system
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device operating system", Name="OperatingSystem", ParameterType="body")]
        public virtual string OperatingSystem { get; set; }

        ///<summary>
        ///Device brand
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device brand", Name="Brand", ParameterType="body")]
        public virtual string Brand { get; set; }

        ///<summary>
        ///Device name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device name", Name="DeviceName", ParameterType="body")]
        public virtual string DeviceName { get; set; }

        ///<summary>
        ///Device timezone, expects to get a TZ database name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device timezone, expects to get a TZ database name", Name="TimeZone", ParameterType="body")]
        public virtual string TimeZone { get; set; }

        ///<summary>
        ///Device language, expects to get a 2 letter identifier
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device language, expects to get a 2 letter identifier", Name="Language", ParameterType="body")]
        public virtual string Language { get; set; }

        ///<summary>
        ///Device locale
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device locale", Name="Locale", ParameterType="body")]
        public virtual string Locale { get; set; }

        ///<summary>
        ///Meta information that comes from device.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Meta information that comes from device.", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }
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
        public virtual string Type { get; set; }

        ///<summary>
        ///Unique discount code.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Unique discount code.", Name="Code", ParameterType="body")]
        public virtual string Code { get; set; }

        ///<summary>
        ///Display name of the discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Display name of the discount.", Name="DisplayName", ParameterType="body")]
        public virtual string DisplayName { get; set; }

        ///<summary>
        ///Start date of being active.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="Start date of being active.", Name="ValidFrom", ParameterType="body")]
        public virtual long? ValidFrom { get; set; }

        ///<summary>
        ///End date of being active.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="End date of being active.", Name="ValidUntil", ParameterType="body")]
        public virtual long? ValidUntil { get; set; }

        ///<summary>
        ///Restriction type of discount. Can be Fixed, Percentage, Price or Quantity.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Restriction type of discount. Can be Fixed, Percentage, Price or Quantity.", Name="RestrictionType", ParameterType="body")]
        public virtual string RestrictionType { get; set; }

        ///<summary>
        ///Discount amount for Fixed or Percentage restriction types.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="double", Description="Discount amount for Fixed or Percentage restriction types.", Name="Amount", ParameterType="body")]
        public virtual double? Amount { get; set; }

        ///<summary>
        ///Discount amounts for Price or Quantity restriction types.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Discount amounts for Price or Quantity restriction types.", Name="Boundaries", ParameterType="body")]
        public virtual List<PaymentDiscountBoundary> Boundaries { get; set; }

        ///<summary>
        ///Target type for specific records. Can be All, Category, Individual.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Target type for specific records. Can be All, Category, Individual.", Name="TargetType", ParameterType="body")]
        public virtual string TargetType { get; set; }

        ///<summary>
        ///Records for Individual target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Records for Individual target type.", Name="Records", ParameterType="body")]
        public virtual List<string> Records { get; set; }

        ///<summary>
        ///Collection field for Category target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Collection field for Category target type.", Name="CategoryField", ParameterType="body")]
        public virtual string CategoryField { get; set; }

        ///<summary>
        ///Values for Category target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Values for Category target type.", Name="CategoryValues", ParameterType="body")]
        public virtual List<string> CategoryValues { get; set; }

        ///<summary>
        ///Limitations for discounts to be used only with certain payment accounts.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Limitations for discounts to be used only with certain payment accounts.", Name="PaymentAccounts", ParameterType="body")]
        public virtual List<string> PaymentAccounts { get; set; }

        ///<summary>
        ///Users who can redeem this discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Users who can redeem this discount.", Name="Users", ParameterType="body")]
        public virtual List<string> Users { get; set; }

        ///<summary>
        ///Emails for potential users who can redeem this discount. When user registers with one of the provided emails, he will be allowed to use this discount. Doesn't work with existing users.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Emails for potential users who can redeem this discount. When user registers with one of the provided emails, he will be allowed to use this discount. Doesn't work with existing users.", Name="Users", ParameterType="body")]
        public virtual List<string> Emails { get; set; }

        ///<summary>
        ///Amount of times that the same user can redeem. Default - 1.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of times that the same user can redeem. Default - 1.", Format="int32", Name="UserCanRedeem", ParameterType="body")]
        public virtual int? UserCanRedeem { get; set; }

        ///<summary>
        ///Amount of times in total this discount can be redeemed.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of times in total this discount can be redeemed.", Format="int32", Name="TotalCanRedeem", ParameterType="body")]
        public virtual int? TotalCanRedeem { get; set; }

        ///<summary>
        ///Should the discount be enabled. Default - true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should the discount be enabled. Default - true.", Name="Enabled", ParameterType="body")]
        public virtual bool Enabled { get; set; }

        ///<summary>
        ///Should the discount be combined with other discounts. Default - true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should the discount be combined with other discounts. Default - true.", Name="CombineDiscounts", ParameterType="body")]
        public virtual bool CombineDiscounts { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Id of the payment method.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of the payment method.", Name="PaymentMethodId", ParameterType="body")]
        public virtual string PaymentMethodId { get; set; }

        ///<summary>
        ///Available values: 'card' or 'bank'. Used if payment method not selected
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Available values: 'card' or 'bank'. Used if payment method not selected", Name="PreferredPaymentMethod", ParameterType="body")]
        public virtual string PreferredPaymentMethod { get; set; }
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
        public virtual string Message { get; set; }

        ///<summary>
        ///Custom items providing information.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Custom items providing information.", Name="Items", ParameterType="body")]
        public virtual Dictionary<string, string> Items { get; set; }

        ///<summary>
        ///Severity level of the log.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Severity level of the log.", Name="Level", ParameterType="body")]
        public virtual string Level { get; set; }
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
        public virtual Guid AccountId { get; set; }

        ///<summary>
        ///Order schema ID. Must match one of your order schemas including user zone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Order schema ID. Must match one of your order schemas including user zone.", IsRequired=true, Name="OrderSchemaId", ParameterType="body")]
        public virtual Guid OrderSchemaId { get; set; }

        ///<summary>
        ///User ID. Requires administrator permission.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User ID. Requires administrator permission.", Name="UserId", ParameterType="body")]
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///Order lines.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Order lines.", IsRequired=true, Name="Lines", ParameterType="body")]
        public virtual List<OrderLineInput> Lines { get; set; }

        ///<summary>
        ///Coupon discounts.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Coupon discounts.", Name="Coupons", ParameterType="body")]
        public virtual List<string> Coupons { get; set; }

        ///<summary>
        ///If true, will consider a buyer as a guest user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, will consider a buyer as a guest user.", Name="AsGuest", ParameterType="body")]
        public virtual bool AsGuest { get; set; }

        ///<summary>
        ///Customer details. Should be provided if this is a guest. If this is a user, then this will override user details.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Object", Description="Customer details. Should be provided if this is a guest. If this is a user, then this will override user details.", Name="BuyerDetails", ParameterType="body")]
        public virtual OrderCustomerInput CustomerDetails { get; set; }

        ///<summary>
        ///Consider this as a test order
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Consider this as a test order", Name="IsTest", ParameterType="body")]
        public virtual bool IsTest { get; set; }

        ///<summary>
        ///Additional information for order
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Object", Description="Additional information for order", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }
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
        public virtual string Email { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///To choose which mode to use from payment settings.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="To choose which mode to use from payment settings.", IsRequired=true, Name="Mode", ParameterType="body")]
        public virtual string Mode { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Payment method ID to use.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method ID to use.", IsRequired=true, Name="PaymentMethodId", ParameterType="body")]
        public virtual string PaymentMethodId { get; set; }

        ///<summary>
        ///Customer ID to whom belongs this payment method.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer ID to whom belongs this payment method.", IsRequired=true, Name="CustomerId", ParameterType="body")]
        public virtual string CustomerId { get; set; }
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
        public virtual string CustomerId { get; set; }

        ///<summary>
        ///Subscription plan ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Subscription plan ID.", IsRequired=true, Name="PlanId", ParameterType="body")]
        public virtual Guid PlanId { get; set; }

        ///<summary>
        ///Payment method's ID. If not provided will use default.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's ID. If not provided will use default.", Name="PaymentMethodId", ParameterType="body")]
        public virtual string PaymentMethodId { get; set; }

        ///<summary>
        ///Discount coupon ID if supported.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Discount coupon ID if supported.", Name="Coupon", ParameterType="body")]
        public virtual string Coupon { get; set; }
    }

    public class CreateSubscriptionResponse
        : ResponseBase<PaymentSubscription>
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
        public virtual string UserName { get; set; }

        ///<summary>
        ///User's login password.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's login password.", IsRequired=true, Name="Password", ParameterType="body")]
        public virtual string Password { get; set; }
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
            Subscriptions = new List<PaymentSubscription>{};
            Meta = new Dictionary<string, string>{};
        }

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string Provider { get; set; }
        public virtual string ProviderId { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Email { get; set; }
        public virtual string City { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Area { get; set; }
        public virtual string UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string ProjectId { get; set; }
        public virtual string PaymentAccountId { get; set; }
        public virtual List<PaymentMethod> PaymentMethods { get; set; }
        public virtual List<PaymentSubscription> Subscriptions { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
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
        public virtual string Token { get; set; }

        ///<summary>
        ///Password for confirmation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Password for confirmation", IsRequired=true, Name="Password", ParameterType="body")]
        public virtual string Password { get; set; }
    }

    public class DectaOrder
    {
        public virtual string Id { get; set; }
        public virtual bool Paid { get; set; }
        public virtual string FullPageCheckout { get; set; }
        public virtual string IframeCheckout { get; set; }
        public virtual string DirectPost { get; set; }
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
        public virtual string Id { get; set; }
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
        public virtual Guid Id { get; set; }

        ///<summary>
        ///Keys to be deleted
        ///</summary>
        [DataMember]
        [ApiMember(DataType="List", Description="Keys to be deleted", Name="Keys", ParameterType="body")]
        public virtual List<string> Keys { get; set; }

        ///<summary>
        ///Delete all keys
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Delete all keys", Name="DeleteAllKeys", ParameterType="form")]
        public virtual bool DeleteAllKeys { get; set; }
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
        public virtual string Id { get; set; }
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
        public virtual Guid Id { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public virtual string DeviceKey { get; set; }
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
        public virtual string Id { get; set; }
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
        public virtual string Id { get; set; }
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
        public virtual string FileId { get; set; }
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
        ///Specifies deletion criteria using query operators. To delete all documents in a collection, pass in an empty document "{}"
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Specifies deletion criteria using query operators. To delete all documents in a collection, pass in an empty document \"{}\"", Name="Filter", ParameterType="query")]
        public virtual string Filter { get; set; }

        ///<summary>
        ///If true, does not activate triggers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, does not activate triggers", Name="IgnoreTriggers", ParameterType="query")]
        public virtual bool IgnoreTriggers { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public virtual string DeviceKey { get; set; }
    }

    public class DeleteNotificationResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}", "DELETE")]
    [Route("/{version}/db/{collectionName}", "DELETE")]
    [Route("/{version}/db/{collectionName}/{id}", "DELETE")]
    [Api(Description="Database services")]
    [DataContract]
    public class DeleteOneRequest
        : CodeMashDbRequestBase, IReturn<DeleteOneResponse>
    {
        ///<summary>
        ///ID of a record to delete. Required if Filter is empty.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of a record to delete. Required if Filter is empty.", Name="Id", ParameterType="path")]
        public virtual string Id { get; set; }

        ///<summary>
        ///Specifies deletion criteria using query operators. Specify an empty document "{}" to delete the first document returned in the collection. Required if Id is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Specifies deletion criteria using query operators. Specify an empty document \"{}\" to delete the first document returned in the collection. Required if Id is not set.", Name="Filter", ParameterType="query")]
        public virtual string Filter { get; set; }

        ///<summary>
        ///If true, does not activate triggers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, does not activate triggers", Name="IgnoreTriggers", ParameterType="query")]
        public virtual bool IgnoreTriggers { get; set; }
    }

    public class DeleteOneResponse
        : ResponseBase<DeleteResult>
    {
    }

    [DataContract]
    public class DeleteResult
    {
        [DataMember(Name="deletedCount")]
        public virtual long DeletedCount { get; set; }

        [DataMember(Name="isAcknowledged")]
        public virtual bool IsAcknowledged { get; set; }
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
        public virtual Guid Id { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", Name="CustomerId", ParameterType="query")]
        public virtual string CustomerId { get; set; }
    }

    public class Device
    {
        public Device()
        {
            Meta = new Dictionary<string, string>{};
        }

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual PushNotificationToken Token { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UserId { get; set; }
        public virtual string OperatingSystem { get; set; }
        public virtual string Brand { get; set; }
        public virtual string DeviceName { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string Language { get; set; }
        public virtual string Locale { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual long TotalNotifications { get; set; }
        public virtual string DeviceKey { get; set; }
        public virtual string AccountId { get; set; }
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

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string ModifiedOn { get; set; }
        public virtual string Type { get; set; }
        public virtual string Code { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string ValidFrom { get; set; }
        public virtual string ValidUntil { get; set; }
        public virtual string SchemaId { get; set; }
        public virtual string Cluster { get; set; }
        public virtual string RestrictionType { get; set; }
        public virtual double? Amount { get; set; }
        public virtual List<PaymentDiscountBoundary> Boundaries { get; set; }
        public virtual string TargetType { get; set; }
        public virtual List<string> Records { get; set; }
        public virtual string CategoryField { get; set; }
        public virtual List<string> CategoryValues { get; set; }
        public virtual List<string> PaymentAccounts { get; set; }
        public virtual List<string> Users { get; set; }
        public virtual List<string> Emails { get; set; }
        public virtual int? UserCanRedeem { get; set; }
        public virtual int? TotalCanRedeem { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual bool CombineDiscounts { get; set; }
    }

    public class DiscountAll
    {
        public DiscountAll()
        {
            Lines = new List<DiscountLine>{};
        }

        public virtual List<DiscountLine> Lines { get; set; }
        public virtual decimal Discount { get; set; }
    }

    public class DiscountCategory
    {
        public DiscountCategory()
        {
            Lines = new List<DiscountLine>{};
        }

        public virtual string Category { get; set; }
        public virtual List<DiscountLine> Lines { get; set; }
        public virtual decimal Discount { get; set; }
    }

    public class DiscountIndividualLine
    {
        public virtual string RecordId { get; set; }
        public virtual string Variation { get; set; }
        public virtual decimal Discount { get; set; }
    }

    public class DiscountLine
    {
        public virtual string RecordId { get; set; }
        public virtual string Variation { get; set; }
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
        [ApiMember(DataType="string", Description="The field for which to return distinct values", IsRequired=true, Name="Field", ParameterType="query")]
        public virtual string Field { get; set; }

        ///<summary>
        ///A query that specifies the documents from which to retrieve the distinct values
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies the documents from which to retrieve the distinct values", Name="Filter", ParameterType="query")]
        public virtual string Filter { get; set; }
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
        public virtual string FileId { get; set; }

        ///<summary>
        ///Optimization code of optimized image
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Optimization code of optimized image", Name="Optimization", ParameterType="query")]
        public virtual string Optimization { get; set; }
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
        public virtual Guid Id { get; set; }

        ///<summary>
        ///Parameters of to pass into function
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Parameters of to pass into function", Name="Template", ParameterType="body")]
        public virtual string Data { get; set; }

        ///<summary>
        ///Additional parameters for specific functions
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Additional parameters for specific functions", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }

        ///<summary>
        ///Tokens to inject into queries
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Tokens to inject into queries", Name="Tokens", ParameterType="body")]
        public virtual Dictionary<string, string> Tokens { get; set; }

        ///<summary>
        ///Version or Alias of a function
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Version or Alias of a function", Name="Qualifier", ParameterType="body")]
        public virtual string Qualifier { get; set; }
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
        public virtual string Mode { get; set; }

        ///<summary>
        ///Code received from Facebook services
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Code received from Facebook services", Name="Code", ParameterType="query")]
        public virtual string Code { get; set; }

        ///<summary>
        ///State received with a code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="State received with a code", Name="State", ParameterType="query")]
        public virtual string State { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessToken", ParameterType="query")]
        public virtual string AccessToken { get; set; }
    }

    public class FacebookAuthenticationResponse
        : ResponseBase<AuthResponse>
    {
    }

    public class File
    {
        public virtual string Id { get; set; }
        public virtual string Description { get; set; }
        public virtual string ModifiedOn { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string UniqueName { get; set; }
        public virtual int Enumerator { get; set; }
        public virtual string OriginalName { get; set; }
        public virtual string Directory { get; set; }
        public virtual string ContentType { get; set; }
        public virtual long Size { get; set; }
        public virtual bool IsPublic { get; set; }
        public virtual bool IsParentPublic { get; set; }
        public virtual string PublicUrl { get; set; }
    }

    public class FileDetails
    {
        public virtual string Id { get; set; }
        public virtual string Directory { get; set; }
        public virtual string OriginalFileName { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FilePath { get; set; }
        public virtual string ContentType { get; set; }
        public virtual long ContentLength { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/{Id}", "GET POST")]
    [Route("/db/{CollectionName}/findOne", "GET POST")]
    [Route("/{version}/db/{CollectionName}/{Id}", "GET POST")]
    [Route("/{version}/db/{CollectionName}/findOne", "GET POST")]
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
        ///ID of a record. Required if filter is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of a record. Required if filter is not set.", Name="Id", ParameterType="path")]
        public virtual string Id { get; set; }

        ///<summary>
        ///The selection criteria for the find one operation. Required if Id is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The selection criteria for the find one operation. Required if Id is not set.", Name="Filter", ParameterType="query")]
        public virtual string Filter { get; set; }

        ///<summary>
        ///A query that specifies what fields in record to return
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="A query that specifies what fields in record to return", Name="Projection", ParameterType="query")]
        public virtual string Projection { get; set; }

        ///<summary>
        ///By default schema is excluded in response. To get schema together with the record set this property to true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="By default schema is excluded in response. To get schema together with the record set this property to true.", Name="ExcludeSchema", ParameterType="query")]
        public virtual bool IncludeSchema { get; set; }

        ///<summary>
        ///Prevent setting culture code from headers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Prevent setting culture code from headers", Name="ExcludeCulture", ParameterType="query")]
        public virtual bool ExcludeCulture { get; set; }

        ///<summary>
        ///Includes names of referenced users
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes names of referenced users", Name="IncludeUserNames", ParameterType="query")]
        public virtual bool IncludeUserNames { get; set; }

        ///<summary>
        ///Includes names of referenced roles
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes names of referenced roles", Name="IncludeRoleNames", ParameterType="query")]
        public virtual bool IncludeRoleNames { get; set; }

        ///<summary>
        ///Includes names of referenced records
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes names of referenced records", Name="IncludeCollectionNames", ParameterType="query")]
        public virtual bool IncludeCollectionNames { get; set; }

        ///<summary>
        ///Includes names of referenced terms
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes names of referenced terms", Name="IncludeTermNames", ParameterType="query")]
        public virtual bool IncludeTermNames { get; set; }

        ///<summary>
        ///Includes data of referenced records
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Includes data of referenced records", Name="ReferencedFields", ParameterType="query")]
        public virtual List<ReferencingField> ReferencedFields { get; set; }

        ///<summary>
        ///If true, then references are injected before your list queries are applied
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, then references are injected before your list queries are applied", Name="AddReferencesFirst", ParameterType="query")]
        public virtual bool AddReferencesFirst { get; set; }
    }

    public class FindOneResponse
        : ResponseBase<string>
    {
        public virtual Schema Schema { get; set; }
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
        ///Includes schema in response
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes schema in response", Name="IncludeSchema", ParameterType="query")]
        public virtual bool IncludeSchema { get; set; }

        ///<summary>
        ///Includes names of referenced users
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes names of referenced users", Name="IncludeUserNames", ParameterType="query")]
        public virtual bool IncludeUserNames { get; set; }

        ///<summary>
        ///Includes names of referenced roles
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes names of referenced roles", Name="IncludeRoleNames", ParameterType="query")]
        public virtual bool IncludeRoleNames { get; set; }

        ///<summary>
        ///Includes names of referenced records
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes names of referenced records", Name="IncludeCollectionNames", ParameterType="query")]
        public virtual bool IncludeCollectionNames { get; set; }

        ///<summary>
        ///Includes names of referenced terms
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Includes names of referenced terms", Name="IncludeTermNames", ParameterType="query")]
        public virtual bool IncludeTermNames { get; set; }

        ///<summary>
        ///Prevent setting culture code from headers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Prevent setting culture code from headers", Name="ExcludeCulture", ParameterType="query")]
        public virtual bool ExcludeCulture { get; set; }

        ///<summary>
        ///Includes data of referenced records
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Includes data of referenced records", Name="ReferencedFields", ParameterType="query")]
        public virtual List<ReferencingField> ReferencedFields { get; set; }

        ///<summary>
        ///If true, then references are injected before your list queries are applied
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, then references are injected before your list queries are applied", Name="AddReferencesFirst", ParameterType="query")]
        public virtual bool AddReferencesFirst { get; set; }
    }

    public class FindResponse
        : ResponseBase<string>
    {
        public virtual long TotalCount { get; set; }
        public virtual Schema Schema { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/taxonomies/{CollectionName}/terms/{id}/children", "GET POST")]
    [Route("/{version}/db/taxonomies/{CollectionName}/terms/{id}/children", "GET POST")]
    [Route("/db/taxonomies/{CollectionName}/terms/children", "GET POST")]
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
        public virtual string Id { get; set; }

        ///<summary>
        ///The selection criteria for the parent terms. Required if Id is not set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The selection criteria for the parent terms. Required if Id is not set.", Name="ParentFilter", ParameterType="query")]
        public virtual string ParentFilter { get; set; }

        ///<summary>
        ///Prevent setting culture code from headers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Prevent setting culture code from headers", Name="ExcludeCulture", ParameterType="query")]
        public virtual bool ExcludeCulture { get; set; }
    }

    public class FindTermsChildrenResponse
        : ResponseBase<List<Term>>
    {
        public virtual long TotalCount { get; set; }
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
        public virtual bool IncludeTaxonomy { get; set; }

        ///<summary>
        ///Prevent setting culture code from headers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Prevent setting culture code from headers", Name="ExcludeCulture", ParameterType="query")]
        public virtual bool ExcludeCulture { get; set; }
    }

    public class FindTermsResponse
        : ResponseBase<List<Term>>
    {
        public virtual long TotalCount { get; set; }
        public virtual Taxonomy Taxonomy { get; set; }
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
        public virtual List<string> Codes { get; set; }

        ///<summary>
        ///Order schema ID. Must match one of your order schemas including user zone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Order schema ID. Must match one of your order schemas including user zone.", Name="OrderSchemaId", ParameterType="query")]
        public virtual Guid OrderSchemaId { get; set; }

        ///<summary>
        ///User ID. Requires administrator permission.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User ID. Requires administrator permission.", Name="UserId", ParameterType="body")]
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///If true, will consider a buyer as a guest user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, will consider a buyer as a guest user.", Name="AsGuest", ParameterType="body")]
        public virtual bool AsGuest { get; set; }

        ///<summary>
        ///Order lines. Check which records are applicable for discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Order lines. Check which records are applicable for discount.", IsRequired=true, Name="Lines", ParameterType="query")]
        public virtual List<OrderLineInput> Lines { get; set; }
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
        public virtual Guid OrderSchemaId { get; set; }

        ///<summary>
        ///User ID. Requires administrator permission.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User ID. Requires administrator permission.", Name="UserId", ParameterType="body")]
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///If true, will consider a buyer as a guest user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, will consider a buyer as a guest user.", Name="AsGuest", ParameterType="body")]
        public virtual bool AsGuest { get; set; }

        ///<summary>
        ///Order lines. Check which records are applicable for discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Order lines. Check which records are applicable for discount.", IsRequired=true, Name="Lines", ParameterType="query")]
        public virtual List<OrderLineInput> Lines { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///If true, id should be customer's provider Id.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, id should be customer's provider Id.", Name="UseProviderId", ParameterType="query")]
        public virtual bool UseProviderId { get; set; }

        ///<summary>
        ///If true, id should be customer's user Id.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, id should be customer's user Id.", Name="UseUserId", ParameterType="query")]
        public virtual bool UseUserId { get; set; }

        ///<summary>
        ///Required if UseUserId is set to true.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Required if UseUserId is set to true.", Name="PaymentProvider", ParameterType="query")]
        public virtual string PaymentProvider { get; set; }
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
        public virtual Guid? UserId { get; set; }
    }

    public class GetCustomersResponse
        : ResponseBase<List<Customer>>
    {
        public virtual long TotalCount { get; set; }
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
        public virtual string Id { get; set; }
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
        public virtual Guid? UserId { get; set; }
    }

    public class GetDevicesResponse
        : ResponseBase<List<Device>>
    {
        public virtual long TotalCount { get; set; }
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
        public virtual string FileId { get; set; }

        ///<summary>
        ///Optimization code of optimized image
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Optimization code of optimized image", Name="Optimization", ParameterType="query")]
        public virtual string Optimization { get; set; }
    }

    public class GetFileResponse
        : ResponseBase<string>
    {
        public virtual string FileName { get; set; }
        public virtual bool IsImage { get; set; }
        public virtual File File { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public virtual string DeviceKey { get; set; }
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
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///Notifications delivered to specified device.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Notifications delivered to specified device.", Name="DeviceId", ParameterType="body")]
        public virtual Guid? DeviceId { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public virtual string DeviceKey { get; set; }

        ///<summary>
        ///Optional filter to count only matched notifications.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Optional filter to count only matched notifications.", IsRequired=true, Name="Filter", ParameterType="body")]
        public virtual string Filter { get; set; }

        ///<summary>
        ///Optional filter to count only matched notifications.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Optional filter to count only matched notifications.", IsRequired=true, Name="Filter", ParameterType="body")]
        public virtual string GroupBy { get; set; }
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
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///Notifications delivered to specified device.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Notifications delivered to specified device.", Name="DeviceId", ParameterType="body")]
        public virtual Guid? DeviceId { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public virtual string DeviceKey { get; set; }
    }

    public class GetNotificationsResponse
        : ResponseBase<List<PushNotification>>
    {
        public virtual long TotalCount { get; set; }
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
        public virtual Guid Id { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///If true, includes paid transactions to response.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, includes paid transactions to response.", Name="IncludePaidTransactions", ParameterType="query")]
        public virtual bool IncludePaidTransactions { get; set; }
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
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///If true, includes paid transactions to response.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, includes paid transactions to response.", Name="IncludePaidTransactions", ParameterType="query")]
        public virtual bool IncludePaidTransactions { get; set; }

        ///<summary>
        ///API key of your cluster. Can be passed in a header as X-CM-Cluster.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="API key of your cluster. Can be passed in a header as X-CM-Cluster.", IsRequired=true, Name="Cluster", ParameterType="query")]
        public virtual string Cluster { get; set; }
    }

    public class GetOrdersResponse
        : ResponseBase<List<Order>>
    {
        public virtual long TotalCount { get; set; }
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
        public virtual Guid AccountId { get; set; }

        ///<summary>
        ///Can payment method be used without a user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Can payment method be used without a user.", Name="AllowOffline", ParameterType="query")]
        public virtual bool AllowOffline { get; set; }
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
        public virtual bool IncludePermissions { get; set; }

        ///<summary>
        ///Set true if user devices should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user devices should be returned", Name="IncludeDevices", ParameterType="query")]
        public virtual bool IncludeDevices { get; set; }

        ///<summary>
        ///Set true if user unread notifications count should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user unread notifications count should be returned", Name="IncludeNotificationsCount", ParameterType="query")]
        public virtual bool IncludeUnreadNotifications { get; set; }

        ///<summary>
        ///Set true if user meta data should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user meta data should be returned", Name="IncludeMeta", ParameterType="query")]
        public virtual bool IncludeMeta { get; set; }
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
        public virtual Guid ProjectId { get; set; }
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
        public virtual Guid Id { get; set; }

        ///<summary>
        ///Email of user
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email of user", Name="Email", ParameterType="query")]
        public virtual string Email { get; set; }

        ///<summary>
        ///User phone number, only viable if user registered with phone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User phone number, only viable if user registered with phone", Name="Phone", ParameterType="query")]
        public virtual string Phone { get; set; }

        ///<summary>
        ///Provider of user
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Provider of user", Name="Provider", ParameterType="query")]
        public virtual string Provider { get; set; }

        ///<summary>
        ///Set true if permissions should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if permissions should be returned", Name="IncludePermissions", ParameterType="query")]
        public virtual bool IncludePermissions { get; set; }

        ///<summary>
        ///Set true if user devices should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user devices should be returned", Name="IncludeDevices", ParameterType="query")]
        public virtual bool IncludeDevices { get; set; }

        ///<summary>
        ///Set true if user unread notifications count should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user unread notifications count should be returned", Name="IncludeNotificationsCount", ParameterType="query")]
        public virtual bool IncludeUnreadNotifications { get; set; }

        ///<summary>
        ///Set true if user meta data should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user meta data should be returned", Name="IncludeMeta", ParameterType="query")]
        public virtual bool IncludeMeta { get; set; }
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
        public virtual bool IncludePermissions { get; set; }

        ///<summary>
        ///Set true if user devices should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user devices should be returned", Name="IncludeDevices", ParameterType="path")]
        public virtual bool IncludeDevices { get; set; }

        ///<summary>
        ///Set true if user meta data should be returned
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Set true if user meta data should be returned", Name="IncludeMeta", ParameterType="path")]
        public virtual bool IncludeMeta { get; set; }
    }

    public class GetUsersResponse
        : ResponseBase<List<User>>
    {
        public virtual long TotalCount { get; set; }
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
        public virtual string Mode { get; set; }

        ///<summary>
        ///Code received from Google services
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Code received from Google services", Name="Code", ParameterType="query")]
        public virtual string Code { get; set; }

        ///<summary>
        ///State received with a code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="State received with a code", Name="State", ParameterType="query")]
        public virtual string State { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessToken", ParameterType="query")]
        public virtual string AccessToken { get; set; }
    }

    public class GoogleAuthenticationResponse
        : ResponseBase<AuthResponse>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{collectionName}/bulk", "POST")]
    [Route("/{version}/db/{collectionName}/bulk", "POST")]
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
        [ApiMember(DataType="Array", Description="Array of json records to insert", IsRequired=true, Name="Document", ParameterType="body")]
        public virtual List<string> Documents { get; set; }

        ///<summary>
        ///By default records are validated before insert, set to true to prevent validation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="By default records are validated before insert, set to true to prevent validation", Name="BypassDocumentValidation", ParameterType="body")]
        public virtual bool BypassDocumentValidation { get; set; }

        ///<summary>
        ///If true, does not activate triggers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, does not activate triggers", Name="IgnoreTriggers", ParameterType="body")]
        public virtual bool IgnoreTriggers { get; set; }

        ///<summary>
        ///When calling with full permission, can set responsible user ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="When calling with full permission, can set responsible user ID", Name="ResponsibleUserId", ParameterType="body")]
        public virtual Guid? ResponsibleUserId { get; set; }

        ///<summary>
        ///If true, file fields that are passed will expect file ids given from your storage providers.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, file fields that are passed will expect file ids given from your storage providers.", Name="ResolveProviderFiles", ParameterType="body")]
        public virtual bool ResolveProviderFiles { get; set; }
    }

    public class InsertManyResponse
        : ResponseBase<List<String>>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{collectionName}", "POST")]
    [Route("/{version}/db/{collectionName}", "POST")]
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
        public virtual string Document { get; set; }

        ///<summary>
        ///By default records are validated before insert, set to true to prevent validation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="By default records are validated before insert, set to true to prevent validation", Name="BypassDocumentValidation", ParameterType="body")]
        public virtual bool BypassDocumentValidation { get; set; }

        ///<summary>
        ///By default file uploads are done after the record is inserted, set to true in case you need to wait for files to be uploaded
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="By default file uploads are done after the record is inserted, set to true in case you need to wait for files to be uploaded", Name="WaitForFileUpload", ParameterType="body")]
        public virtual bool WaitForFileUpload { get; set; }

        ///<summary>
        ///If true, does not activate triggers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, does not activate triggers", Name="IgnoreTriggers", ParameterType="body")]
        public virtual bool IgnoreTriggers { get; set; }

        ///<summary>
        ///When calling with full permission, can set responsible user ID
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="When calling with full permission, can set responsible user ID", Name="ResponsibleUserId", ParameterType="body")]
        public virtual Guid? ResponsibleUserId { get; set; }

        ///<summary>
        ///If true, file fields that are passed will expect file ids given from your storage providers.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, file fields that are passed will expect file ids given from your storage providers.", Name="ResolveProviderFiles", ParameterType="body")]
        public virtual bool ResolveProviderFiles { get; set; }
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
        public virtual string DisplayName { get; set; }

        ///<summary>
        ///Email address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email address", IsRequired=true, Name="Email", ParameterType="form")]
        public virtual string Email { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="form")]
        public virtual string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="form")]
        public virtual string LastName { get; set; }

        ///<summary>
        ///Role names to be applied
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Role names to be applied", Name="Roles", ParameterType="form")]
        public virtual List<string> Roles { get; set; }

        ///<summary>
        ///Full permission tree
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Full permission tree", Name="RolesTree", ParameterType="body")]
        public virtual List<UserRoleUpdateInput> RolesTree { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="form")]
        public virtual string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="form")]
        public virtual string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="form")]
        public virtual string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="form")]
        public virtual bool? SubscribeToNews { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public virtual string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public virtual string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public virtual string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public virtual string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public virtual string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public virtual string Address2 { get; set; }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", Name="Phone", ParameterType="body")]
        public virtual string Phone { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public virtual string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public virtual string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public virtual string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public virtual string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public virtual string BirthDate { get; set; }

        ///<summary>
        ///User's zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's zone", Name="Zone", ParameterType="body")]
        public virtual string Zone { get; set; }
    }

    public class InviteUserV2Response
        : ResponseBase<User>
    {
    }

    public class KevinAuthorizationLink
    {
        public virtual string AuthorizationLink { get; set; }
        public virtual string State { get; set; }
        public virtual string RequestId { get; set; }
    }

    public class KevinPaymentStatus
    {
        public virtual string GroupStatus { get; set; }
        public virtual string TransactionId { get; set; }
        public virtual string OrderId { get; set; }
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
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///DeviceId - (Either userId or deviceId is required). The same massage can be spread across many devices, so we need to identify in which device the message has been read
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="DeviceId - (Either userId or deviceId is required). The same massage can be spread across many devices, so we need to identify in which device the message has been read", IsRequired=true, Name="DeviceId", ParameterType="body")]
        public virtual Guid? DeviceId { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public virtual string DeviceKey { get; set; }

        ///<summary>
        ///Optional filter to read only matched notifications.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Optional filter to read only matched notifications.", IsRequired=true, Name="Filter", ParameterType="body")]
        public virtual string Filter { get; set; }
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
        public virtual string NotificationId { get; set; }

        ///<summary>
        ///UserId - (Either userId or deviceId is required). The same massage can be spread across many users, so we need to identify which user read the message
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="UserId - (Either userId or deviceId is required). The same massage can be spread across many users, so we need to identify which user read the message", Name="UserId", ParameterType="form")]
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///DeviceId - (Either userId or deviceId is required). The same massage can be spread across many devices, so we need to identify in which device the message has been read
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="DeviceId - (Either userId or deviceId is required). The same massage can be spread across many devices, so we need to identify in which device the message has been read", IsRequired=true, Name="DeviceId", ParameterType="form")]
        public virtual Guid? DeviceId { get; set; }

        ///<summary>
        ///Device key
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device key", Name="DeviceKey", ParameterType="query")]
        public virtual string DeviceKey { get; set; }
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

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string ModifiedOn { get; set; }
        public virtual string PaidOn { get; set; }
        public virtual long Number { get; set; }
        public virtual string NumberPrefix { get; set; }
        public virtual string PaymentStatus { get; set; }
        public virtual string PaymentProvider { get; set; }
        public virtual string Currency { get; set; }
        public virtual bool AsGuest { get; set; }
        public virtual bool IsTest { get; set; }
        public virtual OrderCustomer Customer { get; set; }
        public virtual string Cluster { get; set; }
        public virtual List<OrderLine> Lines { get; set; }
        public virtual List<OrderFile> Files { get; set; }
        public virtual List<OrderTransaction> Transactions { get; set; }
        public virtual List<OrderDiscount> Discounts { get; set; }
        public virtual string UserId { get; set; }
        public virtual string PaymentAccountId { get; set; }
        public virtual decimal Total { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
    }

    public class OrderCustomer
    {
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Company { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Country { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Area { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }
    }

    public class OrderCustomerInput
    {
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Company { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Country { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Area { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Language { get; set; }
    }

    public class OrderDiscount
    {
        public OrderDiscount()
        {
            IndividualDiscounts = new List<DiscountIndividualLine>{};
            CategoryDiscounts = new List<DiscountCategory>{};
        }

        public virtual string Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Type { get; set; }
        public virtual string TargetType { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Description { get; set; }
        public virtual List<DiscountIndividualLine> IndividualDiscounts { get; set; }
        public virtual List<DiscountCategory> CategoryDiscounts { get; set; }
        public virtual DiscountAll AllDiscount { get; set; }
    }

    public class OrderFile
    {
        public virtual string Category { get; set; }
        public virtual string Id { get; set; }
        public virtual string Directory { get; set; }
        public virtual string OriginalFileName { get; set; }
        public virtual string FileName { get; set; }
        public virtual string ContentType { get; set; }
        public virtual long ContentLength { get; set; }
    }

    public class OrderLine
    {
        public OrderLine()
        {
            PriceFields = new List<string>{};
        }

        public virtual string SchemaId { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string RecordId { get; set; }
        public virtual List<string> PriceFields { get; set; }
        public virtual string Variation { get; set; }
        public virtual string RecordData { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int Quantity { get; set; }
    }

    public class OrderLineInput
    {
        public virtual string CollectionName { get; set; }
        public virtual string RecordId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual string Variation { get; set; }
    }

    public class OrderTransaction
    {
        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string PayUntil { get; set; }
        public virtual string PaidOn { get; set; }
        public virtual string CallbackOn { get; set; }
        public virtual string Provider { get; set; }
        public virtual string EventStatus { get; set; }
        public virtual string EventUniqueId { get; set; }
        public virtual string Type { get; set; }
        public virtual string Data { get; set; }
        public virtual string PayerIpCountry { get; set; }
        public virtual string PayerCountry { get; set; }
        public virtual string PayerEmail { get; set; }
        public virtual string PaymentType { get; set; }
        public virtual string EventAccount { get; set; }
        public virtual string PayText { get; set; }
        public virtual string EventCurrency { get; set; }
        public virtual decimal EventAmount { get; set; }
    }

    public class PaymentDiscountBoundary
    {
        public virtual double Boundary { get; set; }
        public virtual double Amount { get; set; }
        public virtual string Type { get; set; }
    }

    public class PaymentMethod
    {
        public PaymentMethod()
        {
            Meta = new Dictionary<string, string>{};
        }

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string Type { get; set; }
        public virtual string Data { get; set; }
        public virtual string Email { get; set; }
        public virtual string Name { get; set; }
        public virtual string Phone { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Area { get; set; }
        public virtual string City { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Last4 { get; set; }
        public virtual int ExpMonth { get; set; }
        public virtual int ExpYear { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
    }

    public class PaymentMethodSetup
    {
        public virtual string SetupId { get; set; }
        public virtual string SetupClientSecret { get; set; }
        public virtual string Status { get; set; }
    }

    public class PaymentSubscription
    {
        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string CurrentPeriodStart { get; set; }
        public virtual string CurrentPeriodEnd { get; set; }
        public virtual string CanceledAt { get; set; }
        public virtual bool CancelAtPeriodEnd { get; set; }
        public virtual string TrialStart { get; set; }
        public virtual string TrialEnd { get; set; }
        public virtual string Status { get; set; }
        public virtual string PlanId { get; set; }
        public virtual string AppliedCoupon { get; set; }
        public virtual string PaymentMethodId { get; set; }
        public virtual string CustomerId { get; set; }
    }

    public class Policy
    {
        public Policy()
        {
            Permissions = new List<string>{};
        }

        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual List<string> Permissions { get; set; }
    }

    public class Project
    {
        public Project()
        {
            Tokens = new List<Token>{};
            Languages = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual List<Token> Tokens { get; set; }
        public virtual List<string> Languages { get; set; }
        public virtual string DefaultLanguage { get; set; }
        public virtual string DefaultTimeZone { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string SlugifiedName { get; set; }
        public virtual string LogoUrl { get; set; }
    }

    public class PushNotification
    {
        public PushNotification()
        {
            Meta = new Dictionary<string, string>{};
        }

        public virtual string Id { get; set; }
        public virtual string ReceivedOn { get; set; }
        public virtual string Status { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual string Data { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual bool IsRead { get; set; }
        public virtual string UserId { get; set; }
        public virtual string DeviceId { get; set; }
        public virtual string SenderId { get; set; }
    }

    public class PushNotificationTemplate
    {
        public PushNotificationTemplate()
        {
            Meta = new Dictionary<string, string>{};
            Buttons = new List<PushNotificationTemplateButtons>{};
        }

        public virtual string Id { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual string AccountName { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual string Code { get; set; }
        public virtual string Priority { get; set; }
        public virtual string Data { get; set; }
        public virtual int? Ttl { get; set; }
        public virtual string Url { get; set; }
        public virtual string CollapseId { get; set; }
        public virtual FileDetails Image { get; set; }
        public virtual string AccountId { get; set; }
        public virtual Guid? FileAccountId { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual List<PushNotificationTemplateButtons> Buttons { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual int? IosBadge { get; set; }
        public virtual string IosCategory { get; set; }
        public virtual bool IosContentAvailable { get; set; }
        public virtual string IosSound { get; set; }
        public virtual string IosAppBundleId { get; set; }
        public virtual string IosGroupId { get; set; }
        public virtual string IosPushType { get; set; }
        public virtual string IosLaunchImage { get; set; }
        public virtual string IosAnalyticsLabel { get; set; }
        public virtual string AndroidGroup { get; set; }
        public virtual string AndroidGroupMessage { get; set; }
        public virtual string RestrictedPackageName { get; set; }
        public virtual string AndroidChannelId { get; set; }
        public virtual string AndroidSound { get; set; }
        public virtual string AndroidVisibility { get; set; }
        public virtual bool AndroidDefaultVibration { get; set; }
        public virtual string AndroidVibrateTimings { get; set; }
        public virtual bool AndroidDefaultLight { get; set; }
        public virtual string AndroidAccentColor { get; set; }
        public virtual string AndroidLedColor { get; set; }
        public virtual string AndroidLightOnDuration { get; set; }
        public virtual string AndroidLightOffDuration { get; set; }
        public virtual bool AndroidSticky { get; set; }
        public virtual string AndroidSmallIcon { get; set; }
        public virtual string AndroidLargeIcon { get; set; }
        public virtual AndroidBackgroundLayout AndroidBackground { get; set; }
        public virtual string AndroidAnalyticsLabel { get; set; }
    }

    public class PushNotificationTemplateButtons
    {
        public virtual string Id { get; set; }
        public virtual string Text { get; set; }
        public virtual string Icon { get; set; }
    }

    public class PushNotificationToken
    {
        public virtual string Provider { get; set; }
        public virtual string Token { get; set; }
        public virtual string AccountId { get; set; }
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
        public virtual string RequestId { get; set; }

        ///<summary>
        ///Authorization code received from authenticating.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Authorization code received from authenticating.", IsRequired=true, Name="AuthorizationCode", ParameterType="body")]
        public virtual string AuthorizationCode { get; set; }
    }

    public class ReferencingField
    {
        public virtual string Name { get; set; }
        public virtual int PageSize { get; set; }
        public virtual int PageNumber { get; set; }
        public virtual string Sort { get; set; }
        public virtual string Projection { get; set; }
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
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///Device Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device Id", Name="DeviceId", ParameterType="form")]
        public virtual Guid? DeviceId { get; set; }

        ///<summary>
        ///Token
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Token", IsRequired=true, Name="Token", ParameterType="form")]
        public virtual string Token { get; set; }

        ///<summary>
        ///TimeZone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="TimeZone", Name="TimeZone", ParameterType="form")]
        public virtual string TimeZone { get; set; }

        ///<summary>
        ///Meta information that comes from device.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Meta information that comes from device.", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }
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
        public virtual string Provider { get; set; }

        ///<summary>
        ///Push account ID. If you have more than 1 account for provider pass this instead.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Push account ID. If you have more than 1 account for provider pass this instead.", Name="AccountId", ParameterType="body")]
        public virtual Guid? AccountId { get; set; }

        ///<summary>
        ///Identifier for device depending on provider (device ID, player ID)
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Identifier for device depending on provider (device ID, player ID)", IsRequired=true, Name="Token", ParameterType="body")]
        public virtual string Token { get; set; }

        ///<summary>
        ///User ID to attach this token to.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User ID to attach this token to.", Name="UserId", ParameterType="body")]
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///Device ID to attach this token to. New device will be created if this is null.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Device ID to attach this token to. New device will be created if this is null.", Name="DeviceId", ParameterType="body")]
        public virtual Guid? DeviceId { get; set; }

        ///<summary>
        ///Device operating system
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device operating system", Name="OperatingSystem", ParameterType="body")]
        public virtual string OperatingSystem { get; set; }

        ///<summary>
        ///Device brand
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device brand", Name="Brand", ParameterType="body")]
        public virtual string Brand { get; set; }

        ///<summary>
        ///Device name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device name", Name="DeviceName", ParameterType="body")]
        public virtual string DeviceName { get; set; }

        ///<summary>
        ///Device timezone, expects to get a TZ database name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device timezone, expects to get a TZ database name", Name="TimeZone", ParameterType="body")]
        public virtual string TimeZone { get; set; }

        ///<summary>
        ///Device language, expects to get a 2 letter identifier
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device language, expects to get a 2 letter identifier", Name="Language", ParameterType="body")]
        public virtual string Language { get; set; }

        ///<summary>
        ///Device locale
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device locale", Name="Locale", ParameterType="body")]
        public virtual string Locale { get; set; }

        ///<summary>
        ///Other device information
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Other device information", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }
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
        public virtual string DisplayName { get; set; }

        ///<summary>
        ///Email address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email address", IsRequired=true, Name="Email", ParameterType="body")]
        public virtual string Email { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="body")]
        public virtual string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="body")]
        public virtual string LastName { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="body")]
        public virtual string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="body")]
        public virtual string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="body")]
        public virtual string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="body")]
        public virtual bool? SubscribeToNews { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public virtual string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public virtual string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public virtual string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public virtual string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public virtual string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public virtual string Address2 { get; set; }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", Name="Phone", ParameterType="body")]
        public virtual string Phone { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public virtual string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public virtual string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public virtual string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public virtual string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public virtual string BirthDate { get; set; }

        ///<summary>
        ///Full permission tree
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Full permission tree", Name="RolesTree", ParameterType="body")]
        public virtual List<UserRoleUpdateInput> RolesTree { get; set; }

        ///<summary>
        ///User's zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's zone", Name="Zone", ParameterType="body")]
        public virtual string Zone { get; set; }
    }

    public class RegisterGuestUserResponse
        : ResponseBase<User>
    {
        public virtual string BearerToken { get; set; }
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
        public virtual string Phone { get; set; }

        ///<summary>
        ///Member display name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member display name", Name="DisplayName", ParameterType="body")]
        public virtual string DisplayName { get; set; }

        ///<summary>
        ///Email address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email address", Name="Email", ParameterType="body")]
        public virtual string Email { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="body")]
        public virtual string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="body")]
        public virtual string LastName { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="body")]
        public virtual string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="body")]
        public virtual string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="body")]
        public virtual string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="body")]
        public virtual bool? SubscribeToNews { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public virtual string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public virtual string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public virtual string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public virtual string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public virtual string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public virtual string Address2 { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public virtual string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public virtual string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public virtual string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public virtual string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public virtual string BirthDate { get; set; }

        ///<summary>
        ///Full permission tree
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Full permission tree", Name="RolesTree", ParameterType="body")]
        public virtual List<UserRoleUpdateInput> RolesTree { get; set; }

        ///<summary>
        ///User's zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's zone", Name="Zone", ParameterType="body")]
        public virtual string Zone { get; set; }
    }

    public class RegisterPhoneUserResponse
        : ResponseBase<User>
    {
        public virtual string BearerToken { get; set; }
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
        public virtual string DisplayName { get; set; }

        ///<summary>
        ///Email address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email address", Name="Email", ParameterType="body")]
        public virtual string Email { get; set; }

        ///<summary>
        ///Username
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Username", Name="UserName", ParameterType="body")]
        public virtual string UserName { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="body")]
        public virtual string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="body")]
        public virtual string LastName { get; set; }

        ///<summary>
        ///Password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Password", IsRequired=true, Name="Password", ParameterType="body")]
        public virtual string Password { get; set; }

        ///<summary>
        ///Role names to be applied
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Role names to be applied", Name="Roles", ParameterType="body")]
        public virtual List<string> Roles { get; set; }

        ///<summary>
        ///Full permission tree
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Full permission tree", Name="RolesTree", ParameterType="body")]
        public virtual List<UserRoleUpdateInput> RolesTree { get; set; }

        ///<summary>
        ///Login immediately ?
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Login immediately ?", Name="AutoLogin", ParameterType="body")]
        public virtual bool AutoLogin { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="body")]
        public virtual string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="body")]
        public virtual string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="body")]
        public virtual string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="body")]
        public virtual bool? SubscribeToNews { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public virtual string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public virtual string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public virtual string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public virtual string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public virtual string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public virtual string Address2 { get; set; }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", Name="Phone", ParameterType="body")]
        public virtual string Phone { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public virtual string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public virtual string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public virtual string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public virtual string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public virtual string BirthDate { get; set; }

        ///<summary>
        ///User's zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's zone", Name="Zone", ParameterType="body")]
        public virtual string Zone { get; set; }
    }

    public class RegisterUserV2Response
        : ResponseBase<User>
    {
        public virtual string BearerToken { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{CollectionName}/replaceOne", "PUT")]
    [Route("/{version}/db/{collectionName}/replaceOne", "PUT")]
    [Route("/{version}/db/{collectionName}/replaceOne/{id}", "PUT")]
    [Api(Description="Database services")]
    [DataContract]
    public class ReplaceOneRequest
        : CodeMashDbRequestBase, IReturn<ReplaceOneResponse>
    {
        ///<summary>
        ///Entity represented as json to replace
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Entity represented as json to replace", IsRequired=true, Name="Document", ParameterType="body")]
        public virtual string Document { get; set; }

        ///<summary>
        ///ID of a record to replace. Required if Filter is empty.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of a record to replace. Required if Filter is empty.", Name="Id", ParameterType="path")]
        public virtual string Id { get; set; }

        ///<summary>
        ///The selection criteria for the update. The same query selectors as in the Find method are available. - https://docs.mongodb.org/manual/reference/method/db.collection.replaceOne/ . Specify an empty document '{}' to update the first document returned in the collection
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The selection criteria for the update. The same query selectors as in the Find method are available. - https://docs.mongodb.org/manual/reference/method/db.collection.replaceOne/ . Specify an empty document '{}' to update the first document returned in the collection", Name="Filter", ParameterType="body")]
        public virtual string Filter { get; set; }

        ///<summary>
        ///By default file uploads are done after the record is inserted, set to true in case you need to wait for files to be uploaded
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="By default file uploads are done after the record is inserted, set to true in case you need to wait for files to be uploaded", Name="WaitForFileUpload", ParameterType="body")]
        public virtual bool WaitForFileUpload { get; set; }

        ///<summary>
        ///If true, inserts a new record if current record not found
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, inserts a new record if current record not found", Name="IsUpsert", ParameterType="body")]
        public virtual bool IsUpsert { get; set; }

        ///<summary>
        ///By default records are validated before insert, set to true to prevent validation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="By default records are validated before insert, set to true to prevent validation", IsRequired=true, Name="BypassDocumentValidation", ParameterType="body")]
        public virtual bool BypassDocumentValidation { get; set; }

        ///<summary>
        ///If true, does not activate triggers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, does not activate triggers", Name="IgnoreTriggers", ParameterType="body")]
        public virtual bool IgnoreTriggers { get; set; }

        ///<summary>
        ///If true, file fields that are passed will expect file ids given from your storage providers.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, file fields that are passed will expect file ids given from your storage providers.", Name="ResolveProviderFiles", ParameterType="body")]
        public virtual bool ResolveProviderFiles { get; set; }
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
        public virtual bool IsAcknowledged { get; set; }

        ///<summary>
        ///Checks if modifiedCount is available
        ///</summary>
        [DataMember(Name="isModifiedCountAvailable")]
        [ApiMember(DataType="bool", Description="Checks if modifiedCount is available", Name="IsModifiedCountAvailable")]
        public virtual bool IsModifiedCountAvailable { get; set; }

        ///<summary>
        ///matchedCount containing the number of matched documents
        ///</summary>
        [DataMember(Name="matchedCount")]
        [ApiMember(DataType="bool", Description="matchedCount containing the number of matched documents", Name="MatchedCount")]
        public virtual long MatchedCount { get; set; }

        ///<summary>
        ///modifiedCount containing the number of modified documents
        ///</summary>
        [DataMember(Name="modifiedCount")]
        [ApiMember(DataType="long", Description="modifiedCount containing the number of modified documents", Name="ModifiedCount")]
        public virtual long ModifiedCount { get; set; }

        ///<summary>
        ///upsertedId containing the _id for the upserted document
        ///</summary>
        [DataMember(Name="upsertedId")]
        [ApiMember(DataType="string", Description="upsertedId containing the _id for the upserted document", Name="UpsertedId")]
        public virtual string UpsertedId { get; set; }
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
        public virtual string Token { get; set; }

        ///<summary>
        ///New user password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New user password", IsRequired=true, Name="Password", ParameterType="body")]
        public virtual string Password { get; set; }

        ///<summary>
        ///New repeated user password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New repeated user password", IsRequired=true, Name="RepeatedPassword", ParameterType="body")]
        public virtual string RepeatedPassword { get; set; }
    }

    public class Role
    {
        public Role()
        {
            Policies = new List<Policy>{};
        }

        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual List<Policy> Policies { get; set; }
    }

    public class Schema
    {
        public Schema()
        {
            TranslatableFields = new List<string>{};
        }

        public virtual string CollectionNameAsTitle { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string UiSchema { get; set; }
        public virtual string JsonSchema { get; set; }
        public virtual List<string> TranslatableFields { get; set; }
        public virtual Guid DatabaseId { get; set; }
        public virtual Guid SchemaId { get; set; }
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
        public virtual Guid TemplateId { get; set; }

        ///<summary>
        ///Recipients' email addresses. Emails, Users or Roles are required.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Recipients' email addresses. Emails, Users or Roles are required.", Name="Emails", ParameterType="body")]
        public virtual List<string> Emails { get; set; }

        ///<summary>
        ///Recipients' user IDs. Emails, Users or Roles are required.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Recipients' user IDs. Emails, Users or Roles are required.", Name="Users", ParameterType="body")]
        public virtual List<Guid> Users { get; set; }

        ///<summary>
        ///Recipients' roles. Emails, Users or Roles are required.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Recipients' roles. Emails, Users or Roles are required.", Name="Roles", ParameterType="body")]
        public virtual List<string> Roles { get; set; }

        ///<summary>
        ///CC recipients' email addresses
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="CC recipients' email addresses", Name="CcEmails", ParameterType="body")]
        public virtual List<string> CcEmails { get; set; }

        ///<summary>
        ///CC recipients' user IDs
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="CC recipients' user IDs", Name="CcUsers", ParameterType="body")]
        public virtual List<Guid> CcUsers { get; set; }

        ///<summary>
        ///BCC recipients' email addresses
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="BCC recipients' email addresses", Name="BccEmails", ParameterType="body")]
        public virtual List<string> BccEmails { get; set; }

        ///<summary>
        ///BCC recipients' user IDs
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="BCC recipients' user IDs", Name="BccUsers", ParameterType="body")]
        public virtual List<Guid> BccUsers { get; set; }

        ///<summary>
        ///Custom tokens to inject into template
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Object", Description="Custom tokens to inject into template", Name="Tokens", ParameterType="body")]
        public virtual Dictionary<string, string> Tokens { get; set; }

        ///<summary>
        ///Amount of milliseconds to postpone sending the email
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="Amount of milliseconds to postpone sending the email", Name="Postpone", ParameterType="body")]
        public virtual long? Postpone { get; set; }

        ///<summary>
        ///If true, sends an email by recipient's time zone. Postpone needs to be set for this to have an effect. Requires Users or Roles recipients. Default - true
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, sends an email by recipient's time zone. Postpone needs to be set for this to have an effect. Requires Users or Roles recipients. Default - true", Name="RespectTimeZone", ParameterType="body")]
        public virtual bool RespectTimeZone { get; set; }

        ///<summary>
        ///If true, will try to send an email using a language from CultureCode instead of user's language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, will try to send an email using a language from CultureCode instead of user's language", Name="ForceRequestLanguage", ParameterType="body")]
        public virtual bool ForceRequestLanguage { get; set; }

        ///<summary>
        ///File IDs to attach to email message
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="File IDs to attach to email message", Name="Attachments", ParameterType="body")]
        public virtual List<string> Attachments { get; set; }
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
        public virtual Guid TemplateId { get; set; }

        ///<summary>
        ///Recipients list. You can send messages by specifying CodeMash membership users which are combined with devices.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="List", Description="Recipients list. You can send messages by specifying CodeMash membership users which are combined with devices.", Name="Users", ParameterType="body")]
        public virtual List<Guid> Users { get; set; }

        ///<summary>
        ///Messages to be delivered into specified devices.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="List", Description="Messages to be delivered into specified devices.", Name="Devices", ParameterType="body")]
        public virtual List<Guid> Devices { get; set; }

        ///<summary>
        ///Messages to be delivered to specified roles.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="List", Description="Messages to be delivered to specified roles.", Name="Roles", ParameterType="body")]
        public virtual List<string> Roles { get; set; }

        ///<summary>
        ///Custom tokens which are not provided in project
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Custom tokens which are not provided in project", Name="Tokens", ParameterType="body")]
        public virtual Dictionary<string, string> Tokens { get; set; }

        ///<summary>
        ///Amount of milliseconds to postpone pushing the notification.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="Amount of milliseconds to postpone pushing the notification.", Name="Postpone", ParameterType="body")]
        public virtual long? Postpone { get; set; }

        ///<summary>
        ///Respects user's time zone. If false, send by project time zone. Default - true. Postpone needs to be set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Respects user's time zone. If false, send by project time zone. Default - true. Postpone needs to be set.", Name="RespectTimeZone", ParameterType="body")]
        public virtual bool RespectTimeZone { get; set; }

        ///<summary>
        ///If set to true, notification will not be pushed to user's device. Cannot be true if postpone is set.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If set to true, notification will not be pushed to user's device. Cannot be true if postpone is set.", Name="IsNonPushable", ParameterType="body")]
        public virtual bool IsNonPushable { get; set; }

        ///<summary>
        ///Will try to send a template for language from CultureCode over the user's language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Will try to send a template for language from CultureCode over the user's language", Name="ForceRequestLanguage", ParameterType="body")]
        public virtual bool ForceRequestLanguage { get; set; }
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
        public virtual Guid AccountId { get; set; }

        ///<summary>
        ///Id of the bank to connect with.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Id of the bank to connect with.", Name="BankId", ParameterType="body")]
        public virtual string BankId { get; set; }
    }

    public class StartKevinAuthResponse
        : ResponseBase<KevinAuthorizationLink>
    {
    }

    public class StripePaymentIntent
    {
        public virtual string PaymentId { get; set; }
        public virtual string PaymentClientSecret { get; set; }
        public virtual string Status { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string TransactionId { get; set; }
    }

    public class Taxonomy
    {
        public Taxonomy()
        {
            Dependencies = new List<string>{};
            TranslatableFields = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ParentId { get; set; }
        public virtual List<string> Dependencies { get; set; }
        public virtual string TermsUiSchema { get; set; }
        public virtual string TermsJsonSchema { get; set; }
        public virtual List<string> TranslatableFields { get; set; }
        public virtual Guid DatabaseId { get; set; }
        public virtual Guid TaxonomyId { get; set; }
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

        public virtual string TaxonomyId { get; set; }
        public virtual string TaxonomyName { get; set; }
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Dictionary<string, string> Names { get; set; }
        public virtual Dictionary<string, string> Descriptions { get; set; }
        public virtual string ParentId { get; set; }
        public virtual string ParentName { get; set; }
        public virtual Dictionary<string, string> ParentNames { get; set; }
        public virtual int? Order { get; set; }
        public virtual List<TermMultiParent> MultiParents { get; set; }
        public virtual string Meta { get; set; }
    }

    public class TermMultiParent
    {
        public TermMultiParent()
        {
            Names = new Dictionary<string, string>{};
        }

        public virtual string ParentId { get; set; }
        public virtual string TaxonomyId { get; set; }
        public virtual string Name { get; set; }
        public virtual Dictionary<string, string> Names { get; set; }
    }

    public class Token
    {
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
        public virtual string Owner { get; set; }
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
        public virtual string Mode { get; set; }

        ///<summary>
        ///Code received from Twitter services
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Code received from Twitter services", Name="Code", ParameterType="query")]
        public virtual string Code { get; set; }

        ///<summary>
        ///State received with a code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="State received with a code", Name="State", ParameterType="query")]
        public virtual string State { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessToken", ParameterType="query")]
        public virtual string AccessToken { get; set; }

        ///<summary>
        ///When transferring access token from client app
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="When transferring access token from client app", Name="AccessTokenSecret", ParameterType="query")]
        public virtual string AccessTokenSecret { get; set; }

        ///<summary>
        ///Auth token
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Auth token", Name="OAuthToken", ParameterType="query")]
        public virtual string OAuthToken { get; set; }

        ///<summary>
        ///Auth verifier
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Auth verifier", Name="OAuthVerifier", ParameterType="query")]
        public virtual string OAuthVerifier { get; set; }
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
        public virtual Guid Id { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Customer's phone. Overrides user's phone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's phone. Overrides user's phone.", Name="Phone", ParameterType="body")]
        public virtual string Phone { get; set; }

        ///<summary>
        ///Customer's full name. Overrides user's name.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's full name. Overrides user's name.", Name="Name", ParameterType="body")]
        public virtual string Name { get; set; }

        ///<summary>
        ///Customer's description.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's description.", Name="Description", ParameterType="body")]
        public virtual string Description { get; set; }

        ///<summary>
        ///Customer's email. Overrides user's email.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's email. Overrides user's email.", Name="Email", ParameterType="body")]
        public virtual string Email { get; set; }

        ///<summary>
        ///Customer's city. Overrides user's city.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's city. Overrides user's city.", Name="City", ParameterType="body")]
        public virtual string City { get; set; }

        ///<summary>
        ///Customer's country. Overrides user's country.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's country. Overrides user's country.", Name="CountryCode", ParameterType="body")]
        public virtual string CountryCode { get; set; }

        ///<summary>
        ///Customer's address 1. Overrides user's address 1.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's address 1. Overrides user's address 1.", Name="Address1", ParameterType="body")]
        public virtual string Address { get; set; }

        ///<summary>
        ///Customer's address 2. Overrides user's address 2.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's address 2. Overrides user's address 2.", Name="Address2", ParameterType="body")]
        public virtual string Address2 { get; set; }

        ///<summary>
        ///Customer's postal code. Overrides user's postal code.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's postal code. Overrides user's postal code.", Name="PostalCode", ParameterType="body")]
        public virtual string PostalCode { get; set; }

        ///<summary>
        ///Customer's state. Overrides user's area.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's state. Overrides user's area.", Name="Area", ParameterType="body")]
        public virtual string Area { get; set; }

        ///<summary>
        ///Additional key, value data.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Additional key, value data.", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }
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
        public virtual Guid Id { get; set; }

        ///<summary>
        ///Device Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Dictionary", Description="Device Id", IsRequired=true, Name="Id", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Device operating system
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device operating system", Name="OperatingSystem", ParameterType="body")]
        public virtual string OperatingSystem { get; set; }

        ///<summary>
        ///Device brand
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device brand", Name="Brand", ParameterType="body")]
        public virtual string Brand { get; set; }

        ///<summary>
        ///Device name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device name", Name="DeviceName", ParameterType="body")]
        public virtual string DeviceName { get; set; }

        ///<summary>
        ///Device timezone, expects to get a TZ database name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device timezone, expects to get a TZ database name", Name="TimeZone", ParameterType="body")]
        public virtual string TimeZone { get; set; }

        ///<summary>
        ///Device language, expects to get a 2 letter identifier
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device language, expects to get a 2 letter identifier", Name="Language", ParameterType="body")]
        public virtual string Language { get; set; }

        ///<summary>
        ///Device locale
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Device locale", Name="Locale", ParameterType="body")]
        public virtual string Locale { get; set; }

        ///<summary>
        ///Meta information that comes from device. Pass an empty object to delete.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Object", Description="Meta information that comes from device. Pass an empty object to delete.", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }
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
        public virtual Guid Id { get; set; }

        ///<summary>
        ///In which time zone device is registered. If we are aware of location, we can provide notifications in right time frame.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="In which time zone device is registered. If we are aware of location, we can provide notifications in right time frame.", Name="TimeZone", ParameterType="form")]
        public virtual string TimeZone { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///User Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="User Id", Name="UserId", ParameterType="body")]
        public virtual Guid? UserId { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Display name of the discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Display name of the discount.", Name="DisplayName", ParameterType="body")]
        public virtual string DisplayName { get; set; }

        ///<summary>
        ///Start date of being active.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="Start date of being active.", Name="ValidFrom", ParameterType="body")]
        public virtual long? ValidFrom { get; set; }

        ///<summary>
        ///End date of being active.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="long", Description="End date of being active.", Name="ValidUntil", ParameterType="body")]
        public virtual long? ValidUntil { get; set; }

        ///<summary>
        ///Restriction type of discount. Can be Fixed, Percentage, Price or Quantity.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Restriction type of discount. Can be Fixed, Percentage, Price or Quantity.", Name="RestrictionType", ParameterType="body")]
        public virtual string RestrictionType { get; set; }

        ///<summary>
        ///Discount amount for Fixed or Percentage restriction types.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="double", Description="Discount amount for Fixed or Percentage restriction types.", Name="Amount", ParameterType="body")]
        public virtual double? Amount { get; set; }

        ///<summary>
        ///Discount amounts for Price or Quantity restriction types.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Discount amounts for Price or Quantity restriction types.", Name="Boundaries", ParameterType="body")]
        public virtual List<PaymentDiscountBoundary> Boundaries { get; set; }

        ///<summary>
        ///Target type for specific records. Can be All, Category, Individual.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Target type for specific records. Can be All, Category, Individual.", Name="TargetType", ParameterType="body")]
        public virtual string TargetType { get; set; }

        ///<summary>
        ///Records for Individual target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Records for Individual target type.", Name="Records", ParameterType="body")]
        public virtual List<string> Records { get; set; }

        ///<summary>
        ///Collection field for Category target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Collection field for Category target type.", Name="CategoryField", ParameterType="body")]
        public virtual string CategoryField { get; set; }

        ///<summary>
        ///Values for Category target type.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Values for Category target type.", Name="CategoryValues", ParameterType="body")]
        public virtual List<string> CategoryValues { get; set; }

        ///<summary>
        ///Limitations for discounts to be used only with certain payment accounts.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Limitations for discounts to be used only with certain payment accounts.", Name="PaymentAccounts", ParameterType="body")]
        public virtual List<string> PaymentAccounts { get; set; }

        ///<summary>
        ///Users who can redeem this discount.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Users who can redeem this discount.", Name="Users", ParameterType="body")]
        public virtual List<string> Users { get; set; }

        ///<summary>
        ///Emails for potential users who can redeem this discount. When user registers with one of the provided emails, he will be allowed to use this discount. Doesn't work with existing users.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="array", Description="Emails for potential users who can redeem this discount. When user registers with one of the provided emails, he will be allowed to use this discount. Doesn't work with existing users.", Name="Users", ParameterType="body")]
        public virtual List<string> Emails { get; set; }

        ///<summary>
        ///Amount of times that the same user can redeem.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of times that the same user can redeem.", Format="int32", Name="UserCanRedeem", ParameterType="body")]
        public virtual int? UserCanRedeem { get; set; }

        ///<summary>
        ///Amount of times in total this discount can be redeemed.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="integer", Description="Amount of times in total this discount can be redeemed.", Format="int32", Name="TotalCanRedeem", ParameterType="body")]
        public virtual int? TotalCanRedeem { get; set; }

        ///<summary>
        ///Should the discount be enabled.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should the discount be enabled.", Name="Enabled", ParameterType="body")]
        public virtual bool? Enabled { get; set; }

        ///<summary>
        ///Should the discount be combined with other discounts
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should the discount be combined with other discounts", Name="CombineDiscounts", ParameterType="body")]
        public virtual bool? CombineDiscounts { get; set; }
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{collectionName}/bulk", "PATCH")]
    [Route("/{version}/db/{collectionName}/bulk", "PATCH")]
    [Api(Description="Database services")]
    [DataContract]
    public class UpdateManyRequest
        : CodeMashDbRequestBase, IReturn<UpdateManyResponse>
    {
        ///<summary>
        ///The modifications to apply. Use Update Operators such as $set, $unset, or $rename.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The modifications to apply. Use Update Operators such as $set, $unset, or $rename.", IsRequired=true, Name="Update", ParameterType="body")]
        public virtual string Update { get; set; }

        ///<summary>
        ///The selection criteria for the update. The same query selectors as in the Find method are available. - https://docs.mongodb.org/manual/reference/method/db.collection.updateMany/#db.collection.updateMany . Specify an empty document '{}' to update the first document returned in the collection
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The selection criteria for the update. The same query selectors as in the Find method are available. - https://docs.mongodb.org/manual/reference/method/db.collection.updateMany/#db.collection.updateMany . Specify an empty document '{}' to update the first document returned in the collection", IsRequired=true, Name="Filter", ParameterType="body")]
        public virtual string Filter { get; set; }

        ///<summary>
        ///By default records are validated before insert, set to true to prevent validation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="By default records are validated before insert, set to true to prevent validation", Name="BypassDocumentValidation", ParameterType="body")]
        public virtual bool BypassDocumentValidation { get; set; }

        ///<summary>
        ///If true, does not activate triggers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, does not activate triggers", Name="IgnoreTriggers", ParameterType="body")]
        public virtual bool IgnoreTriggers { get; set; }

        ///<summary>
        ///If true, file fields that are passed will expect file ids given from your storage providers.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, file fields that are passed will expect file ids given from your storage providers.", Name="ResolveProviderFiles", ParameterType="body")]
        public virtual bool ResolveProviderFiles { get; set; }
    }

    public class UpdateManyResponse
        : ResponseBase<UpdateResult>
    {
    }

    ///<summary>
    ///Database services
    ///</summary>
    [Route("/db/{collectionName}", "PATCH")]
    [Route("/db/{collectionName}/{id}", "PATCH")]
    [Route("/{version}/db/{collectionName}", "PATCH")]
    [Route("/{version}/db/{collectionName}/{id}", "PATCH")]
    [Api(Description="Database services")]
    [DataContract]
    public class UpdateOneRequest
        : CodeMashDbRequestBase, IReturn<UpdateOneResponse>
    {
        ///<summary>
        ///ID of record to update. Required if Filter is empty.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of record to update. Required if Filter is empty.", Name="Id", ParameterType="path")]
        public virtual string Id { get; set; }

        ///<summary>
        ///The modifications to apply. Use Update Operators such as $set, $unset, or $rename.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The modifications to apply. Use Update Operators such as $set, $unset, or $rename.", IsRequired=true, Name="Update", ParameterType="body")]
        public virtual string Update { get; set; }

        ///<summary>
        ///The selection criteria for the update. Required if Id is empty.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="The selection criteria for the update. Required if Id is empty.", Name="Filter", ParameterType="body")]
        public virtual string Filter { get; set; }

        ///<summary>
        ///By default file uploads are done after the record is inserted, set to true in case you need to wait for files to be uploaded
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="By default file uploads are done after the record is inserted, set to true in case you need to wait for files to be uploaded", Name="WaitForFileUpload", ParameterType="body")]
        public virtual bool WaitForFileUpload { get; set; }

        ///<summary>
        ///By default records are validated before insert, set to true to prevent validation
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="By default records are validated before insert, set to true to prevent validation", IsRequired=true, Name="BypassDocumentValidation", ParameterType="body")]
        public virtual bool BypassDocumentValidation { get; set; }

        ///<summary>
        ///If true, does not activate triggers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, does not activate triggers", Name="IgnoreTriggers", ParameterType="body")]
        public virtual bool IgnoreTriggers { get; set; }

        ///<summary>
        ///If true, file fields that are passed will expect file ids given from your storage providers.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, file fields that are passed will expect file ids given from your storage providers.", Name="ResolveProviderFiles", ParameterType="body")]
        public virtual bool ResolveProviderFiles { get; set; }
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
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///Current password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Current password", Name="CurrentPassword", ParameterType="body")]
        public virtual string CurrentPassword { get; set; }

        ///<summary>
        ///New password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New password", IsRequired=true, Name="Password", ParameterType="body")]
        public virtual string Password { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", Name="CustomerId", ParameterType="query")]
        public virtual string CustomerId { get; set; }

        ///<summary>
        ///Payment method's phone. Overrides user's phone.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's phone. Overrides user's phone.", Name="Phone", ParameterType="body")]
        public virtual string Phone { get; set; }

        ///<summary>
        ///Payment method's full name. Overrides user's name.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's full name. Overrides user's name.", Name="Name", ParameterType="body")]
        public virtual string Name { get; set; }

        ///<summary>
        ///Payment method's description.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's description.", Name="Description", ParameterType="body")]
        public virtual string Description { get; set; }

        ///<summary>
        ///Payment method's email. Overrides user's email.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's email. Overrides user's email.", Name="Email", ParameterType="body")]
        public virtual string Email { get; set; }

        ///<summary>
        ///Payment method's city. Overrides user's city.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's city. Overrides user's city.", Name="City", ParameterType="body")]
        public virtual string City { get; set; }

        ///<summary>
        ///Payment method's country. Overrides user's country.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's country. Overrides user's country.", Name="CountryCode", ParameterType="body")]
        public virtual string CountryCode { get; set; }

        ///<summary>
        ///Payment method's address 1. Overrides user's address 1.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's address 1. Overrides user's address 1.", Name="Address1", ParameterType="body")]
        public virtual string Address { get; set; }

        ///<summary>
        ///Payment method's address 2. Overrides user's address 2.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's address 2. Overrides user's address 2.", Name="Address2", ParameterType="body")]
        public virtual string Address2 { get; set; }

        ///<summary>
        ///Payment method's postal code. Overrides user's postal code.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's postal code. Overrides user's postal code.", Name="PostalCode", ParameterType="body")]
        public virtual string PostalCode { get; set; }

        ///<summary>
        ///Payment method's state. Overrides user's area.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's state. Overrides user's area.", Name="Area", ParameterType="body")]
        public virtual string Area { get; set; }

        ///<summary>
        ///Additional key, value data.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Additional key, value data.", Name="Meta", ParameterType="body")]
        public virtual Dictionary<string, string> Meta { get; set; }
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
        public virtual string Email { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="form")]
        public virtual string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="form")]
        public virtual string LastName { get; set; }

        ///<summary>
        ///Member display name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member display name", Name="DisplayName", ParameterType="form")]
        public virtual string DisplayName { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="form")]
        public virtual string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="form")]
        public virtual string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="form")]
        public virtual string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="form")]
        public virtual bool? SubscribeToNews { get; set; }

        ///<summary>
        ///Marketing email types to unsubscribe from
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Marketing email types to unsubscribe from", Name="UnsubscribedNewsTags", ParameterType="form")]
        public virtual List<string> UnsubscribedNewsTags { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public virtual string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public virtual string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public virtual string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public virtual string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public virtual string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public virtual string Address2 { get; set; }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", Name="Phone", ParameterType="body")]
        public virtual string Phone { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public virtual string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public virtual string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public virtual string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public virtual string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public virtual string BirthDate { get; set; }
    }

    public class UpdateResult
    {
        public virtual bool IsAcknowledged { get; set; }
        public virtual long MatchedCount { get; set; }
        public virtual long ModifiedCount { get; set; }
        public virtual string UpsertedId { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Customer's ID.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Customer's ID.", IsRequired=true, Name="CustomerId", ParameterType="path")]
        public virtual string CustomerId { get; set; }

        ///<summary>
        ///Discount coupon ID if supported.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Discount coupon ID if supported.", Name="Coupon", ParameterType="body")]
        public virtual string Coupon { get; set; }

        ///<summary>
        ///Payment method's ID to use for subscription.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Payment method's ID to use for subscription.", Name="PaymentMethodId", ParameterType="body")]
        public virtual string PaymentMethodId { get; set; }

        ///<summary>
        ///If subscription is set to cancel at period end, renews the subscription.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If subscription is set to cancel at period end, renews the subscription.", Name="RenewCanceled", ParameterType="body")]
        public virtual bool RenewCanceled { get; set; }
    }

    ///<summary>
    ///Membership
    ///</summary>
    [Route("/membership/users", "PATCH PUT")]
    [Route("/{version}/membership/users", "PATCH PUT")]
    [Route("/membership/users/{id}", "PATCH PUT")]
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
        public virtual Guid Id { get; set; }

        ///<summary>
        ///Guest email. Will not work for normal user.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Guest email. Will not work for normal user.", Name="Email", ParameterType="body")]
        public virtual string Email { get; set; }

        ///<summary>
        ///Member first name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member first name", Name="FirstName", ParameterType="body")]
        public virtual string FirstName { get; set; }

        ///<summary>
        ///Member last name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member last name", Name="LastName", ParameterType="body")]
        public virtual string LastName { get; set; }

        ///<summary>
        ///Member display name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Member display name", Name="DisplayName", ParameterType="body")]
        public virtual string DisplayName { get; set; }

        ///<summary>
        ///Role names to be applied
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Role names to be applied", Name="Roles", ParameterType="body")]
        public virtual List<string> Roles { get; set; }

        ///<summary>
        ///Full permission tree
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Full permission tree", Name="RolesTree", ParameterType="body")]
        public virtual List<UserRoleUpdateInput> RolesTree { get; set; }

        ///<summary>
        ///Meta details as JSON object
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Meta details as JSON object", Name="Meta", ParameterType="body")]
        public virtual string Meta { get; set; }

        ///<summary>
        ///Main user language
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Main user language", Name="Language", ParameterType="body")]
        public virtual string Language { get; set; }

        ///<summary>
        ///User's time zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's time zone", Name="TimeZone", ParameterType="body")]
        public virtual string TimeZone { get; set; }

        ///<summary>
        ///Should user get marketing emails
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="Should user get marketing emails", Name="SubscribeToNews", ParameterType="body")]
        public virtual bool? SubscribeToNews { get; set; }

        ///<summary>
        ///Marketing email types to unsubscribe from
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Array", Description="Marketing email types to unsubscribe from", Name="UnsubscribedNewsTags", ParameterType="body")]
        public virtual List<string> UnsubscribedNewsTags { get; set; }

        ///<summary>
        ///User's country
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's country", Name="Country", ParameterType="body")]
        public virtual string Country { get; set; }

        ///<summary>
        ///User's 2 letter country code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's 2 letter country code", Name="CountryCode", ParameterType="body")]
        public virtual string CountryCode { get; set; }

        ///<summary>
        ///User's state / province
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's state / province", Name="Area", ParameterType="body")]
        public virtual string Area { get; set; }

        ///<summary>
        ///User's city
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's city", Name="City", ParameterType="body")]
        public virtual string City { get; set; }

        ///<summary>
        ///User's street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's street address", Name="Address", ParameterType="body")]
        public virtual string Address { get; set; }

        ///<summary>
        ///User's secondary street address
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's secondary street address", Name="Address2", ParameterType="body")]
        public virtual string Address2 { get; set; }

        ///<summary>
        ///User's phone number
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's phone number", Name="Phone", ParameterType="body")]
        public virtual string Phone { get; set; }

        ///<summary>
        ///User's company
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company", Name="Company", ParameterType="body")]
        public virtual string Company { get; set; }

        ///<summary>
        ///User's company code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's company code", Name="CompanyCode", ParameterType="body")]
        public virtual string CompanyCode { get; set; }

        ///<summary>
        ///User's postal code
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's postal code", Name="PostalCode", ParameterType="body")]
        public virtual string PostalCode { get; set; }

        ///<summary>
        ///User's gender
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's gender", Name="Gender", ParameterType="body")]
        public virtual string Gender { get; set; }

        ///<summary>
        ///User's birthdate
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's birthdate", Name="BirthDate", ParameterType="body")]
        public virtual string BirthDate { get; set; }

        ///<summary>
        ///User's zone
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User's zone", Name="Zone", ParameterType="body")]
        public virtual string Zone { get; set; }
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
        public virtual string Path { get; set; }

        ///<summary>
        ///Alternative way to upload a file by providing a base64 encoded string
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Alternative way to upload a file by providing a base64 encoded string", Name="Base64File", ParameterType="body")]
        public virtual Base64FileUpload Base64File { get; set; }

        ///<summary>
        ///File account ID. If not provided, default account will be used.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="File account ID. If not provided, default account will be used.", Name="AccountId", ParameterType="body")]
        public virtual Guid? AccountId { get; set; }
    }

    public class UploadFileResponse
        : ResponseBase<File>
    {
        public virtual string Key { get; set; }
        public virtual string Name { get; set; }
        public virtual long UploadDate { get; set; }
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
        public virtual string Id { get; set; }

        ///<summary>
        ///Category of a file inside order.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Category of a file inside order.", Name="Category", ParameterType="form")]
        public virtual string Category { get; set; }

        ///<summary>
        ///Alternative way to upload a file by providing a base64 encoded string
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Alternative way to upload a file by providing a base64 encoded string", Name="Base64File", ParameterType="body")]
        public virtual Base64FileUpload Base64File { get; set; }
    }

    public class UploadOrderFileResponse
        : ResponseBase<File>
    {
        public virtual string Key { get; set; }
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
        public virtual string RecordId { get; set; }

        ///<summary>
        ///Record file field name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Record file field name", IsRequired=true, Name="UniqueFieldName", ParameterType="form")]
        public virtual string UniqueFieldName { get; set; }

        ///<summary>
        ///Alternative way to upload a file by providing a base64 encoded string
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Alternative way to upload a file by providing a base64 encoded string", Name="Base64File", ParameterType="body")]
        public virtual Base64FileUpload Base64File { get; set; }

        ///<summary>
        ///If true, does not activate triggers
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, does not activate triggers", Name="IgnoreTriggers", ParameterType="body")]
        public virtual bool IgnoreTriggers { get; set; }
    }

    public class UploadRecordFileResponse
        : ResponseBase<File>
    {
        public virtual string Key { get; set; }
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
        public virtual string ChannelId { get; set; }

        ///<summary>
        ///Alternative way to upload a file by providing a base64 encoded string
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Alternative way to upload a file by providing a base64 encoded string", Name="Base64File", ParameterType="body")]
        public virtual Base64FileUpload Base64File { get; set; }
    }

    public class UploadSseMessageFileResponse
        : ResponseBase<File>
    {
        public virtual string Key { get; set; }
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
        public virtual Guid? UserId { get; set; }

        ///<summary>
        ///User meta file field name
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="User meta file field name", IsRequired=true, Name="MetaFieldName", ParameterType="form")]
        public virtual string MetaFieldName { get; set; }

        ///<summary>
        ///Alternative way to upload a file by providing a base64 encoded string
        ///</summary>
        [DataMember]
        [ApiMember(DataType="object", Description="Alternative way to upload a file by providing a base64 encoded string", Name="Base64File", ParameterType="body")]
        public virtual Base64FileUpload Base64File { get; set; }
    }

    public class UploadUserFileResponse
        : ResponseBase<File>
    {
        public virtual string Key { get; set; }
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

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string ModifiedOn { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string UserName { get; set; }
        public virtual List<Role> Roles { get; set; }
        public virtual List<Device> Devices { get; set; }
        public virtual bool RolesEditable { get; set; }
        public virtual string Status { get; set; }
        public virtual string Type { get; set; }
        public virtual string Meta { get; set; }
        public virtual string Language { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string Country { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Area { get; set; }
        public virtual string City { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Company { get; set; }
        public virtual string CompanyCode { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Gender { get; set; }
        public virtual string BirthDate { get; set; }
        public virtual string Zone { get; set; }
        public virtual List<UserAuthProvider> AuthProviders { get; set; }
        public virtual bool HasCredentials { get; set; }
        public virtual bool SubscribedToNews { get; set; }
        public virtual List<string> UnsubscribedNewsTags { get; set; }
        public virtual long? UnreadNotifications { get; set; }
    }

    public class UserAuthProvider
    {
        public virtual string Provider { get; set; }
        public virtual string UserId { get; set; }
    }

    public class UserPolicyUpdateInput
    {
        public UserPolicyUpdateInput()
        {
            Permissions = new List<string>{};
        }

        public virtual string Policy { get; set; }
        public virtual List<string> Permissions { get; set; }
    }

    public class UserRoleUpdateInput
    {
        public UserRoleUpdateInput()
        {
            Policies = new List<UserPolicyUpdateInput>{};
        }

        public virtual string Role { get; set; }
        public virtual List<UserPolicyUpdateInput> Policies { get; set; }
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
        public virtual string Token { get; set; }

        ///<summary>
        ///If true, includes project details in response
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, includes project details in response", IsRequired=true, Name="IncludeProject", ParameterType="query")]
        public virtual bool IncludeProject { get; set; }
    }

    public class ValidateInvitationTokenResponse
        : ResponseBase<bool>
    {
        public virtual Project Project { get; set; }
        public virtual Guid UserId { get; set; }
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
        public virtual string Token { get; set; }

        ///<summary>
        ///If true, includes project details in response
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, includes project details in response", IsRequired=true, Name="IncludeProject", ParameterType="query")]
        public virtual bool IncludeProject { get; set; }
    }

    public class ValidatePasswordTokenResponse
        : ResponseBase<bool>
    {
        public virtual Project Project { get; set; }
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
        public virtual string Token { get; set; }

        ///<summary>
        ///If true, includes project details in response
        ///</summary>
        [DataMember]
        [ApiMember(DataType="bool", Description="If true, includes project details in response", IsRequired=true, Name="IncludeProject", ParameterType="query")]
        public virtual bool IncludeProject { get; set; }
    }

    public class ValidateUserDeactivationTokenResponse
        : ResponseBase<bool>
    {
        public virtual Project Project { get; set; }
        public virtual bool HasCredentials { get; set; }
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
        public virtual string Receipt { get; set; }

        ///<summary>
        ///ID of customer who subscribed. Required for first time calling this endpoint.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of customer who subscribed. Required for first time calling this endpoint.", Name="CustomerId", ParameterType="body")]
        public virtual string CustomerId { get; set; }

        ///<summary>
        ///Subscription plan ID. Required for first time calling this endpoint.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Subscription plan ID. Required for first time calling this endpoint.", Name="PlanId", ParameterType="body")]
        public virtual Guid PlanId { get; set; }
    }

    public class VerifyAppleAppStoreSubscriptionResponse
        : ResponseBase<List<PaymentSubscription>>
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
        public virtual string Receipt { get; set; }

        ///<summary>
        ///ID of customer who subscribed. Required for first time calling this endpoint.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of customer who subscribed. Required for first time calling this endpoint.", Name="CustomerId", ParameterType="body")]
        public virtual string CustomerId { get; set; }

        ///<summary>
        ///Subscription plan ID. Required for first time calling this endpoint.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="Guid", Description="Subscription plan ID. Required for first time calling this endpoint.", Name="PlanId", ParameterType="body")]
        public virtual Guid PlanId { get; set; }
    }

    public class VerifyGooglePlayStoreSubscriptionResponse
        : ResponseBase<List<PaymentSubscription>>
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
        public virtual string Token { get; set; }

        ///<summary>
        ///New user password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New user password", IsRequired=true, Name="Password", ParameterType="body")]
        public virtual string Password { get; set; }

        ///<summary>
        ///New repeated user password
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="New repeated user password", IsRequired=true, Name="RepeatedPassword", ParameterType="body")]
        public virtual string RepeatedPassword { get; set; }
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
        public virtual string Token { get; set; }
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

        public virtual Guid Id { get; set; }
        public virtual string Email { get; set; }
        public virtual AccountStatus Status { get; set; }
        public virtual List<MemberDto> Members { get; set; }
        public virtual List<Token> Tokens { get; set; }
        public virtual DatabaseCredentials DatabaseCredentials { get; set; }
        public virtual List<ProjectSmallDto> Projects { get; set; }
        public virtual Dictionary<string, ProjectBillingSettingsDto> ProjectBilling { get; set; }
        public virtual List<CustomerSettingsDto> Customers { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual List<SubscriptionInvoice> SubscriptionInvoices { get; set; }
        public virtual List<UsageInvoice> UsageInvoices { get; set; }
        public virtual List<CustomerInvoice> CustomerInvoices { get; set; }
        public virtual List<RefundInvoice> RefundInvoices { get; set; }
        public virtual SubscriptionInvoice SubscriptionUnpaidInvoice { get; set; }
        public virtual CardDto Card { get; set; }
        public virtual BillingDto Billing { get; set; }
        public virtual List<DiscountDto> Discounts { get; set; }
        public virtual string CustomerId { get; set; }
    }

    public class BillingDto
    {
        public virtual string Type { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Organization { get; set; }
        public virtual string Vat { get; set; }
        public virtual string Address { get; set; }
        public virtual string Zip { get; set; }
        public virtual string Country { get; set; }
        public virtual string City { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Iban { get; set; }
        public virtual string BillingEmail { get; set; }
    }

    public class CardDto
    {
        public virtual long ExpMonth { get; set; }
        public virtual long ExpYear { get; set; }
        public virtual string Last4 { get; set; }
        public virtual string Name { get; set; }
        public virtual string Brand { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Funding { get; set; }
    }

    public class CustomerSettingsDto
    {
        public virtual CardDto Card { get; set; }
        public virtual string UserId { get; set; }
    }

    public class DiscountDto
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual bool IsPercentDiscount { get; set; }
        public virtual double PercentOff { get; set; }
        public virtual decimal FixedDiscount { get; set; }
        public virtual decimal UsedAmount { get; set; }
        public virtual string DurationType { get; set; }
        public virtual long AppliedOn { get; set; }
        public virtual string AppliedOnString { get; set; }
        public virtual long? ValidUntil { get; set; }
        public virtual string ValidUntilString { get; set; }
        public virtual string DiscountType { get; set; }
    }

    public class MemberDto
    {
        public virtual string Id { get; set; }
        public virtual string FullName { get; set; }
    }

    public class ProjectBillingChargeDto
    {
        public virtual string BillingType { get; set; }
        public virtual string MarginType { get; set; }
        public virtual double MarginPercent { get; set; }
        public virtual decimal FixedPrice { get; set; }
        public virtual bool ChargeCustomer { get; set; }
    }

    public class ProjectBillingInvoiceDto
    {
        public virtual string NumberPrefix { get; set; }
    }

    public class ProjectBillingSettingsDto
    {
        public virtual bool Enabled { get; set; }
        public virtual ProjectBillingChargeDto Charge { get; set; }
        public virtual ProjectBillingInvoiceDto Invoice { get; set; }
        public virtual BillingDto Billing { get; set; }
    }

    public class ProjectContextDto
    {
        public virtual ProjectSmallDto Project { get; set; }
        public virtual AccountDto Account { get; set; }
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

        public virtual Guid Id { get; set; }
        public virtual bool DatabaseEstablished { get; set; }
        public virtual bool DatabaseEnabled { get; set; }
        public virtual int? DatabaseVersion { get; set; }
        public virtual bool EmailEstablished { get; set; }
        public virtual bool EmailEnabled { get; set; }
        public virtual bool MembershipEstablished { get; set; }
        public virtual bool MembershipEnabled { get; set; }
        public virtual bool LoggingEstablished { get; set; }
        public virtual bool LoggingEnabled { get; set; }
        public virtual bool ServerEventsEstablished { get; set; }
        public virtual bool ServerEventsEnabled { get; set; }
        public virtual bool NotificationEstablished { get; set; }
        public virtual bool NotificationEnabled { get; set; }
        public virtual bool SchedulerEstablished { get; set; }
        public virtual bool SchedulerEnabled { get; set; }
        public virtual bool ServerlessEstablished { get; set; }
        public virtual bool ServerlessEnabled { get; set; }
        public virtual bool FilingEstablished { get; set; }
        public virtual bool FilingEnabled { get; set; }
        public virtual int? FilesVersion { get; set; }
        public virtual bool PaymentEstablished { get; set; }
        public virtual bool PaymentEnabled { get; set; }
        public virtual bool AuthorizationEnabled { get; set; }
        public virtual bool AuthenticationEnabled { get; set; }
        public virtual bool BackupsEnabled { get; set; }
        public virtual DatabaseCredentials DatabaseCredentials { get; set; }
        public virtual List<Token> Tokens { get; set; }
        public virtual string LogoUrl { get; set; }
        public virtual string Url { get; set; }
        public virtual string LogoId { get; set; }
        public virtual List<string> Languages { get; set; }
        public virtual string DefaultLanguage { get; set; }
        public virtual ProjectZoneDto ProjectZone { get; set; }
        public virtual List<string> UserZones { get; set; }
        public virtual string DefaultTimeZone { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string SlugifiedName { get; set; }
        public virtual ProjectModulesData ModuleData { get; set; }
        public virtual List<string> AllowedOrigins { get; set; }
        public virtual List<ProjectWidgetDto> Widgets { get; set; }
        public virtual int Connections { get; set; }
        public virtual int Members { get; set; }
        public virtual bool CanCallDatabaseService { get; set; }
        public virtual bool CanCallEmailService { get; set; }
        public virtual bool CanCallMembershipService { get; set; }
        public virtual bool CanCallFilingService { get; set; }
        public virtual bool CanCallLoggingService { get; set; }
        public virtual bool CanCallNotificationService { get; set; }
        public virtual bool CanCallSchedulerService { get; set; }
        public virtual bool CanCallServerlessService { get; set; }
        public virtual bool CanCallPaymentService { get; set; }
        public virtual bool CanCallServerEventsService { get; set; }
    }

    public class ProjectWidgetDto
    {
        public virtual string WidgetType { get; set; }
        public virtual string Module { get; set; }
    }

    public class ProjectZoneDto
    {
        public virtual string UniqueName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Continent { get; set; }
    }

    public class ServerlessFunctionAliasDto
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreatedOnDate { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string Version { get; set; }
        public virtual string AdditionalVersion { get; set; }
        public virtual double AdditionalVersionWeight { get; set; }
        public virtual string Description { get; set; }
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

        public virtual string Id { get; set; }
        public virtual long? ModifiedOn { get; set; }
        public virtual long? RefreshOn { get; set; }
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Description { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string MainModule { get; set; }
        public virtual string Runtime { get; set; }
        public virtual long? CodeSize { get; set; }
        public virtual string Region { get; set; }
        public virtual ServerlessProvider Provider { get; set; }
        public virtual string Template { get; set; }
        public virtual bool IsCreated { get; set; }
        public virtual string Handler { get; set; }
        public virtual long Memory { get; set; }
        public virtual int TimeoutMinutes { get; set; }
        public virtual int TimeoutSeconds { get; set; }
        public virtual Dictionary<string, string> Environment { get; set; }
        public virtual List<ServerlessFunctionAliasDto> Aliases { get; set; }
        public virtual List<ServerlessFunctionVersionDto> Versions { get; set; }
        public virtual bool IsSystem { get; set; }
        public virtual bool IsMultiple { get; set; }
        public virtual bool MustConfigure { get; set; }
        public virtual string SystemVersion { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual List<string> AvailableTokens { get; set; }
        public virtual Dictionary<string, TokenResolverField> TokenResolvers { get; set; }
        public virtual string DocsId { get; set; }
        public virtual List<string> Tags { get; set; }
        public virtual List<string> ResourcesTriggerUsages { get; set; }
        public virtual string AuthProvider { get; set; }
        public virtual string InvokeUrl { get; set; }
        public virtual string ServiceAccount { get; set; }
    }

    public class ServerlessFunctionVersionDto
    {
        public virtual string Version { get; set; }
        public virtual DateTime? CreatedOnDate { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string Description { get; set; }
        public virtual string Runtime { get; set; }
        public virtual string Handler { get; set; }
        public virtual long Memory { get; set; }
        public virtual int TimeoutMinutes { get; set; }
        public virtual int TimeoutSeconds { get; set; }
        public virtual long CodeSize { get; set; }
    }

    public class ServerlessProviderDto
    {
        public ServerlessProviderDto()
        {
            Regions = new List<string>{};
            Tags = new Dictionary<string, string>{};
            SecretKeys = new Dictionary<string, string>{};
        }

        public virtual string Provider { get; set; }
        public virtual int TotalFunctions { get; set; }
        public virtual int RefreshRate { get; set; }
        public virtual long LastUpdated { get; set; }
        public virtual bool IsSystem { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual string EncVersion { get; set; }
        public virtual List<string> Regions { get; set; }
        public virtual Dictionary<string, string> Tags { get; set; }
        public virtual string ClientId { get; set; }
        public virtual string PrivateKeyEnding { get; set; }
        public virtual string SubscriptionId { get; set; }
        public virtual string ResourceGroup { get; set; }
        public virtual Dictionary<string, string> SecretKeys { get; set; }
        public virtual string OrgId { get; set; }
    }

    public class SystemFunctionExecutorData
    {
        public SystemFunctionExecutorData()
        {
            Meta = new Dictionary<string, string>{};
            Tokens = new Dictionary<string, string>{};
        }

        public virtual Guid ProjectId { get; set; }
        public virtual Guid AccountId { get; set; }
        public virtual ServerlessFunctionDto Function { get; set; }
        public virtual ServerlessProviderDto Connection { get; set; }
        public virtual string Data { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual Dictionary<string, string> Tokens { get; set; }
        public virtual Guid? ActivatorId { get; set; }
        public virtual ProjectSmallDto Project { get; set; }
        public virtual string CultureCode { get; set; }
        public virtual string Version { get; set; }
    }

}

