#pragma checksum "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\_FullCalendarModalPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d390991e0c83d9bc6d8f5d347a857ebff823b135"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__FullCalendarModalPartial), @"mvc.1.0.view", @"/Views/Shared/_FullCalendarModalPartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d390991e0c83d9bc6d8f5d347a857ebff823b135", @"/Views/Shared/_FullCalendarModalPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dcb4b5e26a5386507a874264fc78bac08f1ee6e8", @"/_ViewImports.cshtml")]
    public class Views_Shared__FullCalendarModalPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""row"">
    <div class=""col-12"" id=""calender""></div>
</div>

<div id=""myModal"" class=""modal fade"" role=""dialog"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title""><span id=""groupName""></span></h4>
            </div>
            <div class=""modal-body"">
                <p id=""pDetails""> </p>
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-primary"" data-dismiss=""modal"">Close</button>
            </div>
        </div>
    </div>
</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
