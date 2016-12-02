using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMash.Net
{
    using System.Net.Mail;

    using CodeMash.Net.DataContracts;

    public interface IDatabaseRepository
    {

        void InsertOne<T>(T document, /* InsertOneOptions options, */ Notification notification = null)
            where T : EntityBase;

        void  SendMail(SendMail message, List<Attachment> attachments = null, string token = null);


    }
}
