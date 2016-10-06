using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using CodeMash.Net.DataContracts;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using System.Globalization;
using System.Net.Mail;
using Newtonsoft.Json.Linq;

namespace CodeMash.Net
{
    public static class DB
    {
        private static readonly string BaseUrl = Statics.Address;
        public static string ApiKey = Statics.ApiKey;

        public static TR Send<TR>(string url, object requestDto = null, string httpMethod = WebRequestMethods.Http.Get) where TR : class
        {
            try
            {
                if (string.IsNullOrEmpty(httpMethod))
                {
                    httpMethod = WebRequestMethods.Http.Get;
                }

                ApiKey = Statics.ApiKey;

                // TODO : specify this errors with codes and specify type of exception
                if (string.IsNullOrEmpty(ApiKey))
                {
                    throw new Exception("Please specify api key first");
                }

                if (string.IsNullOrEmpty(BaseUrl))
                {
                    throw new Exception("Please specify api address first");
                }

                if (httpMethod == "GET" && requestDto != null)
                {
                    url += "?" + requestDto.ToQueryString();
                }

                var serverUri = new Uri(BaseUrl);
                var relativeUri = new Uri(url, UriKind.Relative);
                var fullUri = new Uri(serverUri, relativeUri);

                var request = (HttpWebRequest)WebRequest.Create(fullUri);

                request.ContentType = "application/json; charset=utf-8";

                request.Accept = "application/json";
                request.Method = httpMethod;

                // ApiKey Auth 
                request.Headers.Add("Authorization", "Bearer " + ApiKey);

                if ((request.Method == WebRequestMethods.Http.Post || request.Method == WebRequestMethods.Http.Put || request.Method == "DELETE") && requestDto != null)
                {
                    var requestDtoAsJson = JsonConvert.SerializeObject(requestDto);
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(requestDtoAsJson);
                        streamWriter.Flush();
                    }
                }

                var response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var json = sr.ReadToEnd();
                    return BsonSerializer.Deserialize<TR>(json);
                }
            }
            catch (Exception e)
            {
                var errorMessage = e.Message;
#if DEBUG
                errorMessage += " " + e.StackTrace;
#endif
                throw new CodeMashException(errorMessage, e);
            }
        }

        private static List<TProjection> Find<T, TProjection>(string collectionName, FilterDefinition<T> filter, SortDefinition<T> sort = null, ProjectionDefinition<T, TProjection> projection = null, int? pageNumber = 0, int? pageSize = 100, FindOptions findOptions = null, bool includeSchema = false) where TProjection : new()
        {

            var serializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            var projectionInfo = projection.Render(serializer, BsonSerializer.SerializerRegistry);

            var projectionAsJson = string.Empty;
            if (projectionInfo.Document != null)
            {
                projectionAsJson = projectionInfo.Document.ToJson();
            }

            var request = new Find
            {
                CollectionName = collectionName,
                Filter = filter.ToJson(),
                Projection = projectionAsJson,
                Sort = sort.ToJson(),
                FindOptions = findOptions,
                PageSize = pageSize ?? 100,
                PageNumber = pageNumber ?? 0,
                IncludeSchema = includeSchema,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Send<FindResponse>("entities/" + collectionName + "/find", request, "POST");

            if (response != null && response.Result.Any())
            {
                var list = BsonSerializer.Deserialize<List<TProjection>>(response.Result);
                return list;
            }
            return new List<TProjection>();
        }

        public static List<T> Find<T>(string collectionName, FilterDefinition<T> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null, int? pageNumber = 0, int? pageSize = 100, FindOptions findOptions = null, bool includeSchema = false)
        {
            var request = new Find
            {
                CollectionName = collectionName,
                Filter = filter.ToJson(),
                Projection = projection.ToJson(),
                Sort = sort.ToJson(),
                FindOptions = findOptions,
                PageSize = pageSize ?? 100,
                PageNumber = pageNumber ?? 0,
                IncludeSchema = includeSchema,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name

            };

            var response = Send<FindResponse>("entities/" + collectionName + "/find", request, "POST");

            if (response != null && response.Result.Any())
            {
                var list = BsonSerializer.Deserialize<List<T>>(response.Result);
                return list;
            }
            return new List<T>();
        }

        public static List<T> Find<T>(string collectionName, Expression<Func<T, bool>> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null, int? pageNumber = 0, int? pageSize = 100, FindOptions findOptions = null, bool includeSchema = false)
        {
            if (filter == null)
            {
                return Find(collectionName, new BsonDocument(), sort, projection, pageNumber, pageSize, findOptions, includeSchema);
            }
            return Find(collectionName, new ExpressionFilterDefinition<T>(filter), sort, projection, pageNumber, pageSize, findOptions, includeSchema);
        }

        public static List<TProjection> Find<T, TProjection>(string collectionName, Expression<Func<T, bool>> filter, SortDefinition<T> sort = null, Expression<Func<T, TProjection>> projection = null, int? pageNumber = 0, int? pageSize = 100, FindOptions findOptions = null, bool includeSchema = false) where TProjection : new()
        {
            if (filter == null)
            {
                return Find(collectionName, new BsonDocument(), sort, Builders<T>.Projection.Expression(projection), pageNumber, pageSize, findOptions, includeSchema);
            }
            return Find(collectionName, new ExpressionFilterDefinition<T>(filter), sort, Builders<T>.Projection.Expression(projection), pageNumber, pageSize, findOptions, includeSchema);
        }

        public static T FindOne<T>(string collectionName, FilterDefinition<T> filter, ProjectionDefinition<BsonDocument> projection, FindOptions findOptions = null, bool includeSchema = false)
        {
            var request = new FindOne
            {
                CollectionName = collectionName,
                Filter = filter.ToJson(),
                Projection = projection.ToJson(),
                FindOptions = findOptions,
                IncludeSchema = includeSchema,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Send<FindOneResponse>("entities/" + collectionName + "/findOne", request, "POST");

            if (response != null && response.Result.Any())
            {
                var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
                return documentAsEntity;
            }
            return default(T);
        }

        public static T FindOne<T>(string collectionName, Expression<Func<T, bool>> filter, ProjectionDefinition<BsonDocument> projection, FindOptions findOptions = null, bool includeSchema = false)
        {
            if (filter == null)
            {
                return FindOne<T>(collectionName, new BsonDocument(), projection, findOptions, includeSchema);
            }
            return FindOne<T>(collectionName, new ExpressionFilterDefinition<T>(filter), projection, findOptions, includeSchema);
        }

        public static T FindOne<T>(string collectionName, Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                return FindOne<T>(collectionName, new BsonDocument(), null, null, false);
            }
            return FindOne<T>(collectionName, new ExpressionFilterDefinition<T>(filter), null, null, false);
        }

        public static T FindOne<T>(string collectionName, FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                return FindOne<T>(collectionName, new BsonDocument(), null, null, false);
            }
            return FindOne<T>(collectionName, filter, null, null, false);
        }


        public static T FindOneById<T>(string collectionName, string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return FindOne<T>(collectionName, filter, null, null, false);
        }


        public static T FindOneAndReplace<T>(string token, string collectionName, FilterDefinition<T> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null, Notification notification = null)
        {
            if (findOneAndReplaceOptions == null)
            {
                findOneAndReplaceOptions = new FindOneAndReplaceOptions<T>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                };
            }

            var request = new FindOneAndReplace
            {
                CollectionName = collectionName,
                Filter = filter.ToJson(),
                FindOneAndReplaceOptions = findOneAndReplaceOptions.ToBson(),
                Document = entity.ToJson(),
                Notification = notification,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Send<FindOneAndReplaceResponse>("entities/" + collectionName + "/findOneAndReplace", request, "POST");

            if (response != null && response.Result.Any())
            {
                var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
                return documentAsEntity;
            }
            return default(T);
        }

        public static T FindOneAndDelete<T>(string token, string collectionName, FilterDefinition<T> filter, FindOneAndDeleteOptions<T> findOneAndReplaceOptions = null, Notification notification = null)
        {
            var request = new FindOneAndDelete
            {
                CollectionName = collectionName,
                Filter = filter.ToJson(),
                FindOneAndDeleteOptions = findOneAndReplaceOptions.ToBson(),
                Notification = notification,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Send<FindOneAndDeleteResponse>("entities/" + collectionName + "/findOneAndDelete", request, "POST");

            if (response != null && response.Result.Any())
            {
                var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
                return documentAsEntity;
            }
            return default(T);
        }



        public static T FindOneAndUpdate<T>(string token, string collectionName, FilterDefinition<BsonDocument> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null, Notification notification = null)
        {
            if (findOneAndUpdateOptions == null)
            {
                findOneAndUpdateOptions = new FindOneAndUpdateOptions<T>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                };
            }

            var request = new FindOneAndUpdate
            {
                CollectionName = collectionName,
                Filter = filter.ToJson(),
                FindOneAndUpdateOptions = findOneAndUpdateOptions.ToBson(),
                Document = entity.RenderToBsonDocument(),
                Notification = notification,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Send<FindOneAndReplaceResponse>("entities/" + collectionName + "/findOneAndReplace", request, "POST");

            if (response != null && response.Result.Any())
            {
                var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
                return documentAsEntity;
            }
            return default(T);
        }

        /*public static  <SaveFileResponse> UploadFile(SaveFile request)
        {
            var response =  Send<SaveFileResponse>("files", request, "POST");
            return response;
        }*/

        public static List<TOutput> Aggregate<TInput, TOutput>(string collectionName, PipelineDefinition<TInput, TOutput> aggregation, AggregateOptions options)
        {
            var request = new Aggregate
            {
                CollectionName = collectionName,
                AggregateOptions = options,
                Aggregation = aggregation.ToJson(),
                OutputMode = JsonOutputMode.Strict,
            };

            var response = Send<AggregateResponse>("entities/" + collectionName + "/aggregate", request, "POST");

            if (response != null && response.Result.Any())
            {
                var list = BsonSerializer.Deserialize<List<TOutput>>(response.Result);
                return list;
            }
            return new List<TOutput>();
        }

        public static List<TOutput> Aggregate<TInput, TOutput>(string collectionName, List<BsonDocument> stages, AggregateOptions options)
        {
            var request = new Aggregate
            {
                CollectionName = collectionName,
                AggregateOptions = options,
                Aggregation = stages.ToJson(new JsonWriterSettings() { OutputMode = JsonOutputMode.Strict }),
                OutputMode = JsonOutputMode.Strict,
            };

            var response = Send<AggregateResponse>("entities/" + collectionName + "/aggregate", request, "POST");

            if (response != null && response.Result.Any())
            {
                var list = BsonSerializer.Deserialize<List<TOutput>>(response.Result);
                return list;
            }
            return new List<TOutput>();
        }

        public static long Count<T>(string token, string collectionName, FilterDefinition<T> filter, CountOptions options = null)
        {
            var request = new Count
            {
                CollectionName = collectionName,
                CountOptions = options,
                Filter = filter.ToJson(),
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Send<CountResponse>("entities/" + collectionName + "/count", request, "POST");

            if (response != null)
            {
                return response.Result;
            }
            return 0;
        }

        public static List<string> Distinct<T>(string token, string collectionName, string field, FilterDefinition<BsonDocument> filter, DistinctOptions options = null)
        {
            var request = new Distinct
            {
                CollectionName = collectionName,
                DistinctOptions = options,
                Filter = filter.ToJson(),
                Field = field,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Send<DistinctResponse>("entities/" + collectionName + "/distinct", request, "POST");

            if (response != null)
            {
                return response.Result;
            }
            return new List<string>();
        }

        public static T InsertOne<T>(string collectionName, T document, /* InsertOneOptions options, */ Notification notification = null)
        {
            var request = new InsertOne
            {
                CollectionName = collectionName,
                OutputMode = JsonOutputMode.Strict,
                Notification = notification,
                Document = document.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }),
                //InsertOneOptions = options,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Send<InsertOneResponse>("entities/" + collectionName, request, "POST");

            if (response?.Result == null)
            {
                return default(T);
            }
            var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
            return documentAsEntity;
        }


        public static bool InsertMany<T>(string collectionName, IEnumerable<T> documents, InsertManyOptions insertManyOptions = null, Notification notification = null)
        {
            var request = new InsertMany
            {
                CollectionName = collectionName,
                OutputMode = JsonOutputMode.Strict,
                Notification = notification,
                Documents = documents.Select(x => x.ToBsonDocument().ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }))
            };

            var response = Send<InsertManyResponse>("entities/" + collectionName + "/bulk", request, "POST");

            return response?.Result != null && response.Result;
        }

        public static T ReplaceOne<T>(string collectionName, FilterDefinition<T> filter, T document, UpdateOptions updateOptions = null, string schemaId = "", Notification notification = null)
        {
            var request = new ReplaceOne
            {
                CollectionName = collectionName,
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                Document = document.ToBsonDocument().ToJson(),
                UpdateOptions = updateOptions,
                SchemaId = schemaId
            };

            var response = Send<ReplaceOneResponse>("entities/" + collectionName + "/replaceOne", request, "POST");

            if (response?.Result == null)
            {
                return default(T);
            }
            var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
            return documentAsEntity;
        }

        public static T ReplaceOne<T>(string collectionName, Expression<Func<T, bool>> filter, T document, UpdateOptions updateOptions = null, string schemaId = "", Notification notification = null)
        {
            return ReplaceOne(collectionName, new ExpressionFilterDefinition<T>(filter), document, updateOptions, schemaId, notification);
        }

        public static T FindOneAndReplace<T>(string collectionName, FilterDefinition<T> filter, T document, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null, string schemaId = "", Notification notification = null)
        {
            var request = new FindOneAndReplace
            {
                CollectionName = collectionName,
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                Document = document.ToBsonDocument().ToJson(),
                FindOneAndReplaceOptions = findOneAndReplaceOptions
            };

            var response = Send<FindOneAndReplaceResponse>("entities/" + collectionName + "/findOneAndReplace", request, "POST");

            if (response?.Result == null)
            {
                return default(T);
            }
            var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
            return documentAsEntity;
        }

        public static T FindOneAndReplace<T>(string collectionName, Expression<Func<T, bool>> filter, T document, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null, string schemaId = "", Notification notification = null)
        {
            return FindOneAndReplace(collectionName, new ExpressionFilterDefinition<T>(filter), document, findOneAndReplaceOptions, schemaId, notification);
        }


        public static DataContracts.UpdateResult UpdateOne<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions = null, string schemaId = "", Notification notification = null)
        {
            var request = new UpdateOne
            {
                CollectionName = collectionName,
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                Update = update.ToJson(),
                UpdateOptions = updateOptions,
                SchemaId = schemaId
            };

            var response = Send<UpdateOneResponse>("entities/" + collectionName, request, "PUT");
            return response.Result;
        }

        public static DataContracts.UpdateResult UpdateOne<T>(string collectionName, Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions = null, string schemaId = "", Notification notification = null)
        {
            return UpdateOne(collectionName, new ExpressionFilterDefinition<T>(filter), update, updateOptions, schemaId, notification);
        }

        public static DataContracts.UpdateResult UpdateMany<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions, string schemaId = "", Notification notification = null)
        {
            var request = new UpdateMany
            {
                CollectionName = collectionName,
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                Update = update.ToBsonDocument().ToJson(),
                UpdateOptions = updateOptions,
                SchemaId = schemaId,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Send<UpdateOneResponse>("entities/" + collectionName + "/bulk", request, "PUT");
            return response.Result;
        }

        public static DataContracts.UpdateResult UpdateMany<T>(string collectionName, Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions = null, string schemaId = "", Notification notification = null)
        {
            return UpdateMany(collectionName, new ExpressionFilterDefinition<T>(filter), update, updateOptions, schemaId, notification);
        }

        public static DataContracts.DeleteResult DeleteOne<T>(string collectionName, FilterDefinition<T> filter, Notification notification = null)
        {
            var request = new DeleteOne
            {
                CollectionName = collectionName,
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                CultureCode = CultureInfo.CurrentCulture.Name,

            };

            var response = Send<DeleteOneResponse>("entities/" + collectionName, request, "DELETE");
            return response.Result;
        }

        public static DataContracts.DeleteResult DeleteOne<T>(string collectionName, Expression<Func<T, bool>> filter, Notification notification = null)
        {
            return DeleteOne(collectionName, new ExpressionFilterDefinition<T>(filter), notification);
        }

        public static DataContracts.DeleteResult DeleteOne<T>(string collectionName, string id, Notification notification = null)
        {
            FilterDefinition<BsonDocument> filter = new BsonDocument("_id", ObjectId.Parse(id));
            return DeleteOne(collectionName, filter, notification);
        }

        public static DataContracts.DeleteResult DeleteMany<T>(string collectionName, FilterDefinition<T> filter, Notification notification = null)
        {
            var request = new DeleteMany
            {
                CollectionName = collectionName,
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Send<DeleteManyResponse>("entities/" + collectionName + "/bulk", request, "DELETE");
            return response.Result;
        }

        public static DataContracts.DeleteResult DeleteMany<T>(string collectionName, Expression<Func<T, bool>> filter, Notification notification = null)
        {
            return DeleteMany<T>(collectionName, new ExpressionFilterDefinition<T>(filter), notification);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="toEmail">To email. - One recipient. Also you can provide emails comma separated</param>
        /// <param name="subject">The subject of email</param>
        /// <param name="body">The body of email</param>
        /// <param name="fromEmail">From email. - One email</param>
        /// <returns>.</returns>
        public static void SendMail(string toEmail, string subject, string body, string fromEmail)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(emails, subject, body, fromEmail);
            }
            SendMail(new[] { toEmail }, subject, body, fromEmail);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="toEmails">To emails. - You can provide an array of emails. The message will be sent to those recipients</param>
        /// <param name="subject">The subject of email</param>
        /// <param name="body">The body of email</param>
        /// <param name="fromEmail">From email. - One email</param>
        /// <returns>.</returns>
        public static void SendMail(string[] toEmails, string subject, string body, string fromEmail)
        {
            if (toEmails != null && toEmails.Any())
            {
                toEmails.ToList().ForEach(email =>
                {
                    var request = new SendMail
                    {
                        To = email,
                        Subject = subject,
                        Body = body,
                        From = fromEmail
                    };
                    SendMail(request);
                });
            }
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="toEmail">To email. - One recipient. Also you can provide emails comma separated</param>
        /// <param name="subject">The subject of email</param>
        /// <param name="body">The body of email</param>
        /// <param name="fromEmail">From email. - One email</param>
        /// <param name="attachments">The attachments.</param>
        /// <returns>.</returns>

        public static void SendMail(string toEmail, string subject, string body, string fromEmail, string[] attachments)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(emails, subject, body, fromEmail, attachments);
            }
            SendMail(new[] { toEmail }, subject, body, fromEmail, attachments);
        }

        public static void SendMail(string[] toEmails, string subject, string body, string fromEmail, string[] attachments)
        {
            List<Attachment> mailAttachments = null;

            if (attachments != null)
            {
                mailAttachments = (from attachment in attachments where !string.IsNullOrEmpty(attachment) select new Attachment(attachment)).ToList();
            }

            if (toEmails != null && toEmails.Any())
            {
                toEmails.ToList().ForEach(email =>
                {
                    var request = new SendMail
                    {
                        To = email,
                        Subject = subject,
                        Body = body,
                        From = fromEmail,
                    };
                    SendMail(request, mailAttachments);
                });
            }
        }
        public static void SendMail(string toEmail, string subject, string templateName, JObject model, string fromEmail)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(emails, subject, templateName, model, fromEmail, (string[])null);

            }
            SendMail(new[] { toEmail }, subject, templateName, model, fromEmail, (string[])null);
        }


        public static void SendMail(string toEmail, string subject, string templateName, JObject model, string fromEmail, string[] attachments)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(emails, subject, templateName, model, fromEmail, attachments);
            }
            SendMail(new[] { toEmail }, subject, templateName, model, fromEmail, attachments);
        }


        public static void SendMail(string[] toEmails, string subject, string templateName, JObject model, string fromEmail, string[] attachments)
        {
            // dynamic input from inbound JSON
            dynamic json = model;

            string jsonAsString = JsonConvert.SerializeObject(json);

            List<Attachment> mailAttachments = null;

            if (attachments != null)
            {
                mailAttachments = (from attachment in attachments where !string.IsNullOrEmpty(attachment) select new Attachment(attachment)).ToList();
            }

            if (toEmails != null && toEmails.Any())
            {
                toEmails.ToList().ForEach(email =>
                {
                    var request = new SendMail
                    {
                        To = email,
                        Subject = subject,
                        TemplateName = templateName,
                        ModelInJsonAsString = jsonAsString,
                        From = fromEmail,
                    };
                    SendMail(request, mailAttachments);
                });
            }
        }

        public static void SendMail(SendMail message, List<Attachment> attachments = null, string token = null)
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

            Send<SendMailResponse>("mail", message, "POST");
        }

    }
}