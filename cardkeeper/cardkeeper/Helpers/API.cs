using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace cardkeeper.Helpers
{
    public static class API
    {
        public static async Task<byte[]> GetQRCode(string accountNumber)
        {
            try
            {
                string url = $"http://api.qrserver.com/v1/create-qr-code/?data={accountNumber}!&size=150x150";
                WebRequest request = WebRequest.Create(url);
                WebResponse response = await request.GetResponseAsync();
                BinaryReader reader = new BinaryReader(response.GetResponseStream());
                byte[] qrCode = reader.ReadBytes(22500);
                return qrCode;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

    }
}
