using System;
namespace AutoTwister.Common
{
	public static class Perms
	{
		public static async Task RequestPermissions()
		{
            PermissionStatus statusStorageRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
			if(statusStorageRead == PermissionStatus.Denied)
			{
				if(await Permissions.RequestAsync<Permissions.StorageRead>() == PermissionStatus.Denied)
				{
					await RequestPermissions();
				}
			}

			PermissionStatus statusStorageWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
			if(statusStorageWrite == PermissionStatus.Denied)
			{
				if(await Permissions.RequestAsync<Permissions.StorageWrite>() == PermissionStatus.Denied)
				{
					await RequestPermissions();
				}
			}

            PermissionStatus statusVibrate = await Permissions.CheckStatusAsync<Permissions.Vibrate>();
            if (statusVibrate == PermissionStatus.Denied)
            {
                if (await Permissions.RequestAsync<Permissions.Vibrate>() == PermissionStatus.Denied)
                {
                    await RequestPermissions();
                }
            }

        }
	}
}

