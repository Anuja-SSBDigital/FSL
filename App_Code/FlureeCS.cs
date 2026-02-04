using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using log4net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Linq;

[assembly: log4net.Config.XmlConfigurator(ConfigFile =
                "log4net.config", Watch = true)]

/// <summary>
/// Summary description for FlureeCS
/// </summary>public class FlureeCS
public class FlureeCS
{
    HttpWebRequest request;
    private static readonly ILog log = LogManager.GetLogger
          (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static string token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzc2IvZGZzIiwic3ViIjoiVGZHRnNvQzUzOFA2M0o5Y1JGQTc5MURkZDhnWEJoQ0RhUnIiLCJleHAiOjE3MDg4ODkxMTI5NjYsImlhdCI6MTY0NTc3NTIwODk2NiwicHJpIjoiYjc5ZTgxZmQzOTg5MGE5MTczMDI2MWEyZDM0NTQwMjQxMTNmZWM4M2U4YmZjMWE3NmFjNzU2MDNhZWJjNzllODY1M2U4MDRkN2Y3MGYzZGJlNzBhZGNhMWYyMGY5NGEyNmM5Mzg5NzQ3NjFmMzk1NmJhYTUwOTQ2MjZkN2RiZjVlMTc1NjgxYzNhZTBmZDg2MTVmMGQ3ZGQ2NWMxNTMwOCJ9.nm73nNsD1sW1isCH6JX1NG7Fp28kB5GjjXKKNYT0hPs";
    //public static string token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ1bml2ZXJzaXR5L2NlcnRpZmljYXRlZGVtbyIsInN1YiI6IlRmSFpxSFpVeTI4aXJlY3dNMzdBbkZwVUxacTZScnVHRG9oIiwiZXhwIjoxNjA4NzIzMDY5MzUzLCJpYXQiOjE1NzcxODcwNjkzNTMsInByaSI6Ijg3NTc3MjM1M2M1ZTM4MWYwMWI3NGYyZjIwNzlhMDQzODVkNWZkOTg2ZDY4YTIzOWE5NzkzZjMzMTE4ZWYzNDk0YjkzMWIyNzVlNmRkODQ5MWU1N2I4MjIyMTllMDQ3MGNlMzc2Y2U3MDY4YWE5OTU4Yjg4MGIxMzMxOTFkNjNmYzlmZGRiM2I5MDgyMzZiYzBiNzVmNzdjOTU0MzNmZDgifQ.yrpIN9YIBKTHnRTrBiP2njDkCulpWSWpWQZWZRfIUWQ";
    //public static string servertranurl = "http://122.170.117.118:8084/fdb/university/certificatedemo/transact";
    //public static string serverqryurl = "http://122.170.117.118:8084/fdb/university/certificatedemo/query";
    //public static string servermulqryurl = "http://122.170.117.118:8084/fdb/universihttp://localhost:61653/css/ty/certificatedemo/multi-query";

    //public static string servertranurl = "http://122.170.117.118:8012/fdb/ssb/dfs/transact";
    //public static string serverqryurl = "http://122.170.117.118:8012/fdb/ssb/dfs/query";
    //public static string servermulqryurl = "http://122.170.117.118:8012/fdb/ssb/dfs/multi-query";

    public static string servertranurl = "http://localhost:8091/fdb/ssb/fsl/transact";
    public static string serverqryurl = "http://localhost:8091/fdb/ssb/fsl/query";
    public static string servermulqryurl = "http://localhost:8091/fdb/ssb/fsl/multi-query";

    //public static string servertranurl = "http://192.168.1.246:8090/fdb/ssb/dfs/transact";
    //public static string serverqryurl = "http://192.168.1.246:8090/fdb/ssb/dfs/query";
    //public static string servermulqryurl = "http://192.168.1.246:8090/fdb/ssb/dfs/multi-query";


    //public static string servertranurl = "http://192.168.1.246:8090/fdb/ssb/fsl/transact";
    //public static string serverqryurl = "http://192.168.1.246:8090/fdb/ssb/fsl/query";
    //public static string servermulqryurl = "http://192.168.1.246:8090/fdb/ssb/fsl/multi-query";

    public static DataTable TempTable()
    {
        DataTable dt = new DataTable();

        DataRow dr = dt.NewRow();

        DataColumn col = new DataColumn();
        col.ColumnName = "Name";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "Value";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "Key";
        col.DataType = typeof(int);
        dt.Columns.Add(col);

        return dt;
    }

    public static DataTable GetStatusTable()
    {
        DataTable dt = TempTable();

        DataRow dr = dt.NewRow();
        dr["Name"] = "Active";
        dr["Key"] = 0;
        dr["Value"] = "Active";
        dt.Rows.Add(dr);
        dt.AcceptChanges();

        dr = dt.NewRow();
        dr["Name"] = "Assigned";
        dr["Key"] = 1;
        dr["Value"] = "Assigned";
        dt.Rows.Add(dr);
        dt.AcceptChanges();

        dr = dt.NewRow();
        dr["Name"] = "In Process";
        dr["Key"] = 2;
        dr["Value"] = "In Process";
        dt.Rows.Add(dr);
        dt.AcceptChanges();

        dr = dt.NewRow();
        dr["Name"] = "Send To IO";
        dr["Key"] = 3;
        dr["Value"] = "Send To IO";
        dt.Rows.Add(dr);
        dt.AcceptChanges();

        dr = dt.NewRow();
        dr["Name"] = "Received By IO";
        dr["Key"] = 4;
        dr["Value"] = "Received By IO";
        dt.Rows.Add(dr);
        dt.AcceptChanges();

        return dt;

    }

    public static DataTable dtStatus = GetStatusTable();

    #region Other
    private Random _random = new Random();

    public string EncryptString(string str)
    {
        MD5 md5Hash = MD5.Create();
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
        // Create a new Stringbuilder to collect the bytes  
        // and create a string.  
        StringBuilder sBuilder = new StringBuilder();
        // Loop through each byte of the hashed data  
        // and format each one as a hexadecimal string.  
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }

    public string GenerateRandomNo()
    {
        return _random.Next(0, 999999).ToString("D6");
    }

    public string GetIP()
    {
        string strHostName = "";
        strHostName = System.Net.Dns.GetHostName();

        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

        IPAddress[] addr = ipEntry.AddressList;

        return addr[addr.Length - 1].ToString();

    }

    public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

    public long ConvertToTimestamp(DateTime value)
    {
        TimeSpan elapsedTime = value - Epoch;
        return (long)elapsedTime.TotalMilliseconds;
    }


    public double ConvertDateTimeToTimestamp(DateTime value)
    {
        TimeSpan epoch = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        //return the total seconds (which is a UNIX timestamp)
        return (double)epoch.TotalMilliseconds;
    }

    public string sendTransaction(string DatatoPost, string url)
    {
        log.Info("Web request called");
        log.Info("query :" + DatatoPost + "");
        request = (HttpWebRequest)WebRequest.Create(url);
        ////request.Timeout = 360000;
        ////request.ReadWriteTimeout = 360000;
        ////request.KeepAlive = true;
        //if (token != "")
        //    request.Headers.Add("Authorization", token);
        request.Method = "POST";
        try
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(DatatoPost);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            log.Info("GetRequestStream function called");
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();
            log.Info("Request executed");
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            return responseFromServer;

        }
        catch (Exception ex)
        {
            log.Error("************** Error : " + ex.Message, ex);

            return "Error : " + ex.Message;
        }
    }


