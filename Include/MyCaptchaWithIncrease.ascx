<%@ Control Language="C#" ClassName="MyCaptchaWithIncrease" AutoEventWireup="true" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Ajax.UI.Captcha" Assembly="Obout.Ajax.UI" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<%@ Import Namespace="System.Collections.Generic" %>
<script runat="server" language="c#">
    // default captcha image size
    protected int minWidth = 208;
    protected int minHeight = 80;
    // increased captcha image size
    protected int maxWidth = 400;
    protected int maxHeight = 150;
    // button images names
    protected string plusImage = "Plus";
    protected string minusImage = "Minus";
    // button images titles
    protected string plusImageTitle = "Increase the captcha image";
    protected string minusImageTitle = "Decrease the captcha image";
    /// <summary>
    /// inner captcha image
    /// </summary>
    protected myCaptchaImage InnerCaptchaImage = null;
    /// <summary>
    /// on page init
    /// </summary>
    protected void Page_Init(object sender, EventArgs e)
    {
        // trick with OboutTextBox in ASCX control
        CaptchaInputField.ID = this.ID + "_" + CaptchaInputField.ID;
        CaptchaValidator.ControlToValidate = CaptchaInputField.ID;
    }
    /// <summary>
    /// Add captcha image to the page
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        InnerCaptchaImage = new myCaptchaImage();
        // set its properties
        InnerCaptchaImage.ForeColor = System.Drawing.Color.Goldenrod;
        InnerCaptchaImage.TextLength = 7;
        InnerCaptchaImage.BackColor = System.Drawing.Color.WhiteSmoke;
        InnerCaptchaImage.TextBrush = BrushType.Horizontal;
        InnerCaptchaImage.BackBrush = BrushType.Solid;
        InnerCaptchaImage.BrushFillerColor = System.Drawing.Color.Goldenrod;
        InnerCaptchaImage.BackgroundNoise = NoiseLevel.Low;
        InnerCaptchaImage.LineNoise = NoiseLevel.Low;
        InnerCaptchaImage.FontFamily = "Times New Roman";
        InnerCaptchaImage.FontWarpLevel = NoiseLevel.Medium;
        InnerCaptchaImage.ID = "InnerCaptchaImage";
        InnerCaptchaImage.Width = new Unit(minWidth + 50, UnitType.Pixel);
        InnerCaptchaImage.Height = new Unit(minHeight, UnitType.Pixel);
        CaptchaImagePlace.Controls.Add(InnerCaptchaImage);
    }
    /// <summary>
    /// Some things before the control render 
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        // clear the input field
        CaptchaInputField.Text = "";
        // set 'onclick' of the "Generate a new image" button
        GenerateNew.Attributes["onclick"] =
            "$find('" + InnerCaptchaImage.ClientID + "').getNewImage();";
        // we need to add some client-side functionality when a new image was generated
        InnerCaptchaImage.OnClientImageChanged = ClientID + "_onImageChanged";

        // we need to add a custom client-side method to the CaptchaImage component
        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "UpdateCaptchaImage_" + InnerCaptchaImage.ClientID,
            "(function(){" +
            // function to run on page load
               "function l(){" +
            // set the flag: whether to increase on "Change size" button click
                   ClientID + "_whetherIncrease = " + (InnerCaptchaImage.Height.Value == minHeight).ToString().ToLower() + ";" +
            // get the CaptchaImage client-side component
                   "var captcha = $find('" + InnerCaptchaImage.ClientID + "');" +
            // add custom method to be called on "Change size" button click
                   "captcha._my_increase = " + ClientID + "_my_increase;" +
            // don't run it again
                   "Sys.Application.remove_load(l);" +
               "}" +
            // run the function above on page load
               "Sys.Application.add_load(l);" +
            "})();", true);

    }
    /// <summary>
    /// Validation group
    /// </summary>
    public string ValidationGroup
    {
        get { return CaptchaValidator.ValidationGroup; }
        set { CaptchaValidator.ValidationGroup = value; }
    }
    /// <summary>
    /// Whether to enable client-side validation
    /// </summary>
    public bool EnableClientScript
    {
        get { return CaptchaValidator.EnableClientScript; }
        set { CaptchaValidator.EnableClientScript = value; }
    }
    /// <summary>
    /// Inner CaptchaImage control
    /// </summary>
    public CaptchaImage CaptchaImage
    {
        get { return InnerCaptchaImage; }
    }

    /// <summary>
    /// Our Captcha Image - we can change the image size on postback
    /// </summary>
    protected class myCaptchaImage : CaptchaImage
    {
        /// <summary>
        /// Loads the client state data
        /// </summary>
        /// <param name="clientState"></param>
        protected override void LoadClientState(string clientState)
        {
            // call base method
            base.LoadClientState(clientState);
            // deserialize client state
            Dictionary<string, object> state = (Dictionary<string, object>)new JavaScriptSerializer().DeserializeObject(clientState);
            // Width and Height can be changed in client-side
            Width = new Unit((int)state["Width"]);
            Height = new Unit((int)state["Height"]);
        }
    }
