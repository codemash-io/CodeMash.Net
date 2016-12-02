using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMash.Net
{
    using System.Globalization;
    using System.Net.Mail;

    using CodeMash.Net.DataContracts;

    using MongoDB.Bson;
    using MongoDB.Bson.IO;
    using MongoDB.Bson.Serialization;

    public class DatabaseRepository : CodeMashBase, IDatabaseRepository
    {

        public void InsertOne<T>(T document, /* InsertOneOptions options, */ Notification notification = null) where T : EntityBase
        {
            var request = new InsertOne
            {
                CollectionName = GetCollectionName<T>(),
                OutputMode = JsonOutputMode.Strict,
                Notification = notification,
                Document = document.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }),
                //InsertOneOptions = options,
                CultureCode = CultureInfo.CurrentCulture.Name
            };


            var response = Client.Post<InsertOneResponse>(request);

            if (response?.Result == null)
            {
                return;
            }
            var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
            document.Id = documentAsEntity.Id;
        }
        private static string GetCollectionName<T>()
        {
            var collectionName = typeof(T).BaseType == typeof(object)
                ? GetCollectionNameFromInterface<T>()
                : GetCollectionNameFromType(typeof(T));

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }

        private static string GetCollectionNameFromInterface<T>()
        {
            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));
            var collectionName = att != null ? ((CollectionName)att).Name : typeof(T).Name;

            return collectionName;
        }

        private static string GetCollectionNameFromType(Type entityType)
        {
            string collectionName = string.Empty;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var customAttribute = Attribute.GetCustomAttribute(entityType, typeof(CollectionName));
            if (customAttribute != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionName = ((CollectionName)customAttribute).Name;
            }
            else
            {
                if (typeof(EntityBase).IsAssignableFrom(entityType))
                {
                    while (entityType != null && entityType.BaseType != typeof(EntityBase))
                    {
                        entityType = entityType.BaseType;
                    }
                }
                if (entityType != null) collectionName = entityType.Name;
            }

            return collectionName;
        }

        public void SendMail(SendMail message, List<Attachment> attachments = null, string token = null)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "You didn't provide message which should be sent.");
            }

            if (string.IsNullOrEmpty(message.To))
            {
                throw new ArgumentNullException(nameof(message.To), "Please provide email of recipient. To who you want send the message ?");
            }

            if (string.IsNullOrEmpty(message.Subject))
            {
                throw new ArgumentNullException(nameof(message.Subject), "Please provide subject of the email.");
            }

            if (string.IsNullOrEmpty(message.From))
            {
                throw new ArgumentNullException(nameof(message.From), "Please provide email of sender. It's not polite send emails as anonymous ;)");
            }

            if (string.IsNullOrEmpty(message.Body) && string.IsNullOrEmpty(message.TemplateName))
            {
                throw new ArgumentNullException(nameof(message.Body), "You didn't provide mail content - body. Consider send something useful and use either body or template property");
            }


            if (attachments != null)
            {
                var mailAttachments = (from attachment in attachments
                                       where attachment.ContentStream != null
                                       select new MailAttachmentDataContract(attachment.Name, attachment.ContentStream.ReadFully())).ToList();

                message.Attachments = mailAttachments;
            }

            Client.Post<SendMailResponse>(message);
        }
    }
}
