/* Options:
Date: 2022-10-13 21:12:11
Version: 6.02
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:5001

GlobalNamespace: CodeMash.Tests.Types.Hub
//MakePartial: True
//MakeVirtual: True
//MakeInternal: False
//MakeDataContractsExtensible: False
//AddReturnMarker: True
//AddDescriptionAsComments: True
//AddDataContractAttributes: False
//AddIndexesToDataMembers: False
//AddGeneratedCodeAttributes: False
//AddResponseStatus: False
//AddImplicitVersion: 
//InitializeCollections: True
//ExportValueTypes: False
//IncludeTypes: 
//ExcludeTypes: 
//AddNamespaces: 
//AddDefaultXmlNamespace: http://schemas.servicestack.net/types
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System.IO;
using CodeMash.Tests.Types.Hub;

namespace CodeMash.Tests.Types.Hub
{
    public partial interface IRequestBase
    {
    }

    public partial class CodeMashDbRequestBase
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

    public partial class CodeMashListRequestBase
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

    public partial class CodeMashRequestBase
        : RequestBase
    {
        ///<summary>
        ///ID of your project. Can be passed in a header as X-CM-ProjectId.
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="ID of your project. Can be passed in a header as X-CM-ProjectId.", IsRequired=true, Name="X-CM-ProjectId", ParameterType="header")]
        public virtual Guid ProjectId { get; set; }
    }

    public partial interface ICultureBasedRequest
    {
        string CultureCode { get; set; }
    }

    public partial interface IRequestWithFilter
    {
        string Filter { get; set; }
    }

    public partial interface IRequestWithPaging
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }

    public partial interface IRequestWithSorting
    {
        string Sort { get; set; }
    }

    public partial interface IVersionBasedRequest
    {
        string CultureCode { get; set; }
    }

    [DataContract(Namespace="http://codemash.io/types/")]
    public partial class RequestBase
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

    public partial class ResponseBase<T>
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(Name="result")]
        public virtual T Result { get; set; }
    }

    public partial class Entity
        : EntityBase, IEntityWithModifiedOn
    {
        [DataMember]
        public virtual DateTime ModifiedOn { get; set; }
    }

    [DataContract]
    public partial class EntityBase
        : IEntity, IEntityWithCreatedOn
    {
        [DataMember]
        public virtual string Id { get; set; }

        [DataMember]
        public virtual DateTime CreatedOn { get; set; }
    }

    public partial interface IEntity
    {
        string Id { get; set; }
    }

    public partial interface IEntityWithCreatedOn
    {
        DateTime CreatedOn { get; set; }
    }

    public partial interface IEntityWithModifiedOn
    {
        DateTime ModifiedOn { get; set; }
    }

    public partial class PaymentDiscountBoundary
    {
        public virtual double Boundary { get; set; }
        public virtual double Amount { get; set; }
        public virtual PaymentDiscountPriceType Type { get; set; }
    }

    public partial class DbFile
    {
        public DbFile()
        {
            Optimizations = new List<DbFileOptimization>{};
        }

        public virtual string Id { get; set; }
        public virtual string Directory { get; set; }
        public virtual string OriginalFileName { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FilePath { get; set; }
        public virtual string ContentType { get; set; }
        public virtual long ContentLength { get; set; }
        public virtual List<DbFileOptimization> Optimizations { get; set; }
    }

    public partial class DbFileOptimization
    {
        public virtual string OptimizedFileId { get; set; }
        public virtual string Optimization { get; set; }
        public virtual string OriginalFileName { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FilePath { get; set; }
        public virtual string ContentType { get; set; }
        public virtual long ContentLength { get; set; }
        public virtual string Directory { get; set; }
    }

    public partial class FunctionResource
        : Entity
    {
        public virtual string UniqueName { get; set; }
        public virtual string ConfigName { get; set; }
    }

    public partial class SchemaCollectionProperty
        : SchemaProperty
    {
        public SchemaCollectionProperty()
        {
            ResponsibleInsertReference = new List<string>{};
        }

        public virtual string CollectionId { get; set; }
        public virtual string FieldToDisplay { get; set; }
        public virtual bool IsMultiple { get; set; }
        public virtual List<string> ResponsibleInsertReference { get; set; }
    }

    public partial class SchemaDateProperty
        : SchemaProperty
    {
        public virtual string MinType { get; set; }
        public virtual int MinDifference { get; set; }
        public virtual string MaxType { get; set; }
        public virtual int MaxDifference { get; set; }
        public virtual long? ConcreteMinDate { get; set; }
        public virtual long? ConcreteMaxDate { get; set; }
    }

    public partial class SchemaEmailProperty
        : SchemaProperty
    {
    }

    public partial class SchemaFileProperty
        : SchemaProperty
    {
        public SchemaFileProperty()
        {
            Storages = new List<SchemaFilePropertyStorage>{};
        }

        public virtual string FilePath { get; set; }
        public virtual string AllowedFileType { get; set; }
        public virtual bool SetPublicAccess { get; set; }
        public virtual long? MaxSizeMb { get; set; }
        public virtual bool IsMultiple { get; set; }
        public virtual bool StrongConnection { get; set; }
        public virtual List<SchemaFilePropertyStorage> Storages { get; set; }
    }

    public partial class SchemaFilePropertyStorage
    {
        public virtual string Path { get; set; }
        public virtual string Cluster { get; set; }
        public virtual string Storage { get; set; }
    }

    public partial class SchemaNestProperty
        : SchemaProperty
    {
    }

    public partial class SchemaNumberProperty
        : SchemaProperty
    {
        public virtual bool IsDecimal { get; set; }
        public virtual bool IsCurrency { get; set; }
        public virtual bool IsTotal { get; set; }
        public virtual int DecimalNumbers { get; set; }
    }

    public partial class SchemaProperty
    {
        public virtual string Name { get; set; }
        public virtual string Path { get; set; }
        public virtual bool RestrictInsert { get; set; }
        public virtual bool RestrictUpdate { get; set; }
    }

    public partial class SchemaRoleProperty
        : SchemaProperty
    {
        public virtual bool IsMultiple { get; set; }
    }

    public partial class SchemaSettings
    {
        public virtual bool SoftDelete { get; set; }
    }

    public partial class SchemaTagsProperty
        : SchemaProperty
    {
    }

    public partial class SchemaTaxonomyProperty
        : SchemaProperty
    {
        public virtual string TaxonomyId { get; set; }
        public virtual bool IsMultiple { get; set; }
    }

    public partial class SchemaTemplate
        : Entity
    {
        public SchemaTemplate()
        {
            References = new List<SchemaTemplateRef>{};
        }

        public virtual string Title { get; set; }
        public virtual string IconUrl { get; set; }
        public virtual string IconUrlDark { get; set; }
        public virtual List<SchemaTemplateRef> References { get; set; }
        public virtual SchemaTemplateRefStructure Structure { get; set; }
    }

    public partial class SchemaTemplateRef
    {
        public SchemaTemplateRef()
        {
            Dependencies = new List<string>{};
        }

        public virtual string TempId { get; set; }
        public virtual bool IsTax { get; set; }
        public virtual string Title { get; set; }
        public virtual string UiSchema { get; set; }
        public virtual string JsonSchema { get; set; }
        public virtual string Description { get; set; }
        public virtual string ParentId { get; set; }
        public virtual List<string> Dependencies { get; set; }
    }

    public partial class SchemaTemplateRefStructure
    {
        public SchemaTemplateRefStructure()
        {
            References = new List<SchemaTemplateRefStructure>{};
        }

        public virtual string TempId { get; set; }
        public virtual string Title { get; set; }
        public virtual bool IsTax { get; set; }
        public virtual List<SchemaTemplateRefStructure> References { get; set; }
    }

    public partial class SchemaTextProperty
        : SchemaProperty
    {
    }

    public partial class SchemaUriProperty
        : SchemaProperty
    {
    }

    public partial class SchemaUserProperty
        : SchemaProperty
    {
        public virtual string FieldToDisplay { get; set; }
        public virtual bool IsMultiple { get; set; }
        public virtual bool AsResponsible { get; set; }
        public virtual bool AsResponsibleUpdate { get; set; }
        public virtual bool AsResponsibleDelete { get; set; }
    }

    public partial class DatabaseRegion
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool Available { get; set; }
        public virtual string Region { get; set; }
        public virtual bool SharedAvailable { get; set; }
        public virtual bool FreeAvailable { get; set; }
        public virtual double NodeValue { get; set; }
    }

    public partial class DatabaseTier
    {
        public DatabaseTier()
        {
            AvailableClusters = new List<string>{};
        }

        public virtual string Tier { get; set; }
        public virtual List<string> AvailableClusters { get; set; }
    }

    public partial class DatabaseCollectionStats
    {
        public DatabaseCollectionStats()
        {
            IndexesSizes = new Dictionary<string, long>{};
        }

        public virtual string CollectionName { get; set; }
        public virtual string CollectionNameAsTitle { get; set; }
        public virtual long RecordsSize { get; set; }
        public virtual long RecordsCount { get; set; }
        public virtual long AverageRecordSize { get; set; }
        public virtual long StorageSize { get; set; }
        public virtual long IndexesCount { get; set; }
        public virtual long IndexesSize { get; set; }
        public virtual Dictionary<string, long> IndexesSizes { get; set; }
    }

    public enum EmailProvider
    {
        None,
        Smtp,
        Sendgrid,
        Mailgun,
        AwsSes,
        CodeMashAws,
    }

    public enum ExportFileTypes
    {
        Csv,
        Json,
    }

    public enum ImportFileTypes
    {
        Csv,
        Json,
    }

    public enum NotificationPriority
    {
        None,
        Default,
        Normal,
        High,
    }

    public enum OwnerType
    {
        System,
        User,
    }

    public enum PaymentDiscountPriceType
    {
        Fixed,
        Percentage,
    }

    public enum PaymentDiscountRestrictionType
    {
        Fixed,
        Percentage,
        Price,
        Quantity,
    }

    public enum PaymentDiscountTargetType
    {
        All,
        Category,
        Individual,
    }

    public enum PaymentDiscountType
    {
        Coupon,
        Discount,
    }

    public enum PaymentProvider
    {
        None,
        Paysera,
        Stripe,
        AppleAppStore,
        GooglePlayStore,
        Kevin,
        Decta,
    }

    public enum PaymentTriggerActionTypes
    {
        Lambda,
        Notification,
        Email,
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

    public enum PushProvider
    {
        None = 0,
        Expo = 1,
        OneSignal = 2,
        Firebase = 4,
    }

    public enum SystemFunctionProvider
    {
        Amazon,
        CodeMash,
    }

    public enum TriggerUsages
    {
        All,
        Collections,
        Users,
        Files,
        Payment,
    }

    public enum UsersTriggerActionTypes
    {
        Lambda,
        Notification,
        Email,
        Webhooks,
    }

    public partial class KeyValue
    {
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
    }

    public partial class KeyValuesItem
    {
        public KeyValuesItem()
        {
            Values = new List<string>{};
        }

        public virtual string Key { get; set; }
        public virtual List<string> Values { get; set; }
    }

    public partial class NameId
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
    }

    public partial class ModuleData
    {
        public virtual string WidgetType { get; set; }
        public virtual ModuleWidget Settings { get; set; }
    }

    public partial class ModuleWidget
    {
    }

    public partial class ProjectModulesData
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

    public enum CustomerBillingType
    {
        Margin,
        Fixed,
    }

    public enum CustomerMarginType
    {
        Percent,
        Fixed,
    }

    public partial class BaseInvoice
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

    public partial class SubscriptionInvoice
        : BaseInvoice
    {
        public virtual string SubscriptionId { get; set; }
        public virtual SubscriptionPlan Plan { get; set; }
    }

    public enum BillingType
    {
        Organization,
        Individual,
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

    public partial class DatabaseCredentials
    {
        public virtual string DbName { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Region { get; set; }
        public virtual string SrvClusterName { get; set; }
        public virtual bool UseSrvName { get; set; }
        public virtual string EncVersion { get; set; }
    }

    public partial class CodeMashDatabaseClusterUpgrade
    {
        public CodeMashDatabaseClusterUpgrade()
        {
            ReadOnlyRegions = new List<string>{};
        }

        public virtual string Tier { get; set; }
        public virtual string Region { get; set; }
        public virtual string RegionName { get; set; }
        public virtual bool AutoScaling { get; set; }
        public virtual bool MultiRegion { get; set; }
        public virtual List<string> ReadOnlyRegions { get; set; }
        public virtual long? StorageSize { get; set; }
        public virtual DateTime? UpdateOn { get; set; }
    }

    public partial class FileAccountProperties
    {
        public virtual FileProvider Provider { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string PublicKey { get; set; }
        public virtual string PrivateKey { get; set; }
        public virtual string BucketName { get; set; }
        public virtual string Region { get; set; }
    }

    public enum FileProvider
    {
        None,
        CodeMash,
        S3,
    }

    public partial class Campaign
    {
        public Campaign()
        {
            Tokens = new List<CampaignToken>{};
            TargetTags = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual Guid TemplateId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? SendDate { get; set; }
        public virtual DateTime? LastSent { get; set; }
        public virtual CampaignStatus Status { get; set; }
        public virtual CampaignType Type { get; set; }
        public virtual CampaignRepeatType RepeatType { get; set; }
        public virtual CampaignTargetType Target { get; set; }
        public virtual bool IsDraft { get; set; }
        public virtual int TimesSent { get; set; }
        public virtual List<CampaignToken> Tokens { get; set; }
        public virtual List<string> TargetTags { get; set; }
    }

    [Flags]
    public enum CampaignRepeatType
    {
        None = 1,
        EveryMonday = 2,
        EveryTuesday = 4,
        EveryWednesday = 8,
        EveryThursday = 16,
        EveryFriday = 32,
        EverySaturday = 64,
        EverySunday = 128,
        EveryDay = 254,
        EveryWeek = 256,
        EveryMonth = 512,
        EveryYear = 1024,
    }

    public enum CampaignStatus
    {
        None,
        Planned,
        Processing,
        Archived,
    }

    [Flags]
    public enum CampaignTargetType
    {
        All = 0,
        ByTags = 1,
    }

    public partial class CampaignToken
    {
        public virtual string Key { get; set; }
        public virtual bool IsField { get; set; }
        public virtual string Value { get; set; }
    }

    public enum CampaignType
    {
        Manual = 1,
        OneTime = 2,
        Recurrent = 3,
    }

    public partial class Permission
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Value { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual Modules Module { get; set; }
    }

    public partial class Policy
    {
        public Policy()
        {
            Permissions = new List<Permission>{};
        }

        public virtual string Name { get; set; }
        public virtual OwnerType Type { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual List<Permission> Permissions { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual bool IsFullAccess { get; set; }
    }

    public partial class Role
    {
        public Role()
        {
            Policies = new List<Policy>{};
        }

        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual List<Policy> Policies { get; set; }
        public virtual OwnerType Type { get; set; }
        public virtual bool UserAbleModify { get; set; }
    }

    public partial class CredentialsSettings
    {
        public CredentialsSettings()
        {
            Modes = new List<CredentialsSettingsMode>{};
        }

        public virtual string LogoutUrl { get; set; }
        public virtual bool AllowUsernames { get; set; }
        public virtual List<CredentialsSettingsMode> Modes { get; set; }
    }

    public partial class CredentialsSettingsMode
    {
        public virtual string Name { get; set; }
        public virtual string LogoutUrl { get; set; }
    }

    public partial interface IOAuthModeSettings
    {
        string Name { get; set; }
        string CallbackUrl { get; set; }
        string LogoutUrl { get; set; }
        string FailureRedirectUrl { get; set; }
        string Role { get; set; }
        string Zone { get; set; }
        bool OverrideDefaultScopes { get; set; }
        List<string> Scopes { get; set; }
    }

    public partial interface IOAuthSettings
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string FailureRedirectUrl { get; set; }
        string CallbackUrl { get; set; }
        string LogoutUrl { get; set; }
        string Role { get; set; }
        string Zone { get; set; }
        string EncVersion { get; set; }
        List<string> Scopes { get; set; }
    }

    public partial class AzureActiveDirSettings
        : IOAuthSettings
    {
        public AzureActiveDirSettings()
        {
            Modes = new List<AzureActiveDirSettingsMode>{};
            Scopes = new List<string>{};
        }

        public virtual string TenantId { get; set; }
        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string ClientSecretEnding { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual string EncVersion { get; set; }
        public virtual List<AzureActiveDirSettingsMode> Modes { get; set; }
        public virtual List<string> Scopes { get; set; }
        public virtual string AppTenantId { get; set; }
    }

    public partial class AzureActiveDirSettingsMode
        : IOAuthModeSettings
    {
        public AzureActiveDirSettingsMode()
        {
            Scopes = new List<string>{};
        }

        public virtual string Name { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual bool OverrideDefaultScopes { get; set; }
        public virtual List<string> Scopes { get; set; }
    }

    public partial class AppleOAuthSettings
        : IOAuthSettings
    {
        public AppleOAuthSettings()
        {
            Modes = new List<AppleOAuthSettingsMode>{};
            Scopes = new List<string>{};
        }

        public virtual string TeamId { get; set; }
        public virtual string BundleId { get; set; }
        public virtual string KeyId { get; set; }
        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string ClientSecretEnding { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual string EncVersion { get; set; }
        public virtual List<AppleOAuthSettingsMode> Modes { get; set; }
        public virtual List<string> Scopes { get; set; }
    }

    public partial class AppleOAuthSettingsMode
        : IOAuthModeSettings
    {
        public AppleOAuthSettingsMode()
        {
            Scopes = new List<string>{};
        }

        public virtual string Name { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual bool OverrideDefaultScopes { get; set; }
        public virtual List<string> Scopes { get; set; }
    }

    public partial class FacebookSettings
        : IOAuthSettings
    {
        public FacebookSettings()
        {
            Scopes = new List<string>{};
            Modes = new List<FacebookSettingsMode>{};
        }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string ClientSecretEnding { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual string EncVersion { get; set; }
        public virtual List<string> Scopes { get; set; }
        [DataMember]
        public virtual List<FacebookSettingsMode> Modes { get; set; }
    }

    [DataContract(Name="FacebookSettingsMode")]
    public partial class FacebookSettingsMode
        : IOAuthModeSettings
    {
        public FacebookSettingsMode()
        {
            Scopes = new List<string>{};
        }

        [DataMember]
        public virtual string Name { get; set; }

        [DataMember]
        public virtual string CallbackUrl { get; set; }

        [DataMember]
        public virtual string LogoutUrl { get; set; }

        [DataMember]
        public virtual string FailureRedirectUrl { get; set; }

        [DataMember]
        public virtual string Role { get; set; }

        [DataMember]
        public virtual string Zone { get; set; }

        [DataMember]
        public virtual bool OverrideDefaultScopes { get; set; }

        [DataMember]
        public virtual List<string> Scopes { get; set; }
    }

    public partial class GoogleSettings
        : IOAuthSettings
    {
        public GoogleSettings()
        {
            Modes = new List<GoogleSettingsMode>{};
            Scopes = new List<string>{};
            ServiceAccounts = new List<GoogleSettingsServiceAccount>{};
        }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string ClientSecretEnding { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string RedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual string EncVersion { get; set; }
        public virtual List<GoogleSettingsMode> Modes { get; set; }
        public virtual List<string> Scopes { get; set; }
        public virtual List<GoogleSettingsServiceAccount> ServiceAccounts { get; set; }
    }

    public partial class GoogleSettingsMode
        : IOAuthModeSettings
    {
        public GoogleSettingsMode()
        {
            Scopes = new List<string>{};
        }

        public virtual string Name { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual bool OverrideDefaultScopes { get; set; }
        public virtual List<string> Scopes { get; set; }
    }

    public partial class GoogleSettingsServiceAccount
    {
        public GoogleSettingsServiceAccount()
        {
            Scopes = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ClientId { get; set; }
        public virtual string Email { get; set; }
        public virtual string SecretJsonKey { get; set; }
        public virtual string EncVersion { get; set; }
        public virtual string PrivateKeyEnding { get; set; }
        public virtual List<string> Scopes { get; set; }
    }

    public partial class TwitterSettings
        : IOAuthSettings
    {
        public TwitterSettings()
        {
            Scopes = new List<string>{};
            Modes = new List<TwitterSettingsMode>{};
        }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string ClientSecretEnding { get; set; }
        public virtual string CodeCallbackUrl { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual string EncVersion { get; set; }
        public virtual List<string> Scopes { get; set; }
        public virtual List<TwitterSettingsMode> Modes { get; set; }
    }

    public partial class TwitterSettingsMode
        : IOAuthModeSettings
    {
        public TwitterSettingsMode()
        {
            Scopes = new List<string>{};
        }

        public virtual string Name { get; set; }
        public virtual string CodeCallbackUrl { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual bool OverrideDefaultScopes { get; set; }
        public virtual List<string> Scopes { get; set; }
    }

    public partial class EmailPreferencesForMembership
    {
        public virtual EmailPreferencesForMembership.OnNewUserRegistration NewUserRegistration { get; set; }
        public virtual EmailPreferencesForMembership.OnNewUserRegistration NewUserVerification { get; set; }
        public virtual EmailPreferencesForMembership.OnNewUserRegistration PasswordReset { get; set; }
        public virtual EmailPreferencesForMembership.OnNewUserRegistration NewUserInvitation { get; set; }
        public virtual EmailPreferencesForMembership.OnNewUserRegistration UserDeactivation { get; set; }
        public partial class OnNewUserRegistration
        {
            public virtual bool SendEmail { get; set; }
            public virtual MessageTemplate Template { get; set; }
            public virtual string Callback { get; set; }
        }

    }

    public partial class PasswordComplexity
    {
        public virtual int? MinLength { get; set; }
        public virtual int? MaxLength { get; set; }
        public virtual int? MinNumbers { get; set; }
        public virtual int? MaxNumbers { get; set; }
        public virtual int? MinUpper { get; set; }
        public virtual int? MaxUpper { get; set; }
        public virtual int? MinLower { get; set; }
        public virtual int? MaxLower { get; set; }
        public virtual int? MinSpecial { get; set; }
        public virtual int? MaxSpecial { get; set; }
        public virtual string AllowedSpecial { get; set; }
    }

    public partial class EmailPreferenceTexts
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string UnsubAllTitle { get; set; }
        public virtual string UnsubAllDescription { get; set; }
        public virtual string UpdateButton { get; set; }
    }

    public partial class EmailProperties
    {
        public virtual EmailProvider EmailProvider { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string EmailDisplayName { get; set; }
        public virtual string Host { get; set; }
        public virtual int Port { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual bool UseSsl { get; set; }
        public virtual bool UseCredentials { get; set; }
        public virtual string Token { get; set; }
        public virtual string WebHookSigningKey { get; set; }
        public virtual string Domain { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string Region { get; set; }
        public virtual string ConfigurationSetName { get; set; }
    }

    public enum EmailTemplateType
    {
        Transactional,
        Marketing,
    }

    public partial class MessageTemplate
    {
        public MessageTemplate()
        {
            PreferenceTags = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual Guid? EmailAccountId { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual string Code { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual EmailTemplateType Type { get; set; }
        public virtual List<string> PreferenceTags { get; set; }
        public virtual bool IncludeSubscriptionLink { get; set; }
    }

    public partial class AndroidBackgroundLayout
    {
        public virtual string Image { get; set; }
        public virtual string HeadingColor { get; set; }
        public virtual string ContentColor { get; set; }
    }

    public partial class PushNotificationButtons
    {
        public virtual string Id { get; set; }
        public virtual string Text { get; set; }
        public virtual string Icon { get; set; }
    }

    public partial class PushAccountProperties
    {
        public virtual PushProvider PushProvider { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string SecretKey { get; set; }
    }

    public partial class PaymentAccountProperties
    {
        public virtual PaymentProvider Provider { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string ValidationToken { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual string WebHookKey { get; set; }
        public virtual string EndpointKey { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string AuthRedirectUrl { get; set; }
        public virtual string PaymentRedirectUrl { get; set; }
        public virtual string ReceiverName { get; set; }
        public virtual string ReceiverIban { get; set; }
    }

    public partial class PaymentPlanProperties
    {
        public PaymentPlanProperties()
        {
            Roles = new List<string>{};
            RolesAfterExpire = new List<string>{};
        }

        public virtual Guid PaymentAccountId { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string ProductId { get; set; }
        public virtual string PackageName { get; set; }
        public virtual string PricingId { get; set; }
        public virtual List<string> Roles { get; set; }
        public virtual List<string> RolesAfterExpire { get; set; }
    }

    public partial class PaymentMode
    {
        public virtual string Name { get; set; }
        public virtual bool IsDefault { get; set; }
        public virtual string AcceptUrl { get; set; }
        public virtual string CancelUrl { get; set; }
        public virtual string PayText { get; set; }
    }

    public partial class PaymentOrderSchema
    {
        public PaymentOrderSchema()
        {
            Collections = new List<PaymentProductCollection>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Currency { get; set; }
        public virtual string Zone { get; set; }
        public virtual string Cluster { get; set; }
        public virtual List<PaymentProductCollection> Collections { get; set; }
    }

    public partial class PaymentProductCollection
    {
        public virtual Guid SchemaId { get; set; }
        public virtual string PriceField { get; set; }
        public virtual string VariationField { get; set; }
        public virtual string Variation { get; set; }
    }

    public enum ServerlessProvider
    {
        None,
        CodemashAmazon,
        Amazon,
        Azure,
        Google,
    }

    public partial class ConnectionProperties
    {
        public ConnectionProperties()
        {
            Regions = new List<string>{};
            Tags = new Dictionary<string, string>{};
        }

        public virtual ServerlessProvider Provider { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual List<string> Regions { get; set; }
        public virtual int RefreshRate { get; set; }
        public virtual Dictionary<string, string> Tags { get; set; }
    }

    public partial class TokenResolverField
    {
        public virtual string Name { get; set; }
        public virtual string Config { get; set; }
    }

    public enum SchedulerRepeatType
    {
        None,
        Minutely,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
    }

    public enum SchedulerType
    {
        None,
        Notification,
        Custom,
    }

    public partial interface IAuthorizedModule
        : IModule
    {
    }

    public partial interface IModule
    {
        bool IsEnabled { get; set; }
        bool IsEstablished { get; set; }
        string Name { get; set; }
    }

    [Flags]
    public enum Modules
    {
        None = 0,
        Membership = 1,
        Database = 2,
        Email = 4,
        Marketing = 8,
        Logging = 16,
        Files = 32,
        Translation = 64,
        Notification = 128,
        Scheduler = 256,
        Serverless = 512,
        Payment = 1024,
        ServerEvents = 2048,
    }

    public partial class DisplayOption
    {
        public virtual string Key { get; set; }
        public virtual DisplayOption SubGroup { get; set; }
        public virtual string Group { get; set; }
        public virtual int GroupOrder { get; set; }
        public virtual bool WithCustomPolicyOnly { get; set; }
    }

    [Route("/notifications/push/accounts", "POST")]
    public partial class CreatePushAccount
        : CodeMashRequestBase, IReturn<CreatePushAccountResponse>
    {
        public virtual PushAccountProperties Model { get; set; }
    }

    public partial class CreatePushAccountResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/notifications/push/accounts/{Id}", "DELETE")]
    public partial class DeletePushAccount
        : CodeMashRequestBase, IReturn<DeletePushAccountResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeletePushAccountResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/push/accounts/{id}", "GET")]
    public partial class GetPushAccount
        : CodeMashRequestBase, IReturn<GetPushAccountResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetPushAccountResponse
        : ResponseBase<PushAccountDto>
    {
    }

    [Route("/notifications/push/accounts", "GET")]
    public partial class GetPushAccounts
        : CodeMashRequestBase, IReturn<GetPushAccountsResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetPushAccountsResponse
        : ResponseBase<List<PushAccountDto>>
    {
    }

    [Route("/notifications/push/accounts/{id}/default", "PUT")]
    public partial class SetPushAccountAsDefault
        : CodeMashRequestBase, IReturn<SetPushAccountAsDefaultResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class SetPushAccountAsDefaultResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/push/accounts/{id}", "PUT")]
    public partial class UpdatePushAccount
        : CodeMashRequestBase, IReturn<UpdatePushAccountResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual PushAccountProperties Model { get; set; }
    }

    public partial class UpdatePushAccountResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/payments/discounts/{id}", "GET")]
    public partial class GetPaymentDiscount
        : CodeMashRequestBase, IReturn<GetPaymentDiscountResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class GetPaymentDiscountResponse
        : ResponseBase<PaymentDiscountDto>
    {
    }

    [Route("/payments/discounts", "GET")]
    public partial class GetPaymentDiscounts
        : CodeMashListRequestBase, IReturn<GetPaymentDiscountsResponse>
    {
        public virtual string Cluster { get; set; }
    }

    public partial class GetPaymentDiscountsResponse
        : ResponseBase<List<PaymentDiscountListDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    ///<summary>
    ///Sign In
    ///</summary>
    [Route("/shared/forms/auth/aad", "GET POST")]
    [Api(Description="Sign In")]
    public partial class AadAuthentication
        : Authenticate, IReturn<AuthenticateResponse>
    {
    }

    ///<summary>
    ///Sign In
    ///</summary>
    [Route("/serverless/connections/aad/auth", "GET POST")]
    [Api(Description="Sign In")]
    public partial class AadConnectionAuthentication
        : Authenticate, IReturn<AuthenticateResponse>
    {
    }

    [Route("/serverless/system/functions/{id}", "POST")]
    public partial class AddSystemFunction
        : CodeMashRequestBase, IReturn<AddSystemFunctionResponse>
    {
        public AddSystemFunction()
        {
            Meta = new Dictionary<string, string>{};
            AvailableTokens = new List<string>{};
            TokenResolvers = new Dictionary<string, TokenResolverField>{};
            Tags = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual string Template { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Description { get; set; }
        public virtual int TimeoutMinutes { get; set; }
        public virtual int TimeoutSeconds { get; set; }
        public virtual long Memory { get; set; }
        public virtual List<string> AvailableTokens { get; set; }
        public virtual Dictionary<string, TokenResolverField> TokenResolvers { get; set; }
        public virtual List<string> Tags { get; set; }
    }

    public partial class AddSystemFunctionResponse
        : ResponseBase<string>
    {
    }

    [Route("/db/clusters/{id}/upgrade", "PUT")]
    public partial class AskCodeMashForClusterUpdate
        : CodeMashRequestBase, IReturn<AskCodeMashForClusterUpdateResponse>
    {
        public AskCodeMashForClusterUpdate()
        {
            MultiRegions = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string Cluster { get; set; }
        public virtual int StorageSize { get; set; }
        public virtual string DatabaseRegion { get; set; }
        public virtual bool AutoScaling { get; set; }
        public virtual bool EnableMultiRegion { get; set; }
        public virtual List<string> MultiRegions { get; set; }
        public virtual string Message { get; set; }
        public virtual DateTime? UpdateOn { get; set; }
    }

    public partial class AskCodeMashForClusterUpdateResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/clusters/get-free", "POST")]
    public partial class AskCodeMashForFreeCluster
        : CodeMashRequestBase, IReturn<AskCodeMashForFreeClusterResponse>
    {
    }

    public partial class AskCodeMashForFreeClusterResponse
        : ResponseBase<bool>
    {
    }

    [Route("/shared/forms/auth/credentials", "POST")]
    public partial class AuthCredentialsSharedForm
        : CodeMashFormRequest, IReturn<AuthCredentialsSharedFormResponse>
    {
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
    }

    public partial class AuthCredentialsSharedFormResponse
        : ResponseBase<string>
    {
    }

    [Route("/account/auth", "POST")]
    public partial class AuthenticateToAccount
        : IReturn<AuthenticateToAccountResponse>
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual Guid AccountId { get; set; }
    }

    public partial class AuthenticateToAccountResponse
        : ResponseBase<AuthenticateResponse>
    {
        public virtual string ApiLogin { get; set; }
    }

    [Route("/serverless/connections/azure/auth/state", "POST")]
    public partial class AzureConnectionAuthState
        : CodeMashRequestBase, IReturn<AzureConnectionAuthStateResponse>
    {
        public AzureConnectionAuthState()
        {
            SecretKeys = new Dictionary<string, string>{};
        }

        public virtual string SubscriptionId { get; set; }
        public virtual string ResourceGroup { get; set; }
        public virtual string ClientId { get; set; }
        public virtual Dictionary<string, string> SecretKeys { get; set; }
    }

    public partial class AzureConnectionAuthStateResponse
        : ResponseBase<string>
    {
    }

    [Route("/payments/accounts/{id}/validate", "POST")]
    public partial class BeginPaymentAccountValidation
        : CodeMashRequestBase, IReturn<BeginPaymentAccountValidationResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class BeginPaymentAccountValidationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/users/{Id}/block", "PUT")]
    public partial class BlockUser
        : RequestBase, IReturn<BlockUserResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class BlockUserResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/schemas/{collectionName}/rename-field/cancel", "PUT")]
    public partial class CancelRenameFieldUniqueName
        : CodeMashDbRequestBase, IReturn<CancelRenameFieldUniqueNameResponse>
    {
    }

    public partial class CancelRenameFieldUniqueNameResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/users/meta/rename-field/cancel", "PUT")]
    public partial class CancelRenameUserFieldUniqueName
        : CodeMashDbRequestBase, IReturn<CancelRenameUserFieldUniqueNameResponse>
    {
    }

    public partial class CancelRenameUserFieldUniqueNameResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/plan", "DELETE")]
    public partial class CancelSubscription
        : IReturn<CancelSubscriptionResponse>
    {
    }

    public partial class CancelSubscriptionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/taxonomies/{collectionName}/rename-field/cancel", "PUT")]
    public partial class CancelTaxonomyRenameFieldUniqueName
        : CodeMashDbRequestBase, IReturn<CancelTaxonomyRenameFieldUniqueNameResponse>
    {
    }

    public partial class CancelTaxonomyRenameFieldUniqueNameResponse
        : ResponseBase<bool>
    {
    }

    public partial class ChangeDatabaseStorageSize
        : CodeMashRequestBase, IReturn<ChangeDatabaseStorageSizeResponse>
    {
        public virtual double NewSize { get; set; }
    }

    public partial class ChangeDatabaseStorageSizeResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/users/password/reset/token", "GET")]
    public partial class CheckAccountPasswordReset
        : IReturn<CheckAccountPasswordResetResponse>
    {
        public virtual string Token { get; set; }
        public virtual bool IncludeAccounts { get; set; }
    }

    public partial class CheckAccountPasswordResetResponse
        : ResponseBase<bool>
    {
        public CheckAccountPasswordResetResponse()
        {
            Accounts = new List<PasswordResetAccountDto>{};
        }

        public virtual List<PasswordResetAccountDto> Accounts { get; set; }
    }

    [Route("/account/users/invitation/token", "GET")]
    public partial class CheckAccountUserInvitationToken
        : IReturn<CheckAccountUserInvitationTokenResponse>
    {
        public virtual string Token { get; set; }
        public virtual bool IncludeAccountDetails { get; set; }
    }

    public partial class CheckAccountUserInvitationTokenResponse
        : ResponseBase<bool>
    {
        public CheckAccountUserInvitationTokenResponse()
        {
            Projects = new List<string>{};
        }

        public virtual AccountListItemDto AccountDetails { get; set; }
        public virtual List<string> Projects { get; set; }
        public virtual Guid AccountId { get; set; }
        public virtual Guid UserId { get; set; }
    }

    [Route("/account/auth", "GET")]
    public partial class CheckAuth
        : IReturn<CheckAuthResponse>
    {
    }

    public partial class CheckAuthResponse
        : ResponseBase<AuthenticateResponse>
    {
    }

    [Route("/account/plan/check", "GET")]
    public partial class CheckIfCanChangeSubscription
        : IReturn<CheckIfCanChangeSubscriptionResponse>
    {
        public virtual SubscriptionPlan Plan { get; set; }
    }

    public partial class CheckIfCanChangeSubscriptionResponse
        : ResponseBase<bool>
    {
        public CheckIfCanChangeSubscriptionResponse()
        {
            PaymentErrors = new List<string>{};
            ProjectErrors = new List<ProjectPlanChangeCheckDto>{};
        }

        public virtual List<string> PaymentErrors { get; set; }
        public virtual List<ProjectPlanChangeCheckDto> ProjectErrors { get; set; }
    }

    [Route("/serverless/settings/cache/aad", "POST")]
    public partial class ClearAadFunctionsKeyCache
        : CodeMashRequestBase, IReturn<ClearAadFunctionsKeyCacheResponse>
    {
    }

    public partial class ClearAadFunctionsKeyCacheResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/schemas/{id}/clear", "POST")]
    public partial class ClearSchemaData
        : CodeMashRequestBase, IReturn<ClearSchemaDataResponse>
    {
        public ClearSchemaData()
        {
            Clusters = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual List<string> Clusters { get; set; }
    }

    public partial class ClearSchemaDataResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/taxonomies/{id}/clear", "POST")]
    public partial class ClearTaxonomyData
        : CodeMashDbRequestBase, IReturn<ClearTaxonomyDataResponse>
    {
        public ClearTaxonomyData()
        {
            Clusters = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual List<string> Clusters { get; set; }
    }

    public partial class ClearTaxonomyDataResponse
        : ResponseBase<bool>
    {
    }

    public partial class CodeMashFormRequest
        : RequestBase
    {
        public virtual string Token { get; set; }
    }

    [Route("/account/complete-register", "POST")]
    public partial class CompleteRegisterAuthentication
        : IReturn<CompleteRegisterAuthenticationResponse>
    {
        public virtual string SessionSecret { get; set; }
    }

    public partial class CompleteRegisterAuthenticationResponse
        : ResponseBase<bool>
    {
        public virtual bool NotReady { get; set; }
        public virtual string ApiLogin { get; set; }
    }

    ///<summary>
    ///Creates a new CodeMash account
    ///</summary>
    [Route("/account", "POST")]
    [Api(BodyParameter=2, Description="Creates a new CodeMash account")]
    public partial class CreateAccount
        : IReturn<CreateAccountResponse>
    {
        ///<summary>
        ///First name of the account holder
        ///</summary>
        [ApiMember(DataType="string", Description="First name of the account holder", Name="FirstName", ParameterType="form")]
        public virtual string FirstName { get; set; }

        ///<summary>
        ///Last name of account holder
        ///</summary>
        [ApiMember(DataType="string", Description="Last name of account holder", Name="LastName", ParameterType="form")]
        public virtual string LastName { get; set; }

        ///<summary>
        ///Real email of account holder
        ///</summary>
        [ApiMember(DataType="string", Description="Real email of account holder", IsRequired=true, Name="Email", ParameterType="form")]
        public virtual string Email { get; set; }

        ///<summary>
        ///Set password for a new account
        ///</summary>
        [ApiMember(DataType="string", Description="Set password for a new account", Format="password", IsRequired=true, Name="Password", ParameterType="form")]
        public virtual string Password { get; set; }
    }

    [Route("/account/users/password/reset/token", "POST")]
    public partial class CreateAccountPasswordReset
        : IReturn<CreateAccountPasswordResetResponse>
    {
        public virtual string Email { get; set; }
    }

    public partial class CreateAccountPasswordResetResponse
        : ResponseBase<bool>
    {
    }

    public partial class CreateAccountResponse
        : ResponseBase<Guid>
    {
        public virtual string Session { get; set; }
        public virtual string ApiLogin { get; set; }
    }

    [Route("/db/backups", "POST")]
    public partial class CreateBackup
        : CodeMashRequestBase, IReturn<CreateBackupResponse>
    {
        public virtual Guid? Cluster { get; set; }
    }

    public partial class CreateBackupResponse
        : ResponseBase<BackupListItemDto>
    {
    }

    [Route("/account/card/intent", "GET")]
    public partial class CreateCardIntent
        : IReturn<CreateCardIntentResponse>
    {
    }

    public partial class CreateCardIntentResponse
        : ResponseBase<string>
    {
    }

    [Route("/db/clusters", "POST")]
    public partial class CreateCluster
        : CodeMashRequestBase, IReturn<CreateClusterResponse>
    {
        public CreateCluster()
        {
            MultiRegions = new List<string>{};
        }

        public virtual string Provider { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual int StorageSize { get; set; }
        public virtual string DatabaseRegion { get; set; }
        public virtual string Cluster { get; set; }
        public virtual bool AutoScaling { get; set; }
        public virtual bool EnableMultiRegion { get; set; }
        public virtual List<string> MultiRegions { get; set; }
        public virtual string AtlasUserName { get; set; }
        public virtual string AtlasPassword { get; set; }
        public virtual string AtlasClusterId { get; set; }
        public virtual string AtlasClusterName { get; set; }
    }

    public partial class CreateClusterResponse
        : ResponseBase<ClusterDto>
    {
    }

    [Route("/db/{collectionName}/forms", "POST")]
    public partial class CreateCollectionForm
        : CodeMashDbRequestBase, IReturn<CreateCollectionFormResponse>
    {
        public CreateCollectionForm()
        {
            Authentications = new List<string>{};
        }

        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsPublic { get; set; }
        public virtual bool LimitOneResponse { get; set; }
        public virtual bool CanEdit { get; set; }
        public virtual string Cluster { get; set; }
        public virtual List<string> Authentications { get; set; }
    }

    public partial class CreateCollectionFormResponse
        : ResponseBase<Guid>
    {
        public virtual string Token { get; set; }
    }

    [Route("/db/triggers", "POST")]
    public partial class CreateCollectionTrigger
        : CodeMashDbRequestBase, IReturn<CreateCollectionTriggerResponse>
    {
        public virtual SchemaTriggerCreateDto Trigger { get; set; }
    }

    public partial class CreateCollectionTriggerResponse
        : ResponseBase<string>
    {
    }

    [Route("/serverless/custom/functions", "POST")]
    public partial class CreateCustomFunction
        : CodeMashRequestBase, IReturn<CreateCustomFunctionResponse>
    {
        public CreateCustomFunction()
        {
            Environment = new Dictionary<string, string>{};
            Tags = new List<string>{};
        }

        public virtual string FileId { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Runtime { get; set; }
        public virtual string Handler { get; set; }
        public virtual string Description { get; set; }
        public virtual int TimeoutMinutes { get; set; }
        public virtual int TimeoutSeconds { get; set; }
        public virtual long Memory { get; set; }
        public virtual Dictionary<string, string> Environment { get; set; }
        public virtual string Template { get; set; }
        public virtual List<string> Tags { get; set; }
        public virtual string ServiceAccount { get; set; }
    }

    [Route("/serverless/custom/functions/{id}/aliases", "POST")]
    public partial class CreateCustomFunctionAlias
        : CodeMashRequestBase, IReturn<CreateCustomFunctionAliasResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Version { get; set; }
        public virtual string AdditionalVersion { get; set; }
        public virtual int AdditionalVersionWeight { get; set; }
    }

    public partial class CreateCustomFunctionAliasResponse
        : ResponseBase<bool>
    {
    }

    public partial class CreateCustomFunctionResponse
        : ResponseBase<string>
    {
    }

    [Route("/serverless/custom/functions/{id}/versions", "POST")]
    public partial class CreateCustomFunctionVersion
        : CodeMashRequestBase, IReturn<CreateCustomFunctionVersionResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual string Description { get; set; }
    }

    public partial class CreateCustomFunctionVersionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/email/accounts", "POST")]
    public partial class CreateEmailAccount
        : CodeMashRequestBase, IReturn<CreateEmailAccountResponse>
    {
        public CreateEmailAccount()
        {
            Signatures = new Dictionary<string, string>{};
            SubscriptionLinks = new Dictionary<string, string>{};
        }

        public virtual EmailProperties Model { get; set; }
        public virtual Dictionary<string, string> Signatures { get; set; }
        public virtual Dictionary<string, string> SubscriptionLinks { get; set; }
    }

    public partial class CreateEmailAccountResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/files/accounts", "POST")]
    public partial class CreateFileAccount
        : CodeMashRequestBase, IReturn<CreateFileAccountResponse>
    {
        public virtual FileAccountProperties Model { get; set; }
    }

    public partial class CreateFileAccountResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/files/triggers", "POST")]
    public partial class CreateFilesTrigger
        : CodeMashRequestBase, IReturn<CreateFilesTriggerResponse>
    {
        public virtual FilesTriggerCreateDto Trigger { get; set; }
    }

    public partial class CreateFilesTriggerResponse
        : ResponseBase<string>
    {
    }

    [Route("/files/folder", "POST")]
    public partial class CreateFolder
        : CodeMashRequestBase, IReturn<CreateFolderResponse>
    {
        public virtual Guid? FileAccountId { get; set; }
        public virtual string Path { get; set; }
        public virtual string FolderName { get; set; }
    }

    public partial class CreateFolderResponse
        : ResponseBase<BrowseObjectDto>
    {
    }

    [Route("/db/{collectionName}/indexes", "POST")]
    public partial class CreateIndex
        : CodeMashDbRequestBase, IReturn<CreateIndexResponse>
    {
        public CreateIndex()
        {
            Key = new Dictionary<string, Object>{};
        }

        public virtual Dictionary<string, Object> Key { get; set; }
        public virtual string Options { get; set; }
    }

    public partial class CreateIndexResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/notifications/email/templates", "POST")]
    public partial class CreateMessageTemplate
        : CodeMashRequestBase, IReturn<CreateMessageTemplateResponse>
    {
        public CreateMessageTemplate()
        {
            StaticAttachments = new List<string>{};
            Tags = new List<string>{};
        }

        public virtual string TemplateName { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual string Code { get; set; }
        public virtual List<string> StaticAttachments { get; set; }
        public virtual Guid? EmailAccountId { get; set; }
        public virtual EmailTemplateType Type { get; set; }
        public virtual List<string> Tags { get; set; }
        public virtual bool IncludeSubscriptionLink { get; set; }
    }

    public partial class CreateMessageTemplateResponse
        : ResponseBase<Nullable<Guid>>
    {
        public virtual string SubjectError { get; set; }
        public virtual string BodyError { get; set; }
    }

    [Route("/notifications/push/templates", "POST")]
    public partial class CreateNotificationTemplate
        : CodeMashRequestBase, IReturn<CreateNotificationTemplateResponse>
    {
        public CreateNotificationTemplate()
        {
            Meta = new Dictionary<string, string>{};
            Buttons = new List<PushNotificationButtons>{};
        }

        public virtual string TemplateName { get; set; }
        public virtual NotificationPriority Priority { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual string Data { get; set; }
        public virtual int? Ttl { get; set; }
        public virtual string Url { get; set; }
        public virtual string Code { get; set; }
        public virtual string CollapseId { get; set; }
        public virtual string Image { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual List<PushNotificationButtons> Buttons { get; set; }
        public virtual Guid? AccountId { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual int? IosBadge { get; set; }
        public virtual string IosCategory { get; set; }
        public virtual string IosSound { get; set; }
        public virtual bool IosContentAvailable { get; set; }
        public virtual string IosAnalyticsLabel { get; set; }
        public virtual string IosAppBundleId { get; set; }
        public virtual string IosGroupId { get; set; }
        public virtual string IosPushType { get; set; }
        public virtual string IosLaunchImage { get; set; }
        public virtual string AndroidChannelId { get; set; }
        public virtual string AndroidGroup { get; set; }
        public virtual string AndroidGroupMessage { get; set; }
        public virtual string RestrictedPackageName { get; set; }
        public virtual string AndroidSound { get; set; }
        public virtual string AndroidVisibility { get; set; }
        public virtual string AndroidLedColor { get; set; }
        public virtual string AndroidLightOnDuration { get; set; }
        public virtual string AndroidLightOffDuration { get; set; }
        public virtual string AndroidAccentColor { get; set; }
        public virtual bool AndroidSticky { get; set; }
        public virtual bool AndroidDefaultLight { get; set; }
        public virtual bool AndroidDefaultVibration { get; set; }
        public virtual string AndroidAnalyticsLabel { get; set; }
        public virtual string AndroidVibrateTimings { get; set; }
        public virtual string AndroidSmallIcon { get; set; }
        public virtual string AndroidLargeIcon { get; set; }
        public virtual AndroidBackgroundLayout AndroidBackgroundLayout { get; set; }
    }

    public partial class CreateNotificationTemplateResponse
        : ResponseBase<Nullable<Guid>>
    {
        public virtual string TitleError { get; set; }
        public virtual string BodyError { get; set; }
        public virtual string SubtitleError { get; set; }
        public virtual string DataError { get; set; }
    }

    [Route("/serverless/connections/amazon", "POST")]
    public partial class CreateOrUpdateAmazonServerlessConnection
        : CodeMashRequestBase, IReturn<CreateOrUpdateAmazonServerlessConnectionResponse>
    {
        public CreateOrUpdateAmazonServerlessConnection()
        {
            Regions = new List<string>{};
            Tags = new Dictionary<string, string>{};
        }

        public virtual string AccessKey { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual string NewSecretKey { get; set; }
        public virtual List<string> Regions { get; set; }
        public virtual int RefreshRate { get; set; }
        public virtual Dictionary<string, string> Tags { get; set; }
    }

    public partial class CreateOrUpdateAmazonServerlessConnectionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/serverless/connections/azure", "POST")]
    public partial class CreateOrUpdateAzureServerlessConnection
        : IReturn<CreateOrUpdateAzureServerlessConnectionResponse>
    {
        public virtual string AccessToken { get; set; }
        public virtual string State { get; set; }
    }

    public partial class CreateOrUpdateAzureServerlessConnectionResponse
        : ResponseBase<bool>
    {
        public virtual string ValidationError { get; set; }
        public virtual string RedirectUri { get; set; }
    }

    [Route("/notifications/email/settings/tags", "POST")]
    public partial class CreateOrUpdateEmailTag
        : CodeMashRequestBase, IReturn<CreateOrUpdateEmailTagResponse>
    {
        public CreateOrUpdateEmailTag()
        {
            Titles = new Dictionary<string, string>{};
            Descriptions = new Dictionary<string, string>{};
        }

        public virtual string Identifier { get; set; }
        public virtual Dictionary<string, string> Titles { get; set; }
        public virtual Dictionary<string, string> Descriptions { get; set; }
    }

    public partial class CreateOrUpdateEmailTagResponse
        : ResponseBase<string>
    {
    }

    [Route("/serverless/connections/google", "POST")]
    public partial class CreateOrUpdateGoogleServerlessConnection
        : CodeMashRequestBase, IReturn<CreateOrUpdateGoogleServerlessConnectionResponse>
    {
        public CreateOrUpdateGoogleServerlessConnection()
        {
            Regions = new List<string>{};
            Tags = new Dictionary<string, string>{};
        }

        public virtual string SecretKey { get; set; }
        public virtual List<string> Regions { get; set; }
        public virtual int RefreshRate { get; set; }
        public virtual Dictionary<string, string> Tags { get; set; }
    }

    public partial class CreateOrUpdateGoogleServerlessConnectionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/settings/modes", "POST")]
    public partial class CreateOrUpdatePaymentMode
        : CodeMashRequestBase, IReturn<CreateOrUpdatePaymentModeResponse>
    {
        public virtual string Name { get; set; }
        public virtual string AcceptUrl { get; set; }
        public virtual string CancelUrl { get; set; }
        public virtual string PayText { get; set; }
        public virtual bool IsDefault { get; set; }
    }

    public partial class CreateOrUpdatePaymentModeResponse
        : ResponseBase<string>
    {
    }

    [Route("/payments/settings/product-collections", "POST")]
    public partial class CreateOrUpdatePaymentProductCollection
        : CodeMashRequestBase, IReturn<CreateOrUpdatePaymentProductCollectionResponse>
    {
        public CreateOrUpdatePaymentProductCollection()
        {
            Collections = new List<PaymentProductCollection>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Currency { get; set; }
        public virtual string Zone { get; set; }
        public virtual string Cluster { get; set; }
        public virtual List<PaymentProductCollection> Collections { get; set; }
    }

    public partial class CreateOrUpdatePaymentProductCollectionResponse
        : ResponseBase<string>
    {
    }

    [Route("/payments/accounts", "POST")]
    public partial class CreatePaymentAccount
        : CodeMashRequestBase, IReturn<CreatePaymentAccountResponse>
    {
        public virtual PaymentAccountProperties Model { get; set; }
    }

    public partial class CreatePaymentAccountResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/payments/plans", "POST")]
    public partial class CreatePaymentPlan
        : CodeMashRequestBase, IReturn<CreatePaymentPlanResponse>
    {
        public virtual PaymentPlanProperties Model { get; set; }
    }

    public partial class CreatePaymentPlanResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/payments/triggers", "POST")]
    public partial class CreatePaymentTrigger
        : CodeMashRequestBase, IReturn<CreatePaymentTriggerResponse>
    {
        public virtual PaymentTriggerCreateDto Trigger { get; set; }
    }

    public partial class CreatePaymentTriggerResponse
        : ResponseBase<string>
    {
    }

    [Route("/projects", "POST")]
    public partial class CreateProject
        : IReturn<CreateProjectResponse>
    {
        public virtual string ProjectName { get; set; }
        public virtual string ZoneName { get; set; }
        public virtual string Description { get; set; }
    }

    public partial class CreateProjectResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/project/tokens", "POST")]
    public partial class CreateProjectToken
        : CodeMashRequestBase, IReturn<CreateProjectTokenResponse>
    {
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
    }

    public partial class CreateProjectTokenResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/roles", "POST")]
    public partial class CreateRole
        : CodeMashRequestBase, IReturn<CreateRoleResponse>
    {
        public CreateRole()
        {
            Policies = new List<string>{};
        }

        public virtual string Name { get; set; }
        public virtual List<string> Policies { get; set; }
    }

    public partial class CreateRoleResponse
        : ResponseBase<string>
    {
    }

    [Route("/db/schema", "POST")]
    public partial class CreateSchema
        : CodeMashDbRequestBase, IReturn<CreateSchemaResponse>
    {
        public virtual string UiSchema { get; set; }
        public virtual string JsonSchema { get; set; }
        public virtual string MongoDbSchema { get; set; }
    }

    [Route("/db/aggregates", "POST")]
    public partial class CreateSchemaAggregate
        : CodeMashRequestBase, IReturn<CreateSchemaAggregateResponse>
    {
        public virtual Guid SchemaId { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Query { get; set; }
    }

    public partial class CreateSchemaAggregateResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/db/exports", "POST")]
    public partial class CreateSchemaExport
        : CodeMashRequestBase, IReturn<CreateSchemaExportResponse>
    {
        public CreateSchemaExport()
        {
            Fields = new List<string>{};
        }

        public virtual string CollectionName { get; set; }
        public virtual Guid? SchemaId { get; set; }
        public virtual Guid? FunctionId { get; set; }
        public virtual string Cluster { get; set; }
        public virtual Guid? FileAccountId { get; set; }
        public virtual List<string> Fields { get; set; }
        public virtual string Filter { get; set; }
        public virtual string Sort { get; set; }
        public virtual string Delimiter { get; set; }
        public virtual ExportFileTypes OutputType { get; set; }
        public virtual int? FromPage { get; set; }
        public virtual int? ToPage { get; set; }
        public virtual int? PageSize { get; set; }
        public virtual string Language { get; set; }
        public virtual bool IncludeReferencedNames { get; set; }
    }

    public partial class CreateSchemaExportResponse
        : ResponseBase<string>
    {
    }

    [Route("/db/schemas/system-templates/{id}", "POST")]
    public partial class CreateSchemaFromTemplate
        : CodeMashDbRequestBase, IReturn<CreateSchemaFromTemplateResponse>
    {
        public CreateSchemaFromTemplate()
        {
            Mapping = new Dictionary<string, string>{};
            Names = new Dictionary<string, string>{};
            DisplayFields = new Dictionary<string, string>{};
            CustomDisplayFields = new Dictionary<string, string>{};
            Created = new List<string>{};
            Excluded = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual Dictionary<string, string> Mapping { get; set; }
        public virtual Dictionary<string, string> Names { get; set; }
        public virtual Dictionary<string, string> DisplayFields { get; set; }
        public virtual Dictionary<string, string> CustomDisplayFields { get; set; }
        public virtual List<string> Created { get; set; }
        public virtual List<string> Excluded { get; set; }
    }

    public partial class CreateSchemaFromTemplateResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/imports", "POST")]
    public partial class CreateSchemaImport
        : CodeMashRequestBase, IReturn<CreateSchemaImportResponse>
    {
        public CreateSchemaImport()
        {
            Columns = new Dictionary<int, string>{};
            Resolvers = new Dictionary<int, string>{};
            ColumnOptions = new Dictionary<int, string>{};
        }

        public virtual Guid SchemaId { get; set; }
        public virtual string Cluster { get; set; }
        public virtual Guid? FileAccountId { get; set; }
        public virtual Dictionary<int, string> Columns { get; set; }
        public virtual Dictionary<int, string> Resolvers { get; set; }
        public virtual Dictionary<int, string> ColumnOptions { get; set; }
        public virtual string Delimiter { get; set; }
        public virtual string FileId { get; set; }
        public virtual bool HasHeader { get; set; }
        public virtual bool ReplaceMatches { get; set; }
        public virtual ImportFileTypes InputType { get; set; }
    }

    public partial class CreateSchemaImportResponse
        : ResponseBase<string>
    {
    }

    public partial class CreateSchemaResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/db/taxonomy", "POST")]
    public partial class CreateTaxonomy
        : CodeMashRequestBase, IReturn<CreateTaxonomyResponse>
    {
        public CreateTaxonomy()
        {
            Dependencies = new List<string>{};
        }

        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string JsonTermsMetaSchema { get; set; }
        public virtual string UiTermsMetaSchema { get; set; }
        public virtual List<string> Dependencies { get; set; }
        public virtual string Parent { get; set; }
    }

    public partial class CreateTaxonomyResponse
        : ResponseBase<Guid>
    {
        public virtual string TaxonomyName { get; set; }
    }

    [Route("/db/taxonomies/{collectionName}", "POST")]
    public partial class CreateTerm
        : CodeMashDbRequestBase, IReturn<InsertOneResponse>
    {
        public CreateTerm()
        {
            Dependencies = new Dictionary<string, List<String>>{};
        }

        public virtual string Document { get; set; }
        public virtual string Meta { get; set; }
        public virtual Dictionary<string, List<String>> Dependencies { get; set; }
        public virtual string Parent { get; set; }
    }

    [Route("/membership/triggers", "POST")]
    public partial class CreateUsersTrigger
        : CodeMashRequestBase, IReturn<CreateUsersTriggerResponse>
    {
        public virtual UsersTriggerCreateDto Trigger { get; set; }
    }

    public partial class CreateUsersTriggerResponse
        : ResponseBase<string>
    {
    }

    [Route("/payments/orders/test", "DELETE")]
    public partial class DeleteAllTestOrders
        : CodeMashRequestBase, IReturn<DeleteAllTestOrdersResponse>
    {
    }

    public partial class DeleteAllTestOrdersResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/backups/{id}", "DELETE")]
    public partial class DeleteBackup
        : CodeMashRequestBase, IReturn<DeleteBackupResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class DeleteBackupResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/clusters/{id}", "DELETE")]
    public partial class DeleteCluster
        : CodeMashRequestBase, IReturn<DeleteClusterResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteClusterResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/triggers/{id}", "DELETE")]
    public partial class DeleteCollectionTrigger
        : CodeMashDbRequestBase, IReturn<DeleteCollectionTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteCollectionTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/serverless/custom/functions/{id}/aliases/{name}", "DELETE")]
    public partial class DeleteCustomFunctionAlias
        : CodeMashRequestBase, IReturn<DeleteCustomFunctionAliasResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
    }

    public partial class DeleteCustomFunctionAliasResponse
        : ResponseBase<bool>
    {
    }

    [Route("/serverless/custom/functions/{id}/versions/{version}", "DELETE")]
    public partial class DeleteCustomFunctionVersion
        : CodeMashRequestBase, IReturn<DeleteCustomFunctionVersionResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual string Version { get; set; }
    }

    public partial class DeleteCustomFunctionVersionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/discounts", "DELETE")]
    public partial class DeleteDiscount
        : IReturn<DeleteDiscountResponse>
    {
        public virtual string Code { get; set; }
    }

    public partial class DeleteDiscountResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/email/accounts/{Id}", "DELETE")]
    public partial class DeleteEmailAccount
        : CodeMashRequestBase, IReturn<DeleteEmailAccountResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteEmailAccountResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Deletes emails group from queue
    ///</summary>
    [Route("/notifications/email/groups/{id}", "DELETE")]
    [Api(Description="Deletes emails group from queue")]
    [DataContract]
    public partial class DeleteEmailGroupRequest
        : CodeMashRequestBase, IReturn<DeleteEmailGroupResponse>
    {
        ///<summary>
        ///Email group Id
        ///</summary>
        [DataMember]
        [ApiMember(DataType="string", Description="Email group Id", IsRequired=true, Name="Id", ParameterType="path")]
        public virtual string Id { get; set; }
    }

    public partial class DeleteEmailGroupResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/email/settings/tags", "DELETE")]
    public partial class DeleteEmailTag
        : CodeMashRequestBase, IReturn<DeleteEmailTagResponse>
    {
        public virtual string Identifier { get; set; }
    }

    public partial class DeleteEmailTagResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/email/templates/{Id}", "DELETE")]
    public partial class DeleteEmailTemplate
        : CodeMashRequestBase, IReturn<DeleteEmailTemplateResponse>
    {
        public virtual Guid Id { get; set; }
    }

    [Route("/notifications/email/templates/{Id}/contents", "DELETE")]
    public partial class DeleteEmailTemplateContent
        : CodeMashRequestBase, IReturn<DeleteEmailTemplateContentResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteEmailTemplateContentResponse
        : ResponseBase<bool>
    {
    }

    public partial class DeleteEmailTemplateResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/accounts/{Id}", "DELETE")]
    public partial class DeleteFileAccount
        : CodeMashRequestBase, IReturn<DeleteFileAccountResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteFileAccountResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/triggers/{id}", "DELETE")]
    public partial class DeleteFilesTrigger
        : CodeMashRequestBase, IReturn<DeleteFilesTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteFilesTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/folder/{FolderId}", "DELETE")]
    public partial class DeleteFolder
        : CodeMashRequestBase, IReturn<DeleteFolderResponse>
    {
        public virtual Guid? FileAccountId { get; set; }
        public virtual string Path { get; set; }
        public virtual Guid FolderId { get; set; }
    }

    public partial class DeleteFolderResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/{collectionName}/indexes/{id}", "DELETE")]
    public partial class DeleteIndex
        : CodeMashDbRequestBase, IReturn<DeleteIndexResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteIndexResponse
        : ResponseBase<bool>
    {
    }

    [Route("/logging/logs", "DELETE")]
    public partial class DeleteLogs
        : CodeMashRequestBase, IReturn<DeleteLogsResponse>
    {
        public DeleteLogs()
        {
            Ids = new List<string>{};
        }

        public virtual string ClearType { get; set; }
        [DataMember(Name="ids[]")]
        public virtual List<string> Ids { get; set; }
    }

    public partial class DeleteLogsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/customers/bulk", "DELETE")]
    public partial class DeleteManyCustomers
        : CodeMashRequestBase, IReturn<DeleteManyCustomersResponse>
    {
        public DeleteManyCustomers()
        {
            Ids = new List<string>{};
        }

        [DataMember(Name="ids[]")]
        public virtual List<string> Ids { get; set; }
    }

    public partial class DeleteManyCustomersResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/discounts/bulk", "DELETE")]
    public partial class DeleteManyDiscounts
        : CodeMashRequestBase, IReturn<DeleteManyDiscountsResponse>
    {
        public DeleteManyDiscounts()
        {
            Ids = new List<string>{};
        }

        [DataMember(Name="ids[]")]
        public virtual List<string> Ids { get; set; }
    }

    public partial class DeleteManyDiscountsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/bulk", "DELETE")]
    public partial class DeleteManyFiles
        : CodeMashRequestBase, IReturn<DeleteManyFilesResponse>
    {
        public DeleteManyFiles()
        {
            Ids = new List<string>{};
        }

        [DataMember(Name="ids[]")]
        public virtual List<string> Ids { get; set; }
    }

    public partial class DeleteManyFilesResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/push/groups/bulk", "DELETE")]
    public partial class DeleteManyNotificationGroupsRequest
        : CodeMashRequestBase, IReturn<DeleteManyNotificationGroupsResponse>
    {
        public DeleteManyNotificationGroupsRequest()
        {
            Ids = new List<string>{};
        }

        [DataMember(Name="ids[]")]
        public virtual List<string> Ids { get; set; }
    }

    public partial class DeleteManyNotificationGroupsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/orders/bulk", "DELETE")]
    public partial class DeleteManyOrders
        : CodeMashRequestBase, IReturn<DeleteManyOrdersResponse>
    {
        public DeleteManyOrders()
        {
            Ids = new List<string>{};
        }

        [DataMember(Name="ids[]")]
        public virtual List<string> Ids { get; set; }
    }

    public partial class DeleteManyOrdersResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/taxonomies/{CollectionName}/terms/bulk", "DELETE")]
    public partial class DeleteManyTermsRequest
        : CodeMashDbRequestBase, IReturn<DeleteManyTermsResponse>
    {
        public virtual string Filter { get; set; }
    }

    public partial class DeleteManyTermsResponse
        : ResponseBase<DeleteResult>
    {
    }

    [Route("/notifications/push/groups/{id}", "DELETE")]
    public partial class DeleteNotificationGroupRequest
        : CodeMashRequestBase, IReturn<DeleteNotificationGroupResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class DeleteNotificationGroupResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/push/templates/{id}", "DELETE")]
    public partial class DeleteNotificationTemplate
        : CodeMashRequestBase, IReturn<DeleteNotificationTemplateResponse>
    {
        public virtual Guid Id { get; set; }
    }

    [Route("/notifications/push/templates/{id}/contents", "DELETE")]
    public partial class DeleteNotificationTemplateContent
        : CodeMashRequestBase, IReturn<DeleteNotificationTemplateContentResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteNotificationTemplateContentResponse
        : ResponseBase<bool>
    {
    }

    public partial class DeleteNotificationTemplateResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/orders/{Id}", "DELETE")]
    public partial class DeleteOrder
        : CodeMashRequestBase, IReturn<DeleteOrderResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class DeleteOrderResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/accounts/{Id}", "DELETE")]
    public partial class DeletePaymentAccount
        : CodeMashRequestBase, IReturn<DeletePaymentAccountResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeletePaymentAccountResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/settings/modes", "DELETE")]
    public partial class DeletePaymentMode
        : CodeMashRequestBase, IReturn<DeletePaymentModeResponse>
    {
        public virtual string Name { get; set; }
    }

    public partial class DeletePaymentModeResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/plans/{Id}", "DELETE")]
    public partial class DeletePaymentPlan
        : CodeMashRequestBase, IReturn<DeletePaymentPlanResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeletePaymentPlanResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/settings/product-collections", "DELETE")]
    public partial class DeletePaymentProductCollection
        : CodeMashRequestBase, IReturn<DeletePaymentProductCollectionResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeletePaymentProductCollectionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/triggers/{id}", "DELETE")]
    public partial class DeletePaymentTrigger
        : CodeMashRequestBase, IReturn<DeletePaymentTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeletePaymentTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/{id}", "DELETE")]
    public partial class DeleteProject
        : IReturn<DeleteProjectResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual bool Confirmed { get; set; }
    }

    [Route("/project/logo", "DELETE")]
    public partial class DeleteProjectLogo
        : CodeMashRequestBase, IReturn<DeleteProjectLogoResponse>
    {
    }

    public partial class DeleteProjectLogoResponse
        : ResponseBase<bool>
    {
    }

    public partial class DeleteProjectResponse
        : ResponseBase<bool>
    {
    }

    [Route("/project/tokens", "DELETE")]
    public partial class DeleteProjectToken
        : CodeMashRequestBase, IReturn<DeleteProjectTokenResponse>
    {
        public virtual string Key { get; set; }
    }

    public partial class DeleteProjectTokenResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/roles/{name}", "DELETE")]
    public partial class DeleteRole
        : CodeMashRequestBase, IReturn<DeleteRoleResponse>
    {
        public virtual string Name { get; set; }
    }

    public partial class DeleteRoleResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/aggregates/{id}", "DELETE")]
    public partial class DeleteSchemaAggregate
        : CodeMashRequestBase, IReturn<DeleteSchemaAggregateResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteSchemaAggregateResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/exports/{exportId}", "DELETE")]
    public partial class DeleteSchemaExport
        : CodeMashRequestBase, IReturn<DeleteSchemaExportResponse>
    {
        public virtual string ExportId { get; set; }
    }

    public partial class DeleteSchemaExportResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/imports/{importId}", "DELETE")]
    public partial class DeleteSchemaImport
        : CodeMashRequestBase, IReturn<DeleteSchemaImportResponse>
    {
        public virtual string ImportId { get; set; }
    }

    public partial class DeleteSchemaImportResponse
        : ResponseBase<bool>
    {
    }

    [Route("/serverless/connections/{provider}", "DELETE")]
    public partial class DeleteServerlessConnection
        : CodeMashRequestBase, IReturn<DeleteServerlessConnectionResponse>
    {
        public virtual ServerlessProvider Provider { get; set; }
    }

    public partial class DeleteServerlessConnectionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/taxonomies/{id}", "DELETE")]
    public partial class DeleteTaxonomy
        : CodeMashRequestBase, IReturn<DeleteTaxonomyResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteTaxonomyResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/taxonomies/{collectionName}/terms/{termId}", "DELETE")]
    public partial class DeleteTerm
        : CodeMashDbRequestBase, IReturn<InsertOneResponse>
    {
        public virtual string TermId { get; set; }
    }

    [Route("/account/users/{Id}", "DELETE")]
    public partial class DeleteUser
        : RequestBase, IReturn<DeleteUserResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteUserResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/triggers/{id}", "DELETE")]
    public partial class DeleteUsersTrigger
        : CodeMashRequestBase, IReturn<DeleteUsersTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteUsersTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/backups/disable", "POST")]
    public partial class DisableBackups
        : CodeMashRequestBase, IReturn<DisableBackupsResponse>
    {
        public virtual Guid? Cluster { get; set; }
    }

    public partial class DisableBackupsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/triggers/{id}/disable", "PUT")]
    public partial class DisableCollectionTrigger
        : CodeMashDbRequestBase, IReturn<DisableCollectionTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DisableCollectionTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/disable", "GET")]
    public partial class DisableDatabase
        : CodeMashRequestBase, IReturn<DisableDatabaseResponse>
    {
        public virtual string Password { get; set; }
    }

    public partial class DisableDatabaseResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/email/disable", "GET")]
    public partial class DisableEmail
        : CodeMashRequestBase, IReturn<DisableEmailResponse>
    {
    }

    public partial class DisableEmailResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/disable", "GET")]
    public partial class DisableFiles
        : CodeMashRequestBase, IReturn<DisableFilesResponse>
    {
    }

    public partial class DisableFilesResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/triggers/{id}/disable", "PUT")]
    public partial class DisableFilesTrigger
        : CodeMashRequestBase, IReturn<DisableFilesTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DisableFilesTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/logging/disable", "GET")]
    public partial class DisableLogging
        : CodeMashRequestBase, IReturn<DisableLoggingResponse>
    {
    }

    public partial class DisableLoggingResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/disable", "GET")]
    public partial class DisablePayments
        : CodeMashRequestBase, IReturn<DisablePaymentsResponse>
    {
    }

    public partial class DisablePaymentsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/triggers/{id}/disable", "PUT")]
    public partial class DisablePaymentTrigger
        : CodeMashRequestBase, IReturn<DisablePaymentTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DisablePaymentTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/server-events/disable", "GET")]
    public partial class DisableServerEvents
        : CodeMashRequestBase, IReturn<DisableServerEventsResponse>
    {
    }

    public partial class DisableServerEventsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/serverless/disable", "GET")]
    public partial class DisableServerless
        : CodeMashRequestBase, IReturn<DisableServerlessResponse>
    {
    }

    public partial class DisableServerlessResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/triggers/{id}/disable", "PUT")]
    public partial class DisableUsersTrigger
        : CodeMashRequestBase, IReturn<DisableUsersTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DisableUsersTriggerResponse
        : ResponseBase<bool>
    {
    }

    public partial class DownloadBackup
        : CodeMashRequestBase, IReturn<HttpResult>
    {
        public virtual string Id { get; set; }
    }

    [Route("/shared/forms/files", "GET")]
    public partial class DownloadFormFile
        : CodeMashFormRequest, IReturn<HttpResult>
    {
        public virtual string FileId { get; set; }
    }

    [Route("/shared/forms/files/url", "GET")]
    public partial class DownloadFormFileUrl
        : CodeMashFormRequest, IReturn<DownloadFormFileUrlResponse>
    {
        public virtual string FileId { get; set; }
    }

    public partial class DownloadFormFileUrlResponse
        : ResponseBase<string>
    {
    }

    [Route("/account/invoices/{invoiceId}/download", "GET")]
    public partial class DownloadInvoice
        : IReturn<HttpResult>
    {
        public virtual string InvoiceId { get; set; }
    }

    [Route("/account/invoices/download/token", "GET")]
    public partial class DownloadInvoiceFromUrl
        : IReturn<HttpResult>
    {
        public virtual string Token { get; set; }
    }

    [Route("/db/schemas/{id}/duplicate", "POST")]
    public partial class DuplicateSchema
        : CodeMashDbRequestBase, IReturn<DuplicateSchemaResponse>
    {
        public DuplicateSchema()
        {
            Clusters = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string NewCollectionName { get; set; }
        public virtual bool IncludeTriggers { get; set; }
        public virtual bool IncludeIndexes { get; set; }
        public virtual bool IncludeRecords { get; set; }
        public virtual List<string> Clusters { get; set; }
        public virtual bool IncludeFiles { get; set; }
    }

    public partial class DuplicateSchemaResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/notifications/email/accounts", "PUT")]
    public partial class EditEmailAccount
        : CodeMashRequestBase, IReturn<EditEmailAccountResponse>
    {
        public EditEmailAccount()
        {
            Signatures = new Dictionary<string, string>{};
            SubscriptionLinks = new Dictionary<string, string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual EmailProperties Model { get; set; }
        public virtual Dictionary<string, string> Signatures { get; set; }
        public virtual Dictionary<string, string> SubscriptionLinks { get; set; }
    }

    public partial class EditEmailAccountResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/email/accounts/{Id}/token", "PUT")]
    public partial class EditEmailAccountToken
        : CodeMashRequestBase, IReturn<EditEmailAccountTokenResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual string Token { get; set; }
    }

    public partial class EditEmailAccountTokenResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/accounts/{id}", "PUT")]
    public partial class EditFileAccount
        : CodeMashRequestBase, IReturn<EditFileAccountResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual FileAccountProperties Model { get; set; }
    }

    public partial class EditFileAccountResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/email/templates", "PUT")]
    public partial class EditMessageTemplate
        : CodeMashRequestBase, IReturn<EditMessageTemplateResponse>
    {
        public EditMessageTemplate()
        {
            StaticAttachments = new List<string>{};
            Tags = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual Guid? EmailAccountId { get; set; }
        public virtual List<string> StaticAttachments { get; set; }
        public virtual string Code { get; set; }
        public virtual EmailTemplateType Type { get; set; }
        public virtual List<string> Tags { get; set; }
        public virtual bool IncludeSubscriptionLink { get; set; }
    }

    public partial class EditMessageTemplateResponse
        : ResponseBase<bool>
    {
        public virtual string SubjectError { get; set; }
        public virtual string BodyError { get; set; }
    }

    [Route("/db/taxonomy", "PUT")]
    public partial class EditTaxonomy
        : CodeMashRequestBase, IReturn<EditTaxonomyResponse>
    {
        public EditTaxonomy()
        {
            Dependencies = new List<string>{};
        }

        public virtual Guid TaxonomyId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string JsonTermsMetaSchema { get; set; }
        public virtual string UiTermsMetaSchema { get; set; }
        public virtual List<string> Dependencies { get; set; }
        public virtual string Parent { get; set; }
    }

    public partial class EditTaxonomyResponse
        : ResponseBase<Guid>
    {
        public virtual string TaxonomyName { get; set; }
    }

    [Route("/db/backups/enable", "POST")]
    public partial class EnableBackups
        : CodeMashRequestBase, IReturn<EnableBackupsResponse>
    {
        public virtual Guid? Cluster { get; set; }
        public virtual int Hour { get; set; }
        public virtual int Copies { get; set; }
    }

    public partial class EnableBackupsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/triggers/{id}/enable", "PUT")]
    public partial class EnableCollectionTrigger
        : CodeMashDbRequestBase, IReturn<EnableCollectionTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class EnableCollectionTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/enable", "GET")]
    public partial class EnableDatabase
        : CodeMashRequestBase, IReturn<EstablishDatabaseResponse>
    {
        public EnableDatabase()
        {
            MultiRegions = new List<string>{};
        }

        public virtual string Provider { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string FreeRegion { get; set; }
        public virtual int StorageSize { get; set; }
        public virtual string DatabaseRegion { get; set; }
        public virtual string Cluster { get; set; }
        public virtual bool AutoScaling { get; set; }
        public virtual bool EnableMultiRegion { get; set; }
        public virtual List<string> MultiRegions { get; set; }
        public virtual string AtlasUserName { get; set; }
        public virtual string AtlasPassword { get; set; }
        public virtual string AtlasClusterId { get; set; }
        public virtual string AtlasClusterName { get; set; }
    }

    [Route("/notifications/email/enable", "POST")]
    public partial class EnableEmail
        : CodeMashRequestBase, IReturn<EnableEmailResponse>
    {
        public virtual EmailProperties Model { get; set; }
    }

    public partial class EnableEmailResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/files/enable", "POST")]
    public partial class EnableFiles
        : CodeMashRequestBase, IReturn<EnableFilesResponse>
    {
        public virtual FileAccountProperties Model { get; set; }
    }

    public partial class EnableFilesResponse
        : ResponseBase<Guid>
    {
        public virtual Guid? FileAccountId { get; set; }
        public virtual string FileAccountName { get; set; }
        public virtual int? FilesVersion { get; set; }
    }

    [Route("/files/triggers/{id}/enable", "PUT")]
    public partial class EnableFilesTrigger
        : CodeMashRequestBase, IReturn<EnableFilesTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class EnableFilesTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/logging/enable", "GET")]
    public partial class EnableLogging
        : CodeMashRequestBase, IReturn<EnableLoggingResponse>
    {
    }

    public partial class EnableLoggingResponse
        : ResponseBase<Guid>
    {
    }

    public partial class EnableMarketing
        : CodeMashRequestBase, IReturn<EnableMarketingResponse>
    {
    }

    public partial class EnableMarketingResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/payments/enable", "POST")]
    public partial class EnablePayments
        : CodeMashRequestBase, IReturn<EnablePaymentsResponse>
    {
        public virtual PaymentAccountProperties Model { get; set; }
    }

    public partial class EnablePaymentsResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/payments/triggers/{id}/enable", "PUT")]
    public partial class EnablePaymentTrigger
        : CodeMashRequestBase, IReturn<EnablePaymentTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class EnablePaymentTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/server-events/enable", "GET")]
    public partial class EnableServerEvents
        : CodeMashRequestBase, IReturn<EnableServerEventsResponse>
    {
    }

    public partial class EnableServerEventsResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/serverless/enable", "POST")]
    public partial class EnableServerless
        : CodeMashRequestBase, IReturn<EnableServerlessResponse>
    {
        public virtual ConnectionProperties Model { get; set; }
    }

    public partial class EnableServerlessResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/membership/triggers/{id}/enable", "PUT")]
    public partial class EnableUsersTrigger
        : CodeMashRequestBase, IReturn<EnableUsersTriggerResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class EnableUsersTriggerResponse
        : ResponseBase<bool>
    {
    }

    public partial class EstablishDatabaseResponse
        : ResponseBase<Guid>
    {
        public EstablishDatabaseResponse()
        {
            DatabaseClusters = new List<ClusterDto>{};
        }

        public virtual List<ClusterDto> DatabaseClusters { get; set; }
        public virtual int? DatabaseVersion { get; set; }
        public virtual string DatabaseProvider { get; set; }
    }

    [Route("/serverless/functions/{id}/execute", "POST")]
    public partial class ExecuteTestFunction
        : CodeMashRequestBase, IReturn<ExecuteTestFunctionResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class ExecuteTestFunctionResponse
        : ResponseBase<ServerlessTestFunctionLog>
    {
    }

    public partial class FormRegisterOAuthUser
        : IReturn<FormRegisterOAuthUserResponse>
    {
        public virtual string ProviderUserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual ProjectUserProviders Provider { get; set; }
        public virtual Guid ProjectId { get; set; }
    }

    public partial class FormRegisterOAuthUserResponse
        : ResponseBase<bool>
    {
        public virtual string UserId { get; set; }
        public virtual int UserAuthId { get; set; }
        public virtual string AccountId { get; set; }
    }

    [Route("/account/permissions", "GET")]
    public partial class GetAccountAvailablePermissions
        : RequestBase, IReturn<GetAccountAvailablePermissionsResponse>
    {
    }

    public partial class GetAccountAvailablePermissionsResponse
        : ResponseBase<List<RoleDisplayItemDto>>
    {
    }

    [Route("/account/status", "GET")]
    public partial class GetAccountStatus
        : IReturn<GetAccountStatusResponse>
    {
    }

    public partial class GetAccountStatusResponse
        : ResponseBase<AccountStatusDto>
    {
    }

    [Route("/activities", "GET")]
    public partial class GetActivities
        : CodeMashListRequestBase, IReturn<GetActivitiesResponse>
    {
    }

    public partial class GetActivitiesResponse
        : ResponseBase<List<ActivityListDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/activities/filters", "GET")]
    public partial class GetActivityFilters
        : IReturn<GetActivityFiltersResponse>
    {
    }

    public partial class GetActivityFiltersResponse
        : ResponseBase<ActivityFiltersDto>
    {
    }

    public partial class GetBackups
        : CodeMashListRequestBase, IReturn<GetBackupsResponse>
    {
        public virtual Guid? ClusterId { get; set; }
    }

    [Route("/db/backups/settings", "GET")]
    public partial class GetBackupSettings
        : CodeMashRequestBase, IReturn<GetBackupSettingsResponse>
    {
    }

    public partial class GetBackupSettingsResponse
        : ResponseBase<BackupSettingsDto>
    {
        public GetBackupSettingsResponse()
        {
            Backups = new List<BackupListItemDto>{};
        }

        public virtual List<BackupListItemDto> Backups { get; set; }
        public virtual decimal PerGbCost { get; set; }
    }

    public partial class GetBackupsResponse
        : ResponseBase<List<BackupListItemDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/db/{collectionName}/form", "GET")]
    public partial class GetCollectionForm
        : CodeMashDbRequestBase, IReturn<GetCollectionFormResponse>
    {
    }

    public partial class GetCollectionFormResponse
        : ResponseBase<CollectionFormDto>
    {
    }

    [Route("/db/{collectionName}/functions", "GET")]
    public partial class GetCollectionFunctions
        : CodeMashDbRequestBase, IReturn<GetCollectionFunctionsResponse>
    {
        public virtual bool IsList { get; set; }
    }

    public partial class GetCollectionFunctionsResponse
        : ResponseBase<List<NameIdDto>>
    {
    }

    [Route("/db/collections/tags/filter", "GET POST")]
    [Route("/{version}/db/collections/tags/filter", "GET POST")]
    public partial class GetCollectionTagsFilter
        : CodeMashListRequestBase, IReturn<GetCollectionTagsFilterResponse>
    {
        public GetCollectionTagsFilter()
        {
            Initial = new List<string>{};
        }

        public virtual List<string> Initial { get; set; }
        public virtual string Search { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string Cluster { get; set; }
        public virtual string CollectionId { get; set; }
    }

    public partial class GetCollectionTagsFilterResponse
        : ResponseBase<List<SelectItem>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/serverless/system/functions/{id}/config/collections", "GET")]
    public partial class GetCollectionTriggerFunctionConfig
        : CodeMashDbRequestBase, IReturn<GetCollectionTriggerFunctionConfigResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetCollectionTriggerFunctionConfigResponse
        : ResponseBase<CollectionSysTriggerConfigDto>
    {
    }

    [Route("/account/customer/payment", "GET")]
    public partial class GetCustomerPayment
        : IReturn<GetCustomerPaymentResponse>
    {
    }

    public partial class GetCustomerPaymentResponse
        : ResponseBase<CustomerSettingsDto>
    {
    }

    [Route("/db/settings", "GET")]
    public partial class GetDatabaseSettings
        : CodeMashRequestBase, IReturn<GetDatabaseSettingsResponse>
    {
        public virtual bool IncludeSystemSettings { get; set; }
    }

    public partial class GetDatabaseSettingsResponse
        : ResponseBase<DbSettingsDto>
    {
        public GetDatabaseSettingsResponse()
        {
            Clusters = new List<ClusterDto>{};
        }

        public virtual List<ClusterDto> Clusters { get; set; }
        public virtual int? DatabaseVersion { get; set; }
        public virtual string DatabaseProvider { get; set; }
        public virtual DatabaseSettingsDto SystemSettings { get; set; }
        public virtual bool FreeClusterRequested { get; set; }
    }

    [Route("/db/system-settings", "GET")]
    public partial class GetDatabaseSystemSettings
        : CodeMashRequestBase, IReturn<GetDatabaseSystemSettingsResponse>
    {
    }

    public partial class GetDatabaseSystemSettingsResponse
        : ResponseBase<DatabaseSettingsDto>
    {
    }

    [Route("/files/directories", "GET")]
    public partial class GetDirectories
        : CodeMashListRequestBase, IReturn<GetDirectoriesResponse>
    {
        public virtual string Path { get; set; }
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class GetDirectoriesResponse
        : ResponseBase<List<String>>
    {
    }

    [Route("/account/discounts", "GET")]
    public partial class GetDiscounts
        : IReturn<GetDiscountsResponse>
    {
    }

    public partial class GetDiscountsResponse
        : ResponseBase<List<DiscountDto>>
    {
    }

    [Route("/notifications/emails/{id}", "GET")]
    public partial class GetEmail
        : CodeMashRequestBase, IReturn<GetEmailResponse>
    {
        public virtual string Id { get; set; }
    }

    [Route("/notifications/email/accounts/{id}", "GET")]
    public partial class GetEmailAccount
        : CodeMashRequestBase, IReturn<GetEmailAccountResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetEmailAccountResponse
        : ResponseBase<EmailAccountDto>
    {
    }

    [Route("/notifications/email/accounts", "GET")]
    public partial class GetEmailAccounts
        : CodeMashRequestBase, IReturn<GetEmailAccountsResponse>
    {
    }

    public partial class GetEmailAccountsResponse
        : ResponseBase<List<EmailAccountDto>>
    {
    }

    [Route("/notifications/email/groups/{id}", "GET")]
    public partial class GetEmailGroup
        : CodeMashRequestBase, IReturn<GetEmailGroupResponse>
    {
        public virtual string Id { get; set; }
        public virtual string Language { get; set; }
    }

    [Route("/notifications/email/groups/{id}/events", "GET")]
    public partial class GetEmailGroupEvents
        : CodeMashRequestBase, IReturn<GetEmailGroupEventsResponse>
    {
        public virtual string Id { get; set; }
        public virtual string Language { get; set; }
    }

    public partial class GetEmailGroupEventsResponse
        : ResponseBase<List<EmailGroupEventDto>>
    {
    }

    public partial class GetEmailGroupResponse
        : ResponseBase<EmailGroupDataEditDto>
    {
    }

    [Route("/notifications/email/groups", "GET")]
    public partial class GetEmailGroups
        : CodeMashListRequestBase, IReturn<GetEmailGroupsResponse>
    {
    }

    public partial class GetEmailGroupsResponse
        : ResponseBase<List<EmailGroupDataDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/notifications/email/metrics", "GET")]
    public partial class GetEmailMetrics
        : CodeMashRequestBase, IReturn<GetEmailMetricsResponse>
    {
    }

    public partial class GetEmailMetricsResponse
        : ResponseBase<EmailMetricsDto>
    {
    }

    public partial class GetEmailResponse
        : ResponseBase<EmailDataEditDto>
    {
    }

    [Route("/notifications/emails", "GET")]
    public partial class GetEmails
        : CodeMashListRequestBase, IReturn<GetEmailsResponse>
    {
        public virtual string GroupId { get; set; }
        public virtual string Language { get; set; }
    }

    public partial class GetEmailsResponse
        : ResponseBase<List<EmailDataDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    public partial class GetEstablishedProjectModules
        : CodeMashRequestBase, IReturn<GetEstablishedProjectModulesResponse>
    {
    }

    public partial class GetEstablishedProjectModulesResponse
        : ResponseBase<List<IAuthorizedModule>>
    {
    }

    [Route("/files/accounts/{id}", "GET")]
    public partial class GetFileAccount
        : CodeMashRequestBase, IReturn<GetFileAccountResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetFileAccountResponse
        : ResponseBase<FileAccountDto>
    {
    }

    [Route("/files/accounts", "GET")]
    public partial class GetFileAccounts
        : CodeMashRequestBase, IReturn<GetFileAccountsResponse>
    {
    }

    public partial class GetFileAccountsResponse
        : ResponseBase<List<FileAccountDto>>
    {
    }

    [Route("/files/{id}/optimizations", "GET")]
    public partial class GetFileOptimizations
        : CodeMashRequestBase, IReturn<GetFileOptimizationsResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class GetFileOptimizationsResponse
        : ResponseBase<List<DbFileOptimization>>
    {
    }

    public partial class GetFilesFilter
        : CodeMashListRequestBase, IReturn<GetFilesFilterResponse>
    {
        public GetFilesFilter()
        {
            Initial = new List<string>{};
        }

        public virtual List<string> Initial { get; set; }
        public virtual string Search { get; set; }
    }

    public partial class GetFilesFilterResponse
        : ResponseBase<List<SelectItem>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/files/settings", "GET")]
    public partial class GetFilesSettings
        : CodeMashRequestBase, IReturn<GetFilesSettingsResponse>
    {
    }

    public partial class GetFilesSettingsResponse
        : ResponseBase<FilesSettingsDto>
    {
    }

    [Route("/serverless/system/functions/{id}/config/files", "GET")]
    public partial class GetFilesTriggerFunctionConfig
        : CodeMashRequestBase, IReturn<GetFilesTriggerFunctionConfigResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetFilesTriggerFunctionConfigResponse
        : ResponseBase<CollectionSysTriggerConfigDto>
    {
    }

    [Route("/files/triggers", "GET")]
    public partial class GetFilesTriggers
        : CodeMashRequestBase, IReturn<GetFilesTriggersResponse>
    {
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class GetFilesTriggersResponse
        : ResponseBase<List<FilesTriggerCreateDto>>
    {
    }

    [Route("/files/tokens/keys", "GET")]
    public partial class GetFileTokenKeys
        : CodeMashRequestBase, IReturn<GetFileTokenKeysResponse>
    {
    }

    public partial class GetFileTokenKeysResponse
        : ResponseBase<List<String>>
    {
    }

    [Route("/files/folder", "GET")]
    public partial class GetFolderFiles
        : CodeMashListRequestBase, IReturn<GetFolderFilesResponse>
    {
        public virtual Guid? FileAccountId { get; set; }
        public virtual string Path { get; set; }
    }

    public partial class GetFolderFilesResponse
        : ResponseBase<List<BrowseObjectDto>>
    {
        public virtual long TotalCount { get; set; }
        public virtual bool CanInsert { get; set; }
        public virtual bool IsParentPublic { get; set; }
    }

    [Route("/shared/forms/tags/filter", "POST")]
    public partial class GetFormCollectionTagsFilter
        : CodeMashFormRequest, IReturn<GetFormCollectionTagsFilterResponse>
    {
        public GetFormCollectionTagsFilter()
        {
            Initial = new List<string>{};
        }

        public virtual List<string> Initial { get; set; }
        public virtual string Search { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string CollectionId { get; set; }
        public virtual int PageSize { get; set; }
        public virtual int PageNumber { get; set; }
    }

    public partial class GetFormCollectionTagsFilterResponse
        : ResponseBase<List<SelectItem>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/shared/forms/collections/filter", "POST")]
    public partial class GetFormRecordsFilter
        : CodeMashFormRequest, IReturn<GetFormRecordsFilterResponse>
    {
        public GetFormRecordsFilter()
        {
            Initial = new List<string>{};
        }

        public virtual List<string> Initial { get; set; }
        public virtual string Search { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string CollectionId { get; set; }
        public virtual string LabelField { get; set; }
        public virtual int PageSize { get; set; }
        public virtual int PageNumber { get; set; }
    }

    public partial class GetFormRecordsFilterResponse
        : ResponseBase<List<SelectItem>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/shared/forms/roles", "GET")]
    public partial class GetFormRoles
        : CodeMashFormRequest, IReturn<GetFormRolesResponse>
    {
    }

    public partial class GetFormRolesResponse
        : ResponseBase<List<RoleBaseDto>>
    {
    }

    [Route("/shared/forms/taxonomies/filter", "POST")]
    public partial class GetFormTermsFilter
        : CodeMashFormRequest, IReturn<GetFormTermsFilterResponse>
    {
        public GetFormTermsFilter()
        {
            Initial = new List<string>{};
        }

        public virtual List<string> Initial { get; set; }
        public virtual string Search { get; set; }
        public virtual string TaxonomyName { get; set; }
        public virtual string TaxonomyId { get; set; }
        public virtual int PageSize { get; set; }
        public virtual int PageNumber { get; set; }
    }

    public partial class GetFormTermsFilterResponse
        : ResponseBase<List<SelectItem>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/shared/forms/users/filter", "POST")]
    public partial class GetFormUsersFilter
        : CodeMashFormRequest, IReturn<GetFormUsersFilterResponse>
    {
        public GetFormUsersFilter()
        {
            Initial = new List<Guid>{};
            Meta = new Dictionary<string, string>{};
        }

        public virtual List<Guid> Initial { get; set; }
        public virtual string Search { get; set; }
        public virtual string LabelField { get; set; }
        public virtual string ValueField { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual int PageSize { get; set; }
        public virtual int PageNumber { get; set; }
    }

    public partial class GetFormUsersFilterResponse
        : ResponseBase<List<SelectItem>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/serverless/functions/{id}/usage", "GET")]
    public partial class GetFunctionUsage
        : CodeMashRequestBase, IReturn<GetFunctionUsageResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetFunctionUsageResponse
        : ResponseBase<EmailTemplateUsageDto>
    {
    }

    [Route("/account/invoices", "GET")]
    public partial class GetInvoices
        : IReturn<GetInvoicesResponse>
    {
    }

    public partial class GetInvoicesResponse
        : ResponseBase<List<InvoiceDto>>
    {
    }

    [Route("/logging/logs/{id}", "GET")]
    public partial class GetLog
        : CodeMashRequestBase, IReturn<GetLogResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class GetLogResponse
        : ResponseBase<LogDto>
    {
    }

    [Route("/logging/logs", "GET")]
    public partial class GetLogs
        : CodeMashListRequestBase, IReturn<GetLogsResponse>
    {
    }

    public partial class GetLogsResponse
        : ResponseBase<List<LogListItemDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/notifications/email/templates/{id}", "GET")]
    public partial class GetMessageTemplate
        : CodeMashRequestBase, IReturn<GetMessageTemplateResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetMessageTemplateResponse
        : ResponseBase<MessageTemplateDto>
    {
        public virtual bool ContentFound { get; set; }
    }

    [Route("/notifications/email/templates", "GET")]
    public partial class GetMessageTemplates
        : CodeMashRequestBase, IReturn<GetMessageTemplatesResponse>
    {
    }

    public partial class GetMessageTemplatesResponse
        : ResponseBase<List<MessageTemplateDto>>
    {
        public virtual bool HasEmailAccount { get; set; }
    }

    [Route("/notifications/email/templates/tags", "GET")]
    public partial class GetMessageTemplateTags
        : CodeMashRequestBase, IReturn<GetMessageTemplateTagsResponse>
    {
    }

    public partial class GetMessageTemplateTagsResponse
        : ResponseBase<List<KeyValue>>
    {
    }

    [Route("/notifications/email/templates/{id}/tokens", "GET")]
    public partial class GetMessageTemplateTokens
        : CodeMashRequestBase, IReturn<GetMessageTemplateTokensResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetMessageTemplateTokensResponse
        : ResponseBase<List<String>>
    {
    }

    [Route("/notifications/email/templates/{id}/usage", "GET")]
    public partial class GetMessageTemplateUsage
        : CodeMashRequestBase, IReturn<GetMessageTemplateUsageResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetMessageTemplateUsageResponse
        : ResponseBase<EmailTemplateUsageDto>
    {
    }

    [Route("/notifications/push/{id}", "GET")]
    public partial class GetNotification
        : CodeMashRequestBase, IReturn<GetNotificationResponse>
    {
        public virtual string Id { get; set; }
    }

    [Route("/notifications/push/groups/{id}", "GET")]
    public partial class GetNotificationGroup
        : CodeMashRequestBase, IReturn<GetNotificationGroupResponse>
    {
        public virtual string Id { get; set; }
        public virtual string Language { get; set; }
    }

    [Route("/notifications/push/groups/{id}/events", "GET")]
    public partial class GetNotificationGroupEvents
        : CodeMashRequestBase, IReturn<GetNotificationGroupEventsResponse>
    {
        public virtual string Id { get; set; }
        public virtual string Language { get; set; }
    }

    public partial class GetNotificationGroupEventsResponse
        : ResponseBase<List<EmailGroupEventDto>>
    {
    }

    public partial class GetNotificationGroupResponse
        : ResponseBase<NotificationGroupDataEditDto>
    {
    }

    [Route("/notifications/push/groups", "GET")]
    public partial class GetNotificationGroups
        : CodeMashListRequestBase, IReturn<GetNotificationGroupsResponse>
    {
    }

    public partial class GetNotificationGroupsResponse
        : ResponseBase<List<NotificationGroupDataDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/notifications/push/metrics", "GET")]
    public partial class GetNotificationMetrics
        : CodeMashRequestBase, IReturn<GetNotificationMetricsResponse>
    {
    }

    public partial class GetNotificationMetricsResponse
        : ResponseBase<EmailMetricsDto>
    {
    }

    public partial class GetNotificationResponse
        : ResponseBase<NotificationDataEditDto>
    {
    }

    [Route("/notifications/push", "GET")]
    public partial class GetNotifications
        : CodeMashListRequestBase, IReturn<GetNotificationsResponse>
    {
        public virtual string GroupId { get; set; }
        public virtual string Language { get; set; }
    }

    public partial class GetNotificationsResponse
        : ResponseBase<List<NotificationDataDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/notifications/push/templates/{id}/tokens", "GET")]
    public partial class GetNotificationTemplateTokens
        : CodeMashRequestBase, IReturn<GetNotificationTemplateTokensResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetNotificationTemplateTokensResponse
        : ResponseBase<List<String>>
    {
    }

    [Route("/notifications/push/templates/{id}/usage", "GET")]
    public partial class GetNotificationTemplateUsage
        : CodeMashRequestBase, IReturn<GetNotificationTemplateUsageResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetNotificationTemplateUsageResponse
        : ResponseBase<EmailTemplateUsageDto>
    {
    }

    [Route("/payments/orders/{orderId}/transactions/{id}", "GET")]
    public partial class GetOrderTransaction
        : CodeMashRequestBase, IReturn<GetOrderTransactionResponse>
    {
        public virtual string OrderId { get; set; }
        public virtual string Id { get; set; }
    }

    public partial class GetOrderTransactionResponse
        : ResponseBase<TransactionDto>
    {
    }

    [Route("/payments/orders/{orderId}/transactions", "GET")]
    public partial class GetOrderTransactions
        : CodeMashListRequestBase, IReturn<GetOrderTransactionsResponse>
    {
        public virtual string OrderId { get; set; }
    }

    public partial class GetOrderTransactionsResponse
        : ResponseBase<List<TransactionDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/payments/accounts/{id}", "GET")]
    public partial class GetPaymentAccount
        : CodeMashRequestBase, IReturn<GetPaymentAccountResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetPaymentAccountResponse
        : ResponseBase<PaymentAccountDto>
    {
    }

    [Route("/payments/accounts", "GET")]
    public partial class GetPaymentAccounts
        : CodeMashRequestBase, IReturn<GetPaymentAccountsResponse>
    {
    }

    public partial class GetPaymentAccountsResponse
        : ResponseBase<List<PaymentAccountDto>>
    {
    }

    [Route("/payments/plans/{id}", "GET")]
    public partial class GetPaymentPlan
        : CodeMashRequestBase, IReturn<GetPaymentPlanResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetPaymentPlanResponse
        : ResponseBase<PaymentPlanDto>
    {
    }

    [Route("/payments/plans", "GET")]
    public partial class GetPaymentPlans
        : CodeMashRequestBase, IReturn<GetPaymentPlansResponse>
    {
    }

    public partial class GetPaymentPlansResponse
        : ResponseBase<List<PaymentPlanDto>>
    {
    }

    [Route("/serverless/system/functions/{id}/config/payments", "GET")]
    public partial class GetPaymentsTriggerFunctionConfig
        : CodeMashRequestBase, IReturn<GetPaymentsTriggerFunctionConfigResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetPaymentsTriggerFunctionConfigResponse
        : ResponseBase<CollectionSysTriggerConfigDto>
    {
    }

    [Route("/payments/tokens/keys", "GET")]
    public partial class GetPaymentTokenKeys
        : CodeMashRequestBase, IReturn<GetPaymentTokenKeysResponse>
    {
    }

    public partial class GetPaymentTokenKeysResponse
        : ResponseBase<List<String>>
    {
    }

    [Route("/payments/triggers", "GET")]
    public partial class GetPaymentTriggers
        : CodeMashRequestBase, IReturn<GetPaymentTriggersResponse>
    {
    }

    public partial class GetPaymentTriggersResponse
        : ResponseBase<List<PaymentTriggerCreateDto>>
    {
    }

    [Route("/account/profile", "GET")]
    public partial class GetProfile
        : RequestBase, IReturn<GetProfileResponse>
    {
    }

    public partial class GetProfileResponse
        : ResponseBase<AccountUserDto>
    {
    }

    [Route("/projects/{projectId}", "GET")]
    public partial class GetProject
        : CodeMashRequestBase, IReturn<GetProjectResponse>
    {
    }

    [Route("/projects/{projectId}/billing", "GET")]
    public partial class GetProjectBillingSettings
        : CodeMashRequestBase, IReturn<GetProjectBillingSettingsResponse>
    {
    }

    public partial class GetProjectBillingSettingsResponse
        : ResponseBase<ProjectBillingSettingsDto>
    {
    }

    [Route("/projects/{projectId}/languages", "GET")]
    public partial class GetProjectLanguages
        : CodeMashRequestBase, IReturn<GetProjectLanguagesResponse>
    {
    }

    public partial class GetProjectLanguagesResponse
        : ResponseBase<bool>
    {
        public GetProjectLanguagesResponse()
        {
            AvailableLanguages = new List<string>{};
        }

        public virtual List<string> AvailableLanguages { get; set; }
        public virtual string DefaultLanguage { get; set; }
    }

    [Route("/project/logo", "GET")]
    public partial class GetProjectLogo
        : CodeMashRequestBase, IReturn<HttpResult>
    {
        public virtual bool AsText { get; set; }
    }

    [Route("/projects/{projectId}/modules", "GET")]
    public partial class GetProjectModuleData
        : CodeMashRequestBase, IReturn<GetProjectModuleDataResponse>
    {
    }

    public partial class GetProjectModuleDataResponse
        : ResponseBase<ProjectModulesData>
    {
    }

    [Route("/regions", "GET")]
    public partial class GetProjectRegions
        : IReturn<GetProjectRegionsResponse>
    {
    }

    public partial class GetProjectRegionsResponse
        : ResponseBase<List<ProjectZoneDto>>
    {
    }

    public partial class GetProjectResponse
        : ResponseBase<ProjectExposedDto>
    {
        public GetProjectResponse()
        {
            DatabaseClusters = new List<ClusterDto>{};
            FileAccounts = new List<FileAccountBasicDto>{};
        }

        public virtual string AccountPlan { get; set; }
        public virtual List<ClusterDto> DatabaseClusters { get; set; }
        public virtual int? DatabaseVersion { get; set; }
        public virtual string DatabaseProvider { get; set; }
        public virtual int? FilesVersion { get; set; }
        public virtual List<FileAccountBasicDto> FileAccounts { get; set; }
    }

    [Route("/projects", "GET")]
    public partial class GetProjects
        : RequestBase, IReturn<GetProjectsResponse>
    {
    }

    public partial class GetProjectsResponse
        : ResponseBase<List<ProjectSmallDto>>
    {
    }

    [Route("/projects/{projectId}/tokens/keys", "GET")]
    public partial class GetProjectTokenKeys
        : CodeMashRequestBase, IReturn<GetProjectTokenKeysResponse>
    {
    }

    public partial class GetProjectTokenKeysResponse
        : ResponseBase<List<String>>
    {
    }

    [Route("/projects/{projectId}/tokens", "GET")]
    public partial class GetProjectTokens
        : CodeMashRequestBase, IReturn<GetProjectTokensResponse>
    {
    }

    public partial class GetProjectTokensResponse
        : ResponseBase<List<Token>>
    {
        public GetProjectTokensResponse()
        {
            RuntimeTokens = new List<string>{};
        }

        public virtual List<string> RuntimeTokens { get; set; }
    }

    [Route("/db/collections/filter", "GET POST")]
    [Route("/{version}/db/collections/filter", "GET POST")]
    public partial class GetRecordsFilter
        : CodeMashListRequestBase, IReturn<GetRecordsFilterResponse>
    {
        public GetRecordsFilter()
        {
            Initial = new List<string>{};
        }

        public virtual List<string> Initial { get; set; }
        public virtual string Search { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string CollectionId { get; set; }
        public virtual string Cluster { get; set; }
        public virtual string LabelField { get; set; }
    }

    public partial class GetRecordsFilterResponse
        : ResponseBase<List<SelectItem>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/membership/roles/{name}", "GET")]
    public partial class GetRole
        : CodeMashRequestBase, IReturn<GetRoleResponse>
    {
        public virtual string Name { get; set; }
    }

    public partial class GetRoleResponse
        : ResponseBase<Role>
    {
    }

    [Route("/membership/roles", "GET")]
    public partial class GetRoles
        : CodeMashRequestBase, IReturn<GetRolesResponse>
    {
        public virtual bool IncludePolicies { get; set; }
    }

    public partial class GetRolesResponse
        : ResponseBase<List<RoleBaseDto>>
    {
    }

    [Route("/scheduler/tasks/{taskId}/logs/{id}", "GET")]
    public partial class GetScheduledTaskLog
        : CodeMashListRequestBase, IReturn<GetScheduledTaskLogResponse>
    {
        public virtual string Id { get; set; }
        public virtual Guid TaskId { get; set; }
    }

    public partial class GetScheduledTaskLogResponse
        : ResponseBase<SchedulerTaskExecutionLogDto>
    {
    }

    [Route("/scheduler/tasks/{id}/logs", "GET")]
    public partial class GetScheduledTaskLogs
        : CodeMashListRequestBase, IReturn<GetScheduledTaskLogsResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetScheduledTaskLogsResponse
        : ResponseBase<List<SchedulerTaskExecutionLogDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/db/schemas/{collectionName}", "GET")]
    public partial class GetSchema
        : CodeMashDbRequestBase, IReturn<GetSchemaResponse>
    {
    }

    [Route("/db/aggregates/{id}", "GET")]
    public partial class GetSchemaAggregate
        : CodeMashRequestBase, IReturn<GetSchemaAggregateResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetSchemaAggregateResponse
        : ResponseBase<CollectionAggregateDto>
    {
    }

    [Route("/db/aggregates", "GET")]
    public partial class GetSchemaAggregates
        : CodeMashRequestBase, IReturn<GetSchemaAggregatesResponse>
    {
    }

    public partial class GetSchemaAggregatesResponse
        : ResponseBase<List<CollectionAggregateDto>>
    {
    }

    [Route("/db/exports/{exportId}", "GET")]
    public partial class GetSchemaExport
        : CodeMashRequestBase, IReturn<GetSchemaExportResponse>
    {
        public virtual string ExportId { get; set; }
    }

    public partial class GetSchemaExportResponse
        : ResponseBase<ExportItemDto>
    {
    }

    [Route("/db/exports", "GET")]
    public partial class GetSchemaExports
        : CodeMashListRequestBase, IReturn<GetSchemaExportsResponse>
    {
        public virtual string Cluster { get; set; }
    }

    public partial class GetSchemaExportsResponse
        : ResponseBase<List<ExportListItemDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/db/imports/{importId}", "GET")]
    public partial class GetSchemaImport
        : CodeMashRequestBase, IReturn<GetSchemaImportResponse>
    {
        public virtual string ImportId { get; set; }
    }

    public partial class GetSchemaImportResponse
        : ResponseBase<ImportItemDto>
    {
    }

    [Route("/db/imports", "GET")]
    public partial class GetSchemaImports
        : CodeMashListRequestBase, IReturn<GetSchemaImportsResponse>
    {
        public virtual string Cluster { get; set; }
    }

    public partial class GetSchemaImportsResponse
        : ResponseBase<List<ImportListItemDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    public partial class GetSchemaResponse
        : ResponseBase<SchemaDto>
    {
        public GetSchemaResponse()
        {
            AvailableLanguages = new List<string>{};
            TranslatableFields = new List<string>{};
            Indexes = new List<CollectionIndexDto>{};
        }

        public virtual List<string> AvailableLanguages { get; set; }
        public virtual List<string> TranslatableFields { get; set; }
        public virtual string DefaultLanguage { get; set; }
        public virtual List<CollectionIndexDto> Indexes { get; set; }
        public virtual bool IsFieldRenaming { get; set; }
        public virtual string RenamingFieldFrom { get; set; }
        public virtual string RenamingFieldTo { get; set; }
    }

    [Route("/db/schemas", "GET")]
    public partial class GetSchemas
        : CodeMashRequestBase, IReturn<GetSchemasResponse>
    {
    }

    public partial class GetSchemasResponse
        : ResponseBase<List<SchemaBasicDto>>
    {
    }

    [Route("/db/schemas/system-templates/{id}", "GET")]
    public partial class GetSchemaTemplate
        : CodeMashRequestBase, IReturn<GetSchemaTemplateResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class GetSchemaTemplateResponse
        : ResponseBase<SchemaTemplate>
    {
    }

    [Route("/db/schemas/system-templates", "GET")]
    public partial class GetSchemaTemplates
        : CodeMashRequestBase, IReturn<GetSchemaTemplatesResponse>
    {
    }

    public partial class GetSchemaTemplatesResponse
        : ResponseBase<List<SchemaTemplateDto>>
    {
    }

    [Route("/notifications/server-events/settings", "GET")]
    public partial class GetServerEventsSettings
        : CodeMashRequestBase, IReturn<GetServerEventsSettingsResponse>
    {
    }

    public partial class GetServerEventsSettingsResponse
        : ResponseBase<ServerEventsSettingsDto>
    {
    }

    [Route("/serverless/connections/{provider}", "GET")]
    public partial class GetServerlessConnection
        : CodeMashRequestBase, IReturn<GetServerlessConnectionResponse>
    {
        public virtual ServerlessProvider Provider { get; set; }
    }

    public partial class GetServerlessConnectionResponse
        : ResponseBase<ServerlessProviderDto>
    {
    }

    [Route("/serverless/connections", "GET")]
    public partial class GetServerlessConnections
        : CodeMashRequestBase, IReturn<GetServerlessConnectionsResponse>
    {
    }

    public partial class GetServerlessConnectionsResponse
        : ResponseBase<List<ServerlessProviderDto>>
    {
    }

    [Route("/shared/forms", "GET")]
    public partial class GetSharedForm
        : CodeMashFormRequest, IReturn<GetSharedFormResponse>
    {
        public virtual string EditId { get; set; }
        public virtual bool IsResult { get; set; }
    }

    public partial class GetSharedFormResponse
        : ResponseBase<CollectionFormDto>
    {
        public GetSharedFormResponse()
        {
            AvailableLanguages = new List<string>{};
            TranslatableFields = new List<string>{};
        }

        public virtual string JsonSchema { get; set; }
        public virtual string UiSchema { get; set; }
        public virtual Guid ProjectId { get; set; }
        public virtual Guid AccountId { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string SchemaId { get; set; }
        public virtual List<string> AvailableLanguages { get; set; }
        public virtual string DefaultLanguage { get; set; }
        public virtual List<string> TranslatableFields { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string Records { get; set; }
        public virtual string EditRecord { get; set; }
        public virtual bool NeedAuth { get; set; }
        public virtual long TotalInserted { get; set; }
        public virtual bool PublicSession { get; set; }
        public virtual string LogoUrl { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual string LoggedInName { get; set; }
    }

    [Route("/account/plan", "GET")]
    public partial class GetSubscription
        : IReturn<GetSubscriptionResponse>
    {
    }

    public partial class GetSubscriptionResponse
        : ResponseBase<AccountSettingsDto>
    {
    }

    [Route("/serverless/system/functions/{id}", "GET")]
    public partial class GetSystemFunction
        : CodeMashRequestBase, IReturn<GetSystemFunctionResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetSystemFunctionResponse
        : ResponseBase<ServerlessSystemFunctionDto>
    {
        public virtual FunctionResource SystemResources { get; set; }
        public virtual string FunctionResources { get; set; }
        public virtual bool AllowMemoryChange { get; set; }
        public virtual bool AllowTimeoutChange { get; set; }
        public virtual bool ShowAdvanced { get; set; }
        public virtual GoogleSettings GoogleSettings { get; set; }
        public virtual AzureActiveDirSettings AzureActiveDirSettings { get; set; }
        public virtual bool AuthProviderDisabled { get; set; }
    }

    [Route("/serverless/system/functions", "GET")]
    public partial class GetSystemFunctions
        : CodeMashListRequestBase, IReturn<GetSystemFunctionsResponse>
    {
        public virtual bool ExcludeEnabled { get; set; }
    }

    public partial class GetSystemFunctionsResponse
        : ResponseBase<List<ServerlessFunctionDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/taxonomies/{taxonomyName}/terms", "GET")]
    public partial class GetSystemTerms
        : CodeMashRequestBase, IReturn<GetSystemTermsResponse>
    {
        public virtual string TaxonomyName { get; set; }
    }

    public partial class GetSystemTermsResponse
        : ResponseBase<List<TermBasicDto>>
    {
    }

    [Route("/db/taxonomies", "GET")]
    public partial class GetTaxonomies
        : CodeMashRequestBase, IReturn<GetTaxonomiesResponse>
    {
    }

    public partial class GetTaxonomiesResponse
        : ResponseBase<List<TaxonomyListDto>>
    {
        public virtual bool CanUseAdvance { get; set; }
    }

    [Route("/db/taxonomies/{collectionName}", "GET")]
    public partial class GetTaxonomy
        : CodeMashDbRequestBase, IReturn<GetTaxonomyResponse>
    {
    }

    public partial class GetTaxonomyResponse
        : ResponseBase<TaxonomyDto>
    {
        public GetTaxonomyResponse()
        {
            AvailableLanguages = new List<string>{};
        }

        public virtual List<string> AvailableLanguages { get; set; }
        public virtual string DefaultLanguage { get; set; }
    }

    [Route("/db/taxonomies/{collectionName}/terms/{id}", "GET")]
    public partial class GetTerm
        : CodeMashDbRequestBase, IReturn<GetTermResponse>
    {
        public virtual string Id { get; set; }
    }

    [Route("/db/taxonomies/{collectionName}/term-create", "GET")]
    public partial class GetTermForCreate
        : CodeMashDbRequestBase, IReturn<GetTermForCreateResponse>
    {
    }

    public partial class GetTermForCreateResponse
        : ResponseBase<string>
    {
        public GetTermForCreateResponse()
        {
            Taxonomies = new List<TaxonomyBasicDto>{};
            AvailableLanguages = new List<string>{};
        }

        public virtual TaxonomyDto Taxonomy { get; set; }
        public virtual List<TaxonomyBasicDto> Taxonomies { get; set; }
        public virtual List<string> AvailableLanguages { get; set; }
        public virtual string DefaultLanguage { get; set; }
    }

    public partial class GetTermResponse
        : ResponseBase<TermDto>
    {
    }

    [Route("/db/taxonomies/terms/filter", "GET POST")]
    [Route("/{version}/db/taxonomies/terms/filter", "GET POST")]
    public partial class GetTermsFilter
        : CodeMashListRequestBase, IReturn<GetTermsFilterResponse>
    {
        public GetTermsFilter()
        {
            Initial = new List<string>{};
        }

        public virtual List<string> Initial { get; set; }
        public virtual string Search { get; set; }
        public virtual string TaxonomyName { get; set; }
        public virtual string Cluster { get; set; }
        public virtual string TaxonomyId { get; set; }
    }

    public partial class GetTermsFilterResponse
        : ResponseBase<List<SelectItem>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/timezones", "GET")]
    public partial class GetTimeZones
        : IReturn<GetTimeZonesResponse>
    {
    }

    public partial class GetTimeZonesResponse
        : ResponseBase<List<NameId>>
    {
    }

    [Route("/account/usage", "GET")]
    public partial class GetUsage
        : IReturn<GetUsageResponse>
    {
    }

    public partial class GetUsageResponse
        : ResponseBase<List<MonthlyUsageDto>>
    {
        public virtual MonthlyUsageSettingsDto Settings { get; set; }
    }

    [Route("/account/users/{Id}", "GET")]
    public partial class GetUser
        : RequestBase, IReturn<GetUserResponse>
    {
        public virtual Guid Id { get; set; }
    }

    [Route("/notifications/email/user/preferences", "GET")]
    public partial class GetUserEmailPreferences
        : IReturn<GetUserEmailPreferencesResponse>
    {
        public virtual string Token { get; set; }
    }

    public partial class GetUserEmailPreferencesResponse
        : ResponseBase<List<NameIdDto>>
    {
        public GetUserEmailPreferencesResponse()
        {
            UnsubscribedNewsTags = new List<string>{};
        }

        public virtual bool IsMarketing { get; set; }
        public virtual bool SubscribedToNews { get; set; }
        public virtual List<string> UnsubscribedNewsTags { get; set; }
        public virtual string LogoUrl { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual EmailPreferenceTexts Texts { get; set; }
        public virtual Guid ProjectId { get; set; }
        public virtual Guid UserId { get; set; }
    }

    [Route("/account/users/permissions", "GET")]
    public partial class GetUserPermissions
        : RequestBase, IReturn<GetUserPermissionsResponse>
    {
    }

    public partial class GetUserPermissionsResponse
        : ResponseBase<List<String>>
    {
        public virtual string Role { get; set; }
    }

    public partial class GetUserResponse
        : ResponseBase<AccountUserDto>
    {
    }

    [Route("/account/users", "GET")]
    public partial class GetUsers
        : CodeMashListRequestBase, IReturn<GetUsersResponse>
    {
    }

    [Route("/membership/users/filter", "GET POST")]
    [Route("/{version}/membership/users/filter", "GET POST")]
    public partial class GetUsersFilter
        : CodeMashListRequestBase, IReturn<GetUsersFilterResponse>
    {
        public GetUsersFilter()
        {
            Initial = new List<Guid>{};
            Meta = new Dictionary<string, string>{};
        }

        public virtual List<Guid> Initial { get; set; }
        public virtual string Search { get; set; }
        public virtual string LabelField { get; set; }
        public virtual string ValueField { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
    }

    public partial class GetUsersFilterResponse
        : ResponseBase<List<SelectItem>>
    {
        public virtual long TotalCount { get; set; }
    }

    public partial class GetUsersResponse
        : ResponseBase<List<AccountUserDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/membership/triggers", "GET")]
    public partial class GetUsersTrigger
        : CodeMashRequestBase, IReturn<GetUsersTriggerResponse>
    {
    }

    [Route("/serverless/system/functions/{id}/config/users", "GET")]
    public partial class GetUsersTriggerFunctionConfig
        : CodeMashRequestBase, IReturn<GetUsersTriggerFunctionConfigResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetUsersTriggerFunctionConfigResponse
        : ResponseBase<CollectionSysTriggerConfigDto>
    {
    }

    public partial class GetUsersTriggerResponse
        : ResponseBase<List<UsersTriggerCreateDto>>
    {
    }

    [Route("/users/tokens/keys", "GET")]
    public partial class GetUserTokenKeys
        : CodeMashRequestBase, IReturn<GetUserTokenKeysResponse>
    {
    }

    public partial class GetUserTokenKeysResponse
        : ResponseBase<List<String>>
    {
    }

    ///<summary>
    ///Sign In
    ///</summary>
    [Route("/shared/forms/auth/google", "GET POST")]
    [Api(Description="Sign In")]
    public partial class GoogleAuthentication
        : Authenticate, IReturn<AuthenticateResponse>
    {
    }

    [Route("/files/{id}/access/public", "PUT")]
    public partial class GrantPublicFileAccess
        : CodeMashRequestBase, IReturn<GrantPublicFileAccessResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class GrantPublicFileAccessResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/folders/access/public", "PUT")]
    public partial class GrantPublicFolderAccess
        : CodeMashRequestBase, IReturn<GrantPublicFolderAccessResponse>
    {
        public GrantPublicFolderAccess()
        {
            Referrers = new List<string>{};
        }

        public virtual string Path { get; set; }
        public virtual List<string> Referrers { get; set; }
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class GrantPublicFolderAccessResponse
        : ResponseBase<bool>
    {
    }

    [Route("/shared/forms/response", "POST")]
    public partial class InsertSharedForm
        : CodeMashFormRequest, IReturn<InsertSharedFormResponse>
    {
        public virtual string Document { get; set; }
    }

    public partial class InsertSharedFormResponse
        : ResponseBase<string>
    {
    }

    [Route("/account/users/invite", "POST")]
    public partial class InviteUser
        : IReturn<InviteUserResponse>
    {
        public InviteUser()
        {
            Permissions = new List<string>{};
            AllowedProjects = new List<string>{};
        }

        public virtual string DisplayName { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual List<string> Permissions { get; set; }
        public virtual List<string> AllowedProjects { get; set; }
    }

    public partial class InviteUserResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/unpaid-invoices/pay", "POST")]
    public partial class PayForUnpaidInvoices
        : IReturn<PayForUnpaidInvoicesResponse>
    {
    }

    public partial class PayForUnpaidInvoicesResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/backups/{id}/rebuild", "GET")]
    public partial class RebuildBackup
        : CodeMashRequestBase, IReturn<RebuildBackupResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class RebuildBackupResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/discounts", "POST")]
    public partial class RedeemDiscount
        : IReturn<RedeemDiscountResponse>
    {
        public virtual string Code { get; set; }
    }

    public partial class RedeemDiscountResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/{projectId}/modules/refresh", "POST")]
    public partial class RefreshProjectModuleData
        : CodeMashRequestBase, IReturn<RefreshProjectModuleDataResponse>
    {
        public virtual Modules Module { get; set; }
    }

    public partial class RefreshProjectModuleDataResponse
        : ResponseBase<bool>
    {
    }

    [Route("/project/key/regenerate", "POST")]
    public partial class RegenerateProjectToken
        : CodeMashRequestBase, IReturn<RegenerateProjectTokenResponse>
    {
    }

    public partial class RegenerateProjectTokenResponse
        : ResponseBase<string>
    {
    }

    [Route("/serverless/connections/{provider}/functions/reload", "GET")]
    public partial class ReloadServerlessFunctions
        : CodeMashRequestBase, IReturn<ReloadServerlessFunctionsResponse>
    {
        public virtual ServerlessProvider Provider { get; set; }
    }

    public partial class ReloadServerlessFunctionsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/admin/serverless/system/functions/{provider}/reload", "GET")]
    public partial class ReloadSystemFunctions
        : IReturn<ReloadSystemFunctionsResponse>, IRequestBase
    {
        public virtual SystemFunctionProvider Provider { get; set; }
    }

    public partial class ReloadSystemFunctionsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/serverless/functions/{id}", "DELETE")]
    public partial class RemoveFunction
        : CodeMashRequestBase, IReturn<RemoveFunctionResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class RemoveFunctionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/schemas/{collectionName}/rename-field", "PUT")]
    public partial class RenameFieldUniqueName
        : CodeMashDbRequestBase, IReturn<RenameFieldUniqueNameResponse>
    {
        public virtual string OldName { get; set; }
        public virtual string NewName { get; set; }
    }

    public partial class RenameFieldUniqueNameResponse
        : ResponseBase<string>
    {
    }

    [Route("/files/folder/{id}", "PUT")]
    public partial class RenameFolder
        : CodeMashRequestBase, IReturn<RenameFolderResponse>
    {
        public virtual string Path { get; set; }
        public virtual Guid Id { get; set; }
        public virtual string NewName { get; set; }
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class RenameFolderResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/schemas/rename", "PUT")]
    public partial class RenameSchema
        : CodeMashRequestBase, IReturn<RenameSchemaResponse>
    {
        public virtual Guid SchemaId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual bool RenameUniqueName { get; set; }
    }

    public partial class RenameSchemaResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/taxonomies/{collectionName}/rename-field", "PUT")]
    public partial class RenameTaxonomyFieldUniqueName
        : CodeMashDbRequestBase, IReturn<RenameTaxonomyFieldUniqueNameResponse>
    {
        public virtual string OldName { get; set; }
        public virtual string NewName { get; set; }
    }

    public partial class RenameTaxonomyFieldUniqueNameResponse
        : ResponseBase<string>
    {
    }

    [Route("/membership/users/meta/rename-field", "PUT")]
    public partial class RenameUserFieldUniqueName
        : CodeMashDbRequestBase, IReturn<RenameUserFieldUniqueNameResponse>
    {
        public virtual string OldName { get; set; }
        public virtual string NewName { get; set; }
    }

    public partial class RenameUserFieldUniqueNameResponse
        : ResponseBase<string>
    {
    }

    [Route("/account/plan/renew", "POST")]
    public partial class RenewCanceledSubscription
        : IReturn<RenewCanceledSubscriptionResponse>
    {
    }

    public partial class RenewCanceledSubscriptionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/users/{id}/invitation/resend", "POST")]
    public partial class ResendAccountUserInvitation
        : IReturn<ResendAccountUserInvitationResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class ResendAccountUserInvitationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/verify/resend", "POST")]
    public partial class ResendVerificationToken
        : IReturn<ResendVerificationTokenResponse>
    {
        public virtual Guid? UserId { get; set; }
    }

    public partial class ResendVerificationTokenResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/users/password/reset", "POST")]
    public partial class ResetAccountPassword
        : IReturn<ResetAccountPasswordResponse>
    {
        public virtual string Token { get; set; }
        public virtual string Password { get; set; }
        public virtual string RepeatedPassword { get; set; }
        public virtual Guid AccountId { get; set; }
    }

    public partial class ResetAccountPasswordResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/{id}/access/private", "PUT")]
    public partial class RevokePublicFileAccess
        : CodeMashRequestBase, IReturn<RevokePublicFileAccessResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class RevokePublicFileAccessResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/folders/access/private", "PUT")]
    public partial class RevokePublicFolderAccess
        : CodeMashRequestBase, IReturn<RevokePublicFolderAccessResponse>
    {
        public virtual string Path { get; set; }
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class RevokePublicFolderAccessResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/region-inform", "POST")]
    public partial class SaveInformDatabaseRegion
        : CodeMashRequestBase, IReturn<SaveInformDatabaseRegionResponse>
    {
        public virtual string DatabaseRegion { get; set; }
    }

    public partial class SaveInformDatabaseRegionResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Send email to recipients
    ///</summary>
    [Route("/notifications/email", "POST")]
    [Api(Description="Send email to recipients")]
    public partial class SendEmail
        : CodeMashRequestBase, IReturn<SendPushNotificationResponse>
    {
        public virtual string TemplateId { get; set; }
        public virtual long? Postpone { get; set; }
        public virtual bool RespectTimeZone { get; set; }
        public virtual bool DistinctEmail { get; set; }
        public virtual string Source { get; set; }
        public virtual string Action { get; set; }
        public virtual bool ForceRequestLanguage { get; set; }
    }

    [Route("/notifications/emails/{id}/send", "POST")]
    public partial class SendPostponedEmail
        : CodeMashRequestBase, IReturn<SendPostponedEmailResponse>
    {
        public virtual string Id { get; set; }
    }

    [Route("/notifications/email/groups/{id}/send", "POST")]
    public partial class SendPostponedEmailGroup
        : CodeMashRequestBase, IReturn<SendPostponedEmailGroupResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class SendPostponedEmailGroupResponse
        : ResponseBase<bool>
    {
    }

    public partial class SendPostponedEmailResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/push/{id}/send", "POST")]
    public partial class SendPostponedNotification
        : CodeMashRequestBase, IReturn<SendPostponedNotificationResponse>
    {
        public virtual string Id { get; set; }
    }

    [Route("/notifications/push/groups/{id}/send", "POST")]
    public partial class SendPostponedNotificationGroup
        : CodeMashRequestBase, IReturn<SendPostponedNotificationGroupResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class SendPostponedNotificationGroupResponse
        : ResponseBase<bool>
    {
    }

    public partial class SendPostponedNotificationResponse
        : ResponseBase<bool>
    {
    }

    ///<summary>
    ///Send email to recipients
    ///</summary>
    [Route("/notifications/push", "POST")]
    [Api(Description="Send email to recipients")]
    public partial class SendPushNotification
        : CodeMashRequestBase, IReturn<SendPushNotificationResponse>
    {
        public virtual string TemplateId { get; set; }
        public virtual long? Postpone { get; set; }
        public virtual bool RespectTimeZone { get; set; }
        public virtual bool IsNonPushable { get; set; }
        public virtual string Source { get; set; }
        public virtual string Action { get; set; }
        public virtual bool ForceRequestLanguage { get; set; }
        public virtual string UserOnBehalf { get; set; }
    }

    public partial class SendPushNotificationResponse
        : ResponseBase<string>
    {
    }

    [Route("/notifications/email/accounts/default", "PUT")]
    public partial class SetEmailAccountAsDefault
        : CodeMashRequestBase, IReturn<SetEmailAccountAsDefaultResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class SetEmailAccountAsDefaultResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/accounts/{id}/default", "PUT")]
    public partial class SetFileAccountAsDefault
        : CodeMashRequestBase, IReturn<SetFileAccountAsDefaultResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class SetFileAccountAsDefaultResponse
        : ResponseBase<bool>
    {
    }

    [Route("/shared/forms/auth/logout", "POST")]
    public partial class SharedFormLogout
        : CodeMashFormRequest
    {
    }

    [Route("/files/accounts/{id}/sync-files", "POST")]
    public partial class SyncFilesFromProvider
        : CodeMashRequestBase, IReturn<SyncFilesFromProviderResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class SyncFilesFromProviderResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/aggregates/test", "GET")]
    public partial class TestAggregate
        : CodeMashDbRequestBase, IReturn<TestAggregateResponse>
    {
        public TestAggregate()
        {
            Tokens = new Dictionary<string, string>{};
        }

        public virtual string Pipeline { get; set; }
        public virtual Dictionary<string, string> Tokens { get; set; }
    }

    public partial class TestAggregateResponse
        : ResponseBase<string>
    {
    }

    [Route("/serverless/connections/amazon/features-test", "POST")]
    public partial class TestAmazonAccountConnection
        : CodeMashRequestBase, IReturn<TestAmazonAccountConnectionResponse>
    {
        public TestAmazonAccountConnection()
        {
            Regions = new List<string>{};
            Tags = new Dictionary<string, string>{};
        }

        public virtual string AccessKey { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual List<string> Regions { get; set; }
        public virtual int RefreshRate { get; set; }
        public virtual Dictionary<string, string> Tags { get; set; }
    }

    public partial class TestAmazonAccountConnectionResponse
        : ResponseBase<ServerlessFeatureTestDto>
    {
    }

    [Route("/payments/accounts/apple/features-test", "POST")]
    public partial class TestAppleAppStoreConnection
        : CodeMashRequestBase, IReturn<TestAppleAppStoreConnectionResponse>
    {
        public virtual string DisplayName { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual Guid? PaymentAccountId { get; set; }
    }

    public partial class TestAppleAppStoreConnectionResponse
        : ResponseBase<PaymentFeatureTestDto>
    {
    }

    [Route("/db/clusters/features-test", "POST")]
    public partial class TestDatabaseAccountFeatures
        : CodeMashRequestBase, IReturn<TestDatabaseAccountFeaturesResponse>
    {
        public virtual string Provider { get; set; }
        public virtual string AtlasUserName { get; set; }
        public virtual string AtlasPassword { get; set; }
        public virtual string AtlasClusterId { get; set; }
        public virtual string AtlasClusterName { get; set; }
    }

    public partial class TestDatabaseAccountFeaturesResponse
        : ResponseBase<DatabaseFeatureTestDto>
    {
    }

    [Route("/payments/accounts/decta/features-test", "POST")]
    public partial class TestDectaAccountConnection
        : CodeMashRequestBase, IReturn<TestDectaAccountConnectionResponse>
    {
        public virtual string DisplayName { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual Guid? PaymentAccountId { get; set; }
    }

    public partial class TestDectaAccountConnectionResponse
        : ResponseBase<PaymentFeatureTestDto>
    {
    }

    public partial class TestEmailAccount
        : CodeMashRequestBase, IReturn<SendPushNotificationResponse>
    {
        public TestEmailAccount()
        {
            Emails = new List<string>{};
            Users = new List<Guid>{};
        }

        public virtual Guid Id { get; set; }
        public virtual List<string> Emails { get; set; }
        public virtual List<Guid> Users { get; set; }
    }

    [Route("/notifications/email/accounts/features-test", "POST")]
    public partial class TestEmailAccountFeatures
        : CodeMashRequestBase, IReturn<TestEmailAccountFeaturesResponse>
    {
        public virtual EmailProperties Model { get; set; }
        public virtual Guid? EmailAccountId { get; set; }
    }

    public partial class TestEmailAccountFeaturesResponse
        : ResponseBase<FeatureTestDto>
    {
    }

    [Route("/files/accounts/features-test", "POST")]
    public partial class TestFileAccountFeatures
        : CodeMashRequestBase, IReturn<TestFileAccountFeaturesResponse>
    {
        public virtual Guid? Id { get; set; }
        public virtual FileAccountProperties Model { get; set; }
    }

    public partial class TestFileAccountFeaturesResponse
        : ResponseBase<FileAccountFeatureTestDto>
    {
    }

    [Route("/serverless/connections/google/features-test", "POST")]
    public partial class TestGoogleAccountConnection
        : CodeMashRequestBase, IReturn<TestGoogleAccountConnectionResponse>
    {
        public TestGoogleAccountConnection()
        {
            Regions = new List<string>{};
        }

        public virtual string SecretKey { get; set; }
        public virtual List<string> Regions { get; set; }
        public virtual int RefreshRate { get; set; }
    }

    public partial class TestGoogleAccountConnectionResponse
        : ResponseBase<ServerlessFeatureTestDto>
    {
    }

    [Route("/payments/accounts/google/features-test", "POST")]
    public partial class TestGooglePlayStoreAccountConnection
        : CodeMashRequestBase, IReturn<TestGooglePlayStoreAccountConnectionResponse>
    {
        public virtual string DisplayName { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual Guid? PaymentAccountId { get; set; }
    }

    public partial class TestGooglePlayStoreAccountConnectionResponse
        : ResponseBase<PaymentFeatureTestDto>
    {
    }

    [Route("/payments/accounts/kevin/features-test", "POST")]
    public partial class TestKevinAccountConnection
        : CodeMashRequestBase, IReturn<TestKevinAccountConnectionResponse>
    {
        public virtual string DisplayName { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual Guid? PaymentAccountId { get; set; }
    }

    public partial class TestKevinAccountConnectionResponse
        : ResponseBase<PaymentFeatureTestDto>
    {
    }

    [Route("/payments/accounts/paysera/features-test", "POST")]
    public partial class TestPayseraAccountConnection
        : CodeMashRequestBase, IReturn<TestPayseraAccountConnectionResponse>
    {
        public virtual string DisplayName { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual string ValidationToken { get; set; }
        public virtual Guid? PaymentAccountId { get; set; }
    }

    public partial class TestPayseraAccountConnectionResponse
        : ResponseBase<PaymentFeatureTestDto>
    {
    }

    [Route("/notifications/push/accounts/features-test", "POST")]
    public partial class TestPushAccountFeatures
        : CodeMashRequestBase, IReturn<TestPushAccountFeaturesResponse>
    {
        public virtual PushAccountProperties Model { get; set; }
        public virtual Guid? PushAccountId { get; set; }
    }

    public partial class TestPushAccountFeaturesResponse
        : ResponseBase<PushFeatureTestDto>
    {
    }

    [Route("/serverless/functions/{id}/scheduler/execute", "POST")]
    public partial class TestSchedulerFunction
        : CodeMashRequestBase, IReturn<TestSchedulerFunctionResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class TestSchedulerFunctionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/accounts/stripe/features-test", "POST")]
    public partial class TestStripeAccountConnection
        : CodeMashRequestBase, IReturn<TestStripeAccountConnectionResponse>
    {
        public virtual string DisplayName { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual Guid? PaymentAccountId { get; set; }
    }

    public partial class TestStripeAccountConnectionResponse
        : ResponseBase<PaymentFeatureTestDto>
    {
    }

    [Route("/account/try-auth", "POST")]
    public partial class TryAuthenticate
        : IReturn<TryAuthenticateResponse>
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
    }

    public partial class TryAuthenticateResponse
        : ResponseBase<AuthenticateResponse>
    {
        public TryAuthenticateResponse()
        {
            AvailableAccounts = new List<AccountListItemDto>{};
        }

        public virtual List<AccountListItemDto> AvailableAccounts { get; set; }
        public virtual string ApiLogin { get; set; }
    }

    [Route("/account/users/{Id}/unblock", "PUT")]
    public partial class UnblockUser
        : RequestBase, IReturn<UnblockUserResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class UnblockUserResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/email/templates/attachments", "POST")]
    public partial class UpdaloadEmailTemplateAttachment
        : CodeMashRequestBase, IReturn<UpdaloadEmailTemplateAttachmentResponse>
    {
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class UpdaloadEmailTemplateAttachmentResponse
        : ResponseBase<bool>
    {
        public virtual string Key { get; set; }
    }

    [Route("/membership/authorization", "PUT")]
    public partial class UpdateAuthorization
        : CodeMashRequestBase, IReturn<UpdateAuthorizationResponse>
    {
        public UpdateAuthorization()
        {
            AllowedRegisterRoles = new List<string>{};
            AllowedProviderRegisterRoles = new List<string>{};
        }

        public virtual string Setting { get; set; }
        public virtual string UserRegistersAsRole { get; set; }
        public virtual string GuestRegistersAsRole { get; set; }
        public virtual List<string> AllowedRegisterRoles { get; set; }
        public virtual List<string> AllowedProviderRegisterRoles { get; set; }
        public virtual bool NeedVerification { get; set; }
        public virtual string VerifyUserCallback { get; set; }
        public virtual Guid VerificationEmailTemplate { get; set; }
        public virtual Guid DeactivationEmailTemplate { get; set; }
        public virtual bool SendWelcomeEmail { get; set; }
        public virtual Guid WelcomeEmailTemplate { get; set; }
        public virtual bool AllowResetPassword { get; set; }
        public virtual Guid ResetPasswordEmailTemplate { get; set; }
        public virtual string ResetPasswordCallback { get; set; }
        public virtual bool AllowInviteUsers { get; set; }
        public virtual bool AllowDeactivateUsers { get; set; }
        public virtual Guid InviteUserEmailTemplate { get; set; }
        public virtual string InviteUserCallback { get; set; }
        public virtual string DeactivateUserCallback { get; set; }
        public virtual long? ResetPasswordTokenExpiration { get; set; }
        public virtual long? InvitationExpiration { get; set; }
        public virtual long? EmailVerificationExpiration { get; set; }
        public virtual long? DeactivationExpiration { get; set; }
        public virtual bool DefaultSubscribeToNews { get; set; }
    }

    public partial class UpdateAuthorizationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/backups/settings", "PUT")]
    public partial class UpdateBackupSettings
        : CodeMashRequestBase, IReturn<UpdateBackupSettingsResponse>
    {
        public virtual Guid? Cluster { get; set; }
        public virtual int Hour { get; set; }
        public virtual int Copies { get; set; }
    }

    public partial class UpdateBackupSettingsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/billing", "PUT")]
    public partial class UpdateBillingDetails
        : IReturn<UpdateBillingDetailsResponse>
    {
        public virtual BillingType Type { get; set; }
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
    }

    public partial class UpdateBillingDetailsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/card", "PUT")]
    public partial class UpdateCard
        : IReturn<UpdateCardResponse>
    {
        public virtual string SetupIntentId { get; set; }
        public virtual string ClientSecret { get; set; }
    }

    public partial class UpdateCardResponse
        : ResponseBase<bool>
    {
        public virtual string SubscriptionError { get; set; }
        public virtual CardDto Card { get; set; }
    }

    [Route("/db/clusters/{id}", "PUT")]
    public partial class UpdateCluster
        : CodeMashRequestBase, IReturn<UpdateClusterResponse>
    {
        public UpdateCluster()
        {
            MultiRegions = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string Cluster { get; set; }
        public virtual int StorageSize { get; set; }
        public virtual string DatabaseRegion { get; set; }
        public virtual bool AutoScaling { get; set; }
        public virtual bool EnableMultiRegion { get; set; }
        public virtual List<string> MultiRegions { get; set; }
        public virtual string AtlasUserName { get; set; }
        public virtual string AtlasPassword { get; set; }
        public virtual string AtlasClusterId { get; set; }
        public virtual string AtlasClusterName { get; set; }
    }

    public partial class UpdateClusterResponse
        : ResponseBase<ClusterDto>
    {
    }

    [Route("/db/{collectionName}/forms", "PUT")]
    public partial class UpdateCollectionForm
        : CodeMashDbRequestBase, IReturn<UpdateCollectionFormResponse>
    {
        public UpdateCollectionForm()
        {
            Authentications = new List<string>{};
        }

        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsPublic { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual string DisabledMessage { get; set; }
        public virtual bool LimitOneResponse { get; set; }
        public virtual bool CanEdit { get; set; }
        public virtual string Cluster { get; set; }
        public virtual List<string> Authentications { get; set; }
    }

    public partial class UpdateCollectionFormResponse
        : ResponseBase<string>
    {
    }

    [Route("/db/{collectionName}/forms/token", "PUT")]
    public partial class UpdateCollectionFormToken
        : CodeMashDbRequestBase, IReturn<UpdateCollectionFormTokenResponse>
    {
    }

    public partial class UpdateCollectionFormTokenResponse
        : ResponseBase<string>
    {
    }

    [Route("/db/triggers/{id}", "PUT")]
    public partial class UpdateCollectionTrigger
        : CodeMashDbRequestBase, IReturn<UpdateCollectionTriggerResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual SchemaTriggerCreateDto Trigger { get; set; }
    }

    public partial class UpdateCollectionTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/customer/payment", "PUT")]
    public partial class UpdateCustomerPayment
        : IReturn<UpdateCustomerPaymentResponse>
    {
        public virtual string SetupIntentId { get; set; }
        public virtual string ClientSecret { get; set; }
    }

    public partial class UpdateCustomerPaymentResponse
        : ResponseBase<CardDto>
    {
    }

    [Route("/serverless/custom/functions/{id}", "PUT")]
    public partial class UpdateCustomFunction
        : CodeMashRequestBase, IReturn<UpdateCustomFunctionResponse>
    {
        public UpdateCustomFunction()
        {
            Environment = new Dictionary<string, string>{};
            Tags = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Runtime { get; set; }
        public virtual string Handler { get; set; }
        public virtual string Description { get; set; }
        public virtual int TimeoutMinutes { get; set; }
        public virtual int TimeoutSeconds { get; set; }
        public virtual long Memory { get; set; }
        public virtual Dictionary<string, string> Environment { get; set; }
        public virtual string Template { get; set; }
        public virtual List<string> Tags { get; set; }
        public virtual string ServiceAccount { get; set; }
    }

    [Route("/serverless/custom/functions/{id}/aliases/{name}", "PUT")]
    public partial class UpdateCustomFunctionAlias
        : CodeMashRequestBase, IReturn<UpdateCustomFunctionAliasResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Version { get; set; }
        public virtual string AdditionalVersion { get; set; }
        public virtual int AdditionalVersionWeight { get; set; }
    }

    public partial class UpdateCustomFunctionAliasResponse
        : ResponseBase<bool>
    {
    }

    [Route("/serverless/custom/functions/{id}/code", "PUT")]
    public partial class UpdateCustomFunctionCode
        : CodeMashRequestBase, IReturn<UpdateCustomFunctionCodeResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual string FileId { get; set; }
    }

    public partial class UpdateCustomFunctionCodeResponse
        : ResponseBase<bool>
    {
    }

    public partial class UpdateCustomFunctionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/settings", "PATCH")]
    public partial class UpdateDatabaseSettings
        : CodeMashRequestBase, IReturn<UpdateDatabaseSettingsResponse>
    {
        public virtual Guid? ClusterId { get; set; }
        public virtual string Setting { get; set; }
        public virtual string DisplayName { get; set; }
    }

    public partial class UpdateDatabaseSettingsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/clusters/{id}/default", "POST")]
    public partial class UpdateDefaultCluster
        : CodeMashRequestBase, IReturn<UpdateDefaultClusterResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class UpdateDefaultClusterResponse
        : ResponseBase<bool>
    {
    }

    [Route("/project/language/default", "POST")]
    public partial class UpdateDefaultProjectLanguage
        : CodeMashRequestBase, IReturn<UpdateDefaultProjectLanguageResponse>
    {
        public virtual string Language { get; set; }
    }

    public partial class UpdateDefaultProjectLanguageResponse
        : ResponseBase<bool>
    {
    }

    [Route("/project/timezone", "POST")]
    public partial class UpdateDefaultProjectTimeZone
        : CodeMashRequestBase, IReturn<UpdateDefaultProjectTimeZoneResponse>
    {
        public virtual string TimeZone { get; set; }
    }

    public partial class UpdateDefaultProjectTimeZoneResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/email/settings/preferences", "POST")]
    public partial class UpdateEmailPreferences
        : CodeMashRequestBase, IReturn<UpdateEmailPreferencesResponse>
    {
        public UpdateEmailPreferences()
        {
            Texts = new Dictionary<string, EmailPreferenceTexts>{};
        }

        public virtual Dictionary<string, EmailPreferenceTexts> Texts { get; set; }
    }

    public partial class UpdateEmailPreferencesResponse
        : ResponseBase<bool>
    {
    }

    [Route("/files/triggers/{id}", "PUT")]
    public partial class UpdateFilesTrigger
        : CodeMashRequestBase, IReturn<UpdateFilesTriggerResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual FilesTriggerCreateDto Trigger { get; set; }
    }

    public partial class UpdateFilesTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/serverless/functions/{id}", "PUT")]
    public partial class UpdateFunction
        : CodeMashRequestBase, IReturn<UpdateFunctionResponse>
    {
        public UpdateFunction()
        {
            Meta = new Dictionary<string, string>{};
            AvailableTokens = new List<string>{};
            TokenResolvers = new Dictionary<string, TokenResolverField>{};
            Tags = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string Template { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Description { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual int TimeoutMinutes { get; set; }
        public virtual int TimeoutSeconds { get; set; }
        public virtual long Memory { get; set; }
        public virtual List<string> AvailableTokens { get; set; }
        public virtual Dictionary<string, TokenResolverField> TokenResolvers { get; set; }
        public virtual List<string> Tags { get; set; }
        public virtual string ServiceAccount { get; set; }
    }

    public partial class UpdateFunctionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/push/templates/{id}", "PUT")]
    public partial class UpdateNotificationTemplate
        : CodeMashRequestBase, IReturn<UpdateNotificationTemplateResponse>
    {
        public UpdateNotificationTemplate()
        {
            Meta = new Dictionary<string, string>{};
            Buttons = new List<PushNotificationButtons>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual NotificationPriority Priority { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual string Data { get; set; }
        public virtual int? Ttl { get; set; }
        public virtual string Url { get; set; }
        public virtual string Code { get; set; }
        public virtual string CollapseId { get; set; }
        public virtual string Image { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual List<PushNotificationButtons> Buttons { get; set; }
        public virtual Guid? AccountId { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual int? IosBadge { get; set; }
        public virtual string IosCategory { get; set; }
        public virtual string IosSound { get; set; }
        public virtual bool IosContentAvailable { get; set; }
        public virtual string IosAnalyticsLabel { get; set; }
        public virtual string IosAppBundleId { get; set; }
        public virtual string IosGroupId { get; set; }
        public virtual string IosPushType { get; set; }
        public virtual string IosLaunchImage { get; set; }
        public virtual string AndroidChannelId { get; set; }
        public virtual string AndroidGroup { get; set; }
        public virtual string AndroidGroupMessage { get; set; }
        public virtual string RestrictedPackageName { get; set; }
        public virtual string AndroidSound { get; set; }
        public virtual string AndroidVisibility { get; set; }
        public virtual string AndroidLedColor { get; set; }
        public virtual string AndroidLightOnDuration { get; set; }
        public virtual string AndroidLightOffDuration { get; set; }
        public virtual string AndroidAccentColor { get; set; }
        public virtual bool AndroidSticky { get; set; }
        public virtual bool AndroidDefaultLight { get; set; }
        public virtual bool AndroidDefaultVibration { get; set; }
        public virtual string AndroidAnalyticsLabel { get; set; }
        public virtual string AndroidVibrateTimings { get; set; }
        public virtual string AndroidSmallIcon { get; set; }
        public virtual string AndroidLargeIcon { get; set; }
        public virtual AndroidBackgroundLayout AndroidBackgroundLayout { get; set; }
    }

    public partial class UpdateNotificationTemplateResponse
        : ResponseBase<bool>
    {
        public virtual string TitleError { get; set; }
        public virtual string BodyError { get; set; }
        public virtual string SubtitleError { get; set; }
        public virtual string DataError { get; set; }
    }

    [Route("/account/profile/password", "PUT")]
    public partial class UpdatePassword
        : RequestBase, IReturn<UpdatePasswordResponse>
    {
        public virtual string CurrentPassword { get; set; }
        public virtual string Password { get; set; }
        public virtual string RepeatedPassword { get; set; }
    }

    [Route("/membership/authorization/password-complexity", "PUT")]
    public partial class UpdatePasswordComplexity
        : CodeMashRequestBase, IReturn<UpdatePasswordComplexityResponse>
    {
        public virtual int? MinLength { get; set; }
        public virtual int? MaxLength { get; set; }
        public virtual int? MinNumbers { get; set; }
        public virtual int? MaxNumbers { get; set; }
        public virtual int? MinUpper { get; set; }
        public virtual int? MaxUpper { get; set; }
        public virtual int? MinLower { get; set; }
        public virtual int? MaxLower { get; set; }
        public virtual int? MinSpecial { get; set; }
        public virtual int? MaxSpecial { get; set; }
        public virtual string AllowedSpecial { get; set; }
    }

    public partial class UpdatePasswordComplexityResponse
        : ResponseBase<bool>
    {
    }

    public partial class UpdatePasswordResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/accounts/{id}", "PUT")]
    public partial class UpdatePaymentAccount
        : CodeMashRequestBase, IReturn<UpdatePaymentAccountResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual PaymentAccountProperties Model { get; set; }
    }

    public partial class UpdatePaymentAccountResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/plans/{id}", "PUT")]
    public partial class UpdatePaymentPlan
        : CodeMashRequestBase, IReturn<UpdatePaymentPlanResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual PaymentPlanProperties Model { get; set; }
    }

    public partial class UpdatePaymentPlanResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/settings", "PUT")]
    public partial class UpdatePaymentSettings
        : CodeMashRequestBase, IReturn<UpdatePaymentSettingsResponse>
    {
        public virtual string Setting { get; set; }
        public virtual long? PayseraTimeLimit { get; set; }
        public virtual bool AllowGuests { get; set; }
        public virtual bool PayseraAllowTest { get; set; }
        public virtual string PayseraOnlyPayments { get; set; }
        public virtual string PayseraBlockedPayments { get; set; }
        public virtual string PayseraLanguage { get; set; }
        public virtual bool PayseraLanguageByIp { get; set; }
        public virtual string OrderPrefix { get; set; }
        public virtual bool StripeSubscriptionCancelInstant { get; set; }
        public virtual bool StripeSubscriptionRefundOnCancelInstant { get; set; }
        public virtual bool StripeSubscriptionRefundOnChange { get; set; }
        public virtual bool StripeApplyPreviousCouponOnChange { get; set; }
        public virtual int? StripeMaximumSubscriptions { get; set; }
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class UpdatePaymentSettingsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/triggers/{id}", "PUT")]
    public partial class UpdatePaymentTrigger
        : CodeMashRequestBase, IReturn<UpdatePaymentTriggerResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual PaymentTriggerCreateDto Trigger { get; set; }
    }

    public partial class UpdatePaymentTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/profile", "PUT")]
    public partial class UpdateProfile
        : CodeMashRequestBase, IReturn<UpdateProfileResponse>
    {
        public virtual string DisplayName { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Country { get; set; }
        public virtual string BillingEmail { get; set; }
    }

    public partial class UpdateProfileResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/{projectId}/origins", "PUT")]
    public partial class UpdateProjectAllowedOrigins
        : CodeMashRequestBase, IReturn<UpdateProjectAllowedOriginsResponse>
    {
        public UpdateProjectAllowedOrigins()
        {
            Origins = new List<string>{};
        }

        public virtual List<string> Origins { get; set; }
    }

    public partial class UpdateProjectAllowedOriginsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/{projectId}/billing/charge", "PUT")]
    public partial class UpdateProjectBillingChargeSettings
        : CodeMashRequestBase, IReturn<UpdateProjectBillingChargeSettingsResponse>
    {
        public virtual CustomerBillingType BillingType { get; set; }
        public virtual CustomerMarginType MarginType { get; set; }
        public virtual double MarginPercent { get; set; }
        public virtual decimal FixedPrice { get; set; }
        public virtual bool ChargeCustomer { get; set; }
    }

    public partial class UpdateProjectBillingChargeSettingsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/{projectId}/billing/details", "PUT")]
    public partial class UpdateProjectBillingDetailsSettings
        : CodeMashRequestBase, IReturn<UpdateProjectBillingDetailsSettingsResponse>
    {
        public virtual BillingType Type { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Organization { get; set; }
        public virtual string Vat { get; set; }
        public virtual string Address { get; set; }
        public virtual string Zip { get; set; }
        public virtual string Country { get; set; }
        public virtual string City { get; set; }
        public virtual string Phone { get; set; }
        public virtual string BillingEmail { get; set; }
    }

    public partial class UpdateProjectBillingDetailsSettingsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/{projectId}/billing/invoice", "PUT")]
    public partial class UpdateProjectBillingInvoiceSettings
        : CodeMashRequestBase, IReturn<UpdateProjectBillingInvoiceSettingsResponse>
    {
        public virtual string NumberPrefix { get; set; }
    }

    public partial class UpdateProjectBillingInvoiceSettingsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/{projectId}/billing/status", "PUT")]
    public partial class UpdateProjectBillingStatus
        : CodeMashRequestBase, IReturn<UpdateProjectBillingStatusResponse>
    {
    }

    public partial class UpdateProjectBillingStatusResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/{projectId}/description", "PUT")]
    public partial class UpdateProjectDescription
        : CodeMashRequestBase, IReturn<UpdateProjectDescriptionResponse>
    {
        public virtual string Description { get; set; }
    }

    public partial class UpdateProjectDescriptionResponse
        : ResponseBase<bool>
    {
    }

    [Route("/project/languages", "POST")]
    public partial class UpdateProjectLanguages
        : CodeMashRequestBase, IReturn<UpdateProjectLanguagesResponse>
    {
        public UpdateProjectLanguages()
        {
            Languages = new List<string>{};
        }

        public virtual List<string> Languages { get; set; }
    }

    public partial class UpdateProjectLanguagesResponse
        : ResponseBase<bool>
    {
    }

    [Route("/project/logo", "PUT")]
    public partial class UpdateProjectLogo
        : CodeMashRequestBase, IReturn<UpdateProjectLogoResponse>
    {
    }

    public partial class UpdateProjectLogoResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/change-name", "PUT")]
    public partial class UpdateProjectName
        : CodeMashRequestBase, IReturn<UpdateProjectNameResponse>
    {
        public virtual string Name { get; set; }
    }

    public partial class UpdateProjectNameResponse
        : ResponseBase<bool>
    {
        public virtual string SlugifiedName { get; set; }
    }

    [Route("/project/tokens", "PUT")]
    public partial class UpdateProjectToken
        : CodeMashRequestBase, IReturn<UpdateProjectTokenResponse>
    {
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
    }

    public partial class UpdateProjectTokenResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/{projectId}/url", "PUT")]
    public partial class UpdateProjectUrl
        : CodeMashRequestBase, IReturn<UpdateProjectUrlResponse>
    {
        public virtual string Url { get; set; }
    }

    public partial class UpdateProjectUrlResponse
        : ResponseBase<bool>
    {
    }

    [Route("/project/user-zones", "POST")]
    public partial class UpdateProjectUserZones
        : CodeMashRequestBase, IReturn<UpdateProjectUserZonesResponse>
    {
        public UpdateProjectUserZones()
        {
            Zones = new List<string>{};
        }

        public virtual List<string> Zones { get; set; }
    }

    public partial class UpdateProjectUserZonesResponse
        : ResponseBase<bool>
    {
    }

    [Route("/projects/{projectId}/widgets", "PUT")]
    public partial class UpdateProjectWidget
        : CodeMashRequestBase, IReturn<UpdateProjectWidgetResponse>
    {
        public virtual string Type { get; set; }
        public virtual Modules Module { get; set; }
    }

    public partial class UpdateProjectWidgetResponse
        : ResponseBase<bool>
    {
    }

    [Route("/project/zone", "POST")]
    public partial class UpdateProjectZone
        : CodeMashRequestBase, IReturn<UpdateProjectZoneResponse>
    {
        public virtual string ZoneName { get; set; }
    }

    public partial class UpdateProjectZoneResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/roles/{formerName}", "PUT")]
    public partial class UpdateRole
        : CodeMashRequestBase, IReturn<UpdateRoleResponse>
    {
        public UpdateRole()
        {
            Policies = new List<string>{};
        }

        public virtual string FormerName { get; set; }
        public virtual string Name { get; set; }
        public virtual List<string> Policies { get; set; }
    }

    public partial class UpdateRoleResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/aggregates/{id}", "PUT")]
    public partial class UpdateSchemaAggregate
        : CodeMashRequestBase, IReturn<UpdateSchemaAggregateResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Query { get; set; }
    }

    public partial class UpdateSchemaAggregateResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/notifications/server-events/settings", "PUT")]
    public partial class UpdateServerEventsSettings
        : CodeMashRequestBase, IReturn<UpdateServerEventsSettingsResponse>
    {
        public virtual string Setting { get; set; }
        public virtual Guid? FileAccountId { get; set; }
        public virtual bool SendPush { get; set; }
    }

    public partial class UpdateServerEventsSettingsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/shared/forms/response", "PUT")]
    public partial class UpdateSharedForm
        : CodeMashFormRequest, IReturn<UpdateSharedFormResponse>
    {
        public virtual string Document { get; set; }
    }

    public partial class UpdateSharedFormResponse
        : ResponseBase<bool>
    {
    }

    [Route("/account/plan", "PUT")]
    public partial class UpdateSubscription
        : IReturn<UpdateSubscriptionResponse>
    {
        public virtual SubscriptionPlan Plan { get; set; }
        public virtual string SetupIntentId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string DiscountCode { get; set; }
    }

    public partial class UpdateSubscriptionResponse
        : ResponseBase<bool>
    {
        public UpdateSubscriptionResponse()
        {
            PaymentErrors = new List<string>{};
            ProjectErrors = new List<ProjectPlanChangeCheckDto>{};
        }

        public virtual List<string> PaymentErrors { get; set; }
        public virtual List<ProjectPlanChangeCheckDto> ProjectErrors { get; set; }
        public virtual CardDto Card { get; set; }
    }

    [Route("/db/taxonomies/{collectionName}/terms/{termId}", "PUT")]
    public partial class UpdateTerm
        : CodeMashDbRequestBase, IReturn<InsertOneResponse>
    {
        public UpdateTerm()
        {
            Dependencies = new Dictionary<string, List<String>>{};
        }

        public virtual string TermId { get; set; }
        public virtual string Document { get; set; }
        public virtual string Meta { get; set; }
        public virtual Dictionary<string, List<String>> Dependencies { get; set; }
        public virtual string Parent { get; set; }
    }

    [Route("/account/users/{id}", "PUT")]
    public partial class UpdateUser
        : CodeMashRequestBase, IReturn<UpdateUserResponse>
    {
        public UpdateUser()
        {
            Permissions = new List<string>{};
            AllowedProjects = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual List<string> Permissions { get; set; }
        public virtual List<string> AllowedProjects { get; set; }
    }

    [Route("/notifications/email/user/preferences", "PUT")]
    public partial class UpdateUserEmailPreferences
        : IReturn<UpdateUserEmailPreferencesResponse>
    {
        public UpdateUserEmailPreferences()
        {
            UnsubscribedNewsTags = new List<string>{};
        }

        public virtual string Token { get; set; }
        public virtual bool UnsubscribeFromAll { get; set; }
        public virtual List<string> UnsubscribedNewsTags { get; set; }
    }

    public partial class UpdateUserEmailPreferencesResponse
        : ResponseBase<bool>
    {
    }

    public partial class UpdateUserResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/triggers/{id}", "PUT")]
    public partial class UpdateUsersTrigger
        : CodeMashRequestBase, IReturn<UpdateUsersTriggerResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual UsersTriggerCreateDto Trigger { get; set; }
    }

    public partial class UpdateUsersTriggerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/shared/forms/files", "POST")]
    public partial class UploadFormFile
        : CodeMashFormRequest, IReturn<UploadFormFileResponse>
    {
        public virtual string UniqueFieldName { get; set; }
    }

    public partial class UploadFormFileResponse
        : ResponseBase<File>
    {
        public virtual string Key { get; set; }
    }

    [Route("/serverless/custom/functions/upload", "POST")]
    public partial class UploadFunctionFile
        : CodeMashRequestBase, IReturn<UploadFunctionFileResponse>
    {
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class UploadFunctionFileResponse
        : ResponseBase<string>
    {
        public virtual string Key { get; set; }
    }

    [Route("/notifications/push/templates/attachments", "POST")]
    public partial class UploadPushTemplateAttachment
        : CodeMashRequestBase, IReturn<UploadPushTemplateAttachmentResponse>
    {
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class UploadPushTemplateAttachmentResponse
        : ResponseBase<bool>
    {
        public virtual string Key { get; set; }
    }

    [Route("/db/imports/upload", "POST")]
    public partial class UploadSchemaImportFile
        : CodeMashRequestBase, IReturn<UploadSchemaImportFileResponse>
    {
        public virtual string Delimiter { get; set; }
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class UploadSchemaImportFileResponse
        : ResponseBase<List<Object>>
    {
        public virtual string Key { get; set; }
        public virtual string Delimiter { get; set; }
    }

    [Route("/serverless/functions/files/upload", "POST")]
    public partial class UploadServerlessFile
        : CodeMashRequestBase, IReturn<UploadServerlessFileResponse>
    {
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class UploadServerlessFileResponse
        : ResponseBase<bool>
    {
        public virtual string Key { get; set; }
    }

    [Route("/db/taxonomies/{collectionName}/files", "POST")]
    public partial class UploadTermFileRequest
        : CodeMashDbRequestBase, IReturn<UploadRecordFileResponse>
    {
        public virtual string UniqueFieldName { get; set; }
    }

    [Route("/account/verify", "POST")]
    public partial class VerifyAccount
        : IReturn<VerifyAccountResponse>
    {
        public virtual string Token { get; set; }
    }

    public partial class VerifyAccountResponse
        : ResponseBase<bool>
    {
        public virtual AccountListItemDto AccountDetails { get; set; }
    }

    [Route("/account/users/verify", "POST")]
    public partial class VerifyAccountUser
        : IReturn<VerifyAccountUserResponse>
    {
        public virtual string Token { get; set; }
        public virtual string Password { get; set; }
        public virtual string RepeatedPassword { get; set; }
    }

    public partial class VerifyAccountUserResponse
        : ResponseBase<AuthenticateResponse>
    {
        public virtual string ApiLogin { get; set; }
    }

    [Route("/db/schema/{schemaId}", "DELETE")]
    public partial class DeleteSchema
        : CodeMashRequestBase, IReturn<DeleteSchemaResponse>
    {
        public virtual Guid SchemaId { get; set; }
    }

    public partial class DeleteSchemaResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/schema", "PUT")]
    public partial class EditSchema
        : CodeMashRequestBase, IReturn<EditSchemaResponse>
    {
        public virtual Guid SchemaId { get; set; }
        public virtual string UiSchema { get; set; }
        public virtual string JsonSchema { get; set; }
        public virtual string MongoDbSchema { get; set; }
    }

    public partial class EditSchemaResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/db/schemas/{collectionName}/records/{id}/restore", "POST")]
    public partial class RestoreDeletedRecord
        : CodeMashDbRequestBase, IReturn<RestoreDeletedRecordResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class RestoreDeletedRecordResponse
        : ResponseBase<bool>
    {
    }

    [Route("/db/schemas/{id}/settings", "PUT")]
    public partial class UpdateSchemaSettings
        : CodeMashRequestBase, IReturn<UpdateSchemaSettingsResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual bool SoftDelete { get; set; }
    }

    public partial class UpdateSchemaSettingsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/push/devices/bulk", "DELETE")]
    public partial class DeleteManyDevices
        : CodeMashRequestBase, IReturn<DeleteManyDevicesResponse>
    {
        public DeleteManyDevices()
        {
            Ids = new List<Guid>{};
        }

        [DataMember(Name="ids[]")]
        public virtual List<Guid> Ids { get; set; }
    }

    public partial class DeleteManyDevicesResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/push/disable", "GET")]
    public partial class DisableNotification
        : CodeMashRequestBase, IReturn<DisableNotificationResponse>
    {
    }

    public partial class DisableNotificationResponse
        : ResponseBase<bool>
    {
    }

    public partial class DisableTranslation
        : CodeMashRequestBase, IReturn<DisableTranslationResponse>
    {
    }

    public partial class DisableTranslationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/notifications/push/enable", "POST")]
    public partial class EnableNotification
        : CodeMashRequestBase, IReturn<EnableNotificationResponse>
    {
        public virtual PushAccountProperties Model { get; set; }
    }

    public partial class EnableNotificationResponse
        : ResponseBase<Guid>
    {
    }

    public partial class EnableTranslation
        : CodeMashRequestBase, IReturn<EnableTranslationResponse>
    {
    }

    public partial class EnableTranslationResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/notifications/push/devices/metadata", "GET")]
    public partial class GetDevicesMetaData
        : CodeMashRequestBase, IReturn<GetDevicesMetaDataResponse>
    {
    }

    public partial class GetDevicesMetaDataResponse
        : ResponseBase<List<KeyValuesItem>>
    {
    }

    public partial class CreateCampaign
        : CodeMashRequestBase, IReturn<CreateCampaignResponse>
    {
        public virtual Guid TemplateId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string SendDate { get; set; }
        public virtual CampaignType Type { get; set; }
        public virtual CampaignRepeatType RepeatType { get; set; }
        public virtual CampaignTargetType Target { get; set; }
        public virtual bool IsDraft { get; set; }
        public virtual string Tokens { get; set; }
        public virtual string Tags { get; set; }
    }

    public partial class CreateCampaignResponse
        : ResponseBase<Guid>
    {
    }

    public partial class DeleteCampaign
        : CodeMashRequestBase, IReturn<DeleteCampaignResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteCampaignResponse
        : ResponseBase<bool>
    {
    }

    public partial class GetCampaign
        : CodeMashRequestBase, IReturn<GetCampaignResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetCampaignResponse
        : ResponseBase<Campaign>
    {
    }

    public partial class GetCampaigns
        : CodeMashRequestBase, IReturn<GetCampaignsResponse>
    {
        public virtual int Status { get; set; }
    }

    public partial class GetCampaignsResponse
        : ResponseBase<List<CampaignListDto>>
    {
    }

    public partial class ImportContacts
        : CodeMashRequestBase, IReturn<ImportContactsResponse>
    {
        public virtual string ImportOptions { get; set; }
        public virtual string Tags { get; set; }
    }

    public partial class ImportContactsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/disable", "GET")]
    public partial class DisableMembership
        : CodeMashRequestBase, IReturn<DisableMembershipResponse>
    {
    }

    public partial class DisableMembershipResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/enable", "GET")]
    public partial class EnableMembership
        : CodeMashRequestBase, IReturn<EnableMembershipResponse>
    {
    }

    public partial class EnableMembershipResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/membership/users/{id}/reactivate", "POST")]
    public partial class ReactivateUser
        : CodeMashRequestBase, IReturn<ReactivateUserResponse>, IRequestBase
    {
        public virtual Guid Id { get; set; }
    }

    public partial class ReactivateUserResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/users/service/{id}/key/regenerate", "PUT")]
    public partial class RegenerateServiceUserToken
        : CodeMashRequestBase, IReturn<RegenerateServiceUserTokenResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class RegenerateServiceUserTokenResponse
        : ResponseBase<string>
    {
    }

    [Route("/membership/users/service/register", "POST")]
    public partial class RegisterServiceUser
        : CodeMashRequestBase, IReturn<RegisterServiceUserResponse>
    {
        public RegisterServiceUser()
        {
            RolesTree = new List<UserRoleUpdateInput>{};
        }

        public virtual string DisplayName { get; set; }
        public virtual string Zone { get; set; }
        public virtual List<UserRoleUpdateInput> RolesTree { get; set; }
    }

    public partial class RegisterServiceUserResponse
        : ResponseBase<string>
    {
    }

    [Route("/membership/users/service", "PUT")]
    public partial class UpdateServiceUser
        : CodeMashRequestBase, IReturn<UpdateServiceUserResponse>, IRequestBase
    {
        public UpdateServiceUser()
        {
            RolesTree = new List<UserRoleUpdateInput>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Zone { get; set; }
        public virtual List<UserRoleUpdateInput> RolesTree { get; set; }
    }

    public partial class UpdateServiceUserResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/users/meta", "PUT")]
    public partial class UpdateUserMetaSchema
        : CodeMashRequestBase, IReturn<UpdateUserMetaSchemaResponse>, IRequestBase
    {
        public virtual string JsonSchema { get; set; }
        public virtual string UiSchema { get; set; }
    }

    public partial class UpdateUserMetaSchemaResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/google/services", "POST")]
    public partial class CreateGoogleServiceAccount
        : CodeMashRequestBase, IReturn<CreateGoogleServiceAccountResponse>
    {
        public CreateGoogleServiceAccount()
        {
            Scopes = new List<string>{};
        }

        public virtual string Name { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual List<string> Scopes { get; set; }
    }

    public partial class CreateGoogleServiceAccountResponse
        : ResponseBase<string>
    {
        public virtual string ClientId { get; set; }
        public virtual string PrivateKeyEnding { get; set; }
    }

    [Route("/membership/authentication/google/services/{id}", "DELETE")]
    public partial class DeleteGoogleServiceAccount
        : CodeMashRequestBase, IReturn<DeleteGoogleServiceAccountResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteGoogleServiceAccountResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/aad/disable", "GET")]
    public partial class DisableAadAuthentication
        : CodeMashRequestBase, IReturn<DisableAadAuthenticationResponse>
    {
    }

    public partial class DisableAadAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/apple/disable", "GET")]
    public partial class DisableAppleAuthentication
        : CodeMashRequestBase, IReturn<DisableAppleAuthenticationResponse>
    {
    }

    public partial class DisableAppleAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/facebook/disable", "GET")]
    public partial class DisableFacebookAuthentication
        : CodeMashRequestBase, IReturn<DisableFacebookAuthenticationResponse>
    {
    }

    public partial class DisableFacebookAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/google/disable", "GET")]
    public partial class DisableGoogleAuthentication
        : CodeMashRequestBase, IReturn<DisableGoogleAuthenticationResponse>
    {
    }

    public partial class DisableGoogleAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/twitter/disable", "GET")]
    public partial class DisableTwitterAuthentication
        : CodeMashRequestBase, IReturn<DisableTwitterAuthenticationResponse>
    {
    }

    public partial class DisableTwitterAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/aad/enable", "POST")]
    public partial class EnableAadAuthentication
        : CodeMashRequestBase, IReturn<EnableAadAuthenticationResponse>
    {
        public EnableAadAuthentication()
        {
            Modes = new List<AzureActiveDirSettingsMode>{};
        }

        public virtual string TenantId { get; set; }
        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual List<AzureActiveDirSettingsMode> Modes { get; set; }
        public virtual bool EnableWithOldSettings { get; set; }
    }

    public partial class EnableAadAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/apple/enable", "POST")]
    public partial class EnableAppleAuthentication
        : CodeMashRequestBase, IReturn<EnableAppleAuthenticationResponse>
    {
        public EnableAppleAuthentication()
        {
            Modes = new List<AppleOAuthSettingsMode>{};
        }

        public virtual string ServiceId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string KeyId { get; set; }
        public virtual string BundleId { get; set; }
        public virtual string TeamId { get; set; }
        public virtual string RedirectUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual List<AppleOAuthSettingsMode> Modes { get; set; }
        public virtual bool EnableWithOldSettings { get; set; }
    }

    public partial class EnableAppleAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/facebook/enable", "POST")]
    public partial class EnableFacebookAuthentication
        : CodeMashRequestBase, IReturn<EnableFacebookAuthenticationResponse>
    {
        public EnableFacebookAuthentication()
        {
            Modes = new List<FacebookSettingsMode>{};
        }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual List<FacebookSettingsMode> Modes { get; set; }
        public virtual bool EnableWithOldSettings { get; set; }
    }

    public partial class EnableFacebookAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/google/enable", "POST")]
    public partial class EnableGoogleAuthentication
        : CodeMashRequestBase, IReturn<EnableGoogleAuthenticationResponse>
    {
        public EnableGoogleAuthentication()
        {
            Modes = new List<GoogleSettingsMode>{};
        }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual List<GoogleSettingsMode> Modes { get; set; }
        public virtual bool EnableWithOldSettings { get; set; }
    }

    public partial class EnableGoogleAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/twitter/enable", "POST")]
    public partial class EnableTwitterAuthentication
        : CodeMashRequestBase, IReturn<EnableTwitterAuthenticationResponse>
    {
        public EnableTwitterAuthentication()
        {
            Modes = new List<TwitterSettingsMode>{};
        }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string CodeCallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual List<TwitterSettingsMode> Modes { get; set; }
        public virtual bool EnableWithOldSettings { get; set; }
    }

    public partial class EnableTwitterAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/aad/api", "PUT")]
    public partial class UpdateAadApiSettings
        : CodeMashRequestBase, IReturn<UpdateAadApiSettingsResponse>
    {
        public UpdateAadApiSettings()
        {
            Scopes = new List<string>{};
            Modes = new List<AzureActiveDirSettingsMode>{};
        }

        public virtual List<string> Scopes { get; set; }
        public virtual string AppTenantId { get; set; }
        public virtual List<AzureActiveDirSettingsMode> Modes { get; set; }
    }

    public partial class UpdateAadApiSettingsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/aad", "PUT")]
    public partial class UpdateAadAuthentication
        : CodeMashRequestBase, IReturn<UpdateAadAuthenticationResponse>
    {
        public UpdateAadAuthentication()
        {
            Modes = new List<AzureActiveDirSettingsMode>{};
        }

        public virtual string TenantId { get; set; }
        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual List<AzureActiveDirSettingsMode> Modes { get; set; }
    }

    public partial class UpdateAadAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/apple", "PUT")]
    public partial class UpdateAppleAuthentication
        : CodeMashRequestBase, IReturn<UpdateAppleAuthenticationResponse>
    {
        public UpdateAppleAuthentication()
        {
            Modes = new List<AppleOAuthSettingsMode>{};
        }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string KeyId { get; set; }
        public virtual string BundleId { get; set; }
        public virtual string TeamId { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual List<AppleOAuthSettingsMode> Modes { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
    }

    public partial class UpdateAppleAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/credentials", "PUT")]
    public partial class UpdateCredentialsAuthentication
        : CodeMashRequestBase, IReturn<UpdateCredentialsAuthenticationResponse>
    {
        public UpdateCredentialsAuthentication()
        {
            Modes = new List<CredentialsSettingsMode>{};
        }

        public virtual string LogoutUrl { get; set; }
        public virtual bool AllowUsernames { get; set; }
        public virtual List<CredentialsSettingsMode> Modes { get; set; }
    }

    public partial class UpdateCredentialsAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/facebook", "PUT")]
    public partial class UpdateFacebookAuthentication
        : CodeMashRequestBase, IReturn<UpdateFacebookAuthenticationResponse>
    {
        public UpdateFacebookAuthentication()
        {
            Modes = new List<FacebookSettingsMode>{};
        }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual List<FacebookSettingsMode> Modes { get; set; }
    }

    public partial class UpdateFacebookAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/google/api", "PUT")]
    public partial class UpdateGoogleApiSettings
        : CodeMashRequestBase, IReturn<UpdateGoogleApiSettingsResponse>
    {
        public UpdateGoogleApiSettings()
        {
            Scopes = new List<string>{};
            Modes = new List<GoogleSettingsMode>{};
        }

        public virtual List<string> Scopes { get; set; }
        public virtual List<GoogleSettingsMode> Modes { get; set; }
    }

    public partial class UpdateGoogleApiSettingsResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/google", "PUT")]
    public partial class UpdateGoogleAuthentication
        : CodeMashRequestBase, IReturn<UpdateGoogleAuthenticationResponse>
    {
        public UpdateGoogleAuthentication()
        {
            Modes = new List<GoogleSettingsMode>{};
        }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual List<GoogleSettingsMode> Modes { get; set; }
    }

    public partial class UpdateGoogleAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication/google/services/{id}", "PUT")]
    public partial class UpdateGoogleServiceAccount
        : CodeMashRequestBase, IReturn<UpdateGoogleServiceAccountResponse>
    {
        public UpdateGoogleServiceAccount()
        {
            Scopes = new List<string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string SecretKey { get; set; }
        public virtual List<string> Scopes { get; set; }
    }

    public partial class UpdateGoogleServiceAccountResponse
        : ResponseBase<bool>
    {
        public virtual string ClientId { get; set; }
        public virtual string PrivateKeyEnding { get; set; }
    }

    [Route("/membership/authentication/twitter", "PUT")]
    public partial class UpdateTwitterAuthentication
        : CodeMashRequestBase, IReturn<UpdateTwitterAuthenticationResponse>
    {
        public UpdateTwitterAuthentication()
        {
            Modes = new List<TwitterSettingsMode>{};
        }

        public virtual string ClientId { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string CodeCallbackUrl { get; set; }
        public virtual string CallbackUrl { get; set; }
        public virtual string FailureRedirectUrl { get; set; }
        public virtual string LogoutUrl { get; set; }
        public virtual string Role { get; set; }
        public virtual string Zone { get; set; }
        public virtual List<TwitterSettingsMode> Modes { get; set; }
    }

    public partial class UpdateTwitterAuthenticationResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/policies/{policy}/permissions/{permission}/apply", "POST")]
    public partial class ApplyPermissionToUsers
        : CodeMashRequestBase, IReturn<ApplyPermissionToUsersResponse>
    {
        public virtual string Permission { get; set; }
        public virtual string Policy { get; set; }
    }

    public partial class ApplyPermissionToUsersResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/policies", "POST")]
    public partial class CretePolicy
        : CodeMashRequestBase, IReturn<CretePolicyResponse>
    {
        public CretePolicy()
        {
            Permissions = new List<string>{};
        }

        public virtual string Name { get; set; }
        public virtual List<string> Permissions { get; set; }
    }

    public partial class CretePolicyResponse
        : ResponseBase<string>
    {
    }

    [Route("/membership/policies/{name}", "DELETE")]
    public partial class DeletePolicy
        : CodeMashRequestBase, IReturn<DeletePolicyResponse>
    {
        public virtual string Name { get; set; }
    }

    public partial class DeletePolicyResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/authentication", "GET")]
    public partial class GetAuthenticationSettings
        : CodeMashRequestBase, IReturn<GetAuthenticationSettingsResponse>
    {
    }

    public partial class GetAuthenticationSettingsResponse
        : ResponseBase<AuthenticationDto>
    {
    }

    [Route("/membership/authorization", "GET")]
    public partial class GetAuthorizationSettings
        : CodeMashRequestBase, IReturn<GetAuthorizationSettingsResponse>
    {
    }

    public partial class GetAuthorizationSettingsResponse
        : ResponseBase<AuthorizationDto>
    {
    }

    [Route("/payments/settings", "GET")]
    public partial class GetPaymentSettings
        : CodeMashRequestBase, IReturn<GetPaymentSettingsResponse>
    {
    }

    public partial class GetPaymentSettingsResponse
        : ResponseBase<PaymentSettingsDto>
    {
    }

    [Route("/membership/policies", "GET")]
    public partial class GetPolicies
        : CodeMashRequestBase, IReturn<GetPoliciesResponse>
    {
        public virtual bool IncludePermissions { get; set; }
        public virtual string RoleName { get; set; }
    }

    public partial class GetPoliciesResponse
        : ResponseBase<List<PolicyBaseDto>>
    {
    }

    [Route("/membership/policies/{name}", "GET")]
    public partial class GetPolicy
        : CodeMashRequestBase, IReturn<GetPolicyResponse>
    {
        public virtual string Name { get; set; }
    }

    public partial class GetPolicyResponse
        : ResponseBase<PolicyBaseDto>
    {
    }

    [Route("/system/policies", "GET")]
    public partial class GetSystemPolicies
        : CodeMashRequestBase, IReturn<GetSystemPoliciesResponse>
    {
        public virtual string Policy { get; set; }
    }

    public partial class GetSystemPoliciesResponse
        : ResponseBase<List<PolicyBaseDto>>
    {
    }

    [Route("/membership/users/meta", "GET")]
    public partial class GetUserMetaSchema
        : CodeMashRequestBase, IReturn<GetUserMetaSchemaResponse>
    {
    }

    public partial class GetUserMetaSchemaResponse
        : ResponseBase<UserMetaSchemaDto>
    {
        public GetUserMetaSchemaResponse()
        {
            AvailableLanguages = new List<string>{};
        }

        public virtual List<string> AvailableLanguages { get; set; }
        public virtual string DefaultLanguage { get; set; }
    }

    [Route("/membership/policies/{policy}/permissions/{permission}", "GET")]
    public partial class GetUsersPermissionDetails
        : CodeMashRequestBase, IReturn<GetUsersPermissionDetailsResponse>
    {
        public virtual string Policy { get; set; }
        public virtual string Permission { get; set; }
    }

    public partial class GetUsersPermissionDetailsResponse
        : ResponseBase<PermissionUsersDetailsDto>
    {
    }

    [Route("/membership/policies/{policy}/permissions/{permission}/revoke", "POST")]
    public partial class RevokePermissionFromUsers
        : CodeMashRequestBase, IReturn<RevokePermissionFromUsersResponse>
    {
        public virtual string Policy { get; set; }
        public virtual string Permission { get; set; }
    }

    public partial class RevokePermissionFromUsersResponse
        : ResponseBase<bool>
    {
    }

    [Route("/membership/policies/{formerName}", "PUT")]
    public partial class UpdatePolicy
        : CodeMashRequestBase, IReturn<UpdatePolicyResponse>
    {
        public UpdatePolicy()
        {
            Permissions = new List<string>{};
        }

        public virtual string FormerName { get; set; }
        public virtual string Name { get; set; }
        public virtual List<string> Permissions { get; set; }
    }

    public partial class UpdatePolicyResponse
        : ResponseBase<string>
    {
    }

    [Route("/scheduler/tasks", "POST")]
    public partial class CreateScheduledTask
        : CodeMashRequestBase, IReturn<CreateScheduledTaskResponse>
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string StartDate { get; set; }
        public virtual SchedulerRepeatType RepeatType { get; set; }
        public virtual int? RepeatInterval { get; set; }
        public virtual string EndDate { get; set; }
        public virtual bool StopOnError { get; set; }
        public virtual ServerlessProvider Provider { get; set; }
        public virtual Guid FunctionId { get; set; }
        public virtual string Qualifier { get; set; }
        public virtual bool QualifierIsAlias { get; set; }
        public virtual string MetaData { get; set; }
    }

    public partial class CreateScheduledTaskResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/scheduler/tasks/{id}", "DELETE")]
    public partial class DeleteScheduledTask
        : CodeMashRequestBase, IReturn<DeleteScheduledTaskResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class DeleteScheduledTaskResponse
        : ResponseBase<bool>
    {
    }

    [Route("/scheduler/disable", "GET")]
    public partial class DisableScheduler
        : CodeMashRequestBase, IReturn<DisableSchedulerResponse>
    {
    }

    public partial class DisableSchedulerResponse
        : ResponseBase<bool>
    {
    }

    [Route("/scheduler/enable", "GET")]
    public partial class EnableScheduler
        : CodeMashRequestBase, IReturn<EnableSchedulerResponse>
    {
    }

    public partial class EnableSchedulerResponse
        : ResponseBase<Guid>
    {
    }

    [Route("/scheduler/tasks/{id}", "GET")]
    public partial class GetScheduledTask
        : CodeMashRequestBase, IReturn<GetScheduledTaskResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetScheduledTaskResponse
        : ResponseBase<SchedulerDisplayTaskDto>
    {
    }

    [Route("/scheduler/tasks", "GET")]
    public partial class GetScheduledTasks
        : CodeMashListRequestBase, IReturn<GetScheduledTasksResponse>
    {
    }

    public partial class GetScheduledTasksResponse
        : ResponseBase<List<SchedulerDisplayTaskDto>>
    {
        public virtual long TotalCount { get; set; }
    }

    [Route("/scheduler/tasks/{id}/execute", "POST")]
    public partial class RunScheduledTask
        : CodeMashRequestBase, IReturn<RunScheduledTaskResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class RunScheduledTaskResponse
        : ResponseBase<bool>
    {
    }

    [Route("/payments/discounts/{id}/enabled", "PUT")]
    public partial class TogglePaymentDiscountEnabled
        : CodeMashRequestBase, IReturn<TogglePaymentDiscountEnabledResponse>
    {
        public virtual string Id { get; set; }
    }

    public partial class TogglePaymentDiscountEnabledResponse
        : ResponseBase<bool>
    {
    }

    [Route("/scheduler/tasks/{id}/enabled", "PUT")]
    public partial class ToggleScheduledTaskEnabled
        : CodeMashRequestBase, IReturn<ToggleScheduledTaskEnabledResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class ToggleScheduledTaskEnabledResponse
        : ResponseBase<bool>
    {
    }

    public partial class ToggleScheduledTaskOnError
        : CodeMashRequestBase, IReturn<ToggleScheduledTaskOnErrorResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class ToggleScheduledTaskOnErrorResponse
        : ResponseBase<bool>
    {
    }

    [Route("/scheduler/tasks/{id}", "PUT")]
    public partial class UpdateScheduledTask
        : CodeMashRequestBase, IReturn<UpdateScheduledTaskResponse>
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual SchedulerType Type { get; set; }
        public virtual string StartDate { get; set; }
        public virtual SchedulerRepeatType RepeatType { get; set; }
        public virtual int? RepeatInterval { get; set; }
        public virtual string EndDate { get; set; }
        public virtual bool StopOnError { get; set; }
        public virtual ServerlessProvider Provider { get; set; }
        public virtual Guid FunctionId { get; set; }
        public virtual string Qualifier { get; set; }
        public virtual bool QualifierIsAlias { get; set; }
        public virtual string MetaData { get; set; }
    }

    public partial class UpdateScheduledTaskResponse
        : ResponseBase<bool>
    {
    }

    [Route("/serverless/functions/{id}", "GET")]
    public partial class GetFunction
        : CodeMashRequestBase, IReturn<GetFunctionResponse>
    {
        public virtual Guid Id { get; set; }
    }

    public partial class GetFunctionResponse
        : ResponseBase<ServerlessFunctionDto>
    {
        public virtual FunctionResource SystemResources { get; set; }
        public virtual string FunctionResources { get; set; }
        public virtual bool AllowMemoryChange { get; set; }
        public virtual bool AllowTimeoutChange { get; set; }
        public virtual string Schema { get; set; }
        public virtual string TestSchema { get; set; }
        public virtual bool CantBeTested { get; set; }
        public virtual bool IsSchedulerFunction { get; set; }
        public virtual bool ShowAdvanced { get; set; }
        public virtual GoogleSettings GoogleSettings { get; set; }
        public virtual AzureActiveDirSettings AzureActiveDirSettings { get; set; }
    }

    [Route("/serverless/functions", "GET")]
    public partial class GetFunctions
        : CodeMashRequestBase, IReturn<GetFunctionsResponse>
    {
        public virtual ServerlessProvider Provider { get; set; }
        public virtual TriggerUsages Usage { get; set; }
        public virtual bool ForScheduler { get; set; }
    }

    public partial class GetFunctionsResponse
        : ResponseBase<List<ServerlessFunctionDto>>
    {
        public GetFunctionsResponse()
        {
            Tags = new List<string>{};
        }

        public virtual long TotalCount { get; set; }
        public virtual List<string> Tags { get; set; }
    }

    [Route("/serverless/functions/tags", "GET")]
    public partial class GetFunctionTags
        : CodeMashRequestBase, IReturn<GetFunctionTagsResponse>
    {
    }

    public partial class GetFunctionTagsResponse
        : ResponseBase<List<String>>
    {
    }

    [Route("/membership/functions", "GET")]
    public partial class GetUserFunctions
        : CodeMashDbRequestBase, IReturn<GetUserFunctionsResponse>
    {
        public virtual bool IsList { get; set; }
    }

    public partial class GetUserFunctionsResponse
        : ResponseBase<List<NameIdDto>>
    {
    }

    public partial class NameIdDto
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsDefault { get; set; }
    }

    [DataContract]
    public partial class DeleteResult
    {
        [DataMember(Name="deletedCount")]
        public virtual long DeletedCount { get; set; }

        [DataMember(Name="isAcknowledged")]
        public virtual bool IsAcknowledged { get; set; }
    }

    public partial class File
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

    [DataContract]
    public partial class InsertOneResponse
        : ResponseBase<string>
    {
    }

    public partial class PushNotificationToken
    {
        public virtual string Provider { get; set; }
        public virtual string Token { get; set; }
        public virtual string AccountId { get; set; }
    }

    public partial class SelectItem
    {
        public virtual string Label { get; set; }
        public virtual string Value { get; set; }
    }

    public partial class Token
    {
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
        public virtual string Owner { get; set; }
    }

    public partial class UploadRecordFileResponse
        : ResponseBase<File>
    {
        public virtual string Key { get; set; }
    }

    public partial class UserPolicyUpdateInput
    {
        public UserPolicyUpdateInput()
        {
            Permissions = new List<string>{};
        }

        public virtual string Policy { get; set; }
        public virtual List<string> Permissions { get; set; }
    }

    public partial class UserRoleUpdateInput
    {
        public UserRoleUpdateInput()
        {
            Policies = new List<UserPolicyUpdateInput>{};
        }

        public virtual string Role { get; set; }
        public virtual List<UserPolicyUpdateInput> Policies { get; set; }
    }

    public partial class AccountListItemDto
    {
        public virtual string AccountId { get; set; }
        public virtual long AccountNumber { get; set; }
        public virtual string OwnerEmail { get; set; }
        public virtual bool IsOwner { get; set; }
    }

    public partial class AccountSettingsDto
    {
        public virtual bool IsAccountUnverified { get; set; }
        public virtual SubscriptionDto Subscription { get; set; }
        public virtual SubscriptionInvoice UnpaidInvoice { get; set; }
        public virtual CardDto Card { get; set; }
        public virtual BillingDto Billing { get; set; }
    }

    public partial class AccountStatusDto
    {
        public AccountStatusDto()
        {
            Permissions = new List<string>{};
            Roles = new List<string>{};
            AllowedProjects = new List<string>{};
            AccountPermissions = new Dictionary<string, string>{};
            ProjectPermissions = new Dictionary<string, string>{};
            MembershipPermissions = new Dictionary<string, string>{};
            EmailPermissions = new Dictionary<string, string>{};
            PushPermissions = new Dictionary<string, string>{};
            LoggingPermissions = new Dictionary<string, string>{};
            FilesPermissions = new Dictionary<string, string>{};
            SchedulerPermissions = new Dictionary<string, string>{};
            ServerlessPermissions = new Dictionary<string, string>{};
            DatabasePermissions = new Dictionary<string, string>{};
            PaymentPermissions = new Dictionary<string, string>{};
            ServerEventsPermissions = new Dictionary<string, string>{};
        }

        public virtual string Status { get; set; }
        public virtual string SubscriptionStatus { get; set; }
        public virtual bool IsTrial { get; set; }
        public virtual string EmailWithVerificationCode { get; set; }
        public virtual string Plan { get; set; }
        public virtual List<string> Permissions { get; set; }
        public virtual List<string> Roles { get; set; }
        public virtual List<string> AllowedProjects { get; set; }
        public virtual Dictionary<string, string> AccountPermissions { get; set; }
        public virtual Dictionary<string, string> ProjectPermissions { get; set; }
        public virtual Dictionary<string, string> MembershipPermissions { get; set; }
        public virtual Dictionary<string, string> EmailPermissions { get; set; }
        public virtual Dictionary<string, string> PushPermissions { get; set; }
        public virtual Dictionary<string, string> LoggingPermissions { get; set; }
        public virtual Dictionary<string, string> FilesPermissions { get; set; }
        public virtual Dictionary<string, string> SchedulerPermissions { get; set; }
        public virtual Dictionary<string, string> ServerlessPermissions { get; set; }
        public virtual Dictionary<string, string> DatabasePermissions { get; set; }
        public virtual Dictionary<string, string> PaymentPermissions { get; set; }
        public virtual Dictionary<string, string> ServerEventsPermissions { get; set; }
    }

    public partial class AccountUserDto
    {
        public AccountUserDto()
        {
            Permissions = new List<string>{};
            AllowedProjects = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string AccountId { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string City { get; set; }
        public virtual string Address { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual string BillingEmail { get; set; }
        public virtual string Email { get; set; }
        public virtual bool IsUser { get; set; }
        public virtual List<string> Permissions { get; set; }
        public virtual List<string> AllowedProjects { get; set; }
        public virtual string Status { get; set; }
    }

    public partial class ActivityFiltersDto
    {
        public ActivityFiltersDto()
        {
            Members = new List<NameIdDto>{};
            Projects = new List<NameIdDto>{};
            Activities = new List<NameIdDto>{};
        }

        public virtual List<NameIdDto> Members { get; set; }
        public virtual List<NameIdDto> Projects { get; set; }
        public virtual List<NameIdDto> Activities { get; set; }
    }

    public partial class ActivityListDto
    {
        public virtual string Id { get; set; }
        public virtual string Activity { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual long Date { get; set; }
        public virtual string Module { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UserId { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual string ProjectId { get; set; }
    }

    public partial class AuthenticationDto
    {
        public virtual CredentialsSettings CredentialsSettings { get; set; }
        public virtual AzureActiveDirSettings AzureActiveDirSettings { get; set; }
        public virtual bool AzureActiveDirSettingsEnabled { get; set; }
        public virtual FacebookSettings FacebookSettings { get; set; }
        public virtual bool FacebookSettingsEnabled { get; set; }
        public virtual TwitterSettings TwitterSettings { get; set; }
        public virtual bool TwitterSettingsEnabled { get; set; }
        public virtual GoogleSettings GoogleSettings { get; set; }
        public virtual bool GoogleSettingsEnabled { get; set; }
        public virtual AppleOAuthSettings AppleSettings { get; set; }
        public virtual bool AppleSettingsEnabled { get; set; }
    }

    public partial class AuthorizationDto
    {
        public AuthorizationDto()
        {
            AllowedRegisterRoles = new List<string>{};
            AllowedProviderRegisterRoles = new List<string>{};
        }

        public virtual EmailPreferencesForMembership EmailPreferences { get; set; }
        public virtual string UserRegistersAsRole { get; set; }
        public virtual string GuestRegistersAsRole { get; set; }
        public virtual List<string> AllowedRegisterRoles { get; set; }
        public virtual List<string> AllowedProviderRegisterRoles { get; set; }
        public virtual long? ResetPasswordTokenExpiration { get; set; }
        public virtual long? InvitationExpiration { get; set; }
        public virtual long? EmailVerificationExpiration { get; set; }
        public virtual long? DeactivationExpiration { get; set; }
        public virtual bool DefaultSubscribeToNews { get; set; }
        public virtual PasswordComplexity PasswordComplexity { get; set; }
    }

    public partial class BackupClusterSettingsDto
    {
        public virtual string Id { get; set; }
        public virtual int BackupHour { get; set; }
        public virtual int BackupCopies { get; set; }
        public virtual bool BackupsEnabled { get; set; }
    }

    public partial class BackupListItemDto
    {
        public virtual string Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string Status { get; set; }
        public virtual string ClusterId { get; set; }
    }

    public partial class BackupSettingsDto
    {
        public BackupSettingsDto()
        {
            Clusters = new List<BackupClusterSettingsDto>{};
        }

        public virtual int BackupHour { get; set; }
        public virtual int BackupCopies { get; set; }
        public virtual bool BackupsEnabled { get; set; }
        public virtual List<BackupClusterSettingsDto> Clusters { get; set; }
    }

    public partial class BillingDto
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

    public partial class BrowseObjectDto
    {
        public BrowseObjectDto()
        {
            Referrers = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string Key { get; set; }
        public virtual string Description { get; set; }
        public virtual long Size { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string OriginalName { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string LastModified { get; set; }
        public virtual bool IsFolder { get; set; }
        public virtual bool CanDelete { get; set; }
        public virtual bool CanRename { get; set; }
        public virtual bool CanMove { get; set; }
        public virtual string UsedInCollection { get; set; }
        public virtual List<string> Referrers { get; set; }
        public virtual bool IsPublic { get; set; }
        public virtual bool IsParentPublic { get; set; }
        public virtual string PublicUrl { get; set; }
        public virtual string ProviderKey { get; set; }
    }

    public partial class CampaignListDto
    {
        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
    }

    public partial class CardDto
    {
        public virtual long ExpMonth { get; set; }
        public virtual long ExpYear { get; set; }
        public virtual string Last4 { get; set; }
        public virtual string Name { get; set; }
        public virtual string Brand { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Funding { get; set; }
    }

    public partial class ClusterDto
    {
        public ClusterDto()
        {
            ReadOnlyRegions = new List<string>{};
            CollectionStats = new List<DatabaseCollectionStats>{};
        }

        public virtual string Id { get; set; }
        public virtual string Provider { get; set; }
        public virtual string Tier { get; set; }
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Region { get; set; }
        public virtual string RegionName { get; set; }
        public virtual bool AutoScaling { get; set; }
        public virtual bool MultiRegion { get; set; }
        public virtual List<string> ReadOnlyRegions { get; set; }
        public virtual string State { get; set; }
        public virtual string SpecsId { get; set; }
        public virtual long? StorageSize { get; set; }
        public virtual long DataSize { get; set; }
        public virtual DatabaseCredentials Credentials { get; set; }
        public virtual bool IsDefault { get; set; }
        public virtual List<DatabaseCollectionStats> CollectionStats { get; set; }
        public virtual CodeMashDatabaseClusterUpgrade Upgrade { get; set; }
        public virtual bool BackupsEnabled { get; set; }
        public virtual int BackupHour { get; set; }
        public virtual int BackupCopies { get; set; }
    }

    public partial class CollectionAggregateDto
    {
        public virtual string Id { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Query { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string CollectionNameAsTitle { get; set; }
        public virtual string SchemaId { get; set; }
    }

    public partial class CollectionFormDto
    {
        public CollectionFormDto()
        {
            Authentications = new List<string>{};
        }

        public virtual string Token { get; set; }
        public virtual string SchemaId { get; set; }
        public virtual bool IsPublic { get; set; }
        public virtual bool LimitOneResponse { get; set; }
        public virtual bool CanEdit { get; set; }
        public virtual List<string> Authentications { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual string DisabledMessage { get; set; }
        public virtual string Cluster { get; set; }
    }

    public partial class CollectionIndexDto
    {
        public CollectionIndexDto()
        {
            Key = new Dictionary<string, Object>{};
        }

        public virtual string Id { get; set; }
        public virtual string SchemaId { get; set; }
        public virtual string Name { get; set; }
        public virtual Dictionary<string, Object> Key { get; set; }
        public virtual long Operations { get; set; }
        public virtual DateTime? Since { get; set; }
        public virtual string SinceString { get; set; }
        public virtual string Options { get; set; }
        public virtual long? Size { get; set; }
        public virtual string Cluster { get; set; }
    }

    public partial class CollectionSysTriggerConfigCollectionDto
    {
        public CollectionSysTriggerConfigCollectionDto()
        {
            Fields = new Dictionary<string, List<String>>{};
        }

        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual Dictionary<string, List<String>> Fields { get; set; }
    }

    public partial class CollectionSysTriggerConfigDto
    {
        public CollectionSysTriggerConfigDto()
        {
            Fields = new Dictionary<string, List<String>>{};
            CollectionsWithFields = new List<CollectionSysTriggerConfigCollectionDto>{};
            DisabledParams = new List<string>{};
            AllowedTypes = new List<string>{};
            TokenFields = new List<string>{};
            IgnoredTokens = new List<string>{};
            IncludedTriggerOptions = new List<string>{};
        }

        public virtual Dictionary<string, List<String>> Fields { get; set; }
        public virtual List<CollectionSysTriggerConfigCollectionDto> CollectionsWithFields { get; set; }
        public virtual List<string> DisabledParams { get; set; }
        public virtual List<string> AllowedTypes { get; set; }
        public virtual bool BlockTemplateEditInTriggers { get; set; }
        public virtual bool IsBsonTemplate { get; set; }
        public virtual bool AllowTokens { get; set; }
        public virtual List<string> TokenFields { get; set; }
        public virtual List<string> IgnoredTokens { get; set; }
        public virtual List<string> IncludedTriggerOptions { get; set; }
        public virtual string TriggerSchema { get; set; }
    }

    public partial class CustomerSettingsDto
    {
        public virtual CardDto Card { get; set; }
        public virtual string UserId { get; set; }
    }

    public partial class DatabaseFeatureTestDto
    {
        public virtual bool CanCallFind { get; set; }
        public virtual string ErrorMessage { get; set; }
    }

    public partial class DatabaseSettingsDto
    {
        public DatabaseSettingsDto()
        {
            Regions = new List<DatabaseRegion>{};
            Tiers = new List<DatabaseTier>{};
        }

        public virtual List<DatabaseRegion> Regions { get; set; }
        public virtual List<DatabaseTier> Tiers { get; set; }
    }

    public partial class DbSettingsDto
    {
        public DbSettingsDto()
        {
            CollectionStats = new List<DatabaseCollectionStats>{};
        }

        public virtual long? StorageSize { get; set; }
        public virtual long DataSize { get; set; }
        public virtual long? MaxSize { get; set; }
        public virtual string DatabaseRegion { get; set; }
        public virtual string DatabaseRegionName { get; set; }
        public virtual List<DatabaseCollectionStats> CollectionStats { get; set; }
    }

    public partial class DiscountDto
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

    public partial class EmailAccountDto
    {
        public EmailAccountDto()
        {
            Signatures = new Dictionary<string, string>{};
            SubscriptionLinks = new Dictionary<string, string>{};
        }

        public virtual Guid Id { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string EmailDisplayName { get; set; }
        public virtual string Provider { get; set; }
        public virtual bool IsDefault { get; set; }
        public virtual string TokenEnding { get; set; }
        public virtual string WebHookSigningKeyEnding { get; set; }
        public virtual string Domain { get; set; }
        public virtual string Region { get; set; }
        public virtual string ConfigurationSetName { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual Dictionary<string, string> Signatures { get; set; }
        public virtual Dictionary<string, string> SubscriptionLinks { get; set; }
    }

    public partial class EmailDataDto
    {
        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string DeliveredOn { get; set; }
        public virtual string RecipientEmail { get; set; }
        public virtual string RecipientUser { get; set; }
        public virtual string Subject { get; set; }
        public virtual bool Delivered { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual bool DeliverSuccessful { get; set; }
        public virtual string CurrentStatus { get; set; }
    }

    public partial class EmailDataEditDto
    {
        public EmailDataEditDto()
        {
            Events = new List<EmailEventDto>{};
            InitialTokens = new List<KeyValue>{};
            RecipientTokens = new List<KeyValue>{};
            AdditionalTokens = new List<KeyValue>{};
        }

        public virtual string Id { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual string RecipientEmail { get; set; }
        public virtual string DeliveredOn { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual List<EmailEventDto> Events { get; set; }
        public virtual bool Delivered { get; set; }
        public virtual bool DeliverSuccessful { get; set; }
        public virtual string CurrentStatus { get; set; }
        public virtual string EmailProvider { get; set; }
        public virtual string ResponseMessage { get; set; }
        public virtual string ResponseError { get; set; }
        public virtual List<KeyValue> InitialTokens { get; set; }
        public virtual List<KeyValue> RecipientTokens { get; set; }
        public virtual List<KeyValue> AdditionalTokens { get; set; }
    }

    public partial class EmailEventDto
    {
        public virtual string Date { get; set; }
        public virtual string Type { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string Status { get; set; }
        public virtual string Reason { get; set; }
        public virtual string Attempt { get; set; }
        public virtual string Response { get; set; }
        public virtual string ClickUrl { get; set; }
    }

    public partial class EmailGroupDataDto
    {
        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string DeliverOn { get; set; }
        public virtual int TotalDeliverToProvider { get; set; }
        public virtual int TotalDeliverToProviderError { get; set; }
        public virtual int TotalSuccess { get; set; }
        public virtual int TotalError { get; set; }
        public virtual int TotalToDeliver { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual string TemplateId { get; set; }
        public virtual string RecipientEmail { get; set; }
        public virtual string RecipientUserId { get; set; }
        public virtual bool IsCreating { get; set; }
        public virtual bool LockedInProgress { get; set; }
    }

    public partial class EmailGroupDataEditDto
    {
        public EmailGroupDataEditDto()
        {
            Accounts = new List<EmailNotificationGroupAccountDto>{};
            Languages = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string DeliverOn { get; set; }
        public virtual int TotalToDeliver { get; set; }
        public virtual int TotalDeliverToProvider { get; set; }
        public virtual int TotalDeliverToProviderError { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual string TemplateId { get; set; }
        public virtual bool IsPostponed { get; set; }
        public virtual bool RespectTimeZone { get; set; }
        public virtual bool LockedInProgress { get; set; }
        public virtual string RequestLanguage { get; set; }
        public virtual List<EmailNotificationGroupAccountDto> Accounts { get; set; }
        public virtual List<string> Languages { get; set; }
    }

    public partial class EmailGroupEventDto
    {
        public virtual int Type { get; set; }
        public virtual int Count { get; set; }
    }

    public partial class EmailMetricsDto
    {
        public EmailMetricsDto()
        {
            EmailTags = new List<EmailPreferencesTagDto>{};
            PreferencesTexts = new Dictionary<string, EmailPreferenceTexts>{};
        }

        public virtual int TotalEmailAccounts { get; set; }
        public virtual long TotalEmailsOnQueue { get; set; }
        public virtual int TotalUsedInTriggers { get; set; }
        public virtual int TotalTemplates { get; set; }
        public virtual List<EmailPreferencesTagDto> EmailTags { get; set; }
        public virtual Dictionary<string, EmailPreferenceTexts> PreferencesTexts { get; set; }
    }

    public partial class EmailNotificationGroupAccountDto
    {
        public EmailNotificationGroupAccountDto()
        {
            Languages = new List<string>{};
        }

        public virtual string EmailAccountId { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Provider { get; set; }
        public virtual List<string> Languages { get; set; }
    }

    public partial class EmailPreferencesTagDto
    {
        public EmailPreferencesTagDto()
        {
            Titles = new Dictionary<string, string>{};
            Descriptions = new Dictionary<string, string>{};
        }

        public virtual string Identifier { get; set; }
        public virtual Dictionary<string, string> Titles { get; set; }
        public virtual Dictionary<string, string> Descriptions { get; set; }
    }

    public partial class EmailTemplateUsageDto
    {
        public EmailTemplateUsageDto()
        {
            CollectionTriggers = new Dictionary<string, List<String>>{};
            FileTriggers = new List<string>{};
            UserTriggers = new List<string>{};
        }

        public virtual Dictionary<string, List<String>> CollectionTriggers { get; set; }
        public virtual List<string> FileTriggers { get; set; }
        public virtual List<string> UserTriggers { get; set; }
    }

    public partial class ExportItemDto
    {
        public ExportItemDto()
        {
            IncludedFields = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string CompletedDate { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string CollectionNameAsTitle { get; set; }
        public virtual string JsonSchema { get; set; }
        public virtual string UiSchema { get; set; }
        public virtual string Status { get; set; }
        public virtual int TotalRecords { get; set; }
        public virtual string Sort { get; set; }
        public virtual string Filter { get; set; }
        public virtual string OutputType { get; set; }
        public virtual List<string> IncludedFields { get; set; }
        public virtual string FileId { get; set; }
        public virtual string FileName { get; set; }
        public virtual long FileSize { get; set; }
    }

    public partial class ExportListItemDto
    {
        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string CompletedDate { get; set; }
        public virtual string CollectionNameAsTitle { get; set; }
        public virtual string Status { get; set; }
        public virtual int TotalRecords { get; set; }
    }

    public partial class FeatureTestDto
    {
        public virtual bool CanSendEmail { get; set; }
        public virtual string ErrorMessage { get; set; }
    }

    public partial class FileAccountBasicDto
    {
        public virtual string Id { get; set; }
        public virtual bool IsDefault { get; set; }
        public virtual string DisplayName { get; set; }
    }

    public partial class FileAccountDto
    {
        public virtual Guid Id { get; set; }
        public virtual string Provider { get; set; }
        public virtual bool IsDefault { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string TokenEnding { get; set; }
        public virtual string PublicKey { get; set; }
        public virtual string PrivateKey { get; set; }
        public virtual string Region { get; set; }
        public virtual string BucketName { get; set; }
        public virtual string EncVersion { get; set; }
        public virtual bool IsSyncing { get; set; }
        public virtual long? LastSyncDate { get; set; }
        public virtual bool SyncWithError { get; set; }
        public virtual string OrganizationId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string PolicyArn { get; set; }
    }

    public partial class FileAccountFeatureTestDto
    {
        public virtual bool CanGetFiles { get; set; }
        public virtual string ErrorMessage { get; set; }
    }

    public partial class FilesSettingsDto
    {
        public virtual double? TotalSize { get; set; }
        public virtual double UsedSize { get; set; }
    }

    public partial class FilesTriggerCreateDto
    {
        public FilesTriggerCreateDto()
        {
            Types = new List<string>{};
            FileAccounts = new List<Guid>{};
        }

        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Directory { get; set; }
        public virtual List<string> Types { get; set; }
        public virtual string ActionType { get; set; }
        public virtual string Action { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual int? Order { get; set; }
        public virtual bool BreakOnError { get; set; }
        public virtual List<Guid> FileAccounts { get; set; }
    }

    public partial class GroupMessageSenderLocal
    {
        public GroupMessageSenderLocal()
        {
            Params = new string[]{};
        }

        public virtual string MessageCode { get; set; }
        public virtual string GroupName { get; set; }
        public virtual string[] Params { get; set; }
    }

    public partial class ImportItemDto
    {
        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string CompletedDate { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string CollectionNameAsTitle { get; set; }
        public virtual string Status { get; set; }
        public virtual int TotalImported { get; set; }
        public virtual int TotalRecords { get; set; }
        public virtual int TotalSkipped { get; set; }
        public virtual int TotalErrors { get; set; }
        public virtual string InputType { get; set; }
        public virtual string ErrorFileId { get; set; }
        public virtual string ErrorFileName { get; set; }
        public virtual long ErrorFileSize { get; set; }
        public virtual string FileId { get; set; }
        public virtual string FileName { get; set; }
        public virtual long FileSize { get; set; }
    }

    public partial class ImportListItemDto
    {
        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string CompletedDate { get; set; }
        public virtual string CollectionNameAsTitle { get; set; }
        public virtual string Status { get; set; }
        public virtual int TotalImported { get; set; }
        public virtual int TotalErrors { get; set; }
    }

    public partial class InvoiceDto
    {
        public virtual string Id { get; set; }
        public virtual string InvoiceNo { get; set; }
        public virtual string Created { get; set; }
        public virtual string Currency { get; set; }
        public virtual long Amount { get; set; }
        public virtual string Plan { get; set; }
        public virtual bool Paid { get; set; }
        public virtual string Status { get; set; }
    }

    public partial class LogDto
    {
        public virtual string Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string Level { get; set; }
        public virtual string Message { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string Properties { get; set; }
    }

    public partial class LogListItemDto
    {
        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string Level { get; set; }
        public virtual string Type { get; set; }
        public virtual string Method { get; set; }
        public virtual int? StatusCode { get; set; }
        public virtual string Description { get; set; }
        public virtual bool? IsFromHub { get; set; }
    }

    public partial class MessageSenderLocal
    {
        public MessageSenderLocal()
        {
            UserIds = new List<string>{};
            Params = new string[]{};
        }

        public virtual string MessageCode { get; set; }
        public virtual List<string> UserIds { get; set; }
        public virtual string[] Params { get; set; }
    }

    public partial class MessageTemplateDto
    {
        public MessageTemplateDto()
        {
            StaticAttachments = new List<DbFile>{};
            PreferenceTags = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual string EmailAccountId { get; set; }
        public virtual string EmailAccountName { get; set; }
        public virtual Guid? FileAccountId { get; set; }
        public virtual List<DbFile> StaticAttachments { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual string Code { get; set; }
        public virtual string Type { get; set; }
        public virtual List<string> PreferenceTags { get; set; }
        public virtual bool IncludeSubscriptionLink { get; set; }
    }

    public partial class MonthlyUsageDto
    {
        public MonthlyUsageDto()
        {
            ClustersCost = new Dictionary<string, decimal>{};
        }

        public virtual decimal AwsFilesCost { get; set; }
        public virtual decimal AwsLambdaCost { get; set; }
        public virtual decimal BackupsCost { get; set; }
        public virtual Dictionary<string, decimal> ClustersCost { get; set; }
        public virtual decimal AdditionalCost { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string ProjectId { get; set; }
        public virtual string ProjectName { get; set; }
    }

    public partial class MonthlyUsageSettingsDto
    {
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }
        public virtual string Currency { get; set; }
        public virtual double Vat { get; set; }
        public virtual decimal Accumulated { get; set; }
        public virtual decimal Subtotal { get; set; }
        public virtual decimal VatFee { get; set; }
        public virtual decimal Total { get; set; }
        public virtual decimal TotalTrial { get; set; }
    }

    public partial class NotificationDataDto
    {
        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string DeliveredOn { get; set; }
        public virtual string RecipientUser { get; set; }
        public virtual string RecipientUserId { get; set; }
        public virtual bool Delivered { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual int TotalDeliveredSuccessful { get; set; }
        public virtual int TotalDeliveredUnsuccessful { get; set; }
        public virtual bool IsStatic { get; set; }
        public virtual string CurrentStatus { get; set; }
    }

    public partial class NotificationDataEditDto
    {
        public NotificationDataEditDto()
        {
            Meta = new Dictionary<string, string>{};
            Devices = new List<NotificationResponseDevice>{};
            InitialTokens = new List<KeyValue>{};
            RecipientTokens = new List<KeyValue>{};
            AdditionalTokens = new List<KeyValue>{};
        }

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string DeliveredOn { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual string Data { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual string PushProvider { get; set; }
        public virtual string ResponseMessage { get; set; }
        public virtual string ResponseError { get; set; }
        public virtual bool BlockPush { get; set; }
        public virtual bool Delivered { get; set; }
        public virtual string CurrentStatus { get; set; }
        public virtual string UserDisplayName { get; set; }
        public virtual string UserId { get; set; }
        public virtual string SenderId { get; set; }
        public virtual List<NotificationResponseDevice> Devices { get; set; }
        public virtual List<KeyValue> InitialTokens { get; set; }
        public virtual List<KeyValue> RecipientTokens { get; set; }
        public virtual List<KeyValue> AdditionalTokens { get; set; }
    }

    public partial class NotificationGroupDataDto
    {
        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string DeliverOn { get; set; }
        public virtual int TotalSuccess { get; set; }
        public virtual int TotalError { get; set; }
        public virtual int TotalToDeliver { get; set; }
        public virtual int TotalDelivered { get; set; }
        public virtual int TotalDeliverToProvider { get; set; }
        public virtual int TotalDeliverToProviderError { get; set; }
        public virtual int TotalSavedWithoutPushing { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual string TemplateId { get; set; }
        public virtual bool IsNonPushable { get; set; }
        public virtual string RecipientUser { get; set; }
        public virtual string RecipientUserId { get; set; }
        public virtual bool IsCreating { get; set; }
        public virtual bool LockedInProgress { get; set; }
    }

    public partial class NotificationGroupDataEditDto
    {
        public NotificationGroupDataEditDto()
        {
            Accounts = new List<PushNotificationGroupAccountDto>{};
            Languages = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string DeliverOn { get; set; }
        public virtual int TotalSuccess { get; set; }
        public virtual int TotalError { get; set; }
        public virtual int TotalToDeliver { get; set; }
        public virtual int TotalDelivered { get; set; }
        public virtual int TotalToDeliverToProvider { get; set; }
        public virtual int TotalDeliverToProvider { get; set; }
        public virtual int TotalDeliverToProviderError { get; set; }
        public virtual int TotalSavedWithoutPushing { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual string TemplateId { get; set; }
        public virtual bool IsNonPushable { get; set; }
        public virtual bool IsPostponed { get; set; }
        public virtual bool RespectTimeZone { get; set; }
        public virtual bool LockedInProgress { get; set; }
        public virtual string RequestLanguage { get; set; }
        public virtual List<PushNotificationGroupAccountDto> Accounts { get; set; }
        public virtual List<string> Languages { get; set; }
    }

    public partial class NotificationResponseDevice
    {
        public NotificationResponseDevice()
        {
            Meta = new Dictionary<string, string>{};
        }

        public virtual string Id { get; set; }
        public virtual PushNotificationToken Token { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual string ResponseMessage { get; set; }
        public virtual string ResponseError { get; set; }
    }

    public partial class PasswordResetAccountDto
    {
        public virtual string OwnerEmail { get; set; }
        public virtual string AccountId { get; set; }
        public virtual long AccountNumber { get; set; }
        public virtual bool IsOwner { get; set; }
    }

    public partial class PaymentAccountDto
    {
        public virtual string Id { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Provider { get; set; }
        public virtual string ValidationToken { get; set; }
        public virtual string ClientId { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string TokenEnding { get; set; }
        public virtual string HookTokenEnding { get; set; }
        public virtual string EndpointTokenEnding { get; set; }
        public virtual string AuthRedirectUrl { get; set; }
        public virtual string PaymentRedirectUrl { get; set; }
        public virtual string ReceiverName { get; set; }
        public virtual string ReceiverIban { get; set; }
    }

    public partial class PaymentDiscountDto
    {
        public PaymentDiscountDto()
        {
            Boundaries = new List<PaymentDiscountBoundary>{};
            Records = new List<string>{};
            CategoryValues = new List<string>{};
            PaymentAccounts = new List<string>{};
            Users = new List<string>{};
            Emails = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual PaymentDiscountType Type { get; set; }
        public virtual string Code { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string ValidFrom { get; set; }
        public virtual string ValidUntil { get; set; }
        public virtual Guid CollectionId { get; set; }
        public virtual string Cluster { get; set; }
        public virtual PaymentDiscountRestrictionType RestrictionType { get; set; }
        public virtual double? Amount { get; set; }
        public virtual List<PaymentDiscountBoundary> Boundaries { get; set; }
        public virtual PaymentDiscountTargetType TargetType { get; set; }
        public virtual string LabelField { get; set; }
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

    public partial class PaymentDiscountListDto
    {
        public virtual string Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string ValidFrom { get; set; }
        public virtual string ValidUntil { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual string TotalRedeemed { get; set; }
        public virtual string Type { get; set; }
    }

    public partial class PaymentFeatureTestDto
    {
        public virtual bool CanConnect { get; set; }
        public virtual string ErrorMessage { get; set; }
    }

    public partial class PaymentPlanDto
    {
        public PaymentPlanDto()
        {
            Roles = new List<string>{};
            RolesAfterExpire = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string PaymentAccountId { get; set; }
        public virtual string PaymentAccountName { get; set; }
        public virtual string Provider { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string ProductId { get; set; }
        public virtual string PricingId { get; set; }
        public virtual string PackageName { get; set; }
        public virtual List<string> Roles { get; set; }
        public virtual List<string> RolesAfterExpire { get; set; }
    }

    public partial class PaymentSettingsDto
    {
        public PaymentSettingsDto()
        {
            OrderSchemas = new List<PaymentOrderSchema>{};
            Modes = new List<PaymentMode>{};
        }

        public virtual long? PayseraTimeLimit { get; set; }
        public virtual bool PayseraAllowTest { get; set; }
        public virtual bool AllowGuests { get; set; }
        public virtual string OrderPrefix { get; set; }
        public virtual string PayseraOnlyPayments { get; set; }
        public virtual string PayseraBlockedPayments { get; set; }
        public virtual string PayseraLanguage { get; set; }
        public virtual bool PayseraLanguageByIp { get; set; }
        public virtual List<PaymentOrderSchema> OrderSchemas { get; set; }
        public virtual List<PaymentMode> Modes { get; set; }
        public virtual bool StripeSubscriptionCancelInstant { get; set; }
        public virtual bool StripeSubscriptionRefundOnCancelInstant { get; set; }
        public virtual bool StripeSubscriptionRefundOnChange { get; set; }
        public virtual bool StripeApplyPreviousCouponOnChange { get; set; }
        public virtual int? StripeMaximumSubscriptions { get; set; }
        public virtual Guid? FileAccountId { get; set; }
    }

    public partial class PaymentTriggerCreateDto
    {
        public PaymentTriggerCreateDto()
        {
            Types = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual List<string> Types { get; set; }
        public virtual PaymentTriggerActionTypes ActionType { get; set; }
        public virtual string Action { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual int? Order { get; set; }
        public virtual bool BreakOnError { get; set; }
    }

    public partial class PermissionDisplayItemDto
    {
        public virtual string Value { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DisplayOption Group { get; set; }
        public virtual int Order { get; set; }
        public virtual OwnerType PolicyOwner { get; set; }
        public virtual Modules Module { get; set; }
    }

    public partial class PermissionUsersDetailsDto
    {
        public virtual long TotalUsers { get; set; }
        public virtual long TotalUsersHave { get; set; }
        public virtual long TotalApplicableUsers { get; set; }
    }

    public partial class PolicyBaseDto
    {
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual OwnerType Type { get; set; }
    }

    public partial class PolicyDisplayItemDto
        : PolicyBaseDto
    {
        public PolicyDisplayItemDto()
        {
            Permissions = new List<PermissionDisplayItemDto>{};
        }

        public virtual string Role { get; set; }
        public virtual List<PermissionDisplayItemDto> Permissions { get; set; }
        public virtual DisplayOption Group { get; set; }
        public virtual int Order { get; set; }
    }

    public partial class ProjectBillingChargeDto
    {
        public virtual string BillingType { get; set; }
        public virtual string MarginType { get; set; }
        public virtual double MarginPercent { get; set; }
        public virtual decimal FixedPrice { get; set; }
        public virtual bool ChargeCustomer { get; set; }
    }

    public partial class ProjectBillingInvoiceDto
    {
        public virtual string NumberPrefix { get; set; }
    }

    public partial class ProjectBillingSettingsDto
    {
        public virtual bool Enabled { get; set; }
        public virtual ProjectBillingChargeDto Charge { get; set; }
        public virtual ProjectBillingInvoiceDto Invoice { get; set; }
        public virtual BillingDto Billing { get; set; }
    }

    public partial class ProjectExposedDto
    {
        public ProjectExposedDto()
        {
            Tokens = new List<Token>{};
            Languages = new List<string>{};
            UserZones = new List<string>{};
            AllowedOrigins = new List<string>{};
            Widgets = new List<ProjectWidgetDto>{};
        }

        public virtual string Id { get; set; }
        public virtual bool DatabaseEstablished { get; set; }
        public virtual bool DatabaseEnabled { get; set; }
        public virtual bool EmailEstablished { get; set; }
        public virtual bool EmailEnabled { get; set; }
        public virtual bool MembershipEstablished { get; set; }
        public virtual bool MembershipEnabled { get; set; }
        public virtual bool LoggingEstablished { get; set; }
        public virtual bool LoggingEnabled { get; set; }
        public virtual bool NotificationEstablished { get; set; }
        public virtual bool NotificationEnabled { get; set; }
        public virtual bool SchedulerEstablished { get; set; }
        public virtual bool SchedulerEnabled { get; set; }
        public virtual bool ServerlessEstablished { get; set; }
        public virtual bool ServerlessEnabled { get; set; }
        public virtual bool FilingEstablished { get; set; }
        public virtual bool FilingEnabled { get; set; }
        public virtual bool PaymentEstablished { get; set; }
        public virtual bool PaymentEnabled { get; set; }
        public virtual bool ServerEventsEstablished { get; set; }
        public virtual bool ServerEventsEnabled { get; set; }
        public virtual bool AuthorizationEnabled { get; set; }
        public virtual bool AuthenticationEnabled { get; set; }
        public virtual bool BackupsEnabled { get; set; }
        public virtual List<Token> Tokens { get; set; }
        public virtual string LogoUrl { get; set; }
        public virtual string Url { get; set; }
        public virtual string LogoId { get; set; }
        public virtual List<string> Languages { get; set; }
        public virtual string DefaultLanguage { get; set; }
        public virtual ProjectZoneDto ProjectZone { get; set; }
        public virtual string DefaultTimeZone { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string SlugifiedName { get; set; }
        public virtual List<string> UserZones { get; set; }
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

    public partial class ProjectPlanChangeCheckDto
    {
        public virtual string ProjectName { get; set; }
        public virtual string ErrorCode { get; set; }
        public virtual long CurrentDataSize { get; set; }
        public virtual long? NewStorageSize { get; set; }
    }

    public partial class ProjectSmallDto
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

    public partial class ProjectWidgetDto
    {
        public virtual string WidgetType { get; set; }
        public virtual string Module { get; set; }
    }

    public partial class ProjectZoneDto
    {
        public virtual string UniqueName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Continent { get; set; }
    }

    public partial class PushAccountDto
    {
        public virtual Guid Id { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Provider { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string SecretKeyEnding { get; set; }
        public virtual bool IsDefault { get; set; }
    }

    public partial class PushFeatureTestDto
    {
        public virtual bool CanConnect { get; set; }
        public virtual string ErrorMessage { get; set; }
    }

    public partial class PushNotificationGroupAccountDto
    {
        public virtual string AccountId { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Provider { get; set; }
    }

    public partial class RoleBaseDto
    {
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual OwnerType Type { get; set; }
    }

    public partial class RoleDisplayItemDto
        : RoleBaseDto
    {
        public RoleDisplayItemDto()
        {
            Policies = new List<PolicyDisplayItemDto>{};
        }

        public virtual List<PolicyDisplayItemDto> Policies { get; set; }
        public virtual string ProjectId { get; set; }
    }

    public partial class SchedulerDisplayTaskDto
    {
        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual SchedulerType Type { get; set; }
        public virtual string StartDate { get; set; }
        public virtual SchedulerRepeatType RepeatType { get; set; }
        public virtual int? RepeatInterval { get; set; }
        public virtual string EndDate { get; set; }
        public virtual string LastStarted { get; set; }
        public virtual string LastEnded { get; set; }
        public virtual bool LastSuccessful { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual bool StopOnError { get; set; }
        public virtual ServerlessProvider Provider { get; set; }
        public virtual string FunctionId { get; set; }
        public virtual string MetaData { get; set; }
        public virtual int? NextDefaultExecutionSeconds { get; set; }
    }

    public partial class SchedulerTaskExecutionLogDto
    {
        public SchedulerTaskExecutionLogDto()
        {
            Errors = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string StartDate { get; set; }
        public virtual string EndDate { get; set; }
        public virtual bool Success { get; set; }
        public virtual List<string> Errors { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string Response { get; set; }
    }

    public partial class SchemaBasicDto
    {
        public SchemaBasicDto()
        {
            TranslatableFields = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual List<string> TranslatableFields { get; set; }
        public virtual int TotalEnabledTriggers { get; set; }
        public virtual bool DuplicateInProgress { get; set; }
        public virtual bool DuplicateFailed { get; set; }
        public virtual bool IsFieldRenaming { get; set; }
        public virtual string RenamingFieldFrom { get; set; }
        public virtual string RenamingFieldTo { get; set; }
    }

    public partial class SchemaDto
    {
        public SchemaDto()
        {
            TranslatableFields = new List<string>{};
            Triggers = new List<SchemaTriggerCreateDto>{};
            Properties = new List<SchemaProperty>{};
            NestedProperties = new List<SchemaNestProperty>{};
            FileProperties = new List<SchemaFileProperty>{};
            UserProperties = new List<SchemaUserProperty>{};
            EmailProperties = new List<SchemaEmailProperty>{};
            UriProperties = new List<SchemaUriProperty>{};
            NumberProperties = new List<SchemaNumberProperty>{};
            TextProperties = new List<SchemaTextProperty>{};
            RoleProperties = new List<SchemaRoleProperty>{};
            TaxonomyProperties = new List<SchemaTaxonomyProperty>{};
            CollectionProperties = new List<SchemaCollectionProperty>{};
            DateProperties = new List<SchemaDateProperty>{};
            TagsProperties = new List<SchemaTagsProperty>{};
        }

        public virtual string CollectionNameAsTitle { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string UiSchema { get; set; }
        public virtual string JsonSchema { get; set; }
        public virtual string MongoDbJsonSchema { get; set; }
        public virtual List<string> TranslatableFields { get; set; }
        public virtual List<SchemaTriggerCreateDto> Triggers { get; set; }
        public virtual SchemaSettings Settings { get; set; }
        public virtual Guid DatabaseId { get; set; }
        public virtual Guid SchemaId { get; set; }
        public virtual List<SchemaProperty> Properties { get; set; }
        public virtual List<SchemaNestProperty> NestedProperties { get; set; }
        public virtual bool DuplicateInProgress { get; set; }
        public virtual List<SchemaFileProperty> FileProperties { get; set; }
        public virtual List<SchemaUserProperty> UserProperties { get; set; }
        public virtual List<SchemaEmailProperty> EmailProperties { get; set; }
        public virtual List<SchemaUriProperty> UriProperties { get; set; }
        public virtual List<SchemaNumberProperty> NumberProperties { get; set; }
        public virtual List<SchemaTextProperty> TextProperties { get; set; }
        public virtual List<SchemaRoleProperty> RoleProperties { get; set; }
        public virtual List<SchemaTaxonomyProperty> TaxonomyProperties { get; set; }
        public virtual List<SchemaCollectionProperty> CollectionProperties { get; set; }
        public virtual List<SchemaDateProperty> DateProperties { get; set; }
        public virtual List<SchemaTagsProperty> TagsProperties { get; set; }
    }

    public partial class SchemaTemplateDto
    {
        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string IconUrl { get; set; }
        public virtual string IconUrlDark { get; set; }
    }

    public partial class SchemaTriggerCreateDto
    {
        public SchemaTriggerCreateDto()
        {
            Clusters = new List<string>{};
            Types = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual List<string> Clusters { get; set; }
        public virtual List<string> Types { get; set; }
        public virtual string ActionType { get; set; }
        public virtual string Action { get; set; }
        public virtual string ActivationCode { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual int? Order { get; set; }
        public virtual bool BreakOnError { get; set; }
    }

    public partial class ServerEventsSettingsDto
    {
        public virtual Guid? FileAccountId { get; set; }
        public virtual bool SendPush { get; set; }
    }

    public partial class ServerlessFeatureTestDto
    {
        public virtual bool CanConnect { get; set; }
        public virtual string ErrorMessage { get; set; }
    }

    public partial class ServerlessFunctionAliasDto
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreatedOnDate { get; set; }
        public virtual string CreatedOn { get; set; }
        public virtual string Version { get; set; }
        public virtual string AdditionalVersion { get; set; }
        public virtual double AdditionalVersionWeight { get; set; }
        public virtual string Description { get; set; }
    }

    public partial class ServerlessFunctionDto
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

    public partial class ServerlessFunctionVersionDto
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

    public partial class ServerlessProviderDto
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

    public partial class ServerlessSystemFunctionDto
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Runtime { get; set; }
        public virtual long? CodeSize { get; set; }
        public virtual string Region { get; set; }
        public virtual string Provider { get; set; }
        public virtual string Template { get; set; }
        public virtual bool CantBeTested { get; set; }
        public virtual long Memory { get; set; }
        public virtual int TimeoutMinutes { get; set; }
        public virtual int TimeoutSeconds { get; set; }
        public virtual string Schema { get; set; }
        public virtual string DocsId { get; set; }
    }

    public partial class ServerlessTestFunctionLog
    {
        public ServerlessTestFunctionLog()
        {
            Errors = new List<string>{};
        }

        public virtual bool Success { get; set; }
        public virtual List<string> Errors { get; set; }
        public virtual string Response { get; set; }
        public virtual string TimeZone { get; set; }
    }

    public partial class SubscriptionDto
    {
        public virtual SubscriptionPlan Plan { get; set; }
        public virtual SubscriptionStatus Status { get; set; }
        public virtual long Started { get; set; }
        public virtual long From { get; set; }
        public virtual long To { get; set; }
        public virtual long? Canceled { get; set; }
        public virtual long? SuspendedOn { get; set; }
        public virtual bool IsTrialPeriod { get; set; }
    }

    public partial class TaxonomyBasicDto
    {
        public TaxonomyBasicDto()
        {
            Dependencies = new List<string>{};
            TranslatableFields = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string RecordId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ParentTaxonomy { get; set; }
        public virtual List<string> Dependencies { get; set; }
        public virtual List<string> TranslatableFields { get; set; }
        public virtual bool IsFieldRenaming { get; set; }
        public virtual string RenamingFieldFrom { get; set; }
        public virtual string RenamingFieldTo { get; set; }
    }

    public partial class TaxonomyDto
    {
        public TaxonomyDto()
        {
            Dependencies = new List<string>{};
            MetaProperties = new List<SchemaProperty>{};
            TranslatableFields = new List<string>{};
            UserProperties = new List<SchemaUserProperty>{};
            TaxonomyProperties = new List<SchemaTaxonomyProperty>{};
            CollectionProperties = new List<SchemaCollectionProperty>{};
            FileProperties = new List<SchemaFileProperty>{};
        }

        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ParentId { get; set; }
        public virtual List<string> Dependencies { get; set; }
        public virtual string TermsUiSchema { get; set; }
        public virtual string TermsJsonSchema { get; set; }
        public virtual List<SchemaProperty> MetaProperties { get; set; }
        public virtual List<string> TranslatableFields { get; set; }
        public virtual Guid DatabaseId { get; set; }
        public virtual Guid TaxonomyId { get; set; }
        public virtual List<SchemaUserProperty> UserProperties { get; set; }
        public virtual List<SchemaTaxonomyProperty> TaxonomyProperties { get; set; }
        public virtual List<SchemaCollectionProperty> CollectionProperties { get; set; }
        public virtual List<SchemaFileProperty> FileProperties { get; set; }
    }

    public partial class TaxonomyListDto
    {
        public TaxonomyListDto()
        {
            DependencyNames = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string RecordId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ParentName { get; set; }
        public virtual List<string> DependencyNames { get; set; }
    }

    public partial class TermBasicDto
    {
        public TermBasicDto()
        {
            Meta = new Dictionary<string, Object>{};
        }

        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? Order { get; set; }
        public virtual Dictionary<string, Object> Meta { get; set; }
    }

    public partial class TermDto
    {
        public TermDto()
        {
            Name = new Dictionary<string, string>{};
            Description = new Dictionary<string, string>{};
            Dependencies = new Dictionary<string, List<String>>{};
        }

        public virtual string Id { get; set; }
        public virtual Dictionary<string, string> Name { get; set; }
        public virtual Dictionary<string, string> Description { get; set; }
        public virtual int? Order { get; set; }
        public virtual string Parent { get; set; }
        public virtual Dictionary<string, List<String>> Dependencies { get; set; }
        public virtual string Meta { get; set; }
    }

    public partial class TransactionDto
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

    public partial class UserMetaSchemaDto
    {
        public UserMetaSchemaDto()
        {
            TranslatableFields = new List<string>{};
            Properties = new List<SchemaProperty>{};
        }

        public virtual string UiSchema { get; set; }
        public virtual string JsonSchema { get; set; }
        public virtual List<string> TranslatableFields { get; set; }
        public virtual List<SchemaProperty> Properties { get; set; }
        public virtual bool IsFieldRenaming { get; set; }
        public virtual string RenamingFieldFrom { get; set; }
        public virtual string RenamingFieldTo { get; set; }
    }

    public partial class UsersTriggerCreateDto
    {
        public UsersTriggerCreateDto()
        {
            Types = new List<string>{};
        }

        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual List<string> Types { get; set; }
        public virtual UsersTriggerActionTypes ActionType { get; set; }
        public virtual string Action { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual int? Order { get; set; }
        public virtual bool BreakOnError { get; set; }
    }

}

