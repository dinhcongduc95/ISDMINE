using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
namespace MTBT.Web.Common
{
    using System.Web.Mvc;

    public static class Utilities
    {
       
        public static string MerchantInfor { get; set; }

        public static string FormatAmount(decimal Amount)
        {
            return string.Format("{0:N0}", Amount);
        }

        public static class IndexType
        {
            public static int ALL = 0;
            public static int MASOTHANHVIEN = 1;
            public static int MASOTHE = 2;
            public static int PINCODE = 3;
        }

        static Utilities()
        {
            if (string.IsNullOrEmpty(MerchantInfor))
            {
                MerchantInfor = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "SecureMerchant", "MerchantAPI");
            }

            
        }


        /// <summary>
        /// FirstCharToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// ConvertToListString
        /// </summary>
        /// <param name="listInts"></param>
        /// <returns></returns>
        public static List<string> ConvertToListString(List<long> listInts)
        {
            var listStrings = new List<string>();
            listInts.ForEach(id =>
            {
                var idString = id.ToString();
                listStrings.Add(idString);
            });
            return listStrings;
        }

        /// <summary>
        /// ConvertToListString
        /// </summary>
        /// <param name="listInts"></param>
        /// <returns></returns>
        public static string ConvertIntListToString(List<string> listInts)
        {
            string strIntList = string.Empty;
            listInts.ForEach(id =>
            {
                var idString = id.ToString();
                strIntList += idString + ",";
            });
            return strIntList.TrimMark();
        }

        public static List<int> ConvertStringToInts(string listStr)
        {
            
           
                if (string.IsNullOrEmpty(listStr) || !listStr.Contains(","))
                    return null;
                var lstStr = listStr.Split(',');
                if (lstStr.Any())
                {
                    var lstResult = new List<int>();
                    foreach (var s in lstStr)
                    {
                        int id;
                        if (int.TryParse(s,out id )) lstResult.Add(id);
                    }

                    if (lstResult.Any()) return lstResult;
                }
           
           
            return null;
        }

        /// <summary>
        /// ConvertToListLong
        /// </summary>
        /// <param name="listInts"></param>
        /// <returns></returns>
        public static List<long> ConvertToListLong(List<string> listInts)
        {
            var listLongs = new List<long>();
            listInts.ForEach(id =>
            {
                var idString = Convert.ToInt64(id);
                listLongs.Add(idString);
            });
            return listLongs;
        }

        /// <summary>
        /// IntListToLongList
        /// </summary>
        /// <param name="listInts"></param>
        /// <returns></returns>
        public static List<long> IntListToLongList(List<int> listInts)
        {
            var listLongs = new List<long>();
            listInts.ForEach(id =>
            {
                var idString = Convert.ToInt64(id);
                listLongs.Add(idString);
            });
            return listLongs;
        }

        /// <summary>
        /// ConvertToLisInt
        /// </summary>
        /// <param name="listInts"></param>
        /// <returns></returns>
        public static List<int> ConvertToLisInt(List<string> listInts)
        {
            var listLongs = new List<int>();
            listInts.ForEach(id =>
            {
                var idString = Convert.ToInt32(id);
                listLongs.Add(idString);
            });
            return listLongs;
        }

        /// <summary>
        /// CreateLogInforMessage
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="codeId"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Exception CreateLogInforMessage(string strMessage, int codeId, ControllerContext context)
        {
            string actionName = context.RouteData.Values["action"].ToString();
            string controllerName = context.RouteData.Values["controller"].ToString();
            string strException = string.Format("|Message: {0} |Code: {1} |Funtion: {2} |Controller: {3} |Time: {4}", strMessage, codeId, actionName, controllerName, DateTime.Now.ToShortDateString());
            return new Exception(strException);
        }

        /// <summary>
        /// ConvertToString
        /// </summary>
        /// <param name="listInts"></param>
        /// <returns></returns>
        public static string ConvertToString(List<long> listInts)
        {
            string strResult = string.Empty;
            listInts.ForEach(item =>
            {
                strResult += item.ToString() + ",";
            });
            strResult = strResult.Remove(strResult.Length - 1, 1);
            return strResult;
        }

