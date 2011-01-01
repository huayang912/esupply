using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.LocalSystem.Entity;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Utility;
using com.LocalSystem.Web;

public partial class _Main : PageBase
{

    private bool isHiddensmp = false;

    private bool hasPermission = false;

    private string language;

    //导航条多语言
    protected override void InitializeCulture()
    {
        this.language = this.CurrentUser.Language;

        if (this.language == null || this.language.Trim() == string.Empty)
        {
            this.language = System.Globalization.CultureInfo.CurrentCulture.Name;
        }
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.language);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.id_User.Value = this.CurrentUser.Code;

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "method",
            @"<script language='javascript' type='text/javascript'>window.onerror = function() { return true; };
              try{$('#floatdiv').css({'top':($(document).scrollTop())+40});}catch(err){}</script>", false);
    }

    protected override void OnPreInit(EventArgs e)
    {
        LoadUserThemes();
        base.OnPreInit(e);
    }

    protected override void OnInit(EventArgs e)
    {
        LoadModule();
        base.OnInit(e);
    }

    private void LoadUserThemes()
    {
        //从cookie中取出Theme
        if (Request.Cookies["ThemePage"] == null)
        {
            this.Page.Theme = "Default";
        }
        else
        {
            this.Page.Theme = Request.Cookies["ThemePage"].Value;
        }
    }

    private void LoadModule()
    {
        string mid = Request.Params["mid"];

        #region 权限认证
        if (this.CurrentUser.Code.ToLower() != "admin")
        {
            string url = "~/Main.aspx" + Request.Url.Query;
            if (this.CurrentUser != null)
            {
                if (!this.CurrentUser.HasPermission(url) && url != "~/Main.aspx")
                {
                    phModule.Controls.Add(Page.LoadControl("MasterData/Main.ascx"));
                    this.divFavorite.Visible = false;
                    return;
                }
            }
            else
            {
                phModule.Controls.Add(Page.LoadControl("MasterData/Main.ascx"));
                this.divFavorite.Visible = false;
                return;
            }
        }
        #endregion

        if (mid == null)
        {
            phModule.Controls.Add(Page.LoadControl("MainPage/Main.ascx"));
        }
        else
        {
            hasPermission = true;
            //加载Web Use Control
            //segment分4段,用__分隔,分别为mid,mp,ac,ap,多个参数之间用'_'
            string mp = string.Empty;
            string act = string.Empty;
            string ap = string.Empty;
            //用于隐藏smp
            string smp = string.Empty;

            #region 用于命名查询
            if (this.Session["ACT"] != null)
            {
                mid += "__act--" + this.Session["ACT"].ToString();
                Session.Contents.Remove("ACT");
                //this.divsmp.Visible = false;
            }
            if (this.Session["AP"] != null)
            {
                mid += "__ap--" + this.Session["AP"].ToString();
                Session.Contents.Remove("AP");
                //this.divsmp.Visible = false;
            }

            if (this.Session["Temp_Session_ACT"] != null)
            {
                Session.Contents.Remove("Temp_Session_ACT");
                //this.divsmp.Visible = false;
                this.isHiddensmp = true;
            }
            #endregion

            string[] segment = Regex.Split(mid, "__", RegexOptions.IgnoreCase);
            for (int i = 1; i < segment.Length; i++)
            {
                if (segment[i].Substring(0, segment[i].IndexOf("--")) == "mp")
                {
                    mp = segment[i].Substring(segment[i].IndexOf("--") + 2);
                    continue;
                }
                if (segment[i].Substring(0, segment[i].IndexOf("--")) == "act")
                {
                    act = segment[i].Substring(segment[i].IndexOf("--") + 2);
                    continue;
                }
                if (segment[i].Substring(0, segment[i].IndexOf("--")) == "ap")
                {
                    ap = segment[i].Substring(segment[i].IndexOf("--") + 2);
                    continue;
                }
                if (segment[i].Substring(0, segment[i].IndexOf("--")) == "smp")
                {
                    smp = segment[i].Substring(segment[i].IndexOf("--") + 2);
                    continue;
                }
            }

            LoadModule(segment[0], mp, act, ap);
            if (smp == "none")
            {
                this.divsmp.Visible = false;
                this.isHiddensmp = true;
            }
        }
    }

    private void LoadModule(string mid, string mp, string act, string ap)
    {
        string[] path = mid.Split('.');
        string sourceFile = string.Empty;

        foreach (string p in path)
        {
            sourceFile += p + "\\";
        }
        sourceFile += "Main.ascx";

        IDictionary<string, string> mpDic = new Dictionary<string, string>();
        IDictionary<string, string> apDic = new Dictionary<string, string>();

        if (mp != null && mp != string.Empty)
        {
            string[] splitedMp = mp.Split('_');
            foreach (string para in splitedMp)
            {
                mpDic.Add(para.Split('-')[0], para.Split('-')[1]);
            }
        }

        if (ap != null && ap != string.Empty)
        {
            string[] splitedAp = ap.Split('_');
            foreach (string para in splitedAp)
            {
                apDic.Add(para.Split('-')[0], para.Split('-')[1]);
            }
        }
        //    ArrayList pList = new ArrayList();
        //    if (mp != string.Empty)
        //    {
        //        pList.Add();
        //    }
        //    if (act != string.Empty)
        //    {
        //        pList.Add(act);
        //        pList.Add(ap.Split('_'));
        //    }
        //}
        //else
        //{
        //    phModule.Controls.Add(Page.LoadControl(sourceFile));

        //}

        UserControl uc = LoadControl(sourceFile, mpDic, act, apDic);
        phModule.Controls.Add(uc);
    }


    private UserControl LoadControl(string userControlPath, params Object[] constructorParameters)
    {
        List<Type> constParamTypes = new List<Type>();
        foreach (object constParam in constructorParameters)
        {
            constParamTypes.Add(constParam.GetType());
        }

        UserControl ctl = Page.LoadControl(userControlPath) as UserControl;

        // Find the relevant constructor
        ConstructorInfo constructor = ctl.GetType().BaseType.GetConstructor(constParamTypes.ToArray());

        //And then call the relevant constructor
        if (constructor == null)
        {
            //Find the relevant constructor in MainModlueBase
            constructor = ctl.GetType().BaseType.BaseType.GetConstructor(constParamTypes.ToArray());

            if (constructor == null)
            {
                throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
            }
        }

        constructor.Invoke(ctl, constructorParameters);

        // Finally return the fully initialized UC
        return ctl;
    }

    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);

        SiteMapPath smp = (SiteMapPath)SiteMapPath1;
        string hiddensmp = string.Empty;
        if (this.Session["Hiddensmp"] != null)
        {
            hiddensmp = this.Session["Hiddensmp"].ToString();
        }
        if (smp.Provider.CurrentNode == null || hiddensmp == "Hiddensmp" || isHiddensmp)
        {
            this.divsmp.Visible = false;
        }
        else
        {
            string title = smp.Provider.CurrentNode.Title;
            string key = smp.Provider.CurrentNode.Key;
            //this.Title = TheLanguageMgr.TranslateContent(title, this.language);
            this.id_Key.Value = key;

            string pageImage = smp.Provider.CurrentNode.Description;

        }
    }

    protected override void SavePageStateToPersistenceMedium(object state)
    {
        Pair pair;
        PageStatePersister persister = this.PageStatePersister;
        object viewState;
        if (state is Pair)
        {
            pair = (Pair)state;
            persister.ControlState = pair.First;
            viewState = pair.Second;
        }
        else
        {
            viewState = state;
        }

        LosFormatter formatter = new LosFormatter();
        StringWriter writer = new StringWriter();
        formatter.Serialize(writer, viewState);
        string viewStateStr = writer.ToString();
        byte[] data = Convert.FromBase64String(viewStateStr);
        byte[] compressedData = ViewStateHelper.Compress(data);
        string str = Convert.ToBase64String(compressedData);

        persister.ViewState = str;
        persister.Save();

    }

    protected override object LoadPageStateFromPersistenceMedium()
    {
        PageStatePersister persister = this.PageStatePersister;
        persister.Load();

        string viewState = persister.ViewState.ToString();
        byte[] data = Convert.FromBase64String(viewState);
        byte[] uncompressedData = ViewStateHelper.Decompress(data);
        string str = Convert.ToBase64String(uncompressedData);
        LosFormatter formatter = new LosFormatter();
        return new Pair(persister.ControlState, formatter.Deserialize(str));
    }

}
