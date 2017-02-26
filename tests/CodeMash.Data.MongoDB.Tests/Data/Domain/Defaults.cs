using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeMash.Tests.Data
{
    public static class Defaults
    {
        private static readonly Random Rnd = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string RandomString(int size)
        {
            var buffer = new char[size];

            for (var i = 0; i < size; i++)
            {
                buffer[i] = Chars[Rnd.Next(Chars.Length)];
            }
            return new string(buffer);
        }

        public static string Labels = Guid.NewGuid().ToString();
        public static string Descriptions = Guid.NewGuid().ToString();
        public static string Buttons = Guid.NewGuid().ToString();
        public static string ValidationMessages = Guid.NewGuid().ToString();
        public static string ErrorMessages = Guid.NewGuid().ToString();
        public static string Messages = Guid.NewGuid().ToString();

        public static Func<List<string>, List<ResourceValue>> InitValues = laguages =>
                laguages.Select(laguage => new ResourceValue { ResourceLanguageId = laguage, Value = RandomString(10) }).ToList();

        public static Func<List<string>, string, List<ResourceKey>> InitKeys = (supportedLaguages, keyName) => new List<ResourceKey>
		{
		    new ResourceKey {Key = Guid.NewGuid().ToString(), Name = keyName + "_1", Values = InitValues(supportedLaguages)},
		    new ResourceKey {Key = Guid.NewGuid().ToString(), Name = keyName + "_2", Values = InitValues(supportedLaguages)},
		    new ResourceKey {Key = Guid.NewGuid().ToString(), Name = keyName + "_3", Values = InitValues(supportedLaguages)}
		};
        public static IEnumerable<ResourceCategory>  DefaultResourceCategories(List<string> supportedLanguages)
        {
            yield return new ResourceCategory { Key = Labels, Name = "Labels", Keys = InitKeys(supportedLanguages, "Labels") };
            yield return new ResourceCategory { Key = Descriptions, Name = "Descriptions", Keys = InitKeys(supportedLanguages, "Descriptions") };
            yield return new ResourceCategory { Key = Buttons, Name = "Buttons", Keys = InitKeys(supportedLanguages, "Buttons") };
            yield return new ResourceCategory { Key = ValidationMessages, Name = "Validation Messages", Keys = InitKeys(supportedLanguages, "Validation Messages") };
            yield return new ResourceCategory { Key = ErrorMessages, Name = "Error Messages", Keys = InitKeys(supportedLanguages, "Error Messages") };
            yield return new ResourceCategory { Key = Messages, Name = "Messages", Keys = InitKeys(supportedLanguages, "Messages") };
        }

    }
}