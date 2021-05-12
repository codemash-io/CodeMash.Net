using System.Collections.Generic;
using CodeMash.Models;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-ref-file-entity")]
    public class ReferencingFileEntity : Entity
    {
        [Field("text")]
        public string Text { get; set; }
        
        [Field("files")]
        public List<FileEntity> Files { get; set; }
    }
}