#pragma checksum "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a92c35279951d760fdb2bb3e63697d3c0f406dba"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_ShiftApplicationsPerGroup_Default), @"mvc.1.0.view", @"/Views/Shared/Components/ShiftApplicationsPerGroup/Default.cshtml")]
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
#line 1 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
using PlanShift.Web.ViewModels.Shift;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a92c35279951d760fdb2bb3e63697d3c0f406dba", @"/Views/Shared/Components/ShiftApplicationsPerGroup/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dcb4b5e26a5386507a874264fc78bac08f1ee6e8", @"/_ViewImports.cshtml")]
    public class Views_Shared_Components_ShiftApplicationsPerGroup_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShiftWithApplicationsListViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("custom-select custom-select-sm col-7"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "shiftApplicationId", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ShiftApplication", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Approve", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<table class=""table table-hover table-sm"">
    <thead>
        <tr>
            <th scope=""col"">Start Date</th>
            <th scope=""col"">End Date</th>
            <th scope=""col"">Position</th>
            <th scope=""col"">Bonus Payment</th>
            <th scope=""col"">Description</th>
            <th scope=""col"">Applications</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 16 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
         foreach (var shift in this.Model.ShiftsWithApplications)
        {
            var applications = shift.ShiftApplications.Select(x => new SelectListItem(x.EmployeeEmployeeFullName, x.Id));


#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 21 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
               Write(shift.Start.ToString("G"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 22 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
               Write(shift.End.ToString("G"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 23 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
               Write(shift.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 24 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
                Write(shift.BonusPayment == 0 ? "-" : shift.BonusPayment.ToString("0.0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n");
#nullable restore
#line 26 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
                     if (shift.Description == null)
                    {
                        //TODO: FIX The dropdown

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span>No description</span>\r\n");
#nullable restore
#line 30 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <div class=""dropdown"">
                            <button class=""btn btn-info btn-sm dropdown-toggle"" type=""button"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                Read
                            </button>
                            <div class=""dropdown-menu p-4"" style=""max-width: 200px;"">
                                <p class=""mb-0"">
                                    ");
#nullable restore
#line 39 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
                               Write(shift.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </p>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 43 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n                <td>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a92c35279951d760fdb2bb3e63697d3c0f406dba9623", async() => {
                WriteLiteral("\r\n                        <div class=\"row\">\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a92c35279951d760fdb2bb3e63697d3c0f406dba9956", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Name = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 48 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = applications;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            <input type=\"hidden\" name=\"businessId\" id=\"businessId\"");
                BeginWriteAttribute("value", " value=\"", 2226, "\"", 2251, 1);
#nullable restore
#line 49 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
WriteAttributeValue("", 2234, Model.BusinsesId, 2234, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n                            <button class=\"btn-sm btn-primary\" type=\"submit\">Assign</button>\r\n                        </div>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 55 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Shared\Components\ShiftApplicationsPerGroup\Default.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShiftWithApplicationsListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
