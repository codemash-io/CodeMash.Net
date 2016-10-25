using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using CodeMash.Net.DataContracts;
using Machine.Specifications;

namespace CodeMash.Net.Tests
{
    /* [Tags("IntegrationTest", "FS11111")]
    public class FiletSpec
    {
        [Tags("FS11000")]
        public class When_upload_new_file
        {
            static SaveFileResponse Response;

            Because of = async () =>
            {

                var timeStamp = DateTime.Now.TimeOfDay;
                var hash = string.Empty;
                
                using (var md5Hash = MD5.Create())
                {
                    var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes("CodeMash upload yeaa!!!" + timeStamp));

                    var sBuilder = new StringBuilder();
                    foreach (var t in data)
                    {
                        sBuilder.Append(t.ToString("x2"));
                    }
                    hash = sBuilder.ToString();
                }
                byte[] file;
                using (var fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/Files/ExampleFile.jpg", FileMode.Open, FileAccess.Read))
                {
                    file = fileStream.ReadFully();
                }
                
                var request = new SaveFile
                {
                    CollectionName = "Files",
                    CultureCode = "en",
                    File = file,
                    FileName = "ExampleFile.jpg",
                    Hash = hash,
                    Path = "/documents/upload",
                    TimeStamp = timeStamp.ToString(),
                };

                Response = CodeMash.UploadFile(request);
            };

            It should_return_response = () => Response.ShouldNotBeNull();
            It should_save_file = () => Response.Result.ShouldBeTrue();
            It should_return_file_info = () => Response.File.ShouldNotBeNull();
        }
    }*/
}
