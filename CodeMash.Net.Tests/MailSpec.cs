using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Machine.Specifications;

namespace CodeMash.Net.Tests
{
    public class MailSpec
    {
        static readonly string AssemblyLocation = Assembly.GetExecutingAssembly().Location;
        static readonly string ExecutingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string ClientToken = Statics.ApiKey;

        public class When_send_simple_text_message_without_attachments_1
        {
            static bool mailIsSent = true;
            Because of = () =>
            {
                CodeMash.SendMail("domantasjovaisas@gmail.com", "Hi", "This is message without any attachments", "noreply@CodeMash.com");

            };

            It should_send_email = () => mailIsSent.ShouldBeTrue();
        }

        public class When_send_simple_text_message_to_many_recipients_when_they_are_set_as_comma_separated_and_without_attachments_2
        {
            static bool mailIsSent = true;
            Because of = () =>
            {
                CodeMash.SendMail("domantasjovaisas@gmail.com,domantas@isidos.lt", "Hi", "This is message without any attachments, to many recipients", "noreply@CodeMash.com");
            };

            It should_send_email = () => mailIsSent.ShouldBeTrue();
        }

        public class When_send_simple_text_message_to_many_recipients_when_they_are_set_as_array_and_without_attachments_2
        {
            static bool mailIsSent = true;
            Because of = () =>
            {
                var recipients = new[] { "domantasjovaisas@gmail.com", "domantas@isidos.lt" };
                CodeMash.SendMail(recipients, "Hi", "This is message without any attachments, to many recipients", "noreply@CodeMash.com");
            };

            It should_send_email = () => mailIsSent.ShouldBeTrue();
        }

        public class When_send_simple_text_message_with_attachments_when_attachment_is_provided_as_path
        {
            static bool mailIsSent = true;
            Because of = () =>
            {
                const string attachment1FileName = "attachment1.txt";

                var attachment1Path = Path.Combine(ExecutingDir, "Resources", attachment1FileName);

                CodeMash.SendMail("domantasjovaisas@gmail.com", "Hi", "This is message body dude", "noreply@CodeMash.com", new[] { attachment1Path });
            };

            It should_send_email = () => mailIsSent.ShouldBeTrue();
        }

        public class When_send_simple_text_message_with_attachments_when_attachment_is_provided_as_path_and_many_recipient_as_comma_separated
        {
            static bool mailIsSent = true;
            Because of = () =>
            {
                const string attachment1FileName = "attachment1.txt";

                var attachment1Path = Path.Combine(ExecutingDir, "Resources", attachment1FileName);

                CodeMash.SendMail("domantasjovaisas@gmail.com,domantas@isisdos.lt", "Hi", "This is message body dude", "noreply@CodeMash.com", new[] { attachment1Path });
            };

            It should_send_email = () => mailIsSent.ShouldBeTrue();
        }

        public class When_send_simple_text_message_with_attachments_when_attachment_is_provided_as_path_and_many_recipient_as_array
        {
            static bool mailIsSent = true;
            Because of = () =>
            {
                const string attachment1FileName = "attachment1.txt";

                var attachment1Path = Path.Combine(ExecutingDir, "Resources", attachment1FileName);
                var recipients = new[] { "domantasjovaisas@gmail.com", "domantas@isidos.lt" };
                CodeMash.SendMail(recipients, "Hi", "This is message body dude", "noreply@CodeMash.com", new[] { attachment1Path });
            };

            It should_send_email = () => mailIsSent.ShouldBeTrue();
        }

    }
}
