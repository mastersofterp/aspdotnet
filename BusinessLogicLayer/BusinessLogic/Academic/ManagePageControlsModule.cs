using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// Summary description for ManageControlsModule
/// -------------------------------------------------------------------------------------------------
/// Created By : Yograj Chaple
/// Created On : 11-05-2023
/// Purpose    : to change proerties of page as per configuration.
/// version    : 1.0.0
/// -------------------------------------------------------------------------------------------------
/// 
/// </summary>
public static class ManagePageControlsModule
{
    public static void ManagePageControls( Page pageControl, string xmlControlFilePathWithFileName)
    {
        string filepath = "";
        DataSet dsxml = GetControlProperties(xmlControlFilePathWithFileName);
        FindControlIds(pageControl.Controls, dsxml);
    }


    private static DataSet GetControlProperties(string xmlControlFilePathWithFileName)
    {
        string xmlfilepath = xmlControlFilePathWithFileName; 
        string xmlString = System.IO.File.ReadAllText(xmlfilepath);
        StringReader xmlreader = new StringReader(xmlString);
        DataSet dsxml = new DataSet();
        dsxml.ReadXml(xmlreader);
        return dsxml;
    }

    private static void FindControlIds(ControlCollection controls, DataSet dsxml)
    {
        foreach (DataRow dRow in dsxml.Tables[0].Rows)
        {
            foreach (System.Web.UI.Control control in controls)
            {
                if (control is TextBox)
                {
                    TextBox ctrl = (TextBox)control;
                    if (dRow["name"].ToString() == ctrl.ID.ToString())
                    {
                        if (dRow["visible"].ToString().Equals("0"))
                        {
                            ctrl.Visible = false;
                        }
                        else
                        {
                            ctrl.Visible = true;
                        }
                        if (dRow["enable"].ToString().Equals("0"))
                        {
                            ctrl.Enabled = false;
                        }
                        else
                        {
                            ctrl.Enabled = true;
                        }
                    }
                }
                else if (control is Label)
                {
                    Label ctrl = (Label)control;
                    if (dRow["name"].ToString() == ctrl.ID.ToString())
                    {
                        if (dRow["visible"].ToString().Equals("0"))
                        {
                            ctrl.Visible = false;
                        }
                        else
                        {
                            ctrl.Visible = true;
                        }

                        if (dRow["enable"].ToString().Equals("0"))
                        {
                            ctrl.Enabled = false;
                        }
                        else
                        {
                            ctrl.Enabled = true;
                        }
                    }
                }
                else if (control is CheckBox)
                {
                    CheckBox ctrl = (CheckBox)control;
                    if (dRow["name"].ToString() == ctrl.ID.ToString())
                    {
                        if (dRow["visible"].ToString().Equals("0"))
                        {
                            ctrl.Visible = false;
                        }
                        else
                        {
                            ctrl.Visible = true;
                        }
                        if (dRow["enable"].ToString().Equals("0"))
                        {
                            ctrl.Enabled = false;
                        }
                        else
                        {
                            ctrl.Enabled = true;
                        }
                    }
                }
                else if (control is Panel)
                {
                    Panel ctrl = (Panel)control;
                    if (dRow["name"].ToString() == ctrl.ID.ToString())
                    {
                        if (dRow["visible"].ToString().Equals("0"))
                        {
                            ctrl.Visible = false;
                        }
                        else
                        {
                            ctrl.Visible = true;
                        }

                        if (dRow["enable"].ToString().Equals("0"))
                        {
                            ctrl.Enabled = false;
                        }
                        else
                        {
                            ctrl.Enabled = true;
                        }
                    }
                }
                else if (control.HasControls())
                {
                    FindControlIds(control.Controls, dsxml);
                }
            }
        }

    }
}