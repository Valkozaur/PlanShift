#pragma checksum "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\BusinessPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8b6c3bb9985831fb861f540ca24a5ed15a930ca6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Business_BusinessPage), @"mvc.1.0.view", @"/Views/Business/BusinessPage.cshtml")]
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
#line 1 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\BusinessPage.cshtml"
using PlanShift.Web.ViewModels.EmployeeGroup;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8b6c3bb9985831fb861f540ca24a5ed15a930ca6", @"/Views/Business/BusinessPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dcb4b5e26a5386507a874264fc78bac08f1ee6e8", @"/_ViewImports.cshtml")]
    public class Views_Business_BusinessPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ManagementListViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\BusinessPage.cshtml"
  
    this.ViewData["Title"] = "Management";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<table class=""table table-hover table-dark"">
    <thead>
    <tr>
        <th scope=""col"">Username</th>
        <th scope=""col"">Created shifts</th>
        <th scope=""col"">Managed shifts</th>
        <th scope=""col"">Actions</th>
    </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 18 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\BusinessPage.cshtml"
     foreach (var manager in Model.Managers)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 21 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\BusinessPage.cshtml"
           Write(manager.UserUsername);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 22 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\BusinessPage.cshtml"
           Write(manager.CreatedShiftsCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 23 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\BusinessPage.cshtml"
           Write(manager.ManagedShiftsCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td></td>\r\n        </tr>\r\n");
#nullable restore
#line 26 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\BusinessPage.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ManagementListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591