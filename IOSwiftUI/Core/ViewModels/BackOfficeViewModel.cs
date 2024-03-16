using System;
using IOBootstrap.NET.Common.Constants;
using IOBootstrap.NET.Common.Utilities;
using IOBootstrap.NET.Core.ViewModels;
using IOSwiftUI.DataAccess.Context;
using Microsoft.Extensions.Configuration;

namespace IOSwiftUI.Core;

public class BackOfficeViewModel : IOBackOfficeViewModel<DatabaseContext>
{
    public BackOfficeViewModel() : base()
    {
    }

    public string CreatePublicId(string fileName)
    {
		byte[] key = Convert.FromBase64String(Configuration.GetValue<string>(IOConfigurationConstants.EncryptionKey));
		byte[] iv = Convert.FromBase64String(Configuration.GetValue<string>(IOConfigurationConstants.EncryptionIV));
        IOAESUtilities aesUtilities = new IOAESUtilities(key, iv);
        return aesUtilities.Encrypt(fileName);
    }
}
