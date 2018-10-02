using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing;

namespace PhotoDataModel_V2
{

 
    public class PhotoDataItem
    {
      
        public string UniqueId
        {
            get ;
            set ;
        }

         public string Title
        {
            get ;
            set ;
        }

   
        public string OwnerName
        {
            get ;
            set ;
        }

         public string DateTaken
        {
            get ;
            set ;
        }

         public string Description
        {
            get ;
            set ;
        }

        private String _imagePath = null;

        public String ImagePath
        {
            get ;
            set ;
        }
        public Image Image
        {
            get;
            set;
        }
        public override string ToString()
        {
            return this.Title;
        }
    }

  

    public class PhotoDataSource
    {
        public  static List<PhotoDataItem> Search(string SearchTerm = "Rivers",     int results = 50)
        {
        
            string url = string.Format(
                "https://api.flickr.com/services/rest/?" +
                "method=flickr.photos.search&" +
                "api_key=ec2ac3f3b314abae67f27cfc85e21998" +
                "&text=" + SearchTerm + "&sort=" + "interestingness-desc" +
                "&safe_search=" + "1" + "&content_type=" + "1" +
                "&license=1,2,3,4,5,6,7&per_page=" + results + "&page=1&" +
                "extras=owner_name,description,date_upload,date_taken," +
                "geo,tags,url_sq,url_t,url_s,url_m,url_o" +
                "&has_geo1&media=photos&content_type=1&safe_search=1");

            // Download the data to a buffer.
            WebClient client = new WebClient();

            Byte[] pageData = client.DownloadData(url);
           
            string pageXml = Encoding.ASCII.GetString(pageData);
            //Console.WriteLine(pageXml);

            XDocument xDocument = XDocument.Parse(pageXml);

            var photoQuery =
                from entry in xDocument.Descendants("photo")
                select new PhotoDataItem
                {
                     Title = (String)entry.Attribute("title"),
                    OwnerName = (String)entry.Attribute("ownername"),
                    ImagePath = (String)entry.Attribute("url_m"),
                    Description = (String)entry.Element("description"),
                    DateTaken = (String)entry.Attribute("datetaken")
                };

             return photoQuery.ToList();

        }


       //.NET Framework 4.5 非同步
       async   public  static Task < List<PhotoDataItem>> SearchAsync(string SearchTerm = "Rivers", int results = 50)
        {

            string url = string.Format(
                "https://api.flickr.com/services/rest/?" +
                "method=flickr.photos.search&" +
                "api_key=ec2ac3f3b314abae67f27cfc85e21998" +
                "&text=" + SearchTerm + "&sort=" + "interestingness-desc" +
                "&safe_search=" + "1" + "&content_type=" + "1" +
                "&license=1,2,3,4,5,6,7&per_page=" + results + "&page=1&" +
                "extras=owner_name,description,date_upload,date_taken," +
                "geo,tags,url_sq,url_t,url_s,url_m,url_o" +
                "&has_geo1&media=photos&content_type=1&safe_search=1");

            // Download the data to a buffer.
            //httpClient
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string pageXML = await response.Content.ReadAsStringAsync();


            XDocument xDocument = XDocument.Parse(pageXML);

            var photoQuery =
                 from entry in xDocument.Descendants("photo")

                 select new PhotoDataItem
                 {
                     Title = (String)entry.Attribute("title"),
                     OwnerName = (String)entry.Attribute("ownername"),
                     ImagePath = (String)entry.Attribute("url_m"),
                     Description = (String)entry.Element("description"),
                     DateTaken = (String)entry.Attribute("datetaken")
                 };

            List<PhotoDataItem> list =  await Task.Run(() => photoQuery.ToList()); //呼叫任意方法 for CPU bound  - 用 Task 加 await 會在背景做 看Timer
            return list;
        }

   
      static  Bitmap DownLoadData(string url)
       {
           try
           {

               HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
               myRequest.Method = "GET";
               HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
               System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
               myResponse.Close();

               return bmp;
           }
           catch
           {
               return null;// (Bitmap)this.pictureBox1.ErrorImage;
           }

       }

    }
}
