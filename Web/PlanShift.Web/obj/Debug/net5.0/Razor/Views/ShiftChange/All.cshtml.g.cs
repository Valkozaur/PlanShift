#pragma checksum "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\ShiftChange\All.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "39e1486a594da9de8c9bfa3a0125284d543d8ebf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ShiftChange_All), @"mvc.1.0.view", @"/Views/ShiftChange/All.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\_ViewImports.cshtml"
using PlanShift.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\_ViewImports.cshtml"
using PlanShift.Web.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\ShiftChange\All.cshtml"
using PlanShift.Web.ViewModels.Group;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"39e1486a594da9de8c9bfa3a0125284d543d8ebf", @"/Views/ShiftChange/All.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dcb4b5e26a5386507a874264fc78bac08f1ee6e8", @"/_ViewImports.cshtml")]
    public class Views_ShiftChange_All : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GroupListViewModel<GroupBasicInfoViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ShiftChange", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SwitchToTabs", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\ShiftChange\All.cshtml"
  
    this.ViewData["Title"] = "Change requests";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<h2>Shift Changes by group</h2>\r\n<ul class=\"nav nav-tabs\">\r\n");
#nullable restore
#line 11 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\ShiftChange\All.cshtml"
     foreach (var group in this.Model.Groups)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li role=\"presentation\"");
            BeginWriteAttribute("class", " class=\"", 299, "\"", 380, 2);
            WriteAttributeValue("", 307, "nav-link", 307, 8, true);
#nullable restore
#line 13 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\ShiftChange\All.cshtml"
WriteAttributeValue(" ", 315, Model.ActiveTabGroupId == @group.Id ? "active" : string.Empty, 316, 64, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "39e1486a594da9de8c9bfa3a0125284d543d8ebf5105", async() => {
#nullable restore
#line 13 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\ShiftChange\All.cshtml"
                                                                                                                                                                                                              Write(group.Name);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-activeTabGroupId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 13 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\ShiftChange\All.cshtml"
                                                                                                                                                                                            WriteLiteral(group.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["activeTabGroupId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-activeTabGroupId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["activeTabGroupId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</li>\r\n");
#nullable restore
#line 14 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\ShiftChange\All.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>\r\n\r\n<vc:-changes-per-group");
            BeginWriteAttribute("group-id", " group-id=\"", 539, "\"", 573, 1);
#nullable restore
#line 17 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\ShiftChange\All.cshtml"
WriteAttributeValue("", 550, Model.ActiveTabGroupId, 550, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></vc:-changes-per-group>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GroupListViewModel<GroupBasicInfoViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591