</script>
<style type="text/css">
    #<%= MainTable.ClientID %> {
        border:1px solid gray;
        background-color: WhiteSmoke;
        border-radius: 5px;  
        -moz-border-radius: 5px;  
        -webkit-border-radius: 5px;
        align-items:center;
    }
</style>

<script type="text/javascript">
    // when a new image was generated
    function <%= ClientID %>_onImageChanged(sender, args) {
        // clear the textbox - restore its watermark
        <%= CaptchaInputField.ID %>.value(<%= CaptchaInputField.ID %>.WatermarkText);
    
    
    }

    // whether to increase flag 
    var <%= ClientID %>_whetherIncrease = false;

    function <%= ClientID %>_my_increase(increase) {
        // hidden input with component's state
        var clientStateField = this.get_clientStateField();
        // get the state as object
        var clientState = Sys.Serialization.JavaScriptSerializer.deserialize(clientStateField.value);

        // change the state's fields
        clientState.Width = <%= ClientID %>_whetherIncrease?<%= maxWidth.ToString() %>:<%= minWidth.ToString() %>;
        clientState.Height = <%= ClientID %>_whetherIncrease?<%= maxHeight.ToString() %>:<%= minHeight.ToString() %>;

        // serialize and save the modified state object
        clientStateField.value = Sys.Serialization.JavaScriptSerializer.serialize(clientState);

        // set flag: whether to change the "Change size" button image's properties on when new captcha image loaded
        this._customSetChangeButton = true;

        // generate a new image for the captcha
        this.getNewImage();
    }
</script>

<div runat="server" id="MainTable">
    <div class="row" >
         <div class="col-lg-12" runat="server" id="CaptchaImagePlace" align="center">

        <asp:Image runat="server" alt="" title="Generate a new image" ID="GenerateNew"
            ImageUrl="~/Images/refresh.png" Style="cursor: pointer;max-width:50px;"  ImageAlign="Right" CssClass="img-fluid" />
    </div>
    </div>

    <div class="row" >
        <div class="col-lg-12" align="center">
        <obout:OboutTextBox runat="server" ID="CaptchaInputField" Style="text-transform: uppercase; font-size: 20px;margin-left: -51px;"
            WatermarkText="Type the image code"
            FolderStyle="~/interface/styles/black_glass/OboutTextBox" CssClass="form-control"/>
    </div>
    </div>

    <div class="row" >
   
    <div class="col-lg-12" >
        <obout:CaptchaValidator runat="server" CaptchaImageID="InnerCaptchaImage"
            ErrorMessage="The code you entered is not valid." ID="CaptchaValidator"
            ControlToValidate="CaptchaInputField" Display="Dynamic" />
    </div>
</div>
</div>




<%--<table runat="server" id="MainTable" >
   
    <tr>
        <td valign="middle" ></td>
     <td valign="middle">
               
        </td>
    </tr>

    <tr>
        <td align="center">
          

        </td>
     
    </tr>
    <tr>
        <td colspan="2" align="center">
            
        </td>
    </tr>
</table>--%>