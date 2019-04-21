using System.Collections.Generic;
using ax.fileProcessor.Storage;

namespace ax.fileProcessor.Tests.Fake
{
    public static class FakeObjectCreator
    {
        public static AuthCredential CreateAuthCredential()
        {
            return new AuthCredential { Password = "mom", Username = "mama" };
        }

        public static List<ZipEntryInfo> FakeZipArchiveEntryItems()
        {
            return new List<ZipEntryInfo>(){
                new ZipEntryInfo {  FullName = "testFolder/", Name = "" },
                new ZipEntryInfo {  FullName = "testFolder/data.png", Name = "data.png" }};
        }
    }
}
