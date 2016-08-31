// Guids.cs
// MUST match guids.h
using System;

namespace VSShellStub1.AboutBoxPackage
{
    static class GuidList
    {
        public const string guidAboutBoxPackagePkgString = "4bc5327c-ef19-4ffb-b54e-b0115070bc79";
        public const string guidAboutBoxPackageCmdSetString = "ed27eb3d-2eb3-4dc8-a936-28363d0777cf";

        public static readonly Guid guidAboutBoxPackageCmdSet = new Guid(guidAboutBoxPackageCmdSetString);
    };
}