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
        public static async Task<byte[]> GetScanCode(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = await request.GetResponseAsync();
                BinaryReader reader = new BinaryReader(response.GetResponseStream());
                byte[] scanCode = reader.ReadBytes(22500);
                return scanCode;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }
        public static string GetGoogleQRCodeService(string accountNumber)
        {
            return $"https://chart.googleapis.com/chart?cht=qr&chs=150x150&chl={accountNumber}";
        }
        public static string GetOtherQRCodeService(string accountNumber)
        {
            return $"http://api.qrserver.com/v1/create-qr-code/?data={accountNumber}&size=150x150";

        }
        public static string GetBarCodeService(string accountNumber)
        {
            return $"http://www.barcodes4.me/barcode/c128a/{accountNumber}.png";
        }
        public static string GetWeatherService(string accountNumber)
        {
            string weatherKey = "fb8f729d0a513505";

            return $"http://api.wunderground.com/api/{weatherKey}/conditions/q/WI/Franklin.json";
        }
    }
}