        /// <summary>
        /// Mã hoá chuỗi
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string strKey)
        {
            if (string.IsNullOrEmpty(toEncrypt))
                return string.Empty;

            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            bool useHashing = true;
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(strKey));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(strKey);

            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// Giải mã chuỗi
        /// </summary>
        /// <param name="cipherString"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, string strKey)
        {
            if (string.IsNullOrEmpty(cipherString))
                return string.Empty;

            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            bool useHashing = true;
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(strKey));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(strKey);

            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// Write XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <param name="strFilePath"></param>
        public static void Write<T>(T user, string strFilePath)
        {
            var xsSubmit = new XmlSerializer(typeof(T));
            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, user);
                    var xml = sww.ToString();

                    if (File.Exists(strFilePath))
                        File.Delete(strFilePath);

                    if (!Directory.Exists(strFilePath))
                    {
                        Directory.CreateDirectory(strFilePath.Replace("\\MerchantAPI.xml", string.Empty));
                    }

                    using (var write = new StreamWriter(strFilePath))
                    {
                        write.Write(xml);
                    }
                }
            }
        }

        /// <summary>
        /// Read XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        

        /// <summary>
        /// ReadFromServices
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static T ReadFromServices<T>(string strXml)
        {                    
                var serializer = new XmlSerializer(typeof(T));
                string strReader = strXml;
                strReader = strReader.Replace("&", "&amp;");
                object deserialized = serializer.Deserialize(new StringReader(strReader));
                return (T)deserialized;

        }

        
        /// <summary>
        /// ConvertVn
        /// </summary>
        /// <param name="chucodau"></param>
        /// <returns></returns>
        public static string ToVnNonUnicode(this string chucodau)
        {
            if (string.IsNullOrEmpty(chucodau))
            {
                return string.Empty;
            }
            const string strFindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string strReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrChar = strFindText.ToCharArray();
            while ((index = chucodau.IndexOfAny(arrChar)) != -1)
            {
                int index2 = strFindText.IndexOf(chucodau[index]);
                chucodau = chucodau.Replace(chucodau[index], strReplText[index2]);
            }
            return chucodau;
        }



        /// <summary>
        /// CompareObjectList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static bool CompareObjectList<T>(List<T> list1, List<T> list2)
        {
            return (list1.Count == list2.Count) && !(list1.Except(list2).Any());
        }

        /// <summary>
        /// Sinh một dãy số ngẫu nhiên.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string RandomNumber(int size)
        {
            var builder = new StringBuilder();
            var random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < size - 1; i++)
            {
                var ch = random.Next(9).ToString();
                builder.Append(ch);
            }
            return builder.ToString();

        }
        /// <summary>
        /// Sinh một chuỗi ngẫu nhiên.
        /// </summary>
        /// <param name="Size">Độ dài của chuỗi</param>
        /// <returns></returns>
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
        /// <summary>
        /// Tạo một password ngẫu nhiên
        /// </summary>
        /// <returns></returns>
        public static string RandomPassword()
        {
            string part1 = RandomNumber(4);
            string part2 = RandomString(3);
            return string.Concat(part1, part2);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="originalPath">đường dẫn tới ảnh gốc</param>
        /// <param name="thumbSize">cỡ của ảnh sau khi resize</param>
        /// <returns></returns>
        public static bool ResizeImage(string originalPath, Size thumbSize)
        {
            var thumbPath = string.Empty;
            var thumbFilePath = string.Empty;
            if (!File.Exists(originalPath)) return false;
            try
            {
                var original = Image.FromFile(originalPath);
                var fileName = Path.GetFileName(originalPath);
                thumbPath = string.Concat(Path.GetDirectoryName(originalPath), @"\thumb\");
                if (!Directory.Exists(thumbPath)) Directory.CreateDirectory(thumbPath);
                thumbFilePath = thumbPath + fileName;
                var thumb = original.GetThumbnailImage(thumbSize.Width, thumbSize.Height, null, IntPtr.Zero);
                thumb.Save(thumbFilePath, thumb.RawFormat);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Get ckfinder thumb path.
        /// </summary>
        /// <param name="originalPath"></param>
        /// <returns></returns>
        public static string GetThumbImageUrl(string originalPath)
        {
            var thumbPath = originalPath;
            if (originalPath.Contains("uploads"))
            {
                var pos = originalPath.IndexOf("uploads", System.StringComparison.Ordinal);
                thumbPath = originalPath.Insert(pos + 7, "/_thumbs");
            }
            return thumbPath;
        }
        /// <summary>
        /// get youtube embed url.
        /// </summary>
        /// <param name="originalPath"></param>
        /// <returns></returns>
        public static string GetYouTubeEmbedUrl(this String originalPath)
        {
            return originalPath.Replace("watch?v=", "embed/");
        }
        /// <summary>
        /// Get YouTube video key.
        /// </summary>
        /// <param name="originalPath"></param>
        /// <returns></returns>
        public static string GetYouTubeVideoKey(this string originalPath)
        {
            if (string.IsNullOrWhiteSpace(originalPath)) return originalPath;
            var parts = originalPath.Split(new string[] { "v=" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 1) return parts[1];
            return originalPath;
        }


        /// <summary>
        /// Declare the event  LogUpdatedEvent
        /// </summary>
        public delegate void LogUpdatedEventHandler();
        public static event LogUpdatedEventHandler LogUpdatedEvent;

        /// <summary>
        /// Write log errror
        /// </summary>
        /// <param name="exc"></param>
        

        /// <summary>
        /// Rasise log update event
        /// </summary>
        private static void RaiseLogUpdatedEvent()
        {
            if (LogUpdatedEvent != null)
            {
                LogUpdatedEvent();
            }
        }

        /// <summary>
        /// Write log information
        /// </summary>
       

        public static string TrimMark(this string strText)
        {
            if (!string.IsNullOrEmpty(strText))
            {
                while (strText.IndexOf(',') == 0)
                {
                    strText = strText.Remove(0, 1);
                }

                while (strText.LastIndexOf(',') == strText.Length - 1)
                {
                    strText = strText.Remove(strText.Length - 1, 1);
                }
                return strText;
            }
            return string.Empty;
        }

        public static string MakeLicense(string text)
        {
            var strbuilder = new StringBuilder();
            if (text.Length == 40)
            {
                var chars = text.ToCharArray();

                for (int i = 0; i < chars.Length; i++)
                {
                    strbuilder.Append(chars[i]);
                    if (i + 1 == 10 || i + 1 == 20 || i + 1 == 30)
                    {
                        strbuilder.Append("-");
                    }
                }
            }
            return strbuilder.ToString();
        }

        /// <summary>
        /// Combine char in to string params array and except indexStart item
        /// </summary>
        /// <param name="chr">char need to combine</param>
        /// <param name="indexStart">Index element start</param>
        /// <param name="strStrings">string params array</param>
        /// <returns>string which combined</returns>
        public static string CombineChar(char chr, int indexStart = 0, params string[] strStrings)
        {
            string strResult = string.Empty;
            if (!strStrings.Any()) return strResult;
            var strbuilder = new StringBuilder();
            for (int i = 0; i < strStrings.Length; i++)
            {
                if (i < indexStart) continue;
                var item = strStrings[i];
                strbuilder.Append(item);
                strbuilder.Append(chr);
            }
            strResult = strbuilder.ToString().TrimMark();
            return strResult;
        }

        /// <summary>
        /// Combine char in to string params array and except indexStart item
        /// </summary>
        /// <param name="chr">char need to combine</param>
        /// <param name="indexStart">Index element start</param>
        /// <param name="strStrings">string params array</param>
        /// <returns>string which combined</returns>
        public static string CombineChar(char chr, int indexStart = 0, params long[] strStrings)
        {
            string strResult = string.Empty;
            if (!strStrings.Any()) return strResult;
            var strbuilder = new StringBuilder();
            for (int i = 0; i < strStrings.Length; i++)
            {
                if (i < indexStart) continue;
                var item = strStrings[i];
                strbuilder.Append(item);
                strbuilder.Append(chr);
            }
            strResult = strbuilder.ToString().TrimMark();
            return strResult;
        }

        /// <summary>
        /// CombineChar
        /// </summary>
        /// <param name="chr"></param>
        /// <param name="indexStart"></param>
        /// <param name="strStrings"></param>
        /// <returns></returns>
        public static string CombineChar(char chr, int indexStart = 0, params int[] strStrings)
        {
            string strResult = string.Empty;
            if (!strStrings.Any()) return strResult;
            var strbuilder = new StringBuilder();
            for (int i = 0; i < strStrings.Length; i++)
            {
                if (i < indexStart) continue;
                var item = strStrings[i];
                strbuilder.Append(item);
                strbuilder.Append(chr);
            }
            strResult = strbuilder.ToString().TrimMark();
            return strResult;
        }

        /// <summary>
        /// GetLocalIpAddress
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

       
    }
}