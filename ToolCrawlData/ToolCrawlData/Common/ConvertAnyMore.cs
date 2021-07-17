using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolCrawlData.Common
{
    public static class ConvertAnyMore
    {
        // Token: 0x0600006F RID: 111 RVA: 0x0000937C File Offset: 0x0000757C
        public static string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        // Token: 0x06000070 RID: 112 RVA: 0x000093A0 File Offset: 0x000075A0
        public static string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
