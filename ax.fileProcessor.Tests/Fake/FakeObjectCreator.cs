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

        public static List<ZipArchiveEntryItem> FakeZipArchiveEntryItems() => new List<ZipArchiveEntryItem>(){
                new ZipArchiveEntryItem { FullName = "testFolder/", Name = "" },
                new ZipArchiveEntryItem { FullName = "testFolder/data.png", Name = "data.png" }};

    }
}
