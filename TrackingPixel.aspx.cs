using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contents_TrackingPixel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //str_Text adds on the latest e-mail respondent to the list of respondents already on the tracking list.
        string str_Text = System.IO.File.ReadAllText(Server.MapPath(".") + @"\TrackingPixel.txt") + 
            HttpContext.Current.Request.Params[0] + ";" +
            HttpContext.Current.Request.Params[1] + ";" +
            HttpContext.Current.Request.Params[2] + ";" + 
            helper_GetDateTimeNow()+
			Environment.NewLine;
        System.IO.File.WriteAllText((Server.MapPath(".") + @"\TrackingPixel.txt"), str_Text);
        //db_AddData(HttpContext.Current.Request.Params[0], HttpContext.Current.Request.Params[1], HttpContext.Current.Request.Params[2], helper_GetDateTimeNow());
        Response.Redirect("TrackingPixel.bmp", false);

    }
    public static string helper_GetDateTimeNow()
    {
        System.Net.WebClient client = new System.Net.WebClient();
        string str_DownloadedText = client.DownloadString("https://www.timeanddate.com/worldclock/singapore/singapore");
        string str_Date = helper_GetStringBetweenTwoStrings(str_DownloadedText, @"<span id=ctdat>", @"</span>"); //.Split(',')[1].Trim()
        string str_Time = helper_GetStringBetweenTwoStrings(str_DownloadedText, @"<span id=ct class=h1>", @"</span>");
        return (str_Date + "," + str_Time);
    }
    public static string helper_GetStringBetweenTwoStrings(string str_FullString, string str_StartString, string str_EndString)
    {
        //Test with System.Diagnostics.Debug.WriteLine(CLS_Generic.Chp05_7GetStringBetweenTwoStrings("<html><p>Trial Test</p></html>", "<p>", "</p>"));
        return str_FullString.Split(new string[] { str_StartString }, StringSplitOptions.None)[1].Split(new string[] { str_EndString }, StringSplitOptions.None)[0].Trim();
    }

    protected void db_AddData(string str_Email, string str_DateSent, string str_Campaign, string str_DateRead)
    {
        //string str_DBPathName = Server.MapPath(".") + @"\TrackingPixel.accdb";
        //string str_ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + str_DBPathName;
        //System.Data.OleDb.OleDbConnection con = new System.Data.OleDb.OleDbConnection(str_ConnectionString);
        //System.Data.OleDb.OleDbCommand cmd = con.CreateCommand();
        //con.Open();
        //cmd.CommandText = "Insert into CampaignResult(Email,DateSent,Campaign,DateRead)Values('" + str_Email + "','" + str_DateSent + "','" + str_Campaign +"','" + str_DateRead + "')";
        //cmd.Connection = con;
        //cmd.ExecuteNonQuery();
        //p_status.InnerText = ("Record Submitted, congrats");
        //con.Close();
    }    
}