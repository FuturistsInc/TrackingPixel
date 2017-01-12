using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;  
public partial class AccessMaster : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        db_AddData("daniel@gmail.com", @"31/12/2015"); 
    }

    protected void db_AddData(string str_Email, string str_DateRead)
    {
        string str_DBPathName = Server.MapPath(".") + @"\PixelTracking.accdb";
        string str_ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + str_DBPathName;
        OleDbConnection con = new OleDbConnection(str_ConnectionString); 
        OleDbCommand cmd = con.CreateCommand();
        con.Open();
        cmd.CommandText = "Insert into CampaignResult(Email,DateRead)Values('" + str_Email + "','" + str_DateRead + "')";
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
        p_Status.InnerText = ("Record Submitted, congrats");
        con.Close();   
    }    
}