#pragma checksum "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e8576ff8908113518d2471b2bf196ed23b862ab4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Business_Index), @"mvc.1.0.view", @"/Views/Business/Index.cshtml")]
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
#line 1 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\Index.cshtml"
using PlanShift.Web.ViewModels.Business;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8576ff8908113518d2471b2bf196ed23b862ab4", @"/Views/Business/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dcb4b5e26a5386507a874264fc78bac08f1ee6e8", @"/_ViewImports.cshtml")]
    public class Views_Business_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BusinessIndexViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-info float-right"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ShiftChange", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "All", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ShiftApplication", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_FullCalendarModalPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            DefineSection("Styles", async() => {
                WriteLiteral(@"
    <link href=""//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.4.0/fullcalendar.min.css"" rel=""stylesheet""/>
    <link href=""//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.4.0/fullcalendar.print.css"" rel=""stylesheet"" media=""print""/>
    <link href=""css/custom.css"" rel=""stylesheet""/>
");
            }
            );
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-3\">\r\n        <div class=\"card\" style=\"width: 18rem;\">\r\n            <h5 class=\"text-center\">Pending Actions</h5>\r\n            <ul class=\"list-group list-group-flush\">\r\n                <li class=\"list-group-item\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e8576ff8908113518d2471b2bf196ed23b862ab45848", async() => {
#nullable restore
#line 16 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\Index.cshtml"
                                                                                                                                                                          Write(Model.ShiftChangesCount);

#line default
#line hidden
#nullable disable
                WriteLiteral(" Swap requests");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-businessId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 16 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\Index.cshtml"
                                                                                                                                                WriteLiteral(Model.BusinessId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["businessId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-businessId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["businessId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</li>\r\n                <li class=\"list-group-item\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e8576ff8908113518d2471b2bf196ed23b862ab48883", async() => {
#nullable restore
#line 17 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\Index.cshtml"
                                                                                                                                                                               Write(Model.ShiftApplicationsCount);

#line default
#line hidden
#nullable disable
                WriteLiteral(" Shifts requests");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-businessId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 17 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\Index.cshtml"
                                                                                                                                                     WriteLiteral(Model.BusinessId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["businessId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-businessId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["businessId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"</li>
            </ul>
        </div>
    </div>
</div>

<br/>
<div class=""d-flex justify-content-center"">
    <ul id=""shiftEventsCount"" class=""list-group list-group-horizontal-sm col-sm-9"">
        <li id=""upcomingShifts"" class=""list-group-item"">
            <span id=""upcomingShiftsCount"">0</span> Upcoming shifts
        </li>
        <li id=""openShifts"" class=""list-group-item"">
            <span id=""openShiftsCount"">0</span> Open shifts
        </li>
        <li id=""pendingShifts"" class=""list-group-item"">
            <span id=""pendingShiftsCount"">0</span> Swap requests
        </li>
    </ul>
</div>



");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e8576ff8908113518d2471b2bf196ed23b862ab412541", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js""></script>
    <script src=""//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.4.0/fullcalendar.min.js""></script>

    <script>
        $(document).ready(function() {
            var events = [];
            $.ajax({
                type: ""GET"",
                url: ""/api/GetEvents?businessId=");
#nullable restore
#line 51 "C:\Users\valni\Desktop\PlanShift-Repo\PlanShift\Web\PlanShift.Web\Views\Business\Index.cshtml"
                                           Write(Model.BusinessId);

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                success: function(data) {

                    if (data.upcomingShiftsCount != 0) {
                        $('#upcomingShiftsCount').text(data.upcomingShiftsCount);
                        $('#upcomingShiftsCount').css('color', '#00bfff');
                    }
                    if (data.openShiftsCount != 0) {
                        $('#openShiftsCount').text(data.openShiftsCount).css('color', '#FF0000');
                    }
                    if (data.pendingShiftsCount != 0) {
                        $('#pendingShiftsCount').text(data.pendingShiftsCount).css('color', '#9932cc');
                    }


                    $.each(data.allShiftsCalendar,
                        function(i, v) {
                            var color = null;
                            if (v.type == 1) {
                                color = '#00bfff';
                            } else if (v.type == 2) {
                                color = '#FF0000';
                      ");
                WriteLiteral(@"      } else if (v.type == 3) {
                                color = '#9932cc';
                            }
                            console.log(color);
                            events.push({
                                groupName: v.groupName,
                                description: v.description,
                                start: moment(v.start),
                                end: v.End != null ? moment(v.end) : null,
                                color: color,
                                allDay: false,
                                eventDisplay: 'auto'
                            });
                        });

                    GenerateCalender(events);
                },
                error:
                    function(error) {
                        alert('failed');
                    }
            });

            function GenerateCalender(events) {
                $('#calender').fullCalendar('destroy');
                $('#calender').f");
                WriteLiteral(@"ullCalendar({
                    contentHeight: 420,
                    defaultDate: new Date(),
                    timeFormat: 'HH:mm',
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month,basicWeek,basicDay'
                    },
                    eventLimit: true,
                    eventColor: '#378006',
                    events: events,
                    eventClick: function(calEvent, jsEvent, view) {
                        $('#myModal #groupName').text(calEvent.groupName);
                        var $description = $('<div/>');
                        $description.append($('<p/>').html('<b>Start:</b> ' + calEvent.start.format(""DD-MMM-YYYY HH:mm a"")));
                        if (calEvent.end != null) {
                            $description.append($('<p/>').html('<b>End:</b> ' + calEvent.end.format(""DD-MMM-YYYY HH:mm a"")));
                        }
                  ");
                WriteLiteral(@"      $description.append($('<p/>').html('<b>Description:</b>' + calEvent.description));
                        $('#myModal #pDetails').empty().html($description);

                        $('#myModal').modal();
                    }
                });
            }
        })
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BusinessIndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