    public DataTable Tabulate(string json)
    {
        var trgArray = new JArray();
        try
        {
            var jsonLinq = JArray.Parse(json);
            //var jsonLinq = JArray.Parse(json);

            // Find the first array using Linq
            foreach (JObject row in jsonLinq.Children<JObject>())
            {
                var cleanRow = new JObject();

                //var srcArray = jsonLinq.Descendants().Where(d => d is JProperty);
                foreach (JToken column in row.Children<JToken>())
                {
                    if (column.First is JArray)
                    {
                        try
                        {
                            JProperty jt = (JProperty)column;

                            JArray res = JArray.Parse("[" + jarray((JArray)column.First) + "]");
                            foreach (JObject jares in res.First.Children<JObject>())
                                foreach (JToken colpck in jares.Children<JToken>())
                                {
                                    if (colpck.First is JValue)
                                    {
                                        try
                                        {
                                            foreach (JValue colv in colpck)
                                            {
                                                // Only include JValue types
                                                if (colv.Parent is JProperty)
                                                {
                                                    JProperty jtv = (JProperty)colv.Parent;
                                                    //if (col.Value is JValue)
                                                    //{
                                                    cleanRow.Add(jtv.Name, jtv.Value);
                                                    //}
                                                }
                                            }
                                        }
                                        catch (Exception ex) { }
                                    }
                                    //trgArray.Merge(jares);
                                }
                        }
                        catch (Exception ex) { }

                    }
                    else if (column.First is JValue)
                    {
                        try
                        {
                            foreach (JValue col in column)
                            {
                                // Only include JValue types
                                if (col.Parent is JProperty)
                                {
                                    JProperty jt = (JProperty)col.Parent;
                                    //if (col.Value is JValue)
                                    //{
                                    cleanRow.Add(jt.Name, jt.Value);
                                    //}
                                }
                            }
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        try
                        {
                            foreach (JToken t in column.Children<JToken>())
                            {
                                foreach (JProperty v in t)
                                {
                                    string prop = v.Name;
                                    //JProperty p = (JProperty)v.Parent;
                                    if (cleanRow.ContainsKey(v.Name))
                                    {
                                        JProperty colp = (JProperty)column;
                                        prop = colp.Name + "_" + prop;
                                    }
                                    cleanRow.Add(prop, v.Value);
                                }
                            }
                        }
                        catch (Exception ex) { }

                    }

                }
                trgArray.Add(cleanRow);
            }

        }
        catch (Exception ex) { }
        return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
    }

    public JArray jarray(JArray ja)
    {
        JObject cleanRows = new JObject();
        JArray jarow = new JArray();
        foreach (JToken column in ja.Children<JToken>())
        {
            JObject cleanRow = new JObject();
            if (column.Children().Count() > 0)
            {
                if (column.First is JArray)
                {
                    cleanRow.Add(jarray((JArray)column.First));
                }
                else if (column.First is JValue)
                {
                    foreach (JValue col in column)
                    {
                        // Only include JValue types
                        if (col.Parent is JProperty)
                        {
                            JProperty jt = (JProperty)col.Parent;
                            //if (col.Value is JValue)
                            //{
                            cleanRow.Add(jt.Name, jt.Value);
                            //}
                        }
                    }
                }
                else
                {
                    try
                    {
                        foreach (JToken t in column.Children<JToken>())
                        {

                            foreach (JToken col in t.Children<JToken>())
                            {
                                // Only include JValue types
                                try
                                {
                                    if (col is JValue)
                                    {
                                        //foreach (JValue col in column)
                                        //{
                                        //    // Only include JValue types
                                        if (col.Parent is JProperty)
                                        {
                                            JProperty jt = (JProperty)col.Parent;
                                            //if (col.Value is JValue)
                                            //{
                                            cleanRow.Add(jt.Name, jt.Value);
                                            //}
                                        }
                                        //}
                                    }
                                    else
                                    {
                                        foreach (JProperty jt in col.Children<JProperty>())
                                        {
                                            if (jt.First is JValue)
                                                cleanRow.Add(jt.Name, jt.Value);
                                            else
                                            {
                                                foreach (JObject jb in jt.Children<JObject>())
                                                {
                                                    foreach (JProperty jt1 in jb.Children<JProperty>())
                                                        cleanRow.Add(jt1.Name, jt1.Value);
                                                }
                                            }

                                        }
                                    }
                                }
                                catch (Exception ex) { }
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            else
            {
                JProperty jt = (JProperty)column.Parent;
                //if (col.Value is JValue)
                //{
                cleanRow.Add(jt.Name, jt.Value);
            }
            jarow.Add(cleanRow);
        }
        //cleanRows.Add(jarow);
        return jarow;
    }

    public string SHA256CheckSum(Stream fs)
    {
        using (SHA256 SHA256 = SHA256Managed.Create())
        {
            //using (FileStream fileStream = File.OpenRead(filePath))
            byte[] bt = SHA256.ComputeHash(fs);
            string str = "";
            foreach (byte b in bt)
                str += b.ToString("x2");
            return str;
        }
    }

    #endregion

    #region App Functions

    #region Select
    public string GetInstById(string id)
    {
        string res = "";

        string qry = "{";

        qry += "\"select\":{"
            + "\"?ins\":["
            + "\"inst_name\""
            + ",\"inst_code\""
            + ",\"inst_id\""
            + ",\"location\""
            + "]"
            + "},";
        qry += "\"where\":[";
        if (id != "-1")
            qry += "[\"?ins\""
                + ",\"institute_master/inst_id\""
                + ",\"" + id + "\"],";

        qry += "[\"?ins\",\"institute_master/inst_name\",\"?name\"],["
            + "\"?ins\",\"institute_master/is_deleted\",false"
            + "]],"
            + "\"orderBy\":\"?name\"";

        qry += "}";
        res = sendTransaction(qry, serverqryurl);

        return res;
    }

    public string Updatereportrequest(string id, string hodstatus, string hodremarks, string approvedby)
    {

        string res = "[{\"_id\":[\"reportrequest/requestid\",\"" + id
                + "\"],\"hodstatus\":\"" + hodstatus
                + "\",\"hodremarks\":\"" + hodremarks
                + "\",\"approvedby\":\"" + approvedby
                + "\",\"hoddecisionon\":" + ConvertToTimestamp(DateTime.Now)
                + ",\"updateddate\":" + ConvertToTimestamp(DateTime.Now)
                + "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string GetUsersForUpdate(string userid, string instid, string divid)
    {
        string res = "{"
            + "\"select\":{\"?user\":["
            + "\"userid\","
            + "\"firstname\","
            + "\"username\","
            + "\"email\","
            + "\"mobileno\","
            + "\"designation\","
            + "{\"role_id\":[\"role\"]},"
            + "{\"inst_id\":[\"inst_name\",\"inst_code\",\"inst_id\",\"location\"]},"
            + "{\"dept_id\":[\"dept_name\",\"dept_code\",\"dept_id\"]},"
            + "{\"div_id\":[\"div_name\",\"div_code\"]},"
             + "\"profileimage\","
            + "\"promotionletter\","
            + "\"appointmentletter\","
            + "\"appointmentletterrhash\","
            + "\"promotionletterhash\","
            + "\"lastname\""
            + "]},"
            + "\"where\":[";
        if (userid != "-1")
        {
            res += "["
            + "\"?user\",\"userdetails/userid\",\"?u\""
            + "],";
        }

        res += "["
        + "\"?user\",\"userdetails/inst_id\","
        + "[\"institute_master/inst_code\",\"" + instid + "\"]"
        + "],";
        res += "["
        + "\"?user\",\"userdetails/div_id\","
        + "[\"division_master/div_code\",\"" + divid + "\"]"
        + "],["
            + "\"?user\",\"userdetails/is_deleted\",false"
            + "]]";
        if (userid != "-1")
        {
            res += ","
        + "\"filter\":[\"(= ?u \\\"" + userid + "\\\")\"]";
        }
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string Getrequestidbycaseno(string caseno)
    {
        string res = "{"
          + "\"select\":{\"?report\":["
          + "\"requestid\","
          + "\"status\","
          + "\"requestedon\","
          + "\"hodstatus\","
          + "\"requestedby\","
          + "\"officerstatus\","
          + "\"filedownloadedon\","
          + "\"approvedby\","
          + "\"remarksbyofficer\","
          + "\"hodremarks\","
          + "\"createddate\""
               + "]},"
          + "\"where\":[[\"?report\",\"reportrequest/createddate\",\"?createddate\"],"
          + "[\"?report\",\"reportrequest/caseno\",\"" +
          caseno + "\"]],"

           + "\"orderBy\":\"createddate\""
          + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetAllUsers(string UserID, string InstID, string deptcode, string divcode)
    {
        string res = "";
        string qry = "{";
        qry += "\"select\": {";
        qry += "\"?user\":";
        qry += "[";
        qry += "\"_id\",";
        qry += "\"userid\",";
        qry += "\"firstname\",";
        qry += "\"lastname\",";
        qry += "\"username\",";
        qry += "\"password\",";
        qry += "\"email\",";
        qry += "\"designation\",";
        qry += "\"status\",";
        qry += "{\"role_id\":[\"role\"]},";
        qry += "{\"inst_id\":[\"inst_name\",\"inst_code\",\"inst_id\",\"location\"]},";
        qry += "{\"dept_id\":[\"dept_name\",\"dept_code\",\"dept_id\"]},";
        qry += "{\"div_id\":[\"div_name\",\"div_code\"]}";
        qry += "]";
        qry += "},";
        qry += "\"where\": [";
        if (UserID != "-1")
        {
            qry += "["
         + "\"?user\",\"userdetails/userid\",\"" + UserID + "\""
         + "],";
        }
        if (InstID != "")
        {
            qry += "["
        + "\"?user\",\"userdetails/inst_id\","
        + "[\"institute_master/inst_code\",\"" + InstID + "\"]"
        + "],";

        }
        if (deptcode != "")
        {
            qry += "["
            + "\"?user\",\"userdetails/dept_id\","
            + "[\"department_master/dept_code\",\"" + deptcode + "\"]"
            + "],";
        }
        if (divcode != "")
        {
            qry += "["
             + "\"?user\",\"userdetails/div_id\","
             + "[\"division_master/div_code\",\"" + divcode + "\"]"
             + "],";

        }
        qry += "["
            + "\"?user\",\"userdetails/is_deleted\",false"
            + "]]";
        qry += "}";

        res = sendTransaction(qry, serverqryurl);

        return res;
    }

    public string forgotpassword(string username, string emailid)
    {
        string res = "";
        string qry = "{";
        qry += "\"select\": {";
        qry += "\"?user\":";
        qry += "[";
        qry += "\"_id\",";
        qry += "\"userid\",";
        qry += "\"username\",";
        qry += "\"password\",";
        qry += "\"email\"";
        qry += "]";
        qry += "},";
        qry += "\"where\": [";
        qry += "["
         + "\"?user\",\"userdetails/username\",\"" + username + "\""
         + "],";

        qry += "["
        + "\"?user\",\"userdetails/email\",\"" + emailid + "\""
        + "],";

        qry += "["
            + "\"?user\",\"userdetails/is_deleted\",false"
            + "]]";
        qry += "}";

        res = sendTransaction(qry, serverqryurl);

        return res;
    }



    public string GetHDById(string id)
    {
        string res = "";

        string qry = "{";

        qry += "\"select\":{"
            + "\"?hd\":["
            + "\"hd_id\""
            + ",\"hd_name\""
            + ",\"sr_num\""
            + ",\"capacity\""
            + ",\"brand\""
            + ",{\"userid\":[\"userid\",\"username\"]}"
            + "]"
            + "},";
        qry += "\"where\":[["
            + "\"?hd\""
            + ",\"harddisk_master/hd_id\""
            + ",\"" + id + "\""
            + "],["
            + "\"?cert\",\"harddisk_master/is_deleted\",false"
            + "]]";

        qry += "}";
        res = sendTransaction(qry, serverqryurl);

        return res;
    }

    public string GetDeptId(string id)
    {
        string res = "";

        string qry = "{";

        qry += "\"select\":{"
            + "\"?cert\":["
            + "\"dept_name\""
            + ",\"dept_code\""
            + ",\"dept_id\""
            + ",{\"inst_id\":[\"inst_name\",\"inst_id\"]}"
            + "]"
            + "},";
        qry += "\"where\":[["
            + "\"?cert\""
            + ",\"department_master/dept_id\""
            + ",\"" + id + "\""
            + "],["
            + "\"?cert\",\"department_master/is_deleted\",false"
            + "]]";

        qry += "}";
        res = sendTransaction(qry, serverqryurl);

        return res;
    }

    public string GetUserforAdmin(string InstID)
    {
        string res = "{"
          + "\"select\":{\"?user\":["
          + "\"userid\","
          + "\"designation\""
          + ",\"firstname\""
          + ",\"lastname\""
          + ",\"username\""
          + ",\"isactive\""
          + ",\"email\""
          + ",{\"inst_id\":[\"inst_id\",\"inst_name\"]}"
          + ",{\"div_id\":[\"div_id\",\"div_name\"]}"
          + ",{\"dept_id\":[\"dept_id\",\"dept_name\"]}"

          + "]},"
          + "\"where\":[[\"?user\",\"userdetails/username\",\"?username\"]";

        if (InstID != "")
        {
            res += ",["
           + "\"?user\",\"userdetails/inst_id\",[\"institute_master/inst_id\",\"" +
           InstID + "\"]]";

        }




        res += "],"
                 + "\"filter\":[\"(and(not (= ?user \\\"ALL Division\\\"))(not (= ?user \\\"Other Division Sample Warden\\\")))\"],"

        + "\"orderBy\":\"?user\""
        + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string GetCasenofor_Bulk(string divcode, string status, string userid)
    {
        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"caseno\","
            + "\"agencyname\","
            + "\"agencyreferanceno\","
            + "\"caseassign_userid\","
            + "\"status\""
           + "]},"
            + "\"where\":[";

        res += "["
         + "\"?case\",\"evidence_acceptancedetails/evidenceid\",\"?u\""
         + "],";

        res += "["
        + "\"?case\",\"evidence_acceptancedetails/div_code\",\"" + divcode + "\""

        + "],";

        res += "["
       + "\"?case\",\"evidence_acceptancedetails/caseassign_userid\",\"" + userid + "\""

       + "]";

        res += ",["
        + "\"?case\",\"evidence_acceptancedetails/status\",\"" + status + "\""

        + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetUserdeptwise(string dept)
    {
        string res = "{"
          + "\"select\":{\"?user\":["
         + "\"userid\","
          + "\"designation\""
          + ",\"firstname\""
          + ",\"lastname\""
          + ",\"username\""
          + ",\"isactive\""
          + ",\"email\""
          + ",{\"dept_id\":[\"dept_id\",\"dept_code\"]}"
          + ",{\"inst_id\":[\"inst_id\",\"inst_name\"]}"
          + ",{\"div_id\":[\"div_id\",\"div_name\"]}"
          + "]},"
          + "\"where\":[[\"?user\",\"userdetails/username\",\"?username\"]";

        if (dept != "")
        {
            res += ",[\"?user\",\"userdetails/dept_id\",[\"department_master/dept_id\",\"" +
        dept + "\"]]";
        }
        res += "],"

         + "\"orderBy\":\"?user\""
        + "}";


        // string res = "{"
        //  + "\"select\":{\"?user\":["
        // + "\"userid\","
        //  + "\"designation\""
        //  + ",\"firstname\""
        //  + ",\"lastname\""
        //  + ",\"username\""
        //  + ",\"email\""
        //+ ",{\"role_id\":[\"role_id\",\"role\"]}"
        //  + ",{\"dept_id\":[\"dept_id\",\"dept_code\"]}"
        //  + ",{\"inst_id\":[\"inst_id\",\"inst_name\"]}"
        //  + ",{\"div_id\":[\"div_id\",\"div_name\"]}"
        //  + "]},"
        //  + "\"where\":[[\"?user\",\"userdetails/username\",\"?username\"]"
        //  + ",[\"?user\",\"userdetails/role_id\",[\"role/role\",\"?role\"]]";

        // if (dept != "")
        // {
        //     res += ",[\"?user\",\"userdetails/dept_id\",[\"department_master/dept_id\",\"" +
        // dept + "\"]]";
        // }
        // res += "],";

        // res += "\"filter\":[\"(not (= ?role \\\"Department Head\\\"))\"],"
        // + "\"orderBy\":\"?user\""
        // + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetUserforSuperAdmin(string instID, string deptId)
    {

        string res = "{"
          + "\"select\":{\"?user\":["
         + "\"userid\","
          + "\"designation\""
          + ",\"firstname\""
          + ",\"lastname\""
          + ",\"username\""
          + ",\"isactive\""
          + ",\"email\""
          + ",{\"dept_id\":[\"dept_id\",\"dept_code\"]}"
          + ",{\"inst_id\":[\"inst_id\",\"inst_name\"]}"
          + ",{\"div_id\":[\"div_id\",\"div_name\"]}"
          + "]},"
          + "\"where\":[[\"?user\",\"userdetails/username\",\"?username\"]";

        if (instID != "")
        {
            res += ",[\"?user\",\"userdetails/inst_id\",[\"institute_master/inst_id\",\"" +
        instID + "\"]]";
        }

        if (deptId != "")
        {
            res += ",[\"?user\",\"userdetails/dept_id\",[\"department_master/dept_id\",\"" +
        deptId + "\"]]";
        }

        res += "],"
         + "\"orderBy\":\"?user\""
        + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetEvidencebyId(string id)
    {
        string res = "";

        string qry = "{";

        qry += "\"select\":{"
            + "\"?evidence\":["
            + "\"caseno\""
            + ",\"evidenceid\""
            + ",\"dept_id\""
            + ",\"agencyname\""
            + ",\"noof_exhibits\""
            + ",\"receiptfilepath\""
              + ",\"agencyreferanceno\""
               + ",\"notes\""

            + "]"
            + "},";
        qry += "\"where\":[["
            + "\"?evidence\""
            + ",\"evidence_acceptancedetails/evidenceid\""
            + ",\"" + id + "\""
            + "]]";

        qry += "}";
        res = sendTransaction(qry, serverqryurl);

        return res;
    }

    public string GetCertiByHash(string hash)
    {
        string res = "";

        string qry = "{";

        qry += "\"select\":{"
            + "\"?cert\":["
            + "\"filename\""
            + ",\"type\""
            + ",\"createddate\""
            + ",\"path\""
            + "]"
            + "},";
        qry += "\"where\":[["
            + "\"?cert\""
            + ",\"attachement_master/hash\""
            + ",\"" + hash + "\""
            + "],["
            + "\"?cert\",\"attachement_master/is_deleted\",false"
            + "]]";

        qry += "}";
        res = sendTransaction(qry, serverqryurl);

        return res;
    }



    public string CheckLogin(string un, string pass)
    {
        string res = "";
        string qry = "{";
        qry += "\"select\": {";
        qry += "\"?user\":";
        qry += "[";
        qry += "\"_id\",";
        qry += "\"userid\",";
        qry += "\"firstname\",";
        qry += "\"lastname\",";
        qry += "\"username\",";
        qry += "\"password\",";
        qry += "\"designation\",";
        qry += "\"status\",";
        qry += "{\"role_id\":[\"role\"]},";
        qry += "{\"inst_id\":[\"inst_name\",\"inst_code\",\"inst_id\",\"location\"]},";
        qry += "{\"dept_id\":[\"dept_name\",\"dept_code\",\"dept_id\"]},";
        qry += "{\"div_id\":[\"div_name\",\"div_code\"]}";
        qry += "]";
        qry += "},";
        qry += "\"where\": [[\"?user\", "
            + "\"userdetails/username\", \"" + un + "\"],[\"?user\", "
            + "\"userdetails/password\", \"" + EncryptString(pass) + "\"],["
            + "\"?user\",\"userdetails/is_deleted\",false"
            + "],["
            + "\"?user\",\"userdetails/isactive\",\"1\""
            + "]]";
        qry += "}";

        res = sendTransaction(qry, serverqryurl);

        return res;
    }

    public string GetRightsByRole(string role)
    {
        string qry = "{"
             + "\"select\":{\"?right\":["
             + "\"rolerights\""
             + "]},"
             + "\"where\":[["
             + "\"?right\",\"rolerights/role_id\",["
             + "\"role/role\",\"" + role + "\""
             + "]]"
             + "]"
             + "}";
        string res = sendTransaction(qry, serverqryurl);

        return res;
    }

    public string GetLastCaseNo()
    {
        string res = "{\"selectOne\":\"(max ?num)\","
            //{\"?case\":[\"case_no\"]},"
            + "\"where\":[["
            + "\"?case\",\"case_master/No\",\"?num\""
            + "],["
            + "\"?case\",\"case_master/year\"," + DateTime.Now.Year + ""
            + "]]"
            + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string GetUsersDeptcodewiseafterIndexchanges(string deptcode)
    {
        string res = "{"
            + "\"select\":{\"?user\":["
            + "\"userid\","
            + "\"firstname\","
            + "\"lastname\""
            + "]},"
            + "\"where\":["
            + "[\"?user\",\"userdetails/dept_id\","
            + "[\"department_master/dept_code\",\"" + deptcode + "\"]]"
            + "]"
            + "}";

        string resp = sendTransaction(res, serverqryurl);
        return resp;
    }

    public string GetUsersDeptcodewise(string deptcode)
    {


        //string res = "{"
        //    + "\"select\":{\"?user\":["
        //    + "\"userid\","
        //    + "\"firstname\","
        //    + "\"lastname\""
        //   + "]},"
        //    + "\"where\":[";

        //res += "["
        //+ "\"?user\",\"userdetails/dept_id\","
        //+ "[\"department_master/dept_code\",\"" + deptcode + "\"]"
        //+ "]]";


        //res += "}";
        string res = "{"
         + "\"select\":{\"?user\":["
         + "\"userid\","
         + "\"firstname\","
         + "\"lastname\""
         + "]},"
         + "\"where\":["
         + "[\"?user\",\"userdetails/dept_id\","
         + "[\"department_master/dept_code\",\"" + deptcode + "\"]]"
         + "]"
         + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;


      
    }
    public string GetEvidencereport(string Agencyname, string CaseNo, string Referenceno, string from, string to, string Division, string user, string status,string institutecode)
    {
        string res = "{"
         + "\"select\":{\"?stk\":["
         + "\"department_code\","
         + "\"updateddate\","
         + "\"createddate\","
         + "\"agencyname\","
         + "\"inst_code\","
         + "\"agencyreferanceno\","
         + "\"receiptfilepath\","
         + "\"noof_exhibits\","
         + "\"enteredby\","
         + "\"caseno\","
         + "\"evidenceid\","
         + "\"div_code\","
         + "\"status\","
         + "\"notes\""
         + "]},"
         + "\"where\":["

         + "[\"?stk\",\"evidence_acceptancedetails/createddate\",\"?date\"],"
         + "[\"?stk\",\"evidence_acceptancedetails/status\",\"?status\"]";
         //+ "[\"?stk\",\"commodityprice/createdby\",[\"userdetails/userid\",\"" + userid + "\"]],"
       
        if (Agencyname != "")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/agencyname\",\"" + Agencyname + "\"]";
        }
        if (CaseNo != "")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/caseno\",\"" + CaseNo + "\"]";
        }
        if (Referenceno != "")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/agencyreferanceno\",\"" + Referenceno + "\"]";
        }
        if (Division != "")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/department_code\",\"" + Division + "\"]";

        }
        if (user != "-1")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/caseassign_userid\",\"" + user + "\"]";

        }
        if (institutecode != "")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/inst_code\",\"" + institutecode + "\"]";

        }
        if (status != "-1")
        {
            if (status == "Report Submission")
            {
                res += ",[\"?stk\",\"evidence_acceptancedetails/status\",\"Report Submission\"]";
            }
        }

        res += "]";


        if (from != "0" && to != "0")
        {
            res += ",\"filter\": [\"(and (<= " + from + " ?date) (>= " + to + " ?date))\"],\"limit\": 5000";
        }
        if (status != "-1")
        {
            if (status == "Pending for Assign")
            {
                //res += ",[\"?stk\",\"evidence_acceptancedetails/status\",\"Assigned\"]";
                res += ",\"filter\":[\"(or (= ?status \\\"Pending for Assign\\\")"
                + " (strStarts ?status \\\"Assigned\\\"))\"]";
                //res += "{\"filter\":[\" (and(= ?status \\\"Assigned\\\")(= ?status \\\"Pending for Assign\\\"))\"]}";

            }
            //else
            //{
            //    res += ",\"filter\":[\"(or (= ?status \\\"Report Submission\\\")]";

            //   // res += ",[\"?stk\",\"evidence_acceptancedetails/status\",\"Report Submission\"]]";

            //}
        }
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetEvidenceDetails(string Agencyname, string from, string to, string user,string instcode)
    {
        string res = "{"
         + "\"select\":{\"?stk\":["
         + "\"department_code\","
         + "\"updateddate\","
         + "\"createddate\","
         + "\"agencyname\","
         + "\"inst_code\","
         + "\"agencyreferanceno\","
        + "\"receiptfilepath\","
         + "\"enteredby\","
         + "\"caseno\","
         + "\"evidenceid\","
         + "\"noof_exhibits\","
         + "\"div_code\","
         + "\"status\","
         + "\"notes\""
         + "]},"
         + "\"where\":["

         + "[\"?stk\",\"evidence_acceptancedetails/createddate\",\"?date\"],"
         //+ "[\"?stk\",\"commodityprice/createdby\",[\"userdetails/userid\",\"" + userid + "\"]],"
         + "[\"?stk\",\"evidence_acceptancedetails/inst_code\",\""+ instcode + "\"]";
        if (Agencyname != "-1")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/department_code\",\"" + Agencyname + "\"]";
        }
        if (user != "")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/caseassign_userid\",\"" + user + "\"]";
        }
        res += "]";
        if (from != "0" && to != "0")
        {
            res += ",\"filter\": [\"(and (<= " + from + " ?date) (>= " + to + " ?date))\"]";

        }
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetEvidenceData(string InstCode, string caseno, string from, string to, string Division, string user)
    {
        string res = "{"
         + "\"select\":{\"?stk\":["
         + "\"department_code\","
         + "\"updateddate\","
         + "\"createddate\","
         + "\"agencyname\","
         + "\"inst_code\","
         + "\"agencyreferanceno\","
          + "\"caseassign_userid\","
        // + "{\"caseassign_userid\":[\"username\"]},"
         + "\"receiptfilepath\","
         + "\"enteredby\","
         + "\"caseno\","
         + "\"evidenceid\","
         + "\"noof_exhibits\","
         + "\"div_code\","
         + "\"status\","
         + "\"notes\""
         + "]},"
         + "\"where\":["

         + "[\"?stk\",\"evidence_acceptancedetails/createddate\",\"?date\"],"
         //+ "[\"?stk\",\"commodityprice/createdby\",[\"userdetails/userid\",\"" + userid + "\"]],"
         + "[\"?stk\",\"evidence_acceptancedetails/inst_code\",\"" + InstCode + "\"]";
        if (caseno != "")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/caseno\",\"" + caseno + "\"]";
        }
        if (user != "")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/caseassign_userid\",\"" + user + "\"]";
        }
        if (Division != "")
        {
            res += ",[\"?stk\",\"evidence_acceptancedetails/div_code\",\"" + Division + "\"]";
        }
        res += "]";
        if (from != "0" && to != "0")
        {
            res += ",\"filter\": [\"(and (<= " + from + " ?date) (>= " + to + " ?date))\"]";

        }
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetUsers(string userid, string instid, string divid)
    {
        string res = "{"
            + "\"select\":{\"?user\":["
            + "\"userid\","
            + "\"firstname\","
            + "\"username\","
            + "\"email\","
            + "\"lastname\""
            + "]},"
            + "\"where\":[";
        if (userid != "-1")
        {
            res += "["
            + "\"?user\",\"userdetails/userid\",\"?u\""
            + "],";
        }

        res += "["
        + "\"?user\",\"userdetails/inst_id\","
        + "[\"institute_master/inst_code\",\"" + instid + "\"]"
        + "],";
        res += "["
        + "\"?user\",\"userdetails/div_id\","
        + "[\"division_master/div_code\",\"" + divid + "\"]"
        + "],["
            + "\"?user\",\"userdetails/isactive\",\"1\""
            + "]]";
        if (userid != "-1")
        {
            res += ","
        + "\"filter\":[\"(not (= ?u \\\"" + userid + "\\\"))\"]";
        }
        res +=


            "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetAssignedCase(string userid, string div_id)
    {
        string res = "{"
            + "\"select\":{\"?case\":["
            + "{\"userid\":[\"userid\",\"username\"]},"
            + "\"status\","
            + "\"year\","
            + "\"case_id\","
            + "\"notes\""
            + "]},";
        res += "\"where\":[";
        if (userid != "-1")
        {
            res += "[\"?case\",\"case_master/userid\","
            + "[\"userdetails/userid\",\"" + userid + "\"]"
            + "]";
            res += "],";
        }
        res += "["
            + "\"?case\",\"case_master/div_id\",["
            + "\"division_master/div_code\",\"" + div_id + "\""
            + "],["
            + "\"?case\",\"division_master/is_deleted\",false"
            + "]";

        //if (userid != "-1")
        //{
        //    res += ","
        //+ "\"filter\":[\"(not (= ?u \\\"" + userid + "\\\"))\"]";
        //}
        res += "]}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetActiveCase(string div_id)
    {
        string res = "{"
            + "\"select\":{\"?case\":["
            + "{\"userid\":[\"userid\",\"username\"]},"
            + "\"status\","
            + "\"year\","
            + "\"case_id\","
            + "\"notes\""
            + "]},";
        res += "\"where\":[["
        + "\"?case\",\"case_master/status\","
        + "\"Active\""
        + "],["
        + "\"?case\",\"case_master/div_id\",["
        + "\"division_master/div_code\",\"" + div_id + "\""
        + "]"
        + "],["
            + "\"?case\",\"case_master/is_deleted\",false"
            + "]";
        res += "]";
        //if (userid != "-1")
        //{
        //    res += ","
        //+ "\"filter\":[\"(not (= ?u \\\"" + userid + "\\\"))\"]";
        //}
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetAssignedCaseById(string c_id, string u_id, string div_id, string dept_code)
    {
        string res = "{"
            + "\"select\":{\"?case\":["
            + "{\"userid\":[\"userid\",\"username\"]},"
            + "\"status\","
            + "\"year\","
            + "\"case_id\","
            + "\"notes\""
            + "]},";
        if (c_id != "-1" && u_id == "-1")
        {
            res += "\"where\":[["
            + "\"?case\",\"case_master/case_id\",\"" + c_id + "\""
            + "],";
            res += "["
            + "\"?case\",\"case_master/div_id\",["
            + "\"division_master/div_code\",\"" + div_id + "\""
            + "]]" +
            ",["
            + "\"?case\",\"case_master/dept_code\",\"" + dept_code + "\""
            + "],["
            + "\"?case\",\"case_master/is_deleted\",false"
            + "]";
            res += "]";
        }
        else if (c_id == "-1" && u_id != "-1")
        {
            res += "\"where\":[["
            + "\"?case\",\"case_master/userid\",[\"userdetails/userid\",\"" + u_id + "\"]"
            + "],";
            res += "["
            + "\"?case\",\"case_master/div_id\",["
           + "\"division_master/div_code\",\"" + div_id + "\""
            + "]]" +
            ",["
            + "\"?case\",\"case_master/dept_code\",\"" + dept_code + "\""
            + "],["
            + "\"?case\",\"case_master/is_deleted\",false"
            + "]";
            res += "]";
        }
        else if (c_id != "-1" && u_id != "-1")
        {
            res += "\"where\":[["
            + "\"?case\",\"case_master/case_id\",\"" + c_id + "\""
            + "],["
            + "\"?case\",\"case_master/userid\",[\"userdetails/userid\",\"" + u_id + "\"]"
            + "],";
            res += "["
            + "\"?case\",\"case_master/div_id\",["
             + "\"division_master/div_code\",\"" + div_id + "\""
            + "]]" +
            ",["
            + "\"?case\",\"case_master/dept_code\",\"" + dept_code + "\""
            + "],["
            + "\"?case\",\"case_master/is_deleted\",false"
            + "]";
            res += "]";
        }
        else
        {
            res += "\"where\":[";
            res += "["
            + "\"?case\",\"case_master/div_id\",["
            + "\"division_master/div_code\",\"" + div_id + "\""
            + "]]" +
            ",["
            + "\"?case\",\"case_master/dept_code\",\"" + dept_code + "\""
            + "],["
            + "\"?case\",\"case_master/is_deleted\",false"
            + "]]";
        }

        //if (userid != "-1")
        //{
        //    res += ","
        //+ "\"filter\":[\"(not (= ?u \\\"" + userid + "\\\"))\"]";
        //}
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetuserforDepartment(string DeptID)
    {
        string res = "{"
          + "\"select\":{\"?div\":["
          + "\"div_id\","
          + "\"div_name\""
          + ",\"div_code\""
          + ",{\"inst_id\":[\"inst_id\",\"inst_name\"]}"
          + ",{\"dept_id\":[\"dept_id\",\"dept_name\"]}"
          + "]},"
          + "\"where\":[[\"?div\",\"division_master/div_name\",\"?divname\"],"
          + "[\"?div\",\"division_master/is_deleted\",false]";
        if (DeptID != "-1")
        {
            res += ",["
           + "\"?div\",\"division_master/dept_id\",[\"department_master/dept_id\",\"" +
           DeptID + "\"]]";

        }

        res += "],"
         + "\"orderBy\":\"?divname\""
        + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetDivisionforDepartment(string DeptID)
    {
        string res = "{"
          + "\"select\":{\"?div\":["
          + "\"div_id\","
          + "\"div_name\""
          + ",\"div_code\""
          + ",{\"inst_id\":[\"inst_id\",\"inst_name\"]}"
          + ",{\"dept_id\":[\"dept_id\",\"dept_name\"]}"
          + "]},"
          + "\"where\":[[\"?div\",\"division_master/div_name\",\"?divname\"],"
          + "[\"?div\",\"division_master/is_deleted\",false]";
        if (DeptID != "-1")
        {
            res += ",["
           + "\"?div\",\"division_master/dept_id\",[\"department_master/dept_id\",\"" +
           DeptID + "\"]]";

        }

        res += "],"
         + "\"orderBy\":\"?divname\""
        + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetDeptByIdForAdmin(string IntsId)
    {
        string res = "{"
            + "\"select\":{\"?dept\":["
            + "\"dept_id\","
            + "\"dept_code\","
            + "\"dept_name\""
            + "]},"
            + "\"where\":[[\"?dept\",\"department_master/dept_name\",\"?name\"],"
            + "[\"?dept\",\"department_master/inst_id\",[\"institute_master/inst_id\",\"" +
            IntsId + "\"]],["
            + "\"?dept\",\"department_master/is_deleted\",false"
            + "]"
            + "],"
             + "\"filter\":[\"(and(not (= ?name \\\"ALL Department\\\"))(not (= ?name \\\"Other Sample Warden\\\")))\"],"
        + "\"orderBy\":\"?name\""
            + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetDivisionforAdmin(string InstID)
    {
        string res = "{"
          + "\"select\":{\"?div\":["
          + "\"div_id\","
          + "\"div_name\""
          + ",\"div_code\""
          + ",{\"inst_id\":[\"inst_id\",\"inst_name\"]}"
          + ",{\"dept_id\":[\"dept_id\",\"dept_name\"]}"
          + "]},"
          + "\"where\":[[\"?div\",\"division_master/div_name\",\"?divname\"],"
          + "[\"?div\",\"division_master/is_deleted\",false]";
        if (InstID != "-1")
        {
            res += ",["
           + "\"?div\",\"division_master/inst_id\",[\"institute_master/inst_id\",\"" +
           InstID + "\"]]";

        }

        res += "],"
                 + "\"filter\":[\"(and(not (= ?divname \\\"ALL Division\\\"))(not (= ?divname \\\"Other Division Sample Warden\\\")))\"],"

        + "\"orderBy\":\"?divname\""
        + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetUserCaseById(string c_id, string u_id, string div_id)
    {
        string res = "{"
            + "\"select\":{\"?case\":["
            + "{\"userid\":[\"userid\",\"username\"]},"
            + "\"status\","
            + "\"type\","
            + "\"date\","
            + "{\"case_id\":[\"case_id\"]},"
            + "\"notes\""
            + "]},";
        if (c_id != "-1" && u_id == "-1")
        {
            res += "\"where\":[["
            + "\"?case\",\"usercase/case_id\",[\"case_master/case_id\",\"" + c_id + "\"]"
            + "],";
            res += "["
            + "\"?case\",\"usercase/div_id\",["
            + "\"division_master/div_code\",\"" + div_id + "\""
            + "]],["
            + "\"?case\",\"usercase/is_deleted\",false"
            + "]";
            res += "]";
        }
        else if (c_id == "-1" && u_id != "-1")
        {
            res += "\"where\":[["
            + "\"?case\",\"usercase/userid\",[\"userdetails/userid\",\"" + u_id + "\"]"
            + "],";
            res += "["
            + "\"?case\",\"usercase/div_id\",["
            + "\"division_master/div_code\",\"" + div_id + "\""
            + "]],["
            + "\"?case\",\"usercase/is_deleted\",false"
            + "]";
            res += "]";
        }
        else if (c_id != "-1" && u_id != "-1")
        {
            res += "\"where\":[["
            + "\"?case\",\"usercase/case_id\",[\"case_master/case_id\",\"" + c_id + "\"]"
            + "],["
            + "\"?case\",\"usercase/userid\",[\"userdetails/userid\",\"" + u_id + "\"]"
            + "],";
            res += "["
            + "\"?case\",\"usercase/div_id\",["
            + "\"division_master/div_code\",\"" + div_id + "\""
            + "]],["
            + "\"?case\",\"usercase/is_deleted\",false"
            + "]";
            res += "]";
        }
        else
        {
            res += "\"where\":[";
            res += "["
            + "\"?case\",\"usercase/div_id\",["
            + "\"division_master/div_code\",\"" + div_id + "\""
            + "]],["
            + "\"?case\",\"usercase/is_deleted\",false"
            + "]]";
        }

        //if (userid != "-1")
        //{
        //    res += ","
        //+ "\"filter\":[\"(not (= ?u \\\"" + userid + "\\\"))\"]";
        //}
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetInst()
    {
        string res = "{"
            + "\"select\":{\"?ins\":["
            + "\"inst_id\","
            + "\"inst_name\""
            + "]},"
            + "\"where\":[[\"?ins\",\"institute_master/inst_name\",\"?name\"],["
            + "\"?ins\",\"institute_master/is_deleted\",false"
            + "]],"
            + "\"orderBy\":\"?name\""
            + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetDeptById(string IntsId)
    {
        string res = "{"
            + "\"select\":{\"?dept\":["
            + "\"dept_id\","
            + "\"dept_code\","
            + "\"dept_name\""
            + "]},"
            + "\"where\":[[\"?dept\",\"department_master/dept_name\",\"?name\"],"
            + "[\"?dept\",\"department_master/inst_id\",[\"institute_master/inst_id\",\"" +
            IntsId + "\"]],["
            + "\"?dept\",\"department_master/is_deleted\",false"
            + "]"
            + "],"
              + "\"filter\":[\"(and(not (= ?name \\\"ALL Department\\\"))(not (= ?name \\\"Other Sample Warden\\\")))\"],"
            + "\"orderBy\":\"?name\""
            + "}";
 
        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetDeptByIdForSuperadmin()
    {
        string res = "{"
            + "\"select\":{\"?dept\":["
            + "\"dept_id\","
            + "\"dept_code\","
            + "\"dept_name\""
            + "]},"
            + "\"where\":[[\"?dept\",\"department_master/dept_name\",\"?name\"],"
            //+ "[\"?dept\",\"department_master/inst_id\",[\"institute_master/inst_id\",\"" +
            //IntsId + "\"]],["
            + "[\"?dept\",\"department_master/is_deleted\",false"
            + "]"
            + "],"
            + "\"orderBy\":\"?name\""
            + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string GetCaseNoBydeptcode(string Deptcode)
    {
        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"caseno\","
            + "\"department_code\","
            + "\"evidenceid\""
            + "]},"
            + "\"where\":[";

        res += "["
         + "\"?case\",\"evidence_acceptancedetails/evidenceid\",\"?u\""
         + "],";

        res += "["
        + "\"?case\",\"evidence_acceptancedetails/department_code\",\"" + Deptcode + "\""

        + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetCaseNoBydeptcodeforDirector(string Deptcode)
    {
        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"caseno\","
            + "\"department_code\","
            + "\"evidenceid\""
            + "]},"
            + "\"where\":[";

        res += "["
         + "\"?case\",\"evidence_acceptancedetails/evidenceid\",\"?u\""
         + "],";

        res += "["
        + "\"?case\",\"evidence_acceptancedetails/department_code\",\"" + Deptcode + "\""

        + "]";
        res += ",["
        + "\"?case\",\"evidence_acceptancedetails/status\",\"Assigned\""

        + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string GetCaseNo(string evidenceid, string divid)
    {
        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"evidenceid\","
            + "\"caseno\""
            + "]},"

            + "\"where\":[";
        if (evidenceid != "-1")
        {
            res += "["
            + "\"?case\",\"evidence_acceptancedetails/evidenceid\",\"?u\""
            + "],";
        }


        res += "["
        + "\"?case\",\"evidence_acceptancedetails/department_code\",\"" + divid + "\""

        + "],";
        res += "["
      + "\"?case\",\"evidence_acceptancedetails/status\",\"Pending for Assign\""

      + "]]";
        if (evidenceid != "-1")
        {
            res += ","
        + "\"filter\":[\"(not (= ?u \\\"" + evidenceid + "\\\"))\"]";
        }
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;

    }

    public string CheckUsername(string username, string instid, string divid)
    {
        string res = "{"
            + "\"select\":{\"?user\":["
            + "\"userid\","
            + "\"firstname\","
            + "\"username\","
            + "\"email\","
            + "\"mobileno\","
            + "\"designation\","
            + "{\"role_id\":[\"role\"]},"
            + "{\"inst_id\":[\"inst_name\",\"inst_code\",\"inst_id\",\"location\"]},"
            + "{\"dept_id\":[\"dept_name\",\"dept_code\",\"dept_id\"]},"
            + "{\"div_id\":[\"div_name\",\"div_code\"]},"
             + "\"profileimage\","
            + "\"promotionletter\","
            + "\"appointmentletter\","
            + "\"appointmentletterrhash\","
            + "\"promotionletterhash\","
            + "\"lastname\""
            + "]},"
            + "\"where\":[";
        if (username != "-1")
        {
            res += "["
            + "\"?user\",\"userdetails/username\",\"?u\""
            + "],";
        }

        res += "["
        + "\"?user\",\"userdetails/inst_id\","
        + "[\"institute_master/inst_code\",\"" + instid + "\"]"
        + "],";
        res += "["
        + "\"?user\",\"userdetails/div_id\","
        + "[\"division_master/div_code\",\"" + divid + "\"]"
        + "],["
            + "\"?user\",\"userdetails/is_deleted\",false"
            + "]]";
        if (username != "-1")
        {
            res += ","
        + "\"filter\":[\"(= ?u \\\"" + username + "\\\")\"]";
        }
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetHashFromAttachment(string hash)
    {

        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"type\","
            + "\"notes\","
            + "\"filename\","
            + "\"path\","
             + "\"hash\","
            + "\"attach_id\""

            + "]},"
            + "\"where\":[";

        res += "["
        + "\"?case\",\"attachement_master/attach_id\",\"?u\""
        + "],";

        res += "["
        + "\"?case\",\"attachement_master/hash\",\"" + hash + "\""

        + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetCaseuserwise(string caseno, string userid)
    {

        string res = "{"
            + "\"select\":{\"?case\":["

            + "\"caseno\","
            + "\"_id\""

            + "]},"
            + "\"where\":[[\"?case\",\"evidence_acceptancedetails/evidenceid\",\"?name\"]";

        res += ",[\"?case\",\"evidence_acceptancedetails/caseno\",\"" + caseno
                + "\"]";
        //if (deptcode != "")
        //{
        //    res += ",[\"?case\",\"evidence_acceptancedetails/department_code\",\"" + deptcode + "\"]";
        //}
        if (userid != "")
        {
            res += ",[\"?case\",\"evidence_acceptancedetails/caseassign_userid\",\"" + userid + "\"]";
        }
        res += "]}";


        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }



    public string GetCaseNoforcheck(string caseno, string deptcode, string userid)
    {

        string res = "{"
            + "\"select\":{\"?case\":["

            + "\"caseno\""

            + "]},"
            + "\"where\":[[\"?case\",\"evidence_acceptancedetails/evidenceid\",\"?name\"]";

        res += ",[\"?case\",\"evidence_acceptancedetails/caseno\",\"" + caseno
                + "\"]";
        if (deptcode != "")
        {
            res += ",[\"?case\",\"evidence_acceptancedetails/department_code\",\"" + deptcode + "\"]";
        }
        if (userid != "")
        {
            res += ",[\"?case\",\"evidence_acceptancedetails/caseassign_userid\",\"" + userid + "\"]";
        }
        res += "]}";


        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string GetHashFromUserAcceptance(string hash)
    {

        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"agencyreferanceno\","
            + "\"receiptfilepath\","
            + "\"agencyname\","
            + "\"caseno\","
            + "\"noof_exhibits\","
            + "\"hash\","
            + "\"evidenceid\","

            + "\"notes\""
            + "]},"
            + "\"where\":[";

        res += "["
        + "\"?case\",\"evidence_acceptancedetails/evidenceid\",\"?u\""
        + "],";


        res += "["
        + "\"?case\",\"evidence_acceptancedetails/hash\",\"" + hash + "\"";
        res += "],["
      + "\"?case\",\"evidence_acceptancedetails/hash\",\"?hash\""

      + "]";
        res += "],"
         + "\"filter\":[\" (not= ?hash \\\"\\\")\"]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string IsActiveUsers(string userid, string isactive)
    {
        string res = "";

        string qry = "[{";
        qry += "\"_id\":[\"userdetails/userid\",\"" + userid
            + "\"],\"isactive\":\"" + isactive
            + "\"";
        qry += "}]";
        res = sendTransaction(qry, servertranurl);

        return res;
    }

    public string GetUseridwise(string userid)
    {

        string res = "{"
          + "\"select\":{\"?user\":["
          + "\"userid\","
          + "\"firstname\","
          + "\"username\","
          + "\"lastname\","
          + "\"designation\","
          + "\"dept_code\","
          + "\"email\","
          + "\"mobileno\","
          + "\"appointmentletter\","
          + "\"promotionletter\","
          + "\"isactive\","
          + "\"lastname\""
         + ",{\"inst_id\":[\"inst_id\",\"inst_name\"]}"
          + ",{\"dept_id\":[\"dept_id\",\"dept_name\"]}"
           + ",{\"role_id\":[\"role_id\",\"role_name\"]}"
           + ",{\"div_id\":[\"div_id\",\"div_name\"]}"
          + "]},"
            + "\"where\":[";

        res += "["
        + "\"?user\",\"userdetails/userid\",\"?u\""
        + "],";

        res += "["
        + "\"?user\",\"userdetails/userid\",\"" + userid + "\""

        + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }



    public string GetUserAcceptanceDetails(string caseno)
    {


        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"agencyreferanceno\","
            + "\"receiptfilepath\","
            + "\"agencyname\","
            + "\"caseno\","
            + "\"evidenceid\","
            + "\"noof_exhibits\","
            + "\"status\","

            + "\"notes\""
            + "]},"
            + "\"where\":[";

        res += "["
        + "\"?case\",\"evidence_acceptancedetails/evidenceid\",\"?u\""
        + "],";

        res += "["
        + "\"?case\",\"evidence_acceptancedetails/caseno\",\"" + caseno + "\""

        + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetUserAcceptance_detailsforassign(string caseno)
    {


        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"agencyreferanceno\","
            + "\"receiptfilepath\","
            + "\"agencyname\","
            + "\"caseno\","
            + "\"evidenceid\","
            + "\"noof_exhibits\","
            + "\"status\","

            + "\"notes\""
            + "]},"
            + "\"where\":[";

        res += "["
        + "\"?case\",\"evidence_acceptancedetails/evidenceid\",\"?u\""
        + "],";

        res += "["
        + "\"?case\",\"evidence_acceptancedetails/status\",\"Pending for Assign\""
        + "],";

        res += "["
        + "\"?case\",\"evidence_acceptancedetails/caseno\",\"" + caseno + "\""

        + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }





    public string GetDashboardCountForDirector(string inst_id)
    {
        string res = "";

        string qry = "{";

        qry += "\"Departments\":{"
          + "\"select\":\"(count ?dept)\","
          + "\"where\":[[";
        qry += "\"?c\",\"department_master/dept_id\",\"?dept\"";
        qry += "],";
        qry += "["
            + "\"?c\",\"department_master/dept_name\",\"?dept_name\"],";
        qry += "[\"?c\",\"department_master/inst_id\",[\"institute_master/inst_id\",\"" + inst_id + "\"]";
        qry += "],"
          + "[\"?c\",\"department_master/is_deleted\",false]"
          + "],"
          + "\"filter\":[\"(and(not (= ?dept_name \\\"ALL Department\\\"))(not (= ?dept_name \\\"Other Sample Warden\\\")))\"]"

            + "},";


        qry += "\"User\":{"
           + "\"select\":\"(count ?user)\","
           + "\"where\":[[";
        qry += "\"?c\",\"userdetails/userid\",\"?user\"";
        qry += "],";
        qry += "[\"?c\",\"userdetails/inst_id\",[\"institute_master/inst_id\",\"" + inst_id + "\"]],"

          + "[\"?c\",\"userdetails/isactive\",\"1\"]"
            + "]"
              + "}";


        qry += "}";
        res = sendTransaction(qry, servermulqryurl);

        return res;
    }

    public string GetDashboardCountForSuperAdmin()
    {
        string res = "";

        string qry = "{";

        qry += "\"Departments\":{"
          + "\"select\":\"(count ?dept)\","
          + "\"where\":[[";
        qry += "\"?c\",\"department_master/dept_id\",\"?dept\"";
        qry += "],";

        //qry += "[\"?c\",\"department_master/inst_id\",[\"institute_master/inst_id\",\"" + inst_id + "\"]";
        //qry += "],"
        qry += "[\"?c\",\"department_master/is_deleted\",false]"
          + "]"
            + "},";


        qry += "\"User\":{"
           + "\"select\":\"(count ?user)\","
           + "\"where\":[[";
        qry += "\"?c\",\"userdetails/userid\",\"?user\"";
        qry += "],";
        //qry += "[\"?c\",\"userdetails/inst_id\",[\"institute_master/inst_id\",\"" + inst_id + "\"]],"

        qry += "[\"?c\",\"userdetails/isactive\",\"1\"]"
            + "]"
              + "}";


        qry += "}";
        res = sendTransaction(qry, servermulqryurl);

        return res;
    }

    public string GetPDFTrekDetails(string caseno)
    {

        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"caseno\","
            + "\"status\","
            + "\"notes\","
            + "\"caseassignby\","
            + "\"assignto\","
            + "\"notes\","
            + "\"statuschangedby\","
            + "\"createddate\""
            + "]},"
            + "\"where\":[";

        res += "["
        + "\"?case\",\"trackmaster/trackid\",\"?u\""
        + "],";
        res += "["
     + "\"?case\",\"trackmaster/createddate\",\"?date\""
     + "],";
        res += "["
        + "\"?case\",\"trackmaster/caseno\",\"" + caseno + "\""

        + "]],";

        res += "\"orderBy\":\"?date\"";
        //res += "\"from\":\"department_master\"";
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string GetTrackDetails(string caseno)
    {

        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"trackid\","
            + "\"caseno\","
            + "\"status\","
            + "\"notes\","
            + "\"caseassignby\","
            + "\"assignto\","
            + "\"notes\","
            + "\"statuschangedby\","
            + "\"createddate\""
            + "]},"
            + "\"where\":[";

        res += "["
        + "\"?case\",\"trackmaster/trackid\",\"?u\""
        + "],";

        res += "["
        + "\"?case\",\"trackmaster/caseno\",\"" + caseno + "\""

        + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetStatusRemakrs(string caseno, string status)
    {

        string res = "{"
            + "\"select\":{\"?case\":["
            + "\"trackid\","
            + "\"caseno\","
            + "\"status\","
            + "\"notes\","
            + "\"caseassignby\","
            + "\"assignto\","
            + "\"notes\","
            + "\"statuschangedby\","
            + "\"createddate\""
            + "]},"
            + "\"where\":[";

        res += "["
        + "\"?case\",\"trackmaster/trackid\",\"?u\""
        + "],";

        res += "["
        + "\"?case\",\"trackmaster/caseno\",\"" + caseno + "\""

        + "],";
        res += "["
      + "\"?case\",\"trackmaster/status\",\"" + status + "\""

      + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string GetUserDetails(string assignto)
    {
        string res = "{"
          + "\"select\":{\"?user\":["
          + "\"userid\","
          + "\"firstname\","
          + "\"username\","
          + "\"lastname\""
          + "]},"
          + "\"where\":[";


        res += "["
        + "\"?user\",\"userdetails/userid\",\"" + assignto + "\""
        + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetUserDetailsforCaseAssignBY(string caseassignby)
    {
        string res = "{"
         + "\"select\":{\"?user\":["
         + "\"userid\","
         + "\"firstname\","
         + "\"username\","
         + "\"lastname\""
         + "]},"
         + "\"where\":[";


        res += "["
        + "\"?user\",\"userdetails/username\",\"" + caseassignby + "\""
        + "]]";

        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string GetAssignedHD(string userid)
    {
        string res = "{"
            + "\"select\":{\"?hd\":["
            + "{\"userid\":[\"userid\",\"username\"]},"
            + "\"hd_name\","
            + "\"sr_num\","
            + "\"capacity\","
            + "\"brand\","
            + "\"hd_id\""
            + "]},";
        if (userid != "-1")
        {
            res += "\"where\":[["
            + "\"?hd\",\"harddisk_master/userid\","
            + "[\"userdetails/userid\",\"" + userid + "\"]"
            + "],["
            + "\"?hd\",\"harddisk_master/is_deleted\",false"
            + "]";
            res += "]";
        }
        else
        {
            res = "{\"select\":{\"?hd\":["
            + "{\"userid\":[\"userid\",\"username\"]},"
            + "\"hd_name\","
            + "\"sr_num\","
            + "\"capacity\","
            + "\"brand\","
            + "\"hd_id\""
            + "]},";
            res += "\"where\":[["
            + "\"?hd\",\"harddisk_master/is_deleted\",false"
            + "]]";
            //res += "\"from\":\"harddisk_master\"";
        }

        //if (userid != "-1")
        //{
        //    res += ","
        //+ "\"filter\":[\"(not (= ?u \\\"" + userid + "\\\"))\"]";
        //}
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string Getreportpathbycaseno(string caseno)
    {

        string qry = "{";

        qry += "\"select\":{"
            + "\"?path\":["
            + "\"requestid\""
            + ",\"reportpath\""
            + "]"
            + "},";
        qry += "\"where\":[["
            + "\"?path\""
            + ",\"reportrequest/caseno\""
            + ",\"" + caseno + "\""
            + "]]";

        qry += "}";

        string resp = sendTransaction(qry, serverqryurl);

        return resp;
    }
    public string GetReportData(string userid, string year, string caseid)
    {
        string res = "{"
            + "\"select\":{\"?hd\":["
            + "{\"userid\":[\"userid\",\"username\"]},"
            + "\"case_id\","
            + "\"year\","
            + "\"notes\""
            + "]},";
        if (userid != "-1" || year != "" || caseid != "")
        {
            res += "\"where\":[";
            if (userid != "-1")
            {
                res += "[\"?hd\",\"case_master/userid\","
                + "[\"userdetails/userid\",\"" + userid + "\"]],";

            }
            if (caseid != "")
            {
                res += "[\"?hd\",\"case_master/case_id\","
                + "\"" + caseid.Replace("\\", "\\\\") + "\"],";
            }
            if (year != "")
            {
                res += "[\"?hd\",\"case_master/year\","
                + "" + year + "],";
            }
            res = res.Substring(0, res.Length - 1);
            res += ",["
            + "\"?hd\",\"case_master/is_deleted\",false"
            + "]]";
        }
        else
        {
            res = "{\"select\":{\"?case\":["
               + "{\"userid\":[\"userid\",\"username\"]},"
            + "\"case_id\","
            + "\"year\","
            + "\"notes\""
                + "]},";
            res += "\"where\":[["
            + "\"?case\",\"case_master/is_deleted\",false"
            + "]]";
            //res += "\"from\":\"case_master\"";
        }
        //if (userid != "-1")
        //{
        //    res += ","
        //+ "\"filter\":[\"(not (= ?u \\\"" + userid + "\\\"))\"]";
        //}
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetCasewiseAttachment(string caseid)
    {
        string res = "{"
            + "\"select\":{\"?hd\":["
            //+ "{\"hd_id\":[\"hd_id\",\"hd_name\"]},"
            + "\"type\","
            + "\"notes\","
            + "\"path\","
            + "\"hash\","
            + "\"filename\","
            + "\"attach_id\","
            + "\"createddate\","
            + "{\"case_id\":[\"case_id\"]}"

        + "]},";
        //if (userid != "-1")
        //{
        res += "\"where\":[["
        + "\"?hd\",\"attachement_master/case_id\","
        + "[\"case_master/case_id\",\"" + caseid + "\"]"
        + "]";
        res += "]";
        //}
        //else
        //{
        //    res = "{\"select\":["
        //    + "{\"userid\":[\"userid\",\"username\"]},"
        //    + "\"hd_name\","
        //    + "\"sr_num\","
        //    + "\"capacity\","
        //    + "\"brand\","
        //    + "\"hd_id\""
        //    + "],";
        //    res += "\"from\":\"harddisk_master\"";
        //}

        //if (userid != "-1")
        //{
        //    res += ","
        //+ "\"filter\":[\"(not (= ?u \\\"" + userid + "\\\"))\"]";
        //}
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetCasewiseReqDoc(string caseid)
    {
        string res = "{"
            + "\"select\":{\"?hd\":["
            + "\"doc_type\","
            + "\"path\","
            + "\"hash\","
            + "\"filename\","
            + "\"createddate\""
            + "]},";
        //if (userid != "-1")
        //{
        res += "\"where\":[["
        + "\"?hd\",\"required_document/case_id\","
        + "[\"case_master/case_id\",\"" + caseid + "\"]"
        + "],["
            + "\"?hd\",\"required_document/is_deleted\",false"
            + "]";
        res += "]";
        //}
        //else
        //{
        //    res = "{\"select\":["
        //    + "{\"userid\":[\"userid\",\"username\"]},"
        //    + "\"hd_name\","
        //    + "\"sr_num\","
        //    + "\"capacity\","
        //    + "\"brand\","
        //    + "\"hd_id\""
        //    + "],";
        //    res += "\"from\":\"harddisk_master\"";
        //}

        //if (userid != "-1")
        //{
        //    res += ","
        //+ "\"filter\":[\"(not (= ?u \\\"" + userid + "\\\"))\"]";
        //}
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    #endregion

    #region insert
    public string InsertHDDetails(string alias, string sr, string cap, string brand, string userid)
    {
        string res = "[{\"_id\":\"harddisk_master\","
            //{\"?case\":[\"case_no\"]},"
            + "\"hd_id\":\"" + Guid.NewGuid() + "\","
            + "\"hd_name\":\"" + alias + "\","
            + "\"sr_num\":\"" + sr + "\","
            + "\"capacity\":\"" + cap + "\","
            + "\"brand\":\"" + brand + "\","
            + "\"is_deleted\":false,"
            + "\"userid\":[\"userdetails/userid\",\"" + userid + "\"]"
            + "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string UpdateHD(string id, string alias, string sr, string cap, string brand, string userid)
    {
        string res = "[{\"_id\":[\"harddisk_master/hd_id\",\"" + id
                + "\"],\"hd_name\":\"" + alias
                + "\",\"sr_num\":\"" + sr
                + "\",\"capacity\":\"" + cap
                + "\",\"brand\":\"" + brand
                + "\",\"userid\":[\"userdetails/userid\",\"" + userid
                + "\"]}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string DeleteHD(string id)
    {
        string res = "[{\"_id\":[\"harddisk_master/hd_id\",\"" + id
                + "\"],\"is_deleted\":true}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string InsertCaseAssign(string caseid, string refby, string userid, string notes,
        string div_id, string dept_code)
    {

        string res = "[{\"_id\":\"case_master\","
          + "\"status\":\"Assigned\","
           + "\"year\":" + DateTime.Now.Year + ","
           + "\"case_id\":\"" + caseid + "\","
           + "\"notes\":\"" + notes + "\","
           + "\"ref_by\":\"" + refby + "\","
           + "\"dept_code\":\"" + dept_code + "\","
           + "\"is_deleted\":false,"
           + "\"userid\":[\"userdetails/userid\",\"" + userid + "\"],"
           + "\"div_id\":[\"division_master/div_code\",\"" + div_id + "\"]"
           + "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string Insertreportrequest(string id, string caseno, string remarksbyofficer, string requestedby, string div_code, string inst_code, string fileupload)
    {
        string res = "[{\"_id\":\"reportrequest\","
            + "\"requestid\":\"" + id + "\","
            + "\"caseno\":\"" + caseno + "\","
            + "\"remarksbyofficer\":\"" + remarksbyofficer + "\","
            + "\"requestedby\":\"" + requestedby + "\","
            + "\"status\":\"\","
            + "\"approvedby\":\"\","
            + "\"hodstatus\":\"\","
            + "\"hodremarks\":\"\","
            + "\"div_code\":\"" + div_code + "\","
            + "\"inst_code\":\"" + inst_code + "\","
            + "\"fileupload\":\"" + fileupload + "\","
            + "\"requestedon\":" + ConvertToTimestamp(DateTime.Now) + ","
            + "\"officerstatus\":\"Requested\","
            + "\"createddate\":" + ConvertToTimestamp(DateTime.Now) + ","
            + "\"updateddate\":" + ConvertToTimestamp(DateTime.Now) +
           "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }
    public string InsertCaseAssignPsy(string caseno, string caseid, string firno, string notes,
        string div_id)
    {
        string res = "[{\"_id\":\"case_master\","
            //{\"?case\":[\"case_no\"]},"
            + "\"status\":\"Active\","
            + "\"year\":" + DateTime.Now.Year + ","
            + "\"case_id\":\"" + caseid + "\","
            + "\"notes\":\"" + notes + "\","
            + "\"No\":" + caseno + ","
            + "\"fir_no\":\"" + firno + "\","
            + "\"is_deleted\":false,"
            + "\"div_id\":[\"division_master/div_code\",\"" + div_id + "\"]"
            + "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string InsertUserCase(string caseid, string userid, string notes,
        string div_id, string type, string date)
    {
        string res = "[{\"_id\":\"usercase\","
            //{\"?case\":[\"case_no\"]},"
            + "\"status\":\"Assigned\","
            + "\"notes\":\"" + notes + "\","
            + "\"type\":\"" + type + "\","
            + "\"createddate\":" + ConvertToTimestamp(DateTime.Now) + ","
            + "\"date\":" + ConvertToTimestamp(Convert.ToDateTime(date)) + ","
            + "\"is_deleted\":false,"
            + "\"userid\":[\"userdetails/userid\",\"" + userid + "\"],"
            + "\"case_id\":[\"case_master/case_id\",\"" + caseid + "\"],"
            + "\"div_id\":[\"division_master/div_code\",\"" + div_id + "\"]"
            + "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }


    public string UpdateStatus(string caseid, string status)
    {
        string res = "[{\"_id\":[\"usercase/case_id\",[\"case_master/case_id\",\""
            + caseid + "\"]],"
            //{\"?case\":[\"case_no\"]},"
            + "\"status\":\"" + status + "\""
            + "},{"
            + "\"_id\":[\"case_master/case_id\",\""
            + caseid + "\"],"
            + "\"status\":\"" + status + "\""
            + "}"
            + "]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string Updatereportpath(string id, string reportpath)
    {
        string res = "[{\"_id\":[\"reportrequest/requestid\",\"" + id
                + "\"],\"reportpath\":\"" + reportpath
                + "\",\"status\":\"Yes\""
                 + ",\"filedownloadedon\":" + ConvertToTimestamp(DateTime.Now)
                 + ",\"updateddate\":" + ConvertToTimestamp(DateTime.Now)
                + "}]";



        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string InsertInst(string id, string instname, string instcode, string instloc)
    {
        string res = "[{\"_id\":\"institute_master\","
                + "\"inst_id\":\"" + id
                + "\",\"inst_name\":\"" + instname
                + "\",\"inst_code\":\"" + instcode
                + "\",\"is_deleted\":false"
                + ",\"location\":\"" + instloc
                + "\"}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string InsertDept(string id, string deptname, string deptcode, string instid)
    {
        string res = "[{\"_id\":\"department_master\","
                + "\"dept_id\":\"" + id
                + "\",\"dept_name\":\"" + deptname
                + "\",\"dept_code\":\"" + deptcode
                + "\",\"is_deleted\":false"
                + ",\"inst_id\":[\"institute_master/inst_id\",\"" + instid
                + "\"]}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string UpdateDept(string id, string deptname, string deptcode, string instid)
    {
        string res = "[{\"_id\":[\"department_master/dept_id\",\"" + id
                + "\"],\"dept_name\":\"" + deptname
                + "\",\"dept_code\":\"" + deptcode
                + "\",\"inst_id\":[\"institute_master/inst_id\",\"" + instid
                + "\"]}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string DeleteDept(string id)
    {
        string res = "[{\"_id\":[\"department_master/dept_id\",\"" + id
               + "\"],\"_action\":\"delete\"}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string UpdateInst(string id, string instname, string instcode, string location)
    {
        string res = "[{\"_id\":[\"institute_master/inst_id\",\"" + id
                + "\"],\"inst_name\":\"" + instname
                + "\",\"inst_code\":\"" + instcode
                + "\",\"location\":\"" + location
                + "\"}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string DeleteInst(string id)
    {
        string res = "[{\"_id\":[\"institute_master/inst_id\",\"" + id
                  + "\"],\"_action\":\"delete\"}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string InsertUserAcceptance(string evidenceid, string caseno, string receiptfilepath,
        string agencyreferanceno, string agencyname, string notes, string status,
        string department_code, string instcode, string divcode, string enteredby, string hash,
        string NoOfExhibits, string AssignCaseUserID)
    {
        string resUser = "[{\"_id\":\"evidence_acceptancedetails\","
          + "\"evidenceid\":\"" + evidenceid + "\","
           + "\"caseno\":\"" + caseno + "\","
           + "\"receiptfilepath\":\"" + receiptfilepath + "\","
           + "\"agencyreferanceno\":\"" + agencyreferanceno + "\","
           + "\"agencyname\":\"" + agencyname + "\","
           + "\"notes\":\"" + notes + "\","
           + "\"status\":\"" + status + "\","
           + "\"hash\":\"" + hash + "\","
           + "\"department_code\":\"" + department_code + "\","
           + "\"inst_code\":\"" + instcode + "\","
           + "\"div_code\":\"" + divcode + "\","
           + "\"noof_exhibits\":\"" + NoOfExhibits + "\","
           + "\"caseassign_userid\":\"" + AssignCaseUserID + "\","
           + "\"enteredby\":\"" + enteredby + "\","
           + "\"createddate\":" + ConvertToTimestamp(DateTime.Now) + ","
           + "\"updateddate\":" + ConvertToTimestamp(DateTime.Now) +
                "}]";


       string respUser = sendTransaction(resUser, servertranurl);

        return respUser;

    }

    public string InsertTrack(string caseno, string status, string caseassignby, string assignto, string notestrack, string statuschangedby)
    {

        //string resTrack = "[{\"_id\":\"trackmaster\","
        //       + "\"trackid\":\"" + Guid.NewGuid()
        //       + "\",\"caseno\":\"" + caseno
        //       + "\",\"status\":\"" + status
        //       + "\",\"caseassignby\":\"" + caseassignby
        //       + "\",\"assignto\":\"" + assignto
        //       + "\",\"notes\":\"" + notestrack

        //+ "\",\"createddate\":\"" + ConvertToTimestamp(
        //          Convert.ToDateTime(createddate))
        //      + "\",\"updateddate\":\"" + ConvertToTimestamp(
        //          Convert.ToDateTime(updateddate))


        string resTrack = "[{\"_id\":\"trackmaster\","
           + "\"trackid\":\"" + Guid.NewGuid() + "\","
            + "\"caseno\":\"" + caseno + "\","
            + "\"status\":\"" + status + "\","
            + "\"caseassignby\":\"" + caseassignby + "\","
            + "\"assignto\":\"" + assignto + "\","
            + "\"notes\":\"" + notestrack + "\","
            + "\"statuschangedby\":\"" + statuschangedby + "\","
           + "\"createddate\":" + ConvertToTimestamp(DateTime.Now) +
                 "}]";


        string respTrack = sendTransaction(resTrack, servertranurl);

        return respTrack;


    }


    public string UpdateTrack(string TrackID, string notestrack)
    {
        string res = "[{\"_id\":[\"trackmaster/trackid\",\"" + TrackID
              + "\"],\"notes\":\"" + notestrack
              + "\"}]";

        string respTrack = sendTransaction(res, servertranurl);

        return respTrack;


    }

    public string InsertDiv(string id, string divname, string divcode, string instid, string deptid)
    {
        string res = "[{\"_id\":\"division_master\","
                + "\"div_id\":\"" + id
                + "\",\"div_name\":\"" + divname
                + "\",\"div_code\":\"" + divcode
                + "\",\"is_deleted\":false"
                + ",\"inst_id\":[\"institute_master/inst_id\",\"" + instid
                + "\"],\"dept_id\":[\"department_master/dept_id\",\"" + deptid
                + "\"]}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string UpdateDiv(string id, string divname, string divcode, string instid, string deptid)
    {
        string res = "[{\"_id\":[\"division_master/div_id\",\"" + id
                + "\"],\"div_name\":\"" + divname
                + "\",\"div_code\":\"" + divcode
                + "\",\"inst_id\":[\"institute_master/inst_id\",\"" + instid
                + "\"],\"dept_id\":[\"department_master/dept_id\",\"" + deptid
                + "\"]}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string UpdateEvidencefile(string evidenceid, string agencyname, string file, string referenceno,
        string remarks, string NoOfExhibits)
    {


        string res = "[{\"_id\":[\"evidence_acceptancedetails/evidenceid\",\"" + evidenceid
        + "\"],\"agencyname\":\"" + agencyname;

        if (file != "")
        {
            res += "\",\"receiptfilepath\":\"" + file;
        }
        //if (referenceno == "")
        //{
        res += "\",\"agencyreferanceno\":\"" + referenceno;
        //}
        //if (remarks == "")
        //{
        res += "\",\"notes\":\"" + remarks
             + "\",\"noof_exhibits\":\"" + NoOfExhibits;
        //}


        res += "\",\"updateddate\":" + ConvertToTimestamp(DateTime.Now)

        + "}]";


        string resp = sendTransaction(res, servertranurl);

        return resp;
    }


    public string UpdateUserAcceptanceStatus(string caseno, string status, string notes, string userid)
    {
        string res = "[{\"_id\":[\"evidence_acceptancedetails/caseno\",\"" + caseno
     + "\"],\"status\":\"" + status;


        if (status == "Mismatch Found")
        {
            res += "\",\"notes\":\"" + notes;
        }
        if (userid != "")
        {
            res += "\",\"caseassign_userid\":\"" + userid;
        }
        res += "\",\"updateddate\":" + ConvertToTimestamp(DateTime.Now)

        + "}]";


        string resp = sendTransaction(res, servertranurl);

        return resp;
    }


    public string UpdateUserAcceptanceStatus_1(string id, string status, string notes, string userid)
    {
        string res = "[{\"_id\":" + id
             + ",\"status\":\"" + status;
   


        if (status == "Mismatch Found")
        {
            res += "\",\"notes\":\"" + notes;
        }
        if (userid != "")
        {
            res += "\",\"caseassign_userid\":\"" + userid;
        }
        res += "\",\"updateddate\":" + ConvertToTimestamp(DateTime.Now)

        + "}]";


        string resp = sendTransaction(res, servertranurl);

        return resp;
    }



    public string DeleteDiv(string id)
    {
        string res = "[{\"_id\":[\"division_master/div_id\",\"" + id
                + "\"],\"_action\":\"delete\"}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }


    public string InsertAttachment(string hd_id, string case_id, string notes, string type,
        string hash, string path, string filename, string date, string uploaded)
    {
        string res = "[{\"_id\":\"attachement_master\","
                + "\"attach_id\":\"" + Guid.NewGuid()
                + "\",\"notes\":\"" + notes
                + "\",\"type\":\"" + type
                + "\",\"hash\":\"" + hash
                + "\",\"path\":\"" + path
                + "\",\"filename\":\"" + filename
                + "\",\"uploaded\":\"" + uploaded
                + "\",\"createddate\":" + date
                + ",\"is_deleted\":false,";
        if (hd_id != "")
            res += "\"hd_id\":[\"harddisk_master/hd_id\",\"" + hd_id
                    + "\"],";
        res += "\"case_id\":[\"case_master/case_id\",\"" + case_id
        + "\"]}]";
        
        string resp = sendTransaction(res, servertranurl);

        return resp;
    }


    public string InsertViewRequest(string id, string case_details, string department, string designation,
        string name, string email, string mno, string notes)
    {
        string res = "[{\"_id\":\"view_request\","
                + "\"name\":\"" + name
                + "\",\"department\":\"" + department
                + "\",\"designation\":\"" + designation
                + "\",\"req_id\":\"" + id
                + "\",\"expiredate\":" + ConvertToTimestamp(DateTime.Now.AddYears(5))
                + ",\"status\":\"P\",\"mob_no\":\"" + mno
                + "\",\"mail_id\":\"" + email
                + "\",\"notes\":\"" + notes
                + "\",\"case_details\":\"" + case_details
                + "\"}]";
        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string InserReqDoc(string createddate, string case_id, string filename, string path,
        string doc_type)
    {
        string res = "[{\"_id\":\"required_document\","
                + "\"is_deleted\":false,\"createddate\":" + ConvertToTimestamp(
                    Convert.ToDateTime(createddate))
                + ",\"filename\":\"" + filename
                + "\",\"path\":\"" + path
                + "\",\"doc_type\":\"" + doc_type
                + "\",\"case_id\":[\"case_master/case_id\",\"" + case_id
                + "\"]}]";


        string resp = sendTransaction(res, servertranurl);

        return resp;
    }
    #endregion

    public string UpdateRequestStatus(string id, string status, string note)
    {
        string res = "";
        string qry = "[{\"_id\":[\"view_request/req_id\",\"" + id
               + "\"],\"status\":\"" + status
               + "\",\"response\":\"" + note
               + "\"}]";

        res = sendTransaction(qry, servertranurl);

        return res;
    }

    public string GetRequeststatuswise(string deptcode, string status, string instcode)
    {
        string res = "{"
          + "\"select\":{\"?report\":["
           + "\"requestid\","
            + "\"caseno\","
            + "\"remarksbyofficer\","
            + "\"requestedon\","
            + "\"hodstatus\","
            + "\"requestedby\","
            + "\"reportpath\","
            + "\"createddate\","
            + "\"fileupload\","
            + "\"officerstatus\""
          + "]},"
          + "\"where\":[[\"?report\",\"reportrequest/caseno\",\"?name\"]";
        if (deptcode != "")
            res += ",[\"?report\",\"reportrequest/div_code\",\"" + deptcode
                    + "\"]";
        res += ",[\"?report\",\"reportrequest/hodstatus\",\"" + status
                   + "\"],";
        res += "[\"?report\",\"reportrequest/inst_code\",\"" + instcode
        + "\"]]}";



        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetReportrequestdetails(string deptcode, string instcode)
    {
        string res = "{"
          + "\"select\":{\"?report\":["
           + "\"requestid\","
            + "\"caseno\","
            + "\"remarksbyofficer\","
            + "\"requestedby\","
            + "\"requestedon\","
            + "\"hodstatus\","
            + "\"hodremarks\","
            + "\"requestedby\","
            + "\"fileupload\","
            + "\"reportpath\","
            + "\"createddate\","
            + "\"officerstatus\""
          + "]},"
          + "\"where\":[[\"?report\",\"reportrequest/caseno\",\"?name\"]";
        if (deptcode != "")
            res += ",[\"?report\",\"reportrequest/div_code\",\"" + deptcode
                    + "\"],";
        res += "[\"?report\",\"reportrequest/inst_code\",\"" + instcode
        + "\"]]}";



        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetReportRequestDetail(string deptcode, string instcode, string username)
    {
        string res = "{"
          + "\"select\":{\"?report\":["
           + "\"requestid\","
            + "\"caseno\","
            + "\"remarksbyofficer\","
            + "\"requestedby\","
            + "\"requestedon\","
            + "\"hodstatus\","
            + "\"hodremarks\","
            + "\"requestedby\","
            + "\"reportpath\","
            + "\"createddate\","
            + "\"officerstatus\""
          + "]},"
          + "\"where\":[[\"?report\",\"reportrequest/caseno\",\"?name\"]";
        if (username != "")
        {
            res += ",[\"?report\",\"reportrequest/requestedby\",\"" + username
                 + "\"]";
        }
        if (deptcode != "")
            res += ",[\"?report\",\"reportrequest/div_code\",\"" + deptcode
                    + "\"],";
        res += "[\"?report\",\"reportrequest/inst_code\",\"" + instcode
        + "\"]]}";



        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string GetViewReq(string status)
    {
        string res = "{\"select\":{\"?req\":["
            + "\"name\","
            + "\"department\","
            + "\"case_details\","
            + "\"req_id\","
            + "\"notes\","
            + "\"mail_id\","
            + "\"mob_no\","
            + "\"status\","
            + "\"designation\""
            + "]},";
        res += "\"where\":[[\"?req\",\"view_request/case_details\",\"?name\"]";
        if (status != "")
        {
            res += ",["
                + "\"?req\",\"view_request/status\",\"" + status + "\""
                + "]";
        }
        res += "],"
            + "\"orderBy\":\"?name\"";
        //res += "\"from\":\"department_master\"";
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetDept()
    {
        string res = "{\"select\":{\"?dep\":["
            + "\"dept_id\","
            + "\"dept_name\","
            + "\"dept_code\""
            + "]},";
        res += "\"where\":[[\"?dep\",\"department_master/is_deleted\",false],"
            + "[\"?dep\",\"department_master/dept_name\",\"?name\"]"
            + "],"
          + "\"orderBy\":\"?name\"";
        //res += "\"from\":\"department_master\"";
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetAllEviAcceptanceDetails(string caseno)
    {
        string res = "{\"select\":{\"?case\":["
            + "\"evidenceid\","
            + "\"_id\","
            + "\"caseno\""

            + "]},";
        res += "\"where\":["
            + "[\"?case\",\"evidence_acceptancedetails/caseno\",\"?name\"]"
            + "]";

        res += ","
    + "\"filter\":[\"(not (= ?name \\\"" + caseno + "\\\"))\"]";

        //res += "\"from\":\"department_master\"";
        res += "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }




    public string Gettimelinedetails(string caseno)
    {
        string res = "";

        string qry = "{";
        qry += "\"evidence_acceptancedetails\":{"
             + "\"select\":{\"?case\":["
             + "\"evidenceid\","
             + "\"caseno\","
             + "\"agencyreferanceno\","
             + "\"agencyname\","
             + "\"evidenceid\","
             + "\"notes\","
             + "\"status\","
             + "\"notes\","
             + "\"department_code\","
             + "\"noof_exhibits\","
             + "\"createddate\","
             + "\"updateddate\","
             + "\"div_code\","
             + "\"inst_code\","
             + "\"enteredby\""
             + "]},"
             + "\"where\":[";


        qry += "["
        + "\"?case\",\"evidence_acceptancedetails/caseno\",\"" + caseno + "\""

        + "]]"

        + "},";


        qry += "\"trackmaster\":{"
              + "\"select\":{\"?case\":["
              + "\"trackid\","
              + "\"caseno\","
              + "\"status\","
              + "\"caseassignby\","
              + "\"createddate\","
              + "\"notes\""

              + "]},"
              + "\"where\":[";



        qry += "["
        + "\"?case\",\"trackmaster/caseno\",\"" + caseno + "\""

        + "]]"

        + "}";

        qry += "}";
        res = sendTransaction(qry, servermulqryurl);

        return res;



    }


    public string Getdbdata(string caseno, string FromDate, string ToDate)
    {
        string res = "";

        string qry = "{";
        qry += "\"evidence_acceptancedetails\":{"
             + "\"select\":{\"?case\":["
             + "\"evidenceid\","
             + "\"caseno\","
             + "\"agencyreferanceno\","
             + "\"agencyname\","
             + "\"evidenceid\","
             + "\"notes\","
             + "\"status\","
             + "\"notes\","
             + "\"department_code\","
             + "\"noof_exhibits\","
             + "\"createddate\","
             + "\"updateddate\","
             + "\"div_code\","
             + "\"inst_code\","
             + "\"enteredby\""
             + "]},"
             + "\"where\":[";



        if (caseno != "")
        {
            qry += "[\"?case\",\"evidence_acceptancedetails/caseno\",\"" + caseno
                 + "\"],";
        }

        //qry += "["
        //+ "\"?case\",\"evidence_acceptancedetails/caseno\",\"" + caseno + "\""

        //+ "],["

        qry += "["
       + "\"?case\",\"evidence_acceptancedetails/createddate\",\"?date\""
       + "]]";

        if (FromDate != "0" && ToDate != "0")
        {
            qry += ",\"filter\": [\"(and (<= " + FromDate + " ?date) (>= " + ToDate + " ?date))\"]";

        }


        qry += "},";


        qry += "\"trackmaster\":{"
              + "\"select\":{\"?track\":["
              + "\"trackid\","
              + "\"caseno\","
              + "\"status\","
              + "\"caseassignby\","
              + "\"createddate\","
              + "\"notes\""

              + "]},"
              + "\"where\":[";


        if (caseno != "")
        {
            qry += "[\"?track\",\"evidence_acceptancedetails/caseno\",\"" + caseno
                 + "\"]]";
        }
        if (FromDate != "0" && ToDate != "0")
        {
            qry += ",\"filter\": [\"(and (<= " + FromDate + " ?date) (>= " + ToDate + " ?date))\"]";

        }
        //qry += "["
        //+ "\"?case\",\"trackmaster/caseno\",\"" + caseno + "\""

        //+ "]]"

        qry += "},";


        qry += "\"case_master\":{"
            + "\"select\":{\"?case\":["
            + "\"case_id\","
            + "\"ref_by\""


            + "]},"
            + "\"where\":[";



        if (caseno != "")
        {
            qry += "[\"?case\",\"evidence_acceptancedetails/caseno\",\"" + caseno
                 + "\"]]";
        }
        if (FromDate != "0" && ToDate != "0")
        {
            qry += ",\"filter\": [\"(and (<= " + FromDate + " ?date) (>= " + ToDate + " ?date))\"]";

        }
        //qry += "["
        //+ "\"?case\",\"case_master/case_id\",\"" + caseno + "\""

        //+ "]]"

        qry += "},";

        qry += "\"attachement_master\":{"
           + "\"select\":{\"?attach\":["
           + "\"case_id\","
           + "\"filename\","
           + "\"uploaded\""

           + "]},"
           + "\"where\":[";



        if (caseno != "")
        {
            qry += "["

        + "\"?attach\",\"attachement_master/case_id\",[\"case_master/case_id\",\"" + caseno + "\"]"

        //  + "\"?case\",\"attachement_master/case_id\",\"" + caseno + "\""

        + "]]";

        }

        if (FromDate != "0" && ToDate != "0")
        {
            qry += ",\"filter\": [\"(and (<= " + FromDate + " ?date) (>= " + ToDate + " ?date))\"]";

        }
        qry += "}}";
        res = sendTransaction(qry, servermulqryurl);

        return res;



    }

    //public string GetDiv(string div_id)
    //{
    //    string res = "{\"select\":{\"?div\":["
    //        + "\"div_id\","
    //        + "\"div_name\","
    //        + "\"div_code\""
    //        + "]},";
    //    res += "\"where\":[[\"?ins\",\"division_master/is_deleted\",false]]";
    //    //res += "\"from\":\"division_master\"";
    //    res += "}";

    //    string resp = sendTransaction(res, serverqryurl);

    //    return resp;
    //}
    #region ,hemaxi
    public string CreateUser(string userid, string firstname, string lastname, string username,
    string designation, string password, string institute, string department, string division,
    string roles, string Appoitmentletter, string Promotionletter, string AppoitmentLetterHash,
    string PromotionLetterHash, string Email, string profileimage, string mobileno)
    {

        string qry = "[{";

        if (userid == "")
        {
            qry += "\"_id\":\"userdetails\","
                  + "\"userid\":\"" + Guid.NewGuid() + "\","
                  + "\"firstname\":\"" + firstname + "\","
                  + "\"lastname\":\"" + lastname + "\","
                  + "\"username\":\"" + username + "\","
                  + "\"designation\":\"" + designation + "\","
                  + "\"email\":\"" + Email + "\","
                  + "\"password\":\"" + password + "\","
                  + "\"profileimage\":\"" + profileimage + "\","
                   + "\"mobileno\":\"" + mobileno + "\","
                  + "\"appointmentletter\":\"" + Appoitmentletter + "\","
                  + "\"promotionletter\":\"" + Promotionletter + "\","
                  + "\"appointmentletterrhash\":\"" + AppoitmentLetterHash + "\","
                  + "\"promotionletterhash\":\"" + PromotionLetterHash + "\","
                  + "\"createddate\":" + ConvertToTimestamp(DateTime.Now) + ","
                  + "\"updateddate\":" + ConvertToTimestamp(DateTime.Now) + ","
                  + "\"isactive\":\"1\","
                  + "\"is_deleted\":false,"
                  + "\"inst_id\":[\"institute_master/inst_id\",\"" + institute + "\"],"
                  + "\"dept_id\":[\"department_master/dept_id\",\"" + department + "\"],"
                  + "\"div_id\":[\"division_master/div_id\",\"" + division + "\"],"
                  + "\"role_id\":[\"role/role_id\",\"" + roles + "\"]";

            qry += "}]";

        }
        string resp = sendTransaction(qry, servertranurl);

        return resp;
    }

    public string UpdateUser(string id, string firstname, string lastname, string username,
    string designation, string institute, string department, string division,
    string roles, string Appoitmentletter, string Promotionletter, string AppoitmentLetterHash,
    string PromotionLetterHash, string Email, string profileimage, string mobileno)
    {
        string res = "[{\"_id\":[\"userdetails/userid\",\"" + id + "\"],"
               + "\"firstname\":\"" + firstname + "\","
                  + "\"lastname\":\"" + lastname + "\","
                  + "\"username\":\"" + username + "\","
                  + "\"designation\":\"" + designation + "\","
                  + "\"email\":\"" + Email + "\",";
        if (profileimage != "")
        {
            res += "\"profileimage\":\"" + profileimage + "\",";
        }

        res += "\"mobileno\":\"" + mobileno + "\",";
        if (Appoitmentletter != "")
        {
            res += "\"appointmentletter\":\"" + Appoitmentletter + "\",";
        }

        if (Promotionletter != "")
        {
            res += "\"promotionletter\":\"" + Promotionletter + "\",";
        }

        if (AppoitmentLetterHash != "")
        {
            res += "\"appointmentletterrhash\":\"" + AppoitmentLetterHash + "\",";
        }

        if (PromotionLetterHash != "")
        {
            res += "\"promotionletterhash\":\"" + PromotionLetterHash + "\",";
        }

        res += "\"inst_id\":[\"institute_master/inst_id\",\"" + institute + "\"],"
                  + "\"dept_id\":[\"department_master/dept_id\",\"" + department + "\"],"
                  + "\"div_id\":[\"division_master/div_id\",\"" + division + "\"],"
                  + "\"role_id\":[\"role/role_id\",\"" + roles + "\"],"
                  + "\"updateddate\":" + ConvertToTimestamp(DateTime.Now) + ""
                  + "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }


    public string ResetPassword(string id, string password)
    {
        string res = "[{\"_id\":[\"userdetails/userid\",\"" + id + "\"],"
               + "\"password\":\"" + password + "\""
               + "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string AddResetPasswordData(string userid, string password)
    {

        string qry = "[{";

        if (userid != "")
        {
            qry += "\"_id\":[\"userdetails/userid\",\"" + userid + "\"],"
             + "\"password\":\"" + password + "\"";

            qry += "}]";

        }
        string resp = sendTransaction(qry, servertranurl);

        return resp;
    }

    public string GetHashFromCreateUserForAppoi(string AppoitmentLetterHash)
    {

        string res = "{"
            + "\"select\":{\"?user\":["
            + "\"firstname\","
            + "\"lastname\","
            + "\"username\","
            + "\"designation\","
             + "\"appointmentletter\","
            + "\"promotionletter\","
            + "\"appointmentletterrhash\","
            + "\"promotionletterhash\""
            + "]},"
            + "\"where\":[";

        res += "["
        + "\"?user\",\"userdetails/appointmentletterrhash\",\"" + AppoitmentLetterHash + "\"]";

        res += "]}";


        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetGraphData(string Inst_Code, string Dept_Code, string User)
    {
        string res = "";
        string Status = "Report Submission";
        string qry = "{";

        qry += "\"select\":{\"?case\":["
          + "\"caseno\","
          + "\"agencyname\","
          + "\"agencyreferanceno\","
          + "\"updateddate\","
          + "\"caseassign_userid\","
           + "\"status\""
          + "]},"
         + "\"where\":[["
         + "\"?case\",\"evidence_acceptancedetails/inst_code\",\"" + Inst_Code + "\""
         + "],["
         + "\"?case\",\"evidence_acceptancedetails/department_code\",\"" + Dept_Code + "\""
         + "],["
         + "\"?case\",\"evidence_acceptancedetails/status\",\"" + Status + "\""
         + "]";

        if (User != "")
        {
            qry += ",["
            + "\"?case\",\"evidence_acceptancedetails/caseassign_userid\",\"" + User + "\""
             + "]";
        }
        qry += "]";
        qry += "}";

        res = sendTransaction(qry, serverqryurl);

        return res;
    }


    public string GetHashFromCreateUserForPromo(string PromotionLetterHash)
    {

        string res = "{"
            + "\"select\":{\"?user\":["
            + "\"firstname\","
            + "\"lastname\","
            + "\"username\","
            + "\"designation\","
             + "\"appointmentletter\","
            + "\"promotionletter\","
            + "\"appointmentletterrhash\","
            + "\"promotionletterhash\""
            + "]},"
            + "\"where\":[";

        res += "["
        + "\"?user\",\"userdetails/promotionletterhash\",\"" + PromotionLetterHash + "\"]";
        res += ",["
        + "\"?user\",\"userdetails/promotionletterhash\",\"?hash\"]";
        res += "],"
         + "\"filter\":[\" (not= ?hash \\\"\\\")\"]"

        + "}";


        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }


    public string GetRoleByUserData(string Role)
    {
        string res = "";
        string qry = "{";
        qry += "\"select\": {";
        qry += "\"?user\":";
        qry += "[";
        qry += "\"userid\",";
        qry += "\"firstname\",";
        qry += "\"lastname\",";
        qry += "\"username\",";
        qry += "\"password\",";
        qry += "\"designation\",";
        qry += "\"status\",";
        qry += "{\"role_id\":[\"role\"]},";
        qry += "{\"inst_id\":[\"inst_name\",\"inst_code\",\"inst_id\",\"location\"]},";
        qry += "{\"dept_id\":[\"dept_name\",\"dept_code\",\"dept_id\"]},";
        qry += "{\"div_id\":[\"div_name\",\"div_id\",\"div_code\"]}";
        qry += "]";
        qry += "},";
        qry += "\"where\": [[\"?user\", "
            + "\"userdetails/role_id\",["
            + "\"role/role_id\", \"" + Role + "\"]"
            + "]]";
        qry += "}";

        res = sendTransaction(qry, serverqryurl);

        return res;
    }
    public string ChangePassword(string userid, string password)
    {
        string res = "";

        string qry = "[{";
        qry += "\"_id\":[\"userdetails/userid\",\"" + userid
            + "\"],\"password\":\"" + password
            + "\"";
        qry += "}]";
        res = sendTransaction(qry, servertranurl);

        return res;
    }

    //public string GetRoles()
    //{
    //    string res = "{"
    //      + "\"select\":{\"?role\":["
    //      + "\"role_id\","
    //      + "\"role\""
    //      + "]},"
    //      + "\"where\":[[\"?role\",\"role/role\",\"?name\"],"
    //      + "[\"?role\",\"role/is_deleted\",false]"
    //      + "],"
    //      + "\"orderBy\":\"?name\""
    //      + "}";


    //    string resp = sendTransaction(res, serverqryurl);

    //    return resp;
    //}

    //public string GetRole(string role_id)
    //{
    //    string res = "{\"select\":["
    //        + "\"role_id\","
    //        + "\"role\""
    //        + "],";
    //    res += "\"from\":\"role\"";

    //    res += "}";

    //    string resp = sendTransaction(res, serverqryurl);

    //    return resp;
    //}



    public string GetRights(string role)
    {
        string res = "";

        string qry = "{";

        qry += "\"select\":{"
            + "\"?role\":["
           + "\"rolerights\","
          + "{\"role_id\":[\"role\",\"role_id\"]}"
            + "]"
            + "},"
            + "\"where\":[["
            + "\"?role\",\"rolerights/role_id\",[\"role/role_id\",\"" +
            role + "\"]]]";
        //+ ",[\"?role\",\"rolerights/is_deleted\",false]"
        //+ "]]";


        qry += "}";

        res = sendTransaction(qry, serverqryurl);

        return res;
    }
    public string InsertRights(string rights, string role)
    {
        string res = "";
        string qry = "[{\"_id\":\"rolerights\","
            + "\"role_id\":[\"role/role_id\",\"" + role
                + "\"],\"rolerights\":\"" + rights
                + "\"}]";

        //string qry = "[{\"_id\":\"rolerights\","
        //      + "\"role_id\":[\"role/role_id\",\"" + role + "\"]
        //      + "\",\"rolerights\":\"" + rights + "\""
        //      + "}]";

        res = sendTransaction(qry, servertranurl);

        return res;
    }
    public string UpdateRights(string rights, string role)
    {
        string res = "";

        string qry = "[{";

        qry += "\"_id\":[\"rolerights/role_id\",[\"role/role_id\",\"" + role + "\"]],"
            + "\"rolerights\":\"" + rights + "\"";

        qry += "}]";
        res = sendTransaction(qry, servertranurl);

        return res;
    }

    public string UpdateBasicDetails(string userid, string firstname, string lastname, string designation,
      string AppoitmentLetter, string PromotionLetter, string AppoitmentLetterHash,
      string PromotionLetterHash, string Email, string ProfileImg, string MobileNo)
    {
        string res = "[{";

        res += "\"_id\":[\"userdetails/userid\",\"" + userid + "\"],"
                 + "\"profileimage\":\"" + ProfileImg + "\","
                 + "\"firstname\":\"" + firstname + "\","
                 + "\"mobileno\":\"" + MobileNo + "\","
                 + "\"email\":\"" + Email + "\","
                 + "\"designation\":\"" + designation + "\","
                 + "\"lastname\":\"" + lastname + "\","
                 + "\"appointmentletterrhash\":\"" + AppoitmentLetterHash + "\","
                 + "\"appointmentletter\":\"" + AppoitmentLetter + "\","
                 + "\"promotionletter\":\"" + PromotionLetter + "\","
                 + "\"promotionletterhash\":\"" + PromotionLetterHash + "\","
                 + "\"updateddate\":" + ConvertToTimestamp(DateTime.Now);

        res += "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string GetIndexCount(string deptcode, string user)
    {
        string res = "";

        string qry = "{";

        qry += "\"TotalCases\":{"
         + "\"select\":\"(count ?case)\","
         + "\"where\":[["
         + "\"?case\",\"evidence_acceptancedetails/department_code\",\"" + deptcode + "\""
         + "]";

        if (user != "")
        {
            qry += ",["
            + "\"?case\",\"evidence_acceptancedetails/caseassign_userid\",\"" + user + "\""
             + "]";
        }
        qry += "]";
        qry += "},";

        qry += "\"PendingCase\":{"
           + "\"select\":\"(count ?pending)\","
           + "\"where\":[["
            + "\"?pending\",\"evidence_acceptancedetails/status\",\"Pending\""
            + "],["
           + "\"?pending\",\"evidence_acceptancedetails/department_code\",\"" + deptcode + "\""
           + "]";

        if (user != "")
        {
            qry += ",["
            + "\"?pending\",\"evidence_acceptancedetails/caseassign_userid\",\"" + user + "\""
             + "]";
        }
        qry += "]";
        qry += "},";


        qry += "\"Preparation\":{"
         + "\"select\":\"(count ?prepare)\","
         + "\"where\":[["
          + "\"?prepare\",\"evidence_acceptancedetails/status\",\"Report Preparation\""
          + "],["
         + "\"?prepare\",\"evidence_acceptancedetails/department_code\",\"" + deptcode + "\""
         + "]";
        if (user != "")
        {
            qry += ",["
            + "\"?prepare\",\"evidence_acceptancedetails/caseassign_userid\",\"" + user + "\""
             + "]";
        }
        qry += "]";
        qry += "},";

        qry += "\"Completed\":{"
        + "\"select\":\"(count ?complete)\","
        + "\"where\":[["
         + "\"?complete\",\"evidence_acceptancedetails/status\",\"Report Submission\""
         + "],["
        + "\"?complete\",\"evidence_acceptancedetails/department_code\",\"" + deptcode + "\""
        + "]";
        if (user != "")
        {
            qry += ",["
            + "\"?complete\",\"evidence_acceptancedetails/caseassign_userid\",\"" + user + "\""
             + "]";
        }
        qry += "]";
        qry += "}";


        qry += "}";
        res = sendTransaction(qry, servermulqryurl);

        return res;
    }

    public string GetAllDeptCodeWiseCount(string DeptCode, string InstCode, string CurrentDate,
        string FromDate, string ToDate)
    {

        string res = "";

        string qry = "{";

        qry += "\"TotalCases\":{"
     + "\"select\":\"(count ?total)\","
     + "\"where\":["
      + "["
      + "\"?total\",\"evidence_acceptancedetails/inst_code\",\"" + InstCode + "\""
      + "],["
      + "\"?total\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
      + "]";
        qry += "]";
        qry += "},";

        //qry += "\"Assigned\":{"
        //   + "\"select\":\"(count ?assign)\","
        //   + "\"where\":[["
        //    + "\"?assign\",\"evidence_acceptancedetails/status\",\"Assigned\""
        //    + "],["
        //    + "\"?assign\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
        //    + "]";
        //qry += "],\"group By\":[\"department_code\",\"status\"]";
        //qry += "},";

        qry += "\"Pending\":{"
            + "\"select\":\"(count ?pending)\","
            + "\"where\":[["
             + "\"?pending\",\"evidence_acceptancedetails/status\",\"Pending\""
             + "],["
            + "\"?pending\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
            + "]";
        qry += "],\"group By\":[\"department_code\",\"status\"]";
        qry += "},";

        // qry += "\"Preparation\":{"
        //  + "\"select\":\"(count ?prepare)\","
        //  + "\"where\":[["
        //   + "\"?prepare\",\"evidence_acceptancedetails/status\",\"Report Preparation\""
        //   + "],["
        //     + "\"?prepare\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
        //     + "]";
        // qry += "],\"group By\":[\"department_code\",\"status\"]";
        // qry += "},";

        // qry += "\"Signature\":{"
        //+ "\"select\":\"(count ?sign)\","
        //+ "\"where\":[["
        // + "\"?sign\",\"evidence_acceptancedetails/status\",\"Pending for Director/HOD Signature\""
        // + "],["
        //     + "\"?sign\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
        //     + "]";
        // qry += "],\"group By\":[\"department_code\",\"status\"]";
        // qry += "},";

        // qry += "\"PendingForAssign\":{"
        //    + "\"select\":\"(count ?pendingassign)\","
        //    + "\"where\":[["
        //     + "\"?pendingassign\",\"evidence_acceptancedetails/status\",\"Pending for Assign\""
        //     + "],["
        //     + "\"?pendingassign\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
        //     + "]";
        // qry += "],\"group By\":[\"department_code\",\"status\"]";
        // qry += "},";

        qry += "\"Submitted\":{"
       + "\"select\":\"(count ?submit)\","
       + "\"where\":[["
        + "\"?submit\",\"evidence_acceptancedetails/status\",\"Report Submission\""
        + "],["
        + "\"?submit\",\"evidence_acceptancedetails/createddate\",\"?date\""
        + "],["
            + "\"?submit\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
            + "]]";
        if (FromDate != "" && ToDate != "")
        {
            qry += ",\"filter\": [\"(and (<= " + FromDate + " ?date) (>= " + ToDate + " ?date))\"]";

        }
        else
        {
            qry += ",\"filter\": [\"(and (<= " + CurrentDate + " ?date) (>= " + ConvertToTimestamp(DateTime.Now) + " ?date))\"]";
        }
        qry += ",\"group By\":[\"department_code\",\"status\"]";
        qry += "}";

        qry += "}";
        res = sendTransaction(qry, servermulqryurl);

        return res;
    }
    //public string GetAllDeptCodeWiseCount(string DeptCode)
    //{
    //    string res = "";

    //    string qry = "{";

    //    qry += "\"Assigned\":{"
    //       + "\"select\":\"(count ?assign)\","
    //       + "\"where\":[["
    //        + "\"?assign\",\"evidence_acceptancedetails/status\",\"Assigned\""
    //        + "],["
    //        + "\"?assign\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
    //        + "]";
    //    qry += "],\"group By\":[\"department_code\",\"status\"]";
    //    qry += "},";

    //    qry += "\"Pending\":{"
    //        + "\"select\":\"(count ?pending)\","
    //        + "\"where\":[["
    //         + "\"?pending\",\"evidence_acceptancedetails/status\",\"Pending\""
    //         + "],["
    //        + "\"?pending\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
    //        + "]";
    //    qry += "],\"group By\":[\"department_code\",\"status\"]";
    //    qry += "},";

    //    qry += "\"Preparation\":{"
    //     + "\"select\":\"(count ?prepare)\","
    //     + "\"where\":[["
    //      + "\"?prepare\",\"evidence_acceptancedetails/status\",\"Report Preparation\""
    //      + "],["
    //        + "\"?prepare\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
    //        + "]";
    //    qry += "],\"group By\":[\"department_code\",\"status\"]";
    //    qry += "},";

    //    qry += "\"Signature\":{"
    //   + "\"select\":\"(count ?sign)\","
    //   + "\"where\":[["
    //    + "\"?sign\",\"evidence_acceptancedetails/status\",\"Pending for Director/HOD Signature\""
    //    + "],["
    //        + "\"?sign\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
    //        + "]";
    //    qry += "],\"group By\":[\"department_code\",\"status\"]";
    //    qry += "},";

    //    qry += "\"PendingForAssign\":{"
    //       + "\"select\":\"(count ?pendingassign)\","
    //       + "\"where\":[["
    //        + "\"?pendingassign\",\"evidence_acceptancedetails/status\",\"Pending for Assign\""
    //        + "],["
    //        + "\"?pendingassign\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
    //        + "]";
    //    qry += "],\"group By\":[\"department_code\",\"status\"]";
    //    qry += "},";

    //    qry += "\"Submitted\":{"
    //   + "\"select\":\"(count ?submit)\","
    //   + "\"where\":[["
    //    + "\"?submit\",\"evidence_acceptancedetails/status\",\"Report Submission\""
    //    + "],["
    //        + "\"?submit\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
    //        + "]";
    //    qry += "],\"group By\":[\"department_code\",\"status\"]";
    //    qry += "}";

    //    qry += "}";
    //    res = sendTransaction(qry, servermulqryurl);

    //    return res;
    //}

    public string GetIndexCountPsy(string userid)
    {
        string res = "";

        string qry = "{";

        qry += "\"Case\":{"
            + "\"select\":\"(count ?case)\","
            + "\"where\":[[";
        if (userid == "-1")
            qry += "\"?c\",\"usercase/case_id\",\"?case\"],[\"?c\"";
        else
            qry += "\"?case\",\"usercase/userid\",["
                + "\"userdetails/userid\",\"" + userid + "\""
                + "]],[\"?case\"";
        qry += ",\"usercase/is_deleted\",false]"
          + "]"
            + "},";

        qry += "\"HD\":{"
           + "\"select\":\"(count ?case)\","
           + "\"where\":[[";
        string vari = "";
        if (userid == "-1")
        {
            qry += "\"?c\",\"usercase/case_id\",\"?case\"";
            vari = "c";
        }
        else
        {
            qry += "\"?case\",\"usercase/userid\",["
                + "\"userdetails/userid\",\"" + userid + "\""
                + "]";
            vari = "case";
        }
        qry += "],["
            + "\"?" + vari + "\",\"usercase/status\",\"In Process\""
            + "],"
          + "[\"?" + vari + "\",\"usercase/is_deleted\",false]"
          + "]";
        qry += "},";


        qry += "\"User\":{"
           + "\"select\":\"(count ?case)\","
           + "\"where\":[[";
        //if (userid != "-1")
        qry += "\"?c\",\"userdetails/userid\",\"?case\"";
        //else
        //    qry += "\"?case\",\"case_master/userid\",["
        //        + "\"userdetails/userid\",\"" + userid + "\""
        //        + "]";
        qry += "],"
          + "[\"?case\",\"userdetails/is_deleted\",false]"
          + "]"
            + "}";


        //qry += "\"Case\":{"
        //   + "\"select\":\"(count ?case)\","
        //   + "\"where\":[[";
        //if (userid != "-1")
        //    qry += "\"?c\",\"case_master/case_id\",\"?case\"";
        //else
        //    qry += "\"?case\",\"case_master/userid\",["
        //        + "\"userdetails/userid\",\"" + userid + "\""
        //        + "]";
        //qry += "]]"
        //    + "},";


        qry += "}";
        res = sendTransaction(qry, servermulqryurl);

        return res;
    }

    public string GetDivById(string DeptId)
    {
        string res = "{"
          + "\"select\":{\"?div\":["
          + "\"div_id\","
          + "\"div_name\""
          + "]},"
          + "\"where\":[[\"?div\",\"division_master/div_name\",\"?name\"],"
          + "[\"?div\",\"division_master/dept_id\",[\"department_master/dept_id\",\"" +
          DeptId + "\"]],"
          + "[\"?div\",\"division_master/is_deleted\",false]"
          + "],"
          + "\"orderBy\":\"?name\""
          + "}";
        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string InsertRole(string role_id, string role, string roledesc)
    {
        string res = "[{\"_id\":\"role\","
                + "\"role_id\":\"" + role_id
                + "\",\"role\":\"" + role
                + "\",\"roledesc\":\"" + roledesc
                + "\",\"is_deleted\":false"
                + "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string UpdateRole(string role_id, string role, string roledesc)
    {
        string res = "[{\"_id\":[\"role/role_id\",\"" + role_id
                + "\"],\"role\":\"" + role
                + "\",\"roledesc\":\"" + roledesc
                + "\"}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string InsertRolePsy(string role_id, string role)
    {
        string res = "[{\"_id\":\"role\","
                + "\"role_id\":\"" + role_id
                + "\",\"role\":\"" + role
                + "\",\"is_deleted\":false"
                + "}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string UpdateRolePsy(string role_id, string role)
    {
        string res = "[{\"_id\":[\"role/role_id\",\"" + role_id
                + "\"],\"role\":\"" + role
                + "\"}]";

        string resp = sendTransaction(res, servertranurl);

        return resp;
    }

    public string DeleteRole(string id)
    {
        string res = "[{\"_id\":[\"role/role_id\",\"" + id
             + "\"],\"_action\":\"delete\"}]";
        string resp = sendTransaction(res, servertranurl);

        return resp;
    }
    public string GetDivision(string id)
    {
        string res = "{"
          + "\"select\":{\"?div\":["
          + "\"div_id\","
          + "\"div_name\""
          + ",\"div_code\""
          + ",{\"inst_id\":[\"inst_id\",\"inst_name\"]}"
          + ",{\"dept_id\":[\"dept_id\",\"dept_name\"]}"
          + "]},"
          + "\"where\":[[\"?div\",\"division_master/div_name\",\"?divname\"],"
          + "[\"?div\",\"division_master/is_deleted\",false]";
        if (id != "-1")
        {
            res += ",[\"?div\",\"division_master/div_id\",\"" + id + "\"]";
        }
        res += "],"
         + "\"orderBy\":\"?divname\""
        + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }

    public string GetRoleByID(string role_id)
    {
        string res = "";

        string qry = "{";

        qry += "\"select\":{"
            + "\"?role\":["
            + "\"role_id\""
            + ",\"role\""
            + ",\"roledesc\"]"
            + "},"
            + "\"where\":[[\"?role\",\"role/role\",\"?name\"],";
        if (role_id != "-1")
            qry += "["
            + "\"?role\","
            + "\"role/role_id\","
            + "\"" + role_id + "\""
            + "],";
        qry += "[\"?role\",\"role/is_deleted\",false]"
            + "],"
            + "\"orderBy\":\"?name\"";

        qry += "}";
        res = sendTransaction(qry, serverqryurl);

        return res;
    }

    public string GetDeptBycode(string IntsId)
    {
        string res = "{"
            + "\"select\":{\"?dept\":["
            + "\"dept_id\","
            + "\"dept_code\","
            + "\"dept_name\""
            + "]},"
            + "\"where\":[[\"?dept\",\"department_master/dept_name\",\"?name\"],"
            + "[\"?dept\",\"department_master/inst_id\",[\"institute_master/inst_code\",\"" +
            IntsId + "\"]],["
            + "\"?dept\",\"department_master/is_deleted\",false"
            + "]"
            + "],"
              + "\"filter\":[\"(and(not (= ?name \\\"ALL Department\\\"))(not (= ?name \\\"Other Sample Warden\\\")))\"],"
            + "\"orderBy\":\"?name\""
            + "}";

        string resp = sendTransaction(res, serverqryurl);

        return resp;
    }
    public string GetAllDeptCodeWiseCount_instwise(string Inst, string FromDate, string ToDate)
    {
        string DeptCode = "";
        string Deptname = "";
        string res = "";

        string TotalCases = "";
        string Submitted = "";
        string concatdata = "";
        string countdeptwise = "";
        string resdetails = "";
        string finalurl = "";
        FlureeCS fl = new FlureeCS();

        double fd = 0;
        double td = 0;
        fd = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(FromDate + " 00:00:00"));
        td = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(ToDate + " 23:59:59"));

        //finalurl = URL + query;

        //string resinst = fl.GetInstById(Inst);
        //if (!resinst.StartsWith("Error"))
        //{
        //    DataTable dt = fl.Tabulate(resinst);
        //    if (dt.Rows.Count > 0)
        //    {
        //        string instcode = dt.Rows[0]["inst_code"].ToString();
        //        if (instcode == "DFS-S")
        //        {
        //            URL = "";
        //        }
        //        else if (instcode == "DFS-A")
        //        {
        //            URL = "";
        //        }
        //        else if (instcode == "DFS-J")
        //        {
        //            URL = "";
        //        }
        //        else if (instcode == "DFS-B")
        //        {
        //            URL = "";
        //        }
        //        else if (instcode == "DFS-R")
        //        {
        //            URL = "";
        //        }
        //        else if (instcode == "DFS")
        //        {
        //            URL = "http://192.168.1.246:8090/fdb/ssb/dfs/multi-query";
        //        }
        //    }
        //    //else
        //    //{
        //    //    return "Data not Found..!";
        //    //}

        //}

        //if (URL == "")
        //{
        //    return "No URL Found..!";
        //}
        //else
        //{

        resdetails = fl.GetDeptBycode(Inst);
        if (!resdetails.StartsWith("Error"))
        {
            DataTable dt_data = fl.Tabulate(resdetails);
            if (dt_data.Rows.Count > 0)
            {
                for (int i = 0; i < dt_data.Rows.Count; i++)
                {

                    DeptCode = dt_data.Rows[i]["dept_code"].ToString();
                    Deptname = dt_data.Rows[i]["dept_name"].ToString();

                    string qry = "{";
                    qry += "\"TotalCases\":{"
                                   + "\"select\":\"(count ?total)\","
                                   + "\"where\":["
                                    + "["
                                    + "\"?total\",\"evidence_acceptancedetails/inst_code\",\"" + Inst + "\""
                                    + "],["
                                    + "\"?total\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
                                    + "]";
                    qry += "]";
                    qry += "},";

                    //qry += "\"Assigned\":{"
                    //   + "\"select\":\"(count ?assign)\","
                    //   + "\"where\":[["
                    //    + "\"?assign\",\"evidence_acceptancedetails/status\",\"Assigned\""
                    //    + "],["
                    //    + "\"?assign\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
                    //    + "]";
                    //qry += "],\"group By\":[\"department_code\",\"status\"]";
                    //qry += "},";

                    qry += "\"Pending\":{"
                        + "\"select\":\"(count ?pending)\","
                        + "\"where\":[["
                         + "\"?pending\",\"evidence_acceptancedetails/status\",\"Pending\""
                         + "],["
                        + "\"?pending\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
                        + "]";
                    qry += "],\"group By\":[\"department_code\",\"status\"]";
                    qry += "},";

                    // qry += "\"Preparation\":{"
                    //  + "\"select\":\"(count ?prepare)\","
                    //  + "\"where\":[["
                    //   + "\"?prepare\",\"evidence_acceptancedetails/status\",\"Report Preparation\""
                    //   + "],["
                    //     + "\"?prepare\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
                    //     + "]";
                    // qry += "],\"group By\":[\"department_code\",\"status\"]";
                    // qry += "},";

                    // qry += "\"Signature\":{"
                    //+ "\"select\":\"(count ?sign)\","
                    //+ "\"where\":[["
                    // + "\"?sign\",\"evidence_acceptancedetails/status\",\"Pending for Director/HOD Signature\""
                    // + "],["
                    //     + "\"?sign\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
                    //     + "]";
                    // qry += "],\"group By\":[\"department_code\",\"status\"]";
                    // qry += "},";

                    // qry += "\"PendingForAssign\":{"
                    //    + "\"select\":\"(count ?pendingassign)\","
                    //    + "\"where\":[["
                    //     + "\"?pendingassign\",\"evidence_acceptancedetails/status\",\"Pending for Assign\""
                    //     + "],["
                    //     + "\"?pendingassign\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
                    //     + "]";
                    // qry += "],\"group By\":[\"department_code\",\"status\"]";
                    // qry += "},";

                    qry += "\"Submitted\":{"
                   + "\"select\":\"(count ?submit)\","
                   + "\"where\":[["
                    + "\"?submit\",\"evidence_acceptancedetails/status\",\"Report Submission\""
                    + "],["
                    + "\"?submit\",\"evidence_acceptancedetails/createddate\",\"?date\""
                    + "],["
                        + "\"?submit\",\"evidence_acceptancedetails/department_code\",\"" + DeptCode + "\""
                        + "]]";
                    if (FromDate != "" && ToDate != "")
                    {
                        qry += ",\"filter\": [\"(and (<= " + fd + " ?date) (>= " + td + " ?date))\"]";

                    }
                    //else
                    //{
                    //    qry += ",\"filter\": [\"(and (<= " + CurrentDate + " ?date) (>= " + ConvertToTimestamp(DateTime.Now) + " ?date))\"]";
                    //}
                    qry += ",\"group By\":[\"department_code\",\"status\"]";
                    qry += "}";

                    qry += "}";

                    res = sendTransaction(qry, servermulqryurl);
                    //countdeptwise += res + "~";
                    concatdata += Deptname + "^" + res + "~";
                    //return res;
                    //if (!res.StartsWith("Error"))
                    //{

                    //    DataTable dtres  = fl.Tabulate(res);
                    //    if (dtres.Rows.Count > 0)
                    //    {

                    //        if (dtres.Columns.Contains("TotalCases"))
                    //            TotalCases = dtres.Rows[0]["TotalCases"].ToString();


                    //        if (dtres.Columns.Contains("Submitted"))
                    //            Submitted = dtres.Rows[0]["Submitted"].ToString();

                    //        concatdata += Deptname + "~" + TotalCases + "~" + Submitted + "^";

                    //    }


                    //    }
                }
            }

            //String[] splitconcatdata = concatdata.Split('');
        }

        //}
        if (!res.StartsWith("Error") || !resdetails.StartsWith("Error"))
        {
            return concatdata;
        }
        else
        {
            return "0";
        }
        //string finalres = "";
        //if(!res.StartsWith("Error"))
        //{
        //    finalres = "1";
        //}
        //else
        //{
        //    finalres = "0";
        //}


    }

    public string GetRoleByRole(string role, string ChkRole, string ChkRole1, string ChkRole2, string ChkRole3, string ChkRole4)
    {
        string res = "";

        string qry = "{";

        qry += "\"select\":{"
            + "\"?role\":["
            + "\"role_id\""
            + ",\"role\""
            + ",\"roledesc\"]"
            + "},"
            + "\"where\":[[\"?role\",\"role/role\",\"?name\"],";

        if (role == "Admin")
        {
            qry += "{\"filter\":[\" (and(not= ?name \\\"" + ChkRole + "\\\")(not= ?name \\\"SuperAdmin\\\"))\"]},";
        }

        if (role == "Additional Director")
        {
            qry += "{\"filter\":[\" (and(not= ?name \\\"" + ChkRole + "\\\")(not= ?name \\\""
            + ChkRole1 + "\\\")(not= ?name \\\"SuperAdmin\\\"))\"]},";
        }

        if (role == "Assistant Director")
        {
            qry += "{\"filter\":[\" (and(not= ?name \\\"" + ChkRole + "\\\")(not= ?name \\\""
                + ChkRole1 + "\\\")(not= ?name \\\"" + ChkRole2 + "\\\")(not= ?name \\\"SuperAdmin\\\"))\"]},";
        }

        if (role == "Deputy Director")
        {
            qry += "{\"filter\":[\" (and(not= ?name \\\"" + ChkRole + "\\\")(not= ?name \\\""
                + ChkRole1 + "\\\")(not= ?name \\\"" + ChkRole2 + "\\\")(not= ?name \\\""
                + ChkRole3 + "\\\")(not= ?name \\\"SuperAdmin\\\"))\"]},";
        }

        if (role == "Department Head")
        {
            qry += "{\"filter\":[\" (and(not= ?name \\\"" + ChkRole + "\\\")(not= ?name \\\""
                + ChkRole1 + "\\\")(not= ?name \\\"" + ChkRole2 + "\\\")(not= ?name \\\""
                + ChkRole3 + "\\\")(not= ?name \\\"" + ChkRole4 + "\\\")(not= ?name \\\"SuperAdmin\\\"))\"]},";
        }

        qry += "[\"?role\",\"role/is_deleted\",false]"
            + "],"
            + "\"orderBy\":\"?name\"";

        qry += "}";
        res = sendTransaction(qry, serverqryurl);

        return res;
    }
    #endregion

    #endregion
}