namespace QCimiss
{
    using cma.cimiss;
    using cma.cimiss.client;
    using Ice;
    using QCimiss.Properties;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public static class Cimiss
    {
        public static string Api_getDatasetInfo(string findtype, string classid, string datacode) => 
            webclientmethed("http://10.104.89.28:8008/cimissapiweb/apiwebuserinfo_findBy" + findtype + ".action", "ids=" + classid + "&dataCode=" + datacode);

        public static string Api_getElements(string classid)
        {
            string urlpost = "http://10.104.89.28:8008/cimissapiweb/apidatadefine_getElements.action";
            return webclientmethed(urlpost, "ids=" + classid);
        }

        public static RetFilesInfo downFiles(string interfaceId, Dictionary<string, string> param, string savePath)
        {
            DataQueryClient client = new DataQueryClient();
            RetFilesInfo retFilesInfo = new RetFilesInfo();
            try
            {
                client.initResources();
                int num = client.callAPI_to_downFile(Settings.Default.APIuser, Settings.Default.APIpassword, interfaceId, param, savePath, retFilesInfo);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                client.destroyResources();
            }
            return retFilesInfo;
        }

        public static RetArray2D getArray2D(string interfaceId, Dictionary<string, string> param)
        {
            DataQueryClient client = new DataQueryClient();
            RetArray2D arrayd = new RetArray2D();
            try
            {
                client.initResources();
                int num = client.callAPI_to_array2D(Settings.Default.APIuser, Settings.Default.APIpassword, interfaceId, param, arrayd);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                client.destroyResources();
            }
            return arrayd;
        }

        public static RetGridArray2D getNWF2D(string interfaceId, Dictionary<string, string> param)
        {
            DataQueryClient client = new DataQueryClient();
            RetGridArray2D arrayd = new RetGridArray2D();
            try
            {
                client.initResources();
                if (client.callAPI_to_gridArray2D(Settings.Default.APIuser, Settings.Default.APIpassword, interfaceId, param, arrayd) != 0)
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                client.destroyResources();
            }
            return arrayd;
        }

        public static string IdentifierCode_findByparamId(string paramid)
        {
            string urlpost = "http://10.104.89.28:8008/cimissapiweb/IdentifierCode_findByparamId.action";
            return webclientmethed(urlpost, "paramId=" + paramid);
        }

        public static RetFilesInfo saveasFile(string interfaceId, Dictionary<string, string> param, string savePath, string format)
        {
            DataQueryClient client = new DataQueryClient();
            RetFilesInfo retFilesInfo = new RetFilesInfo();
            try
            {
                client.initResources();
                int num = client.callAPI_to_saveAsFile(Settings.Default.APIuser, Settings.Default.APIpassword, interfaceId, param, format, savePath, retFilesInfo);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                client.destroyResources();
            }
            return retFilesInfo;
        }

        private static string webclientmethed(string urlpost, string postString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(postString);
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            client.Headers.Add("Accept-Encoding", "gzip, deflate");
            client.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            client.Headers.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            client.Headers.Add("Pragma", "no-cache");
            client.Headers.Add("Origin", "http://10.104.89.28:8008");
            client.Headers.Add("Referer", "http://10.104.89.28:8008/cimissapiweb/apiwebuserinfo_userService.action");
            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36");
            client.Headers.Add("Cookie", "JSESSIONID=F0B7092B4B955AEB9489C0E6219B5CE2");
            client.Headers.Add("X-Requested-With", "XMLHttpRequest");
            string str = "";
            try
            {
                byte[] buffer2 = client.UploadData(urlpost, "POST", bytes);
                str = Encoding.UTF8.GetString(buffer2);
            }
            catch
            {
            }
            return str;
        }
    }
}

