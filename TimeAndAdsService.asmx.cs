﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace WebApplication1
{

    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class TimeAndAdsService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCurrentDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetRandomAd()
        {
            string adFolderPath = Server.MapPath("~/Ads");

            if (!Directory.Exists(adFolderPath))
            {
                throw new DirectoryNotFoundException("Ad folder not found.");
            }

            string[] adImages = Directory.GetFiles(adFolderPath, "*.jpg");

            if (adImages.Length < 1)
            {
                throw new FileNotFoundException("No ad images found in the folder.");
            }

            Random random = new Random();

            // Get two random images, even if they are the same
            string randomAdImage1 = adImages[random.Next(adImages.Length)];
            string randomAdImage2 = adImages[random.Next(adImages.Length)];

           
            string relativePath1 = "/Ads/" + Path.GetFileName(randomAdImage1);
            string relativePath2 = "/Ads/" + Path.GetFileName(randomAdImage2);

            var response = new
            {
                ImagePath1 = relativePath1,
                ImagePath2 = relativePath2
            };

            return JsonConvert.SerializeObject(response);
        }

    }
}
